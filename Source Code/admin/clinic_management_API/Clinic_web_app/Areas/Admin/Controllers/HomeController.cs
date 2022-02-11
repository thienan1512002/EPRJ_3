using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ClinicDBContext _context;
        public HomeController(ClinicDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StaffAccount()
        {
            return View();
        }
        public IActionResult EquipmentForClinic()
        {
            return View();
        }
        public IActionResult EquipmentForMedicine()
        {
            return View();
        }
        public IActionResult Medicine()
        {
            return View();
        }

        public IActionResult Course()
        {
            return View();
        }
        public IActionResult CustomerAccount()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public  IActionResult ProcessLogin(string username , string password)
        {
            if (username.Equals("") && password.Equals(""))
            {
                return View();
            }
            var staffAccount =  _context.StaffAccounts.SingleOrDefault(account => account.Username.Equals(username) && account.Password.Equals(password));
            if(staffAccount == null)
            {
                return View();
                ViewBag.Message = "Incorect username or Password";
            }
            HttpContext.Session.SetString("accountId", staffAccount.AccountId);
            HttpContext.Session.SetString("accountName", staffAccount.Username);
            HttpContext.Session.SetString("avatar", staffAccount.Image);
            return RedirectToAction("Index");
        }
    }
}
