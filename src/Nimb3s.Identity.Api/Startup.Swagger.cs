using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Nimb3s.Identity.Api.OperationFilters;

namespace Nimb3s.Identity.Api
{
    public partial class Startup
    {
        /// <summary>
        /// Configure swagger services. Example https://codewithmukesh.com/blog/api-versioning-in-aspnet-core-3-1/
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddApiVersioning(i =>
            {
                i.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-API-Version"),
                    new QueryStringApiVersionReader("api-version")
                );
                i.ReportApiVersions = true;
                i.AssumeDefaultVersionWhenUnspecified = true;
                i.DefaultApiVersion = new ApiVersion(1, 0);
            });

            //https://www.meziantou.net/versioning-an-asp-net-core-api.htm
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                options.OperationFilter<AuthenticationRequirementsOperationFilter>();

            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfig>();
        }
    }
}
