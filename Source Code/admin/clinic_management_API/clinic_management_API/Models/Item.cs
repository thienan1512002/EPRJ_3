using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic_management_API.Models
{
    public class Item
    {
        public Brand Brand { get; set; }
        public EquipmentForClinic EquipmentForClinic { get; set; }
        public EquipmentForEcomerce EquipmentForEcomerce { get; set; }
        public Medicine Medicine { get; set; }
    }
}
