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

            var data = await _context.EcomerceOrders.ToListAsync();
            var orderToday = await _context.EcomerceMedOrderDetails
                .Include(e => e.OrderDetail)
                .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 0 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay2 = await _context.EcomerceMedOrderDetails
                .Include(e => e.OrderDetail)
                .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 1 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay3 = await _context.EcomerceMedOrderDetails
                .Include(e => e.OrderDetail)
                .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 2 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay4 = await _context.EcomerceMedOrderDetails
               .Include(e => e.OrderDetail)
               .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 3 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay5 = await _context.EcomerceMedOrderDetails
               .Include(e => e.OrderDetail)
               .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 4 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay6 = await _context.EcomerceMedOrderDetails
               .Include(e => e.OrderDetail)
               .Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 5 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var orderDay7 = await _context.EcomerceMedOrderDetails.Include(e => e.OrderDetail).Where(p => EF.Functions.DateDiffDay(p.OrderDetail.OrderDate, DateTime.Today) == 6 && p.OrderDetail.Status.Equals("Completed")).ToListAsync();
            var customerRegisted = await _context.CustomerAccounts.ToListAsync();
            var staffAccount = await _context.StaffAccounts.ToListAsync();
            var notyf = await _context.Notifications.Where(m=>m.IsRead==false).ToListAsync();
            var course = await _context.Courses.Where(c => EF.Functions.DateDiffMonth(c.StartDate, DateTime.Today) == 0).ToListAsync();
            ViewBag.Notyf = notyf;
            ViewBag.CourseInMonth = course;
            ViewBag.OrderToday = orderToday;
            ViewBag.OrderDay2 = orderDay2;
            ViewBag.OrderDay3 = orderDay3;
            ViewBag.OrderDay4 = orderDay4;
            ViewBag.OrderDay5 = orderDay5;
            ViewBag.OrderDay6 = orderDay6;
            ViewBag.OrderDay7 = orderDay7;
            ViewBag.Customer = customerRegisted;
            ViewBag.Staff = staffAccount;
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
