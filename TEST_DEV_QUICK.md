# 🧪 TEST DEV RAPIDE - VALIDATION AVANT PRODUCTION

**Durée:** 15-30 minutes  
**Objectif:** Valider encryption fonctionne avant migration production

---

## ✅ ÉTAPE 1: VÉRIFIER MASTER KEY (2 min)

**Ouvrir PowerShell et exécuter:**

```powershell
# Vérifier master key existe
$keyPath = "$env:APPDATA\Ezzipos\Security\.masterkey"
if (Test-Path $keyPath) {
    $keyInfo = Get-Item $keyPath
    Write-Host "✅ Master key found!" -ForegroundColor Green
    Write-Host "   Path: $keyPath"
    Write-Host "   Size: $($keyInfo.Length) bytes"
    Write-Host "   Created: $($keyInfo.CreationTime)"
    Write-Host "   Modified: $($keyInfo.LastWriteTime)"
} else {
    Write-Host "❌ Master key NOT found!" -ForegroundColor Red
    Write-Host "   Key will be auto-generated on first run"
}

# Vérifier backup key (recommandé)
$backupPath = "$env:APPDATA\Ezzipos\Security\.masterkey.backup"
if (Test-Path $backupPath) {
    Write-Host "✅ Backup key found" -ForegroundColor Green
} else {
    Write-Host "⚠️  No backup key - creating one..." -ForegroundColor Yellow
    if (Test-Path $keyPath) {
        Copy-Item $keyPath $backupPath
        Write-Host "✅ Backup key created: $backupPath" -ForegroundColor Green
    }
}
```

**Résultat attendu:**
```
✅ Master key found!
   Path: C:\Users\...\AppData\Roaming\Ezzipos\Security\.masterkey
   Size: 32 bytes
   Created: 2026-06-10 14:30:00
✅ Backup key created
```

---

## ✅ ÉTAPE 2: BUILD & RUN TEST SUITE (5 min)

**Dans Visual Studio:**

1. **Ouvrir:** `Pos/Pos/Program.cs`

2. **Ajouter AVANT `Application.Run()`:**

```csharp
// ═══════════════════════════════════════════════════════════════
// TEST DEV: GDPR Encryption Validation
// ═══════════════════════════════════════════════════════════════
#if DEBUG
Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
Console.WriteLine("║     GDPR ENCRYPTION TEST SUITE (DEV MODE)                ║");
Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// Exécuter test suite complet
string testResults = Pos.Function.GdprEncryptionTest.RunAllTests();
Console.WriteLine(testResults);

Console.WriteLine();
Console.WriteLine("═══════════════════════════════════════════════════════════");
Console.WriteLine("Press ENTER to continue to application...");
Console.WriteLine("Or press ESC to exit");
Console.WriteLine("═══════════════════════════════════════════════════════════");

var key = Console.ReadKey(true);
if (key.Key == ConsoleKey.Escape)
{
    Console.WriteLine("Exiting...");
    return;
}

Console.WriteLine("Starting application...");
Console.WriteLine();
#endif
// ═══════════════════════════════════════════════════════════════
```

3. **Compiler:** `Ctrl + Shift + B`

4. **Exécuter:** `Ctrl + F5` (Start Without Debugging)

**Résultat attendu:**
```
╔═══════════════════════════════════════════════════════════╗
║         GDPR ENCRYPTION TEST SUITE                       ║
╠═══════════════════════════════════════════════════════════╣
║ Date: 2026-06-10 15:45:30                             ║
╠═══════════════════════════════════════════════════════════╣

TEST 1: Customer Email/Phone Encryption
─────────────────────────────────────────────────
✅ ENCRYPTION TEST PASSED!
   - Customer saved with encrypted fields
   - Decryption successful: test.encryption@gdprtest.local
   - Test customer cleaned up (ID: 1234)
Status: ✅ PASSED

TEST 2: Employee Biometric Data (Article 9)
─────────────────────────────────────────────────
✅ EMPLOYEE ENCRYPTION TEST PASSED!
   - BloodGroup (Article 9) encrypted/decrypted
   - NoCard (Classified) encrypted/decrypted
   - Decryption successful
   - Test employee cleaned up (ID: 567)
Status: ✅ PASSED

TEST 3: Encryption Primitives
─────────────────────────────────────────────────
✅ All primitives working!
   - String encryption: OK
   - Byte array encryption: OK
   - Null handling: OK
   - Encrypted detection: OK
Status: ✅ PASSED

╠═══════════════════════════════════════════════════════════╣
║ SUMMARY: 3/3 tests passed                                ║
║ VERDICT: ✅ READY FOR PRODUCTION MIGRATION               ║
╚═══════════════════════════════════════════════════════════╝
```

