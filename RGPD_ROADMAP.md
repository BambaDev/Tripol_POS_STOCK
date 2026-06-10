# 🔐 ROADMAP RGPD - CONFORMITÉ COMPLÈTE

**Date Début:** 2026-06-10  
**Durée Estimée:** 3-4 semaines  
**État Actuel:** 30% conforme (authentification sécurisée uniquement)  
**Objectif:** 95% conformité RGPD

---

## 📊 INVENTAIRE PII (Personal Identifiable Information)

### **HIGH SENSITIVITY (12 entités)**
| Entité | Champs Critiques | Protection Actuelle | Risque RGPD |
|--------|------------------|---------------------|-------------|
| **Employee** | FirstName, LastName, Email, Phone, Address, DateOfBirth, BloodGroup, NoCard, NoPass, Image | PLAINTEXT | 🔴 CRITIQUE |
| **EmployeeHealth** | HealthCondition, DateDiagnosed, Notes | PLAINTEXT | 🔴 CRITIQUE (Article 9) |
| **User** | Email, Phone, Password, PINs, Image | Password: HASHED, Reste: PLAINTEXT | 🟠 ÉLEVÉ |
| **Customer** | FirstName, LastName, Email, Phone, Address, Image | PLAINTEXT | 🟠 ÉLEVÉ |
| **Supplier** | FirstName, LastName, Email, Image | PLAINTEXT | 🟡 MOYEN |
| **EmployeeDependent** | Name, DateOfBirth, Relationship | PLAINTEXT | 🟡 MOYEN |
| **Payroll** | GrossPay, NetPay, Deductions | PLAINTEXT | 🟡 MOYEN |
| **Company** | Email, Tel, Rib, Compte, IdFiscal | PLAINTEXT | 🟡 MOYEN |
| **AuditTrail** | OldValues, NewValues (contient toutes PII) | PLAINTEXT | 🔴 CRITIQUE |
| **RepairInvoice** | Address, PasswordPatternLock | PLAINTEXT | 🟡 MOYEN |
| **SalePayment** | Amount, Paid, Due (lié Customer) | PLAINTEXT | 🟡 MOYEN |
| **BankTransaction** | Total, ReferenceNo | PLAINTEXT | 🟡 MOYEN |

### **Données Sensibles Article 9 GDPR (Protection Renforcée)**
```
- Health data: EmployeeHealth.HealthCondition, EmployeeHealth.Notes
- Biometric data: Employee.Image, User.Image, Customer.Image, Employee.BloodGroup
- Identity documents: Employee.NoCard, Employee.NoPass
```

---

## 🎯 PHASES D'IMPLÉMENTATION

### **PHASE 4A: CLASSIFICATION & INFRASTRUCTURE (Semaine 1)**
**Durée:** 3-4 jours  
**Objectif:** Créer fondations pour encryption et classification

#### **4A.1 - Data Classification Attributes**
```csharp
[AttributeUsage(AttributeTargets.Property)]
public class SensitiveDataAttribute : Attribute
{
    public SensitivityLevel Level { get; set; }
    public string Category { get; set; } // "PII", "Health", "Financial", "Biometric"
    public bool RequiresEncryption { get; set; }
    public bool LogAccessAttempts { get; set; }
}

public enum SensitivityLevel
{
    Public = 0,      // Pas de restriction
    Internal = 1,    // Données internes
    Confidential = 2,// PII standard
    Restricted = 3,  // Données sensibles Article 9
    Classified = 4   // Données hautement sensibles
}
```

**Fichier:** `Pos/Pos/Function/GdprAttributes.cs`

#### **4A.2 - Field-Level Encryption Helper**
```csharp
public static class FieldEncryption
{
    private static readonly byte[] EncryptionKey = DeriveKeyFromMasterPassword();
    
    // AES-256-GCM pour encryption at-rest
    public static string Encrypt(string plaintext);
    public static string Decrypt(string ciphertext);
    
    // Tokenization pour données très sensibles (carte bancaire, NoPass)
    public static string Tokenize(string sensitiveData);
    public static string Detokenize(string token);
    
    // Hashing irréversible pour données ne nécessitant pas de décryption
    public static string HashIrreversible(string data);
}
```

**Fichiers:**
- `Pos/Pos/Function/FieldEncryption.cs`
- `Pos/Pos/Function/KeyManagement.cs` (gestion clés encryption)

