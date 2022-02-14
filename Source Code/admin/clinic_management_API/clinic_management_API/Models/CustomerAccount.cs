using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
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
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public virtual ICollection<EcomerceOrder> EcomerceOrders { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
