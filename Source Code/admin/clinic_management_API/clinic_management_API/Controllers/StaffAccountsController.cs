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
    public class StaffAccountsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IHostingEnvironment _environment;

        public StaffAccountsController(ClinicDBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/StaffAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffAccount>>> GetStaffAccounts()
        {
            return await _context.StaffAccounts.ToListAsync();
        }

        // GET: api/StaffAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffAccount>> GetStaffAccount(string id)
        {
            var staffAccount = await _context.StaffAccounts.FindAsync(id);

            if (staffAccount == null)
            {
                return NotFound();
            }

            return staffAccount;
        }

        // PUT: api/StaffAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffAccount(string id, StaffAccount staffAccount)
        {
            if (id != staffAccount.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(staffAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffAccountExists(id))
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

        // POST: api/StaffAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StaffAccount>> PostStaffAccount(StaffAccount staffAccount)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file.FileName);
                    var newfilename = "Image_" + DateTime.Now.TimeOfDay.Milliseconds + fi.Extension;
                    var path = Path.Combine("", _environment.ContentRootPath + "/Images/" + newfilename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    StaffAccount staffAccount1 = new StaffAccount();
                    staffAccount1.Image = path;
                    staffAccount.Image = staffAccount1.Image;
                    _context.StaffAccounts.Add(staffAccount);
                    await _context.SaveChangesAsync();
                }
                return Ok(staffAccount);
            } else
            {
                return NotFound();
            }
        }

        // DELETE: api/StaffAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffAccount(string id)
        {
            var staffAccount = await _context.StaffAccounts.FindAsync(id);
            if (staffAccount == null)
            {
                return NotFound();
            }

            _context.StaffAccounts.Remove(staffAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffAccountExists(string id)
        {
            return _context.StaffAccounts.Any(e => e.AccountId == id);
        }
    }
}
