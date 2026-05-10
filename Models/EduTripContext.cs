using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EduTrip.Models;

public partial class EduTripContext : DbContext
{
    public EduTripContext()
    {
    }

    public EduTripContext(DbContextOptions<EduTripContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<MentorAvailability> MentorAvailabilities { get; set; }

    public virtual DbSet<MentorEarning> MentorEarnings { get; set; }

    public virtual DbSet<MentorProfile> MentorProfiles { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageEnrollment> PackageEnrollments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Payout> Payouts { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceOrder> ServiceOrders { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EduTrip;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC07268730CB");

            entity.HasIndex(e => e.OrderId, "UQ__Bookings__C3905BCE15DF9555").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SessionLink).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Confirmed");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Availability).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AvailabilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MentorAvailability");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MentorProfiles");

            entity.HasOne(d => d.Order).WithOne(p => p.Booking)
                .HasForeignKey<Booking>(d => d.OrderId)
                .HasConstraintName("FK_Bookings_Orders");

            entity.HasOne(d => d.Student).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_StudentUsers");
        });

        modelBuilder.Entity<MentorAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MentorAv__3214EC072DB0C437");

            entity.ToTable("MentorAvailability");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DayOfWeek).HasMaxLength(10);
            entity.Property(e => e.IsRecurring).HasDefaultValue(true);

            entity.HasOne(d => d.Mentor).WithMany(p => p.MentorAvailabilities)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK_MentorAvailability_MentorProfiles");
        });

        modelBuilder.Entity<MentorEarning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MentorEa__3214EC078E938174");

            entity.HasIndex(e => e.OrderId, "UQ__MentorEa__C3905BCE0D568466").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CommissionRate)
                .HasDefaultValue(70.00m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MentorShare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PlatformShare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mentor).WithMany(p => p.MentorEarnings)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MentorEarnings_MentorProfiles");

            entity.HasOne(d => d.Order).WithOne(p => p.MentorEarning)
                .HasForeignKey<MentorEarning>(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MentorEarnings_Orders");

            entity.HasOne(d => d.Payout).WithMany(p => p.MentorEarnings)
                .HasForeignKey(d => d.PayoutId)
                .HasConstraintName("FK_MentorEarnings_Payouts");
        });

        modelBuilder.Entity<MentorProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MentorPr__3214EC0786413712");

            entity.HasIndex(e => e.UserId, "UQ__MentorPr__1788CC4DC2374744").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AverageRating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Specialties).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithOne(p => p.MentorProfile)
                .HasForeignKey<MentorProfile>(d => d.UserId)
                .HasConstraintName("FK_MentorProfiles_Users");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07976C54B5");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Notifications_Users");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07DD3A633A");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(5)
                .HasDefaultValue("EGP");
            entity.Property(e => e.OrderType).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("FK_Orders_MentorProfiles");

            entity.HasOne(d => d.Package).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_Orders_Packages");

            entity.HasOne(d => d.Service).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Orders_Services");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Packages__3214EC07BCB7CBFA");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<PackageEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PackageE__3214EC07BE1B19FC");

            entity.HasIndex(e => e.OrderId, "UQ__PackageE__C3905BCE334C942F").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithOne(p => p.PackageEnrollment)
                .HasForeignKey<PackageEnrollment>(d => d.OrderId)
                .HasConstraintName("FK_PackageEnrollments_Orders");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageEnrollments)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageEnrollments_Packages");

            entity.HasOne(d => d.Student).WithMany(p => p.PackageEnrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PackageEnrollments_StudentUsers");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07161BD20F");

            entity.HasIndex(e => e.OrderId, "UQ__Payments__C3905BCE99F9616D").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ExternalId).HasMaxLength(200);
            entity.Property(e => e.Provider).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Initiated");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.OrderId)
                .HasConstraintName("FK_Payments_Orders");
        });

        modelBuilder.Entity<Payout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payouts__3214EC073AEA5EE0");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Currency)
                .HasMaxLength(5)
                .HasDefaultValue("EGP");
            entity.Property(e => e.Method).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.InitiatedByNavigation).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.InitiatedBy)
                .HasConstraintName("FK_Payouts_InitiatedByUsers");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Payouts)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payouts_MentorProfiles");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Services__3214EC072A935A90");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ServiceOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ServiceO__3214EC073FE4EB5A");

            entity.HasIndex(e => e.OrderId, "UQ__ServiceO__C3905BCE34BE36F0").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DeliverableUrl).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.AssignedMentor).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.AssignedMentorId)
                .HasConstraintName("FK_ServiceOrders_AssignedMentorProfiles");

            entity.HasOne(d => d.Order).WithOne(p => p.ServiceOrder)
                .HasForeignKey<ServiceOrder>(d => d.OrderId)
                .HasConstraintName("FK_ServiceOrders_Orders");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceOrders_Services");

            entity.HasOne(d => d.Student).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ServiceOrders_StudentUsers");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07B08D834C");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BillingCycle).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Plan).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Subscriptions_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07CA3576CE");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105340023F999").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.AccountTier).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
