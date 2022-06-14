using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Core.Entities
{
    public abstract class Reservation
    {
        public ReservationId Id { get; }


        public Date Date { get; private set; }

        protected Reservation(ReservationId id, Date date)
        {
            Id = id;
            Date = date;
        }

        ////Puesto aqui se puede controlar como se asigna la licencia y poder realizar validaciones
        //public void UpdateLicensePlate(LicensePlate licensePlate)
        //{
        //    LicensePlate = licensePlate;
        //}



    }
}
