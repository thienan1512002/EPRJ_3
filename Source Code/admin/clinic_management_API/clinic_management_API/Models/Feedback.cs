using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string CustomerId { get; set; }
        public string Content { get; set; }
        public DateTime? DateCreate { get; set; }
        public string Reply { get; set; }

        public virtual CustomerAccount Customer { get; set; }
    }
}
