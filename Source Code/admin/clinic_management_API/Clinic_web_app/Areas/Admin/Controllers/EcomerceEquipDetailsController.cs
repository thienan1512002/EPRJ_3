using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using System.IO;
using SelectPdf;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EcomerceEquipDetailsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public EcomerceEquipDetailsController(ClinicDBContext context , INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        // GET: Admin/EcomerceEquipDetails
        public async Task<IActionResult> Index(int pageNumber =1)
        {
            const int pageSize = 10;
            var clinicDBContext = _context.EcomerceEquipDetails.Include(e => e.Equipment).Include(e => e.OrderDetail).ThenInclude(e=>e.Customer);
            var data = await PaginatedList<EcomerceEquipDetail>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            return View(data);
        }

        // GET: Admin/EcomerceEquipDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ecomerceEquipDetail = await _context.EcomerceEquipDetails
                .Include(e => e.Equipment)
                .Include(e => e.OrderDetail)
                .ThenInclude(e=>e.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            if (ecomerceEquipDetail == null)
            {
                return NotFound();
            }

            return View(ecomerceEquipDetail);
        }
        public async Task<IActionResult> MakeReport(int id)
        {
            var ecomerceEquipDetail = await _context.EcomerceEquipDetails
               .Include(e => e.Equipment)
               .Include(e => e.OrderDetail)
               .ThenInclude(e => e.Customer)
               .FirstOrDefaultAsync(m => m.OrderId == id);
            string html;
            using (StreamReader HtmlReader = System.IO.File.OpenText(@"wwwroot/reportTemplate/report.html"))
            {
                html = HtmlReader.ReadToEnd();
                html = html.Replace("cusId", ecomerceEquipDetail.OrderDetail.Customer.CustomerId);
                html = html.Replace("cusName", ecomerceEquipDetail.OrderDetail.Customer.CustomerName);
                html = html.Replace("cusAddress", ecomerceEquipDetail.OrderDetail.Address);
                html = html.Replace("cusProduct", ecomerceEquipDetail.Equipment.EquipmentName);
                html = html.Replace("cusQuantity", ecomerceEquipDetail.Quantity.ToString());
                html = html.Replace("cusTotal", ecomerceEquipDetail.Total.ToString());
            }
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
            byte[] pdf = pdfDocument.Save();
            pdfDocument.Close();
            return File(pdf, "application/pdf", "OrderReportNo"+ecomerceEquipDetail.OrderId+".pdf");
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
        private bool EcomerceEquipDetailExists(int id)
        {
            return _context.EcomerceEquipDetails.Any(e => e.OrderId == id);
        }
    }
}
