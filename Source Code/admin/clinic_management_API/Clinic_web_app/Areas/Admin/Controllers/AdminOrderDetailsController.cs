using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminOrderDetailsController : Controller
    {
        private readonly ClinicDBContext _context;

        public AdminOrderDetailsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOrderDetails
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (HttpContext.Session.GetString("userRole").Equals("Manager"))
            {
                var clinicDBContext = _context.AdminOrderDetails.Include(a => a.Equipment).Include(a => a.OrderDetail).ThenInclude(a => a.Account);
                return View(await clinicDBContext.ToListAsync());
            }else
            {
                var clinicDBContext = _context.AdminOrderDetails.Include(a => a.Equipment).Include(a => a.OrderDetail).ThenInclude(a => a.Account).Where(a=>a.OrderDetail.AccountId.Equals(HttpContext.Session.GetString("accountId")));
                return View(await clinicDBContext.ToListAsync());
            }
            
        }

        // GET: Admin/AdminOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrderDetail = await _context.AdminOrderDetails
                .Include(a => a.Equipment)
                .Include(a => a.OrderDetail)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrderDetail == null)
            {
                return NotFound();
            }

            return View(adminOrderDetail);
        }

        // GET: Admin/AdminOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentForClinics, "EquipmentId", "EquipmentId");
            ViewData["OrderDetailId"] = new SelectList(_context.AdminOrders, "OrderId", "OrderId");
            return View();
        }

        // POST: Admin/AdminOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDetailId,EquipmentId,Quantity,Purpose")] AdminOrderDetail adminOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentForClinics, "EquipmentId", "EquipmentId", adminOrderDetail.EquipmentId);
            ViewData["OrderDetailId"] = new SelectList(_context.AdminOrders, "OrderId", "OrderId", adminOrderDetail.OrderDetailId);
            return View(adminOrderDetail);
        }

        // GET: Admin/AdminOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrderDetail = await _context.AdminOrderDetails.FindAsync(id);
            if (adminOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentForClinics, "EquipmentId", "EquipmentId", adminOrderDetail.EquipmentId);
            ViewData["OrderDetailId"] = new SelectList(_context.AdminOrders, "OrderId", "OrderId", adminOrderDetail.OrderDetailId);
            return View(adminOrderDetail);
        }

        // POST: Admin/AdminOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDetailId,EquipmentId,Quantity,Purpose")] AdminOrderDetail adminOrderDetail)
        {
            if (id != adminOrderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminOrderDetailExists(adminOrderDetail.OrderId))
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
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentForClinics, "EquipmentId", "EquipmentId", adminOrderDetail.EquipmentId);
            ViewData["OrderDetailId"] = new SelectList(_context.AdminOrders, "OrderId", "OrderId", adminOrderDetail.OrderDetailId);
            return View(adminOrderDetail);
        }

        // GET: Admin/AdminOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminOrderDetail = await _context.AdminOrderDetails
                .Include(a => a.Equipment)
                .Include(a => a.OrderDetail)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrderDetail == null)
            {
                return NotFound();
            }

            return View(adminOrderDetail);
        }

        // POST: Admin/AdminOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminOrderDetail = await _context.AdminOrderDetails.FindAsync(id);
            _context.AdminOrderDetails.Remove(adminOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminOrderDetailExists(int id)
        {
            return _context.AdminOrderDetails.Any(e => e.OrderId == id);
        }
    }
}
