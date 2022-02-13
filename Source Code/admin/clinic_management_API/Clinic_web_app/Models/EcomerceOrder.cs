using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class EcomerceOrder
    {
        public EcomerceOrder()
        {
            EcomerceEquipDetails = new HashSet<EcomerceEquipDetail>();
            EcomerceMedOrderDetails = new HashSet<EcomerceMedOrderDetail>();
        }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public virtual CustomerAccount Customer { get; set; }
        public virtual ICollection<EcomerceEquipDetail> EcomerceEquipDetails { get; set; }
        public virtual ICollection<EcomerceMedOrderDetail> EcomerceMedOrderDetails { get; set; }
    }
}
