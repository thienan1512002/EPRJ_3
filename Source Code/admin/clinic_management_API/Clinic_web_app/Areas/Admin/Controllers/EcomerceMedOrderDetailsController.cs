using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EcomerceMedOrderDetailsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public EcomerceMedOrderDetailsController(ClinicDBContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/EcomerceMedOrderDetails
        public async Task<IActionResult> Index(int pageNumber=1)
        {
            const int pageSize = 10;
            var clinicDBContext = _context.EcomerceMedOrderDetails.Include(e => e.Med).Include(e => e.OrderDetail).ThenInclude(e => e.Customer);
            var data = await PaginatedList<EcomerceMedOrderDetail>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            return View(data);
        }

        // GET: Admin/EcomerceMedOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecomerceEquipDetail = await _context.EcomerceMedOrderDetails
                .Include(e => e.Med)
                .Include(e => e.OrderDetail)
                .ThenInclude(e => e.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (ecomerceEquipDetail == null)
            {
                return NotFound();
            }

            return View(ecomerceEquipDetail);
        }
        public async Task<IActionResult> CompletedOrder(int id)
        {
            var order = await _context.EcomerceOrders.SingleOrDefaultAsync(m => m.OrderId == id);
            order.Status = "Completed";
            _context.EcomerceOrders.Update(order);
            await _context.SaveChangesAsync();
            _notyf.Custom("Order has been completed", 10, "green", "fa fa-check-circle");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CloseOrder(int id)
        {
            var order = await _context.EcomerceOrders.SingleOrDefaultAsync(m => m.OrderId == id);
            order.Status = "Decline";
            _context.EcomerceOrders.Update(order);
            await _context.SaveChangesAsync();
            _notyf.Custom("Order has been cancle", 10, "green", "fa fa-check-circle");
            return RedirectToAction("Index");
        }
    }
}
