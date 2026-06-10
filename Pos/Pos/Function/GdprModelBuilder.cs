using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4B RGPD: Helper pour configuration automatique de l'encryption dans EF Core
    /// Scanne tous les modèles pour [SensitiveData(RequiresEncryption=true)]
    /// et applique automatiquement les ValueConverters appropriés
    /// </summary>
    public static class GdprModelBuilder
    {
        /// <summary>
        /// Configure l'encryption automatique pour toutes les propriétés marquées [SensitiveData]
        /// À appeler dans AppDbContext.OnModelCreating()
        /// </summary>
        /// <param name="modelBuilder">EF Core ModelBuilder</param>
        /// <param name="assemblyToScan">Assembly contenant les modèles (optionnel, utilise assembly appelant par défaut)</param>
        public static void ApplyGdprEncryption(this ModelBuilder modelBuilder, Assembly assemblyToScan = null)
        {
            if (assemblyToScan == null)
            {
                // Utiliser l'assembly des modèles (Pos.Models)
                assemblyToScan = typeof(Pos.Models.Customer).Assembly;
            }

            // Obtenir tous les types entity du model
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            int encryptedPropertiesCount = 0;

            foreach (var entityType in entityTypes)
            {
                var clrType = entityType.ClrType;

                // Obtenir toutes les propriétés du type
                var properties = clrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    // Vérifier si la propriété a l'attribut [SensitiveData]
                    var sensitiveAttr = property.GetCustomAttribute<SensitiveDataAttribute>();

                    if (sensitiveAttr == null)
                        continue;

                    // Vérifier si l'encryption est requise
                    if (!sensitiveAttr.RequiresEncryption)
                        continue;

                    // Vérifier si la propriété a [NoEncryption] (override)
                    var noEncryptionAttr = property.GetCustomAttribute<NoEncryptionAttribute>();
                    if (noEncryptionAttr != null)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"GDPR: Skipping {clrType.Name}.{property.Name} - marked [NoEncryption]: {noEncryptionAttr.Reason}");
                        continue;
                    }

                    // Appliquer le converter approprié selon le type
                    bool converterApplied = ApplyConverterForProperty(modelBuilder, entityType, property);

                    if (converterApplied)
                    {
                        encryptedPropertiesCount++;
                        System.Diagnostics.Debug.WriteLine(
                            $"GDPR: Encryption enabled for {clrType.Name}.{property.Name} " +
                            $"(Level: {sensitiveAttr.Level}, Category: {sensitiveAttr.Category})");
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine($"GDPR: Applied encryption to {encryptedPropertiesCount} properties across {entityTypes.Count()} entities");
        }

        /// <summary>
        /// Applique le ValueConverter approprié selon le type de propriété
        /// </summary>
        private static bool ApplyConverterForProperty(
            ModelBuilder modelBuilder,
            Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType,
            PropertyInfo property)
        {
            var propertyType = property.PropertyType;

            // String
            if (propertyType == typeof(string))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(new EncryptedStringConverter());
                return true;
            }

            // Byte array (images, documents)
            if (propertyType == typeof(byte[]))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(new EncryptedBytesConverter());
                return true;
            }

            // DateTime nullable
            if (propertyType == typeof(DateTime?))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(new EncryptedDateTimeConverter());
                return true;
            }

            // Decimal nullable
            if (propertyType == typeof(decimal?))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(new EncryptedDecimalConverter());
                return true;
            }

            // Int nullable
            if (propertyType == typeof(int?))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(new EncryptedIntConverter());
                return true;
            }

            // Type non supporté
            System.Diagnostics.Debug.WriteLine(
                $"GDPR WARNING: No converter available for {entityType.ClrType.Name}.{property.Name} " +
                $"of type {propertyType.Name}. Property marked for encryption but will remain plaintext.");
            return false;
        }

        /// <summary>
        /// Obtient les statistiques d'encryption pour un DbContext
        /// Utile pour compliance reporting
        /// </summary>
        public static GdprEncryptionStats GetEncryptionStats(ModelBuilder modelBuilder)
        {
            var stats = new GdprEncryptionStats();
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entityTypes)
            {
                var clrType = entityType.ClrType;
                var properties = clrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                stats.TotalEntities++;

                foreach (var property in properties)
                {
                    var sensitiveAttr = property.GetCustomAttribute<SensitiveDataAttribute>();

                    if (sensitiveAttr != null)
                    {
                        stats.TotalSensitiveProperties++;

                        if (sensitiveAttr.RequiresEncryption)
                        {
                            stats.TotalEncryptedProperties++;

                            // Compter par niveau de sensibilité
                            switch (sensitiveAttr.Level)
                            {
                                case SensitivityLevel.Restricted:
                                    stats.RestrictedProperties++;
                                    break;
                                case SensitivityLevel.Classified:
                                    stats.ClassifiedProperties++;
                                    break;
                                case SensitivityLevel.Confidential:
                                    stats.ConfidentialProperties++;
                                    break;
                            }

                            // Compter par catégorie
                            if (sensitiveAttr.Category == DataCategory.Health)
                                stats.HealthDataProperties++;
                            else if (sensitiveAttr.Category == DataCategory.Financial)
                                stats.FinancialDataProperties++;
                            else if (sensitiveAttr.Category == DataCategory.Biometric)
                                stats.BiometricDataProperties++;
                        }
                    }
                }
            }

            stats.EncryptionCoveragePercent = stats.TotalSensitiveProperties > 0
                ? (int)((double)stats.TotalEncryptedProperties / stats.TotalSensitiveProperties * 100)
                : 0;

            return stats;
        }

        /// <summary>
        /// Génère un rapport d'audit GDPR pour logging
        /// </summary>
        public static string GenerateAuditReport(ModelBuilder modelBuilder)
        {
            var stats = GetEncryptionStats(modelBuilder);

            return $@"
╔══════════════════════════════════════════════════════════════╗
║           GDPR ENCRYPTION AUDIT REPORT                       ║
╠══════════════════════════════════════════════════════════════╣
║ Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}                              ║
║                                                              ║
║ ENTITIES SCANNED:          {stats.TotalEntities,4}                        ║
║ SENSITIVE PROPERTIES:      {stats.TotalSensitiveProperties,4}                        ║
║ ENCRYPTED PROPERTIES:      {stats.TotalEncryptedProperties,4}                        ║
║ ENCRYPTION COVERAGE:       {stats.EncryptionCoveragePercent,3}%                        ║
║                                                              ║
║ BY SENSITIVITY LEVEL:                                        ║
║   • CLASSIFIED (Level 4):  {stats.ClassifiedProperties,4}                        ║
║   • RESTRICTED (Level 3):  {stats.RestrictedProperties,4}                        ║
║   • CONFIDENTIAL (Level 2):{stats.ConfidentialProperties,4}                        ║
║                                                              ║
║ BY DATA CATEGORY:                                            ║
║   • Health Data (Art. 9):  {stats.HealthDataProperties,4}                        ║
║   • Biometric Data (Art.9):{stats.BiometricDataProperties,4}                        ║
║   • Financial Data:        {stats.FinancialDataProperties,4}                        ║
║                                                              ║
║ COMPLIANCE STATUS:         {(stats.EncryptionCoveragePercent >= 95 ? "✓ COMPLIANT" : "✗ NON-COMPLIANT"),20} ║
╚══════════════════════════════════════════════════════════════╝
";
        }
    }

    /// <summary>
    /// Statistiques d'encryption GDPR
    /// </summary>
    public class GdprEncryptionStats
    {
        public int TotalEntities { get; set; }
        public int TotalSensitiveProperties { get; set; }
        public int TotalEncryptedProperties { get; set; }
        public int EncryptionCoveragePercent { get; set; }

        public int ClassifiedProperties { get; set; }
        public int RestrictedProperties { get; set; }
        public int ConfidentialProperties { get; set; }

        public int HealthDataProperties { get; set; }
        public int BiometricDataProperties { get; set; }
        public int FinancialDataProperties { get; set; }

        public bool IsCompliant => EncryptionCoveragePercent >= 95;
    }
}
