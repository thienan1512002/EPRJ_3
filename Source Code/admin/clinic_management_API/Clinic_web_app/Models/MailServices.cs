using Clinic_web_app.Setting;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_web_app.Models
{
    public class MailServices
    {
        private readonly MailSettings _setting;
        public MailServices(IOptions<MailSettings> options)
        {
            _setting = options.Value;
        }
        public async Task SendMailAsync(string to , string reply)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_setting.Mail);
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Reply Feedback";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(@"~/mailTemplate/mail.html"))
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
