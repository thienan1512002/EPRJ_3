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
    public class EquipmentForEcomercesController : Controller
    {
        private readonly ClinicDBContext _context;

        public EquipmentForEcomercesController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: EquipmentForEcomerces
        public async Task<IActionResult> Index()
        {
            var clinicDBContext = _context.EquipmentForEcomerces.Include(e => e.Brand);
            return View(await clinicDBContext.ToListAsync());
        }

        // GET: EquipmentForEcomerces/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentForEcomerce = await _context.EquipmentForEcomerces
                .Include(e => e.Brand)
                .FirstOrDefaultAsync(m => m.EquipmentId == id);
            if (equipmentForEcomerce == null)
            {
                return NotFound();
            }

            return View(equipmentForEcomerce);
        }
    }
}
