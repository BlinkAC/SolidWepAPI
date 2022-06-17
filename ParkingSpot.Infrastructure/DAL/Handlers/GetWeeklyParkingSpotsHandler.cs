using ParkingSpot.Application.DTO;
using ParkingSpot.Application.Queries;
using ParkingSpot.Application.Abstractions;
using ParkingSpot.Infrastructure.DAL.Repositories;
using ParkingSpot.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using ParkingSpot.Infrastructure.DAL.Handlers;

namespace ParkingSpot.Infrastructure.DAL.Handlers
{
    internal sealed  class GetWeeklyParkingSpotsHandler : IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDTO>>
    {
        private readonly MySpotDbContext _context;

        public GetWeeklyParkingSpotsHandler(MySpotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeeklyParkingSpotDTO>> HandleAsync(GetWeeklyParkingSpots query)
        {
            var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
            var weeklyParkingSpots = await _context.weeklyParkingSpots
                .Where(x => week == null || x.Week == week)
                .Include(x => x.Reservations)
                .AsNoTracking()
                .ToListAsync();

            return weeklyParkingSpots.Select(x => x.AsDto());
        }
    }
}
