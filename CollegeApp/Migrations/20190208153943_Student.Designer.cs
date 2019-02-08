﻿// <auto-generated />
using System;
using CollegeApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CollegeApp.Migrations
{
    [DbContext(typeof(CollegeAppContext))]
    [Migration("20190208153943_Student")]
    partial class Student
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CollegeApp.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CourseID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("CollegeApp.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("Grade");

                    b.Property<int>("StudentID");

                    b.Property<int>("SubjectID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("StudentID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("CollegeApp.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday")
                        .IsRequired();

                    b.Property<int>("CourseID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("StudentID");

                    b.HasIndex("CourseID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("CollegeApp.Models.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("TeacherID")
                        .IsRequired();

                    b.HasKey("SubjectID");

                    b.HasIndex("CourseID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("CollegeApp.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal?>("Salary")
                        .IsRequired()
                        .HasColumnType("money");

                    b.HasKey("TeacherID");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("CollegeApp.Models.Enrollment", b =>
                {
                    b.HasOne("CollegeApp.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CollegeApp.Models.Subject", "Subject")
                        .WithMany("Enrollments")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CollegeApp.Models.Student", b =>
                {
                    b.HasOne("CollegeApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CollegeApp.Models.Subject", b =>
                {
                    b.HasOne("CollegeApp.Models.Course")
                        .WithMany("Subjects")
                        .HasForeignKey("CourseID");

                    b.HasOne("CollegeApp.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
