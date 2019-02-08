using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class Subject
    {

        public int SubjectID { get; set; }

        [Required]
        public int? TeacherID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}