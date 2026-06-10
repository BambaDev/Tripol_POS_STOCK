using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Adjustment> Adjustments { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AuditTrail> AuditTrails { get; set; }

    public virtual DbSet<BankTransaction> BankTransactions { get; set; }

    public virtual DbSet<BankTransfer> BankTransfers { get; set; }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BusinessLocation> BusinessLocations { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerTier> CustomerTiers { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAsset> EmployeeAssets { get; set; }

    public virtual DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }

    public virtual DbSet<EmployeeDependent> EmployeeDependents { get; set; }

    public virtual DbSet<EmployeeEngagementSurvey> EmployeeEngagementSurveys { get; set; }

    public virtual DbSet<EmployeeExpense> EmployeeExpenses { get; set; }

    public virtual DbSet<EmployeeFeedback> EmployeeFeedbacks { get; set; }

    public virtual DbSet<EmployeeFoodService> EmployeeFoodServices { get; set; }

    public virtual DbSet<EmployeeGoal> EmployeeGoals { get; set; }

    public virtual DbSet<EmployeeHealth> EmployeeHealths { get; set; }

    public virtual DbSet<EmployeePerformanceReview> EmployeePerformanceReviews { get; set; }

    public virtual DbSet<EmployeeReview> EmployeeReviews { get; set; }

    public virtual DbSet<EmployeeReward> EmployeeRewards { get; set; }

    public virtual DbSet<EmployeeShiftSchedule> EmployeeShiftSchedules { get; set; }

    public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }

    public virtual DbSet<EmployeeTransport> EmployeeTransports { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public virtual DbSet<FoodService> FoodServices { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PriceGroup> PriceGroups { get; set; }

    public virtual DbSet<Printer> Printers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAdjustment> ProductAdjustments { get; set; }

    public virtual DbSet<ProductBarCode> ProductBarCodes { get; set; }

    public virtual DbSet<ProductBatch> ProductBatches { get; set; }

    public virtual DbSet<ProductField> ProductFields { get; set; }

    public virtual DbSet<ProductFieldValue> ProductFieldValues { get; set; }

    public virtual DbSet<ProductHasField> ProductHasFields { get; set; }

    public virtual DbSet<ProductPrice> ProductPrices { get; set; }

    public virtual DbSet<ProductPurchaseReturn> ProductPurchaseReturns { get; set; }

    public virtual DbSet<ProductReturn> ProductReturns { get; set; }

    public virtual DbSet<ProductTransfer> ProductTransfers { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<ProductVariantValue> ProductVariantValues { get; set; }

    public virtual DbSet<ProductWarehouse> ProductWarehouses { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }

    public virtual DbSet<ProjectMilestone> ProjectMilestones { get; set; }

    public virtual DbSet<ProjectTask> ProjectTasks { get; set; }

    public virtual DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<Redemption> Redemptions { get; set; }

    public virtual DbSet<Register> Registers { get; set; }

    public virtual DbSet<RegisterRecord> RegisterRecords { get; set; }

    public virtual DbSet<RepairInvoice> RepairInvoices { get; set; }

    public virtual DbSet<RepairInvoiceDetail> RepairInvoiceDetails { get; set; }

    public virtual DbSet<RepairInvoicePayment> RepairInvoicePayments { get; set; }

    public virtual DbSet<RepairPayment> RepairPayments { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<ReturnPurchase> ReturnPurchases { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    public virtual DbSet<RewardHistory> RewardHistories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleHasPermission> RoleHasPermissions { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<SaleDetailHold> SaleDetailHolds { get; set; }

    public virtual DbSet<SaleHold> SaleHolds { get; set; }

    public virtual DbSet<SalePayment> SalePayments { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TodoList> TodoLists { get; set; }

    public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    public virtual DbSet<LoginAttempt> LoginAttempts { get; set; }

    public virtual DbSet<CashDiscrepancy> CashDiscrepancies { get; set; }

    public virtual DbSet<UserHasPermission> UserHasPermissions { get; set; }

    public virtual DbSet<UserHasRole> UserHasRoles { get; set; }

    public virtual DbSet<Variation> Variations { get; set; }

    public virtual DbSet<VariationValue> VariationValues { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    public virtual DbSet<Waste> Wastes { get; set; }

    public virtual DbSet<WasteItem> WasteItems { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<ComplaintCategory> ComplaintCategories { get; set; }

    public virtual DbSet<LoyaltyCard> LoyaltyCards { get; set; }

    public virtual DbSet<PointsTransaction> PointsTransactions { get; set; }

    public virtual DbSet<CardType> CardTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Server=.\\sqlexpress;Database=ezzipos;Trusted_Connection=True;" +
            "TrustServerCertificate=true;" +
            "Encrypt=true;" +  // PHASE 3G: Force TLS/SSL encryption
            "Connection Timeout=30;");

    /// <summary>
    /// Override SaveChanges pour valider toutes les entités avant insertion/modification
    /// Défense en profondeur - garantit validation même si Forms bypassé
    /// </summary>
    public override int SaveChanges()
    {
        ValidateEntitiesBeforeSave();
        return base.SaveChanges();
    }

    /// <summary>
    /// Override SaveChangesAsync pour validation
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ValidateEntitiesBeforeSave();
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Valide toutes les entités trackées avant save
    /// </summary>
    private void ValidateEntitiesBeforeSave()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in entries)
        {
            // Validation Sale
            if (entry.Entity is Sale sale)
            {
                var validation = Function.SaleValidator.ValidateSale(sale);
                if (!validation.isValid)
                {
                    throw new InvalidOperationException($"Sale validation failed: {validation.errorMessage}");
                }
            }

            // Validation Return
            if (entry.Entity is Return returnEntity)
            {
                // Valider seulement si a SaleId et GrandTotal
                if (returnEntity.SaleId.HasValue && returnEntity.GrandTotal.HasValue)
                {
                    var validation = Function.ReturnValidator.ValidateReturn(
                        returnEntity.SaleId.Value,
                        returnEntity.GrandTotal.Value,
                        this);

                    if (!validation.isValid)
                    {
                        throw new InvalidOperationException($"Return validation failed: {validation.errorMessage}");
                    }
                }
            }

            // Validation montants négatifs génériques
            ValidateNoNegativeAmounts(entry);
        }
    }

    /// <summary>
    /// Vérifie qu'aucun montant négatif n'est présent (sauf pour Returns/Adjustments)
    /// </summary>
    private void ValidateNoNegativeAmounts(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
    {
        // Skip pour entités autorisées à avoir montants négatifs
        if (entry.Entity is Return || entry.Entity is Adjustment || entry.Entity is BankTransaction)
            return;

        var entity = entry.Entity;
        var properties = entity.GetType().GetProperties();

        foreach (var prop in properties)
        {
            // Chercher propriétés "Amount", "Price", "Total", etc.
            if ((prop.Name.Contains("Amount") || prop.Name.Contains("Price") || prop.Name.Contains("Total"))
                && prop.PropertyType == typeof(decimal))
            {
                var value = (decimal?)prop.GetValue(entity);
                if (value.HasValue && value.Value < 0)
                {
                    throw new InvalidOperationException(
                        $"{entity.GetType().Name}.{prop.Name} cannot be negative: {value.Value}");
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== PHASE 4B RGPD: AUTOMATIC FIELD ENCRYPTION =====
        // Scanne tous les modèles pour [SensitiveData(RequiresEncryption=true)]
        // et applique automatiquement les ValueConverters d'encryption
        modelBuilder.ApplyGdprEncryption();

        // Générer rapport d'audit GDPR au démarrage (debug only)
        #if DEBUG
        string auditReport = Function.GdprModelBuilder.GenerateAuditReport(modelBuilder);
        System.Diagnostics.Debug.WriteLine(auditReport);
        #endif
        // ===== FIN PHASE 4B =====

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.ActivityLogs).HasConstraintName("FK_ActivityLog_Customer");
        });

        modelBuilder.Entity<Adjustment>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Adjustments).HasConstraintName("FK_Adjustment_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Adjustments).HasConstraintName("FK_Adjustment_Warehouse");
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assets__3214EC07E67BB756");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendan__3214EC07EEEB981D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances).HasConstraintName("FK__Attendanc__Emplo__6DED13EB");

            entity.HasOne(d => d.User).WithMany(p => p.Attendances).HasConstraintName("FK_Attendance_User");
        });

        modelBuilder.Entity<AuditTrail>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.AuditTrails).HasConstraintName("FK_AuditTrail_User");
        });

        modelBuilder.Entity<BankTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AddBank");

            entity.HasOne(d => d.User).WithMany(p => p.BankTransactions).HasConstraintName("FK_BankTransaction_User");
        });

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Benefits__3214EC07117BA9C6");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category_1");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasOne(d => d.State).WithMany(p => p.Cities).HasConstraintName("FK_City_State");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(d => d.PriceGroupNavigation).WithMany(p => p.Customers).HasConstraintName("FK_Customer_PriceGroup");

            entity.HasOne(d => d.TierLevel).WithMany(p => p.Customers).HasConstraintName("FK_Customer_CustomerTier");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Customer_User");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC07B9F69100");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07A403663E");

            entity.Property(e => e.Assured).HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateOfBirth).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Employees).HasConstraintName("FK_Employee_BusinessLocation");

            entity.HasOne(d => d.City).WithMany(p => p.Employees).HasConstraintName("FK_Employee_City");

            entity.HasOne(d => d.Country).WithMany(p => p.Employees).HasConstraintName("FK_Employee_Country");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasConstraintName("FK__Employee__Depart__68343A95");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees).HasConstraintName("FK__Employee__Positi__69285ECE");

            entity.HasOne(d => d.State).WithMany(p => p.Employees).HasConstraintName("FK_Employee_State");
        });

        modelBuilder.Entity<EmployeeAsset>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.AssetId }).HasName("PK__Employee__4EE4DD2461778A31");

            entity.HasOne(d => d.Asset).WithMany(p => p.EmployeeAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeAssets_Assets");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAssets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeAssets_Employee");
        });

        modelBuilder.Entity<EmployeeBenefit>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.BenefitId }).HasName("PK__Employee__CFA5037CD7154C8E");

            entity.HasOne(d => d.Benefit).WithMany(p => p.EmployeeBenefits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBenefits_Benefits");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeBenefits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBenefits_Employee");
        });

        modelBuilder.Entity<EmployeeDependent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07AEABB2E4");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDependents).HasConstraintName("FK_EmployeeDependents_Employee");
        });

        modelBuilder.Entity<EmployeeEngagementSurvey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07527A2F5F");
        });

        modelBuilder.Entity<EmployeeExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC070ABAA85E");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeExpenses).HasConstraintName("FK_EmployeeExpenses_Employee");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeExpenses).HasConstraintName("FK_EmployeeExpenses_User");
        });

        modelBuilder.Entity<EmployeeFeedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0796DDB9CF");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeFeedbackEmployees).HasConstraintName("FK_EmployeeFeedback_Employee");

            entity.HasOne(d => d.FeedbackByNavigation).WithMany(p => p.EmployeeFeedbackFeedbackByNavigations).HasConstraintName("FK_EmployeeFeedback_Employee_Feedback_By");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeFeedbacks).HasConstraintName("FK_EmployeeFeedback_User");
        });

        modelBuilder.Entity<EmployeeFoodService>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeFoodServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFoodServices_Employee");

            entity.HasOne(d => d.FoodService).WithMany(p => p.EmployeeFoodServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeFoodServices_FoodServices");
        });

        modelBuilder.Entity<EmployeeGoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07B8972461");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeGoals).HasConstraintName("FK_EmployeeGoals_Employee");
        });

        modelBuilder.Entity<EmployeeHealth>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07435A3772");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeHealths).HasConstraintName("FK_EmployeeHealth_User");
        });

        modelBuilder.Entity<EmployeePerformanceReview>(entity =>
        {
            entity.HasOne(d => d.EmployeeReview).WithMany(p => p.EmployeePerformanceReviews).HasConstraintName("FK_EmployeePerformanceReview_EmployeeReview");

            entity.HasOne(d => d.Evaluation).WithMany(p => p.EmployeePerformanceReviews).HasConstraintName("FK_EmployeePerformanceReview_Evaluation");
        });

        modelBuilder.Entity<EmployeeReview>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeReviews).HasConstraintName("FK_EmployeeReview_Employee");

            entity.HasOne(d => d.Interviewer).WithMany(p => p.EmployeeReviewInterviewers).HasConstraintName("FK_EmployeeReview_User_Interviewer");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeReviewUsers).HasConstraintName("FK_EmployeeReview_User");
        });

        modelBuilder.Entity<EmployeeReward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0787ECD045");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRewards).HasConstraintName("FK_EmployeeRewards_Employee");

            entity.HasOne(d => d.User).WithMany(p => p.EmployeeRewards).HasConstraintName("FK_EmployeeRewards_User");
        });

        modelBuilder.Entity<EmployeeShiftSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC07371FAACC");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeShiftSchedules).HasConstraintName("FK_EmployeeShiftSchedules_Employee");
        });

        modelBuilder.Entity<EmployeeTraining>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.TrainingProgramId }).HasName("PK__Employee__BE28D8B4D8839471");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTrainings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTraining_Employee");

            entity.HasOne(d => d.TrainingProgram).WithMany(p => p.EmployeeTrainings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTraining_TrainingPrograms");
        });

        modelBuilder.Entity<EmployeeTransport>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.TransportId }).HasName("PK__Employee__BB4ED500746C2FC3");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTransports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTransport_Employee");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(d => d.ExpenseCategory).WithMany(p => p.Expenses).HasConstraintName("FK_Expense_ExpenseCategory");

            entity.HasOne(d => d.User).WithMany(p => p.Expenses).HasConstraintName("FK_Expense_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Expenses).HasConstraintName("FK_Expense_Warehouse");
        });

        modelBuilder.Entity<FoodService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodServ__3214EC07A56D9131");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Inventories).HasConstraintName("FK_Inventory_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Inventories).HasConstraintName("FK_Inventory_Warehouse");
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasOne(d => d.Inventory).WithMany(p => p.InventoryItems).HasConstraintName("FK_InventoryItem_Inventory");

            entity.HasOne(d => d.Item).WithMany(p => p.InventoryItems).HasConstraintName("FK_InventoryItem_Product");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LeaveReq__3214EC07D8A6B00C");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequests).HasConstraintName("FK_LeaveRequests_Employee");
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payroll__3214EC0701774F48");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Employee).WithMany(p => p.Payrolls).HasConstraintName("FK__Payroll__Employe__72B1C908");

            entity.HasOne(d => d.User).WithMany(p => p.Payrolls).HasConstraintName("FK_Payroll_User");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_permissions");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC07236A43FE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Printer>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Printers).HasConstraintName("FK_Printer_BusinessLocation");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasConstraintName("FK_Product_Brand");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Unit).WithMany(p => p.Products).HasConstraintName("FK_Product_Unit1");

            entity.HasOne(d => d.User).WithMany(p => p.Products).HasConstraintName("FK_Product_User");

            entity.HasOne(d => d.Warranty).WithMany(p => p.Products).HasConstraintName("FK_Product_Warranty");
        });

        modelBuilder.Entity<ProductAdjustment>(entity =>
        {
            entity.HasOne(d => d.Adjustment).WithMany(p => p.ProductAdjustments).HasConstraintName("FK_ProductAdjustment_Adjustment");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAdjustments).HasConstraintName("FK_ProductAdjustment_Product");

            entity.HasOne(d => d.Variant).WithMany(p => p.ProductAdjustments).HasConstraintName("FK_ProductAdjustment_Variation");
        });

        modelBuilder.Entity<ProductBarCode>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.ProductBarCodes).HasConstraintName("FK_ProductBarCode_Product");
        });

        modelBuilder.Entity<ProductFieldValue>(entity =>
        {
            entity.HasOne(d => d.ProductField).WithMany(p => p.ProductFieldValues).HasConstraintName("FK_ProductFieldValue_ProductField");
        });

        modelBuilder.Entity<ProductHasField>(entity =>
        {
            entity.HasOne(d => d.ProductField).WithMany(p => p.ProductHasFields).HasConstraintName("FK_ProductHasField_ProductField");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductHasFields).HasConstraintName("FK_ProductHasField_Product");
        });

        modelBuilder.Entity<ProductPrice>(entity =>
        {
            entity.HasOne(d => d.PriceGroup).WithMany(p => p.ProductPrices).HasConstraintName("FK_ProductPrice_PriceGroup");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPrices).HasConstraintName("FK_ProductPrice_Product");
        });

        modelBuilder.Entity<ProductPurchaseReturn>(entity =>
        {
            entity.HasOne(d => d.ProductBatch).WithMany(p => p.ProductPurchaseReturns).HasConstraintName("FK_ProductPurchaseReturn_ProductBatch");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPurchaseReturns).HasConstraintName("FK_ProductPurchaseReturn_Product");

            entity.HasOne(d => d.Return).WithMany(p => p.ProductPurchaseReturns).HasConstraintName("FK_ProductPurchaseReturn_ReturnPurchase1");
        });

        modelBuilder.Entity<ProductReturn>(entity =>
        {
            entity.HasOne(d => d.ProductBatch).WithMany(p => p.ProductReturns).HasConstraintName("FK_ProductReturn_ProductBatch");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReturns).HasConstraintName("FK_ProductReturn_Product");

            entity.HasOne(d => d.Return).WithMany(p => p.ProductReturns).HasConstraintName("FK_ProductReturn_Return");

            entity.HasOne(d => d.Variant).WithMany(p => p.ProductReturns).HasConstraintName("FK_ProductReturn_Variation");
        });

        modelBuilder.Entity<ProductTransfer>(entity =>
        {
            entity.HasOne(d => d.ProductBatch).WithMany(p => p.ProductTransfers).HasConstraintName("FK_ProductTransfer_ProductBatch");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductTransfers).HasConstraintName("FK_ProductTransfer_Product");

            entity.HasOne(d => d.Transfer).WithMany(p => p.ProductTransfers).HasConstraintName("FK_ProductTransfer_Transfer");

            entity.HasOne(d => d.Variant).WithMany(p => p.ProductTransfers).HasConstraintName("FK_ProductTransfer_Variation");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductVariant_1");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants).HasConstraintName("FK_ProductVariant_Product");

            entity.HasOne(d => d.Variation).WithMany(p => p.ProductVariants).HasConstraintName("FK_ProductVariant_Variation");
        });

        modelBuilder.Entity<ProductVariantValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductVariant");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariantValues).HasConstraintName("FK_ProductVariantValue_Product");

            entity.HasOne(d => d.Variation).WithMany(p => p.ProductVariantValues).HasConstraintName("FK_ProductVariantValue_Variation");
        });

        modelBuilder.Entity<ProductWarehouse>(entity =>
        {
            entity.HasOne(d => d.ProductBatch).WithMany(p => p.ProductWarehouses).HasConstraintName("FK_ProductWarehouse_ProductBatch");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductWarehouses).HasConstraintName("FK_ProductWarehouse_Product");

            entity.HasOne(d => d.Variant).WithMany(p => p.ProductWarehouses).HasConstraintName("FK_ProductWarehouse_Variation");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ProductWarehouses).HasConstraintName("FK_ProductWarehouse_Warehouse");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC07D4985E51");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects).HasConstraintName("FK_Projects_Customer");

            entity.HasOne(d => d.Manager).WithMany(p => p.Projects).HasConstraintName("FK_Projects_Employee");

            entity.HasOne(d => d.User).WithMany(p => p.Projects).HasConstraintName("FK_Projects_User");
        });

        modelBuilder.Entity<ProjectDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectD__3214EC07F8A1E5EA");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectDocuments).HasConstraintName("FK_ProjectDocuments_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectDocuments).HasConstraintName("FK_ProjectDocuments_User");
        });

        modelBuilder.Entity<ProjectMilestone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectM__3214EC074274A7F6");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectMilestones).HasConstraintName("FK_ProjectMilestones_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectMilestones).HasConstraintName("FK_ProjectMilestones_User");
        });

        modelBuilder.Entity<ProjectTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectT__3214EC0715950D1E");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.ProjectTasks).HasConstraintName("FK_ProjectTasks_Employee");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks).HasConstraintName("FK_ProjectTasks_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectTasks).HasConstraintName("FK_ProjectTasks_User");
        });

        modelBuilder.Entity<ProjectTeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProjectT__3214EC07184F1187");

            entity.HasOne(d => d.Employee).WithMany(p => p.ProjectTeamMembers).HasConstraintName("FK_ProjectTeamMembers_Employee");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTeamMembers).HasConstraintName("FK_ProjectTeamMembers_Projects");

            entity.HasOne(d => d.User).WithMany(p => p.ProjectTeamMembers).HasConstraintName("FK_ProjectTeamMembers_User");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.Promotions).HasConstraintName("FK_Promotion_Product");

            entity.HasOne(d => d.User).WithMany(p => p.Promotions).HasConstraintName("FK_Promotion_User");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Purchases).HasConstraintName("FK_Purchase_BusinessLocation");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchases).HasConstraintName("FK_Purchase_Supplier");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases).HasConstraintName("FK_Purchase_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Purchases).HasConstraintName("FK_Purchase_Warehouse");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseDetails).HasConstraintName("FK_PurchaseDetail_Purchase");
        });

        modelBuilder.Entity<Redemption>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.Redemptions).HasConstraintName("FK_Redemption_Customer");

            entity.HasOne(d => d.Reward).WithMany(p => p.Redemptions).HasConstraintName("FK_Redemption_Reward");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Registers).HasConstraintName("FK_Register_BusinessLocation");
        });

        modelBuilder.Entity<RegisterRecord>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.RegisterRecords).HasConstraintName("FK_RegisterRecord_BusinessLocation");

            entity.HasOne(d => d.ClosedBy).WithMany(p => p.RegisterRecordClosedBies).HasConstraintName("FK_RegisterRecord_User1");

            entity.HasOne(d => d.Register).WithMany(p => p.RegisterRecords).HasConstraintName("FK_RegisterRecord_Register");

            entity.HasOne(d => d.TransferredTo).WithMany(p => p.RegisterRecordTransferredTos).HasConstraintName("FK_RegisterRecord_User2");

            entity.HasOne(d => d.User).WithMany(p => p.RegisterRecordUsers).HasConstraintName("FK_RegisterRecord_User");
        });

        modelBuilder.Entity<RepairInvoice>(entity =>
        {
            entity.HasOne(d => d.Brand).WithMany(p => p.RepairInvoices).HasConstraintName("FK_RepairInvoice_Brand");

            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.RepairInvoices).HasConstraintName("FK_RepairInvoice_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.RepairInvoices).HasConstraintName("FK_RepairInvoice_Customer");

            entity.HasOne(d => d.User).WithMany(p => p.RepairInvoices).HasConstraintName("FK_RepairInvoice_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.RepairInvoices).HasConstraintName("FK_RepairInvoice_Warehouse");
        });

        modelBuilder.Entity<RepairInvoiceDetail>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.RepairInvoiceDetails).HasConstraintName("FK_RepairInvoiceDetail_Product");

            entity.HasOne(d => d.RepairInvoice).WithMany(p => p.RepairInvoiceDetails).HasConstraintName("FK_RepairInvoiceDetail_RepairInvoice");
        });

        modelBuilder.Entity<RepairInvoicePayment>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.RepairInvoicePayments).HasConstraintName("FK_RepairInvoicePayment_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.RepairInvoicePayments).HasConstraintName("FK_RepairInvoicePayment_Customer");

            entity.HasOne(d => d.RepairInvoice).WithMany(p => p.RepairInvoicePayments).HasConstraintName("FK_RepairInvoicePayment_RepairInvoice");

            entity.HasOne(d => d.User).WithMany(p => p.RepairInvoicePayments).HasConstraintName("FK_RepairInvoicePayment_User");
        });

        modelBuilder.Entity<RepairPayment>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.RepairPayments).HasConstraintName("FK_RepairPayment_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.RepairPayments).HasConstraintName("FK_RepairPayment_Customer");

            entity.HasOne(d => d.RepairInvoice).WithMany(p => p.RepairPayments).HasConstraintName("FK_RepairPayment_RepairInvoice");

            entity.HasOne(d => d.User).WithMany(p => p.RepairPayments).HasConstraintName("FK_RepairPayment_User");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasOne(d => d.CashRegister).WithMany(p => p.Returns).HasConstraintName("FK_Return_RegisterRecord");

            entity.HasOne(d => d.Customer).WithMany(p => p.Returns).HasConstraintName("FK_Return_Customer");

            entity.HasOne(d => d.Sale).WithMany(p => p.Returns).HasConstraintName("FK_Return_Sale");

            entity.HasOne(d => d.User).WithMany(p => p.Returns).HasConstraintName("FK_Return_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Returns).HasConstraintName("FK_Return_Warehouse");
        });

        modelBuilder.Entity<ReturnPurchase>(entity =>
        {
            entity.HasOne(d => d.CashRegister).WithMany(p => p.ReturnPurchases).HasConstraintName("FK_ReturnPurchase_RegisterRecord");

            entity.HasOne(d => d.Purchase).WithMany(p => p.ReturnPurchases).HasConstraintName("FK_ReturnPurchase_Purchase");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ReturnPurchases).HasConstraintName("FK_ReturnPurchase_Supplier");

            entity.HasOne(d => d.User).WithMany(p => p.ReturnPurchases).HasConstraintName("FK_ReturnPurchase_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.ReturnPurchases).HasConstraintName("FK_ReturnPurchase_Warehouse");
        });

        modelBuilder.Entity<RewardHistory>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.RewardHistories).HasConstraintName("FK_RewardHistory_Customer");

            entity.HasOne(d => d.Reward).WithMany(p => p.RewardHistories).HasConstraintName("FK_RewardHistory_Reward");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_roles");
        });

        modelBuilder.Entity<RoleHasPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_role_has_permissions");

            entity.HasOne(d => d.Permission).WithMany(p => p.RoleHasPermissions).HasConstraintName("FK_role_has_permissions_permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleHasPermissions).HasConstraintName("FK_role_has_permissions_roles");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Sales).HasConstraintName("FK_Sale_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.Sales).HasConstraintName("FK_Sale_Customer");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.Sales).HasConstraintName("FK_Sale_Hold_Warehouse");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.SaleDetails).HasConstraintName("FK_SaleDetail_Hold_Product");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails).HasConstraintName("FK_SaleDetail_Hold_Sale");
        });

        modelBuilder.Entity<SaleDetailHold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SaleDetail_Hold");

            entity.HasOne(d => d.Product).WithMany(p => p.SaleDetailHolds).HasConstraintName("FK_SaleDetailHold_Product");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetailHolds).HasConstraintName("FK_SaleDetailHold_SaleHold");
        });

        modelBuilder.Entity<SaleHold>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Sale_Hold");

            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.SaleHolds).HasConstraintName("FK_SaleHold_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleHolds).HasConstraintName("FK_SaleHold_Customer");

            entity.HasOne(d => d.User).WithMany(p => p.SaleHolds).HasConstraintName("FK_SaleHold_User");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.SaleHolds).HasConstraintName("FK_SaleHold_Warehouse");
        });

        modelBuilder.Entity<SalePayment>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.SalePayments).HasConstraintName("FK_SalePayment_BusinessLocation");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalePayments).HasConstraintName("FK_SalePayment_Customer");

            entity.HasOne(d => d.Sale).WithMany(p => p.SalePayments).HasConstraintName("FK_SalePayment_Sale");

            entity.HasOne(d => d.User).WithMany(p => p.SalePayments).HasConstraintName("FK_SalePayment_User");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasOne(d => d.PrinterDocumentNavigation).WithMany(p => p.SettingPrinterDocumentNavigations).HasConstraintName("FK_Setting_Printer1");

            entity.HasOne(d => d.PrinterRecieptNavigation).WithMany(p => p.SettingPrinterRecieptNavigations).HasConstraintName("FK_Setting_Printer");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasOne(d => d.Country).WithMany(p => p.States).HasConstraintName("FK_State_Country");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Suppliers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Supplier_User");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<TodoList>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.TodoLists).HasConstraintName("FK_TodoList_User");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Training__3214EC0713E7136A");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasOne(d => d.Customer).WithMany(p => p.Transactions).HasConstraintName("FK_Transaction_Customer");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasOne(d => d.FromWarehouse).WithMany(p => p.TransferFromWarehouses).HasConstraintName("FK_Transfer_Warehouse");

            entity.HasOne(d => d.ToWarehouse).WithMany(p => p.TransferToWarehouses).HasConstraintName("FK_Transfer_Warehouse1");

            entity.HasOne(d => d.User).WithMany(p => p.Transfers).HasConstraintName("FK_Transfer_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users");

            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Users).HasConstraintName("FK_User_BusinessLocation");
        });

        modelBuilder.Entity<UserHasPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user_has_permissions");

            entity.HasOne(d => d.Permission).WithMany(p => p.UserHasPermissions).HasConstraintName("FK_user_has_permissions_permissions");

            entity.HasOne(d => d.User).WithMany(p => p.UserHasPermissions).HasConstraintName("FK_user_has_permissions_users");
        });

        modelBuilder.Entity<UserHasRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_user_has_roles");

            entity.HasOne(d => d.Role).WithMany(p => p.UserHasRoles).HasConstraintName("FK_user_has_roles_roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserHasRoles).HasConstraintName("FK_user_has_roles_users");
        });

        modelBuilder.Entity<VariationValue>(entity =>
        {
            entity.HasOne(d => d.Variation).WithMany(p => p.VariationValues).HasConstraintName("FK_VariationValue_Variation");
        });

        modelBuilder.Entity<Waste>(entity =>
        {
            entity.HasOne(d => d.BusinessLocation).WithMany(p => p.Wastes).HasConstraintName("FK_Waste_BusinessLocation");

            entity.HasOne(d => d.Employee).WithMany(p => p.Wastes).HasConstraintName("FK_Waste_Employee");

            entity.HasOne(d => d.User).WithMany(p => p.Wastes).HasConstraintName("FK_Waste_User");
        });

        modelBuilder.Entity<WasteItem>(entity =>
        {
            entity.HasOne(d => d.Waste).WithMany(p => p.WasteItems).HasConstraintName("FK_WasteItem_Product");

            entity.HasOne(d => d.WasteNavigation).WithMany(p => p.WasteItems).HasConstraintName("FK_WasteItem_Waste");
        });

        modelBuilder.Entity<LoyaltyCard>(entity =>
        {
            entity.HasOne(d => d.CardType).WithMany(p => p.LoyaltyCards).HasConstraintName("FK_LoyaltyCard_CardType");

            entity.HasOne(d => d.Customer).WithMany(p => p.LoyaltyCards).HasConstraintName("FK_LoyaltyCard_Customer");
        });

        modelBuilder.Entity<CardType>(entity =>
        {
            entity.Property(e => e.CurrencyPerPoint).HasDefaultValue(1.0m);
        });

        modelBuilder.Entity<PointsTransaction>(entity =>
        {
            entity.HasOne(d => d.LoyaltyCard).WithMany(p => p.PointsTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PointsTransactions_LoyaltyCards");

            entity.HasOne(d => d.Sale).WithMany(p => p.PointsTransactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PointsTransactions_Sales");

            entity.HasOne(d => d.User).WithMany(p => p.PointsTransactions).HasConstraintName("FK_PointsTransactions_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
