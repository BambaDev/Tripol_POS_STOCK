-- ══════════════════════════════════════════════════════════════════════════
-- PHASE 4B.3: MIGRATION PRODUCTION - ENCRYPTION DONNÉES SENSIBLES
-- ══════════════════════════════════════════════════════════════════════════
-- Date: 2026-06-10
-- Version: 1.0.0
-- CRITIQUE: Backup automatique avant migration
-- Durée estimée: 5-30 minutes selon taille DB
-- ══════════════════════════════════════════════════════════════════════════

-- ┌──────────────────────────────────────────────────────────────────────────┐
-- │ ÉTAPE 0: VÉRIFICATIONS PRÉ-MIGRATION                                     │
-- └──────────────────────────────────────────────────────────────────────────┘

USE [Pos];  -- Ou [ezzipos] selon votre nom de DB
GO

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'GDPR ENCRYPTION MIGRATION - PRE-FLIGHT CHECKS';
PRINT '══════════════════════════════════════════════════════════════';
PRINT '';

-- Vérifier version SQL Server (minimum SQL Server 2016)
DECLARE @version INT = CAST(SERVERPROPERTY('ProductMajorVersion') AS INT);
IF @version < 13
BEGIN
    RAISERROR('SQL Server 2016 ou supérieur requis pour encryption', 16, 1);
    RETURN;
END
PRINT '✓ SQL Server version OK';

-- Vérifier espace disque disponible (minimum 2x taille DB actuelle)
DECLARE @dbSize BIGINT;
SELECT @dbSize = SUM(size) * 8 / 1024 FROM sys.database_files;
PRINT CONCAT('✓ Database size: ', @dbSize, ' MB');

-- Compter enregistrements sensibles
DECLARE @customerCount INT = (SELECT COUNT(*) FROM Customer);
DECLARE @employeeCount INT = (SELECT COUNT(*) FROM Employee);
DECLARE @userCount INT = (SELECT COUNT(*) FROM [User]);
DECLARE @totalRecords INT = @customerCount + @employeeCount + @userCount;

PRINT CONCAT('✓ Records to migrate:');
PRINT CONCAT('  - Customers: ', @customerCount);
PRINT CONCAT('  - Employees: ', @employeeCount);
PRINT CONCAT('  - Users: ', @userCount);
PRINT CONCAT('  - TOTAL: ', @totalRecords);
PRINT '';

-- Vérifier backup existe
DECLARE @backupPath NVARCHAR(500) = 'C:\Backups\';
DECLARE @backupFile NVARCHAR(500) = CONCAT(@backupPath, 'Pos_PreEncryption_', FORMAT(GETDATE(), 'yyyyMMdd_HHmmss'), '.bak');

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'ÉTAPE 1: BACKUP AUTOMATIQUE';
PRINT '══════════════════════════════════════════════════════════════';
PRINT CONCAT('Backup path: ', @backupFile);
PRINT 'Création backup... (peut prendre plusieurs minutes)';

-- Créer backup FULL
BACKUP DATABASE [Pos]
TO DISK = @backupFile
WITH
    COMPRESSION,
    INIT,
    NAME = 'Pre-Encryption Backup',
    DESCRIPTION = 'Backup automatique avant migration GDPR encryption',
    STATS = 10;

PRINT '✅ Backup créé avec succès!';
PRINT '';

-- ┌──────────────────────────────────────────────────────────────────────────┐
-- │ ÉTAPE 2: CRÉER TABLE MIGRATION LOG                                      │
-- └──────────────────────────────────────────────────────────────────────────┘

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'GdprMigrationLog')
BEGIN
    CREATE TABLE GdprMigrationLog (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MigrationStarted DATETIME2 NOT NULL DEFAULT GETDATE(),
        MigrationCompleted DATETIME2 NULL,
        Status NVARCHAR(50) NOT NULL,  -- 'InProgress', 'Completed', 'Failed', 'RolledBack'
        RecordsMigrated INT NULL,
        ErrorMessage NVARCHAR(MAX) NULL,
        BackupPath NVARCHAR(500) NULL
    );
    PRINT '✓ Migration log table created';
END

-- Enregistrer début migration
DECLARE @migrationId INT;
INSERT INTO GdprMigrationLog (Status, BackupPath)
VALUES ('InProgress', @backupFile);
SET @migrationId = SCOPE_IDENTITY();
PRINT CONCAT('✓ Migration ID: ', @migrationId);
PRINT '';

