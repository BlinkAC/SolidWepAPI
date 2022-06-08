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
    internal class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
    {
        public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ParkingSpotId(x));

            builder.Property(x => x.Week)
                .HasConversion(x => x.From.Value, x => new Week(x));

            builder.Property(x => x.Name)
                .HasConversion(x => x.Value, x => new ParkingSpotName(x));
        }
    }
}