#### **4A.3 - GDPR Configuration**
```json
{
  "Gdpr": {
    "EnableFieldEncryption": true,
    "EnableAuditTrailMasking": true,
    "DataRetentionDays": {
      "Customers": 2555,      // 7 ans (légal fiscal)
      "Employees": 1825,      // 5 ans après départ
      "Transactions": 3650,   // 10 ans (comptabilité)
      "AuditTrail": 1095      // 3 ans
    },
    "AnonymizationRules": {
      "ReplaceWithPlaceholder": ["FirstName", "LastName", "Email"],
      "Hash": ["Phone", "Address"],
      "Delete": ["Image", "HealthCondition"]
    }
  }
}
```

**Fichier:** `Pos/Pos/appsettings.gdpr.json`

---

### **PHASE 4B: ENCRYPTION AUTOMATIQUE (Semaine 1-2)**
**Durée:** 5-6 jours  
**Objectif:** Encryption transparente via EF Core ValueConverter

#### **4B.1 - EF Core Value Converters**
```csharp
public class EncryptedStringConverter : ValueConverter<string, string>
{
    public EncryptedStringConverter()
        : base(
            v => FieldEncryption.Encrypt(v),
            v => FieldEncryption.Decrypt(v))
    { }
}

// Dans AppDbContext.OnModelCreating()
modelBuilder.Entity<Employee>()
    .Property(e => e.Email)
    .HasConversion(new EncryptedStringConverter());
```

#### **4B.2 - Annoter Modèles avec [SensitiveData]**
```csharp
public partial class Employee
{
    [SensitiveData(Level = SensitivityLevel.Confidential, Category = "PII", RequiresEncryption = true)]
    public string FirstName { get; set; }
    
    [SensitiveData(Level = SensitivityLevel.Confidential, Category = "PII", RequiresEncryption = true)]
    public string Email { get; set; }
    
    [SensitiveData(Level = SensitivityLevel.Restricted, Category = "Health", RequiresEncryption = true, LogAccessAttempts = true)]
    public string BloodGroup { get; set; }
}
```

#### **4B.3 - Migration Encryption Existante**
```csharp
// Script migration one-time pour encrypter données existantes
public class EncryptExistingPiiMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Backup d'abord !
        migrationBuilder.Sql("BACKUP DATABASE [Pos] TO DISK = 'C:\\Backups\\Pos_PreEncryption.bak'");
        
        // Encrypter données Customer
        migrationBuilder.Sql(@"
            UPDATE Customer 
            SET Email = dbo.fn_EncryptField(Email),
                Phone = dbo.fn_EncryptField(Phone),
                Address = dbo.fn_EncryptField(Address)
            WHERE Email IS NOT NULL OR Phone IS NOT NULL OR Address IS NOT NULL
        ");
        
        // Encrypter Employee...
    }
}
```

**Fichiers:**
- `Pos/Pos/Migrations/YYYYMMDD_EncryptExistingPii.cs`
- `Pos/Pos/Function/EncryptionMigrationHelper.cs`

---

### **PHASE 4C: AUDIT TRAIL MASKING (Semaine 2)**
**Durée:** 2-3 jours  
**Objectif:** Masquer PII dans logs d'audit

#### **4C.1 - PII Masking dans AuditTrail**
```csharp
public static class AuditTrailMasker
{
    public static string MaskPiiInJson(string jsonData, Type entityType)
    {
        var json = JObject.Parse(jsonData);
        
        foreach (var property in entityType.GetProperties())
        {
            var sensitiveAttr = property.GetCustomAttribute<SensitiveDataAttribute>();
            if (sensitiveAttr != null && sensitiveAttr.RequiresEncryption)
            {
                if (json[property.Name] != null)
                {
                    json[property.Name] = MaskValue(json[property.Name].ToString(), sensitiveAttr.Category);
                }
            }
        }
        
        return json.ToString();
    }
    
    private static string MaskValue(string value, string category)
    {
        return category switch
        {
            "PII" => $"{value[0]}***{value[^1]}", // "John" → "J***n"
            "Email" => MaskEmail(value),           // "john@test.com" → "j***@test.com"
            "Phone" => $"***{value[^4..]}",        // "0555123456" → "***3456"
            "Financial" => "***.**",
            "Health" => "[REDACTED]",
            _ => "***"
        };
    }
}
```

#### **4C.2 - Modifier SaveChanges pour Masquage Automatique**
```csharp
// Dans AppDbContext.cs
public override int SaveChanges()
{
    // Existing validation...
    ValidateEntitiesBeforeSave();
    
    // PHASE 4C: Masquer PII dans audit trail
    var auditEntries = ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Modified || e.State == EntityState.Deleted)
        .Select(e => CreateAuditEntry(e))
        .ToList();
    
    foreach (var auditEntry in auditEntries)
    {
        auditEntry.OldValues = AuditTrailMasker.MaskPiiInJson(auditEntry.OldValues, auditEntry.EntityType);
        auditEntry.NewValues = AuditTrailMasker.MaskPiiInJson(auditEntry.NewValues, auditEntry.EntityType);
        AuditTrails.Add(auditEntry);
    }
    
    return base.SaveChanges();
}
```

