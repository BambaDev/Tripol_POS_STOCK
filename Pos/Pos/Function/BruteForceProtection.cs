using System;
using System.Linq;
using System.Threading;
using Pos.Models;

namespace Pos.Function
{
    public static class BruteForceProtection
    {
        private const int MAX_FAILED_ATTEMPTS = 5;
        private const int LOCKOUT_DURATION_MINUTES = 15;
        private const int ATTEMPT_WINDOW_MINUTES = 30;

        // ===== PHASE 3F: RATE LIMITING PROGRESSIF =====
        // Délais progressifs pour ralentir brute force
        private static readonly int[] PROGRESSIVE_DELAYS_SECONDS = { 0, 2, 5, 10, 20, 30 };

        /// <summary>
        /// Vérifie si un compte est verrouillé (trop de tentatives échouées)
        /// </summary>
        public static bool IsAccountLocked(string username, out int remainingMinutes)
        {
            remainingMinutes = 0;

            using (var context = new AppDbContext())
            {
                var cutoffTime = DateTime.Now.AddMinutes(-ATTEMPT_WINDOW_MINUTES);

                // Compter les échecs récents
                var recentFailedAttempts = context.LoginAttempts
                    .Where(a => a.Username.ToLower() == username.ToLower()
                             && !a.IsSuccessful
                             && a.AttemptTime >= cutoffTime)
                    .OrderByDescending(a => a.AttemptTime)
                    .ToList();

                if (recentFailedAttempts.Count >= MAX_FAILED_ATTEMPTS)
                {
                    // Vérifier si le verrouillage est encore actif
                    var lastFailedAttempt = recentFailedAttempts.First();
                    var lockoutEndTime = lastFailedAttempt.AttemptTime.AddMinutes(LOCKOUT_DURATION_MINUTES);

                    if (DateTime.Now < lockoutEndTime)
                    {
                        remainingMinutes = (int)(lockoutEndTime - DateTime.Now).TotalMinutes + 1;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Enregistre une tentative de connexion
        /// </summary>
        public static void RecordLoginAttempt(string username, bool isSuccessful, string failureReason = null, string ipAddress = null, string userAgent = null)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var attempt = new LoginAttempt
                    {
                        Username = username,
                        AttemptTime = DateTime.Now,
                        IsSuccessful = isSuccessful,
                        FailureReason = failureReason,
                        IpAddress = ipAddress,
                        UserAgent = userAgent
                    };

                    context.LoginAttempts.Add(attempt);
                    context.SaveChanges();

                    // Nettoyer les anciennes tentatives (> 7 jours)
                    CleanupOldAttempts(context);
                }
            }
            catch
            {
                // Erreur lors de l'enregistrement - ne pas bloquer le login
            }
        }

        /// <summary>
        /// Calcule le délai progressif basé sur le nombre d'échecs
        /// </summary>
        public static void ApplyProgressiveDelay(string username)
        {
            using (var context = new AppDbContext())
            {
                var cutoffTime = DateTime.Now.AddMinutes(-ATTEMPT_WINDOW_MINUTES);

                var recentFailedAttempts = context.LoginAttempts
                    .Where(a => a.Username.ToLower() == username.ToLower()
                             && !a.IsSuccessful
                             && a.AttemptTime >= cutoffTime)
                    .Count();

                if (recentFailedAttempts > 0)
                {
                    // Délai progressif: 1s, 2s, 4s, 8s, 16s...
                    int delaySeconds = (int)Math.Pow(2, Math.Min(recentFailedAttempts - 1, 5));
                    Thread.Sleep(delaySeconds * 1000);
                }
            }
        }

        /// <summary>
        /// Réinitialise le compteur d'échecs après une connexion réussie
        /// </summary>
        public static void ResetFailedAttempts(string username)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var cutoffTime = DateTime.Now.AddMinutes(-ATTEMPT_WINDOW_MINUTES);

                    // Marquer les anciennes tentatives comme "réinitialisées" en les supprimant
                    var oldAttempts = context.LoginAttempts
                        .Where(a => a.Username.ToLower() == username.ToLower()
                                 && a.AttemptTime >= cutoffTime
                                 && !a.IsSuccessful)
                        .ToList();

                    context.LoginAttempts.RemoveRange(oldAttempts);
                    context.SaveChanges();
                }
            }
            catch
            {
                // Erreur lors de la réinitialisation - ne pas bloquer
            }
        }

        /// <summary>
        /// Nettoie les tentatives de connexion de plus de 7 jours
        /// </summary>
        private static void CleanupOldAttempts(AppDbContext context)
        {
            try
            {
                var cutoffDate = DateTime.Now.AddDays(-7);

                var oldAttempts = context.LoginAttempts
                    .Where(a => a.AttemptTime < cutoffDate)
                    .Take(100) // Limiter pour ne pas surcharger
                    .ToList();

                if (oldAttempts.Any())
                {
                    context.LoginAttempts.RemoveRange(oldAttempts);
                    context.SaveChanges();
                }
            }
            catch
            {
                // Erreur lors du nettoyage - ne pas bloquer
            }
        }

        /// <summary>
        /// Obtient le nombre de tentatives échouées récentes
        /// </summary>
        public static int GetRecentFailedAttempts(string username)
        {
            using (var context = new AppDbContext())
            {
                var cutoffTime = DateTime.Now.AddMinutes(-ATTEMPT_WINDOW_MINUTES);

                return context.LoginAttempts
                    .Where(a => a.Username.ToLower() == username.ToLower()
                             && !a.IsSuccessful
                             && a.AttemptTime >= cutoffTime)
                    .Count();
            }
        }

        /// <summary>
        /// PHASE 3F: Calcule le délai progressif avant la prochaine tentative
        /// Plus il y a de tentatives échouées, plus le délai est long
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        /// <returns>Nombre de secondes à attendre avant la prochaine tentative</returns>
        public static int GetProgressiveDelay(string username)
        {
            using (var context = new AppDbContext())
            {
                var cutoffTime = DateTime.Now.AddMinutes(-ATTEMPT_WINDOW_MINUTES);

                // Compter les échecs récents
                int failedCount = context.LoginAttempts
                    .Where(a => a.Username.ToLower() == username.ToLower()
                             && !a.IsSuccessful
                             && a.AttemptTime >= cutoffTime)
                    .Count();

                // Retourner délai progressif
                if (failedCount >= PROGRESSIVE_DELAYS_SECONDS.Length)
                {
                    return PROGRESSIVE_DELAYS_SECONDS[PROGRESSIVE_DELAYS_SECONDS.Length - 1];
                }

                return PROGRESSIVE_DELAYS_SECONDS[failedCount];
            }
        }

        /// <summary>
        /// PHASE 3F: Applique le délai progressif (bloque le thread)
        /// À appeler AVANT de vérifier les credentials
        /// </summary>
        /// <param name="username">Nom d'utilisateur</param>
        public static void ApplyProgressiveDelay(string username)
        {
            int delaySeconds = GetProgressiveDelay(username);

            if (delaySeconds > 0)
            {
                // Bloquer le thread pour ralentir les attaques
                Thread.Sleep(delaySeconds * 1000);
            }
        }
    }
}
