using Pos.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace Pos.Function
{
    /// <summary>
    /// Système de logging d'audit pour tracer toutes les opérations sensibles
    /// Adapté au modèle AuditTrail existant (TableName, ActionType, etc.)
    /// </summary>
    public static class AuditLogger
    {
        /// <summary>
        /// Log une création d'entité
        /// </summary>
        public static void LogCreate<T>(AppDbContext context, int entityId, T entity, int userId)
        {
            string entityType = typeof(T).Name;
            string details = SerializeEntity(entity);

            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = entityType,
                ActionType = "Create",
                KeyValues = entityId.ToString(),
                OldValues = null,
                NewValues = details,
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);
        }

        /// <summary>
        /// Log une modification d'entité
        /// </summary>
        public static void LogUpdate<T>(AppDbContext context, int entityId, T oldValues, T newValues, int userId)
        {
            string entityType = typeof(T).Name;

            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = entityType,
                ActionType = "Update",
                KeyValues = entityId.ToString(),
                OldValues = SerializeEntity(oldValues),
                NewValues = SerializeEntity(newValues),
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);
        }

        /// <summary>
        /// Log une suppression d'entité
        /// </summary>
        public static void LogDelete<T>(AppDbContext context, int entityId, T entity, int userId)
        {
            string entityType = typeof(T).Name;

            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = entityType,
                ActionType = "Delete",
                KeyValues = entityId.ToString(),
                OldValues = SerializeEntity(entity),
                NewValues = null,
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);
        }

        /// <summary>
        /// Log une action personnalisée
        /// </summary>
        public static void LogAction(AppDbContext context, string action, string entityType, int? entityId, string details, int userId)
        {
            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = entityType,
                ActionType = action,
                KeyValues = entityId?.ToString() ?? "N/A",
                OldValues = null,
                NewValues = details,
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);
        }

        /// <summary>
        /// Log une transaction financière sensible
        /// </summary>
        public static void LogFinancialTransaction(
            AppDbContext context,
            string transactionType,
            int transactionId,
            decimal amount,
            string description,
            int userId)
        {
            var details = new
            {
                Type = transactionType,
                Amount = amount,
                Description = description,
                Timestamp = DateTime.Now
            };

            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = transactionType,
                ActionType = "Financial Transaction",
                KeyValues = transactionId.ToString(),
                OldValues = null,
                NewValues = JsonSerializer.Serialize(details),
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);
        }

        /// <summary>
        /// Log une modification de prix (sensible pour détection fraude)
        /// </summary>
        public static void LogPriceChange(
            AppDbContext context,
            int productId,
            string productName,
            decimal oldPrice,
            decimal newPrice,
            int userId,
            string reason = null)
        {
            var details = new
            {
                ProductId = productId,
                ProductName = productName,
                OldPrice = oldPrice,
                NewPrice = newPrice,
                PriceChange = newPrice - oldPrice,
                PercentageChange = oldPrice > 0 ? ((newPrice - oldPrice) / oldPrice * 100) : 0,
                Reason = reason ?? "Non spécifié"
            };

            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = "Product",
                ActionType = "Price Change",
                KeyValues = productId.ToString(),
                OldValues = oldPrice.ToString(),
                NewValues = JsonSerializer.Serialize(details),
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);

            // Alerte si changement de prix > 20%
            if (oldPrice > 0 && Math.Abs((newPrice - oldPrice) / oldPrice) > 0.20m)
            {
                LogAction(context, "Price Change Alert", "Product", productId,
                    $"ALERTE: Changement > 20% pour {productName} ({MoneyHelper.Format(oldPrice)} → {MoneyHelper.Format(newPrice)})",
                    userId);
            }
        }

        /// <summary>
        /// Log une tentative d'action non autorisée
        /// </summary>
        public static void LogUnauthorizedAttempt(
            AppDbContext context,
            string action,
            string entityType,
            int? entityId,
            int userId)
        {
            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = entityType,
                ActionType = "Unauthorized Attempt",
                KeyValues = entityId?.ToString() ?? "N/A",
                OldValues = null,
                NewValues = $"Tentative: {action}",
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);

            System.Diagnostics.Debug.WriteLine($"⚠️ ALERTE: User {userId} → {action} non autorisé sur {entityType}");
        }

        /// <summary>
        /// Log une anomalie détectée (suspicion de fraude)
        /// </summary>
        public static void LogAnomaly(
            AppDbContext context,
            string anomalyType,
            string description,
            int userId)
        {
            var audit = new AuditTrail
            {
                UserId = userId,
                TableName = anomalyType,
                ActionType = "Anomaly Detected",
                KeyValues = "N/A",
                OldValues = null,
                NewValues = $"ANOMALIE: {description}",
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(audit);

            System.Diagnostics.Debug.WriteLine($"🚨 ANOMALIE: {anomalyType} - {description} (User: {userId})");
        }

        /// <summary>
        /// Sérialise une entité en JSON pour le log
        /// </summary>
        private static string SerializeEntity<T>(T entity)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };

                return JsonSerializer.Serialize(entity, options);
            }
            catch
            {
                return entity?.ToString() ?? "null";
            }
        }

        /// <summary>
        /// Compte les actions suspectes d'un utilisateur dans les dernières 24h
        /// </summary>
        public static int GetSuspiciousActivityCount(AppDbContext context, int userId)
        {
            var yesterday = DateTime.Now.AddHours(-24);

            return context.AuditTrails
                .Count(a => a.UserId == userId
                         && a.ChangeTime >= yesterday
                         && (a.ActionType.Contains("Unauthorized")
                             || a.ActionType.Contains("Anomaly")
                             || a.ActionType.Contains("Alert")));
        }

        /// <summary>
        /// Vérifie si un utilisateur a un comportement suspect
        /// </summary>
        public static bool IsSuspiciousUser(AppDbContext context, int userId)
        {
            const int SUSPICIOUS_THRESHOLD = 5;
            return GetSuspiciousActivityCount(context, userId) >= SUSPICIOUS_THRESHOLD;
        }
    }
}
