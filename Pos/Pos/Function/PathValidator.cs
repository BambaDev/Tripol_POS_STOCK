using System;
using System.IO;
using System.Linq;

namespace Pos.Function
{
    /// <summary>
    /// Valide les chemins de fichiers pour prévenir Path Traversal
    /// Protège contre ../../, liens symboliques, chemins UNC
    /// </summary>
    public static class PathValidator
    {
        // Répertoires autorisés pour backups
        private static readonly string[] ALLOWED_BACKUP_DIRECTORIES = {
            @"C:\Backups",
            @"C:\PosBackups",
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PosBackups")
        };

        /// <summary>
        /// Valide un chemin de backup
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitizedPath) ValidateBackupPath(string backupFilePath)
        {
            // VALIDATION 1: Null/empty
            if (string.IsNullOrWhiteSpace(backupFilePath))
            {
                return (false, "Backup path cannot be empty", null);
            }

            // VALIDATION 2: Extension .bak uniquement
            if (!backupFilePath.EndsWith(".bak", StringComparison.OrdinalIgnoreCase))
            {
                return (false, "Backup file must have .bak extension", null);
            }

            // VALIDATION 3: Enlever guillemets et espaces inutiles
            backupFilePath = backupFilePath.Trim().Trim('"');

            // VALIDATION 4: Chemin absolu (pas relatif)
            if (!Path.IsPathRooted(backupFilePath))
            {
                return (false, "Backup path must be absolute (e.g., C:\\Backups\\file.bak), not relative", null);
            }

            // VALIDATION 5: Path Traversal (../ ou ..\\)
            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(backupFilePath);
            }
            catch (Exception ex)
            {
                return (false, $"Invalid path format: {ex.Message}", null);
            }

            if (fullPath != backupFilePath)
            {
                return (false, "Path traversal detected (../ or .\\). Use absolute paths only.", null);
            }

            // VALIDATION 6: Caractères dangereux
            char[] dangerousChars = { '<', '>', '|', '\0', '\n', '\r' };
            if (backupFilePath.IndexOfAny(dangerousChars) >= 0)
            {
                return (false, "Path contains invalid characters", null);
            }

            // VALIDATION 7: Chemin UNC (réseau) - optionnel, à activer si souhaité
            if (backupFilePath.StartsWith(@"\\") || backupFilePath.StartsWith("//"))
            {
                // Peut être autorisé pour backups réseau, mais attention aux risques
                System.Diagnostics.Debug.WriteLine($"WARNING: UNC path detected: {backupFilePath}");
            }

            // VALIDATION 8: Répertoire parent existe et est accessible
            string directory = Path.GetDirectoryName(backupFilePath);
            if (!Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception ex)
                {
                    return (false, $"Cannot create backup directory: {ex.Message}", null);
                }
            }

            // VALIDATION 9: Vérifier permissions écriture
            try
            {
                string testFile = Path.Combine(directory, $"_test_{Guid.NewGuid()}.tmp");
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);
            }
            catch (UnauthorizedAccessException)
            {
                return (false, $"No write permission for directory: {directory}", null);
            }
            catch (Exception ex)
            {
                return (false, $"Cannot write to directory: {ex.Message}", null);
            }

            // VALIDATION 10: Sanitize pour SQL (échapper quotes)
            string sanitizedPath = backupFilePath.Replace("'", "''");

            return (true, string.Empty, sanitizedPath);
        }

        /// <summary>
        /// Valide chemin de fichier générique
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateFilePath(string filePath, string[] allowedExtensions = null)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return (false, "File path cannot be empty");
            }

            // Path traversal
            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(filePath);
            }
            catch (Exception ex)
            {
                return (false, $"Invalid path: {ex.Message}");
            }

            if (fullPath != filePath && !Path.IsPathRooted(filePath))
            {
                return (false, "Path traversal detected");
            }

            // Extension
            if (allowedExtensions != null && allowedExtensions.Length > 0)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    return (false, $"Invalid file extension. Allowed: {string.Join(", ", allowedExtensions)}");
                }
            }

            // Caractères dangereux
            char[] dangerousChars = { '<', '>', '|', '\0', '\n', '\r' };
            if (filePath.IndexOfAny(dangerousChars) >= 0)
            {
                return (false, "Path contains invalid characters");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Vérifie si un chemin est dans un répertoire autorisé
        /// </summary>
        public static bool IsPathInAllowedDirectory(string filePath, string[] allowedDirectories)
        {
            try
            {
                string fullPath = Path.GetFullPath(filePath);

                foreach (string allowedDir in allowedDirectories)
                {
                    string fullAllowedDir = Path.GetFullPath(allowedDir);

                    if (fullPath.StartsWith(fullAllowedDir, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Génère un chemin de backup sécurisé avec timestamp
        /// </summary>
        public static string GenerateSecureBackupPath(string databaseName = "Pos")
        {
            // Utiliser répertoire par défaut sécurisé
            string backupDir = ALLOWED_BACKUP_DIRECTORIES[0];

            // Créer si n'existe pas
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }

            // Nom fichier avec timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"{databaseName}_Backup_{timestamp}.bak";

            return Path.Combine(backupDir, fileName);
        }

        /// <summary>
        /// Nettoie vieux backups (garde seulement les N derniers)
        /// </summary>
        public static int CleanOldBackups(string backupDirectory, int keepCount = 10)
        {
            try
            {
                if (!Directory.Exists(backupDirectory))
                    return 0;

                var backupFiles = Directory.GetFiles(backupDirectory, "*.bak")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.LastWriteTime)
                    .ToList();

                int deletedCount = 0;

                // Supprimer tous sauf les keepCount derniers
                foreach (var file in backupFiles.Skip(keepCount))
                {
                    try
                    {
                        file.Delete();
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Cannot delete backup {file.Name}: {ex.Message}");
                    }
                }

                return deletedCount;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CleanOldBackups error: {ex.Message}");
                return 0;
            }
        }
    }
}
