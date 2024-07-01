using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        static string connString = "localhost";
        IDatabaseInterface db = new DatabaseInterface(connString);

        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAppointment/{datetime}/{name}")]
        public Appointment Get(DateTime datetime, string name)
        {
            var dataTable = db.Select("Appointment", ["*"], $"date='{datetime}' and name='{name}'");
            var appointment = new Appointment();
            if (dataTable != null)
            {
                appointment.Map(dataTable.Rows[0]);
            }

            return appointment;
        }

        [HttpGet(Name = "GetAppointments")]
        public IEnumerable<Appointment> GetAllAppointments()
        {
            var appointments = new List<Appointment>();
            var dataTable = db.Select("Appointment", ["Name", "Date"]);
            if (dataTable != null)
            {
                foreach (var row in dataTable.Rows)
                {
                    var appointment = new Appointment();
                    appointment.Map(dataTable.Rows[0]);
                    appointments.Add(appointment);
                }
            }

            return appointments;
        }
    }
}
