using Loan.Datas;
using Loan.Models.Domain;
using Loan.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Loan.Controllers

{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var list = _unitOfWork.Context.User.ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _unitOfWork.Context.User.FirstOrDefault(x => x.UserName == model.UserName);
            if (user == null)
            {
                ViewBag.LoginErrorMessage = "USER NOT FOUND!!!";
                return View(model);
            }
            var unhash = HashPassword(model.Password);
            if (user.Password != unhash)
            {
                ViewBag.LoginErrorMessage = "PASWWORD MISMATCHED!!!";
                return View(model);
            }

            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            _unitOfWork.ContextAccessor.HttpContext.Session.SetString("UserId", user.UserId.ToString());
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));

            _unitOfWork.ContextAccessor.HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(model.UserName))
                    {
                        ViewBag.Message = "USERNAME IS REQUIRED!!!";
                        return View(model);
                    }
                    if (_unitOfWork.Context.User.Any(x => x.UserName == model.UserName))
                    {
                        ViewBag.Message = "User Already Exists!!";
                        return View(model);
                    }
                    string hashedPassword = HashPassword(model.Password);
                    var newUser = new User
                    {
                        UserName = model.UserName,
                        Password = hashedPassword,
                        Email = model.Email
                    };

                    _unitOfWork.Context.User.Add(newUser);
                    _unitOfWork.Context.SaveChanges();

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error occurred while registering the user. Please try again later.";
                    return View(model);
                }
            }
            ViewBag.Message = "PLEASE FILL THE FORM PROPERLY!!!";
            return View(model);
        }
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            _unitOfWork.ContextAccessor.HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }

}
