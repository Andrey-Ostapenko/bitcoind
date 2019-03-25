using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BitCoind.Api.Infrastructure.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string version, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options
                    .SwaggerDoc($"v{version}",
                        new Info
                        {
                            Version = $"v{version}",
                            Title = "Client API"
                        });

                var xmlDocsPath = configuration.GetValue<string>("xml_docs");
                if (string.IsNullOrWhiteSpace(xmlDocsPath) == false)
                    options.IncludeXmlComments(xmlDocsPath);

                options.DescribeAllEnumsAsStrings();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(
            this IApplicationBuilder app,
            IHostingEnvironment env,
            string version
        )
        {
            var endpoint = $"/swagger/v{version}/swagger.json";

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(endpoint,
                    $"Client API v{version}");
                options.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}

