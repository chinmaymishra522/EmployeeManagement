using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementWeb.Models;
using EmployeeManagementWeb.Repository.IRepository;
using EmployeeManagementWeb.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace EmployeeManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInstituteRepository _insRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IAccountRepository _accRepo;


        public HomeController(ILogger<HomeController> logger, IInstituteRepository insRepo,
            IEmployeeRepository employeeRepo, IAccountRepository accRepo)
        {
            _insRepo = insRepo;
            _employeeRepo = employeeRepo;
            _logger = logger;
            _accRepo = accRepo;

        }

        public async Task<IActionResult> Index()
        {
            IndexVM listOfInstitutesAndEmployees = new IndexVM()
            {
                InstituteList = await _insRepo.GetAllAsync(SD.InstituteAPIPath, HttpContext.Session.GetString("JWToken")),
                EmployeeList = await _employeeRepo.GetAllAsync(SD.EmployeeAPIPath, HttpContext.Session.GetString("JWToken")),
            };
            return View(listOfInstitutesAndEmployees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            User obj = new User();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User obj)
        {
            User objUser = await _accRepo.LoginAsync(SD.AccountAPIPath + "authenticate/", obj);
            if (objUser.Token == null)
            {
                TempData["alert"] = "please enter valid user-id and password";
                return View();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, objUser.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, objUser.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            HttpContext.Session.SetString("JWToken", objUser.Token);
            TempData["alert"] = "Welcome " + objUser.Username;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User obj)
        {
            bool result = await _accRepo.RegisterAsync(SD.AccountAPIPath + "register/", obj);
            if (result == false)
            {
                TempData["alert"] = "Please fill the username and password";
                return View();
            }
            TempData["alert"] = "Registeration Successful";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", "");
            TempData["alert"] = "Successfully Logout ";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
