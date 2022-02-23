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
    public class AdminOrdersController : Controller
    {
        private readonly ClinicDBContext _context;

        public AdminOrdersController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOrders
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.AdminOrders.Include(a => a.Account).OrderByDescending(m=>m.OrderDate);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrder = await _context.AdminOrders
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrder == null)
            {
                return NotFound();
            }

            return View(adminOrder);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "AccountId");
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,AccountId,OrderDate")] AdminOrder adminOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "AccountId", adminOrder.AccountId);
            return View(adminOrder);
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrder = await _context.AdminOrders.FindAsync(id);
            if (adminOrder == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "AccountId", adminOrder.AccountId);
            return View(adminOrder);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,AccountId,OrderDate")] AdminOrder adminOrder)
        {
            if (id != adminOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminOrderExists(adminOrder.OrderId))
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
            ViewData["AccountId"] = new SelectList(_context.StaffAccounts, "AccountId", "AccountId", adminOrder.AccountId);
            return View(adminOrder);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrder = await _context.AdminOrders
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrder == null)
            {
                return NotFound();
            }

            return View(adminOrder);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminOrder = await _context.AdminOrders.FindAsync(id);
            _context.AdminOrders.Remove(adminOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminOrderExists(int id)
        {
            return _context.AdminOrders.Any(e => e.OrderId == id);
        }
    }
}
