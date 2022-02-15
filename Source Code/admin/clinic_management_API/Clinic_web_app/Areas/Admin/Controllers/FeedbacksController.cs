using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_web_app.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using MimeKit;
using Microsoft.Extensions.Options;
using Clinic_web_app.Setting;
using System.IO;
using MailKit.Security;

namespace Clinic_web_app.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbacksController : Controller
    {
        private readonly MailSettings _setting;
        private readonly ClinicDBContext _context;
        private readonly INotyfService _notyf;
        public FeedbacksController(ClinicDBContext context , INotyfService notyf , IOptions<MailSettings> options)
        {
            _context = context;
            _notyf = notyf;
            _setting = options.Value;
        }

        // GET: Admin/Feedbacks
        public async Task<IActionResult> Index(int pageNumber =1)
        {
            const int pageSize = 6;
            var clinicDBContext = _context.Feedbacks.Include(f => f.Customer);
            var data = await PaginatedList<Feedback>.CreateAsync(clinicDBContext,pageNumber,pageSize);

            return View(data);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }
        // GET: Admin/Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.Feedbacks
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                    await SendMailAsync(feedback.CustomerId, feedback.Reply);
                    _notyf.Custom("Reply this feedback successfully", 10, "green", "fa fa-check");
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(feedback);
        }
        public async Task SendMailAsync(string to, string reply)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_setting.Mail);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Reply Feedback";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(@"wwwroot/mailTemplate/mail.html"))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
                builder.HtmlBody = builder.HtmlBody.Replace("Lorem", reply);
            }
            email.Body = builder.ToMessageBody();
            using var stmp = new SmtpClient();
            stmp.Connect(_setting.Host, _setting.Port, SecureSocketOptions.StartTls);
            stmp.Authenticate(_setting.Mail, _setting.Password);
            await stmp.SendAsync(email);
            stmp.Disconnect(true);
        }
    }
}
