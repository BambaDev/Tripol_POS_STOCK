# 🔐 PHASE 4B.3 - GUIDE MIGRATION PRODUCTION

**Date:** 2026-06-10  
**Version:** 1.0.0  
**Durée:** 30 min - 2h selon taille DB  
**Criticité:** 🔴 **HAUTE - BACKUP OBLIGATOIRE**

---

## ⚠️ AVERTISSEMENT CRITIQUE

Cette migration encrypte **TOUTES** les données sensibles en production.

**RISQUES:**
- ✅ **Avec backup:** Rollback possible en 5 minutes
- ❌ **Sans backup:** **PERTE DÉFINITIVE DES DONNÉES**

**Ne JAMAIS exécuter sans:**
1. ✅ Backup complet testé
2. ✅ Test sur copie DB de développement
3. ✅ Fenêtre de maintenance planifiée
4. ✅ Plan rollback prêt

---

## 📋 CHECKLIST PRÉ-MIGRATION

### **✅ Prérequis Techniques**
- [ ] SQL Server 2016 ou supérieur
- [ ] Espace disque: 3x taille DB actuelle (backup + migration + buffer)
- [ ] Application Ezzipos fermée (aucun utilisateur connecté)
- [ ] Permissions DBA sur SQL Server
- [ ] Visual Studio ou dotnet CLI installé
- [ ] Master encryption key générée (`%APPDATA%\Ezzipos\Security\.masterkey`)

### **✅ Prérequis Organisationnels**
- [ ] Fenêtre de maintenance approuvée (2-4h recommandées)
- [ ] Utilisateurs notifiés de l'indisponibilité
- [ ] Équipe technique en standby
- [ ] Rollback plan validé par management
- [ ] Communication post-migration préparée

### **✅ Tests Préliminaires**
- [ ] Test encryption sur DB développement réussi
- [ ] GdprEncryptionTest.RunAllTests() passé (3/3)
- [ ] Backup/restore testé (< 10 min)
- [ ] Application démarre après encryption

---

## 🚀 PROCÉDURE MIGRATION (STEP-BY-STEP)

### **ÉTAPE 1: BACKUP AUTOMATIQUE (5-15 min)**

**Exécuter le script SQL de migration:**

```sql
-- Dans SQL Server Management Studio (SSMS)
USE master;
GO

-- Ouvrir et exécuter:
-- MIGRATION_GDPR_ENCRYPTION.sql
```

**Ce script va:**
1. ✅ Vérifier version SQL Server
2. ✅ Compter records à migrer
3. ✅ Créer backup complet automatiquement
4. ✅ Créer table `GdprMigrationLog` pour audit
5. ✅ Afficher instructions pour étapes suivantes

**Résultat attendu:**
```
✅ Backup créé avec succès!
Backup path: C:\Backups\Pos_PreEncryption_20260610_143025.bak
```

**⚠️ NOTER LE CHEMIN BACKUP** - vous en aurez besoin pour rollback !

---

### **ÉTAPE 2: DRY RUN (SIMULATION) (2-5 min)**

**Ouvrir Visual Studio → ouvrir `Program.cs` et ajouter temporairement:**

```csharp
// Dans Program.cs, AVANT Application.Run(new MainFrm())
// TEST: Dry run migration (simulation)
Console.WriteLine("=== GDPR MIGRATION DRY RUN ===");
var dryRunResult = Pos.Function.GdprDataMigration.MigrateAllData(
    dryRun: true,    // Simulation uniquement
    batchSize: 100
);

string report = Pos.Function.GdprDataMigration.GenerateReport(dryRunResult);
Console.WriteLine(report);

// Attendre confirmation utilisateur
Console.WriteLine("\nDry run completed. Press ENTER to exit...");
Console.ReadLine();
return; // Ne pas lancer l'application
```

**Exécuter l'application (F5 ou Ctrl+F5)**

**Résultat attendu:**
```
╔═══════════════════════════════════════════════════════════╗
║       GDPR DATA MIGRATION REPORT                         ║
╠═══════════════════════════════════════════════════════════╣
║ Mode: DRY RUN (simulation)                               ║
║ Duration: 2.34 seconds                                   ║
╠═══════════════════════════════════════════════════════════╣
║ Customers migrated:          523                          ║
║ Employees migrated:           45                          ║
║ Users migrated:               12                          ║
║ EmployeeHealths migrated:      3                          ║
║ TOTAL RECORDS MIGRATED:      583                          ║
╠═══════════════════════════════════════════════════════════╣
║ STATUS: ✅ SUCCESS                                       ║
╚═══════════════════════════════════════════════════════════╝
```

**✅ Si SUCCESS:** Continuer ÉTAPE 3  
**❌ Si FAILED:** Corriger erreur avant migration réelle

---

### **ÉTAPE 3: MIGRATION RÉELLE (10-60 min)**

**⚠️ POINT DE NON-RETOUR**

**Modifier Program.cs:**

