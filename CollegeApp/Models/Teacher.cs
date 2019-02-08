using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.Models
{
    public class Teacher
    {

        public int TeacherID { get; set; }
        [StringLength(100, ErrorMessage = "O nome deve possuir menos de 100 caracteres.")]

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }
    }
}