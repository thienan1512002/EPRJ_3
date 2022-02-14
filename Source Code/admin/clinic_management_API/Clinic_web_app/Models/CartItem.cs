using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_web_app.Models
{
    public class CartItem
    {
        public int quantity { set; get; }
        public Medicine medicine { set; get; }
    }
}
