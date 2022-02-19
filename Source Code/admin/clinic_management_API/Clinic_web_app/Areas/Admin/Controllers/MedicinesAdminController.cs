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
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicinesAdminController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public MedicinesAdminController(ClinicDBContext context ,INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/MedicinesAdmin
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var clinicDBContext = _context.Medicines.Include(m => m.Brand);
            const int pageSize = 5;
            var data = await PaginatedList<Medicine>.CreateAsync(clinicDBContext, pageNumber , pageSize);
            return View(data);
        }

        // GET: Admin/MedicinesAdmin/Details/5
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
            var medicine = await _context.Medicines
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.MedId == id);
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        // GET: Admin/MedicinesAdmin/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: Admin/MedicinesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicine medicine, IFormFile file, string checkbox)
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
                    medicine.Image = "images/" + fileName;
                }
                medicine.DateCreate = DateTime.Now;
                if (checkbox != null)
                {
                    medicine.Featured = true;
                }
                else
                {
                    medicine.Featured = false;
                }
                _context.Add(medicine);
                await _context.SaveChangesAsync();
                _notyf.Custom("Add new medicine completed ",10,"green","fa fa-check");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", medicine.BrandId);
            return View(medicine);
        }

        // GET: Admin/MedicinesAdmin/Edit/5
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

            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", medicine.BrandId);
            return View(medicine);
        }

        // POST: Admin/MedicinesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Medicine medicine, IFormFile file, string checkbox)
        {
            if (id != medicine.MedId)
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
                        medicine.Image = "images/" + fileName;
                    }
                    if (checkbox != null)
                    {
                        medicine.Featured = true;
                    }
                    else
                    {
                        medicine.Featured = false;
                    }

                    _context.Update(medicine);
                    await _context.SaveChangesAsync();
                    _notyf.Custom("Update new medicine completed ", 10, "green", "fa fa-check");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.MedId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", medicine.BrandId);
            return View(medicine);
        }

        // GET: Admin/MedicinesAdmin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var medicine = await _context.Medicines
                .Include(m => m.Brand)
                .FirstOrDefaultAsync(m => m.MedId == id);
            if (medicine == null)
            {
                return NotFound();
            }
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool MedicineExists(string id)
        {
            return _context.Medicines.Any(e => e.MedId == id);
        }
    }
}
