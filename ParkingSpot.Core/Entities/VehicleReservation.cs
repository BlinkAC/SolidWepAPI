using ParkingSpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Core.Entities
{
    public sealed class VehicleReservation : Reservation
    {
        public EmployeeName EmployeeName { get; private set; }

        public LicensePlate LicensePlate { get; private set; }

        public VehicleReservation(ReservationId id, Date date, Capacity capacity, EmployeeName employeeName, LicensePlate licensePlate) 
            : base(id, capacity, date)
        {
            EmployeeName = employeeName;
            LicensePlate = licensePlate;
        }

        //Puesto aqui se puede controlar como se asigna la licencia y poder realizar validaciones
        public void UpdateLicensePlate(LicensePlate licensePlate)
        {
            LicensePlate = licensePlate;
        }
    }
}
