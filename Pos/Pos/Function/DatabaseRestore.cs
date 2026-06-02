using Pos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    internal class DatabaseRestore
    {
        public static async Task<bool> RestoreDatabaseAsync(string backupFilePath)
        {
            try
            {
                using(var context = new AppDbContext())
                {

                    if (!File.Exists(backupFilePath))
                    {
                        Console.WriteLine("Backup file not found.");
                        return false;
                    }

                    var connectionString = context.Database.GetDbConnection().ConnectionString;
                    string databaseName = DatabaseUtility.GetDatabaseName(connectionString);

                    // Step 1: Set the database to SINGLE_USER mode to terminate active connections
                    string setSingleUserQuery = $@"
                    ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    ";

                    await context.Database.ExecuteSqlRawAsync(setSingleUserQuery);

                    // Step 2: Restore the database from the .bak file
                    // SÉCURISÉ: Validation stricte du chemin de fichier
                    if (string.IsNullOrWhiteSpace(backupFilePath))
                        throw new ArgumentException("Backup file path cannot be empty");

                    // Valider que le fichier existe et est un .bak
                    if (!System.IO.File.Exists(backupFilePath))
                        throw new System.IO.FileNotFoundException($"Backup file not found: {backupFilePath}");

                    if (!backupFilePath.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
                        throw new ArgumentException("Only .bak files are allowed");

                    // Échapper les guillemets simples pour SQL (protection contre injection)
                    string sanitizedPath = backupFilePath.Replace("'", "''");

                    string restoreQuery = $@"
                    RESTORE DATABASE [{databaseName}]
                    FROM DISK = '{sanitizedPath}'
                    WITH REPLACE, RECOVERY, STATS = 10;
                    ";

                    await context.Database.ExecuteSqlRawAsync(restoreQuery);

                    // Step 3: Set the database back to MULTI_USER mode
                    string setMultiUserQuery = $@"
                    ALTER DATABASE [{databaseName}] SET MULTI_USER;
                    ";

                    await context.Database.ExecuteSqlRawAsync(setMultiUserQuery);

                    Console.WriteLine($"Database [{databaseName}] restored successfully.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while restoring the database: {ex.Message}");
                return false;
            }
        }
    }
}
