using ParkingSpot.Application.Abstractions;
using ParkingSpot.Core.ValueObjects;

namespace ParkingSpot.Application.Commands
{

    //Se reserva el estacionamiento completo
    public sealed record ReserveParkingForCleaning(DateTime date) : ICommand;
}
