using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clinic_management_API.Models;


namespace clinic_management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombindToBrandsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        ClinicDBContext db = new ClinicDBContext();
        public CombindToBrandsController(ClinicDBContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var data = from b in db.Brands
                       join c in db.EquipmentForClinics
                       on b.BrandId equals c.BrandId
                       select new Item
                       {
                           Brand = b,
                           EquipmentForClinic = c,
                       };
            return Ok(data);
        }
    }
}
