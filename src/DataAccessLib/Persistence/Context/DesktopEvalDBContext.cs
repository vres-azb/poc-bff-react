using DataAccessLib.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using DataAccessLib.Persistence.DbModels;

namespace DataAccessLib.Persistence.Context
{
    public partial class DesktopEvalDBContext : BaseDbContext
    {
        private readonly DesktopEvalDBSettings _desktopEvalDBSettings;

        public DesktopEvalDBContext(DbContextOptions<DesktopEvalDBContext> options,
            IOptions<DesktopEvalDBSettings> desktopEvalDBSettings)
                : base(options)
        {
            _desktopEvalDBSettings = desktopEvalDBSettings.Value;
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<KeyType> KeyTypes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderAdjustedData> OrderAdjustedData { get; set; } = null!;
        public virtual DbSet<OrderSubject> OrderSubjects { get; set; } = null!;
        public virtual DbSet<OrderSubjectKeyValue> OrderSubjectKeyValues { get; set; } = null!;
        public virtual DbSet<OrderSubjectVeroValue> OrderSubjectVeroValues { get; set; } = null!;
        public virtual DbSet<OrderValuationReport> OrderValuationReports { get; set; }
        public virtual DbSet<PreOrder> PreOrders { get; set; } = null!;
        public virtual DbSet<KeyValue> KeyValues { get; set; } = null!;
        public virtual DbSet<IdpType> IdpTypes { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public virtual DbSet<UserLog> UserLogs { get; set; } = null!;
        public virtual DbSet<UserComp> UserComps { get; set; } = null!;
        public virtual DbSet<UserCompType> UserCompTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string? rootConnectionString = _desktopEvalDBSettings.ConnectionString;
                if (string.IsNullOrEmpty(rootConnectionString))
                {
                    throw new InvalidOperationException("DB ConnectionString is not configured.");
                }

                optionsBuilder.UseSqlServer(rootConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeyType>(entity =>
            {
                entity.ToTable("KeyType", "prop");

                entity.Property(e => e.KeyTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyTenantId);

                entity.ToTable("Companies", "prop");

                entity.Property(e => e.AuthPtenantId).HasColumnName("AuthPTenantId");

                entity.Property(e => e.CreateDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DataKey)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "prop");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);
                entity.Property(e => e.DataKey)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("RV");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.PreOrder)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PreOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_PreOrder");
            });

            modelBuilder.Entity<OrderAdjustedData>(entity =>
            {
                entity.HasKey(e => e.OrderAdjustedDataId);
                entity.ToTable("OrderAdjustedData", "prop");

                entity.Property(e => e.Comment)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DataKey)
                       .HasMaxLength(12)
                       .IsUnicode(false);

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.OrderSubject)
                    .WithMany(p => p.OrderAdjustedData)
                    .HasForeignKey(d => d.OrderSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderAdjustedData_OrderSubject");
            });

            modelBuilder.Entity<OrderSubject>(entity =>
            {
                entity.ToTable("OrderSubject", "prop");

                entity.HasIndex(e => new { e.OrderId, e.IsDeleted }, "UK_OrderSubject")
                        .IsUnique();

                entity.Property(e => e.Avfactor)
                    .HasColumnType("decimal(16, 8)")
                    .HasColumnName("AVFactor");

                entity.Property(e => e.AvfactorCompsJson)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("AVFactorCompsJson");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DataKey)
                .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderSubjects)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSubject_Order");

                entity.Property(e => e.IsSubjectAdjusted).HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<OrderSubjectKeyValue>(entity =>
            {
                entity.ToTable("OrderSubjectKeyValue", "prop");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.KeyValue).IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.KeyType)
                    .WithMany(p => p.OrderSubjectKeyValues)
                    .HasForeignKey(d => d.KeyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSubjectKeyValue_KeyType");

                entity.HasOne(d => d.OrderSubject)
                    .WithMany(p => p.OrderSubjectKeyValues)
                    .HasForeignKey(d => d.OrderSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSubjectKeyValue_OrderSubject");
            });

