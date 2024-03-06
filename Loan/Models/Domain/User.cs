using System.ComponentModel.DataAnnotations;

namespace Loan.Models.Domain
{

    public class User 
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public  string UserName { get; set; }

        [Required]
        [EmailAddress]
        public  string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public  string Password { get; set; }

    }
}
