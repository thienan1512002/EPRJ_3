using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
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
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<EcomerceMedOrderDetail> EcomerceMedOrderDetails { get; set; }
    }
}
