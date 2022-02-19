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
    public class BrandsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public BrandsController(ClinicDBContext context , INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            const int pageSize = 5;
            var data = await PaginatedList<Brand>.CreateAsync(_context.Brands,pageNumber, pageSize);
            return View(data);
        }

        // GET: Admin/Brands/Details/5
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
            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Brand brand)
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                _notyf.Custom("Add " + brand.BrandName + " successfully", 10, "green","fa fa-check-circle");
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Admin/Brands/Edit/5
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
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BrandId,BrandName")] Brand brand)
        {
            if (id != brand.BrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    _notyf.Custom("Update " + brand.BrandName + " successfully", 10, "green", "fa fa-check-circle");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.BrandId))
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
            return View(brand);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            if (BrandUsedByMed(brand.BrandId))
            {
                _notyf.Custom("Cannot delete " + brand.BrandName + " because some medicines still use this brand", 10, "orange", "fa fa-exclamation");
                return RedirectToAction("Index");
            }
            if (BrandUsedByEquipClinic(brand.BrandId))
            {
                _notyf.Custom("Cannot delete " + brand.BrandName + " because some euipment in Clinic still use this brand", 10, "orange", "fa fa-exclamation");
                return RedirectToAction("Index");
            }
            if (BrandUsedByEquipEcomerce(brand.BrandId))
            {
                _notyf.Custom("Cannot delete " + brand.BrandName + " because some euipment in ecomerce page still use this brand", 10, "orange", "fa fa-exclamation");
                return RedirectToAction("Index");
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            _notyf.Custom("Delete " + brand.BrandName + " successfully", 10, "green", "fa fa-check-circle");
            return RedirectToAction(nameof(Index));
        }
       
        private bool BrandExists(string id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
        private bool BrandUsedByMed(string id)
        {
            return _context.Medicines.Any(e => e.BrandId == id);
        }
        private bool BrandUsedByEquipClinic(string id)
        {
            return _context.EquipmentForClinics.Any(e => e.BrandId == id);
        }
        private bool BrandUsedByEquipEcomerce(string id)
        {
            return _context.EquipmentForEcomerces.Any(e => e.BrandId == id);
        }
    }
}
