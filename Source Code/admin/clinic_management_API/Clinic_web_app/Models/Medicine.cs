using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            EcomerceMedOrderDetails = new HashSet<EcomerceMedOrderDetail>();
        }

        public string MedId { get; set; }
        public string MedName { get; set; }
        public string Type { get; set; }
        public string BrandId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public bool Featured { get; set; }
        public DateTime CreatedAt { set; get; }
        public string Image { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<EcomerceMedOrderDetail> EcomerceMedOrderDetails { get; set; }
    }
}
