using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
{
    public partial class AdminOrderDetail
    {
        public int OrderId { get; set; }
        public int? OrderDetailId { get; set; }
        public string EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public string Purpose { get; set; }

        public virtual EquipmentForClinic Equipment { get; set; }
        public virtual AdminOrder OrderDetail { get; set; }
    }
}
