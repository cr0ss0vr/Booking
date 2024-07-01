using System.Data;
using static enums;

public class Appointment
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public DateTime Date { get; set; }

    public string PhoneNumber { get; set; }

    public int Age { get; set; }

    public TimeSpan AppointmentLength {
        get
        {
            return _appointmentType.AppointmentLength;
        }
    }

    public string Animal
    {
        get
        {
            return _appointmentType.Name;
        }
    }

    private AppointmentType _appointmentType;

    public Appointment(DataRow dataRow)
    {
        if (dataRow == null) return;
        Id = Convert.ToInt32(dataRow["ID"]);
        Name = Convert.ToString(dataRow["Name"]);
        Date = Convert.ToDateTime(dataRow["Date"]);
        PhoneNumber = Convert.ToString(dataRow["PhoneNumber"]);
        Age = Convert.ToInt32(dataRow["Age"]);
        _appointmentType = new AppointmentType((eAnimalType)Convert.ToInt32(dataRow["AnimalType"]));
    }
}
