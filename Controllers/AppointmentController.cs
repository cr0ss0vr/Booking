using Booking.Server;
using Microsoft.AspNetCore.Mvc;
using Booking.Models;
using Booking;
using System.Data;
using static enums;

public class AppointmentController : Controller
{
    static string connString = "Server=localhost;Database=Booking;User Id=sa;Password=superadmin;";
    IDatabaseInterface db = new DatabaseInterface(connString);

    public IActionResult Index()
    {
        List<Appointment> appointments = GetAllAppointments();

        return View(appointments);
    }

    public List<Appointment> GetAllAppointments()
    {
        List<Appointment> appointments = new List<Appointment>();
        var dataTable = db.Select("Appointment", ["*"]);
        if (dataTable != null)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var appointment = new Appointment((eAnimalType)row["AnimalType"]);
                appointment.Map(row);
                appointments.Add(appointment);
            }
        }

        return appointments;
    }

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

    public IActionResult Detail(Appointment appointment)
    {
        return View(appointment);
    }

    // GET: /Appointment/Create
    public IActionResult Create()
    {
        var viewModel = new AppointmentViewModel
        {
            AnimalTypes = GetAnimalTypes()
        };

        return View(viewModel);
    }

    // POST: /Appointment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Repopulate AnimalTypes dropdown if validation fails, refreshed page, need to keep data!
            model.AnimalTypes = GetAnimalTypes(); 
            return View(model);
        }

        Dictionary<string, object> appointmentData = new Dictionary<string, object>
        {
            { "Date", model.Time },
            { "Name", model.Name },
            { "AnimalType", (int)model.AnimalTypeId },
            { "Age", model.Age },
            { "PhoneNumber", model.PhoneNumber }
        };

        db.Insert("Appointment", appointmentData);

        return RedirectToAction("Index"); // go view appointment in list!
    }

    private List<AnimalType> GetAnimalTypes()
    {
        var dataTable = db.Select("AnimalType", ["*"]);
        List<AnimalType> animalTypes = new List<AnimalType>();
        if (dataTable != null)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var animalType = new AnimalType();
                animalType.Map(row);
                animalTypes.Add(animalType);
            }
        }

        return animalTypes;
    }
}