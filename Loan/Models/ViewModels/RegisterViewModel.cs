using System.ComponentModel.DataAnnotations;

namespace Loan.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        //[ValidatePhoneNumber]
        public string Password { get; set; }
        //[ValidatePhoneNumber]
        //public string PhoneNumber { get; set; } 
    }
}
