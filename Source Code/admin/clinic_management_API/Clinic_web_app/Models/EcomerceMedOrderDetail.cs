using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class EcomerceMedOrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string MedId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }

        public virtual Medicine Med { get; set; }
        public virtual EcomerceOrder OrderDetail { get; set; }
    }
}
