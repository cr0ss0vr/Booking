using static enums;

public class AppointmentType
{
    public eAnimalType TypeID { get; internal set; }
    
    public string Name { get; set; }

    public TimeSpan AppointmentLength { get; internal set; }

    public AppointmentType(eAnimalType? animal = 0)
    {
        string connString = "Server=localhost;Database=Booking;User Id=sa;Password=superadmin;";
        IDatabaseInterface db = new DatabaseInterface(connString);

        var dataTable = db.Select("AnimalType", ["*"], $"id = {(int)animal}");
        if (dataTable.Rows.Count == 0)
        {
            throw new Exception("no type found");
        }

        TypeID = (eAnimalType)Convert.ToInt32(dataTable.Rows[0]["ID"]);
        Name = (string)dataTable.Rows[0]["Name"];
        AppointmentLength = TimeSpan.FromMinutes((long)dataTable.Rows[0]["AppointmentLength"]);

    }
}