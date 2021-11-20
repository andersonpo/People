using Microsoft.AspNetCore.Mvc;
using People.Domain.Interfaces;
using People.Domain.Interfaces.Services;
using System.Net.Mime;
using System.Text.Json;

namespace People.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IApiNotification apiNotification;
        private readonly ILogService logService;

        public BaseController(IApiNotification apiNotification, ILogService logService)
        {
            this.apiNotification = apiNotification;
            this.logService = logService;
        }

        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult HandleResponse<T>(T response)
        {
            // delete methods
            if (response != null && typeof(bool) == response.GetType())
            {
                if (apiNotification.StatusCode == 0)
                {
                    apiNotification.StatusCode = ((bool)(object)response ? 200 : 404);
                }
            }

            var RequestPath = Request.Path;
            int statusCode = apiNotification.StatusCode;
            if (statusCode == 0) statusCode = 200;
            
            if (apiNotification.HasNotifications())
            {
                var notifications = apiNotification.GetFailures().ToList();
                logService.LogInformation($"[BaseController] Path: {RequestPath} - StatusCode: {statusCode} - notifications: {JsonSerializer.Serialize(notifications)}");
                if (statusCode == 400)
                    return BadRequest(notifications);
                else
                    return UnprocessableEntity(notifications);
            }

            logService.LogInformation($"[BaseController] Path: {RequestPath} - StatusCode: {statusCode} - notifications: {JsonSerializer.Serialize(response)}");
            switch (statusCode)
            {
                case 200:
                    return Ok(response);
                case 201:
                    return Created(RequestPath, response);
                case 400:
                    return BadRequest(response);
                case 404:
                    return NotFound(response);
                case 422:
                    return UnprocessableEntity(response);
                case 500:
                    {
                        var result = new ObjectResult(response);
                        result.StatusCode = 500;
                        return result;
                    }
                default:
                    {
                        var result = new ObjectResult(response);
                        result.StatusCode = statusCode;
                        return result;
                    }
            }

        }
    }
}
