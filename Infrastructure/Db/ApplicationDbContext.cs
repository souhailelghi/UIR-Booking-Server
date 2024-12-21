using Domain.Common;
using Domain.Entities;
using Infrastructure.ConfiguringDbTypeEntities;
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

        public DbSet<Event> Events { get; set; }
        public DbSet<SystemeInfo> SystemeInfos { get; set; }
        public DbSet<TimeRange> TimeRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method

            new ConfiguringSportEntity().Configure(modelBuilder.Entity<Sport>());
            new ConfiguringAdministratorEntity().Configure(modelBuilder.Entity<Administrator>());
            new ConfiguringBlackListEntity().Configure(modelBuilder.Entity<BlackList>());
            new ConfiguringEventEntity().Configure(modelBuilder.Entity<Event>());
            new ConfiguringPlanningEntity().Configure(modelBuilder.Entity<Planning>());
            new ConfiguringReservationEntity().Configure(modelBuilder.Entity<Reservation>());
            new ConfiguringStudentEntity().Configure(modelBuilder.Entity<Student>());
            new ConfiguringSystemeInfoEntity().Configure(modelBuilder.Entity<SystemeInfo>());
            new ConfiguringTimeRangeEntity().Configure(modelBuilder.Entity<TimeRange>());
            new ConfiguringUserEntity().Configure(modelBuilder.Entity<User>());
            //modelBuilder.Entity<Student>().HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
