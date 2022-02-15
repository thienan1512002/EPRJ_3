using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class Brand
    {
        public Brand()
        {
            EquipmentForClinics = new HashSet<EquipmentForClinic>();
            
            Medicines = new HashSet<Medicine>();
        }

        public string BrandId { get; set; }
        public string BrandName { get; set; }

        public virtual ICollection<EquipmentForClinic> EquipmentForClinics { get; set; }
       
        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
