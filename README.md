# 🏪 Ezzipos - Point of Sale & Stock Management System

**Comprehensive POS and ERP system for retail businesses**

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![DevExpress](https://img.shields.io/badge/DevExpress-23.2.3-orange)](https://www.devexpress.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Express-red)](https://www.microsoft.com/sql-server)
[![Security Audit](https://img.shields.io/badge/Security%20Audit-81%25-green)](./RAPPORT_SECURITE_2026-06-02.md)

## 📋 Features

### Core Modules
- **Point of Sale (POS)** - Full-featured sales interface with barcode scanning
- **Inventory Management** - Multi-warehouse stock tracking with adjustments and transfers
- **Purchase Management** - Purchase orders, receiving, and supplier management
- **Customer Management** - Customer profiles with loyalty program integration
- **Employee Management (HRM)** - Payroll, attendance, evaluations, and skills tracking
- **Reporting** - Comprehensive sales, inventory, and financial reports

### Advanced Features
- **Loyalty Program (CardFidelity)** - Multi-tier rewards system with points and redemptions
- **Multi-Location** - Support for multiple business locations
- **Multi-Warehouse** - Stock tracking across multiple warehouses
- **Price Groups** - Customer-specific pricing tiers
- **Promotions** - Time-based discounts and special offers
- **Cash Register Management** - Open/close with reconciliation
- **Multi-Language** - English, French, Arabic (with RTL support)

## 🚀 Quick Start

### Prerequisites
- Windows 10/11
- .NET 8.0 SDK
- SQL Server Express (or higher)
- Visual Studio 2022 (recommended) or VS Code

### Installation

1. **Clone the repository:**
```bash
git clone https://github.com/your-org/ezzipos.git
cd ezzipos
```

2. **Restore dependencies:**
```bash
cd Pos/Pos
dotnet restore
```

3. **Configure database:**
   - Connection string is in `Models/AppDbContext.cs:241`
   - Default: `Server=.\sqlexpress;Database=Pos;Trusted_Connection=True;TrustServerCertificate=true;`

4. **Build and run:**
```bash
dotnet build
dotnet run
```

5. **First login:**
   - Database is automatically seeded on first run
   - Default admin credentials are created by `DatabaseSeeder.Seed()`

## 🏗️ Architecture

### Technology Stack
- **Framework:** .NET 8.0 Windows Forms
- **UI Library:** DevExpress WinForms 23.2.3
- **ORM:** Entity Framework Core 9.0.0
- **Database:** SQL Server Express
- **Security:** BCrypt password hashing, HMAC-SHA256 session tokens

### Project Structure
```
Pos/Pos/
├── Forms/              # UI Forms (66+ forms)
│   ├── Customer/       # Customer management
│   ├── Employee/       # HR management
│   ├── Product/        # Product management
│   ├── Sales/          # Sales management
│   ├── Purchase/       # Purchase management
│   ├── LoyaltyForms/   # Loyalty program
│   ├── Reports/        # Reporting
│   └── Screen/         # Main screens (POS, etc.)
├── Models/             # EF Core entities (116+ models)
├── Function/           # Business logic & utilities
│   ├── SessionManager.cs         # User session management
│   ├── BruteForceProtection.cs   # Login security
│   ├── ExportManager.cs          # Secure exports
│   ├── SaleValidator.cs          # Sales validation
│   ├── MoneyHelper.cs            # Decimal calculations
│   └── ...
├── Migrations/         # EF Core migrations
└── Program.cs          # Application entry point
```

## 🔒 Security

This project has undergone a comprehensive security audit with **26/32 vulnerabilities fixed (81%)**.

### Security Features Implemented

#### Phase 1 - Authentication (9/10 ✅)
- ✅ Secure session management with HMAC-SHA256 tokens
- ✅ Brute force protection (5 attempts, timing attack prevention)
- ✅ BCrypt password hashing
- ✅ SQL injection prevention
- ✅ Privilege escalation protection
- ✅ Session expiry (24h default)

#### Phase 2 - Transactions (9/10 ✅)
- ✅ Consistent decimal rounding (2 places, away from zero)
- ✅ Business limits validation (MAX_SALE_AMOUNT: 100M DA)
- ✅ Sale validation (15+ rules)
- ✅ Return validation (amounts, timing, daily limits)
- ✅ Tax validation (19% TVA Algeria)
- ✅ Audit logging
- ✅ Cash discrepancy tracking
- ✅ Soft delete system (archive)

#### Phase 3 - Data Access (8/12 ✅)
- ✅ File upload validation with magic bytes
- ✅ Path traversal protection
- ✅ Excel formula injection prevention
- ✅ CSV injection protection
- ✅ Export limits (50k rows)
- ✅ Permission-controlled exports
- ✅ Input sanitization utilities

### Security Reports
- [Phase 1 - Authentication](./RAPPORT_SECURITE_2026-06-02.md)
- [Phase 2 - Transactions](./RAPPORT_SECURITE_PHASE2_TRANSACTIONS.md)
- [Phase 3A - Data Access](./RAPPORT_SECURITE_PHASE3_ACCES_DONNEES.md)
- [Phase 3B - Export Security](./RAPPORT_PHASE3B_EXPORTS_SECURISES.md)

## 📚 Documentation

- **[CLAUDE.md](./CLAUDE.md)** - Project architecture and conventions
- **[DOCUMENTATION_DEVELOPPEUR.md](./DOCUMENTATION_DEVELOPPEUR.md)** - Developer guide
- **Security Reports** - Detailed security audit results

## 🛠️ Development

### Building
```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release
```

### Database Migrations
```bash
# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Revert migration
dotnet ef database update PreviousMigrationName
```

### Running Tests
```bash
dotnet test
```

## 🌍 Localization

The application supports three languages:
- **English** (en)
- **French** (fr)
- **Arabic** (ar) with RTL support

Resource files are located alongside each form:
- `FormName.resx` (English)
- `FormName.fr.resx` (French)
- `FormName.ar.resx` (Arabic)

## 📊 Database Schema

- **116+ entities** covering all business operations
- **Soft deletes** via `IsArchived` pattern (products)
- **Audit trail** for sensitive operations
- **Multi-tenancy** via `BusinessLocation`

Key tables:
- `Product`, `Category`, `Brand`, `Unit`
- `Sale`, `SaleDetail`, `SalePayment`
- `Purchase`, `PurchaseDetail`
- `Customer`, `Employee`
- `Stock`, `StockAdjustment`, `StockTransfer`
- `CardFidelity`, `PointsTransaction`, `Reward`

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/my-feature`
3. Commit your changes: `git commit -m 'feat: add some feature'`
4. Push to the branch: `git push origin feature/my-feature`
5. Open a Pull Request

### Commit Message Convention
```
feat: Add new feature
fix: Fix bug
docs: Update documentation
refactor: Refactor code
test: Add tests
chore: Maintenance tasks
```

## 📝 License

[Add your license here]

## 👥 Authors

- **Ezzipos Dev Team** - Initial work and security audit

## 🐛 Known Issues

See [Issues](./RAPPORT_SECURITE_2026-06-02.md#vulnerabilities-remaining) for the list of remaining security vulnerabilities and planned improvements.

## 📞 Support

For support and questions:
- Create an issue in the GitHub repository
- Contact: dev@ezzipos.local

## 🗓️ Roadmap

### Upcoming (Phase 3C - Quick Wins)
- [ ] Extend ExportManager to all 119 remaining exports
- [ ] Integrate InputSanitizer across all forms
- [ ] Add SaveChanges validation override
- [ ] Implement rate limiting for login

### Future (Phase 4)
- [ ] RGPD compliance (encryption, right to erasure)
- [ ] Data access logging
- [ ] Sensitive data encryption at rest

### Future (Phase 5)
- [ ] Performance optimizations (caching, indexes)
- [ ] Pagination for large datasets
- [ ] Refactor static `Shared.db` context

---

**Version:** 1.0.0 (Post Security Audit Phase 3B)  
**Last Updated:** 2026-06-02  
**Security Score:** 81% (26/32 vulnerabilities fixed)
