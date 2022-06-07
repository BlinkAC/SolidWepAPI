using ParkingSpot.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Tests.Unit.Shared
{
    internal sealed class TestClock : IClock
    {
        public DateTime Current() => new DateTime(2022, 5, 26);

    }
}
