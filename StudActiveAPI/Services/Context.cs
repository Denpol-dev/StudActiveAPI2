using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudActiveAPI.Models;

#nullable disable

namespace StudActiveAPI.Services
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<StudentStudActive> StudentStudActives { get; set; }
        public virtual DbSet<StudentCouncil> StudentCouncils { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<RoleStudActive> RoleStudActives { get; set; }
        public virtual DbSet<DutyList> DutyLists { get; set; }
        public virtual DbSet<HigherSchool> HigherSchools { get; set; }
        public virtual DbSet<DutyListCalendar> DutyListCalendars { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionsUser> PermissionsUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            var config = builder.Build();
            //optionsBuilder.UseSqlServer("Server=DENPOL,1433;Database=StudActiveDB;User Id=sa;Password=denpol.11.22.63;Trust Server Certificate=true;");//база домашняя
            optionsBuilder.UseSqlServer("Server=217.28.223.127,17160;User Id=user_d3945;Password=9y$GC6d*sW?4;Database=db_556ff;Trust Server Certificate=true;");
            //optionsBuilder.UseSqlServer(config.GetConnectionString("Prod"));//база серверная
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Students_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserName).IsRequired();
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}