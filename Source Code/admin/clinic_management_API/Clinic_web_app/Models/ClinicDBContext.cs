using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Clinic_web_app.Models
{
    public partial class ClinicDBContext : DbContext
    {
        public ClinicDBContext()
        {
        }

        public ClinicDBContext(DbContextOptions<ClinicDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminOrder> AdminOrders { get; set; }
        public virtual DbSet<AdminOrderDetail> AdminOrderDetails { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public virtual DbSet<EcomerceEquipDetail> EcomerceEquipDetails { get; set; }
        public virtual DbSet<EcomerceMedOrderDetail> EcomerceMedOrderDetails { get; set; }
        public virtual DbSet<EcomerceOrder> EcomerceOrders { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<EquipmentForClinic> EquipmentForClinics { get; set; }
        public virtual DbSet<EquipmentForEcomerce> EquipmentForEcomerces { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<StaffAccount> StaffAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ClinicDB;uid=sa;pwd=1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AdminOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__AdminOrd__C3905BAF86CE5680");

                entity.ToTable("AdminOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AdminOrders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__AdminOrde__Accou__38996AB5");
            });

            modelBuilder.Entity<AdminOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__AdminOrd__C3905BAFB9F61B69");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EquipmentID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.Purpose)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.AdminOrderDetails)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__AdminOrde__Equip__3C69FB99");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.AdminOrderDetails)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK__AdminOrde__Order__3B75D760");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CourseID");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_date");

                entity.Property(e => e.Lectures)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_date");
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64B803C20809");

                entity.ToTable("CustomerAccount");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EcomerceEquipDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Ecomerce__C3905BAFAA6010BD");

                entity.ToTable("EcomerceEquipDetail");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EquipmentID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EcomerceEquipDetails)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__EcomerceE__Equip__59FA5E80");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.EcomerceEquipDetails)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK__EcomerceE__Order__59063A47");
            });

            modelBuilder.Entity<EcomerceMedOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Ecomerce__C3905BAFA424332A");

                entity.ToTable("EcomerceMedOrderDetail");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.MedId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MedID");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.HasOne(d => d.Med)
                    .WithMany(p => p.EcomerceMedOrderDetails)
                    .HasForeignKey(d => d.MedId)
                    .HasConstraintName("FK__EcomerceM__MedID__5629CD9C");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.EcomerceMedOrderDetails)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK__EcomerceM__Order__5535A963");
            });

            modelBuilder.Entity<EcomerceOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Ecomerce__C3905BAFE556E1BB");

                entity.ToTable("EcomerceOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.EcomerceOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__EcomerceO__Custo__4BAC3F29");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CourseID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Enrollmen__Accou__6D0D32F4");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Enrollmen__Cours__6C190EBB");
            });

            modelBuilder.Entity<EquipmentForClinic>(entity =>
            {
                entity.HasKey(e => e.EquipmentId)
                    .HasName("PK__Equipmen__3447459990F9F873");

                entity.ToTable("EquipmentForClinic");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EquipmentID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BrandID");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.DateCreate).HasColumnType("datetime").HasColumnName("DateCreate");
                entity.Property(e => e.Description).HasMaxLength(65535).IsUnicode(false);
                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.EquipmentForClinics)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Equipment__Brand__29572725");
            });

            modelBuilder.Entity<EquipmentForEcomerce>(entity =>
            {
                entity.HasKey(e => e.EquipmentId)
                    .HasName("PK__Equipmen__3447459938EC3CEC");

                entity.ToTable("EquipmentForEcomerce");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EquipmentID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BrandID");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.DateCreate).HasColumnType("datetime").HasColumnName("DateCreate");
                entity.Property(e => e.Description).HasMaxLength(65535).IsUnicode(false);
                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.EquipmentForEcomerces)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Equipment__Brand__2C3393D0");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");

                entity.Property(e => e.Content)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.Reply)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Feedback__Custom__5CD6CB2B");
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.MedId)
                    .HasName("PK__Medicine__EB77FC3660F28A5E");

                entity.ToTable("Medicine");

                entity.Property(e => e.MedId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MedID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BrandID");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.DateCreate).HasColumnType("datetime").HasColumnName("DateCreate");
                entity.Property(e => e.Description).HasMaxLength(65535).IsUnicode(false);
                entity.Property(e => e.MedName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Featured).HasColumnType("bit").HasColumnName("Featured");
                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Medicine__BrandI__52593CB8");
            });

            modelBuilder.Entity<StaffAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PK__StaffAcc__349DA5865E479769");

                entity.ToTable("StaffAccount");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}