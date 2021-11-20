using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;

namespace People.Infrastructure.Extensions.Perfomance
{
    public static class PerformanceRequestExtension
    {
        public static IServiceCollection AddCompressHttpCalls(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.Configure<BrotliCompressionProviderOptions>(brotliOptions =>
            {
                brotliOptions.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(gzipOptions =>
            {
                gzipOptions.Level = CompressionLevel.Fastest;
            });

            return services;
        }

        public static IServiceCollection AddJsonResponseConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                var serializerOptions = options.JsonSerializerOptions;
                serializerOptions.WriteIndented = true;
                serializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
    }
}
