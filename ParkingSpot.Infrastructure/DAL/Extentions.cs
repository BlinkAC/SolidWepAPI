using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Infrastructure.DAL.Repositories;
using ParkingSpot.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL
{
    internal static class Extentions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services) 
        {
            const string connectionString = "Host=localhost;Database=mySpot;Username=postgres;Password=secret";
            services.AddDbContext<MySpotDbContext>( x => x.UseNpgsql(connectionString));

            //Con esto se da la oprtunidad que dependiendo de donde viene el request se pueda usar inMemory o de la BD
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            services.AddHostedService<DatabaseInitializer>();

            //Parece que hay un error/bug con versiones anteriores de ngpsql relacionado con el datetimestamp y EF
            //solucion
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            
            return services; 
        }
    }
}
