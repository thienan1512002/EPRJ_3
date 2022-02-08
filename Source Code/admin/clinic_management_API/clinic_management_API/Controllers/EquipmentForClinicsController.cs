using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_management_API.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace clinic_management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentForClinicsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IHostingEnvironment _environment;

        public EquipmentForClinicsController(ClinicDBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/EquipmentForClinics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentForClinic>>> GetEquipmentForClinics()
        {
            return await _context.EquipmentForClinics.ToListAsync();
        }

        // GET: api/EquipmentForClinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentForClinic>> GetEquipmentForClinic(string id)
        {
            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);

            if (equipmentForClinic == null)
            {
                return NotFound();
            }

            return equipmentForClinic;
        }

        // PUT: api/EquipmentForClinics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipmentForClinic(string id, EquipmentForClinic equipmentForClinic)
        {
            if (id != equipmentForClinic.EquipmentId)
            {
                return BadRequest();
            }

            _context.Entry(equipmentForClinic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentForClinicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EquipmentForClinics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipmentForClinic>> PostEquipmentForClinic([FromForm] EquipmentClinicViewModel equipVM)
        {

                if (equipVM.Image != null)
                {
                    var a = _environment.WebRootPath;
                    var fileName = Path.GetFileName(equipVM.Image.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "Images\\Equipment", fileName);

                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await equipVM.Image.CopyToAsync(fileSteam);
                    }

                    EquipmentForClinic equip = new EquipmentForClinic();
                    equip.EquipmentId = equipVM.EquipmentId;
                    equip.EquipmentName = equipVM.EquipmentName;
                    equip.BrandId = equipVM.BrandId;
                    equip.Price = equipVM.Price;
                    equip.Quantity = equipVM.Quantity;
                    equip.Image = filePath;  //save the filePath to database ImagePath field.
                    _context.Add(equip);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            
        }

        // DELETE: api/EquipmentForClinics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentForClinic(string id)
        {
            var equipmentForClinic = await _context.EquipmentForClinics.FindAsync(id);
            if (equipmentForClinic == null)
            {
                return NotFound();
            }

            _context.EquipmentForClinics.Remove(equipmentForClinic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentForClinicExists(string id)
        {
            return _context.EquipmentForClinics.Any(e => e.EquipmentId == id);
        }
    }
}
