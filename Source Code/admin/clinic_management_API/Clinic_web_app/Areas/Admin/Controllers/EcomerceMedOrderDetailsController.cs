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
using System.Text;

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
            var clinicDBContext = _context.EcomerceOrders.OrderByDescending(o=>o.OrderDate);
            var data = await PaginatedList<EcomerceOrder>.CreateAsync(clinicDBContext, pageNumber, pageSize);
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            return View (data);
        }

        // GET: Admin/EcomerceMedOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var notyf = await _context.Notifications.Where(m => m.IsRead == false).ToListAsync();
            ViewBag.Notyf = notyf;
            var ecomerceEquipDetail = await _context.EcomerceOrders
                .Include(e => e.EcomerceMedOrderDetails)
                .ThenInclude(e => e.Med)
                .ThenInclude(e=>e.Brand)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            var seenNotyf = await _context.Notifications.FirstOrDefaultAsync(e => e.OrderId == ecomerceEquipDetail.OrderId);
            seenNotyf.IsRead = true;
            _context.Notifications.Update(seenNotyf);
            await _context.SaveChangesAsync();
            if (ecomerceEquipDetail == null)
            {
                return NotFound();
            }

            return View(ecomerceEquipDetail);
        }
        public async Task<IActionResult> MakeReport(int id)
        {
            var ecomerceMedDetail = await _context.EcomerceOrders
               .Include(e => e.EcomerceMedOrderDetails)
               .ThenInclude(e => e.Med)
               .ThenInclude(e => e.Brand)
               .FirstOrDefaultAsync(m => m.OrderId == id);
            string html;
            using (StreamReader HtmlReader = System.IO.File.OpenText(@"wwwroot/reportTemplate/reporttemplate/report.html"))
            {
                html = HtmlReader.ReadToEnd();
                //html = html.Replace("cusId", ecomerceMedDetail.OrderDetail.Customer.CustomerId);
                html=html.Replace("@cusName",ecomerceMedDetail.CustomerName);
                html =html.Replace("@cusAddress",ecomerceMedDetail.Address);
                html = html.Replace("@cusMail", ecomerceMedDetail.Email);
                html=html.Replace("@orderDate", ecomerceMedDetail.OrderDate.ToString());

                decimal grandTotal = 0;
                decimal productTotal = 0;
                StringBuilder sb = new StringBuilder();
                foreach (var item in ecomerceMedDetail.EcomerceMedOrderDetails)
                {
                    productTotal = item.Med.Price * Convert.ToDecimal(item.Quantity);
                    grandTotal += productTotal;
                    sb.AppendFormat("<tr>"+
                        "<td>{0}</td>"+
                        "<td>{1}</td>"+
                        "<td>{2}</td>"+
                        "<td>{3}</td>"+
                        "<td>{4}</td>"+
                        "</tr>",item.Med.MedId,item.Med.MedName,item.Med.Price,item.Quantity,productTotal);
                }
                html=html.Replace("@data", sb.ToString());
                html = html.Replace("@grandTotal", grandTotal.ToString());
                
            }
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(html);
            byte[] pdf = pdfDocument.Save();
            pdfDocument.Close();
            return File(pdf, "application/pdf","OrderReportNo"+ecomerceMedDetail.OrderId+".pdf");
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
            var order = await _context.EcomerceOrders.Include(o=>o.EcomerceMedOrderDetails).SingleOrDefaultAsync(m => m.OrderId == id);
            order.Status = "Decline";
            _context.EcomerceOrders.Update(order);
            await _context.SaveChangesAsync();
            foreach(var item in order.EcomerceMedOrderDetails)
            {
                await QuantityReduce(item.MedId, item.Quantity.Value);
            }
            _notyf.Custom("Order has been cancle", 10, "green", "fa fa-check-circle");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> QuantityReduce(string medId, int quantity)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.MedId == medId);
            medicine.Quantity += quantity;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
