# 📚 Documentation Développeur - Ezzipos POS

## Table des Matières

1. [Vue d'ensemble](#vue-densemble)
2. [Architecture du Projet](#architecture-du-projet)
3. [Modèles de Données](#modèles-de-données)
4. [Système d'Authentification](#système-dauthentification)
5. [Gestion des Formulaires](#gestion-des-formulaires)
6. [Fonctions Utilitaires](#fonctions-utilitaires)
7. [Patterns et Conventions](#patterns-et-conventions)
8. [Guide de Développement](#guide-de-développement)

---

## Vue d'ensemble

**Ezzipos** est un système ERP complet développé en **C# .NET 8.0** avec **Windows Forms** et **DevExpress 23.2.3**.

### Statistiques du Projet
- **Fichiers C#**: 452 fichiers
- **Modèles de données**: 116+ entités
- **Modules fonctionnels**: 66+ dossiers de formulaires
- **Lignes de code**: ~50,000+ lignes

### Technologies Principales
```xml
<PackageReference Include="DevExpress.Win.BonusSkins" Version="23.2.3" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
<PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
<PackageReference Include="Stripe.net" Version="47.1.0" />
<PackageReference Include="Twilio" Version="7.8.0" />
<PackageReference Include="SqlTableDependency" Version="8.5.8" />
```

---

## Architecture du Projet

### Structure en Couches

```
Pos/Pos/
├── Models/              # 116+ entités EF Core
│   ├── AppDbContext.cs  # Contexte principal
│   ├── Product.cs, Sale.cs, Customer.cs, Employee.cs
│   └── [Toutes les entités métier]
│
├── Forms/               # 66+ modules UI
│   ├── MainFrm.cs       # Fenêtre principale MDI
│   ├── Alert/           # Dialogues et messages
│   ├── Auth/            # Login, LockScreen
│   ├── Product/         # Gestion produits
│   ├── Sale/            # Ventes et POS
│   ├── Customer/        # Gestion clients
│   ├── Employee/        # RH et employés
│   ├── CardFidelity/    # Programme fidélité
│   └── [Autres modules]
│
├── Function/            # 27 classes utilitaires
│   ├── Helper.cs        # Fonctions générales
│   ├── Permission.cs    # Système de droits
│   ├── DatabaseSeeder.cs # Données initiales
│   ├── Sound.cs         # Effets sonores
│   ├── Email.cs         # Envoi emails
│   ├── SmsSender.cs     # SMS via Twilio
│   └── [Autres utilitaires]
│
├── Report/              # Rapports DevExpress
├── Migrations/          # Migrations EF Core
└── Program.cs           # Point d'entrée
```

### Flux de Données

```
┌─────────────┐
│   Program   │ ──> Initialise DB, charge langue
└──────┬──────┘
       │
       ├──> Login/InitAccount
       │
       ├──> MainFrm (MDI Container)
       │    │
       │    ├──> Dashboard
       │    ├──> POS Screen
       │    ├──> Forms (CRUD)
       │    └──> Reports
       │
       └──> AppDbContext ──> SQL Server
                 │
                 ├──> Shared.db (singleton)
                 └──> Using blocks (recommandé)
```

---

## Modèles de Données

### Entités Principales

#### Product
```csharp
[Table("Product")]
public partial class Product
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string ProductName { get; set; }
    
    [StringLength(250)]
    public string Sku { get; set; }
    
    [StringLength(250)]
    public string Barcode { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PurchasePriceExcTax { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }
    
    public int? AlertQuantity { get; set; }
    public byte[] Image { get; set; }
    
    // Relations
    public virtual Brand Brand { get; set; }
    public virtual Category Category { get; set; }
    public virtual Unit Unit { get; set; }
    public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; }
    public virtual ICollection<SaleDetail> SaleDetails { get; set; }
}
```

#### Customer
```csharp
[Table("Customer")]
public partial class Customer
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string FirstName { get; set; }
    
    [StringLength(250)]
    public string LastName { get; set; }
    
    [StringLength(250)]
    public string Email { get; set; }
    
    [StringLength(250)]
    public string Phone { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalPointsAccumulated { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrentPoints { get; set; }
    
    // Programme fidélité
    public int? TierLevelId { get; set; }
    public virtual CustomerTier TierLevel { get; set; }
    public virtual ICollection<LoyaltyCard> LoyaltyCards { get; set; }
    public virtual ICollection<Sale> Sales { get; set; }
}
```

#### Sale
```csharp
[Table("Sale")]
public partial class Sale
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string ReferenceNo { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime? SaleDate { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetTotalAmount { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PaidAmount { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Due { get; set; }
    
    // Relations
    public int? CustomerId { get; set; }
    public int? BusinessLocationId { get; set; }
    public int? WarehouseId { get; set; }
    
    public virtual Customer Customer { get; set; }
    public virtual ICollection<SaleDetail> SaleDetails { get; set; }
    public virtual ICollection<SalePayment> SalePayments { get; set; }
}
```

#### Employee
```csharp
[Table("Employee")]
public partial class Employee
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string FirstName { get; set; }
    
    [StringLength(100)]
    public string LastName { get; set; }
    
    [StringLength(100)]
    public string Email { get; set; }
    
    [StringLength(15)]
    public string PhoneNumber { get; set; }
    
    public DateOnly? HireDate { get; set; }
    
    [Column(TypeName = "image")]
    public byte[] Image { get; set; }
    
    // RH
    public int? DepartmentId { get; set; }
    public int? PositionId { get; set; }
    public int? WorkHoursPerWeek { get; set; }
    
    public virtual Department Department { get; set; }
    public virtual Position Position { get; set; }
    public virtual ICollection<Attendance> Attendances { get; set; }
    public virtual ICollection<Payroll> Payrolls { get; set; }
}
```

### Relations Entre Entités

```
Product ──┬──< ProductWarehouse (stock par entrepôt)
          ├──< ProductPrice (prix par groupe)
          ├──< ProductVariant (variantes)
          ├──< SaleDetail (lignes de vente)
          └──< PurchaseDetail (lignes d'achat)

Customer ──┬──< Sale
           ├──< LoyaltyCard
           ├──< PointsTransaction
           └──< ActivityLog

Sale ──┬──< SaleDetail
       ├──< SalePayment
       └──< Return

Warehouse ──┬──< ProductWarehouse
            ├──< Sale
            ├──< Purchase
            └──< Transfer
```

---

## Système d'Authentification

### Flux d'Authentification

```csharp
// Program.cs - Point d'entrée
static void Main()
{
    // 1. Initialisation DB
    using (AppDbContext db = new())
    {
        db.Database.EnsureCreated();
        DatabaseSeeder.Seed(db);
    }
    
    // 2. Vérification session
    if (IsUserLoggedIn())
    {
        Application.Run(new Forms.MainFrm());
    }
    else
    {
        if (Helper.hasUser())
            Application.Run(new Forms.Auth.Login());
        else
            Application.Run(new Forms.Alert.InitAccount());
    }
}

public static bool IsUserLoggedIn()
{
    return !string.IsNullOrEmpty(Properties.Settings.Default.UserSession);
}
```

### Gestion des Sessions

Les informations de session sont stockées dans `Properties.Settings.Default`:

```csharp
// Variables de session
Properties.Settings.Default.UserSession      // Token session
Properties.Settings.Default.userId           // ID utilisateur
Properties.Settings.Default.UserRoleID       // ID du rôle
Properties.Settings.Default.isAdmin          // "Admin" ou vide
Properties.Settings.Default.CurrentUserFullName
Properties.Settings.Default.BusinessLocation // Location active
```

### Système de Permissions

```csharp
// Function/Permission.cs
public static bool HasPermission(string permissionName)
{
    // Admin bypass toutes les permissions
    if(isAdmin == "Admin")
        return true;
    
    // Vérification en base
    if(Shared.db.RoleHasPermissions
        .Where(u => u.RoleId == userRoleId)
        .Where(p => p.Permission.Name == permissionName)
        .Count() > 0)
        return true;
    
    // Accès refusé
    Forms.Alert.AccessDenied accessDenied = new();
    accessDenied.ShowDialog();
    return false;
}

// Utilisation
if (Permission.HasPermission("List Customers"))
{
    openMdiChildForm(typeof(Customer.Customers));
}
```

### Permissions Prédéfinies

```
- Dashboard
- POS, List Sales, Add Sale
- List Products, Add Product, Edit Product, Delete Product
- List Customers, Add Customer, Edit Customer
- List Purchases, Add Purchase
- List Employees, Add Employee
- Inventory, Stocks, Adjustments
- Reports, Settings
- [100+ permissions...]
```

---

## Gestion des Formulaires

### Pattern MDI (Multiple Document Interface)

```csharp
// MainFrm.cs - Fenêtre principale
public partial class MainFrm : DevExpress.XtraBars.Ribbon.RibbonForm
{
    // Empêche les doublons
    private bool IsFormAlreadyOpen(Type formType)
    {
        foreach (Form form in this.MdiChildren)
        {
            if (form.GetType() == formType)
            {
                form.Activate();
                return true;
            }
        }
        return false;
    }
    
    // Ouvre un formulaire MDI
    private void openMdiChildForm(Type formType)
    {
        if (!IsFormAlreadyOpen(formType))
        {
            Form form = (Form)Activator.CreateInstance(formType);
            form.MdiParent = this;
            form.Show();
        }
    }
}
```

### Pattern Overlay pour Modales

```csharp
// Overlay = fond semi-transparent
private void btnAddCustomer_ItemClick(object sender, ItemClickEventArgs e)
{
    if (Permission.HasPermission("Add Customer"))
    {
        // 1. Créer overlay
        OverlayForm overlay = new OverlayForm(this);
        overlay.Show();
        
        // 2. Créer formulaire modal
        Customer.AddEditCustomer customer = new();
        
        // 3. Fermer overlay quand modal se ferme
        customer.FormClosed += (s, args) => overlay.Close();
        
        // 4. Afficher
        customer.Show();
        customer.TopMost = true;
    }
}
```

### Cycle de Vie d'un Formulaire CRUD

```csharp
// AddEditCustomer.cs
public partial class AddEditCustomer : XtraForm
{
    public string type = "Add"; // "Add" ou "Edit"
    public int currentItemId = 0;
    
    public AddEditCustomer()
    {
        InitializeComponent();
        this.toRtl();              // Support RTL arabe
        this.BorderStyle();        // Bordures arrondies
    }
    
    // Constructeur pour édition
    public AddEditCustomer(int customerId)
    {
        InitializeComponent();
        this.currentItemId = customerId;
        this.type = "Edit";
        this.toRtl();
        this.BorderStyle();
    }
    
    private void Form_Load(object sender, EventArgs e)
    {
        if (type == "Edit")
        {
            LoadCustomerData();
        }
        LoadLookups(); // Charger dropdowns
    }
    
    private void LoadCustomerData()
    {
        using (var db = new AppDbContext())
        {
            var customer = db.Customers
                .Include(c => c.TierLevel)
                .FirstOrDefault(c => c.Id == currentItemId);
                
            if (customer != null)
            {
                txtFirstName.Text = customer.FirstName;
                txtLastName.Text = customer.LastName;
                txtEmail.Text = customer.Email;
                // ... autres champs
            }
        }
    }
    
    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!Validate()) return;
        
        using (var db = new AppDbContext())
        {
            Models.Customer customer;
            
            if (type == "Add")
            {
                customer = new Models.Customer();
                db.Customers.Add(customer);
            }
            else
            {
                customer = db.Customers.Find(currentItemId);
            }
            
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;
            customer.Email = txtEmail.Text;
            customer.UpdatedAt = DateTime.Now;
            
            db.SaveChanges();
            Sound.Added();
            this.Close();
        }
    }
}
```

---

## Fonctions Utilitaires

### Helper.cs - Fonctions Générales

```csharp
// Function/Helper.cs
internal class Helper
{
    // Style grille
    public static string RowColor = "#1abc9c";
    public static string FocusColor = "#3498db";
    public static int RowHeight = 30;
    
    // Redimensionner images
    public static Image ResizeImage(Image image, int maxWidth, int maxHeight)
    {
        int width = image.Width;
        int height = image.Height;
        
        if (width > maxWidth || height > maxHeight)
        {
            float scalingFactor = Math.Min(
                (float)maxWidth / width, 
                (float)maxHeight / height
            );
            
            width = (int)(width * scalingFactor);
            height = (int)(height * scalingFactor);
        }
        
        var resizedImage = new Bitmap(width, height);
        using (var graphics = Graphics.FromImage(resizedImage))
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(image, 0, 0, width, height);
        }
        
        return resizedImage;
    }
    
    // Traiter points fidélité
    public static void ProcessLoyaltyPoints(int saleId)
    {
        using (var context = new AppDbContext())
        {
            var sale = context.Sales
                .Include(s => s.Customer)
                .FirstOrDefault(s => s.Id == saleId);
                
            if (sale?.Customer == null) return;
            
            // Calculer points (1% du montant)
            decimal points = (sale.NetTotalAmount ?? 0) * 0.01m;
            
            // Mettre à jour client
            sale.Customer.CurrentPoints += points;
            sale.Customer.TotalPointsAccumulated += points;
            
            // Créer transaction
            var transaction = new PointsTransaction
            {
                CustomerId = sale.CustomerId,
                SaleId = saleId,
                Points = points,
                TransactionType = "Earned",
                TransactionDate = DateTime.Now
            };
            
            context.PointsTransactions.Add(transaction);
            context.SaveChanges();
        }
    }
}
```

### Sound.cs - Effets Sonores

```csharp
// Function/Sound.cs
internal static class Sound
{
    private static readonly Dictionary<string, SoundPlayer> soundPlayers = new();
    
    static Sound()
    {
        PreloadSounds();
    }
    
    private static void PreloadSounds()
    {
        LoadSound("Added.wav");
        LoadSound("Deleted.wav");
        LoadSound("Selected.wav");
        LoadSound("Denied.wav");
    }
    
    public static void PlaySound(string soundFileName)
    {
        var setting = Shared.db.Settings.FirstOrDefault();
        
        bool canPlay = soundFileName switch
        {
            "Added.wav" => setting?.IsSoundAdded ?? false,
            "Deleted.wav" => setting?.IsSoundDeleted ?? false,
            "Selected.wav" => setting?.IsSoundSelected ?? false,
            _ => false
        };
        
        if (canPlay && soundPlayers.TryGetValue(soundFileName, out var player))
        {
            player.Play();
        }
    }
    
    // Méthodes rapides
    public static void Added() => PlaySound("Added.wav");
    public static void Deleted() => PlaySound("Deleted.wav");
    public static void Selected() => PlaySound("Selected.wav");
}
```

### DatabaseSeeder.cs - Données Initiales

```csharp
// Function/DatabaseSeeder.cs (13,637 lignes!)
internal class DatabaseSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Settings.Any()) return;
        
        // Settings système
        var setting = new Setting
        {
            Company = "Example Company",
            Email = "info@example.com",
            WhatsAppStatus = "Active",
            MailStatus = "Active",
            // ... 40+ paramètres
        };
        context.Settings.Add(setting);
        
        // Permissions (100+)
        SeedPermissions(context);
        
        // Rôles
        SeedRoles(context);
        
        // Données de test
        SeedCategories(context);
        SeedProducts(context);
        
        context.SaveChanges();
    }
    
    public static void SeedProductWarehouse()
    {
        using (var context = new AppDbContext())
        {
            if (context.ProductWarehouses.Any()) return;
            
            var warehouse = context.Warehouses.First();
            var productWarehouses = new List<ProductWarehouse>
            {
                new ProductWarehouse
                {
                    ProductId = context.Products.First(p => p.Sku == "248").Id,
                    WarehouseId = warehouse.Id,
                    Qty = 16,
                    Price = 450.0m,
                    CreatedAt = DateTime.UtcNow
                },
                // ... 100+ produits de démonstration
            };
            
            context.ProductWarehouses.AddRange(productWarehouses);
            context.SaveChanges();
        }
    }
}
```

---

## Patterns et Conventions

### Accès Base de Données

**❌ À ÉVITER - Static Shared.db**
```csharp
// Problème : context long-lived, tracking issues
var customers = Shared.db.Customers.ToList();
```

**✅ RECOMMANDÉ - Using block**
```csharp
using (var db = new AppDbContext())
{
    var customers = db.Customers
        .Include(c => c.TierLevel)
        .Where(c => c.Status == "Active")
        .ToList();
}
```

### Multilingue (i18n)

```csharp
// Chaque formulaire a 3 fichiers ressources:
// - Form.resx (anglais)
// - Form.ar.resx (arabe)
// - Form.fr.resx (français)

// Utilisation
string lang = Properties.Settings.Default.Lang;

string message;
if (lang == "en")
    message = "Customer added successfully";
else if (lang == "fr")
    message = "Client ajouté avec succès";
else
    message = "تمت إضافة العميل بنجاح";

XtraMessageBox.Show(message);
```

### Support RTL (Arabe)

```csharp
public void toRtl()
{
    string lang = Properties.Settings.Default.Lang;
    
    if (lang == "ar")
    {
        CustomArabicFont.LoadCustomFont();
        ApplyCustomArabicFontToControls();
        
        this.RightToLeft = RightToLeft.Yes;
        this.RightToLeftLayout = true;
        
        foreach (Control control in this.Controls)
        {
            control.RightToLeft = RightToLeft.Yes;
        }
    }
}
```

### Gestion Images

```csharp
// Sauvegarder image produit
private void btnSavePicture_Click(object sender, EventArgs e)
{
    using (OpenFileDialog ofd = new())
    {
        ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
        
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            Image image = Image.FromFile(ofd.FileName);
            
            // Redimensionner
            Image resized = Helper.ResizeImage(image, 800, 600);
            
            // Convertir en byte[]
            using (MemoryStream ms = new())
            {
                resized.Save(ms, ImageFormat.Jpeg);
                product.Image = ms.ToArray();
            }
        }
    }
}

// Charger image
if (product.Image != null)
{
    using (MemoryStream ms = new(product.Image))
    {
        pictureEdit.Image = Image.FromStream(ms);
    }
}
```

---

## Guide de Développement

### Ajouter un Nouveau Module

**1. Créer le modèle**
```csharp
// Models/MyEntity.cs
[Table("MyEntity")]
public partial class MyEntity
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(250)]
    public string Name { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }
}
```

**2. Ajouter au DbContext**
```csharp
// Models/AppDbContext.cs
public virtual DbSet<MyEntity> MyEntities { get; set; }
```

**3. Créer migration**
```bash
cd Pos/Pos
dotnet ef migrations add AddMyEntity
dotnet ef database update
```

**4. Créer formulaires**
```
Forms/MyModule/
├── MyEntities.cs              # Liste (grid)
├── MyEntities.Designer.cs
├── AddEditMyEntity.cs         # CRUD
└── AddEditMyEntity.Designer.cs
```

**5. Ajouter permissions**
```csharp
// DatabaseSeeder.cs
new Permission { Name = "List MyEntities" },
new Permission { Name = "Add MyEntity" },
new Permission { Name = "Edit MyEntity" },
new Permission { Name = "Delete MyEntity" },
```

**6. Ajouter au menu**
```csharp
// MainFrm.cs
private void btnMyEntities_ItemClick(object sender, ItemClickEventArgs e)
{
    if (Permission.HasPermission("List MyEntities"))
    {
        openMdiChildForm(typeof(MyModule.MyEntities));
    }
}
```

### Déboguer

```csharp
// Activer logs SQL
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer("...")
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging();
}
```

### Tests Manuels

```csharp
// Créer données de test
using (var db = new AppDbContext())
{
    var customers = Enumerable.Range(1, 100).Select(i => new Customer
    {
        FirstName = $"Customer{i}",
        Email = $"customer{i}@test.com",
        CreatedAt = DateTime.Now
    });
    
    db.Customers.AddRange(customers);
    db.SaveChanges();
}
```

### Performance

```csharp
// ✅ Charger avec Include
var sales = db.Sales
    .Include(s => s.Customer)
    .Include(s => s.SaleDetails)
        .ThenInclude(sd => sd.Product)
    .ToList();

// ✅ Pagination
var customers = db.Customers
    .OrderBy(c => c.Id)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();

// ✅ AsNoTracking pour lecture seule
var products = db.Products
    .AsNoTracking()
    .Where(p => p.Status == "Active")
    .ToList();
```

---

## Raccourcis et Astuces

### Commandes Fréquentes

```bash
# Build
dotnet build Pos/Pos.sln

# Run
cd Pos/Pos && dotnet run

# Migration
dotnet ef migrations add MyMigration
dotnet ef database update

# Reset DB
dotnet ef database drop
dotnet ef database update
```

### Snippets Utiles

```csharp
// Charger lookup
cbxCategory.Properties.DataSource = db.Categories.ToList();
cbxCategory.Properties.DisplayMember = "Name";
cbxCategory.Properties.ValueMember = "Id";

// Exporter grid
GridExporter.Export(gridControl, "xlsx");

// Imprimer
var report = new CustomerReport();
report.ShowPreviewDialog();
```

---

**Version**: 1.0  
**Dernière mise à jour**: 2026-05-31  
**Auteur**: Documentation générée pour Ezzipos POS
