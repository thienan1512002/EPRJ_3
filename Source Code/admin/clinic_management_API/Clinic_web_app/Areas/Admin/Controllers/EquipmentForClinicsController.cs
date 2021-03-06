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
    public class EquipmentForClinicsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public EquipmentForClinicsController(ClinicDBContext context , INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/EquipmentForClinics
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var clinicDBContext = _context.EquipmentForClinics.Include(e => e.Brand).OrderByDescending(m=>m.DateCreate);
            const int pageSize = 5;
            var data = await PaginatedList<EquipmentForClinic>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            return View(data);
        }

        // GET: Admin/EquipmentForClinics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForClinic = await _context.EquipmentForClinics
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }

            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Create
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

        // POST: Admin/EquipmentForClinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentForClinic equipmentForClinic, IFormFile file)
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
                    equipmentForClinic.Image = "images/" + fileName;
                }
                equipmentForClinic.DateCreate = DateTime.Now;
                _context.Add(equipmentForClinic);
                await _context.SaveChangesAsync();
                _notyf.Custom("Add " + equipmentForClinic.EquipmentName + " successfully",10,"green","fa fa-check-circle");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // POST: Admin/EquipmentForClinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EquipmentForClinic equipmentForClinic, IFormFile file)
        {

            if (id != equipmentForClinic.EquipmentId)
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
                        equipmentForClinic.Image = "images/" + fileName;
                    }
                    equipmentForClinic.DateCreate = DateTime.Now;
                    _context.Update(equipmentForClinic);
                    await _context.SaveChangesAsync();
                    _notyf.Custom("Update " + equipmentForClinic.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentForClinicExists(equipmentForClinic.EquipmentId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForClinic = await _context.EquipmentForClinics
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }
            if (EquipmentHasOrderClinic(equipmentForClinic.EquipmentId))
            {
                _notyf.Custom(" Cannot delete " + equipmentForClinic.EquipmentName + " because someone has rented this equipment", 10, "orange", "fa fa-exclamation");
                return RedirectToAction("Index");
            }
            _context.EquipmentForClinics.Remove(equipmentForClinic);
            _notyf.Custom("Delete " + equipmentForClinic.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool EquipmentForClinicExists(string id)
        {
            return _context.EquipmentForClinics.Any(e => e.EquipmentId == id);
        }
        private bool EquipmentHasOrderClinic(string id)
        {
            return _context.AdminOrderDetails.Any(e => e.EquipmentId == id);
        }
    }
}
