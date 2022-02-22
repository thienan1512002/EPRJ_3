using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerAccountsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public CustomerAccountsController(ClinicDBContext context , INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/CustomerAccounts
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            const int pageSize = 5;
            var data = await PaginatedList<CustomerAccount>.CreateAsync(_context.CustomerAccounts.Where(c=>c.OTP!=null), pageNumber, pageSize);
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            return View(data);
        }

        // GET: Admin/CustomerAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            return View(customerAccount);
        }
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (HttpContext.Session.GetString("accountId") == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
        //    ViewBag.Notyf = notyf;
        //    var customerAccount = await _context.CustomerAccounts
        //        .FirstOrDefaultAsync(m => m.CustomerId == id);
        //    if (customerAccount == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customerAccount);
        //}

        // Post: Admin/CustomerAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccounts.FindAsync(id);

            if (customerAccount == null)
            {
                return NotFound();
            }
            if (customerAccount.Status.Equals("Available"))
            {
                customerAccount.Status = "Block";
                _notyf.Custom("Unlock " + customerAccount.CustomerName + " successfully", 10, "green", "fa fa-check-circle");
            }
            else
            {
                customerAccount.Status = "Available";
                _notyf.Custom("Block " + customerAccount.CustomerName + " successfully", 10, "green", "fa fa-check-circle");
            }
            _context.Update(customerAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Admin/CustomerAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.       
    }
}
