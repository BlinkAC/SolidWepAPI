using ParkingSpot.Core.Repositories;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.ValueObjects;
using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Services;

namespace ParkingSpot.Infrastructure.Repositories
{
    internal sealed class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _weeklyParkingSpots = new List<WeeklyParkingSpot> {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
        };
        }
        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync() 
        {
            await Task.CompletedTask;
        return _weeklyParkingSpots;
        } 

        public async Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id) {

            await Task.CompletedTask;
            return _weeklyParkingSpots.SingleOrDefault(sp => sp.Id == id);
        } 



        public async Task AddAsync(WeeklyParkingSpot parkingSpot)
        {
            await Task.CompletedTask;
            _weeklyParkingSpots.Add(parkingSpot);
        }


        public Task UpdateAsync(WeeklyParkingSpot parkingSpot)
        {
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        {
            await Task.CompletedTask;
            return _weeklyParkingSpots.Where(sp => sp.Week == week).ToList();
        } 
    }
}
