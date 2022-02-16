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
    public class EquipmentForEcomercesController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public EquipmentForEcomercesController(ClinicDBContext context , INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/EquipmentForEcomerces
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var clinicDBContext = _context.EquipmentForEcomerces.Include(e => e.Brand);
            int pageSize = 5;
            var data = await PaginatedList<EquipmentForEcomerce>.CreateAsync(clinicDBContext, pageNumber,pageSize);
            return View(data);
        }

        // GET: Admin/EquipmentForEcomerces/Details/5
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

            var equipmentForEcomerce = await _context.EquipmentForEcomerces
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (equipmentForEcomerce == null)
            {
                return NotFound();
            }

            return View(equipmentForEcomerce);
        }

        // GET: Admin/EquipmentForEcomerces/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            return View();
        }

        // POST: Admin/EquipmentForEcomerces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipmentForEcomerce equipmentForEcomerce , IFormFile file)
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
                    equipmentForEcomerce.Image = "images/" + fileName;
                }
                equipmentForEcomerce.DateCreate = DateTime.Now;
                _context.Add(equipmentForEcomerce);
                await _context.SaveChangesAsync();
                _notyf.Custom("Add " + equipmentForEcomerce.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForEcomerce.BrandId);
            return View(equipmentForEcomerce);
        }

        // GET: Admin/EquipmentForEcomerces/Edit/5
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

            var equipmentForEcomerce = await _context.EquipmentForEcomerces.FindAsync(id);
            if (equipmentForEcomerce == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForEcomerce.BrandId);
            return View(equipmentForEcomerce);
        }

        // POST: Admin/EquipmentForEcomerces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EquipmentForEcomerce equipmentForEcomerce , IFormFile file)
        {
            if (id != equipmentForEcomerce.EquipmentId)
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
                        equipmentForEcomerce.Image = "images/" + fileName;
                    }
                    _context.Update(equipmentForEcomerce);
                    await _context.SaveChangesAsync();
                    _notyf.Custom("Update " + equipmentForEcomerce.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentForEcomerceExists(equipmentForEcomerce.EquipmentId))
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", equipmentForEcomerce.BrandId);
            return View(equipmentForEcomerce);
        }

        // GET: Admin/EquipmentForEcomerces/Delete/5
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

            var equipmentForEcomerce = await _context.EquipmentForEcomerces
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (equipmentForEcomerce == null)
            {
                return NotFound();
            }
            if (EquipmentHasOrderEcomerce(equipmentForEcomerce.EquipmentId))
            {
                _notyf.Custom(" Cannot delete " + equipmentForEcomerce.EquipmentName + " because someone has rented this equipment", 10, "orange", "fa fa-exclamation");
                return RedirectToAction("Index");
            }
            _context.EquipmentForEcomerces.Remove(equipmentForEcomerce);
            await _context.SaveChangesAsync();
            _notyf.Custom("Delete " + equipmentForEcomerce.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentHasOrderEcomerce(string id)
        {
            return _context.EcomerceEquipDetails.Any(e => e.EquipmentId == id);
        }

        private bool EquipmentForEcomerceExists(string id)
        {
            return _context.EquipmentForEcomerces.Any(e => e.EquipmentId == id);
        }
    }
}
