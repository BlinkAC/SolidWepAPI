using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Exceptions
{
    public sealed class NoReservationPolicyFounException : CustomException
    {
        public Jobtitle JobTitle { get; }
        public NoReservationPolicyFounException(string jobTitle) : base($"No reservation policy for {jobTitle} was found")
        {
            JobTitle = jobTitle;
        }

        
    }
}
