using AspNetCoreHero.ToastNotification.Abstractions;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public HomeController(ILogger<HomeController> logger , ClinicDBContext context , INotyfService notyf)
        {
            _logger = logger;
            _context = context;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendFeedback(string content)
        {
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                return RedirectToAction("Login", "CustomerAccounts");
            }
            Feedback feedback = new Feedback();
            feedback.CustomerId = HttpContext.Session.GetString("CustomerId");
            feedback.Content = content;
            feedback.DateCreate = DateTime.Now;
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            _notyf.Custom("Send feedback sucessfully , We will reply you on email as soon as we can",10,"green","fa fa-check");
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult KiviNotFound()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
