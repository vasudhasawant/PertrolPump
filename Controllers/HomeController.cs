using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PetrolPumpLog.Models;

namespace PetrolPumpLog.Controllers
{
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(PetrolPumpLog.Models.LoginRequest model)
        {
            if (model.Username == "admin" && model.Password == "admin123")
            {
                // For now, just redirect (later we use JWT/token)
                return RedirectToAction("Index", "DispensingRecords");
            }
            ViewBag.Error = "Invalid Username or Password";
            return View();
        }
    }
}
