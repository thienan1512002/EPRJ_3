using System;
using System.Collections.Generic;

#nullable disable

namespace clinic_management_API.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Lectures { get; set; }
        public DateTime? StartDate { get; set; }
        public string Location { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
