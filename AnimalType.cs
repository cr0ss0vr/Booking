using System.Data;
using static enums;

namespace Booking
{
    public class AnimalType
    {
        public eAnimalType ID { get; set; }
        public string Name { get; set; }
        public int AppointmentLength { get; set; }

        internal void Map(DataRow dataRow)
        {
            if (dataRow == null) return;
            ID = (eAnimalType)Convert.ToInt32(dataRow["ID"]);
            Name = Convert.ToString(dataRow["Name"]);
            AppointmentLength = Convert.ToInt32(dataRow["AppointmentLength"]);
        }
    }
}