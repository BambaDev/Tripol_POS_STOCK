using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pos.Models;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4B.3: Migration des données existantes vers encryption
    /// Encrypte toutes les données plaintext en production de manière sécurisée
    /// </summary>
    public static class GdprDataMigration
    {
        /// <summary>
        /// Migre toutes les entités vers encryption
        /// ATTENTION: Opération lourde - à exécuter en maintenance
        /// </summary>
        public static MigrationResult MigrateAllData(bool dryRun = true, int batchSize = 100)
        {
            var result = new MigrationResult
            {
                StartTime = DateTime.Now,
                IsDryRun = dryRun
            };

            try
            {
                using (var context = new AppDbContext())
                {
                    // Désactiver change tracking pour performance
                    context.ChangeTracker.AutoDetectChangesEnabled = false;
                    context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                    // Migrer chaque type d'entité
                    result.CustomersMigrated = MigrateCustomers(context, dryRun, batchSize);
                    result.EmployeesMigrated = MigrateEmployees(context, dryRun, batchSize);
                    result.UsersMigrated = MigrateUsers(context, dryRun, batchSize);
                    result.EmployeeHealthsMigrated = MigrateEmployeeHealths(context, dryRun, batchSize);
                    result.SuppliersMigrated = MigrateSuppliers(context, dryRun, batchSize);
                    result.PayrollsMigrated = MigratePayrolls(context, dryRun, batchSize);
                    // Company DbSet not available - skip for now
                    result.CompaniesMigrated = 0; // MigrateCompanies(context, dryRun, batchSize);

                    result.TotalRecordsMigrated = result.CustomersMigrated +
                                                 result.EmployeesMigrated +
                                                 result.UsersMigrated +
                                                 result.EmployeeHealthsMigrated +
                                                 result.SuppliersMigrated +
                                                 result.PayrollsMigrated +
                                                 result.CompaniesMigrated;
                }

                result.EndTime = DateTime.Now;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.EndTime = DateTime.Now;
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        /// <summary>
        /// Migre les Customers
        /// </summary>
        private static int MigrateCustomers(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            int offset = 0;

            while (true)
            {
                // Charger batch
                var customers = context.Customers
                    .OrderBy(c => c.Id)
                    .Skip(offset)
                    .Take(batchSize)
                    .AsNoTracking()
                    .ToList();

                if (!customers.Any())
                    break;

                foreach (var customer in customers)
                {
                    // Vérifier si déjà encrypté (éviter double-encryption)
                    if (!IsAlreadyEncrypted(customer.Email))
                    {
                        if (!dryRun)
                        {
                            // Re-attacher entity en mode Modified
                            context.Attach(customer);
                            context.Entry(customer).State = EntityState.Modified;

                            // Les ValueConverters vont automatiquement encrypter lors du SaveChanges
                            // Pas besoin de modifier manuellement - EF Core s'en occupe !
                        }
                        migrated++;
                    }
                }

                if (!dryRun && customers.Any())
                {
                    context.SaveChanges();
                    context.ChangeTracker.Clear(); // Libérer mémoire
                }

                offset += batchSize;

                // Log progression
                System.Diagnostics.Debug.WriteLine($"Customers migration: {offset} processed, {migrated} encrypted");
            }

            return migrated;
        }

        /// <summary>
        /// Migre les Employees
        /// </summary>
        private static int MigrateEmployees(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            int offset = 0;

            while (true)
            {
                var employees = context.Employees
                    .OrderBy(e => e.Id)
                    .Skip(offset)
                    .Take(batchSize)
                    .AsNoTracking()
                    .ToList();

                if (!employees.Any())
                    break;

                foreach (var employee in employees)
                {
                    if (!IsAlreadyEncrypted(employee.Email))
                    {
                        if (!dryRun)
                        {
                            context.Attach(employee);
                            context.Entry(employee).State = EntityState.Modified;
                        }
                        migrated++;
                    }
                }

                if (!dryRun && employees.Any())
                {
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }

                offset += batchSize;
                System.Diagnostics.Debug.WriteLine($"Employees migration: {offset} processed, {migrated} encrypted");
            }

            return migrated;
        }

        /// <summary>
        /// Migre les Users
        /// </summary>
        private static int MigrateUsers(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            int offset = 0;

            while (true)
            {
                var users = context.Users
                    .OrderBy(u => u.Id)
                    .Skip(offset)
                    .Take(batchSize)
                    .AsNoTracking()
                    .ToList();

                if (!users.Any())
                    break;

                foreach (var user in users)
                {
                    if (!IsAlreadyEncrypted(user.Email))
                    {
                        if (!dryRun)
                        {
                            context.Attach(user);
                            context.Entry(user).State = EntityState.Modified;
                        }
                        migrated++;
                    }
                }

                if (!dryRun && users.Any())
                {
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }

                offset += batchSize;
                System.Diagnostics.Debug.WriteLine($"Users migration: {offset} processed, {migrated} encrypted");
            }

            return migrated;
        }

        /// <summary>
        /// Migre les EmployeeHealths (Article 9 GDPR)
        /// </summary>
        private static int MigrateEmployeeHealths(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            int offset = 0;

            while (true)
            {
                var healths = context.EmployeeHealths
                    .OrderBy(h => h.Id)
                    .Skip(offset)
                    .Take(batchSize)
                    .AsNoTracking()
                    .ToList();

                if (!healths.Any())
                    break;

                foreach (var health in healths)
                {
                    if (!IsAlreadyEncrypted(health.HealthCondition))
                    {
                        if (!dryRun)
                        {
                            context.Attach(health);
                            context.Entry(health).State = EntityState.Modified;
                        }
                        migrated++;
                    }
                }

                if (!dryRun && healths.Any())
                {
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }

                offset += batchSize;
            }

            return migrated;
        }

        /// <summary>
        /// Migre les Suppliers
        /// </summary>
        private static int MigrateSuppliers(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            var suppliers = context.Suppliers.AsNoTracking().ToList();

            foreach (var supplier in suppliers)
            {
                if (!IsAlreadyEncrypted(supplier.Email))
                {
                    if (!dryRun)
                    {
                        context.Attach(supplier);
                        context.Entry(supplier).State = EntityState.Modified;
                    }
                    migrated++;
                }
            }

            if (!dryRun && suppliers.Any())
            {
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }

            return migrated;
        }

        /// <summary>
        /// Migre les Payrolls
        /// </summary>
        private static int MigratePayrolls(AppDbContext context, bool dryRun, int batchSize)
        {
            int migrated = 0;
            var payrolls = context.Payrolls.AsNoTracking().ToList();

            foreach (var payroll in payrolls)
            {
                // Payroll a des decimal encryptés, pas de string - toujours migrer
                if (!dryRun)
                {
                    context.Attach(payroll);
                    context.Entry(payroll).State = EntityState.Modified;
                }
                migrated++;
            }

            if (!dryRun && payrolls.Any())
            {
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }

            return migrated;
        }

        /// <summary>
        /// Migre les Companies
        /// Note: Company DbSet not available in AppDbContext - skip for now
        /// </summary>
        private static int MigrateCompanies(AppDbContext context, bool dryRun, int batchSize)
        {
            // TODO: Enable when Company DbSet is available
            return 0;
        }

        /// <summary>
        /// Vérifie si une valeur est déjà encryptée (Base64 format)
        /// </summary>
        private static bool IsAlreadyEncrypted(string value)
        {
            if (string.IsNullOrEmpty(value))
                return true; // Null/empty considéré comme "déjà migré"

            // Vérifier si Base64 (format encryption)
            return FieldEncryption.IsEncrypted(value);
        }

        /// <summary>
        /// Génère un rapport de migration
        /// </summary>
        public static string GenerateReport(MigrationResult result)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("╔═══════════════════════════════════════════════════════════╗");
            report.AppendLine("║       GDPR DATA MIGRATION REPORT                         ║");
            report.AppendLine("╠═══════════════════════════════════════════════════════════╣");
            report.AppendLine($"║ Mode: {(result.IsDryRun ? "DRY RUN (simulation)" : "PRODUCTION (real)")}                    ║");
            report.AppendLine($"║ Start: {result.StartTime:yyyy-MM-dd HH:mm:ss}                           ║");
            report.AppendLine($"║ End:   {result.EndTime:yyyy-MM-dd HH:mm:ss}                           ║");
            report.AppendLine($"║ Duration: {result.Duration.TotalSeconds:F2} seconds                            ║");
            report.AppendLine("╠═══════════════════════════════════════════════════════════╣");
            report.AppendLine($"║ Customers migrated:       {result.CustomersMigrated,6}                    ║");
            report.AppendLine($"║ Employees migrated:       {result.EmployeesMigrated,6}                    ║");
            report.AppendLine($"║ Users migrated:           {result.UsersMigrated,6}                    ║");
            report.AppendLine($"║ EmployeeHealths migrated: {result.EmployeeHealthsMigrated,6}                    ║");
            report.AppendLine($"║ Suppliers migrated:       {result.SuppliersMigrated,6}                    ║");
            report.AppendLine($"║ Payrolls migrated:        {result.PayrollsMigrated,6}                    ║");
            report.AppendLine($"║ Companies migrated:       {result.CompaniesMigrated,6}                    ║");
            report.AppendLine("╠═══════════════════════════════════════════════════════════╣");
            report.AppendLine($"║ TOTAL RECORDS MIGRATED:   {result.TotalRecordsMigrated,6}                    ║");
            report.AppendLine("╠═══════════════════════════════════════════════════════════╣");

            if (result.Success)
            {
                report.AppendLine("║ STATUS: ✅ SUCCESS                                       ║");
            }
            else
            {
                report.AppendLine("║ STATUS: ❌ FAILED                                        ║");
                string errorMsg = result.ErrorMessage ?? "Unknown error";
                report.AppendLine($"║ Error: {errorMsg.Substring(0, Math.Min(40, errorMsg.Length))}");
            }

            report.AppendLine("╚═══════════════════════════════════════════════════════════╝");

            return report.ToString();
        }
    }

    /// <summary>
    /// Résultat de la migration
    /// </summary>
    public class MigrationResult
    {
        public bool Success { get; set; }
        public bool IsDryRun { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration => EndTime - StartTime;

        public int CustomersMigrated { get; set; }
        public int EmployeesMigrated { get; set; }
        public int UsersMigrated { get; set; }
        public int EmployeeHealthsMigrated { get; set; }
        public int SuppliersMigrated { get; set; }
        public int PayrollsMigrated { get; set; }
        public int CompaniesMigrated { get; set; }

        public int TotalRecordsMigrated { get; set; }

        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
