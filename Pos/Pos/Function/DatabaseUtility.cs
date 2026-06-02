using Pos.Models;
using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pos.Function
{
    internal class DatabaseUtility
    {
        public static async Task BackupDatabaseAsync(AppDbContext context, string backupFilePath,bool showMessage = false)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            string databaseName = GetDatabaseName(connectionString);
            string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}'";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(backupQuery, connection))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            if(showMessage)
            {
                MessageBox.Show($"Database backup completed successfully to {backupFilePath}", "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static async Task RestoreDatabaseAsync(AppDbContext context, string backupFilePath, bool showMessage = false)
        {
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            string databaseName = GetDatabaseName(connectionString);
            string restoreQuery = $@"
            USE master;
            ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{databaseName}] FROM DISK = '{backupFilePath}' WITH REPLACE;
            ALTER DATABASE [{databaseName}] SET MULTI_USER;";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(restoreQuery, connection))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            if (showMessage)
            {
                MessageBox.Show($"Database restore completed successfully from {backupFilePath}", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static string GetDatabaseName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

    }
}
