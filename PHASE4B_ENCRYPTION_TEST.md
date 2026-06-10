# 🧪 PHASE 4B - TEST ENCRYPTION AUTOMATIQUE

**Date:** 2026-06-10  
**Status:** READY FOR TESTING

---

## 🎯 OBJECTIF

Vérifier que l'encryption automatique via EF Core ValueConverters fonctionne correctement pour les champs marqués `[SensitiveData(RequiresEncryption=true)]`.

---

## ✅ TEST SIMPLE - CUSTOMER EMAIL

### **Étape 1: Créer Customer avec email sensible**

```csharp
using (var context = new AppDbContext())
{
    var customer = new Customer
    {
        FirstName = "Jean",
        LastName = "Dupont",
        Email = "jean.dupont@test.com", // Doit être encrypté
        Phone = "0555123456",            // Doit être encrypté
        Code = "CUST001",
        Status = "Active",
        CreatedAt = DateTime.Now
    };
    
    context.Customers.Add(customer);
    context.SaveChanges();
    
    Console.WriteLine($"Customer créé avec ID: {customer.Id}");
}
```

### **Étape 2: Vérifier encryption dans la DB**

```sql
-- Dans SQL Server Management Studio ou Azure Data Studio
SELECT TOP 1
    Id,
    FirstName,  -- Devrait être encrypté (Base64)
    LastName,   -- Devrait être encrypté (Base64)
    Email,      -- Devrait être encrypté (Base64)
    Phone,      -- Devrait être encrypté (Base64)
    Code        -- Plaintext (pas marqué RequiresEncryption)
FROM Customer
ORDER BY Id DESC;
```

**Résultat Attendu:**
```
Id: 123
FirstName: "AgECAwQFBgcICQoLDA0ODxAREhM..." (Base64)
LastName:  "FRYXGBkaGxwdHh8gISIjJCUmJyg..." (Base64)
Email:     "KSorLC0uLzAxMjM0NTY3ODk6Ozw..." (Base64)
Phone:     "PT4/QEFCQ0RFRkdISUpLTE1OT1..." (Base64)
Code:      "CUST001" (plaintext)
```

### **Étape 3: Lire Customer et vérifier decryption**

```csharp
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(123); // Utiliser ID de l'étape 1
    
    Console.WriteLine($"FirstName (décrypté): {customer.FirstName}");
    Console.WriteLine($"LastName (décrypté): {customer.LastName}");
    Console.WriteLine($"Email (décrypté): {customer.Email}");
    Console.WriteLine($"Phone (décrypté): {customer.Phone}");
    Console.WriteLine($"Code (plaintext): {customer.Code}");
}
```

**Résultat Attendu:**
```
FirstName (décrypté): Jean
LastName (décrypté): Dupont
Email (décrypté): jean.dupont@test.com
Phone (décrypté): 0555123456
Code (plaintext): CUST001
```

✅ **Si ces valeurs correspondent, l'encryption fonctionne correctement !**

---

## 🔬 TEST AVANCÉ - EMPLOYEE WITH BIOMETRIC DATA

### **Étape 1: Créer Employee avec données Article 9 GDPR**

```csharp
using (var context = new AppDbContext())
{
    var employee = new Employee
    {
        FirstName = "Marie",
        LastName = "Martin",
        Email = "marie.martin@company.com",
        PhoneNumber = "0666789012",
        BloodGroup = "O+",  // Article 9 GDPR - Biometric (Restricted)
        Image = System.IO.File.ReadAllBytes("photo.jpg"), // Biometric
        NoCard = "123456789",     // Classified - Identity document
        NoPass = "AB1234567",     // Classified - Passport
        DateOfBirth = new DateTime(1990, 5, 15),
        Address = "123 Rue de Paris, Alger",
        Gender = "Female",
        CreatedAt = DateTime.Now
    };
    
    context.Employees.Add(employee);
    context.SaveChanges();
    
    Console.WriteLine($"Employee créé avec ID: {employee.Id}");
}
```