**Fichiers:**
- `Pos/Pos/Function/AuditTrailMasker.cs`
- Modification: `Pos/Pos/Models/AppDbContext.cs` (SaveChanges override)

---

### **PHASE 4D: DROITS GDPR (Semaine 2-3)**
**Durée:** 4-5 jours  
**Objectif:** Implémenter droits utilisateurs RGPD

#### **4D.1 - Droit d'Accès (Article 15)**
```csharp
public class GdprDataAccessService
{
    /// <summary>
    /// Génère export complet de toutes données personnelles d'un individu
    /// </summary>
    public GdprDataExport GenerateDataExport(int customerId)
    {
        var export = new GdprDataExport
        {
            GeneratedAt = DateTime.UtcNow,
            Subject = GetCustomerInfo(customerId),
            PersonalData = new Dictionary<string, object>
            {
                ["Profile"] = GetCustomerProfile(customerId),
                ["Purchases"] = GetSalesHistory(customerId),
                ["LoyaltyPoints"] = GetLoyaltyData(customerId),
                ["Communications"] = GetEmailHistory(customerId),
                ["AuditTrail"] = GetDataAccessLog(customerId)
            }
        };
        
        return export;
    }
    
    /// <summary>
    /// Exporte en JSON pour portabilité (Article 20)
    /// </summary>
    public byte[] ExportAsJson(GdprDataExport export);
    
    /// <summary>
    /// Exporte en PDF lisible pour l'utilisateur
    /// </summary>
    public byte[] ExportAsPdf(GdprDataExport export);
}
```

#### **4D.2 - Droit à l'Oubli (Article 17)**
```csharp
public class GdprDataErasureService
{
    /// <summary>
    /// Anonymise un customer selon règles RGPD
    /// Ne supprime PAS les transactions (obligation légale 7 ans)
    /// </summary>
    public void AnonymizeCustomer(int customerId, string reason)
    {
        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                var customer = context.Customers.Find(customerId);
                
                // Log demande erasure
                LogErasureRequest(customerId, reason);
                
                // Anonymiser données PII
                customer.FirstName = $"DELETED_{customer.Id}";
                customer.LastName = "USER";
                customer.Email = $"deleted_{customer.Id}@anonymized.local";
                customer.Phone = null;
                customer.Address = null;
                customer.Image = null;
                customer.IsAnonymized = true;
                customer.AnonymizedAt = DateTime.UtcNow;
                
                // Garder transactions pour comptabilité (requis légalement)
                // Mais masquer référence client dans affichage
                
                context.SaveChanges();
                transaction.Commit();
                
                // Audit
                AuditTrail.Log("GDPR_ERASURE", $"Customer {customerId} anonymized");
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
    
    /// <summary>
    /// Suppression complète Employee (après période rétention)
    /// </summary>
    public void PurgeEmployeeData(int employeeId);
}
```

#### **4D.3 - Droit de Rectification (Article 16)**
```csharp
public class GdprDataRectificationService
{
    /// <summary>
    /// Permet au customer de corriger ses données
    /// </summary>
    public void UpdatePersonalData(int customerId, CustomerUpdateRequest request)
    {
        // Validation input (Phase 3E déjà fait)
        
        // Log rectification request
        AuditTrail.Log("GDPR_RECTIFICATION", $"Customer {customerId} updated profile");
        
        // Apply changes with audit
        var customer = context.Customers.Find(customerId);
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        // ...
        
        context.SaveChanges();
    }
}
```

#### **4D.4 - Droit d'Opposition (Article 21)**
```csharp
public class GdprConsentService
{
    /// <summary>
    /// Gère consentements marketing/communications
    /// </summary>
    public void UpdateConsent(int customerId, ConsentType type, bool granted)
    {
        var consent = context.CustomerConsents.FirstOrDefault(
            c => c.CustomerId == customerId && c.Type == type);
        
        if (consent == null)
        {
            consent = new CustomerConsent
            {
                CustomerId = customerId,
                Type = type,
                Granted = granted,
                GrantedAt = DateTime.UtcNow
            };
            context.CustomerConsents.Add(consent);
        }
        else
        {
            consent.Granted = granted;
            consent.UpdatedAt = DateTime.UtcNow;
        }
        
        context.SaveChanges();
        
        AuditTrail.Log("GDPR_CONSENT_CHANGE", $"Customer {customerId} consent {type}: {granted}");
    }
}
```

