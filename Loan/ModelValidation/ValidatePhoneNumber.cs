using System.ComponentModel.DataAnnotations;

namespace Loan
{
    public class ValidatePhoneNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var phone = value.ToString();
            if(phone == null)
            {
                return new ValidationResult("Phone number cannot be null or empty.");
            }
            var check = phone.Substring(0,2);
            if(check !="98" || check != "97" || check != "96")
            {
                return new ValidationResult("PHONE NUMBER SHOULD STARTS WITH 97,96,98");
            }
            return ValidationResult.Success;

        }
       
    }
}
