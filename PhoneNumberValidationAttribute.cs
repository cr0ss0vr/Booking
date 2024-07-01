using System.ComponentModel.DataAnnotations;

public class PhoneNumberValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string phoneNumber = value.ToString();

            // Check if the phone number starts with '+'
            if (phoneNumber.StartsWith("+"))
            {
                // Phone number must be exactly 13 characters (12 digits) if starting with '+'
                if (phoneNumber.Length != 13)
                {
                    return new ValidationResult("Phone Number must be 13 characters if area code included!");
                }
                // Regex for phone number starting with '+'
                if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+[0-9]{1,12}$"))
                {
                    return new ValidationResult("Phone Number must start with '+' followed by 12 digits.");
                }
            }
            else if (phoneNumber.StartsWith("0"))
            {
                // Phone number must be exactly 11 characters if starting with '0'
                if (phoneNumber.Length != 11)
                {
                    return new ValidationResult("Phone Number must be 11 digits!");
                }
                // Validate the format of the phone number starting with '0'
                // Add additional format validation if needed
            }
            else
            {
                // Invalid phone number format
                return new ValidationResult("Phone Number must start with '+' or '0'.");
            }

            // Phone number format is valid
            return ValidationResult.Success;
        }

        // Null values are considered valid (use [Required] attribute if necessary)
        return ValidationResult.Success;
    }
}
