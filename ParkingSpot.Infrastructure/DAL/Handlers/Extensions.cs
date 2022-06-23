using ParkingSpot.Application.DTO;
using ParkingSpot.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Handlers
{
    //Mapear los dto
    public static class Extensions
    {
        public static WeeklyParkingSpotDTO AsDto(this WeeklyParkingSpot entity)
        => new()
        {
            Id = entity.Id.Value.ToString(),
            Name = entity.Name,
            Capacity = entity.Capacity,
            From = entity.Week.From.Value.DateTime,
            To = entity.Week.To.Value.DateTime,
            Reservations = entity.Reservations.Select(x => new ReservationDTO
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : "cleaning reservation",
                Date = x.Date.Value.Date
            })
        };

        public static UserDto AsDto(this User entity)
        => new()
    {
        UserId = entity.Id,
        UserName = entity.Username,
        FullName = entity.FullName
    };
    }
}
