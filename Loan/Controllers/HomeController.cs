using Loan.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string result)
        {
            if (result == "Y")
            {
                var resuls = "CONGRATULATION!!! YOU CAN GET LOAN!!!";
                TempData["SucessResult"] = resuls;
            }
            else if(result == "N") 
            {
                var resuls = "SORRY!!! YOU DOCUMENT DOESN'T SATISFY LOAN TERMS!!!";
                TempData["RejectResult"] = resuls;
            }
            else
            {
                var results = result;
                TempData["RejectResult"] = result;
            }
            return View();
        }

    }
}
