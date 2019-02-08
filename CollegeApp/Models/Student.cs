using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class Student
    {

        [Display(Name = "Registration")]
        public int StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int CourseID {get;set;}

        public virtual Course Course { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}