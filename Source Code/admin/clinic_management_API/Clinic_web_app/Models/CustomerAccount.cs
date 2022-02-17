using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class CustomerAccount
    {
        public CustomerAccount()
        {
            EcomerceOrders = new HashSet<EcomerceOrder>();
            Feedbacks = new HashSet<Feedback>();
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string OTP { get; set; }

        public virtual ICollection<EcomerceOrder> EcomerceOrders { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
