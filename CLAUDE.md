# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Ezzipos is a comprehensive Point of Sale (POS) and Stock Management System built with C# .NET 8.0 Windows Forms and DevExpress UI controls. It's a full-featured ERP system covering sales, inventory, purchases, customers, employees (HRM), and loyalty programs.

## Build and Run Commands

### Building the Project
```bash
# Build the solution
dotnet build Pos/Pos.sln

# Build in Release mode
dotnet build Pos/Pos.sln -c Release

# Restore NuGet packages
dotnet restore Pos/Pos.sln
```

### Running the Application
```bash
# Run from the project directory
cd Pos/Pos
dotnet run

# Or use Visual Studio to run the project
```

### Database Setup
The application uses SQL Server with the connection string hardcoded in `AppDbContext.cs:241`:
```
Server=.\sqlexpress;Database=Pos;Trusted_Connection=True;TrustServerCertificate=true;
```

Database initialization happens automatically on first run via `DatabaseSeeder.Seed()` in `Program.cs:38-44`.

## Architecture

### Core Patterns

**Three-Layer Structure (with tight coupling)**:
- **Models** (`Pos/Models/`): 116+ EF Core entities with navigation properties
- **Forms** (`Pos/Forms/`): 66+ Windows Forms for UI (contains most business logic)
- **Function** (`Pos/Function/`): Shared utilities (Helper, Permission, DatabaseSeeder, etc.)

**Important**: Business logic is NOT separated into a service layer. Most logic lives directly in Forms, which access `AppDbContext` directly or via the static `Shared.db` instance.

### Key Architectural Components

#### Database Context
- **Primary Context**: `AppDbContext` in `Pos/Models/AppDbContext.cs`
- **Shared Instance**: `Shared.db` is a static singleton instance used throughout the application (`Pos/Function/Shared.cs:14`)
- **Warning**: The static `Shared.db` is long-lived and shared across forms. Be aware of stale entity tracking issues.

#### Permission System
- Role-based access control via `Permission.HasPermission(string permissionName)` in `Pos/Function/Permission.cs:16`
- Admin users bypass all permission checks (`isAdmin == "Admin"`)
- User context stored in `Properties.Settings.Default` (userId, UserRoleID, isAdmin)
- Permissions checked before opening forms/executing actions in `MainFrm.cs`

#### UI Architecture
- **Main Form**: `MainFrm.cs` is a DevExpress RibbonForm with MDI (Multi-Document Interface)
- **Overlay Pattern**: Modal forms use `OverlayForm` to create semi-transparent backgrounds (see usage in `MainFrm.cs:168-176`)
- **Form Management**: `openMdiChildForm(Type formType)` prevents duplicate MDI children (see `MainFrm.cs:214-222`)

#### Multilingual Support
- Languages: English, French, Arabic (with RTL support)
- Language stored in `Properties.Settings.Default.Lang`
- Each form has `.resx`, `.ar.resx`, `.fr.resx` resource files
- RTL handling in `MainFrm.toRtl()` method applies custom Arabic fonts and RTL layout

### Data Flow Example (Sale Creation)

1. User opens POS screen via `Forms/Screen/Pos.cs`
2. POS form directly queries `AppDbContext` or `Shared.db` for products, customers, warehouses
3. Cart items stored in a local `DataTable` (in-memory, not DB)
4. On checkout, creates `Sale` entity with related `SaleDetail` and `SalePayment` entities
5. Saves to DB via `context.SaveChanges()`
6. Loyalty points processed via `Helper.ProcessLoyaltyPoints(saleId)` in `Function/Helper.cs:91`
7. Print receipt if printer configured

## Important Patterns and Conventions

### Entity Relationships
- All models use EF Core conventions with `[ForeignKey]` and `[InverseProperty]` attributes
- Soft deletes are NOT used - deletions are hard deletes
- Timestamps: `CreatedAt` and `UpdatedAt` (DateTime) on most entities

### Form Lifecycle
```csharp
// Typical form pattern in MainFrm
private void btnAddCustomer_ItemClick(object sender, ItemClickEventArgs e)
{
    if (Function.Permission.HasPermission("Add Customer"))
    {
        OverlayForm overlay = new OverlayForm(this);
        overlay.Show();

        Customer.AddEditCustomer customer = new Customer.AddEditCustomer();
        customer.FormClosed += (s, args) => overlay.Close();
        customer.Show();
        customer.TopMost = true;
    }
}
```

### Database Access Patterns
```csharp
// Pattern 1: Using static Shared.db (most common in older code)
var products = Shared.db.Products.Include(p => p.Category).ToList();

// Pattern 2: Using local context (preferred for new code)
using (var context = new AppDbContext())
{
    var products = context.Products.Include(p => p.Category).ToList();
}
```

**Warning**: Mixing these patterns can cause tracking conflicts. The static `Shared.db` instance persists throughout application lifetime.

### Permission Checking
Always check permissions before form operations:
```csharp
if (Function.Permission.HasPermission("Permission Name"))
{
    // Execute action
}
// Permission.HasPermission() shows AccessDenied dialog if unauthorized
```

Use `HasPermissionWithoutAlert()` when you need silent checks without UI feedback.

## Key Modules and Their Responsibilities

