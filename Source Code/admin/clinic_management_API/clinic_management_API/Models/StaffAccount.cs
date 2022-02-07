using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace clinic_management_API.Models
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
    public partial class StaffViewModel
    {
        public string AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public IFormFile Image { get; set; }
    }
}
