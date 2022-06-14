using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Entities
{
    public class CleaningReservation : Reservation
    {
        public CleaningReservation(ReservationId id, Date date) : base(id, capacity: 2,date)
        {
        }
    }
}
