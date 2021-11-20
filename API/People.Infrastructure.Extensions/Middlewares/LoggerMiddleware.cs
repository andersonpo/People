using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using People.Domain.Interfaces.Services;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace People.Infrastructure.Extensions.Middlewares
{
    public class LoggerMiddleware : IMiddleware
    {
        private readonly ILogService logService;

        public LoggerMiddleware(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            HttpRequestRewindExtensions.EnableBuffering(context.Request);
            string requestBody = await GetRequestBody(context);
            string responseBody = string.Empty;

            Exception? error = null;
            Stream originalResponseBody = context.Response.Body;

            try
            {
                using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await next(context);

                memStream.Position = 0;
                responseBody = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalResponseBody);
            }
            catch (Exception ex)
            {
                error = ex;
                var info = new
                {
                    TraceIdentifier = context.TraceIdentifier,
                    ElapsedTimeString = stopWatch.Elapsed.ToString(),
                    ElapsedTimeSeconds = stopWatch.Elapsed.TotalSeconds,
                    Request = new
                    {
                        METHOD = context.Request.Method,
                        PATH = context.Request.Path.HasValue ? context.Request.Path.Value : "",
                        BODY = !string.IsNullOrWhiteSpace(requestBody) ? JsonSerializer.Deserialize<dynamic>(requestBody) : ""
                    },
                    ErrorMessage = ex.Message
                };
                logService.LogException($"[LoggerMiddleware] {JsonSerializer.Serialize(info)}", ex);
                                
                //await HandleExceptionResponse(context, ex);
            }
            finally
            {
                context.Response.Body = originalResponseBody;
                if (error != null)
                {
                    await HandleExceptionResponse(context, error);
                }
                stopWatch.Stop();
                LogRequestResponse(context, requestBody, responseBody, stopWatch.Elapsed);
            }            
        }

        private void LogRequestResponse(HttpContext context, string requestBody, string responseBody, TimeSpan elapsedTime)
        {
            if (context == null || context.Request == null || context.Response == null) return;

            if (context.Request.Path.HasValue)
            {
                var path = context.Request.Path.Value;
                if (path.Contains("/ui/resources") ||
                    path.Contains("/favicon") ||
                    path.Contains("/monitor") || 
                    path.Contains("/swagger") ||
                    path.Contains("/healthchecks-api") ||
                    path.Contains("/healthz"))
                    return;
            }

            var fullInfo = new
            {
                TraceIdentifier = context.TraceIdentifier,
                ElapsedTimeString = elapsedTime.ToString(),
                ElapsedTimeSeconds = elapsedTime.TotalSeconds,
                Request = new
                {
                    METHOD = context.Request.Method,
                    PATH = context.Request.Path.HasValue ? context.Request.Path.Value : "",
                    BODY = !string.IsNullOrWhiteSpace(requestBody) ? JsonSerializer.Deserialize<dynamic>(requestBody) : ""
                },
                Response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Body = !string.IsNullOrWhiteSpace(responseBody) ? JsonSerializer.Deserialize<dynamic>(responseBody) : ""
                }
            };

            var serializerOptions = new JsonSerializerOptions();
            serializerOptions.WriteIndented = false;
            serializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

            var json = JsonSerializer.Serialize(fullInfo, serializerOptions);
            logService.LogInformation($"[REQUEST/RESPONSE]: {@json}");
        }

        private async Task<string> GetRequestBody(HttpContext httpContext)
        {
            try
            {
                var body = string.Empty;

                if (httpContext.Request.ContentLength is null)
                    return body;

                HttpRequestRewindExtensions.EnableBuffering(httpContext.Request);
                Stream streamBody = httpContext.Request.Body;
                byte[] buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
                await httpContext.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
                body = Encoding.UTF8.GetString(buffer);
                streamBody.Seek(0, SeekOrigin.Begin);
                httpContext.Request.Body = streamBody;

                return body;

            }
            catch (Exception ex)
            {
                logService.LogException("[LoggerMiddleware.GetRequestBody]", ex);
                return string.Empty;
            }
        }

        private async Task HandleExceptionResponse(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Ops, encontrei um erro ao processar sua requisição",
                Status = StatusCodes.Status500InternalServerError,
                Type = ex.GetBaseException().GetType().Name,
                Detail = $"Erro fatal na aplicação, entre em contato com o suporte.\n Causa: {ex.Message}",
                Instance = context.Request.Path
            };

            context.Response.StatusCode = (int)problemDetails.Status;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
