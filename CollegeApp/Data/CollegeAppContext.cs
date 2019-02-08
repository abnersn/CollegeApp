using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Models;

namespace CollegeApp.Models
{
    public class CollegeAppContext : DbContext
    {
        public CollegeAppContext (DbContextOptions<CollegeAppContext> options)
            : base(options)
        {
        }

        public DbSet<CollegeApp.Models.Course> Course { get; set; }

        public DbSet<CollegeApp.Models.Subject> Subject { get; set; }

        public DbSet<CollegeApp.Models.Teacher> Teacher { get; set; }

        public DbSet<CollegeApp.Models.Student> Student { get; set; }

        public DbSet<CollegeApp.Models.Enrollment> Enrollment { get; set; }
    }
}
