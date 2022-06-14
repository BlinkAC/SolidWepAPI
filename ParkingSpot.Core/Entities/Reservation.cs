using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Core.Entities
{
    public abstract class Reservation
    {
        public ReservationId Id { get; }

        public Capacity Capacity { get; private set; }
        public Date Date { get; private set; }

        protected Reservation(ReservationId id, Capacity capacity, Date date)
        {
            Id = id;
            Capacity = capacity;
            Date = date;
        }

        ////Puesto aqui se puede controlar como se asigna la licencia y poder realizar validaciones
        //public void UpdateLicensePlate(LicensePlate licensePlate)
        //{
        //    LicensePlate = licensePlate;
        //}



    }
}
