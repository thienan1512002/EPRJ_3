using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Models
{
    public class MailRequest
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
