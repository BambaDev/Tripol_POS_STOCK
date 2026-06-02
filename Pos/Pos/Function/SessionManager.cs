using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Pos.Models;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    public static class SessionManager
    {
        private const int SESSION_DURATION_HOURS = 8;
        private const int SESSION_INACTIVITY_MINUTES = 30;

        /// <summary>
        /// Crée une nouvelle session sécurisée pour l'utilisateur
        /// </summary>
        public static string CreateSession(int userId, string ipAddress = null, string userAgent = null)
        {
            using (var context = new AppDbContext())
            {
                // Révoquer les anciennes sessions de cet utilisateur
                var oldSessions = context.UserSessions
                    .Where(s => s.UserId == userId && !s.IsRevoked)
                    .ToList();

                foreach (var old in oldSessions)
                {
                    old.IsRevoked = true;
                    old.RevokedAt = DateTime.Now;
                }

                // Générer un token cryptographique sécurisé
                string token = GenerateSecureToken();

                // Créer la nouvelle session
                var session = new UserSession
                {
                    Token = token,
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddHours(SESSION_DURATION_HOURS),
                    LastActivityAt = DateTime.Now,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    IsRevoked = false
                };

                context.UserSessions.Add(session);
                context.SaveChanges();

                // Stocker le token chiffré dans Settings
                StoreEncryptedToken(token);

                return token;
            }
        }

        /// <summary>
        /// Valide la session actuelle
        /// </summary>
        public static bool ValidateSession()
        {
            try
            {
                string token = RetrieveEncryptedToken();

                if (string.IsNullOrEmpty(token))
                    return false;

                using (var context = new AppDbContext())
                {
                    var session = context.UserSessions
                        .Include(s => s.User)
                        .ThenInclude(u => u.Role)
                        .FirstOrDefault(s => s.Token == token);

                    if (session == null)
                        return false;

                    // Vérifier si révoquée
                    if (session.IsRevoked)
                        return false;

                    // Vérifier expiration
                    if (DateTime.Now > session.ExpiresAt)
                    {
                        RevokeSession(token);
                        return false;
                    }

                    // Vérifier inactivité
                    if (session.LastActivityAt.HasValue)
                    {
                        var inactiveMinutes = (DateTime.Now - session.LastActivityAt.Value).TotalMinutes;
                        if (inactiveMinutes > SESSION_INACTIVITY_MINUTES)
                        {
                            RevokeSession(token);
                            return false;
                        }
                    }

                    // Mettre à jour l'activité
                    session.LastActivityAt = DateTime.Now;
                    context.SaveChanges();

                    // Recharger les infos utilisateur dans Settings
                    RefreshUserSettings(session.User);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Révoque la session actuelle (logout)
        /// </summary>
        public static void RevokeCurrentSession()
        {
            string token = RetrieveEncryptedToken();
            if (!string.IsNullOrEmpty(token))
            {
                RevokeSession(token);
            }

            ClearStoredToken();
        }

        /// <summary>
        /// Révoque une session spécifique
        /// </summary>
        private static void RevokeSession(string token)
        {
            using (var context = new AppDbContext())
            {
                var session = context.UserSessions.FirstOrDefault(s => s.Token == token);
                if (session != null)
                {
                    session.IsRevoked = true;
                    session.RevokedAt = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Génère un token cryptographique sécurisé
        /// Format: {GUID}-{Timestamp}-{HMAC}
        /// </summary>
        private static string GenerateSecureToken()
        {
            // Partie 1: GUID unique
            string guid = Guid.NewGuid().ToString("N");

            // Partie 2: Timestamp
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Partie 3: HMAC signature
            string data = $"{guid}-{timestamp}";
            string hmac = ComputeHMAC(data);

            return $"{guid}-{timestamp}-{hmac}";
        }

        /// <summary>
        /// Calcule une signature HMAC-SHA256
        /// </summary>
        private static string ComputeHMAC(string data)
        {
            // Clé secrète (à déplacer dans un fichier config sécurisé en production)
            string secret = "Ezzipos_Secret_Key_2026_Change_In_Production_5F9A3E1B";

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash).Substring(0, 16);
            }
        }

        /// <summary>
        /// Stocke le token chiffré avec DPAPI Windows
        /// </summary>
        private static void StoreEncryptedToken(string token)
        {
            try
            {
                byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
                byte[] encryptedBytes = ProtectedData.Protect(
                    tokenBytes,
                    null,
                    DataProtectionScope.CurrentUser
                );

                string encryptedToken = Convert.ToBase64String(encryptedBytes);
                Properties.Settings.Default.UserSession = encryptedToken;
                Properties.Settings.Default.Save();
            }
            catch
            {
                // Fallback: stockage non chiffré (moins sécurisé)
                Properties.Settings.Default.UserSession = token;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Récupère et déchiffre le token
        /// </summary>
        private static string RetrieveEncryptedToken()
        {
            try
            {
                string encryptedToken = Properties.Settings.Default.UserSession;

                if (string.IsNullOrEmpty(encryptedToken))
                    return null;

                // Tenter de déchiffrer
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedToken);
                    byte[] tokenBytes = ProtectedData.Unprotect(
                        encryptedBytes,
                        null,
                        DataProtectionScope.CurrentUser
                    );

                    return Encoding.UTF8.GetString(tokenBytes);
                }
                catch
                {
                    // Si échec déchiffrement, c'est peut-être un token non chiffré (fallback)
                    return encryptedToken;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Efface le token stocké
        /// </summary>
        private static void ClearStoredToken()
        {
            Properties.Settings.Default.UserSession = string.Empty;
            Properties.Settings.Default.userId = 0;
            Properties.Settings.Default.isAdmin = string.Empty;
            Properties.Settings.Default.UserRoleID = 0;
            Properties.Settings.Default.hasLogin = false;
            Properties.Settings.Default.CurrentUserFullName = string.Empty;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Recharge les paramètres utilisateur dans Settings
        /// </summary>
        private static void RefreshUserSettings(User user)
        {
            if (user != null && user.Role != null)
            {
                Properties.Settings.Default.userId = user.Id;
                Properties.Settings.Default.isAdmin = user.Role.Name;
                Properties.Settings.Default.UserRoleID = user.Role.Id;
                Properties.Settings.Default.hasLogin = true;
                Properties.Settings.Default.CurrentUserFullName = $"{user.FirstName} {user.LastName}";
            }
        }
    }
}