**✅ Si 3/3 PASSED:** Continuer ÉTAPE 3  
**❌ Si FAILED:** Debugger avant production (voir Troubleshooting)

---

## ✅ ÉTAPE 3: VÉRIFIER ENCRYPTION EN DB (5 min)

**Dans SSMS (SQL Server Management Studio):**

```sql
USE [Pos];  -- Ou [ezzipos]

-- Vérifier qu'un customer de test a été créé puis supprimé
SELECT TOP 5
    Id,
    FirstName,
    Email,
    CreatedAt
FROM Customer
ORDER BY CreatedAt DESC;

-- Si vous voyez un customer récent avec Email encrypté (Base64):
-- "AgECAwQFBgcICQoLDA0ODxAREhMUFRYXGBka..." → ✅ ENCRYPTION FONCTIONNE
-- "test@example.com" → ❌ PLAINTEXT (encryption pas activée)
```

**Vérifier manuellement avec un customer existant:**

```sql
-- Créer customer test
INSERT INTO Customer (FirstName, LastName, Email, Phone, Code, Status, CreatedAt)
VALUES ('TestManual', 'GDPR', 'manual.test@gdpr.local', '0555999888', 'TEST_MANUAL', 'Active', GETDATE());

-- Lire immédiatement
SELECT TOP 1 * FROM Customer WHERE Code = 'TEST_MANUAL';

-- Vérifier format Email
SELECT
    Id,
    LEFT(Email, 60) as EmailFormat,
    LEN(Email) as EmailLength,
    CASE
        WHEN Email LIKE '%@%' THEN '❌ PLAINTEXT'
        WHEN LEN(Email) > 50 AND Email NOT LIKE '%@%' THEN '✅ ENCRYPTED (Base64)'
        ELSE '⚠️ Uncertain'
    END as Status
FROM Customer
WHERE Code = 'TEST_MANUAL';

-- Cleanup
DELETE FROM Customer WHERE Code = 'TEST_MANUAL';
```

**Résultat attendu:**
```
EmailFormat: AgECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxwdHh8gISIjJCUm...
EmailLength: 68
Status: ✅ ENCRYPTED (Base64)
```

---

## ✅ ÉTAPE 4: TEST APPLICATION COMPLÈTE (10 min)

**Lancer l'application normalement (sans test mode):**

1. **Commenter le code test dans `Program.cs`** (ou condition `#if DEBUG`)

2. **Lancer:** `Ctrl + F5`

3. **Login** avec utilisateur admin

4. **Test Customer:**
   - Ouvrir: Customers → Add Customer
   - Créer: FirstName: "TestApp", Email: "testapp@gdpr.local", Phone: "0666777888"
   - Sauvegarder
   - Fermer et rouvrir liste customers
   - **Vérifier:** Email et Phone s'affichent correctement décryptés

5. **Test Employee:**
   - Ouvrir: Employees → Add Employee
   - Créer: FirstName: "TestEmp", BloodGroup: "O+", NoCard: "123456789"
   - Sauvegarder
   - Rouvrir employee
   - **Vérifier:** BloodGroup et NoCard lisibles