```csharp
// MIGRATION PRODUCTION (RÉELLE)
Console.WriteLine("=== GDPR MIGRATION PRODUCTION ===");
Console.WriteLine("⚠️  WARNING: This will ENCRYPT all sensitive data!");
Console.WriteLine("Press Y to confirm, any other key to cancel...");

var key = Console.ReadKey();
if (key.Key != ConsoleKey.Y)
{
    Console.WriteLine("\nMigration cancelled.");
    return;
}

Console.WriteLine("\n\nStarting production migration...");

var result = Pos.Function.GdprDataMigration.MigrateAllData(
    dryRun: false,   // ⚠️ PRODUCTION MODE
    batchSize: 100   // Ajuster selon RAM disponible (100-1000)
);

string report = Pos.Function.GdprDataMigration.GenerateReport(result);
Console.WriteLine(report);

// Sauvegarder rapport
System.IO.File.WriteAllText(
    $"C:\\Backups\\Migration_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
    report
);

Console.WriteLine("\nPress ENTER to exit...");
Console.ReadLine();
return;
```

**Exécuter l'application:**

```bash
# Depuis répertoire Pos/Pos/
dotnet run --configuration Release
```

**OU via Visual Studio: Ctrl+F5 (Start Without Debugging)**

**Progression attendue:**
```
Starting production migration...
Customers migration: 100 processed, 98 encrypted
Customers migration: 200 processed, 195 encrypted
Customers migration: 300 processed, 290 encrypted
...
Customers migration: 523 processed, 520 encrypted
Employees migration: 45 processed, 45 encrypted
...

╔═══════════════════════════════════════════════════════════╗
║ TOTAL RECORDS MIGRATED:      583                          ║
║ STATUS: ✅ SUCCESS                                       ║
╚═══════════════════════════════════════════════════════════╝

Report saved to: C:\Backups\Migration_Report_20260610_144520.txt
```

**Durée attendue:**
- < 1000 records: 5-10 min
- 1000-10000 records: 10-30 min
- > 10000 records: 30-120 min

---

### **ÉTAPE 4: VÉRIFICATION POST-MIGRATION (5 min)**

**4A. Vérifier encryption en DB (SQL):**

```sql
USE [Pos];

-- Vérifier Customer emails encryptés
SELECT TOP 10
    Id,
    LEFT(Email, 50) as EncryptedEmail,
    LEN(Email) as Length,
    CASE
        WHEN Email LIKE '%@%' THEN '❌ PLAINTEXT'
        WHEN LEN(Email) > 50 THEN '✅ Encrypted'
        ELSE '⚠️ Uncertain'
    END as Status
FROM Customer
WHERE Email IS NOT NULL;

-- Vérifier Employee BloodGroup (Article 9)
SELECT TOP 10
    Id,
    LEFT(BloodGroup, 50) as EncryptedBloodGroup,
    CASE
        WHEN BloodGroup IN ('A+', 'B+', 'O+', 'AB+') THEN '❌ PLAINTEXT'
        WHEN LEN(BloodGroup) > 20 THEN '✅ Encrypted'
        ELSE '⚠️ Uncertain'
    END as Status
FROM Employee
WHERE BloodGroup IS NOT NULL;
```

**Résultat attendu:**
```
Id  | EncryptedEmail                                 | Status
----|------------------------------------------------|------------
1   | AgECAwQFBgcICQoLDA0ODxAREhMUFRYXGBkaGxw...    | ✅ Encrypted
2   | MTIzNDU2Nzg5MDEyMzQ1Njc4OTAxMjM0NTY3ODk...    | ✅ Encrypted
```

**4B. Tester application:**

```csharp
// Retirer code migration de Program.cs
// Lancer application normalement

// Tester lecture Customer
using (var context = new AppDbContext())
{
    var customer = context.Customers.First();
    Console.WriteLine($"Email: {customer.Email}");  // Doit être décrypté automatiquement
}
```

**Résultat attendu:**
```
Email: john.doe@example.com  // Décrypté correctement
```

**4C. Exécuter test suite:**

```csharp
string testResults = Pos.Function.GdprEncryptionTest.RunAllTests();
Console.WriteLine(testResults);
```

**Résultat attendu:**
```
╔═══════════════════════════════════════════════════════════╗
║ SUMMARY: 3/3 tests passed                                ║
║ VERDICT: ✅ READY FOR PRODUCTION                         ║
╚═══════════════════════════════════════════════════════════╝
```

---

### **ÉTAPE 5: MARQUER MIGRATION COMPLÈTE (1 min)**

**Exécuter dans SSMS:**

```sql
USE [Pos];

-- Marquer migration comme réussie
UPDATE GdprMigrationLog
SET
    MigrationCompleted = GETDATE(),
    Status = 'Completed',
    RecordsMigrated = (SELECT COUNT(*) FROM Customer) +
                     (SELECT COUNT(*) FROM Employee) +
                     (SELECT COUNT(*) FROM [User])
WHERE Status = 'InProgress';

-- Vérifier log
SELECT * FROM GdprMigrationLog ORDER BY Id DESC;
```

---

## 🔄 ROLLBACK (EN CAS D'ÉCHEC)

**Si migration échoue ou données corrompues:**

### **Procédure Rollback Rapide (5-10 min)**

