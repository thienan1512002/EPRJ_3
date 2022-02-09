using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;

namespace Clinic_web_app.Controllers
{
    public class CustomerAccountsController : Controller
    {
        private readonly ClinicDBContext _context;

        public CustomerAccountsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: CustomerAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerAccounts.ToListAsync());
        }

        // GET: CustomerAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            return View(customerAccount);
        }

        // GET: CustomerAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,Email,Password,Phone,Address,Status")] CustomerAccount customerAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccounts.FindAsync(id);
            if (customerAccount == null)
            {
                return NotFound();
            }
            return View(customerAccount);
        }

        // POST: CustomerAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,CustomerName,Email,Password,Phone,Address,Status")] CustomerAccount customerAccount)
        {
            if (id != customerAccount.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAccountExists(customerAccount.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Login
        public async Task<IActionResult> Login()
        {
            return View();
        }

        // POST: CustomerAccounts/Delete/5
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string txtEmail, string txtPass)
        {
            if (txtEmail == null || txtPass == null)
            {
                return View();
            }

            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.Email == txtEmail && m.Password == txtPass);
            if (customerAccount == null)
            {
                return View();
                ViewBag.mess = "Invalid email or password!";
            }
            HttpContext.Session.SetString("CustomerName", customerAccount.CustomerName);
            HttpContext.Session.SetString("CustomerId", customerAccount.CustomerId);
            

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("CustomerName");
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

        private bool CustomerAccountExists(string id)
        {
            return _context.CustomerAccounts.Any(e => e.CustomerId == id);
        }
    }
}
