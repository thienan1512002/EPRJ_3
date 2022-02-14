using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;

namespace Clinic_web_app.Controllers
{
    public class EcomerceOrdersController : Controller
    {
        private readonly ClinicDBContext _context;

        public EcomerceOrdersController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: EcomerceOrders
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.EcomerceOrders.Include(e => e.Customer);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: EcomerceOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecomerceOrder = await _context.EcomerceOrders
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (ecomerceOrder == null)
            {
                return NotFound();
            }

            return View(ecomerceOrder);
        }

        // GET: EcomerceOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomerAccounts, "CustomerId", "CustomerId");
            return View();
        }

        // POST: EcomerceOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,Address")] EcomerceOrder ecomerceOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ecomerceOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerAccounts, "CustomerId", "CustomerId", ecomerceOrder.CustomerId);
            return View(ecomerceOrder);
        }

        // GET: EcomerceOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecomerceOrder = await _context.EcomerceOrders.FindAsync(id);
            if (ecomerceOrder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerAccounts, "CustomerId", "CustomerId", ecomerceOrder.CustomerId);
            return View(ecomerceOrder);
        }

        // POST: EcomerceOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,Address")] EcomerceOrder ecomerceOrder)
        {
            if (id != ecomerceOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ecomerceOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EcomerceOrderExists(ecomerceOrder.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerAccounts, "CustomerId", "CustomerId", ecomerceOrder.CustomerId);
            return View(ecomerceOrder);
        }

        // GET: EcomerceOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecomerceOrder = await _context.EcomerceOrders
                .Include(e => e.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (ecomerceOrder == null)
            {
                return NotFound();
            }

            return View(ecomerceOrder);
        }

        // POST: EcomerceOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ecomerceOrder = await _context.EcomerceOrders.FindAsync(id);
            _context.EcomerceOrders.Remove(ecomerceOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EcomerceOrderExists(int id)
        {
            return _context.EcomerceOrders.Any(e => e.OrderId == id);
        }
    }
}
