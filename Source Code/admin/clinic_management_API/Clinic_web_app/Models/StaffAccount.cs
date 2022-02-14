using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class StaffAccount
    {
        public StaffAccount()
        {
            AdminOrders = new HashSet<AdminOrder>();
            Enrollments = new HashSet<Enrollment>();
        }

        public string AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }

        public virtual ICollection<AdminOrder> AdminOrders { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
