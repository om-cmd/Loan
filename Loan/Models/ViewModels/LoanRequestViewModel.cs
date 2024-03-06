using System.ComponentModel.DataAnnotations;

namespace Loan.Models.ViewModels
{
    public class LoanRequestViewModel
    {
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Married is required")]

        public string Married { get; set; }
        [Required(ErrorMessage = "fill this too")]

        public int Dependents { get; set; }
        [Required(ErrorMessage = "Eduction is required")]

        public string Education { get; set; }
        [Required(ErrorMessage = "fill this too ")]

        public string Self_Employed { get; set; }
        [Required(ErrorMessage = "Income is required")]

        public int ApplicantIncome { get; set; }
        [Required(ErrorMessage = "Income is required")]

        public int CoapplicantIncome { get; set; }
        [Required(ErrorMessage = "Amount is required")]

        public int LoanAmount { get; set; }
        [Required(ErrorMessage = "LoanAmount_Term is required")]

        public int Loan_Amount_Term { get; set; }
        [Required(ErrorMessage = "Credit history is required")]

        public int Credit_History { get; set; }
        [Required(ErrorMessage = "Property area is required")]

        public string Property_Area { get; set; }

    }
    public class LoanResult
    {
        public string Result { get; set; }
    }
}