### **Étape 2: Vérifier encryption dans DB**

```sql
SELECT TOP 1
    Id,
    FirstName,    -- Encrypted
    LastName,     -- Encrypted
    Email,        -- Encrypted
    PhoneNumber,  -- Encrypted
    BloodGroup,   -- Encrypted (Article 9)
    NoCard,       -- Encrypted (CLASSIFIED)
    NoPass,       -- Encrypted (CLASSIFIED)
    DateOfBirth,  -- Encrypted
    Address,      -- Encrypted
    Gender,       -- Encrypted
    LEN(Image) as ImageSizeBytes  -- Encrypted byte array
FROM Employee
ORDER BY Id DESC;
```

**Résultat Attendu:**
- Tous les champs sensibles sont en Base64 (strings) ou binaire encrypté (Image)
- ImageSizeBytes devrait être > taille originale (overhead encryption: +28 bytes pour nonce+tag)

### **Étape 3: Vérifier decryption et accès**

```csharp
using (var context = new AppDbContext())
{
    var employee = context.Employees.Find(employeeId);
    
    // Vérifier toutes les données décryptées correctement
    Assert.AreEqual("Marie", employee.FirstName);
    Assert.AreEqual("Martin", employee.LastName);
    Assert.AreEqual("marie.martin@company.com", employee.Email);
    Assert.AreEqual("O+", employee.BloodGroup);
    Assert.AreEqual("123456789", employee.NoCard);
    Assert.AreEqual("AB1234567", employee.NoPass);
    Assert.IsNotNull(employee.Image);
    Assert.IsTrue(employee.Image.Length > 0);
    
    Console.WriteLine("✅ Tous les champs décryptés correctement !");
}
```

---

## 📊 VÉRIFIER RAPPORT D'AUDIT

Au démarrage de l'application (mode DEBUG), un rapport d'audit GDPR est généré dans la console Visual Studio Output:

```
╔══════════════════════════════════════════════════════════════╗
║           GDPR ENCRYPTION AUDIT REPORT                       ║
╠══════════════════════════════════════════════════════════════╣
║ Date: 2026-06-10 14:30:00                              ║
║                                                              ║
║ ENTITIES SCANNED:           80                        ║
║ SENSITIVE PROPERTIES:       65                        ║
║ ENCRYPTED PROPERTIES:       58                        ║
║ ENCRYPTION COVERAGE:        89%                        ║
║                                                              ║
║ BY SENSITIVITY LEVEL:                                        ║
║   • CLASSIFIED (Level 4):   12                        ║
║   • RESTRICTED (Level 3):   18                        ║
║   • CONFIDENTIAL (Level 2): 28                        ║
║                                                              ║
║ BY DATA CATEGORY:                                            ║
║   • Health Data (Art. 9):    5                        ║
║   • Biometric Data (Art.9):  8                        ║
║   • Financial Data:         10                        ║
║                                                              ║
║ COMPLIANCE STATUS:          ✗ NON-COMPLIANT            ║
╚══════════════════════════════════════════════════════════════╝
```

**Note:** Si ENCRYPTION COVERAGE < 95%, c'est normal - nous avons annoté seulement 6 modèles. Pour 100% coverage, il faut annoter les 80+ entités.

---

## ⚠️ POINTS À VÉRIFIER

### **✅ Checklist Encryption**
- [ ] Customer.Email encrypté en DB (Base64)
- [ ] Customer.Email décrypté correct en lecture
- [ ] Employee.BloodGroup encrypté (Article 9)
- [ ] Employee.NoCard encrypté (CLASSIFIED)
- [ ] Employee.Image encrypté (byte array)
- [ ] Recherche par email NE FONCTIONNE PAS (normal - champs encryptés non searchable)
- [ ] Rapport audit généré au démarrage
- [ ] Aucune erreur de conversion

