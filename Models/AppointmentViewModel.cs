using static enums;
using System.ComponentModel.DataAnnotations;

public class AppointmentViewModel
{
    public AppointmentViewModel()
    {
        AppointmentTypes = new List<AppointmentType>();
    }

    [Required(ErrorMessage = "Time is required.")]
    public DateTime Time { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Animal Type is required.")]
    public eAnimalType AnimalTypeId { get; set; }

    [Required(ErrorMessage = "Age is required.")]
    [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
    public int Age { get; set; }

    [PhoneNumberValidation(ErrorMessage = "Invalid Phone Number format.")]
    public string PhoneNumber { get; set; }

    public List<AppointmentType> AppointmentTypes { get; set; }
}
