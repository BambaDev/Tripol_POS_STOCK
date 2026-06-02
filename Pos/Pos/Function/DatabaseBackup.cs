using Pos.Forms.Alert;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Function
{
    internal class DatabaseBackup
    {
        public static async Task<string> CreateBackupAsync()
        {
            using (var context = new AppDbContext())
            {
                string backupFolderPath = @"C:\Backups";
                Directory.CreateDirectory(backupFolderPath);  // Ensure the directory exists

                // Generate a unique file name with a timestamp
                string backupFileName = $"Backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
                string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

                try
                {
                    // Start the backup process asynchronously
                    await Task.Run(() => DatabaseUtility.BackupDatabaseAsync(context, backupFilePath));  // Assuming BackupDatabase is a sync method

                    // Ensure the backup file is fully written before proceeding
                    WaitForFile(backupFilePath);

                    Console.WriteLine("Backup completed successfully.");
                    return backupFilePath;  // Return the path of the created backup file
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during the backup: {ex.Message}");
                    return null;  // Return null in case of failure
                }
            }
        }


        public static void WaitForFile(string filePath)
        {
            while (true)
            {
                try
                {
                    // Step 1: Try opening the file with FileShare.None, meaning no other process can access it
                    using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        if (stream.Length > 0)
                        {
                            break;  // Exit the loop if the file is accessible and non-zero in size
                        }
                    }
                }
                catch (IOException)
                {
                    // Step 2: File is still being used by another process, retry after 1 second
                    Console.WriteLine("Waiting for the file to be released...");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public static string BackupDatabase(string backupFilePath)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Ensure the directory for backup exists
                    var directory = Path.GetDirectoryName(backupFilePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Get the database name from the DbContext connection
                    var dbName = context.Database.GetDbConnection().Database;

                    // SÉCURISÉ: Validation du chemin de backup
                    if (string.IsNullOrWhiteSpace(backupFilePath))
                        throw new ArgumentException("Backup file path cannot be empty");

                    // Valider que le chemin se termine par .bak
                    if (!backupFilePath.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
                        throw new ArgumentException("Backup file must have .bak extension");

                    // Échapper les guillemets simples (protection SQL injection)
                    string sanitizedPath = backupFilePath.Replace("'", "''");

                    // SQL Server backup query
                    string backupQuery = $@"BACKUP DATABASE [{dbName}]
                                        TO DISK = '{sanitizedPath}'
                                        WITH FORMAT, INIT, SKIP, STATS = 10";

                    // Execute the backup command using ExecuteSqlRaw
                    context.Database.ExecuteSqlRaw(backupQuery);

                    Console.WriteLine("Database backup completed successfully.");
                    return backupFilePath;  // Return the path of the backup file
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during backup: " + ex.Message);
                return null;
            }
        }
    }
}