```sql
-- 1. Fermer application Ezzipos immédiatement

-- 2. Dans SSMS
USE master;
GO

-- 3. Forcer déconnexion utilisateurs
ALTER DATABASE [Pos] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- 4. Restaurer backup
RESTORE DATABASE [Pos]
FROM DISK = 'C:\Backups\Pos_PreEncryption_20260610_143025.bak'
WITH REPLACE, RECOVERY;
GO

-- 5. Réautoriser connexions
ALTER DATABASE [Pos] SET MULTI_USER;
GO

-- 6. Marquer rollback dans log
USE [Pos];
UPDATE GdprMigrationLog
SET Status = 'RolledBack', ErrorMessage = 'Manual rollback executed'
WHERE Status = 'InProgress';
GO
```

**Vérifier restauration:**
```sql
-- Données doivent être plaintext
SELECT TOP 5 Email FROM Customer;
-- Résultat attendu: john.doe@example.com (plaintext)
```

**✅ Application fonctionnera comme avant migration**

---

## 📊 MÉTRIQUES & PERFORMANCE

### **Temps Migration Attendus**

| Records | Batch 100 | Batch 500 | Batch 1000 |
|---------|-----------|-----------|------------|
| 1,000   | 3 min     | 2 min     | 1.5 min    |
| 10,000  | 20 min    | 12 min    | 8 min      |
| 100,000 | 180 min   | 90 min    | 60 min     |

**Optimisation:**
- Plus `batchSize` est grand, plus c'est rapide
- Mais plus RAM consommée
- Recommandation: `batchSize = 100` (équilibre performance/RAM)

### **Impact Stockage**

| Champ | Avant | Après | Overhead |
|-------|-------|-------|----------|
| Email (30 chars) | 30 bytes | 68 bytes | +127% |
| Phone (15 chars) | 15 bytes | 48 bytes | +220% |
| Image (50 KB) | 51,200 bytes | 51,228 bytes | +0.05% |

**DB Size attendu:** +15-25% selon données

---

## ✅ VALIDATION FINALE

**Migration considérée réussie SI:**

- [x] Backup créé et testé restaurable
- [x] Dry run exécuté avec SUCCESS
- [x] Migration production STATUS = SUCCESS
- [x] Vérification SQL montre données encryptées (Base64)
- [x] Application démarre normalement
- [x] Customer/Employee lisibles décryptés correctement
- [x] GdprEncryptionTest.RunAllTests() = 3/3 PASSED
- [x] GdprMigrationLog.Status = 'Completed'
- [x] Aucune erreur dans logs application
- [x] Utilisateurs peuvent se connecter
- [x] Transactions fonctionnent (vente test)

---

## 🔧 TROUBLESHOOTING

### **Erreur: "Failed to decrypt data"**

**Cause:** Master key perdue ou changée

**Solution:**
1. Vérifier `%APPDATA%\Ezzipos\Security\.masterkey` existe
2. Si perdu: **ROLLBACK IMMÉDIAT** (données irrécupérables)
3. Restaurer backup

### **Erreur: "Timeout expired"**

**Cause:** DB trop grande, batch trop lent

**Solution:**
1. Augmenter `batchSize` (500 ou 1000)
2. Ou diminuer si RAM limitée (50)
3. Exécuter migration en dehors heures ouvrées

### **Erreur: "Out of memory"**

**Cause:** `batchSize` trop grand

**Solution:**
1. Diminuer `batchSize` à 50
2. Fermer autres applications
3. Augmenter RAM serveur si possible

### **Application lente après migration**

**Cause:** Overhead encryption/decryption

**Solutions:**
1. Caching: Charger données fréquentes en mémoire
2. Optimiser queries: SELECT uniquement champs nécessaires
3. Pagination: Ne pas charger 10k records d'un coup
4. Index DB: Ajouter index sur champs non-encryptés (Code, Id...)

---

## 📞 SUPPORT URGENCE

**En cas de problème critique:**

1. **STOP:** Fermer application immédiatement
2. **ROLLBACK:** Restaurer backup (procédure ci-dessus)
3. **DOCUMENTER:** Capturer message erreur + logs
4. **CONTACTER:** Équipe technique avec:
   - Migration report (`C:\Backups\Migration_Report_*.txt`)
   - SQL error logs
   - Application logs
   - Backup path

**Après rollback:**
- DB retourne à état pré-migration
- Données plaintext récupérées
- Application fonctionne normalement
- Investiguer cause avant nouvelle tentative

---

## ✨ POST-MIGRATION

**Après migration réussie:**

1. ✅ **Backup:** Garder backup 30+ jours minimum
2. ✅ **Monitoring:** Surveiller performance 48h
3. ✅ **Documentation:** Marquer migration dans change log
4. ✅ **Communication:** Notifier équipe migration complète
5. ✅ **Compliance:** Mettre à jour documentation GDPR (50% → 55%)

**Prochaine étape:** Phase 4C - Audit Trail Masking

---

**🎉 Félicitations ! Vos données sensibles sont maintenant protégées par encryption AES-256-GCM conformément à l'Article 32 GDPR.**