**Nouvelles Tables:**
```sql
CREATE TABLE CustomerConsent (
    Id INT IDENTITY PRIMARY KEY,
    CustomerId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL, -- 'Marketing', 'Analytics', 'ThirdParty'
    Granted BIT NOT NULL,
    GrantedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
);

CREATE TABLE GdprRequest (
    Id INT IDENTITY PRIMARY KEY,
    CustomerId INT NULL,
    EmployeeId INT NULL,
    RequestType NVARCHAR(50) NOT NULL, -- 'Access', 'Erasure', 'Rectification', 'Portability'
    RequestedAt DATETIME2 NOT NULL,
    ProcessedAt DATETIME2 NULL,
    Status NVARCHAR(50) NOT NULL, -- 'Pending', 'Completed', 'Rejected'
    Reason NVARCHAR(MAX) NULL,
    ProcessedBy INT NULL
);
```

**Fichiers:**
- `Pos/Pos/Services/GdprDataAccessService.cs`
- `Pos/Pos/Services/GdprDataErasureService.cs`
- `Pos/Pos/Services/GdprDataRectificationService.cs`
- `Pos/Pos/Services/GdprConsentService.cs`
- `Pos/Pos/Models/CustomerConsent.cs`
- `Pos/Pos/Models/GdprRequest.cs`
- Migration: `Pos/Pos/Migrations/YYYYMMDD_AddGdprTables.cs`

---

### **PHASE 4E: DATA RETENTION & AUTO-PURGE (Semaine 3)**
**Durée:** 3-4 jours  
**Objectif:** Suppression automatique données expirées

#### **4E.1 - Scheduled Job Retention**
```csharp
public class DataRetentionJob
{
    /// <summary>
    /// Exécuté chaque nuit à 3h00
    /// </summary>
    public void ExecuteRetentionPolicies()
    {
        var retentionConfig = LoadGdprConfig();
        
        // Anonymiser customers inactifs > 7 ans
        AnonymizeInactiveCustomers(retentionConfig.DataRetentionDays["Customers"]);
        
        // Purger employees partis > 5 ans
        PurgeFormerEmployees(retentionConfig.DataRetentionDays["Employees"]);
        
        // Archiver anciennes transactions > 10 ans
        ArchiveOldTransactions(retentionConfig.DataRetentionDays["Transactions"]);
        
        // Purger audit trail > 3 ans
        PurgeOldAuditTrail(retentionConfig.DataRetentionDays["AuditTrail"]);
        
        // Log execution
        AuditTrail.Log("GDPR_RETENTION_JOB", "Data retention policies applied");
    }
}
```

**Fichiers:**
- `Pos/Pos/Jobs/DataRetentionJob.cs`
- Configuration: Utiliser Windows Task Scheduler ou Hangfire

---

### **PHASE 4F: UI & RAPPORTS CONFORMITÉ (Semaine 3-4)**
**Durée:** 5-6 jours  
**Objectif:** Interface utilisateur pour droits RGPD

#### **4F.1 - Formulaire GDPR Requests**
```
┌─────────────────────────────────────────────┐
│  📋 GDPR Data Subject Request Form          │
├─────────────────────────────────────────────┤
│  Request Type:  [▼ Data Access Request   ]  │
│  Subject:       [▼ Customer              ]  │
│  ID:            [_____________]   [Search]  │
│                                              │
│  Reason:        [________________________]  │
│                 [________________________]  │
│                                              │
│  [Generate Export]  [Anonymize]  [Cancel]  │
└─────────────────────────────────────────────┘
```

**Forms à créer:**
- `Pos/Pos/Forms/Gdpr/GdprRequestForm.cs`
- `Pos/Pos/Forms/Gdpr/ConsentManagementForm.cs`
- `Pos/Pos/Forms/Gdpr/DataRetentionDashboard.cs`

#### **4F.2 - Rapport Conformité RGPD**
```csharp
public class GdprComplianceReport
{
    public int TotalCustomers { get; set; }
    public int CustomersWithConsent { get; set; }
    public int PendingErasureRequests { get; set; }
    public int RecordsAnonymized { get; set; }
    public int EncryptedFields { get; set; }
    public DateTime LastRetentionRun { get; set; }
    public Dictionary<string, int> DataBreachIncidents { get; set; }
}
```

**Fichiers:**
- `Pos/Pos/Reports/GdprComplianceReport.cs`
- `Pos/Pos/Forms/Reports/GdprComplianceDashboard.cs`

---

