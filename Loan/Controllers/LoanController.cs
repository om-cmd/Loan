using Loan.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace Loan.Controllers
{
    public class LoanController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> LoanPredict(LoanRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    LoanResult loanResult = new LoanResult();
                    HttpClient client = new HttpClient();
                    var url = "http://127.0.0.1:8000/api/v1/predict";
                    var serialize = JsonConvert.SerializeObject(model);
                    var strings = new StringContent(serialize, null, "application/json");
                    HttpResponseMessage responseMessage = await client.PostAsync(url, strings);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var content = await responseMessage.Content.ReadAsStringAsync();
                        loanResult = JsonConvert.DeserializeObject<LoanResult>(content);
                    }
                    return RedirectToAction("Index", "Home", new { result = loanResult.Result });
                }catch(Exception ex)
                {
                    return RedirectToAction("Index", "Home", new {result = ex.Message.ToString()});
                }
            }
            else
            {
                return RedirectToAction("Index", "Home", new { result = "INVALID MODEL STATE VALIDATION" });
            }
        }
        [HttpGet]
        public IActionResult DisplayResult(string result)
        {
            LoanResult loanresult = new LoanResult();
            return View(loanresult);
        }
    }
}
