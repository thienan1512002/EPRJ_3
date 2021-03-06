using System;
using System.Collections.Generic;

#nullable disable

namespace Clinic_web_app.Models
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
        public int Slot { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
