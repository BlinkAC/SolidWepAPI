namespace ParkingSpot.Core.Exceptions
{
    public sealed class InvalidDateReservationException : CustomException
    {
        public DateTime date { get; }

        //Se podria usar una exception cualquiera para decir que no se puede
        //guardar la reservacion pero hay que estar conciente de donde (bd, htt, etc) o
        //que clase de exception hay que mostrar
        public InvalidDateReservationException(DateTime date) :
            base($"Reservacion con fecha: {date} es invalida")
        {
        }
    }
}
