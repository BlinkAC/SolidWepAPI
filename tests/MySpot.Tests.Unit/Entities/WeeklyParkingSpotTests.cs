using ParkingSpot.Core.Entities;
using ParkingSpot.Core.Exceptions;
using ParkingSpot.Core.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MySpot.Tests.Unit.Entities
{
    public class WeeklyParkingSpotTests
    {
        //[Fact]
        [Theory]
        [InlineData("2020-05-25")]
        [InlineData("2024-05-25")]
        [InlineData("2022-05-24")]
        public void given_invalid_date_AddReservation_should_fail(string dateString)
        {
            var invalidDate = DateTime.Parse(dateString);

            //Arrange
            var reservation = new Reservation(Guid.NewGuid(), "Juan Robles", "AAA12", new Date(invalidDate));

            //Act

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));
            //Assert

            //Sino not null significa que fue correcta
            //la prueba (salio mal como se esperaba)
            Assert.NotNull(exception);
            Assert.IsType<InvalidDateReservationException>(exception);

            //Assert.Null(exception);

        }

        [Fact]
        public void given_reservation_for_already_existing_date_AddReservation_should_fail()
        {
            //ARRANGE
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), "Juan Robles", "AAA12", reservationDate);
            _weeklyParkingSpot.AddReservation(reservation, reservationDate);

            //ACT
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

            //ASSERT
            Assert.NotNull(exception);
            Assert.IsType<ParkingSpotAlreadyReservedException>(exception);
        }

        [Fact]
        public void given_reservation_for_no_taking_date_AddReservation_should_succeed()
        {
            //ARRANGE
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), "Juan Robles", "AAA12", reservationDate);

            //ACT
            _weeklyParkingSpot.AddReservation(reservation, reservationDate);

            //ASSERT
            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
            _weeklyParkingSpot.Reservations.ShouldContain(reservation);
        }

        #region ARRANGE
        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        private readonly Date _now;
        public WeeklyParkingSpotTests()
        {
            _now = new Date(DateTime.Parse("2022-05-25"));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
        }
        #endregion


    }
}
