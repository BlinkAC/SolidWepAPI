using Microsoft.EntityFrameworkCore;
using ParkingSpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Repositories
{
    internal sealed class MySpotDbContext : DbContext
    {
        public DbSet<Reservation> reservations { get; set; }
        public DbSet<WeeklyParkingSpot> weeklyParkingSpots { get; set; }
        public DbSet<User> users { get; set; }
        public MySpotDbContext(DbContextOptions<MySpotDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //modelBuilder.Entity<Reservation>();
            //modelBuilder.Entity<WeeklyParkingSpot>();
            //base.OnModelCreating(modelBuilder);

            //busca las configuraciones que implementar la interfaz de las configuraciones y las aplica a EF
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
//corriendo dotnet ef add migration por si solo no funciona
//es necesario especificar el proejcto de inicio y de salida