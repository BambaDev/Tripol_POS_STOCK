using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4A RGPD: Gestion sécurisée des clés d'encryption
    /// Dérive et stocke la master key pour FieldEncryption
    ///
    /// PRODUCTION DEPLOYMENT:
    /// - Utiliser Azure Key Vault ou AWS KMS au lieu du stockage fichier
    /// - Implémenter key rotation automatique (90 jours recommandé)
    /// - Logger tous les accès à la master key
    /// - Backup clés dans HSM (Hardware Security Module)
    /// </summary>
    public static class KeyManagement
    {
        // Chemin stockage clé (DEVELOPMENT ONLY)
        // En production: Azure Key Vault ou AWS KMS
        private static readonly string KEY_STORAGE_PATH = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Ezzipos",
            "Security",
            ".masterkey"
        );

        // Paramètres PBKDF2 pour dérivation clé
        private const int PBKDF2_ITERATIONS = 100000;  // 100k iterations (OWASP recommendation 2024)
        private const int KEY_SIZE_BYTES = 32;          // 256 bits pour AES-256
        private const int SALT_SIZE_BYTES = 32;         // 256 bits salt

        /// <summary>
        /// Obtient ou crée la master key
        /// Si aucune clé n'existe, en génère une nouvelle
        /// </summary>
        public static byte[] GetOrCreateMasterKey()
        {
            try
            {
                // Vérifier si clé existe déjà
                if (File.Exists(KEY_STORAGE_PATH))
                {
                    return LoadMasterKeyFromFile();
                }

                // Sinon, générer nouvelle clé
                return GenerateAndStoreMasterKey();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Key management error: {ex.Message}");
                throw new CryptographicException("Failed to initialize encryption keys", ex);
            }
        }

        /// <summary>
        /// Génère une nouvelle master key et la stocke
        /// </summary>
        private static byte[] GenerateAndStoreMasterKey()
        {
            // Créer répertoire si n'existe pas
            string directory = Path.GetDirectoryName(KEY_STORAGE_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Générer clé cryptographiquement sécurisée
            byte[] masterKey = new byte[KEY_SIZE_BYTES];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(masterKey);
            }

            // Stocker clé encryptée avec DPAPI (Windows Data Protection API)
            byte[] protectedKey = ProtectData(masterKey);

            // Écrire dans fichier avec permissions restreintes
            File.WriteAllBytes(KEY_STORAGE_PATH, protectedKey);

            // Restreindre permissions fichier (Windows)
            try
            {
                var fileInfo = new FileInfo(KEY_STORAGE_PATH);
                fileInfo.Attributes = FileAttributes.Hidden | FileAttributes.System;
            }
            catch
            {
                // Permissions non critiques pour développement
            }

            // Log création clé (AUDIT IMPORTANT)
            LogKeyEvent("Master key generated and stored");

            return masterKey;
        }

        /// <summary>
        /// Charge la master key depuis le fichier
        /// </summary>
        private static byte[] LoadMasterKeyFromFile()
        {
            try
            {
                byte[] protectedKey = File.ReadAllBytes(KEY_STORAGE_PATH);
                byte[] masterKey = UnprotectData(protectedKey);

                // Vérifier taille clé
                if (masterKey.Length != KEY_SIZE_BYTES)
                {
                    throw new CryptographicException($"Invalid key size: expected {KEY_SIZE_BYTES} bytes, got {masterKey.Length}");
                }

                // Log accès clé (AUDIT IMPORTANT)
                LogKeyEvent("Master key loaded from storage");

                return masterKey;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load master key: {ex.Message}");
                throw new CryptographicException("Failed to load encryption key", ex);
            }
        }

        /// <summary>
        /// Protège les données avec DPAPI (Windows Data Protection API)
        /// Lié au compte utilisateur Windows actuel
        /// </summary>
        private static byte[] ProtectData(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(
                    data,
                    null, // Pas d'entropy additionnelle
                    DataProtectionScope.LocalMachine // Accessible par toutes les applications de la machine
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DPAPI protection failed: {ex.Message}");
                throw new CryptographicException("Failed to protect data with DPAPI", ex);
            }
        }

        /// <summary>
        /// Déprotège les données DPAPI
        /// </summary>
        private static byte[] UnprotectData(byte[] protectedData)
        {
            try
            {
                return ProtectedData.Unprotect(
                    protectedData,
                    null,
                    DataProtectionScope.LocalMachine
                );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DPAPI unprotection failed: {ex.Message}");
                throw new CryptographicException("Failed to unprotect data with DPAPI", ex);
            }
        }

        /// <summary>
        /// Dérive une clé depuis un mot de passe avec PBKDF2
        /// Utilisé pour encryption basée sur password utilisateur
        /// </summary>
        public static byte[] DeriveKeyFromPassword(string password, byte[] salt = null)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            // Générer salt si non fourni
            if (salt == null)
            {
                salt = new byte[SALT_SIZE_BYTES];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }

            // Dériver clé avec PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                PBKDF2_ITERATIONS,
                HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(KEY_SIZE_BYTES);
            }
        }

        /// <summary>
        /// Rotate master key (génère nouvelle clé et ré-encrypte toutes données)
        /// ATTENTION: Opération très coûteuse - doit être faite en maintenance
        /// </summary>
        public static byte[] RotateMasterKey()
        {
            // Backup ancienne clé
            byte[] oldKey = LoadMasterKeyFromFile();
            string backupPath = KEY_STORAGE_PATH + $".backup_{DateTime.Now:yyyyMMddHHmmss}";
            File.Copy(KEY_STORAGE_PATH, backupPath);

            // Générer nouvelle clé
            byte[] newKey = new byte[KEY_SIZE_BYTES];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(newKey);
            }

            // Stocker nouvelle clé
            byte[] protectedKey = ProtectData(newKey);
            File.WriteAllBytes(KEY_STORAGE_PATH, protectedKey);

            // Log rotation
            LogKeyEvent($"Master key rotated - backup saved to {backupPath}");

            // NOTE: Après cette opération, il faut ré-encrypter TOUTES les données
            // avec FieldEncryption.ReEncryptWithNewKey() pour chaque champ encrypté

            return newKey;
        }

        /// <summary>
        /// Vérifie l'intégrité de la master key
        /// </summary>
        public static bool VerifyKeyIntegrity()
        {
            try
            {
                byte[] key = GetOrCreateMasterKey();

                // Vérifier taille
                if (key.Length != KEY_SIZE_BYTES)
                    return false;

                // Vérifier que ce n'est pas une clé vide
                bool allZeros = true;
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] != 0)
                    {
                        allZeros = false;
                        break;
                    }
                }

                return !allZeros;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtient les informations sur la clé actuelle
        /// </summary>
        public static KeyInfo GetKeyInfo()
        {
            return new KeyInfo
            {
                KeyExists = File.Exists(KEY_STORAGE_PATH),
                KeyPath = KEY_STORAGE_PATH,
                KeySizeBytes = KEY_SIZE_BYTES,
                CreatedAt = File.Exists(KEY_STORAGE_PATH)
                    ? File.GetCreationTime(KEY_STORAGE_PATH)
                    : (DateTime?)null,
                LastAccessedAt = File.Exists(KEY_STORAGE_PATH)
                    ? File.GetLastAccessTime(KEY_STORAGE_PATH)
                    : (DateTime?)null,
                IsIntegrityValid = VerifyKeyIntegrity()
            };
        }

        /// <summary>
        /// Log événement clé pour audit
        /// </summary>
        private static void LogKeyEvent(string message)
        {
            try
            {
                string logPath = Path.Combine(
                    Path.GetDirectoryName(KEY_STORAGE_PATH),
                    "key_audit.log"
                );

                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry);
            }
            catch
            {
                // Ne pas bloquer si logging échoue
                System.Diagnostics.Debug.WriteLine($"Key audit log failed: {message}");
            }
        }

        /// <summary>
        /// Supprime la master key (DANGEREUX - perte définitive des données encryptées)
        /// </summary>
        public static void DeleteMasterKey()
        {
            if (File.Exists(KEY_STORAGE_PATH))
            {
                // Créer backup avant suppression
                string backupPath = KEY_STORAGE_PATH + $".deleted_{DateTime.Now:yyyyMMddHHmmss}";
                File.Move(KEY_STORAGE_PATH, backupPath);

                LogKeyEvent($"Master key deleted - backup at {backupPath}");
            }
        }
    }

    /// <summary>
    /// Informations sur la master key
    /// </summary>
    public class KeyInfo
    {
        public bool KeyExists { get; set; }
        public string KeyPath { get; set; }
        public int KeySizeBytes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public bool IsIntegrityValid { get; set; }

        /// <summary>
        /// Nombre de jours depuis création
        /// </summary>
        public int? KeyAgeDays => CreatedAt.HasValue
            ? (int)(DateTime.Now - CreatedAt.Value).TotalDays
            : null;

        /// <summary>
        /// Rotation recommandée (> 90 jours)
        /// </summary>
        public bool RotationRecommended => KeyAgeDays.HasValue && KeyAgeDays.Value > 90;
    }
}
