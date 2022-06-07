namespace ParkingSpot.Core.ValueObjects
{
    public sealed record Week
    {
        public Date From { get; set; }
        public Date To { get; set; }

        public Week(DateTimeOffset value)
        {
            var daysOfWeekNumber = (int)value.DayOfWeek;
            var pastDays = -1 * daysOfWeekNumber;
            var remainingDays = 7 + pastDays;

            From = new Date(value.AddDays(pastDays));

            To = new Date(value.AddDays(remainingDays));
        }

        public override string ToString() => $"From {From} To {To}";
    }
}
