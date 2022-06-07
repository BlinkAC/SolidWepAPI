namespace ParkingSpot.Application.DTO
{
    //Los DTO permiten esconder o agregar propeidades
    //segun las necesidades para pasar informacion entre las
    //capas de la aplicacion

    //Sin embargo el nombre no nos dice nada
    //no se sabe que se hara con este DTO obetener, guardar, etc
    //ahi se introducen los commands
    public class ReservationDTO
    {
        //autogenerado
        //public Guid Id { get; set; } = Guid.NewGuid();
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }

        public string LicensePlate { get; set; }
        public DateTime Date { get; set; }
    }
}
