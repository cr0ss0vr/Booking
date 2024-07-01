using System.Data;
using System.Xml.Linq;
using static Booking.Server.enums;

namespace Booking.Server
{
    public class Appointment
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public AppointmentType AppointmentType{ get; set; }

        public void Map(DataRow dataRow)
        {
            if (dataRow == null) return;
            Name = (string)dataRow["Name"];
            Date = (DateTime)dataRow["Date"];
            PhoneNumber = (string)dataRow["PhoneNumber"];
            Age = (int)dataRow["Age"];
            AppointmentType.Animal = (AnimalType)dataRow["AnimalType"];
        }
    }
}
