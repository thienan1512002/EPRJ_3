using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public string CourseId { get; set; }
        public string AccountId { get; set; }

        public virtual StaffAccount Account { get; set; }
        public virtual Course Course { get; set; }
    }
}
