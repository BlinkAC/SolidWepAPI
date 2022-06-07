using MySpot.Tests.Unit.Shared;
using ParkingSpot.Application.Commands;
using ParkingSpot.Application.Services;
using ParkingSpot.Core.Repositories;
using ParkingSpot.Infrastructure.Repositories;
using Shouldly;
using System;
using Xunit;
namespace MySpot.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        [Fact]
        public void given_valid_spotId_create_should_succeed()
        {
            //ARRANGE
            var command = new CreateReservation
                (Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.NewGuid(), "Juan Lopez", "ARBN123", 
                DateTime.UtcNow.AddDays(1));
            //ACT
            var reservationId = _reservationService.Create(command);
            //ASSERT
            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }

        [Fact]
        public void given_invalid_spotId_create_should_fail()
        {
            //ARRANGE
            var command = new CreateReservation
                (Guid.Parse("00000000-0000-0000-0000-000000000011"), Guid.NewGuid(), "Juan Lopez", "ARBN123",
                DateTime.UtcNow.AddDays(1));
            //ACT
            var reservationId = _reservationService.Create(command);
            //ASSERT
            reservationId.ShouldBeNull();
        }
        [Fact]
        public void given_reservation_for_already_reserved_create_should_fail()
        {
            //ARRANGE
            var command = new CreateReservation
                (Guid.Parse("00000000-0000-0000-0000-000000000001"), Guid.NewGuid(), "Juan Lopez", "ARBN123",
                DateTime.UtcNow.AddDays(1));

            _reservationService.Create(command);

            //ACT
            var reservationId = _reservationService.Create(command);
            //ASSERT
            reservationId.ShouldBeNull();

        }

        #region ARRANGE
        private readonly IClock _clock;
        private IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private ReservationService _reservationService;
        public ReservationServiceTests()
        {
            _clock = new TestClock();

            //Se cambio de estar en el reservationService porque 
            //las pruebas deberian ser aisladas de los datos como tal
            //cuando se agregue BD obviamente no se estara llamando 
            //la tabla para efectos de pruebas
            //var weeklyParkingSpots = new List<WeeklyParkingSpot>
            //{
            //new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
            //new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            //new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            //new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            //new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5"),
            //};
            //Ahora la lista esta en el repositorio
            _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
            _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);  
        }
        #endregion

    }
}
