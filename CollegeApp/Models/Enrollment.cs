using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class Enrollment
    {

        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }

        [Range(0, 10.0)]
        [DisplayFormat(NullDisplayText = "Sem nota")]
        public float? Grade { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }

    }
}