-- ┌──────────────────────────────────────────────────────────────────────────┐
-- │ ÉTAPE 3: MIGRATION DONNÉES (VIA APPLICATION C#)                         │
-- └──────────────────────────────────────────────────────────────────────────┘

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'ÉTAPE 2: MIGRATION DONNÉES';
PRINT '══════════════════════════════════════════════════════════════';
PRINT '';
PRINT '⚠️  IMPORTANT: Migration doit être exécutée depuis l''application C#';
PRINT '';
PRINT 'Utiliser le code suivant dans l''application:';
PRINT '';
PRINT '```csharp';
PRINT 'using Pos.Function;';
PRINT '';
PRINT '// 1. DRY RUN d''abord (simulation)';
PRINT 'var dryRunResult = GdprDataMigration.MigrateAllData(dryRun: true);';
PRINT 'Console.WriteLine(GdprDataMigration.GenerateReport(dryRunResult));';
PRINT '';
PRINT '// 2. Si DRY RUN OK, migration réelle';
PRINT 'var result = GdprDataMigration.MigrateAllData(dryRun: false, batchSize: 100);';
PRINT 'Console.WriteLine(GdprDataMigration.GenerateReport(result));';
PRINT '```';
PRINT '';
PRINT 'OU utiliser l''outil GUI migration (recommandé):';
PRINT 'Main Menu → Tools → GDPR Data Migration';
PRINT '';

-- ┌──────────────────────────────────────────────────────────────────────────┐
-- │ ÉTAPE 4: VÉRIFICATION POST-MIGRATION                                    │
-- └──────────────────────────────────────────────────────────────────────────┘

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'ÉTAPE 3: VÉRIFICATION POST-MIGRATION';
PRINT '══════════════════════════════════════════════════════════════';
PRINT '';
PRINT 'Après migration C#, exécuter ces vérifications:';
PRINT '';

-- Script vérification (à exécuter APRÈS migration)
/*
-- Vérifier qu'emails sont encryptés (Base64 format)
SELECT TOP 10
    Id,
    LEFT(Email, 40) + '...' as EncryptedEmail,
    LEN(Email) as EmailLength,
    CASE
        WHEN Email LIKE '%@%' THEN '❌ PLAINTEXT'
        WHEN LEN(Email) > 50 THEN '✓ Encrypted (Base64)'
        ELSE '⚠️  Unknown'
    END as Status
FROM Customer
WHERE Email IS NOT NULL;

-- Vérifier BloodGroup encrypté (Article 9 GDPR)
SELECT TOP 10
    Id,
    LEFT(BloodGroup, 40) as EncryptedBloodGroup,
    CASE
        WHEN BloodGroup IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-') THEN '❌ PLAINTEXT'
        WHEN LEN(BloodGroup) > 20 THEN '✓ Encrypted'
        ELSE '⚠️  Unknown'
    END as Status
FROM Employee
WHERE BloodGroup IS NOT NULL;

-- Compter records avec données encryptées vs plaintext
SELECT
    'Customer' as Entity,
    COUNT(*) as Total,
    SUM(CASE WHEN Email NOT LIKE '%@%' AND LEN(Email) > 50 THEN 1 ELSE 0 END) as Encrypted,
    SUM(CASE WHEN Email LIKE '%@%' THEN 1 ELSE 0 END) as Plaintext
FROM Customer
WHERE Email IS NOT NULL
UNION ALL
SELECT
    'Employee',
    COUNT(*),
    SUM(CASE WHEN Email NOT LIKE '%@%' AND LEN(Email) > 50 THEN 1 ELSE 0 END),
    SUM(CASE WHEN Email LIKE '%@%' THEN 1 ELSE 0 END)
FROM Employee
WHERE Email IS NOT NULL
UNION ALL
SELECT
    '[User]',
    COUNT(*),
    SUM(CASE WHEN Email NOT LIKE '%@%' AND LEN(Email) > 50 THEN 1 ELSE 0 END),
    SUM(CASE WHEN Email LIKE '%@%' THEN 1 ELSE 0 END)
FROM [User]
WHERE Email IS NOT NULL;
*/

PRINT 'Queries de vérification disponibles dans ce fichier (commentées)';
PRINT '';

-- ┌──────────────────────────────────────────────────────────────────────────┐
-- │ ÉTAPE 5: ROLLBACK (EN CAS D''ÉCHEC UNIQUEMENT)                          │
-- └──────────────────────────────────────────────────────────────────────────┘

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'ROLLBACK PROCEDURE (si migration échoue)';
PRINT '══════════════════════════════════════════════════════════════';
PRINT '';
PRINT 'En cas d''échec, restaurer le backup:';
PRINT '';
PRINT '1. Fermer application Ezzipos';
PRINT '2. Exécuter:';
PRINT '';
PRINT 'USE master;';
PRINT 'GO';
PRINT 'ALTER DATABASE [Pos] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;';
PRINT 'GO';
PRINT CONCAT('RESTORE DATABASE [Pos] FROM DISK = ''', @backupFile, '''');
PRINT '    WITH REPLACE, RECOVERY;';
PRINT 'GO';
PRINT 'ALTER DATABASE [Pos] SET MULTI_USER;';
PRINT 'GO';
PRINT '';

-- Marquer migration comme complétée (à décommenter APRÈS migration réussie)
/*
UPDATE GdprMigrationLog
SET
    MigrationCompleted = GETDATE(),
    Status = 'Completed',
    RecordsMigrated = @totalRecords
WHERE Id = @migrationId;
*/

PRINT '══════════════════════════════════════════════════════════════';
PRINT 'MIGRATION SCRIPT COMPLET';
PRINT '══════════════════════════════════════════════════════════════';
PRINT '';
PRINT '✅ Backup créé avec succès';
PRINT '⏭️  Suivant: Exécuter migration depuis application C#';
PRINT '';
PRINT 'IMPORTANT:';
PRINT '1. Backup path: ' + @backupFile;
PRINT '2. Garder backup pendant 30 jours minimum';
PRINT '3. Tester application après migration';
PRINT '4. Si problème: restaurer backup immédiatement';
PRINT '';
PRINT '══════════════════════════════════════════════════════════════';

GO
