using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace timesheet.model.Enities
{
    public partial class TimeSheetContext : DbContext
    {
        public TimeSheetContext()
        {
        }

        public TimeSheetContext(DbContextOptions<TimeSheetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeTasks> EmployeeTasks { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=TimeSheet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTasks>(entity =>
            {
                entity.Property(e => e.DayId).HasMaxLength(10);

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeTasks_EmployeeTasks");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.EmployeeTasks)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_EmployeeTasks_Tasks");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
