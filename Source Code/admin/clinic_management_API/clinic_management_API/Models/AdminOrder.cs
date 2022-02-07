using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
{
    public partial class AdminOrder
    {
        public AdminOrder()
        {
            AdminOrderDetails = new HashSet<AdminOrderDetail>();
        }

        public int OrderId { get; set; }
        public string AccountId { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual StaffAccount Account { get; set; }
        public virtual ICollection<AdminOrderDetail> AdminOrderDetails { get; set; }
    }
}
