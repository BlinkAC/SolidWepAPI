using Microsoft.EntityFrameworkCore;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Repositories
{
    internal sealed class PostgresUserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;

        public PostgresUserRepository(MySpotDbContext dbContext)
        {
            _users = dbContext.users;
        }

        public async Task AddAsync(User user)
         => await _users.AddAsync(user);

            public Task<User> GetByEmailAsync(Email email)
        => _users.SingleOrDefaultAsync(x => x.Email == email);


        public Task<User> GetByIdAsync(UserId id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetByUsernameAsync(Username username)
        => _users.SingleOrDefaultAsync(x => x.Username == username);

        //public Task<User> GetByIdAsync(UserId id)


        //public Task<User> GetByEmailAsync(Email email)
        //    => _users.SingleOrDefaultAsync(x => x.Email == email);

        //public Task<User> GetByUsernameAsync(Username username)
        //    => _users.SingleOrDefaultAsync(x => x.Username == username);

        //public async Task AddAsync(User user)
        //    => await _users.AddAsync(user);


    }
}
