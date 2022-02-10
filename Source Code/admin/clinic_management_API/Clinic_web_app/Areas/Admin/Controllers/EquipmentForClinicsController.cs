using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EquipmentForClinicsController : Controller
    {
        private readonly ClinicDBContext _context;

        public EquipmentForClinicsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/EquipmentForClinics
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.EquipmentForClinics.Include(e => e.Brand);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: Admin/EquipmentForClinics/Details/5
        public async Task<IActionResult> Details(string id)
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

            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId");
            return View();
        }

        // POST: Admin/EquipmentForClinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,EquipmentName,BrandId,Price,Quantity,Image")] EquipmentForClinic equipmentForClinic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentForClinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // POST: Admin/EquipmentForClinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EquipmentId,EquipmentName,BrandId,Price,Quantity,Image")] EquipmentForClinic equipmentForClinic)
        {
            if (id != equipmentForClinic.EquipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentForClinic);
                    await _context.SaveChangesAsync();
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
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", equipmentForClinic.BrandId);
            return View(equipmentForClinic);
        }

        // GET: Admin/EquipmentForClinics/Delete/5
        public async Task<IActionResult> Delete(string id)
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

            return View(equipmentForClinic);
        }

        // POST: Admin/EquipmentForClinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);
            _context.EquipmentForClinics.Remove(equipmentForClinic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentForClinicExists(string id)
        {
            return _context.EquipmentForClinics.Any(e => e.EquipmentId == id);
        }
    }
}
