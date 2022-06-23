using Microsoft.EntityFrameworkCore;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;
using ParkingSpot.Core.ValueObjects;
using ParkingSpot.Infrastructure.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL.Handlers
{
    internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
    {
        private readonly MySpotDbContext _dbContext;

        public GetUserHandler(MySpotDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<UserDto> HandleAsync(GetUser query)
        {
            var userId = new UserId(query.UserId);
            var user = await _dbContext.users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);

            return user?.AsDto();
        }
    }
}