6. **Vérifier en DB:**
```sql
-- Customer doit être encrypté
SELECT Email FROM Customer WHERE FirstName = 'TestApp';
-- Résultat: Base64 (encrypté)

-- Employee doit être encrypté
SELECT BloodGroup, NoCard FROM Employee WHERE FirstName = 'TestEmp';
-- Résultat: Base64 (encrypté)
```

**✅ Si tout fonctionne:** READY FOR PRODUCTION !

---

## ✅ ÉTAPE 5: AUDIT REPORT (2 min)

**Vérifier rapport audit au démarrage (dans Visual Studio Output):**

```
╔══════════════════════════════════════════════════════════════╗
║           GDPR ENCRYPTION AUDIT REPORT                       ║
╠══════════════════════════════════════════════════════════════╣
║ Date: 2026-06-10 16:00:00                              ║
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
║ COMPLIANCE STATUS:          ✗ NON-COMPLIANT (< 95%)   ║
╚══════════════════════════════════════════════════════════════╝
```

**Note:** Coverage < 95% est normal - nous avons annoté 6/80 entités (priorité haute).

---

## 🔧 TROUBLESHOOTING

### **Erreur: "Failed to load master key"**

**Solution:**
```powershell
# Supprimer et régénérer
Remove-Item "$env:APPDATA\Ezzipos\Security\.masterkey" -Force
# Relancer application - key auto-générée
```

### **Erreur: "Failed to decrypt data"**

**Cause:** Double-encryption ou clé changée

**Solution:**
```sql
-- Vérifier si données déjà encryptées
SELECT TOP 5 Email, LEN(Email) FROM Customer;
-- Si Email > 60 chars = déjà encrypté
-- Si Email normal (@...) = plaintext OK
```

### **Test FAILED: "Email decryption failed"**

**Solution:**
1. Vérifier Visual Studio Output pour stacktrace
2. Vérifier SQL Server accessible
3. Vérifier connection string dans `appsettings.json`

### **Application lente après encryption**

**Normal:** +50-100% temps lecture première fois. Solutions:
- Caching: Charger customers en mémoire (`ToList()`)
- Pagination: Ne charger que 50-100 records à la fois
- Index: Vérifier index DB sur champs non-encryptés

---

## ✅ VALIDATION FINALE

**Test considéré RÉUSSI si:**

- [x] Master key existe (`%APPDATA%\Ezzipos\Security\.masterkey`)
- [x] GdprEncryptionTest.RunAllTests() = 3/3 PASSED
- [x] Customer Email encrypté en DB (Base64 > 50 chars)
- [x] Customer Email décrypté correctement dans l'app
- [x] Employee BloodGroup (Article 9) encrypté
- [x] Application démarre sans erreurs
- [x] Login fonctionne
- [x] CRUD operations fonctionnent (Create/Read/Update)
- [x] Audit report généré au démarrage (Output window)
- [x] Aucune exception dans logs

---

## 🎉 SI TOUS LES TESTS PASSENT

**Vous êtes PRÊT pour la migration production !**

**Prochaine étape:** Suivre **PHASE4B3_MIGRATION_GUIDE.md** pour migration production avec backup.

**Checklist finale avant production:**
- [ ] Backup complet DB créé et testé
- [ ] Master key backupée
- [ ] Fenêtre maintenance approuvée (2-4h)
- [ ] Équipe en standby
- [ ] Rollback plan imprimé
- [ ] Utilisateurs notifiés

**⚠️ NE PAS OUBLIER:** Backup = survie des données !

---

## 📞 SI PROBLÈME BLOQUANT

**Ne PAS procéder à la migration production si:**
- ❌ Tests FAILED (< 3/3)
- ❌ Erreurs dans logs
- ❌ Application ne démarre pas
- ❌ Données non décryptées correctement
- ❌ Master key introuvable

**Action:** Debugger d'abord, puis retester.

**Aide:** Créer GitHub Issue avec:
- Test output complet
- Visual Studio Output logs
- SQL query results
- Error stacktrace

---

**Temps total test dev:** 15-30 minutes  
**Si succès:** Prêt pour production (Option A)  
**Si échec:** Debugger avant production
