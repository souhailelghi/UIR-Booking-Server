using DataBaseStudentUIR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DataBaseStudentUIR.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = Guid.Parse("f76cb5d7-22a5-4a8f-8c05-3dc1f57faffa"),
                    FirstName = "Souhail",
                    LastName = "Alaoui",
                    CIN = "GSI451479",
                    CodeUIR = "UIR5547",
                    Email = "souhail@jobintech-uir.ma",
                    Password = "123456",
                    Phone = "+212645678901",
                    Address = "321 Maple St, Kentira",
                    BirthDate = DateTime.Parse("1995-07-12T09:20:00"),
                    Gender = "Male"
                },
                new Student
                {
                    Id = Guid.Parse("f76cb5d7-22a5-5a8f-8c05-3dc1f57faffa"),
                    FirstName = "Adam",
                    LastName = "Alaoui",
                    CIN = "GSI451479",
                    CodeUIR = "UIR56477",
                    Email = "adam@jobintech-uir.ma",
                    Password = "123456",
                    Phone = "+212645678901",
                    Address = "321 Maple St, Kentira",
                    BirthDate = DateTime.Parse("1995-07-12T09:20:00"),
                    Gender = "Male"
                },
                new Student
                {
                    Id = Guid.Parse("f76cb5d7-22a5-4a8f-8c05-3dc1f57faffb"),
                    FirstName = "Sara",
                    LastName = "Brahimi",
                    CIN = "GHI456789",
                    CodeUIR = "UIR005",
                    Email = "sara.brahimi@jobintech-uir.ma",
                    Password = "password123",
                    Phone = "+212645678901",
                    Address = "321 Maple St, Agadir",
                    BirthDate = DateTime.Parse("1995-07-12T09:20:00"),
                    Gender = "Female"
                },
                new Student
                {
                    Id = Guid.Parse("f76cb5d7-82a5-4a8f-8c05-3dc1f57faffe"),
                    FirstName = "Ayoub",
                    LastName = "Grami",
                    CIN = "GSI451479",
                    CodeUIR = "UIR57412",
                    Email = "ayoub@jobintech-uir.ma",
                    Password = "123456",
                    Phone = "+212645678901",
                    Address = "321 Maple St, Kentira",
                    BirthDate = DateTime.Parse("1995-07-12T09:20:00"),
                    Gender = "Male"
                }
            );
        }
    }
}
