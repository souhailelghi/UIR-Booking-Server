using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SportCategory> SportCategory { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Planning> Plannings { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SystemeInfo> SystemeInfos { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method

            // Configure User inheritance
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Administrator>("Administrator")
                .HasValue<Student>("Student");

            // Configure Administrator-specific properties
            modelBuilder.Entity<Administrator>()
                .Property(a => a.AdminName)
                .IsRequired();

            // Configure Student-specific properties
            modelBuilder.Entity<Student>()
                .Property(s => s.StudentName)
                .IsRequired();

            // Relationship between BlackList and Reservation (many-to-one)
            modelBuilder.Entity<BlackList>()
                .HasOne<Reservation>()
                .WithMany()
                .HasForeignKey(b => b.ReservationId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure BlackList entries are deleted when a Reservation is deleted

            // Relationship between Reservation and Sport (many-to-one)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Sport)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship between Reservation and Student (many-to-one)
            modelBuilder.Entity<Reservation>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship between Planning and Sport (many-to-one)
            modelBuilder.Entity<Planning>()
                .HasOne<Sport>()
                .WithMany()
                .HasForeignKey(p => p.SportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship between Planning and TimeRange (one-to-many)
            modelBuilder.Entity<Planning>()
                .HasMany(p => p.TimeRanges)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship between Sport and SportCategory (many-to-one)
            modelBuilder.Entity<Sport>()
                .HasOne<SportCategory>()
                .WithMany()
                .HasForeignKey(s => s.CategorieId)
                .OnDelete(DeleteBehavior.Cascade);

            // SystemeInfo properties constraints
            modelBuilder.Entity<SystemeInfo>()
                .Property(si => si.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SystemeInfo>()
                .Property(si => si.ShortName)
                .IsRequired()
                .HasMaxLength(10);

            modelBuilder.Entity<SystemeInfo>()
                .Property(si => si.Home)
                .IsRequired();

            modelBuilder.Entity<SystemeInfo>()
                .Property(si => si.AboutUs)
                .IsRequired();

            // Sport entity constraints
            modelBuilder.Entity<Sport>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Sport>()
                .Property(s => s.ReferenceSport)
                .IsRequired();

            modelBuilder.Entity<Sport>()
                .Property(s => s.NbPlayer)
                .IsRequired();

            // TimeRange constraints
            modelBuilder.Entity<TimeRange>()
                .Property(t => t.HourStart)
                .IsRequired();

            modelBuilder.Entity<TimeRange>()
                .Property(t => t.HourEnd)
                .IsRequired();
        }
    }
}
