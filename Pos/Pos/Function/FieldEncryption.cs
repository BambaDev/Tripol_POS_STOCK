using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4A RGPD: Field-level encryption pour données sensibles
    /// Utilise AES-256-GCM pour encryption at-rest avec authentification
    /// </summary>
    public static class FieldEncryption
    {
        // Taille clé AES-256: 32 bytes (256 bits)
        private const int KEY_SIZE = 32;

        // Taille nonce GCM: 12 bytes recommandé
        private const int NONCE_SIZE = 12;

        // Taille tag authentication GCM: 16 bytes
        private const int TAG_SIZE = 16;

        /// <summary>
        /// Clé d'encryption dérivée du master password
        /// IMPORTANT: En production, utiliser Azure Key Vault ou AWS KMS
        /// </summary>
        private static byte[] _encryptionKey;
        private static readonly object _keyLock = new object();

        /// <summary>
        /// Obtient ou génère la clé d'encryption
        /// </summary>
        private static byte[] GetEncryptionKey()
        {
            if (_encryptionKey == null)
            {
                lock (_keyLock)
                {
                    if (_encryptionKey == null)
                    {
                        // En production: charger depuis Azure Key Vault
                        // Pour développement: dériver depuis mot de passe
                        _encryptionKey = KeyManagement.GetOrCreateMasterKey();
                    }
                }
            }

            return _encryptionKey;
        }

        /// <summary>
        /// Encrypte une string avec AES-256-GCM
        /// Format retourné: [nonce(12 bytes)][tag(16 bytes)][ciphertext]
        /// Encodé en Base64 pour stockage DB
        /// </summary>
        /// <param name="plaintext">Texte en clair à encrypter</param>
        /// <returns>Texte encrypté en Base64, ou null si input null/vide</returns>
        public static string Encrypt(string plaintext)
        {
            // Si vide, retourner null (pas d'encryption nécessaire)
            if (string.IsNullOrEmpty(plaintext))
                return null;

            try
            {
                byte[] key = GetEncryptionKey();
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

                // Générer nonce aléatoire (CRITIQUE: doit être unique par encryption)
                byte[] nonce = new byte[NONCE_SIZE];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(nonce);
                }

                // Encrypter avec AES-GCM
                byte[] ciphertext = new byte[plaintextBytes.Length];
                byte[] tag = new byte[TAG_SIZE];

                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Encrypt(nonce, plaintextBytes, ciphertext, tag);
                }

                // Combiner: nonce + tag + ciphertext
                byte[] result = new byte[NONCE_SIZE + TAG_SIZE + ciphertext.Length];
                Buffer.BlockCopy(nonce, 0, result, 0, NONCE_SIZE);
                Buffer.BlockCopy(tag, 0, result, NONCE_SIZE, TAG_SIZE);
                Buffer.BlockCopy(ciphertext, 0, result, NONCE_SIZE + TAG_SIZE, ciphertext.Length);

                // Encoder en Base64 pour stockage DB
                return Convert.ToBase64String(result);
            }
            catch (Exception ex)
            {
                // Log l'erreur mais ne pas exposer détails
                System.Diagnostics.Debug.WriteLine($"Encryption error: {ex.Message}");
                throw new CryptographicException("Failed to encrypt data", ex);
            }
        }

        /// <summary>
        /// Décrypte une string AES-256-GCM
        /// </summary>
        /// <param name="ciphertext">Texte encrypté en Base64</param>
        /// <returns>Texte en clair, ou null si input null/vide</returns>
        public static string Decrypt(string ciphertext)
        {
            // Si vide, retourner null
            if (string.IsNullOrEmpty(ciphertext))
                return null;

            try
            {
                byte[] key = GetEncryptionKey();
                byte[] encryptedData = Convert.FromBase64String(ciphertext);

                // Vérifier taille minimum
                if (encryptedData.Length < NONCE_SIZE + TAG_SIZE)
                {
                    throw new CryptographicException("Invalid ciphertext format");
                }

                // Extraire nonce, tag, ciphertext
                byte[] nonce = new byte[NONCE_SIZE];
                byte[] tag = new byte[TAG_SIZE];
                byte[] encrypted = new byte[encryptedData.Length - NONCE_SIZE - TAG_SIZE];

                Buffer.BlockCopy(encryptedData, 0, nonce, 0, NONCE_SIZE);
                Buffer.BlockCopy(encryptedData, NONCE_SIZE, tag, 0, TAG_SIZE);
                Buffer.BlockCopy(encryptedData, NONCE_SIZE + TAG_SIZE, encrypted, 0, encrypted.Length);

                // Décrypter avec AES-GCM (vérifie automatiquement le tag)
                byte[] plaintext = new byte[encrypted.Length];

                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Decrypt(nonce, encrypted, tag, plaintext);
                }

                return Encoding.UTF8.GetString(plaintext);
            }
            catch (CryptographicException ex)
            {
                // Tag authentication failed = données corrompues ou clé incorrecte
                System.Diagnostics.Debug.WriteLine($"Decryption authentication failed: {ex.Message}");
                throw new CryptographicException("Failed to decrypt data - authentication failed", ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Decryption error: {ex.Message}");
                throw new CryptographicException("Failed to decrypt data", ex);
            }
        }

        /// <summary>
        /// Encrypte un byte array (pour images, documents)
        /// </summary>
        public static byte[] EncryptBytes(byte[] plainBytes)
        {
            if (plainBytes == null || plainBytes.Length == 0)
                return null;

            try
            {
                byte[] key = GetEncryptionKey();

                byte[] nonce = new byte[NONCE_SIZE];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(nonce);
                }

                byte[] ciphertext = new byte[plainBytes.Length];
                byte[] tag = new byte[TAG_SIZE];

                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Encrypt(nonce, plainBytes, ciphertext, tag);
                }

                // Combiner: nonce + tag + ciphertext
                byte[] result = new byte[NONCE_SIZE + TAG_SIZE + ciphertext.Length];
                Buffer.BlockCopy(nonce, 0, result, 0, NONCE_SIZE);
                Buffer.BlockCopy(tag, 0, result, NONCE_SIZE, TAG_SIZE);
                Buffer.BlockCopy(ciphertext, 0, result, NONCE_SIZE + TAG_SIZE, ciphertext.Length);

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bytes encryption error: {ex.Message}");
                throw new CryptographicException("Failed to encrypt byte array", ex);
            }
        }

        /// <summary>
        /// Décrypte un byte array
        /// </summary>
        public static byte[] DecryptBytes(byte[] encryptedBytes)
        {
            if (encryptedBytes == null || encryptedBytes.Length == 0)
                return null;

            try
            {
                byte[] key = GetEncryptionKey();

                if (encryptedBytes.Length < NONCE_SIZE + TAG_SIZE)
                {
                    throw new CryptographicException("Invalid encrypted bytes format");
                }

                byte[] nonce = new byte[NONCE_SIZE];
                byte[] tag = new byte[TAG_SIZE];
                byte[] encrypted = new byte[encryptedBytes.Length - NONCE_SIZE - TAG_SIZE];

                Buffer.BlockCopy(encryptedBytes, 0, nonce, 0, NONCE_SIZE);
                Buffer.BlockCopy(encryptedBytes, NONCE_SIZE, tag, 0, TAG_SIZE);
                Buffer.BlockCopy(encryptedBytes, NONCE_SIZE + TAG_SIZE, encrypted, 0, encrypted.Length);

                byte[] plaintext = new byte[encrypted.Length];

                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Decrypt(nonce, encrypted, tag, plaintext);
                }

                return plaintext;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bytes decryption error: {ex.Message}");
                throw new CryptographicException("Failed to decrypt byte array", ex);
            }
        }

        /// <summary>
        /// Tokenize données ultra-sensibles (numéro passeport, carte bancaire)
        /// Retourne un token aléatoire et stocke le mapping dans une table séparée
        /// </summary>
        public static string Tokenize(string sensitiveData)
        {
            if (string.IsNullOrEmpty(sensitiveData))
                return null;

            // Générer token unique
            string token = $"TKN_{Guid.NewGuid():N}";

            // TODO: Stocker mapping token -> encryptedData dans table TokenVault
            // Pour l'instant, retourner hash
            return HashIrreversible(token);
        }

        /// <summary>
        /// Détokenize pour récupérer données originales
        /// </summary>
        public static string Detokenize(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            // TODO: Récupérer depuis table TokenVault
            throw new NotImplementedException("Tokenization vault not yet implemented");
        }

        /// <summary>
        /// Hash irréversible avec SHA-256 (pour données ne nécessitant pas décryption)
        /// Utilisé pour comparaisons (ex: vérifier doublon sans stocker valeur)
        /// </summary>
        public static string HashIrreversible(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Vérifie si une string est encryptée (détecte format Base64 valide)
        /// </summary>
        public static bool IsEncrypted(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            try
            {
                byte[] data = Convert.FromBase64String(value);
                // Vérifier taille minimum pour format AES-GCM
                return data.Length >= NONCE_SIZE + TAG_SIZE;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Rotation de clé: re-encrypte avec nouvelle clé
        /// </summary>
        public static string ReEncryptWithNewKey(string oldCiphertext, byte[] newKey)
        {
            // Décrypter avec ancienne clé
            string plaintext = Decrypt(oldCiphertext);

            // Temporairement changer la clé
            byte[] oldKey = _encryptionKey;
            _encryptionKey = newKey;

            try
            {
                // Re-encrypter avec nouvelle clé
                return Encrypt(plaintext);
            }
            finally
            {
                // Restaurer ancienne clé
                _encryptionKey = oldKey;
            }
        }
    }
}