### **❌ Ce Qui NE Doit PAS Fonctionner (Normal)**

**1. Recherche SQL directe sur champs encryptés:**
```csharp
// ❌ NE FONCTIONNERA PAS
var customer = context.Customers
    .Where(c => c.Email == "jean@test.com")
    .FirstOrDefault();
// → null, car Email est encrypté en DB
```

**Solution:** Charger tous les customers en mémoire puis filtrer:
```csharp
// ✅ FONCTIONNE
var customers = context.Customers.ToList();
var customer = customers.FirstOrDefault(c => c.Email == "jean@test.com");
```

**2. ORDER BY sur champs encryptés:**
```csharp
// ❌ NE FONCTIONNERA PAS (tri sur Base64, pas alphabétique)
var sorted = context.Customers
    .OrderBy(c => c.FirstName)
    .ToList();
```

**3. LIKE / Contains sur champs encryptés:**
```csharp
// ❌ NE FONCTIONNERA PAS
var results = context.Customers
    .Where(c => c.Email.Contains("@test.com"))
    .ToList();
```

---

## 🔧 TROUBLESHOOTING

### **Problème: Erreur "Failed to decrypt data - authentication failed"**

**Cause:** Clé d'encryption changée ou données corrompues

**Solution:**
1. Vérifier master key existe: `%APPDATA%\Ezzipos\Security\.masterkey`
2. Si clé perdue, données encryptées sont IRRÉCUPÉRABLES
3. Restaurer backup DB avant encryption

### **Problème: Erreur "Invalid ciphertext format"**

**Cause:** Tentative de décrypter données plaintext (DB non migrée)

**Solution:**
1. Données existantes en DB sont plaintext
2. Il faut migrer avec script EncryptExistingPii (Phase 4B.3)
3. Ou tester sur nouvelle DB vide

### **Problème: Performance dégradée**

**Cause:** Encryption/decryption coûteux en CPU

**Solutions:**
1. Caching en mémoire des entités fréquemment lues
2. Charger uniquement champs nécessaires avec `Select()`
3. Paginer les résultats (ne pas charger 10k customers d'un coup)

---

## 📈 MÉTRIQUES ATTENDUES

### **Performance Impact**

| Opération | Plaintext | Encrypted | Overhead |
|-----------|-----------|-----------|----------|
| **SaveChanges (1 Customer)** | 5ms | 8ms | +60% |
| **Read (1 Customer)** | 2ms | 4ms | +100% |
| **Read (100 Customers)** | 50ms | 150ms | +200% |
| **Query encrypted field** | 10ms | N/A | Impossible |

**Note:** Performance impact est ACCEPTABLE pour conformité GDPR Article 32 (sécurité appropriée).

### **Stockage Impact**

| Champ | Plaintext | Encrypted | Overhead |
|-------|-----------|-----------|----------|
| Email (30 chars) | 30 bytes | 68 bytes | +127% |
| Image (50 KB) | 50,000 bytes | 50,028 bytes | +0.05% |

**Note:** Overhead fixe de 28 bytes (nonce 12 + tag 16) par champ encrypté.

---

## ✅ VALIDATION FINALE

**Pour considérer Phase 4B complète:**

- [x] ValueConverters créés (EncryptedStringConverter, EncryptedBytesConverter)
- [x] GdprModelBuilder scanne attributs automatiquement
- [x] AppDbContext.OnModelCreating() appelle ApplyGdprEncryption()
- [x] Build réussit sans erreurs
- [ ] Test manuel: Customer.Email encrypté/décrypté ✅
- [ ] Test manuel: Employee.BloodGroup (Article 9) encrypté ✅
- [ ] Rapport audit généré au démarrage
- [ ] Documentation migration données existantes (Phase 4B.3)

**Status:** 🟡 PRÊT POUR TEST MANUEL

---

**Prochaine Étape:** Phase 4B.3 - Migration script pour encrypter données existantes en production.
