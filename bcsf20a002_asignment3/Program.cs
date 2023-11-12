
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace bcsf20a002_asignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

           
            using (var context = new MyContext())
            {
                // Create and save a new Students
                Console.WriteLine("Adding new students");

                var student = new Student
                {
                    FirstMidName = "Atyia",
                    LastName = "Alam",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())

                };

                context.Students.Add(student);

                var student1 = new Student
                {
                    FirstMidName = "Ali",
                    LastName = "Ahmed",
                    EnrollmentDate = DateTime.Parse(DateTime.Today.ToString())
                };

                context.Students.Add(student1);
                context.SaveChanges();

                //deleting a student

                Console.WriteLine("deleting");
                var stdelete = context.Students.FirstOrDefault(s => s.ID == 1);
                if (stdelete != null)
                {
                    context.Students.Remove(stdelete);
                    context.SaveChanges();
                }

                //updating a student

                Console.WriteLine("updating");
                var stupdate = context.Students.FirstOrDefault(s => s.ID == 4);
                if (stupdate != null)
                {
                    Console.WriteLine(" after updating");

                    stupdate.City = "Lahore";
                    stupdate.Email = "myemail@email.com";

                    context.SaveChanges();
                }


                // Display all Students from the database
                var students = (from s in context.Students
                                orderby s.FirstMidName
                                select s).ToList<Student>();

                Console.WriteLine("Retrieve all Students from the database:");

                foreach (var stdnt in students)
                {
                    string name = stdnt.FirstMidName + " " + stdnt.LastName + " " + stdnt.City + " " + stdnt.Email;
                    Console.WriteLine("ID: {0}, Name: {1}", stdnt.ID, name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        public enum Grade
        {
            A, B, C, D, F
        }
        public class Enrollment
        {
            public int EnrollmentID { get; set; }
            public int CourseID { get; set; }
            public int StudentID { get; set; }
            public Grade? Grade { get; set; }

            public virtual Course? Course { get; set; }
            public virtual Student? Student { get; set; }
        }

        public class Student
        {
            public int ID { get; set; }
            public string? LastName { get; set; }
            public string? FirstMidName { get; set; }
            public DateTime EnrollmentDate { get; set; }

            public string? Email { get; set; }
            public string? City { get; set; }



            public virtual ICollection<Enrollment>? Enrollments { get; set; }
        }

        public class Course
        {
            public int CourseID { get; set; }
            public string? Title { get; set; }
            public int Credits { get; set; }

            public virtual ICollection<Enrollment>? Enrollments { get; set; }
        }

        public class MyContext : DbContext
        {
            public virtual DbSet<Course> Courses { get; set; }
            public virtual DbSet<Enrollment> Enrollments { get; set; }
            public virtual DbSet<Student> Students { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystudentdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                }
            }
        }

    }
}