using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
