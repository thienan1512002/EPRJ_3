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
        public async Task<ActionResult> PostStaffAccount(StaffViewModel staffVM)
        {
            if (staffVM.Image != null)
            {
                var a = _environment.WebRootPath;
                var fileName = Path.GetFileName(staffVM.Image.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "Images\\Staffs", fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await staffVM.Image.CopyToAsync(fileSteam);
                }

                StaffAccount staff = new StaffAccount();
                staff.AccountId = staffVM.AccountId;
                staff.Username = staffVM.Username;
                staff.Email = staffVM.Email;
                staff.Password = staffVM.Password;
                staff.Role = staffVM.Role;
                staff.Image = filePath;  //save the filePath to database ImagePath field.
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
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

        [HttpGet("process-login")]
        public async Task<StaffAccount> ProcessLogin(string username , string password)
        {
            var account = await _context.StaffAccounts.SingleOrDefaultAsync(account=>account.Username.Equals(username)&& account.Password.Equals(password));
            if (account == null)
            {
                return null;
            }
            return account;

        }

        private bool StaffAccountExists(string id)
        {
            return _context.StaffAccounts.Any(e => e.AccountId == id);
        }
    }
}
