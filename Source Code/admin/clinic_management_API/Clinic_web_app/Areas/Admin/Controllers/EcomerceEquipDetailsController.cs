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
    public class EcomerceEquipDetailsController : Controller
    {
        private readonly ClinicDBContext _context;

        public EcomerceEquipDetailsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: Admin/EcomerceEquipDetails
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.EcomerceEquipDetails.Include(e => e.Equipment).Include(e => e.OrderDetail).ThenInclude(e=>e.Customer);
            return View(await clinicDBContext.ToListAsync());
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
            if (ecomerceEquipDetail == null)
            {
                return NotFound();
            }

            return View(ecomerceEquipDetail);
        }

        private bool EcomerceEquipDetailExists(int id)
        {
            return _context.EcomerceEquipDetails.Any(e => e.OrderId == id);
        }
    }
}
