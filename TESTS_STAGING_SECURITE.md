# 🧪 TESTS STAGING - VALIDATION SÉCURITÉ

**Date:** 2026-06-03  
**Version:** 1.0.0-security-audit  
**Niveau Sécurité:** 94% (30/32)  
**Durée Estimée:** 2 heures

---

## 📋 TABLE DES MATIÈRES

1. [Préparation](#préparation)
2. [Tests Phase 1 - Authentification](#phase-1---authentification)
3. [Tests Phase 2 - Transactions](#phase-2---transactions)
4. [Tests Phase 3 - Accès Données](#phase-3---accès-données)
5. [Résultats](#résultats)

---

## ✅ PRÉPARATION

### **Environnement de Test**
```
✅ Base de données: Test database (copie de prod)
✅ Utilisateurs: Test accounts créés
✅ Données: Sample data chargées
✅ Logs: Actifs pour monitoring
```

### **Créer Base de Test**
```sql
-- Créer DB test
CREATE DATABASE Pos_Test;
GO

-- Restaurer backup
RESTORE DATABASE Pos_Test 
FROM DISK = 'C:\Backups\Pos_Backup.bak'
WITH MOVE 'Pos' TO 'C:\Data\Pos_Test.mdf',
     MOVE 'Pos_log' TO 'C:\Data\Pos_Test_log.ldf';
```

### **Créer Utilisateurs Test**
```sql
USE Pos_Test;

-- Admin test
INSERT INTO [User] (Username, Password, Email, FullName, RoleId, IsActive)
VALUES ('admin_test', '[BCrypt hash]', 'admin@test.com', 'Admin Test', 1, 1);

-- User normal test
INSERT INTO [User] (Username, Password, Email, FullName, RoleId, IsActive)
VALUES ('user_test', '[BCrypt hash]', 'user@test.com', 'User Test', 2, 1);

-- User verrouillé test
INSERT INTO [User] (Username, Password, Email, FullName, RoleId, IsActive)
VALUES ('locked_test', '[BCrypt hash]', 'locked@test.com', 'Locked Test', 2, 0);
```

---

## 🔐 PHASE 1 - AUTHENTIFICATION

### **TEST 1.1: Login Valide** ✅
**Objectif:** Vérifier login normal fonctionne

**Étapes:**
1. Lancer application
2. Entrer credentials valides: `admin_test` / `Password123`
3. Cliquer Login

**Résultat Attendu:**
- ✅ Login réussit
- ✅ Session créée dans `UserSession` table
- ✅ SessionToken présent dans Settings
- ✅ LoginAttempt enregistrée (IsSuccessful = true)

**Vérification DB:**
```sql
-- Vérifier session créée
SELECT TOP 1 * FROM UserSession 
WHERE UserId = (SELECT Id FROM [User] WHERE Username = 'admin_test')
ORDER BY CreatedAt DESC;

-- Vérifier login attempt
SELECT TOP 1 * FROM LoginAttempt
WHERE Username = 'admin_test'
ORDER BY AttemptTime DESC;
```

---

### **TEST 1.2: Brute Force Protection (5 tentatives)** ✅
**Objectif:** Vérifier verrouillage après 5 échecs

**Étapes:**
1. Entrer `user_test` / `WrongPassword1` → Cliquer Login
2. Entrer `user_test` / `WrongPassword2` → Cliquer Login
3. Entrer `user_test` / `WrongPassword3` → Cliquer Login
4. Entrer `user_test` / `WrongPassword4` → Cliquer Login
5. Entrer `user_test` / `WrongPassword5` → Cliquer Login
6. Entrer `user_test` / `[CorrectPassword]` → Cliquer Login

**Résultat Attendu:**
- ✅ Tentatives 1-5: "Invalid credentials" affiché
- ✅ Tentative 6: "Account locked... Try again in 15 minutes" affiché
- ✅ Login refusé même avec bon password
- ✅ 5 LoginAttempt créées (IsSuccessful = false)

**Vérification DB:**
```sql
-- Compter échecs récents
SELECT COUNT(*) as FailedAttempts
FROM LoginAttempt
WHERE Username = 'user_test'
  AND IsSuccessful = 0
  AND AttemptTime >= DATEADD(MINUTE, -30, GETDATE());
-- Doit retourner: 5
```

---

### **TEST 1.3: Rate Limiting Progressif** ✅
**Objectif:** Vérifier délais progressifs (0s, 2s, 5s, 10s, 20s, 30s)

**Étapes:**
1. Créer user fresh: `rate_test`
2. Tentative 1: Noter temps avant/après → Délai attendu: **0s**
3. Tentative 2 (échec): Noter temps → Délai attendu: **2s**
4. Tentative 3 (échec): Noter temps → Délai attendu: **5s**
5. Tentative 4 (échec): Noter temps → Délai attendu: **10s**
6. Tentative 5 (échec): Noter temps → Délai attendu: **20s**
7. Tentative 6 (échec): Noter temps → Délai attendu: **30s**

**Résultat Attendu:**
- ✅ Délais observés correspondent aux attentes
- ✅ Application se "freeze" pendant délai (normal, c'est Thread.Sleep)

**Mesure:**
```
Tentative 1: 14:30:00 → 14:30:00 (0s) ✅
Tentative 2: 14:30:05 → 14:30:07 (2s) ✅
Tentative 3: 14:30:10 → 14:30:15 (5s) ✅
Tentative 4: 14:30:20 → 14:30:30 (10s) ✅
Tentative 5: 14:30:35 → 14:30:55 (20s) ✅
Tentative 6: 14:31:00 → 14:31:30 (30s) ✅
```

---

### **TEST 1.4: Session Timeout** ✅
**Objectif:** Vérifier expiration session après 24h

**Étapes:**
1. Login avec `admin_test`
2. Noter SessionToken
3. Modifier CreatedAt dans DB: `CreatedAt = DATEADD(HOUR, -25, GETDATE())`
4. Redémarrer application
5. Tenter action requérant auth

**Résultat Attendu:**
- ✅ Session expirée détectée
- ✅ Redirection vers login
- ✅ Message "Session expired"

**Vérification DB:**
```sql
-- Forcer expiration session
UPDATE UserSession
SET CreatedAt = DATEADD(HOUR, -25, GETDATE())
WHERE UserId = (SELECT Id FROM [User] WHERE Username = 'admin_test');
```

---

### **TEST 1.5: SQL Injection Login** ✅
**Objectif:** Vérifier protection SQL injection

**Étapes:**
Tenter login avec payloads SQL injection:
1. Username: `admin' OR '1'='1` / Password: `anything`
2. Username: `admin'--` / Password: `anything`
3. Username: `admin'; DROP TABLE User;--` / Password: `anything`

**Résultat Attendu:**
- ✅ Tous les tentatives ÉCHOUENT
- ✅ "Invalid credentials" affiché
- ✅ Pas d'erreur SQL
- ✅ Table User toujours intacte
- ✅ LoginAttempt créées avec le username malveillant (preuve d'attaque)

---

## 💰 PHASE 2 - TRANSACTIONS

### **TEST 2.1: Validation Sale - Montant Négatif** ✅
**Objectif:** Vérifier refus vente avec montant négatif

**Étapes:**
1. Ouvrir POS screen
2. Ajouter produit au panier
3. Modifier manuellement NetTotalAmount dans DB à valeur négative
4. Tenter SaveChanges

**Résultat Attendu:**
- ✅ Exception levée: "NetTotalAmount cannot be negative"
- ✅ Vente PAS sauvegardée
- ✅ Message erreur affiché

**Test Programmatique:**
```csharp
// Dans tests unitaires
[Test]
public void SaleValidator_NegativeAmount_ShouldFail()
{
    var sale = new Sale { NetTotalAmount = -100m };
    var result = SaleValidator.ValidateSale(sale);
    
    Assert.IsFalse(result.isValid);
    Assert.Contains("négatif", result.errorMessage);
}
```

---

### **TEST 2.2: Validation Sale - Limite MAX_SALE_AMOUNT** ✅
**Objectif:** Vérifier refus vente > 100M DA

**Étapes:**
1. Créer vente avec NetTotalAmount = 150,000,000 DA
2. Appeler SaveChanges

**Résultat Attendu:**
- ✅ Exception: "Sale amount exceeds maximum allowed"
- ✅ Vente refusée

**Test DB:**
```sql
-- Tenter insert manuel
INSERT INTO Sale (NetTotalAmount, CustomerId, UserId, CreatedAt)
VALUES (150000000, 1, 1, GETDATE());
-- Doit échouer via SaveChanges validation
```

---

### **TEST 2.3: Validation Return - Daily Limit** ✅
**Objectif:** Vérifier limite retours quotidiens

**Étapes:**
1. Créer 5 retours pour même client aujourd'hui (total 1000 DA chacun)
2. Tenter 6ème retour de 1000 DA

**Résultat Attendu:**
- ✅ Si limite quotidienne dépassée: refus
- ✅ Message: "Daily return limit exceeded"

**Vérification:**
```sql
-- Compter retours aujourd'hui
SELECT COUNT(*), SUM(GrandTotal)
FROM [Return]
WHERE CustomerId = 1
  AND CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE);
```

---

### **TEST 2.4: Money Rounding Consistency** ✅
**Objectif:** Vérifier arrondis cohérents (2 décimales)

**Test Programmatique:**
```csharp
[Test]
public void MoneyHelper_Round_ShouldBe2Decimals()
{
    Assert.AreEqual(10.12m, MoneyHelper.Round(10.123m));
    Assert.AreEqual(10.13m, MoneyHelper.Round(10.125m)); // Away from zero
    Assert.AreEqual(10.13m, MoneyHelper.Round(10.126m));
}

[Test]
public void MoneyHelper_Calculate_ShouldBeConsistent()
{
    var subtotal = 100.00m;
    var discount = 10.50m;
    var tax = 19.0m;
    
    var result = MoneyHelper.CalculateSaleTotal(subtotal, discount, tax);
    
    // (100 - 10.50) = 89.50
    // 89.50 * 1.19 = 106.505 → 106.51
    Assert.AreEqual(106.51m, result.GrandTotal);
}
```

---

### **TEST 2.5: Audit Logging** ✅
**Objectif:** Vérifier audit trail fonctionne

**Étapes:**
1. Créer customer "Test Audit Customer"
2. Modifier customer (changer nom)
3. Archiver product ID 1
4. Créer vente
5. Exporter Excel

**Résultat Attendu:**
- ✅ 5 entrées dans `AuditTrail` table
- ✅ Chaque action enregistrée avec UserId, TableName, ActionType, OldValues, NewValues, ChangeTime

**Vérification DB:**
```sql
SELECT 
    ActionType,
    TableName,
    KeyValues,
    OldValues,
    NewValues,
    ChangeTime
FROM AuditTrail
WHERE UserId = (SELECT Id FROM [User] WHERE Username = 'admin_test')
ORDER BY ChangeTime DESC;
```

**Attendu:**
```
Excel Export  | LoyaltyCards | null    | null | "Exported 50 rows..." | 2026-06-03 10:35
Create Sale   | Sale         | 123     | null | {"NetTotal": 100...}  | 2026-06-03 10:34
Archive       | Product      | 1       | ...  | {"IsArchived": true}  | 2026-06-03 10:33
Update        | Customer     | 45      | ...  | {"Name": "New..."}    | 2026-06-03 10:32
Create        | Customer     | 45      | null | {"Name": "Test..."}   | 2026-06-03 10:31
```

---

## 🔒 PHASE 3 - ACCÈS DONNÉES

### **TEST 3.1: Upload Image - Validation Magic Bytes** ✅
**Objectif:** Vérifier refus fichiers non-images

**Étapes:**
1. Ouvrir Product → AddEditProduct
2. Double-cliquer image upload
3. Tenter uploader fichier .txt renommé en .jpg
4. Tenter uploader vrai .jpg

**Résultat Attendu:**
- ✅ .txt déguisé: Refusé avec "Not a valid image file"
- ✅ Vrai .jpg: Accepté

**Test Fichiers:**
```bash
# Créer .txt déguisé
echo "Not an image" > fake.jpg

# Créer vraie image test
# (Utiliser Paint ou outil pour créer test.jpg)
```

---

### **TEST 3.2: Upload Image - Taille Limite** ✅
**Objectif:** Vérifier refus images > 10 MB

**Étapes:**
1. Créer image 15 MB
2. Tenter upload

**Résultat Attendu:**
- ✅ Refusé: "File too large (15 MB). Maximum: 10 MB"

---

### **TEST 3.3: Export Excel - Permission Check** ✅
**Objectif:** Vérifier contrôle permission avant export

**Étapes:**
1. Login avec user SANS permission "Export Loyalty Cards"
2. Ouvrir LoyaltyCardForm
3. Cliquer "Export Excel"

**Résultat Attendu:**
- ✅ Dialog "Access Denied" affiché
- ✅ Export PAS exécuté
- ✅ Pas de fichier créé

**Setup User Sans Permission:**
```sql
-- Créer role sans export permission
INSERT INTO Role (Name) VALUES ('Limited User');
SET @RoleId = SCOPE_IDENTITY();

-- Ne PAS ajouter permission export
-- INSERT INTO RolePermission...

-- Créer user avec ce role
INSERT INTO [User] (Username, Password, RoleId, ...)
VALUES ('limited_test', '[hash]', @RoleId, ...);
```

---

### **TEST 3.4: Export Excel - Formula Injection Prevention** ✅
**Objectif:** Vérifier sanitization formules Excel

**Étapes:**
1. Créer LoyaltyCard avec nom: `=1+1`
2. Créer LoyaltyCard avec notes: `=SUM(A1:A10)`
3. Exporter en Excel
4. Ouvrir fichier Excel

**Résultat Attendu:**
- ✅ Cellules affichent `=1+1` comme TEXTE (pas calculé)
- ✅ Excel NE calcule PAS les formules
- ✅ Valeurs préfixées avec apostrophe si inspect

---

### **TEST 3.5: Export CSV - Injection Prevention** ✅
**Objectif:** Vérifier sanitization CSV

**Étapes:**
1. Créer data avec valeurs: `=cmd|'/c calc'`, `+1+1`, `-2+2`
2. Exporter CSV
3. Ouvrir dans Excel/LibreOffice

**Résultat Attendu:**
- ✅ Valeurs affichées comme texte
- ✅ Calc.exe NE s'ouvre PAS
- ✅ Cellules préfixées `'=`, `'+`, `'-`

---

### **TEST 3.6: Export - Limite 50k Lignes** ✅
**Objectif:** Vérifier limite exports

**Étapes:**
1. Créer dataset avec 60,000 lignes
2. Tenter exporter

**Résultat Attendu:**
- ✅ Message: "Cannot export more than 50,000 rows"
- ✅ "Current rows: 60,000"
- ✅ "Please filter the data first"
- ✅ Export refusé

---

### **TEST 3.7: Input Sanitization - Customer Form** ✅
**Objectif:** Vérifier validation formulaire Customer

**Étapes - Nom Invalide:**
1. Ouvrir AddEditCustomer
2. FirstName: `<script>alert('xss')</script>`
3. Cliquer Save

**Résultat Attendu:**
- ✅ Erreur: "First Name Error: Name contains invalid characters"
- ✅ Focus sur FirstName
- ✅ Pas de save

**Étapes - Email Invalide:**
1. FirstName: `John` (valide)
2. LastName: `Doe` (valide)
3. Email: `not-an-email`
4. Cliquer Save

**Résultat Attendu:**
- ✅ Erreur: "Email Error: Invalid email format"
- ✅ Pas de save

**Étapes - Phone Invalide:**
1. Email: `john@test.com` (valide)
2. Phone: `abc123`
3. Cliquer Save

**Résultat Attendu:**
- ✅ Erreur: "Phone Error: Phone must contain only digits"

**Étapes - Valeurs Valides:**
1. FirstName: `John`
2. LastName: `Doe`
3. Email: `john.doe@test.com`
4. Phone: `0555123456`
5. Cliquer Save

**Résultat Attendu:**
- ✅ Save réussit
- ✅ Customer créé avec valeurs sanitized

---

### **TEST 3.8: SaveChanges Validation Override** ✅
**Objectif:** Vérifier validation DB-level

**Test Programmatique:**
```csharp
[Test]
public void AppDbContext_SaveChanges_InvalidSale_ShouldThrow()
{
    using (var context = new AppDbContext())
    {
        var sale = new Sale 
        { 
            NetTotalAmount = -500m, // Invalide
            CustomerId = 1,
            UserId = 1,
            CreatedAt = DateTime.Now
        };
        
        context.Sales.Add(sale);
        
        // Doit lever exception
        var ex = Assert.Throws<InvalidOperationException>(() => context.SaveChanges());
        Assert.Contains("Sale validation failed", ex.Message);
    }
}
```

**Test Manuel:**
Tenter insert SQL direct avec valeur invalide - doit échouer via SaveChanges override.

---

### **TEST 3.9: TLS/SSL Database Connection** ✅
**Objectif:** Vérifier encryption DB active

**Vérification:**
```sql
-- Sur SQL Server, vérifier encryption
SELECT 
    session_id,
    encrypt_option,
    net_transport
FROM sys.dm_exec_connections
WHERE session_id = @@SPID;
```

**Résultat Attendu:**
- ✅ `encrypt_option` = 'TRUE'
- ✅ Connection encrypted

**Alternative - Wireshark:**
1. Capturer traffic localhost:1433
2. Vérifier packets encryptés (pas de plaintext SQL visible)

---

### **TEST 3.10: XXE Protection** ✅
**Objectif:** Vérifier XXE impossible

**Test Programmatique:**
```csharp
[Test]
public void XmlSecurityHelper_XXE_ShouldBlock()
{
    string maliciousXml = @"<?xml version=""1.0""?>
<!DOCTYPE foo [
  <!ENTITY xxe SYSTEM ""file:///c:/windows/win.ini"">
]>
<root>&xxe;</root>";

    // Tentative parse avec helper sécurisé
    Assert.Throws<XmlException>(() => 
        XmlSecurityHelper.ParseSecureXml(maliciousXml));
}

[Test]
public void XmlSecurityHelper_ValidateXXE_ShouldDetect()
{
    string xmlWithDTD = @"<!DOCTYPE test><root>data</root>";
    
    var result = XmlSecurityHelper.ValidateXmlForXXE(xmlWithDTD);
    
    Assert.IsFalse(result.isValid);
    Assert.Contains("DOCTYPE", result.errorMessage);
}
```

---

### **TEST 3.11: Mass Assignment Protection** ✅
**Objectif:** Vérifier propriétés sensibles protégées

**Test Programmatique:**
```csharp
[Test]
public void MassAssignmentProtection_ProtectedProperties_ShouldBlock()
{
    var userType = typeof(User);
    
    // Vérifier propriétés sensibles sont protégées
    var idProperty = userType.GetProperty("Id");
    Assert.IsFalse(MassAssignmentProtection.CanAssignProperty(idProperty));
    
    var createdAtProperty = userType.GetProperty("CreatedAt");
    Assert.IsFalse(MassAssignmentProtection.CanAssignProperty(createdAtProperty));
    
    var isAdminProperty = userType.GetProperty("IsAdmin");
    Assert.IsFalse(MassAssignmentProtection.CanAssignProperty(isAdminProperty));
}

[Test]
public void MassAssignmentProtection_SafeCopy_ShouldOnlyCopyAllowed()
{
    var source = new User 
    { 
        Id = 999,  // Protected
        Username = "hacker",  // Allowed
        FullName = "Hacker Man",  // Allowed
        IsAdmin = "Admin",  // Protected
        CreatedAt = DateTime.Now  // Protected
    };
    
    var destination = new User 
    { 
        Id = 1, 
        Username = "original",
        FullName = "Original User",
        IsAdmin = "User",
        CreatedAt = DateTime.Now.AddDays(-30)
    };
    
    MassAssignmentProtection.SafeCopy(source, destination);
    
    // Vérifier seules propriétés allowed copiées
    Assert.AreEqual(1, destination.Id);  // Pas changé (protected)
    Assert.AreEqual("hacker", destination.Username);  // Changé (allowed)
    Assert.AreEqual("Hacker Man", destination.FullName);  // Changé (allowed)
    Assert.AreEqual("User", destination.IsAdmin);  // Pas changé (protected)
}
```

---

## 📊 RÉSULTATS

### **Checklist Tests**

#### **Authentification (6/6)**
```
✅ TEST 1.1: Login valide
✅ TEST 1.2: Brute force protection
✅ TEST 1.3: Rate limiting progressif
✅ TEST 1.4: Session timeout
✅ TEST 1.5: SQL injection prevention
✅ TEST 1.6: BONUS - Timing attack prevention
```

#### **Transactions (5/5)**
```
✅ TEST 2.1: Validation montant négatif
✅ TEST 2.2: Limite MAX_SALE_AMOUNT
✅ TEST 2.3: Validation return daily limit
✅ TEST 2.4: Money rounding consistency
✅ TEST 2.5: Audit logging
```

#### **Accès Données (11/11)**
```
✅ TEST 3.1: Upload magic bytes validation
✅ TEST 3.2: Upload size limit
✅ TEST 3.3: Export permission check
✅ TEST 3.4: Export Excel formula injection
✅ TEST 3.5: Export CSV injection
✅ TEST 3.6: Export row limit
✅ TEST 3.7: Input sanitization formulaires
✅ TEST 3.8: SaveChanges validation override
✅ TEST 3.9: TLS/SSL database
✅ TEST 3.10: XXE protection
✅ TEST 3.11: Mass assignment protection
```

### **Score Final**
```
Tests Réussis:   __/22
Tests Échoués:   __/22
Bugs Trouvés:    __
Score:           __%
```

---

## 🐛 RAPPORT BUGS

### **Template Bug Report**
```markdown
## BUG-XXX: [Titre Court]

**Sévérité:** CRITIQUE / HAUTE / MOYENNE / FAIBLE
**Test:** TEST X.Y
**Date:** 2026-06-03

### Description
[Qu'est-ce qui ne fonctionne pas ?]

### Steps to Reproduce
1. [Étape 1]
2. [Étape 2]
3. [Résultat observé]

### Expected Behavior
[Comportement attendu]

### Actual Behavior
[Comportement réel]

### Evidence
```sql
-- Query ou logs
```

### Impact
[Quel risque ? Quels utilisateurs affectés ?]

### Suggested Fix
[Proposition correction si évidente]
```

---

## ✅ VALIDATION FINALE

### **Avant Mise en Production**

**Sécurité:**
- [ ] Tous tests Phase 1 passent
- [ ] Tous tests Phase 2 passent
- [ ] Tous tests Phase 3 passent
- [ ] Aucun bug CRITIQUE trouvé
- [ ] Bugs HAUTS documentés et triagés

**Performance:**
- [ ] Login < 2 secondes
- [ ] SaveChanges validation < 100ms overhead
- [ ] Export 10k lignes < 30 secondes

**Conformité:**
- [ ] Audit logs activés
- [ ] Aucune donnée sensible dans logs
- [ ] TLS/SSL database actif
- [ ] Branch protection GitHub configurée

**Documentation:**
- [ ] RAPPORT_EXECUTIF à jour
- [ ] Bugs documentés dans GitHub Issues
- [ ] Guide déploiement créé
- [ ] Formation équipe planifiée

---

## 📞 SUPPORT

**En cas de problème pendant tests:**
1. Noter test échoué avec détails
2. Capturer logs/screenshots
3. Créer GitHub Issue
4. Continuer autres tests

**Repository:** https://github.com/BambaDev/Tripol_POS_STOCK  
**Issues:** https://github.com/BambaDev/Tripol_POS_STOCK/issues

---

**Bon Tests ! 🧪**
**Sécurité: 94% → Validation en cours...**
