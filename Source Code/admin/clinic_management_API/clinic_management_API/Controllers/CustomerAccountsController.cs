using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_management_API.Models;

namespace clinic_management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountsController : ControllerBase
    {
        private readonly ClinicDBContext _context;

        public CustomerAccountsController(ClinicDBContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAccount>>> GetCustomerAccounts()
        {
            return await _context.CustomerAccounts.ToListAsync();
        }

        // GET: api/CustomerAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAccount>> GetCustomerAccount(string id)
        {
            var customerAccount = await _context.CustomerAccounts.FindAsync(id);

            if (customerAccount == null)
            {
                return NotFound();
            }

            return customerAccount;
        }

        // PUT: api/CustomerAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerAccount(string id, CustomerAccount customerAccount)
        {
            if (id != customerAccount.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customerAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAccountExists(id))
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

        // POST: api/CustomerAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerAccount>> PostCustomerAccount(CustomerAccount customerAccount)
        {
            _context.CustomerAccounts.Add(customerAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerAccountExists(customerAccount.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerAccount", new { id = customerAccount.CustomerId }, customerAccount);
        }

        // DELETE: api/CustomerAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAccount(string id)
        {
            var customerAccount = await _context.CustomerAccounts.FindAsync(id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            _context.CustomerAccounts.Remove(customerAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAccountExists(string id)
        {
            return _context.CustomerAccounts.Any(e => e.CustomerId == id);
        }
    }
}
