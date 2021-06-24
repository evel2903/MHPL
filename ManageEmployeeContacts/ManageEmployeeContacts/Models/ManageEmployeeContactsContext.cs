using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ManageEmployeeContacts.Models
{
    public partial class ManageEmployeeContactsContext : DbContext
    {
        public ManageEmployeeContactsContext()
        {
        }

        public ManageEmployeeContactsContext(DbContextOptions<ManageEmployeeContactsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. 
                For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                */
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ManageEmployeeContacts;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("department_name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.EmployeeChangeDepartment)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_change_department");

                entity.Property(e => e.EmployeeDob)
                    .HasColumnType("date")
                    .HasColumnName("employee_dob");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_email");

                entity.Property(e => e.EmployeeFullname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_fullname");

                entity.Property(e => e.EmployeeGender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("employee_gender");

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("employee_phone");

                entity.Property(e => e.EmployeeState)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("employee_state");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
