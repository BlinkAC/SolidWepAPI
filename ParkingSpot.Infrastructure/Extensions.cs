using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.Services;
using ParkingSpot.Infrastructure.Auth;
using ParkingSpot.Infrastructure.DAL;
using ParkingSpot.Infrastructure.Exceptions;
using ParkingSpot.Infrastructure.Logging;
using ParkingSpot.Infrastructure.Repositories;
using ParkingSpot.Infrastructure.Security;
using ParkingSpot.Infrastructure.Time;
using System.Runtime.CompilerServices;

//Hace que sea visible para este proeycto
//En este caso esta bien porque es para efectos de pruebas.
[assembly: InternalsVisibleTo("MySpot.Tests.Unit")]
[assembly: InternalsVisibleTo("MySpot.Tests.Integration")]
namespace ParkingSpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ExceptionMiddleware>();
            services.AddHttpContextAccessor();

            services
                .AddPostgres(configuration)
                .AddSingleton<ExceptionMiddleware>()
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
                .AddSingleton<IClock, Clock>();

            services.AddCustomLogging();
            services.AddSecurity();

            var applicationAssembly = typeof(AppOptions).Assembly;

            services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddAuth(configuration);

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My spot API",
                    Version = "v1",
                });

                swagger.EnableAnnotations();
            });

            return services;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            //una forma de que se vea mas profesional usar ReDoc
            //app.UseReDoc(reDoc =>
            //{
            //    reDoc.RoutePrefix = "docs";
            //    reDoc.SpecUrl("swagger/v1/swagger.json");
            //    reDoc.DocumentTitle = "My Spot API";
            //});
            //se abre localhost:xxx/docs
            //nswagger
            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetRequiredSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