### Product Management
- **Products**: Main product CRUD with variants, barcodes, prices
- **Categories**: Product categorization
- **Brands**: Product brands
- **Units**: Measurement units (pieces, kg, liter, etc.)
- **Warehouses**: Multi-warehouse stock tracking
- **Price Groups**: Customer-specific pricing tiers
- **Promotions**: Time-based discounts and special offers

### Sales & POS
- **POS Screen** (`Forms/Screen/Pos.cs`): Full-featured point of sale interface
- **Sales**: Sale management and history
- **Returns**: Customer returns processing
- **Register**: Cash register open/close with cash reconciliation

### Inventory
- **Stocks**: Real-time stock levels across warehouses
- **Adjustments**: Manual stock corrections (damage, theft, count corrections)
- **Transfers**: Inter-warehouse stock movements
- **Inventories**: Physical inventory counts

### Purchasing
- **Purchases**: Purchase orders and receiving
- **Purchase Returns**: Return to supplier functionality
- **Suppliers**: Supplier management

### Customer Loyalty (CardFidelity)
- **Tiers**: Customer tier levels (Bronze, Silver, Gold, etc.)
- **Rewards**: Redeemable rewards catalog
- **Redemptions**: Reward redemption transactions
- **PointsTransaction**: Loyalty points earn/burn history
- **ActivityLog**: Customer activity tracking

### Human Resources (Employee)
- **Employees**: Employee profiles
- **Departments**: Organizational units
- **Positions**: Job positions
- **Attendances**: Time and attendance tracking
- **Payrolls**: Payroll processing
- **Evaluations**: Performance evaluations
- **Skills**: Employee skills tracking

## Critical Files

### Entry Point
- `Program.cs:31` - Application entry, DB seeding, language initialization, login flow

### Core Infrastructure
- `Models/AppDbContext.cs:7` - EF Core DbContext with 80+ DbSets
- `Function/Shared.cs:14` - Static shared DB context instance
- `Function/Permission.cs:16` - Permission checking system
- `Function/Helper.cs` - Utilities (image resize, loyalty processing, grid styling)
- `Function/DatabaseSeeder.cs` - Initial data seeding

### Main UI
- `Forms/MainFrm.cs:28` - Main application window (2000+ lines, ribbon menu, MDI host)
- `Forms/Screen/Pos.cs:48` - Point of Sale screen

## Working with DevExpress Controls

This project uses DevExpress WinForms controls extensively:
- `XtraForm` - Base form class
- `RibbonForm` - Main window with ribbon menu
- `XtraGrid` / `GridView` - Data grids
- `LookUpEdit` - Dropdown lookups
- `RepositoryItem*` - In-grid editors

DevExpress designer files (`.Designer.cs`) are auto-generated. Modify logic in the main `.cs` files.

## Database Migrations

Migrations are managed via EF Core:
```bash
# Create a new migration
cd Pos/Pos
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Revert to previous migration
dotnet ef database update PreviousMigrationName
```

Initial migration: `Migrations/20240518183558_InitialCreate.cs`

## Session and User Context

User session is stored in `Properties.Settings.Default`:
- `UserSession` - Session token/identifier
- `userId` - Current user ID
- `UserRoleID` - Current user's role
- `isAdmin` - "Admin" or empty
- `CurrentUserFullName` - Display name
- `BusinessLocation` - Current business location ID
- `Lang` - Current language (en/fr/ar)

Check `Program.cs:73-77` for session validation logic.

## Register (Cash Register) System

- Each business location can have a register open at a time
- Register must be opened before making sales (`Helper.IsRegisterOpen()`)
- Register tracks opening balance, cash movements, closing balance
- Closing register creates a `RegisterRecord` with reconciliation data

## Common Gotchas

1. **Static DbContext**: `Shared.db` is static and long-lived. Avoid keeping entity references across operations to prevent tracking conflicts.

2. **Permission Dialogs**: `Permission.HasPermission()` shows an `AccessDenied` dialog on failure. Use `HasPermissionWithoutAlert()` for silent checks.

3. **MDI Form Duplication**: `MainFrm.openMdiChildForm()` prevents opening the same form type twice. If you need multiple instances, use `Show()` directly.

4. **DevExpress Licensing**: Requires valid DevExpress license. The project references DevExpress 23.2.3.

5. **Hardcoded Connection String**: DB connection is hardcoded in `AppDbContext.cs:241`. Change for different environments.

6. **Image Storage**: Product images stored as `byte[]` in the database. Use `Helper.ResizeImage()` to optimize before saving.

7. **Decimal Precision**: All monetary values use `decimal(18, 2)`.

8. **DateTime**: Most timestamps use nullable `DateTime?` with UTC recommended but not enforced.

9. **Translation Resources**: When adding new UI text, update all three resource files (.resx, .ar.resx, .fr.resx).

10. **Business Location Context**: Most operations are scoped to `Properties.Settings.Default.BusinessLocation`. Filter queries accordingly.

## Code Style Observations

- Forms contain business logic (no service layer separation)
- Minimal async/await usage (mostly synchronous DB operations)
- Direct EF Core queries in UI code
- Limited error handling in many areas
- Sparse code comments
- Some code duplication (especially in MainFrm.cs event handlers)

When making changes, match the existing style for consistency, but consider suggesting architectural improvements for major refactoring.
