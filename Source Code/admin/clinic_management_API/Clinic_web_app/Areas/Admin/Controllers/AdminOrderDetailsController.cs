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
    public class AdminOrderDetailsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public AdminOrderDetailsController(ClinicDBContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/AdminOrderDetails
        public async Task<IActionResult> Index(int pageNumber =1)
        {
            const int pageSize = 5;
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            if (HttpContext.Session.GetString("accountId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (HttpContext.Session.GetString("userRole").Equals("Manager"))
            {
                var clinicDBContext = _context.AdminOrderDetails.Include(a => a.Equipment).Include(a => a.OrderDetail).ThenInclude(a => a.Account);
                var data = await PaginatedList<AdminOrderDetail>.CreateAsync(clinicDBContext, pageNumber, pageSize);
                return View(data);
            }
            else
            {
                var clinicDBContext = _context.AdminOrderDetails.Include(a => a.Equipment).Include(a => a.OrderDetail).ThenInclude(a => a.Account).Where(a => a.OrderDetail.AccountId.Equals(HttpContext.Session.GetString("accountId")));
                var data = await PaginatedList<AdminOrderDetail>.CreateAsync(clinicDBContext, pageNumber, pageSize);
                return View(data);
            }

        }

        // GET: Admin/AdminOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var adminOrderDetail = await _context.AdminOrderDetails
                .Include(a => a.Equipment)
                .Include(a => a.OrderDetail)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrderDetail == null)
            {
                return NotFound();
            }

            return View(adminOrderDetail);
        }

        // GET: Admin/AdminOrderDetails/Create
        public async Task<IActionResult> Create()
        {
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentForClinics, "EquipmentId", "EquipmentName");
            return View();
        }

        // POST: Admin/AdminOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminOrderDetail adminOrderDetail, string equimentId, int quantity, string purpose)
        {
            
            
                AdminOrder order = GetOrderInfomation();

                _context.AdminOrders.Add(order);
                await _context.SaveChangesAsync();
                adminOrderDetail.OrderDetailId = order.OrderId;
                adminOrderDetail.EquipmentId = equimentId;
                adminOrderDetail.Quantity = quantity;
                adminOrderDetail.Purpose = purpose;

                //Reduce quantity of Equipment
                var equipmentForClinic = await EquipmentForClinicExists(adminOrderDetail.EquipmentId);
                if (equipmentForClinic.Quantity < adminOrderDetail.Quantity)
                {
                    ViewBag.Message=" We don't have enough quantity you required";
                    return View();
                }
                equipmentForClinic.Quantity = equipmentForClinic.Quantity - quantity;
                _context.EquipmentForClinics.Update(equipmentForClinic);
                await _context.SaveChangesAsync();
                _context.Add(adminOrderDetail);
                await _context.SaveChangesAsync();
                _notyf.Custom("Rent " + adminOrderDetail.Equipment.EquipmentName + " successfully", 10, "green", "fa fa-check-circle");
                return RedirectToAction(nameof(Index));           
        }

        private async Task<EquipmentForClinic> EquipmentForClinicExists(string equipmentId)
        {
            return await _context.EquipmentForClinics.FirstOrDefaultAsync(m => m.EquipmentId == equipmentId);
        }
        public async Task<ActionResult> Finish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var adminOrderDetail = await _context.AdminOrderDetails
                .Include(a => a.Equipment)
                .Include(a => a.OrderDetail)
                .ThenInclude(a => a.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (adminOrderDetail == null)
            {
                return NotFound();
            }

            return View(adminOrderDetail);
        }
        [HttpPost, ActionName("Finish")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> FinishConfirm(int id)
        {
            var order = await _context.AdminOrders.SingleOrDefaultAsync(m => m.OrderId == id);
            var orderDetail = await _context.AdminOrderDetails.FirstOrDefaultAsync(m => m.OrderDetailId==id);
            var equipmentForClinic = await EquipmentForClinicExists(orderDetail.EquipmentId);
            equipmentForClinic.Quantity += orderDetail.Quantity;
            _context.EquipmentForClinics.Update(equipmentForClinic);
            await _context.SaveChangesAsync();
            order.Status = "Finished";
            _context.AdminOrders.Update(order);
            await _context.SaveChangesAsync();
            _notyf.Custom("Rent Finished ", 10, "green", "fa fa-check-circle");
            return RedirectToAction("Index");
        }
        private AdminOrder GetOrderInfomation()
        {
            AdminOrder order = new AdminOrder();
            order.AccountId = HttpContext.Session.GetString("accountId");
            order.OrderDate = DateTime.Now;
            order.Status = "Not Yet";
            return order;
        }





        private bool AdminOrderDetailExists(int id)
        {
            return _context.AdminOrderDetails.Any(e => e.OrderId == id);
        }
    }
}
