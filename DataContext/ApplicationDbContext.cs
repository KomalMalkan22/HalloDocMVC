using System;
using System.Collections.Generic;
using HalloDoc.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRegion> AdminRegions { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<BlockRequest> BlockRequests { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<BusinessType> BusinessTypes { get; set; }

    public virtual DbSet<CaseTag> CaseTags { get; set; }

    public virtual DbSet<Concierge> Concierges { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<HealthProfessional> HealthProfessionals { get; set; }

    public virtual DbSet<HealthProfessionalType> HealthProfessionalTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Physician> Physicians { get; set; }

    public virtual DbSet<PhysicianLocation> PhysicianLocations { get; set; }

    public virtual DbSet<PhysicianNotification> PhysicianNotifications { get; set; }

    public virtual DbSet<PhysicianRegion> PhysicianRegions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestBusiness> RequestBusinesses { get; set; }

    public virtual DbSet<RequestClient> RequestClients { get; set; }

    public virtual DbSet<RequestClosed> RequestCloseds { get; set; }

    public virtual DbSet<RequestConcierge> RequestConcierges { get; set; }

    public virtual DbSet<RequestNote> RequestNotes { get; set; }

    public virtual DbSet<RequestStatusLog> RequestStatusLogs { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<RequestWiseFile> RequestWiseFiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftDetail> ShiftDetails { get; set; }

    public virtual DbSet<ShiftDetailRegion> ShiftDetailRegions { get; set; }

    public virtual DbSet<Smslog> Smslogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID = postgres;Password=komal123;Server=localhost;Port=5432;Database=HalloDoc;Integrated Security=true;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("Admin_pkey");

            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).ValueGeneratedNever();
            entity.Property(e => e.Address1).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.AltPhone).HasMaxLength(20);
            entity.Property(e => e.AspNetUserId).HasMaxLength(128);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.Zip).HasMaxLength(10);

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.AdminAspNetUsers)
                .HasForeignKey(d => d.AspNetUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AdminCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Admin_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AdminModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("Admin_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("RegionId");

            entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Admin_RoleId_fkey");
        });

        modelBuilder.Entity<AdminRegion>(entity =>
        {
            entity.HasKey(e => e.AdminRegionId).HasName("AdminRegion_pkey");

            entity.ToTable("AdminRegion");

            entity.Property(e => e.AdminRegionId).ValueGeneratedNever();

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminRegions)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AdminId");

            entity.HasOne(d => d.Region).WithMany(p => p.AdminRegions)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RegionId");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AspNetRoles_pkey");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AspNetUsers_pkey");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.CorePasswordHash)
                .HasColumnType("character varying")
                .HasColumnName("CorePasswordHash ");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailConfirmed)
                .HasColumnType("bit(1)")
                .HasColumnName("EmailConfirmed ");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("character varying")
                .HasColumnName("PasswordHash ");
            entity.Property(e => e.PhoneNumber).HasColumnType("character varying");
            entity.Property(e => e.TwoFactorEnabled)
                .HasColumnType("bit(1)")
                .HasColumnName("TwoFactorEnabled ");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("AspNetUserRoles_pkey");

            entity.Property(e => e.UserId).HasMaxLength(128);
            entity.Property(e => e.RoleId)
                .HasMaxLength(128)
                .HasColumnName("RoleId ");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUsers");
        });

        modelBuilder.Entity<BlockRequest>(entity =>
        {
            entity.HasKey(e => e.BlockRequestId).HasName("BlockRequests_pkey");

            entity.Property(e => e.BlockRequestId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP");
            entity.Property(e => e.IsActive).HasColumnType("bit(1)");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.Request).WithMany(p => p.BlockRequests)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestId");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("Business_pkey");

            entity.ToTable("Business");

            entity.Property(e => e.BusinessId).ValueGeneratedNever();
            entity.Property(e => e.Address1).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.FaxNumber).HasMaxLength(20);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.IsRegistered).HasColumnType("bit(1)");
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ZipCode).HasMaxLength(10);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.BusinessCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Business_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.BusinessModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("Business_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("RegionId");
        });

        modelBuilder.Entity<BusinessType>(entity =>
        {
            entity.HasKey(e => e.BusinessTypeId).HasName("BusinessType_pkey");

            entity.ToTable("BusinessType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CaseTag>(entity =>
        {
            entity.HasKey(e => e.CaseTagId).HasName("CaseTag_pkey");

            entity.ToTable("CaseTag");

            entity.Property(e => e.CaseTagId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(56)
                .HasColumnName("Name ");
        });

        modelBuilder.Entity<Concierge>(entity =>
        {
            entity.HasKey(e => e.ConciergeId).HasName("Concierge_pkey");

            entity.ToTable("Concierge");

            entity.Property(e => e.ConciergeId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ConciergeName).HasMaxLength(100);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);

            entity.HasOne(d => d.Region).WithMany(p => p.Concierges)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RegionId");
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.EmailLogId).HasName("EmailLog_pkey");

            entity.ToTable("EmailLog");

            entity.Property(e => e.EmailLogId)
                .HasPrecision(10)
                .HasColumnName("EmailLogID");
            entity.Property(e => e.ConfirmationNumber).HasMaxLength(256);
            entity.Property(e => e.EmailId)
                .HasMaxLength(256)
                .HasColumnName("EmailID");
            entity.Property(e => e.EmailTemplate).HasColumnType("character varying");
            entity.Property(e => e.FilePath).HasColumnType("character varying");
            entity.Property(e => e.IsEmailSent).HasColumnType("bit(1)");
            entity.Property(e => e.RoleId).HasColumnName("RoleId ");
            entity.Property(e => e.SubjectName).HasMaxLength(256);

            entity.HasOne(d => d.Admin).WithMany(p => p.EmailLogs)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("EmailLog_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.EmailLogs)
                .HasForeignKey(d => d.PhysicianId)
                .HasConstraintName("EmailLog_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.EmailLogs)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("EmailLog_RequestId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.EmailLogs)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("EmailLog_RoleId _fkey");
        });

        modelBuilder.Entity<HealthProfessional>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("HealthProfessionals_pkey");

            entity.Property(e => e.VendorId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(156);
            entity.Property(e => e.BusinessContact).HasMaxLength(128);
            entity.Property(e => e.City).HasMaxLength(156);
            entity.Property(e => e.Email).HasMaxLength(56);
            entity.Property(e => e.FaxNumber).HasMaxLength(56);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("IsDeleted ");
            entity.Property(e => e.PhoneNumber).HasMaxLength(128);
            entity.Property(e => e.State).HasMaxLength(56);
            entity.Property(e => e.VendorName).HasMaxLength(128);
            entity.Property(e => e.Zip).HasMaxLength(56);

            entity.HasOne(d => d.ProfessionNavigation).WithMany(p => p.HealthProfessionals)
                .HasForeignKey(d => d.Profession)
                .HasConstraintName("HealthProfessionals_Profession_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.HealthProfessionals)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("HealthProfessionals_RegionId_fkey");
        });

        modelBuilder.Entity<HealthProfessionalType>(entity =>
        {
            entity.HasKey(e => e.HealthProfessionalId).HasName("HealthProfessionalType_pkey");

            entity.ToTable("HealthProfessionalType");

            entity.Property(e => e.HealthProfessionalId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate ");
            entity.Property(e => e.IsActive).HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("IsDeleted ");
            entity.Property(e => e.ProfessionName).HasMaxLength(56);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("Menu_pkey");

            entity.ToTable("Menu");

            entity.Property(e => e.MenuId)
                .ValueGeneratedNever()
                .HasColumnName("MenuId ");
            entity.Property(e => e.Name)
                .HasMaxLength(56)
                .HasColumnName("Name ");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderDetails_pkey");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id ");
            entity.Property(e => e.BusinessContact).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FaxNumber).HasMaxLength(50);
            entity.Property(e => e.Prescription)
                .HasMaxLength(512)
                .HasColumnName("Prescription ");

            entity.HasOne(d => d.Request).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("OrderDetails_RequestId_fkey");

            entity.HasOne(d => d.Vendor).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("OrderDetails_VendorId_fkey");
        });

        modelBuilder.Entity<Physician>(entity =>
        {
            entity.HasKey(e => e.PhysicianId).HasName("Physician_pkey");

            entity.ToTable("Physician");

            entity.Property(e => e.PhysicianId).ValueGeneratedNever();
            entity.Property(e => e.Address1).HasMaxLength(500);
            entity.Property(e => e.Address2).HasMaxLength(500);
            entity.Property(e => e.AdminNotes).HasMaxLength(500);
            entity.Property(e => e.AltPhone).HasMaxLength(20);
            entity.Property(e => e.AspNetUserId).HasMaxLength(128);
            entity.Property(e => e.BusinessName).HasMaxLength(100);
            entity.Property(e => e.BusinessWebsite).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.IsAgreementDoc).HasColumnType("bit(1)");
            entity.Property(e => e.IsBackgroundDoc).HasColumnType("bit(1)");
            entity.Property(e => e.IsCredentialDoc).HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.IsLicenseDoc).HasColumnType("bit(1)");
            entity.Property(e => e.IsNonDisclosureDoc).HasColumnType("bit(1)");
            entity.Property(e => e.IsTokenGenerate).HasColumnType("bit(1)");
            entity.Property(e => e.IsTrainingDoc).HasColumnType("bit(1)");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MedicalLicense).HasMaxLength(500);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.Npinumber)
                .HasMaxLength(500)
                .HasColumnName("NPINumber");
            entity.Property(e => e.Photo).HasMaxLength(100);
            entity.Property(e => e.Signature).HasMaxLength(100);
            entity.Property(e => e.SyncEmailAddress).HasMaxLength(50);
            entity.Property(e => e.Zip).HasMaxLength(10);

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.PhysicianAspNetUsers)
                .HasForeignKey(d => d.AspNetUserId)
                .HasConstraintName("AspNetUserId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PhysicianCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Physician_CreatedBy_fkey");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PhysicianModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("Physician_ModifiedBy_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Physicians)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("RegionId");

            entity.HasOne(d => d.Role).WithMany(p => p.Physicians)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Physician_RoleId_fkey");
        });

        modelBuilder.Entity<PhysicianLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PhysicianLocation_pkey");

            entity.ToTable("PhysicianLocation");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("LocationId ");
            entity.Property(e => e.Address).HasMaxLength(556);
            entity.Property(e => e.Latitude)
                .HasPrecision(10)
                .HasColumnName("Latitude ");
            entity.Property(e => e.Longitude).HasPrecision(10);
            entity.Property(e => e.PhysicianName).HasMaxLength(56);

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianLocations)
                .HasForeignKey(d => d.PhysicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianLocation_PhysicianId_fkey");
        });

        modelBuilder.Entity<PhysicianNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PhysicianNotification_pkey");

            entity.ToTable("PhysicianNotification");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsNotificationStopped).HasColumnType("bit(1)");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianNotifications)
                .HasForeignKey(d => d.PhysicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianNotification_PhysicianId_fkey");
        });

        modelBuilder.Entity<PhysicianRegion>(entity =>
        {
            entity.HasKey(e => e.PhysicianRegionId).HasName("PhysicianRegion_pkey");

            entity.ToTable("PhysicianRegion");

            entity.Property(e => e.PhysicianRegionId)
                .ValueGeneratedNever()
                .HasColumnName("PhysicianRegionId ");
            entity.Property(e => e.RegionId).HasColumnName("RegionId ");

            entity.HasOne(d => d.Physician).WithMany(p => p.PhysicianRegions)
                .HasForeignKey(d => d.PhysicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_PhysicianId_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.PhysicianRegions)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PhysicianRegion_RegionId _fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("Region_pkey");

            entity.ToTable("Region");

            entity.Property(e => e.RegionId).ValueGeneratedNever();
            entity.Property(e => e.Abbreviation).HasMaxLength(56);
            entity.Property(e => e.Name).HasMaxLength(56);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("Request_pkey");

            entity.ToTable("Request");

            entity.Property(e => e.RequestId).ValueGeneratedNever();
            entity.Property(e => e.CaseNumber)
                .HasMaxLength(56)
                .HasColumnName("CaseNumber ");
            entity.Property(e => e.CaseTag).HasMaxLength(56);
            entity.Property(e => e.CaseTagPhysician).HasMaxLength(56);
            entity.Property(e => e.CompletedByPhysician).HasColumnType("bit(1)");
            entity.Property(e => e.ConfirmationNumber).HasMaxLength(28);
            entity.Property(e => e.CreatedUserId).HasColumnName("CreatedUserId ");
            entity.Property(e => e.DeclinedBy).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(56);
            entity.Property(e => e.FirstName).HasMaxLength(128);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("IsDeleted ");
            entity.Property(e => e.IsMobile).HasColumnType("bit(1)");
            entity.Property(e => e.IsUrgentEmailSent).HasColumnType("bit(1)");
            entity.Property(e => e.LastName).HasMaxLength(128);
            entity.Property(e => e.LastReservationDate).HasColumnName("LastReservationDate ");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate ");
            entity.Property(e => e.PatientAccountId).HasMaxLength(128);
            entity.Property(e => e.PhoneNumber).HasMaxLength(28);
            entity.Property(e => e.PhysicianId).HasColumnName("PhysicianId ");
            entity.Property(e => e.RelationName)
                .HasMaxLength(128)
                .HasColumnName("RelationName ");
            entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeId ");

            entity.HasOne(d => d.Physician).WithMany(p => p.Requests)
                .HasForeignKey(d => d.PhysicianId)
                .HasConstraintName("Request_PhysicianId _fkey");

            entity.HasOne(d => d.RequestType).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Request_RequestTypeId _fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Request_UserId_fkey");
        });

        modelBuilder.Entity<RequestBusiness>(entity =>
        {
            entity.HasKey(e => e.RequestBusinessId).HasName("RequestBusiness_pkey");

            entity.ToTable("RequestBusiness");

            entity.Property(e => e.RequestBusinessId).ValueGeneratedNever();
            entity.Property(e => e.BusinessId).HasColumnName("BusinessId ");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");

            entity.HasOne(d => d.Business).WithMany(p => p.RequestBusinesses)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_BusinessId _fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestBusinesses)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestBusiness_RequestId_fkey");
        });

        modelBuilder.Entity<RequestClient>(entity =>
        {
            entity.HasKey(e => e.RequestClientId).HasName("RequestClient_pkey");

            entity.ToTable("RequestClient");

            entity.Property(e => e.RequestClientId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(528);
            entity.Property(e => e.City)
                .HasMaxLength(128)
                .HasColumnName("City ");
            entity.Property(e => e.Email).HasMaxLength(56);
            entity.Property(e => e.FirstName).HasMaxLength(128);
            entity.Property(e => e.IntDate).HasColumnName("intDate");
            entity.Property(e => e.IntYear).HasColumnName("intYear");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsMobile).HasColumnType("bit(1)");
            entity.Property(e => e.IsSetFollowupSent).HasColumnName("IsSetFollowupSent ");
            entity.Property(e => e.LastName).HasMaxLength(128);
            entity.Property(e => e.Location).HasMaxLength(128);
            entity.Property(e => e.Notes).HasMaxLength(528);
            entity.Property(e => e.NotiEmail)
                .HasMaxLength(56)
                .HasColumnName("NotiEmail ");
            entity.Property(e => e.NotiMobile)
                .HasMaxLength(20)
                .HasColumnName("NotiMobile ");
            entity.Property(e => e.PhoneNumber).HasMaxLength(28);
            entity.Property(e => e.RegionId).HasColumnName("RegionId ");
            entity.Property(e => e.RemindHouseCallCount).HasColumnName("RemindHouseCallCount ");
            entity.Property(e => e.State).HasMaxLength(128);
            entity.Property(e => e.StrMonth)
                .HasMaxLength(20)
                .HasColumnName("strMonth");
            entity.Property(e => e.Street).HasMaxLength(128);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .HasColumnName("ZipCode ");

            entity.HasOne(d => d.Region).WithMany(p => p.RequestClients)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("RequestClient_RegionId _fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestClients)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Request");
        });

        modelBuilder.Entity<RequestClosed>(entity =>
        {
            entity.HasKey(e => e.RequestClosedId).HasName("RequestClosed_pkey");

            entity.ToTable("RequestClosed");

            entity.Property(e => e.RequestClosedId).ValueGeneratedNever();
            entity.Property(e => e.Ip).HasColumnName("IP");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestCloseds)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClosed_RequestId_fkey");

            entity.HasOne(d => d.RequestStatusLog).WithMany(p => p.RequestCloseds)
                .HasForeignKey(d => d.RequestStatusLogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestClosed_RequestStatusLogId_fkey");
        });

        modelBuilder.Entity<RequestConcierge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RequestConcierge_pkey");

            entity.ToTable("RequestConcierge");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("Id ");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");

            entity.HasOne(d => d.Concierge).WithMany(p => p.RequestConcierges)
                .HasForeignKey(d => d.ConciergeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_ConciergeId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestConcierges)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestConcierge_RequestId_fkey");
        });

        modelBuilder.Entity<RequestNote>(entity =>
        {
            entity.HasKey(e => e.RequestNotesId).HasName("RequestNotes_pkey");

            entity.Property(e => e.RequestNotesId)
                .ValueGeneratedNever()
                .HasColumnName("RequestNotesId ");
            entity.Property(e => e.AdministrativeNotes).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.IntDate).HasColumnName("intDate");
            entity.Property(e => e.IntYear).HasColumnName("intYear");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate ");
            entity.Property(e => e.PhysicianNotes).HasColumnName("PhysicianNotes ");
            entity.Property(e => e.StrMonth)
                .HasMaxLength(20)
                .HasColumnName("strMonth ");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestNotes)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Request");
        });

        modelBuilder.Entity<RequestStatusLog>(entity =>
        {
            entity.HasKey(e => e.RequestStatusLogId).HasName("RequestStatusLog_pkey");

            entity.ToTable("RequestStatusLog");

            entity.Property(e => e.RequestStatusLogId).ValueGeneratedNever();
            entity.Property(e => e.AdminId).HasColumnName("AdminId ");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.TransToAdmin).HasColumnType("bit(1)");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestStatusLogs)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("RequestStatusLog_AdminId _fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestStatusLogPhysicians)
                .HasForeignKey(d => d.PhysicianId)
                .HasConstraintName("RequestStatusLog_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestStatusLogs)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestStatusLog_RequestId_fkey");

            entity.HasOne(d => d.TransToPhysician).WithMany(p => p.RequestStatusLogTransToPhysicians)
                .HasForeignKey(d => d.TransToPhysicianId)
                .HasConstraintName("RequestStatusLog_TransToPhysicianId_fkey");
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity.HasKey(e => e.RequestTypeId).HasName("RequestType_pkey");

            entity.ToTable("RequestType");

            entity.Property(e => e.RequestTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(56)
                .HasColumnName("Name ");
        });

        modelBuilder.Entity<RequestWiseFile>(entity =>
        {
            entity.HasKey(e => e.RequestWiseFileId).HasName("RequestWiseFile_pkey");

            entity.ToTable("RequestWiseFile");

            entity.Property(e => e.RequestWiseFileId)
                .ValueGeneratedNever()
                .HasColumnName("RequestWiseFileID");
            entity.Property(e => e.FileName)
                .HasMaxLength(512)
                .HasColumnName("FileName ");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsCompensation).HasColumnType("bit(1)");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.IsFinalize)
                .HasColumnType("bit(1)")
                .HasColumnName("IsFinalize ");
            entity.Property(e => e.IsFrontSide).HasColumnType("bit(1)");
            entity.Property(e => e.IsPatientRecords)
                .HasColumnType("bit(1)")
                .HasColumnName("IsPatientRecords ");
            entity.Property(e => e.PhysicianId).HasColumnName("PhysicianId ");

            entity.HasOne(d => d.Admin).WithMany(p => p.RequestWiseFiles)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("RequestWiseFile_AdminId_fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.RequestWiseFiles)
                .HasForeignKey(d => d.PhysicianId)
                .HasConstraintName("RequestWiseFile_PhysicianId _fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.RequestWiseFiles)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RequestWiseFile_RequestId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsDeleted)
                .HasColumnType("bit(1)")
                .HasColumnName("IsDeleted ");
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(56);
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.HasKey(e => e.RoleMenuId).HasName("RoleMenu_pkey");

            entity.ToTable("RoleMenu");

            entity.Property(e => e.RoleMenuId).ValueGeneratedNever();
            entity.Property(e => e.MenuId).HasColumnName("MenuId ");
            entity.Property(e => e.RoleId).HasColumnName("RoleId ");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Menu");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Role");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("Shift_pkey");

            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP ");
            entity.Property(e => e.IsRepeat).HasColumnType("bit(1)");
            entity.Property(e => e.WeekDays).HasMaxLength(7);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AspNetUsers");

            entity.HasOne(d => d.Physician).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.PhysicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Physician");
        });

        modelBuilder.Entity<ShiftDetail>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailId).HasName("ShiftDetail_pkey");

            entity.ToTable("ShiftDetail");

            entity.Property(e => e.ShiftDetailId).ValueGeneratedNever();
            entity.Property(e => e.EndTime).HasColumnType("time with time zone");
            entity.Property(e => e.EventId)
                .HasMaxLength(100)
                .HasColumnName("EventId ");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.IsSync).HasColumnType("bit(1)");
            entity.Property(e => e.ModifiedBy).HasMaxLength(128);
            entity.Property(e => e.RegionId).HasColumnName("RegionId ");
            entity.Property(e => e.StartTime).HasColumnType("time with time zone");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ShiftDetails)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("AspNetUsers");

            entity.HasOne(d => d.Region).WithMany(p => p.ShiftDetails)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("ShiftDetail_RegionId _fkey");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftDetails)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Shift");
        });

        modelBuilder.Entity<ShiftDetailRegion>(entity =>
        {
            entity.HasKey(e => e.ShiftDetailRegionId).HasName("ShiftDetailRegion_pkey");

            entity.ToTable("ShiftDetailRegion");

            entity.Property(e => e.ShiftDetailRegionId).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.ShiftDetailId).HasColumnName("ShiftDetailId ");

            entity.HasOne(d => d.Region).WithMany(p => p.ShiftDetailRegions)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Region");

            entity.HasOne(d => d.ShiftDetail).WithMany(p => p.ShiftDetailRegions)
                .HasForeignKey(d => d.ShiftDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ShiftDetail");
        });

        modelBuilder.Entity<Smslog>(entity =>
        {
            entity.HasKey(e => e.SmslogId).HasName("SMSLog_pkey");

            entity.ToTable("SMSLog");

            entity.Property(e => e.SmslogId)
                .HasPrecision(10)
                .HasColumnName("SMSLogID");
            entity.Property(e => e.AdminId).HasColumnName("AdminId ");
            entity.Property(e => e.ConfirmationNumber).HasMaxLength(56);
            entity.Property(e => e.CreateDate).HasColumnName("CreateDate ");
            entity.Property(e => e.IsSmssent)
                .HasColumnType("bit(1)")
                .HasColumnName("IsSMSSent");
            entity.Property(e => e.MobileNumber).HasMaxLength(56);
            entity.Property(e => e.Smstemplate)
                .HasColumnType("character varying")
                .HasColumnName("SMSTemplate ");

            entity.HasOne(d => d.Admin).WithMany(p => p.Smslogs)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("SMSLog_AdminId _fkey");

            entity.HasOne(d => d.Physician).WithMany(p => p.Smslogs)
                .HasForeignKey(d => d.PhysicianId)
                .HasConstraintName("SMSLog_PhysicianId_fkey");

            entity.HasOne(d => d.Request).WithMany(p => p.Smslogs)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("SMSLog_RequestId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Smslogs)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("SMSLog_RoleId_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.AspNetUserId)
                .HasMaxLength(128)
                .HasColumnName("AspNetUserId ");
            entity.Property(e => e.City).HasMaxLength(128);
            entity.Property(e => e.CreatedBy).HasMaxLength(128);
            entity.Property(e => e.Email).HasMaxLength(56);
            entity.Property(e => e.FirstName).HasMaxLength(128);
            entity.Property(e => e.IntDate).HasColumnName("intDate");
            entity.Property(e => e.IntYear).HasColumnName("intYear");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .HasColumnName("IP");
            entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");
            entity.Property(e => e.IsMobile).HasColumnType("bit(1)");
            entity.Property(e => e.IsRequestWithEmail).HasColumnType("bit(1)");
            entity.Property(e => e.LastName).HasMaxLength(128);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(128)
                .HasColumnName("ModifiedBy ");
            entity.Property(e => e.ModifiedDate).HasColumnName("ModifiedDate ");
            entity.Property(e => e.State).HasMaxLength(128);
            entity.Property(e => e.StrMonth)
                .HasMaxLength(20)
                .HasColumnName("strMonth");
            entity.Property(e => e.Street).HasMaxLength(128);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("ZipCode ");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.Users)
                .HasForeignKey(d => d.AspNetUserId)
                .HasConstraintName("AspNetUsers");

            entity.HasOne(d => d.Region).WithMany(p => p.Users)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("User_RegionId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
