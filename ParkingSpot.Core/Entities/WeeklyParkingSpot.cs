using ParkingSpot.Core.Exceptions;
using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Core.Entities
{
    public class WeeklyParkingSpot
    {
        public ParkingSpotId Id { get; }

        public Week Week { get;  set; }

        public ParkingSpotName Name { get; }
        //Ienumerable - Solo lectura y se castea reservations 
        //no ingresar la misma reservacion, se puede porque 
        //hash-set implementa ienumerable
        public IEnumerable<Reservation> Reservations => _reservations;

        //Hash-set para no ingresar la misma reservacion
        private readonly HashSet<Reservation> _reservations = new();

        public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
        {
            Id = id;
            Week = week;
            Name = name;
        }

        //Cambia a ser interno porque se agrego logica por la cual
        //no se deberia omitir las reglas y directamente poder llamar el metodo agregar
        internal void AddReservation(Reservation reservation, Date now)
        {
            var isInvalidDate = reservation.Date < Week.From ||
                reservation.Date > Week.To ||
                reservation.Date < now;

            if (isInvalidDate)
            {
                //El retorno del metodo es una exception porque de otroa forma si se regresa
                //un bool o int, este debera ser verificado en alguna otra parte del codigo que lo implemente
                throw new InvalidDateReservationException(reservation.Date.Value.Date);
            }

            var reservedAlready = _reservations.Any(x => x.Date == reservation.Date);

            if (reservedAlready)
            {
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);
            }

            _reservations.Add(reservation);
        }


        public void DeleteReservation(ReservationId id) =>
            _reservations.RemoveWhere(x => x.Id == id);
        //{
        //    var reservationExists = _reservations.FirstOrDefault(x => x.Id == id);

        //    if (reservationExists == null)
        //    {
        //        throw new ReservationNotExists(id);
        //    }

        //    _reservations.Remove(reservationExists);
        //}

        public void DeleteReservations(IEnumerable<Reservation> reservations) =>
        _reservations.RemoveWhere(x => reservations.Any(r => r.Id == x.Id));
    }
}
