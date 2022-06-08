using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Configuration
{
    //Configuracion para mapear los custom value objects
    internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(x => x.Id);
            //Con hasConversion  EF sabe como guardar el valor y leerlo de DB
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ReservationId(x));

            builder.Property(x => x.EmployeeName)
                .HasConversion(x => x.Value, x => new EmployeeName(x));

            builder.Property(x => x.LicensePlate)
                .HasConversion(x => x.Value, x => new LicensePlate(x));

            builder.Property(x => x.Date)
                .HasConversion(x => x.Value, x => new Date(x));

        }
    }
}
