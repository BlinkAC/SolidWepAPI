using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.Auth
{
    public sealed class AuthOptions
    {
        //quien genera el token
        public string Issuer { get; set; }
        //quien utiliza el token
        public string Audience { get; set; }
        //secret
        public string SigningKey { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}
