namespace Booking.Server
{
    public class AppointmentType
    {
        public enums.AnimalType Animal { get; internal set; }
        public TimeSpan appointmentTime { get; internal set; }
    }
}