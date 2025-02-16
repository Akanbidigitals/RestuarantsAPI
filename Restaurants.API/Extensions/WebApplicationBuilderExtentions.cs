using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtentions
    {
        public static void AddPresenstation(this WebApplicationBuilder builder)
        {

            /// Auths for bearer
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
           {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            }, []
        }
              });
            });

            ///////




            //Error handling middleware
            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            // Register if request time is greater than 4sec(4000ms)
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();



            //Serilog for loggings 
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);

                //From program instead of the app.json
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                //.WriteTo.File("logs/Restaurant-API-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit:true)
                //.WriteTo.Console(outputTemplate : "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}|{NewLine} {Message:lj}{NewLine}{Exception}");
            });
        }
    }
}
