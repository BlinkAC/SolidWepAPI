using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Entities
{
    public class User
    {
        public UserId Id { get; set; }
        public Email Email { get; set; }
        public Username Username { get; set; }
        public Password Password { get; set; }
        public FullName FullName { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public User(UserId id, Email email, Username username, Password password, FullName fullName, Role role, DateTime createdAt)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
            FullName = fullName;
            Role = role;
            CreatedAt = createdAt;
        }



    }
}
