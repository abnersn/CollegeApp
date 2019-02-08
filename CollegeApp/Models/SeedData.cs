using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollegeApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context = new CollegeAppContext(serviceProvider.GetRequiredService<DbContextOptions<CollegeAppContext>>()))
            {
                if (!context.Course.Any())
                {
                    var courses = new List<Course>
                    {
                        new Course{Name="Literatura"},
                        new Course{Name="Astrofísica"}
                    };
                    courses.ForEach(c => context.Course.Add(c));
                    context.SaveChanges();
                }

                if (!context.Student.Any())
                {
                    var students = new List<Student>
                    {
                        new Student {Name="Honório Jaguariúna", Birthday=DateTime.Parse("1999-10-03"), CourseID=6},
                        new Student {Name="Lúcia Abranches", Birthday=DateTime.Parse("2000-07-23"), CourseID=7},
                        new Student {Name="Simão Câmara", Birthday=DateTime.Parse("1998-03-15"), CourseID=6},
                        new Student {Name="Vânia Cabral", Birthday=DateTime.Parse("2001-04-24"), CourseID=6},
                        new Student {Name="Guadalupe Viana", Birthday=DateTime.Parse("1996-03-22"), CourseID=7},
                        new Student {Name="Dália Beserril", Birthday=DateTime.Parse("1999-02-12"), CourseID=6},
                        new Student {Name="Bernardo Hollanda", Birthday=DateTime.Parse("1998-08-06"), CourseID=7},
                        new Student {Name="Timóteo Viveros", Birthday=DateTime.Parse("2000-11-12"), CourseID=6},
                        new Student {Name="Zubaida Mourinho", Birthday=DateTime.Parse("2001-12-30"), CourseID=6},
                    };
                    students.ForEach(s => context.Student.Add(s));
                    context.SaveChanges();
                }

                if (!context.Teacher.Any())
                {
                    var teachers = new List<Teacher>
                    {
                        new Teacher{Name="Filinto Botica", Birthday=DateTime.Parse("1965-07-23"), Salary=4234},
                        new Teacher{Name="Inácio Madruga", Birthday=DateTime.Parse("1952-06-12"), Salary=4123},
                        new Teacher{Name="Zélia Pimenta", Birthday=DateTime.Parse("1987-05-14"), Salary=5230},
                        new Teacher{Name="Eugénio Godinho", Birthday=DateTime.Parse("1945-04-11"), Salary=7010},
                        new Teacher{Name="Felisbela Pires", Birthday=DateTime.Parse("1982-10-20"), Salary=8030}
                    };
                    teachers.ForEach(t => context.Teacher.Add(t));
                    context.SaveChanges();
                }
            }
        }
    }
}
