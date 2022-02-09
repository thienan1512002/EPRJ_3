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
    public class EquipmentForEcomercesController : Controller
    {
        private readonly ClinicDBContext _context;

        public EquipmentForEcomercesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/EquipmentForEcomerces
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.EquipmentForEcomerces.Include(e => e.Brand);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: Admin/EquipmentForEcomerces/Details/5
        public async Task<IActionResult> Details(string id)
        {
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
                _context.Add(equipmentForEcomerce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", equipmentForEcomerce.BrandId);
            return View(equipmentForEcomerce);
        }

        // GET: Admin/EquipmentForEcomerces/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
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

        // POST: Admin/EquipmentForEcomerces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var equipmentForEcomerce = await _context.EquipmentForEcomerces.FindAsync(id);
            _context.EquipmentForEcomerces.Remove(equipmentForEcomerce);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentForEcomerceExists(string id)
        {
            return _context.EquipmentForEcomerces.Any(e => e.EquipmentId == id);
        }
    }
}
