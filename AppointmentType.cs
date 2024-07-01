using System.Data;
using static enums;

public class AppointmentType
{
    public eAnimalType TypeID { get; internal set; }
    
    public string Name { get; set; }

    public TimeSpan AppointmentLength { get; internal set; }

    private readonly IDatabaseInterface _db;

    public AppointmentType(List<AppointmentType> appointmentTypes, eAnimalType? animal = 0)
    {
        var appointmentType = appointmentTypes.First(t => t.TypeID == animal);

        TypeID = appointmentType.TypeID;
        Name = appointmentType.Name;
        AppointmentLength = appointmentType.AppointmentLength;

    }

    public AppointmentType(DataRow row)
    {
        TypeID = TypeID = (eAnimalType)Convert.ToInt32(row["ID"]);
        Name = (string)row["Name"];
        AppointmentLength = TimeSpan.FromMinutes((long)row["AppointmentLength"]);
    }
}