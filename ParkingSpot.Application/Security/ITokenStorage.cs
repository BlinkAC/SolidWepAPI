using ParkingSpot.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Application.Security
{
    //usa el HttAccessor
    public interface ITokenStorage
    {
        void Set(JwtDto jwt);
        JwtDto Get();
    }
}
