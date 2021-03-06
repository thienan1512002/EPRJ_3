using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class EquipmentForEcomerce
    {
        public EquipmentForEcomerce()
        {
            EcomerceEquipDetails = new HashSet<EcomerceEquipDetail>();
        }

        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string BrandId { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<EcomerceEquipDetail> EcomerceEquipDetails { get; set; }
    }
}