            modelBuilder.Entity<OrderSubjectVeroValue>(entity =>
            {
                entity.ToTable("OrderSubjectVeroValue", "prop");

                entity.HasIndex(e => new { e.OrderSubjectId, e.IsDeleted }, "UK_OrderSubjectVeroValue")
                        .IsUnique();

                entity.Property(e => e.AccessCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CanRetry).HasDefaultValueSql("((0))");

                entity.Property(e => e.City)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Confidence)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.FaultString)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FSD)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("FSD");
                entity.Property(e => e.ReportDate).HasColumnType("date");

                entity.Property(e => e.ReportNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResultCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.VVResponseId).HasColumnName("VVResponseId");
                entity.Property(e => e.VvstatusId).HasColumnName("VVStatusId");

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DataKey)
                       .HasMaxLength(12)
                       .IsUnicode(false);

                entity.HasOne(d => d.OrderSubject)
                    .WithMany(p => p.OrderSubjectVeroValues)
                    .HasForeignKey(d => d.OrderSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderSubjectVeroValue_OrderSubject");
            });

            modelBuilder.Entity<OrderValuationReport>(entity =>
            {
                entity.ToTable("OrderValuationReport", "prop");

                entity.HasIndex(e => new { e.OrderSubjectId }, "IX_OrderValuationReport");

                entity.HasIndex(e => new { e.ReportNo }, "UK_OrderValuationReport_ReportNo").IsUnique();

                entity.Property(e => e.CreatedBy).HasMaxLength(256);
                entity.Property(e => e.CreateDateTime)
        .HasColumnType("datetime")
        .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DataKey)
                .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ReportNo)
                .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StoragePath)
                .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.OrderSubject)
                    .WithMany(p => p.OrderValuationReport)
                    .HasForeignKey(d => d.OrderSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderValuationReport_OrderSubject");
            });

            modelBuilder.Entity<PreOrder>(entity =>
            {
                entity.ToTable("PreOrder", "prop");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DataKey)
                .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.InputAddress)
                .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.Property(e => e.UserId)
                        .IsRequired()
                        .HasMaxLength(250);
            });

            modelBuilder.Entity<IdpType>(entity =>
            {
                entity.ToTable("IdpType", "dbo");

                entity.HasIndex(e => new { e.Name, e.IsDeleted }, "IX_IdpType_Name")
                    .IsUnique();

                entity.Property(e => e.IdpTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.IsDeleted });

                entity.ToTable("UserProfile", "dbo");

                entity.Property(e => e.UserId).HasMaxLength(256);

                entity.Property(e => e.IdpEmail).HasMaxLength(256);

                entity.Property(e => e.IdpRoles)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdpUserId)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AdditionalInfo).IsUnicode(false);

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.IdpType)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.IdpTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_IdpType");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserLog");

                entity.ToTable("UserLog", "dbo");

                entity.Property(e => e.UserId).HasMaxLength(256);

                entity.Property(e => e.LastLoginDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });


            modelBuilder.Entity<KeyValue>(entity =>
            {
                entity.ToTable("KeyValue", "prop");

                entity.Property(e => e.KeyValueId).HasColumnName("KeyValueID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.Key)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdateUtc).HasColumnType("datetime");

                entity.Property(e => e.RefId).HasColumnName("RefID");

                entity.Property(e => e.Value).IsUnicode(false);
            });

            modelBuilder.Entity<UserComp>(entity =>
            {
                entity.ToTable("UserComp", "prop");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);

                entity.Property(e => e.DataKey)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);

                entity.HasOne(d => d.UserCompType)
                    .WithMany(p => p.UserComps)
                    .HasForeignKey(d => d.UserCompTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserComp_UserCompType");
            });

            modelBuilder.Entity<UserCompType>(entity =>
            {
                entity.ToTable("UserCompType", "prop");

                entity.HasIndex(e => new { e.Name, e.IsDeleted }, "UQ_UserCompType_Name_IsDeleted")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDateTime)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(256);
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("prop");

        }
    }
}