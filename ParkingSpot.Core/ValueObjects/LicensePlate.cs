using ParkingSpot.Core.Exceptions;

namespace ParkingSpot.Core.ValueObjects
{
    //Lo value object se deferencian de las entidades porque son inmutables y no tienen identificadores
    //una entidad puede cambiar sus atributos y seguir siendo la misma (por su id)
    //cuando la placa cambie no es necesario actualizarla sino crear una nueva placa
    public record LicensePlate
    {
        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new invalidLicensePlateException(value);
            }

            if (value.Length is < 5 or > 8)
            {
                throw new invalidLicensePlateException(value);
            }

            Value = value;
        }

        //Es un casteo/conversion implicito definida por uni mismo
        //Acepta string y lo castea a licensePlate
        public static implicit operator string(LicensePlate licensePlate)
            => licensePlate.Value;
        //acepta licensePlate y lo castea a string
        public static implicit operator LicensePlate(string value)
            => new(value);
    }
}//Aunque dependiendo del contexto un object value se puede interpretarse una entidad  y viceversa
