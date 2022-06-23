using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Queries
{
    public class GetUser : IQuery<UserDto>
    {
        public Guid UserId { get; set; }

    }
}
