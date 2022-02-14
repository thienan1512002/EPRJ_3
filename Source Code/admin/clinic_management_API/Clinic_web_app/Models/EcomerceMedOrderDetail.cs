using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class EcomerceMedOrderDetail
    {
        public int OrderId { get; set; }
        public int? OrderDetailId { get; set; }
        public string MedId { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }

        public virtual Medicine Med { get; set; }
        public virtual EcomerceOrder OrderDetail { get; set; }
    }
}
