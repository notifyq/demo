using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace demo_1.Model
{
    public partial class db_demo_2024Context : DbContext
    {
        public db_demo_2024Context()
        {
        }

        public db_demo_2024Context(DbContextOptions<db_demo_2024Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DefectType> DefectTypes { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestComment> RequestComments { get; set; } = null!;
        public virtual DbSet<RequestDesignatedEmployee> RequestDesignatedEmployees { get; set; } = null!;
        public virtual DbSet<RequestEquipmentList> RequestEquipmentLists { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=1941;database=db_demo_2024", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<DefectType>(entity =>
            {
                entity.ToTable("defect_types");

                entity.Property(e => e.DefectTypeId).HasColumnName("defect_type_id");

                entity.Property(e => e.DefectName)
                    .HasMaxLength(45)
                    .HasColumnName("defect_name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.EmployeeRole, "role_fk_idx");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeLogin)
                    .HasMaxLength(45)
                    .HasColumnName("employee_login");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(45)
                    .HasColumnName("employee_name");

                entity.Property(e => e.EmployeePassword)
                    .HasMaxLength(45)
                    .HasColumnName("employee_password");

                entity.Property(e => e.EmployeePatronymic)
                    .HasMaxLength(45)
                    .HasColumnName("employee_patronymic");

                entity.Property(e => e.EmployeeRole).HasColumnName("employee_role");

                entity.Property(e => e.EmployeeSurname)
                    .HasMaxLength(45)
                    .HasColumnName("employee_surname");

                entity.HasOne(d => d.EmployeeRoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("role_fk");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("equipment");

                entity.HasIndex(e => e.DefectTypeId, "defect_fk_idx");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.DefectTypeId).HasColumnName("defect_type_id");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(45)
                    .HasColumnName("equipment_name");

                entity.Property(e => e.EquipmentSerialNumber)
                    .HasMaxLength(150)
                    .HasColumnName("equipment_serial_number");

                entity.HasOne(d => d.DefectType)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.DefectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("defect_fk");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(45)
                    .HasColumnName("client_name");

                entity.Property(e => e.ClientPatronymic)
                    .HasMaxLength(45)
                    .HasColumnName("client_patronymic");

                entity.Property(e => e.ClientSurname)
                    .HasMaxLength(45)
                    .HasColumnName("client_surname");

                entity.Property(e => e.RequestDate).HasColumnName("request_date");

                entity.Property(e => e.RequestDescription)
                    .HasMaxLength(500)
                    .HasColumnName("request_description");
            });

            modelBuilder.Entity<RequestComment>(entity =>
            {
                entity.ToTable("request_comments");

                entity.HasIndex(e => e.RequestId, "request_com_fk_idx");

                entity.Property(e => e.RequestCommentId).HasColumnName("request_comment_id");

                entity.Property(e => e.CommentContent)
                    .HasMaxLength(500)
                    .HasColumnName("comment_content");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestComments)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_com_fk");
            });

            modelBuilder.Entity<RequestDesignatedEmployee>(entity =>
            {
                entity.ToTable("request_designated_employee");

                entity.HasIndex(e => e.EmployeeId, "emp_fk_idx");

                entity.HasIndex(e => e.RequestId, "req_fk_idx");

                entity.Property(e => e.RequestDesignatedEmployeeId).HasColumnName("request_designated_employee_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RequestDesignatedEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("emp_fk");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDesignatedEmployees)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("req_fk");
            });

            modelBuilder.Entity<RequestEquipmentList>(entity =>
            {
                entity.ToTable("request_equipment_list");

                entity.HasIndex(e => e.EquipmentId, "equipment_fk_idx");

                entity.HasIndex(e => e.RequestId, "request_id_fk_idx");

                entity.Property(e => e.RequestEquipmentListId).HasColumnName("request_equipment_list_id");

                entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.RequestEquipmentLists)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("equipment_fk");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestEquipmentLists)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_id_fk");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("request_statuses");

                entity.HasIndex(e => e.RequestId, "request_fk_idx");

                entity.HasIndex(e => e.StatusId, "status_fk_idx");

                entity.Property(e => e.RequestStatusId).HasColumnName("request_status_id");

                entity.Property(e => e.ChangeDate).HasColumnName("change_date");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestStatuses)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("request_fk");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.RequestStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("status_fk");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(45)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(45)
                    .HasColumnName("status_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
