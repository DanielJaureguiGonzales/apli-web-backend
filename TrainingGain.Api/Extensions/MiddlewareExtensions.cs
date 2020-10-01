using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TrainingGain.Api.Extensions
{
    public static class MiddlewareExtensions
    {
       public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c=> 
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title="TrainingGain API",
                    Version="v1",
                    Description= "TrainingGain API RESTful"
                });
                c.EnableAnnotations();
            });
            return services;

        }
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(c=>
            {
                c.RouteTemplate= "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "TraningGain API V1");
                c.RoutePrefix = "api-docs/v1";
            });
            return app;
        }
    }
}
