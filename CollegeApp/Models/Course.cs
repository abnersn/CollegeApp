using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}