using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StaffAccountsController : Controller
    {
        private readonly ClinicDBContext _context;

        public StaffAccountsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/StaffAccounts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(await _context.StaffAccounts.ToListAsync());
        }

        // GET: Admin/StaffAccounts/Details/5
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

            var staffAccount = await _context.StaffAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (staffAccount == null)
            {
                return NotFound();
            }

            return View(staffAccount);
        }

        // GET: Admin/StaffAccounts/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffAccount staffAccount , IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    staffAccount.Image = "images/" + fileName;
                }
                _context.Add(staffAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffAccount);
        }

        // GET: Admin/StaffAccounts/Edit/5
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

            var staffAccount = await _context.StaffAccounts.FindAsync(id);
            if (staffAccount == null)
            {
                return NotFound();
            }
            return View(staffAccount);
        }

        // POST: Admin/StaffAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,StaffAccount staffAccount , IFormFile file)
        {
            if (id != staffAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        staffAccount.Image = "images/" + fileName;
                    }
                    _context.Update(staffAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffAccountExists(staffAccount.AccountId))
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
            return View(staffAccount);
        }

        // GET: Admin/StaffAccounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffAccount = await _context.StaffAccounts
                .FirstOrDefaultAsync(m => m.AccountId == id);
            _context.StaffAccounts.Remove(staffAccount);
            await _context.SaveChangesAsync();
            if (staffAccount == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        // POST: Admin/StaffAccounts/Delete/5
       

        private bool StaffAccountExists(string id)
        {
            return _context.StaffAccounts.Any(e => e.AccountId == id);
        }
    }
}
