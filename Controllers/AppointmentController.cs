using Microsoft.AspNetCore.Mvc;
using System.Data;
using static enums;

public class AppointmentController : Controller
{
    private readonly IDatabaseInterface _db;

    public AppointmentController(IDatabaseInterface db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<Appointment> appointments = GetAllAppointments();
        return View(appointments);
    }

    public List<Appointment> GetAllAppointments()
    {
        List<Appointment> appointments = new List<Appointment>();
        var dataTable = _db.Select("Appointment", new string[] { "*" });
        if (dataTable != null)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var appointment = new Appointment(GetAnimalTypes(), row);
                appointments.Add(appointment);
            }
        }
        return appointments;
    }

    public Appointment Get(DateTime datetime, string name)
    {
        var dataTable = _db.Select("Appointment", ["*"], $"date='{datetime}' and name='{name}'");
        if (dataTable == null)
        {
            return null;
        }

        var appointment = new Appointment(GetAnimalTypes(), dataTable.Rows[0]);
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
            AppointmentTypes = GetAnimalTypes()
        };

        return View(viewModel);
    }

    // POST: /Appointment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentViewModel model)
    {
        model.AppointmentTypes = GetAnimalTypes(); // Repopulate dropdown

        bool available = true;
        bool outOfHours = false;

        var newAppointmentStartTime = model.Time;
        var newAppointmentEndTime = model.Time.Add(model.AppointmentTypes.FirstOrDefault(x => x.TypeID == model.AnimalTypeId).AppointmentLength);
        var closingTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17,0,0);
        
        // Validate AnimalTypeId against enum values
        if (!Enum.IsDefined(typeof(eAnimalType), model.AnimalTypeId))
        {
            ModelState.AddModelError("AnimalTypeId", "Invalid Animal Type selected.");
        }

        if (newAppointmentEndTime > closingTime)
        {
            outOfHours = true;
        }

        List<Appointment> appointments = GetAllAppointments();
        foreach (Appointment appointment in appointments)
        {
            var existingAppointmentStartTime = appointment.Date;
            var existingAppointmentEndTime = appointment.Date.Add(appointment.AppointmentLength);

            if (newAppointmentEndTime > existingAppointmentStartTime && newAppointmentStartTime < existingAppointmentEndTime)
            {
                available = false;
                break;
            }
            if (newAppointmentStartTime < existingAppointmentEndTime && newAppointmentEndTime > existingAppointmentStartTime)
            {
                available = false;
                break;
            }
        }

        // Validate Time Availability
        if (!available)
        {
            ModelState.AddModelError("Time", "Time is already used, please choose another time.");
        }

        if (outOfHours)
        {
            ModelState.AddModelError("Time", "Appointment end-time is after closing time.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // If everything is valid, proceed with saving the appointment
        Dictionary<string, object> appointmentData = new Dictionary<string, object>
        {
            { "Date", model.Time },
            { "Name", model.Name },
            { "AnimalType", (int)model.AnimalTypeId },
            { "Age", model.Age },
            { "PhoneNumber", model.PhoneNumber }
        };

        _db.Insert("Appointment", appointmentData);

        return RedirectToAction("Index"); // Redirect to appointment list
    }

    private List<AppointmentType> GetAnimalTypes()
    {
        var dataTable = _db.Select("AnimalType", ["*"]);
        List<AppointmentType> animalTypes = new List<AppointmentType>();
        if (dataTable != null)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                var animalType = new AppointmentType(row);
                animalTypes.Add(animalType);
            }
        }
        return animalTypes;
    }
}
