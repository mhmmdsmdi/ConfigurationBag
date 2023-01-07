using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ConfigurationBag.EndPoint.Api.Extensions;

public static class SwaggerExtensions
{
    public static void AddApiSwagger(this IServiceCollection services, string xmlDocPath = "")
    {
        services.AddSwaggerGen(options =>
        {
            options.OrderActionsBy(x => x.GroupName);
            options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "Api V1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            if (!string.IsNullOrWhiteSpace(xmlDocPath))
                options.IncludeXmlComments(xmlDocPath, true);
        });
    }

    public static void UseApiSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Main Api");
            c.DocExpansion(DocExpansion.None);
            c.EnableFilter();
        });
    }
}