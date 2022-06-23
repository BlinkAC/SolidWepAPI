using Microsoft.EntityFrameworkCore;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;
using ParkingSpot.Infrastructure.DAL.Repositories;

namespace ParkingSpot.Infrastructure.DAL.Handlers
{
    internal sealed class GetUsersHandler : IQueryHandler<GetUsers, IEnumerable<UserDto>>
    {
        private readonly MySpotDbContext _dbContext;

        public GetUsersHandler(MySpotDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<IEnumerable<UserDto>> HandleAsync(GetUsers query)
            => await _dbContext.users
                .AsNoTracking()
                .Select(x => x.AsDto())
                .ToListAsync();
    }
}
