using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Infrastructure.DAL.Decorators;
using ParkingSpot.Infrastructure.DAL.Repositories;

namespace ParkingSpot.Infrastructure.DAL
{
    internal static class Extentions
    {
        private const string OptionsSectionName = "postgres";
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
            var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);

            services.AddDbContext<MySpotDbContext>( x => x.UseNpgsql(postgresOptions.ConnectionString));

            //Con esto se da la oprtunidad que dependiendo de donde viene el request se pueda usar inMemory o de la BD
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddScoped<IUserRepository, PostgresUserRepository>();

            services.AddHostedService<DatabaseInitializer>();

            services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

            //Parece que hay un error/bug con versiones anteriores de ngpsql relacionado con el datetimestamp y EF
            //solucion
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            return services; 
        }
    }
}
