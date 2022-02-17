using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using Microsoft.AspNetCore.Http;
using Clinic_web_app.Setting;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using MailKit.Security;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace Clinic_web_app.Controllers
{
    public class CustomerAccountsController : Controller
    {
        private readonly ClinicDBContext _context;
        private readonly MailSettings _setting;
        private readonly INotyfService _notyf;
        public CustomerAccountsController(ClinicDBContext context, IOptions<MailSettings> options, INotyfService notyf)
        {
            _context = context;
            _setting = options.Value;
            _notyf = notyf;
        }

        // GET: CustomerAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerAccounts.ToListAsync());
        }

        // GET: CustomerAccounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerAccount == null)
            {
                return NotFound();
            }

            return View(customerAccount);
        }

        // GET: CustomerAccounts/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Authenticate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OTPProcess(string otp)
        {
            if (HttpContext.Session.GetString("OTP") == null)
            {
                _notyf.Warning("Your OTP is expried");
                return RedirectToAction("Create");
            }
            if (!HttpContext.Session.GetString("OTP").Equals(otp))
            {
                _notyf.Warning("Your OTP is not match");
                return RedirectToAction("Authenticate");
            }
            else
            {
                var account = await _context.CustomerAccounts.FirstOrDefaultAsync(m => m.CustomerId == HttpContext.Session.GetString("accountTemp"));
                account.OTP = otp;
                _context.CustomerAccounts.Update(account);
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("state");
                return RedirectToAction("Login");
            }
        }

        public async Task GenerateOTP(string mailSent)
        {
            Random data = new Random();
            var OTP = data.Next(100000, 999999).ToString();
            HttpContext.Session.SetString("OTP", OTP);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_setting.Mail);
            email.To.Add(MailboxAddress.Parse(mailSent));
            email.Subject = "OTP Code";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(@"wwwroot/mailTemplate/reply.html"))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
                builder.HtmlBody = builder.HtmlBody.Replace("Lorem", "Your OTP is " + OTP);
            }
            email.Body = builder.ToMessageBody();
            using var stmp = new SmtpClient();
            stmp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
            stmp.Authenticate(_setting.Mail, _setting.Password);
            await stmp.SendAsync(email);
            stmp.Disconnect(true);

        }
        // POST: CustomerAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,Email,Password,Phone,Address,Status")] CustomerAccount customerAccount)
        {

            if (ModelState.IsValid)
            {
                var check = await _context.CustomerAccounts
                .FirstOrDefaultAsync(c => c.Email == customerAccount.Email);
                if (check == null)
                {
                    customerAccount.CustomerId = customerAccount.Email;
                    _context.Add(customerAccount);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("accountTemp", customerAccount.CustomerId);
                    HttpContext.Session.SetInt32("state", 0);
                    //OTP send to Customer Email
                    await GenerateOTP(customerAccount.Email);
                    return RedirectToAction(nameof(Authenticate));
                }
                else
                {
                    ViewBag.mess = "Email already exists";
                    return View();
                }
            }
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerAccount = await _context.CustomerAccounts.FindAsync(id);
            if (customerAccount == null)
            {
                return NotFound();
            }
            return View(customerAccount);
        }

        // POST: CustomerAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,CustomerName,Email,Password,Phone,Address,Status")] CustomerAccount customerAccount)
        {
            if (id != customerAccount.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _context.CustomerAccounts.FindAsync(id);
                    if (customerAccount.Password == null || customerAccount.Password == "")
                    {
                        customerAccount.Password = customer.Password;
                    }
                    customerAccount.Status = "Available";
                    _context.Update(customerAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAccountExists(customerAccount.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerAccount);
        }

        // GET: CustomerAccounts/Login
        public async Task<IActionResult> Login()
        {
            if (HttpContext.Session.GetInt32("state") != null)
            {
                if (HttpContext.Session.GetInt32("state") == 0)
                {
                    return RedirectToAction("Authenticate");
                }
            }
            return View();
        }

        // POST: CustomerAccounts/Delete/5
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string txtEmail, string txtPass)
        {
            if (txtEmail == null || txtPass == null)
            {
                return View();
            }
            var customerAccount = await _context.CustomerAccounts
                .FirstOrDefaultAsync(m => m.Email == txtEmail && m.Password == txtPass);
            if (customerAccount == null)
            {
                ViewBag.mess = "Invalid email or password!";
                return View();

            }
            if (customerAccount.Status == "BLOCK")
            {
                ViewBag.mess = "Your account is suspended and is not permitted to perform this action";
                return View();

            }
            HttpContext.Session.SetString("CustomerName", customerAccount.CustomerName);
            HttpContext.Session.SetString("CustomerId", customerAccount.CustomerId);


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("CustomerName");
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

        private bool CustomerAccountExists(string id)
        {
            return _context.CustomerAccounts.Any(e => e.CustomerId == id);
        }
    }
}
