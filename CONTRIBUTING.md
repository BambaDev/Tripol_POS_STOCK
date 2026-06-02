# 🤝 Contributing to Ezzipos

Thank you for considering contributing to Ezzipos! This document provides guidelines and standards for contributing to the project.

## 📋 Table of Contents
- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Security Guidelines](#security-guidelines)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)

---

## 📜 Code of Conduct

- Be respectful and professional
- Focus on constructive feedback
- Help create a welcoming environment for all contributors

---

## 🚀 Getting Started

### Prerequisites
1. Install .NET 8.0 SDK
2. Install Visual Studio 2022 or VS Code
3. Install SQL Server Express
4. Read [CLAUDE.md](./CLAUDE.md) for project architecture

### Setup
```bash
# Clone repository
git clone https://github.com/your-org/ezzipos.git
cd ezzipos

# Restore dependencies
cd Pos/Pos
dotnet restore

# Build
dotnet build

# Run
dotnet run
```

---

## 🔄 Development Workflow

### Branch Strategy
```
main                    # Production-ready code
  └── dev/phase-name    # Development branches
       └── feature/xxx  # Feature branches
```

### Creating a Feature Branch
```bash
# Update main
git checkout main
git pull origin main

# Create feature branch
git checkout -b feature/add-inventory-report

# Work on your feature
# ... make changes ...

# Commit with conventional commits
git add .
git commit -m "feat: add inventory report with filters"

# Push
git push origin feature/add-inventory-report
```

---

## 💻 Coding Standards

### C# Style Guide

#### Naming Conventions
```csharp
// Classes, Methods, Properties: PascalCase
public class ProductManager
{
    public string ProductName { get; set; }
    
    public void SaveProduct() { }
}

// Private fields: _camelCase
private readonly AppDbContext _context;
private int _userId;

// Local variables: camelCase
int productCount = 10;
string customerName = "John";

// Constants: UPPER_SNAKE_CASE or PascalCase
public const int MAX_ITEMS = 100;
public const decimal TAX_RATE = 19.0m;
```

#### Code Organization
```csharp
// Order:
// 1. Fields (private)
// 2. Properties (public)
// 3. Constructor
// 4. Public methods
// 5. Private methods

public class Example
{
    // 1. Fields
    private readonly AppDbContext _context;
    private int _counter;
    
    // 2. Properties
    public string Name { get; set; }
    public int Count { get; set; }
    
    // 3. Constructor
    public Example(AppDbContext context)
    {
        _context = context;
    }
    
    // 4. Public methods
    public void DoSomething()
    {
        HelperMethod();
    }
    
    // 5. Private methods
    private void HelperMethod()
    {
        // ...
    }
}
```

#### Comments
```csharp
// Use XML comments for public APIs
/// <summary>
/// Validates a sale before saving to database
/// </summary>
/// <param name="sale">The sale to validate</param>
/// <returns>Validation result with error message if invalid</returns>
public static (bool isValid, string errorMessage) ValidateSale(Sale sale)

// Use inline comments only when necessary (non-obvious logic)
// Calculate tax based on Algeria TVA rate (19%)
decimal taxAmount = netAmount * 0.19m;
```

### Database Conventions

#### Entity Models
```csharp
[Table("Product")]
public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string ProductName { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    
    // Navigation properties
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
    
    public int CategoryId { get; set; }
}
```

#### Migrations
```csharp
// Name: {Timestamp}_{DescriptiveName}.cs
// Example: 20260602000000_AddProductArchiveFields.cs

public partial class AddProductArchiveFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "IsArchived",
            table: "Product",
            nullable: false,
            defaultValue: false);
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsArchived",
            table: "Product");
    }
}
```

---

## 🔒 Security Guidelines

### CRITICAL Rules

#### 1. Never commit sensitive data
```bash
# ❌ NEVER do this
git add appsettings.json  # Contains connection strings
git add secrets.json      # Contains API keys

# ✅ Use .gitignore
appsettings.Development.json
*.pfx
*.key
secrets.json
```

#### 2. Always validate user input
```csharp
// ❌ NEVER directly use user input
var query = $"SELECT * FROM Users WHERE Name = '{txtName.Text}'";

// ✅ Use parameterized queries or validators
var validation = InputSanitizer.ValidateName(txtName.Text);
if (!validation.isValid)
{
    ShowError(validation.errorMessage);
    return;
}
```

#### 3. Always check permissions
```csharp
// ❌ NEVER skip permission checks
private void btnDelete_Click(object sender, EventArgs e)
{
    DeleteProduct(productId);
}

// ✅ Check permission first
private void btnDelete_Click(object sender, EventArgs e)
{
    if (!Permission.HasPermission("Delete Product"))
        return;
    
    DeleteProduct(productId);
}
```

#### 4. Use secure exports
```csharp
// ❌ NEVER use direct export without security
private void btnExport_Click(object sender, EventArgs e)
{
    gridView.ExportToXlsx(fileName);
}

// ✅ Use ExportManager
private void btnExport_Click(object sender, EventArgs e)
{
    ExportManager.SecureExportToExcel(
        gridView,
        "Products",
        userId,
        "Export Products",
        context
    );
}
```

#### 5. Log sensitive operations
```csharp
// When modifying sensitive data
AuditLogger.LogAction(
    context,
    "Delete Customer",
    "Customer",
    customerId,
    $"Deleted customer: {customer.FullName}",
    userId
);
context.SaveChanges();
```

### Security Checklist

Before submitting a PR, verify:
- [ ] No hardcoded passwords or API keys
- [ ] All user inputs validated
- [ ] SQL injection prevention (parameterized queries)
- [ ] XSS prevention (input sanitization)
- [ ] Permission checks for sensitive operations
- [ ] Audit logging for critical actions
- [ ] No sensitive data in logs
- [ ] Secure file uploads (magic bytes validation)
- [ ] Path traversal protection
- [ ] Export security (formula injection prevention)

---

## 📝 Commit Guidelines

### Conventional Commits Format
```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types
- `feat`: New feature
- `fix`: Bug fix
- `docs`: Documentation changes
- `style`: Code style changes (formatting)
- `refactor`: Code refactoring
- `test`: Adding/updating tests
- `chore`: Maintenance tasks
- `security`: Security improvements

### Examples

**Feature:**
```
feat(export): add secure Excel export for sales

- Implement permission check
- Add formula injection prevention
- Add audit logging
- Limit to 50k rows

Closes #123
```

**Bug fix:**
```
fix(sale): prevent negative discount amounts

- Add validation in SaleValidator
- Display error message to user
- Add unit test

Fixes #456
```

**Security:**
```
security(auth): implement brute force protection

- Add LoginAttempt tracking
- Lock account after 5 failed attempts
- Add timing attack prevention
- Log all login attempts

BREAKING CHANGE: Users locked after 5 attempts must contact admin
```

---

## 🔍 Pull Request Process

### Before Creating PR

1. **Update your branch:**
```bash
git checkout main
git pull origin main
git checkout feature/my-feature
git rebase main
```

2. **Run tests:**
```bash
dotnet test
```

3. **Build successfully:**
```bash
dotnet build -c Release
```

4. **Check for security issues:**
   - Review your changes for security vulnerabilities
   - Run security checklist
   - Test permission checks

### Creating PR

1. **Push your branch:**
```bash
git push origin feature/my-feature
```

2. **Create PR with template:**

**Title:** `feat: Add inventory report with filters`

**Description:**
```markdown
## Summary
Adds a new inventory report with filtering by category, warehouse, and date range.

## Changes
- Created `Forms/Reports/InventoryReport.cs`
- Added filter controls (category, warehouse, date range)
- Implemented secure export with `ExportManager`
- Added permission check "View Inventory Report"

## Testing
- [x] Manual testing with various filters
- [x] Export to Excel/CSV/PDF works
- [x] Permission check verified
- [x] Build succeeds

## Screenshots
[Add screenshots if UI changes]

## Checklist
- [x] Code follows style guide
- [x] Security checklist completed
- [x] Tests pass
- [x] Documentation updated
- [x] Commit messages follow convention

## Related Issues
Closes #123
```

### PR Review Process

1. **Automated checks:** Build and tests must pass
2. **Code review:** At least one approval required
3. **Security review:** For security-sensitive changes
4. **Merge:** Squash and merge into main

---

## 🧪 Testing Guidelines

### Unit Tests
```csharp
[Fact]
public void ValidateSale_NegativeAmount_ReturnsFalse()
{
    // Arrange
    var sale = new Sale { NetTotalAmount = -100m };
    
    // Act
    var result = SaleValidator.ValidateSale(sale);
    
    // Assert
    Assert.False(result.isValid);
    Assert.Contains("négatif", result.errorMessage);
}
```

### Integration Tests
```csharp
[Fact]
public void ExportManager_WithoutPermission_ShowsAccessDenied()
{
    // Arrange
    var userId = CreateUserWithoutPermission();
    
    // Act
    ExportManager.SecureExportToExcel(gridView, "Test", userId, "Export Test", context);
    
    // Assert
    // Verify AccessDenied dialog shown
}
```

---

## 📚 Additional Resources

- [CLAUDE.md](./CLAUDE.md) - Architecture guide
- [Security Reports](./RAPPORT_SECURITE_2026-06-02.md)
- [Developer Documentation](./DOCUMENTATION_DEVELOPPEUR.md)

---

## ❓ Questions?

If you have questions:
1. Check existing documentation
2. Search closed issues
3. Create a new issue with the `question` label
4. Contact: dev@ezzipos.local

---

**Thank you for contributing to Ezzipos!** 🎉
