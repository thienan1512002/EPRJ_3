using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Models
{
    public partial class Notification
    {
        public int NotyfId { get; set; }
        public string NotyfName { get; set; }
        public DateTime DateCreate { get; set; }
        public int OrderId { get; set; }
        public bool IsRead { get; set; }
    }
}