## 📈 MÉTRIQUES DE SUCCÈS

### **Conformité Technique**
- ✅ **100%** champs PII annotés avec [SensitiveData]
- ✅ **100%** données HIGH sensitivity encryptées
- ✅ **100%** audit trails masqués
- ✅ **100%** droits GDPR implémentés (4/4)
- ✅ **Automated** data retention policies

### **Conformité Légale**
- ✅ **Article 15** - Droit d'accès (Data Export)
- ✅ **Article 16** - Droit de rectification
- ✅ **Article 17** - Droit à l'oubli (Anonymization)
- ✅ **Article 20** - Portabilité des données (JSON export)
- ✅ **Article 21** - Droit d'opposition (Consent management)
- ✅ **Article 32** - Sécurité du traitement (Encryption + Audit)

### **Documentation**
- ✅ Privacy Policy document
- ✅ Data Processing Agreement (DPA)
- ✅ Data Breach Response Plan
- ✅ DPIA (Data Protection Impact Assessment)

---

## 🚨 RISQUES & MITIGATION

### **Risque 1: Performance Degradation**
- **Impact:** Encryption/decryption peut ralentir queries
- **Mitigation:** 
  - Caching des données décryptées en mémoire (session-scoped)
  - Index sur colonnes encryptées (hash-based)
  - Optimisation requêtes (SELECT uniquement champs nécessaires)

### **Risque 2: Key Management**
- **Impact:** Perte clé encryption = perte données
- **Mitigation:**
  - Azure Key Vault ou AWS KMS pour stockage clés
  - Key rotation automatique tous les 90 jours
  - Backup clés dans HSM (Hardware Security Module)

### **Risque 3: Migration Données Existantes**
- **Impact:** Corruption données lors encryption one-time
- **Mitigation:**
  - Backup COMPLET avant migration
  - Migration par batches (1000 records à la fois)
  - Rollback plan testé
  - Dry-run sur copie DB test

### **Risque 4: Compliance Drift**
- **Impact:** Nouvelles features ajoutent PII sans protection
- **Mitigation:**
  - Code review checklist GDPR
  - Automated tests pour champs non-annotés
  - Pre-commit hook vérifie [SensitiveData]

---

## 📅 PLANNING DÉTAILLÉ

```
SEMAINE 1
├─ Lun: 4A.1 Data Classification Attributes (3h)
├─ Mar: 4A.2 FieldEncryption Helper (5h)
├─ Mer: 4A.3 GDPR Configuration (2h) + 4B.1 EF ValueConverters (3h)
├─ Jeu: 4B.2 Annoter modèles Employee/Customer/User (6h)
└─ Ven: 4B.3 Migration script encryption (4h) + Tests (2h)

SEMAINE 2
├─ Lun: 4B.3 Migration execution (3h) + 4C.1 AuditTrail Masking (3h)
├─ Mar: 4C.2 SaveChanges override masking (4h) + Tests (2h)
├─ Mer: 4D.1 Droit d'Accès implementation (5h)
├─ Jeu: 4D.2 Droit à l'Oubli implementation (5h)
└─ Ven: 4D.3 Rectification + 4D.4 Consent (4h) + Tests (2h)

SEMAINE 3
├─ Lun: Create GdprRequest/CustomerConsent tables (2h) + Migration (2h)
├─ Mar: 4E.1 DataRetentionJob implementation (5h)
├─ Mer: 4E.1 Test retention job (3h) + Schedule setup (2h)
├─ Jeu: 4F.1 GdprRequestForm UI (5h)
└─ Ven: 4F.1 ConsentManagementForm UI (5h)

SEMAINE 4
├─ Lun: 4F.2 GdprComplianceReport (4h) + Dashboard (3h)
├─ Mar: Documentation (Privacy Policy, DPA) (6h)
├─ Mer: Tests end-to-end GDPR workflows (6h)
├─ Jeu: Audit sécurité final + Fix bugs (6h)
└─ Ven: Déploiement staging + Validation (6h)
```

**Total:** ~140 heures (~3.5 semaines full-time)

---

## 🎯 DÉMARRAGE IMMÉDIAT

**Voulez-vous que je commence par:**

**A)** Phase 4A (Classification & Encryption Infrastructure) - 3-4 jours  
**B)** Phase 4D (GDPR Rights) d'abord - plus visible pour users  
**C)** Quick Win: Annoter modèles existants [SensitiveData] - 1 jour  
**D)** Audit complet sécurité actuelle avant de commencer

**Recommendation:** Option **A** pour fondations solides, ou **C** pour progression visible rapide.

Quelle phase voulez-vous démarrer ?
