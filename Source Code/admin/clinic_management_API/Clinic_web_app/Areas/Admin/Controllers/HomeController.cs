using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
           
            var data = await _context.EcomerceOrders
                .Include(e => e.EcomerceMedOrderDetails)
                .ThenInclude(e => e.Med)
                .ThenInclude(e => e.Brand).Where(p=>EF.Functions.DateDiffDay(p.OrderDate,DateTime.Today)>7).ToListAsync();
            return View(data);
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
        public IActionResult ProcessLogin(string username, string password)
        {
            if (username.Equals("") && password.Equals(""))
            {
                return View();
            }
            var staffAccount = _context.StaffAccounts.SingleOrDefault(account => account.Username.Equals(username) && account.Password.Equals(password));
            if (staffAccount == null)
            {
                return View();
                ViewBag.Message = "Incorect username or Password";
            }
            HttpContext.Session.SetString("accountId", staffAccount.AccountId);
            HttpContext.Session.SetString("accountName", staffAccount.Username);
            if (staffAccount.Image != null)
            {
                HttpContext.Session.SetString("avatar", staffAccount.Image);
            }
            HttpContext.Session.SetString("userRole", staffAccount.Role);
            return RedirectToAction("Index");
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("accountId");
            HttpContext.Session.Remove("accountName");
            HttpContext.Session.Remove("avatar");
            HttpContext.Session.Remove("userRole");
            return RedirectToAction("Login");
        }
    }
}
