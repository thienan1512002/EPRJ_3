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
        public IActionResult Course()
        {
            return View();
        }
        public IActionResult CustomerAccount()
        {
            return View();
        }
    }
}
