using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Core.Entities
{
    public class Reservation
    {
        public ReservationId Id { get; }
        public EmployeeName EmployeeName { get; private set; }

        public LicensePlate LicensePlate { get; private set; }

        public Date Date { get; private set; }

        public Reservation(ReservationId id, EmployeeName employeeName, LicensePlate licensePlate, Date date)
        {
            Id = id;
            EmployeeName = employeeName;
            LicensePlate = licensePlate;
            Date = date;
        }

        //Puesto aqui se puede controlar como se asigna la licencia y poder realizar validaciones
        public void UpdateLicensePlate(LicensePlate licensePlate)
        {
            LicensePlate = licensePlate;
        }



    }
}
