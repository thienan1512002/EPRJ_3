using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class EcomerceEquipDetail
    {
        public int OrderId { get; set; }
        public int? OrderDetailId { get; set; }
        public string EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }

        public virtual EquipmentForEcomerce Equipment { get; set; }
        public virtual EcomerceOrder OrderDetail { get; set; }
    }
}
