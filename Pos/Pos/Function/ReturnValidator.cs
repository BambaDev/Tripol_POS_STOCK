using Pos.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    /// <summary>
    /// Valide les remboursements (Returns) pour prévenir les fraudes
    /// </summary>
    public static class ReturnValidator
    {
        /// <summary>
        /// Valide qu'un remboursement est légitime
        /// </summary>
        /// <param name="saleId">ID de la vente originale</param>
        /// <param name="returnAmount">Montant du remboursement demandé</param>
        /// <param name="context">Contexte DB</param>
        /// <returns>Tuple (isValid, errorMessage)</returns>
        public static (bool isValid, string errorMessage) ValidateReturn(int saleId, decimal returnAmount, AppDbContext context)
        {
            // VALIDATION 1: La vente existe
            var sale = context.Sales
                .Include(s => s.SaleDetails)
                .FirstOrDefault(s => s.Id == saleId);

            if (sale == null)
            {
                return (false, $"Vente #{saleId} introuvable.");
            }

            // VALIDATION 2: Montant remboursement > 0
            if (returnAmount <= 0)
            {
                return (false, "Le montant du remboursement doit être positif.");
            }

            // VALIDATION 3: Montant remboursement <= Montant vente originale
            if (returnAmount > sale.NetTotalAmount)
            {
                return (false, $"Le montant du remboursement ({MoneyHelper.Format(returnAmount)}) ne peut pas dépasser le montant de la vente ({MoneyHelper.Format(sale.NetTotalAmount.Value)}).");
            }

            // VALIDATION 4: Calculer total déjà remboursé
            var totalAlreadyReturned = context.Returns
                .Where(r => r.SaleId == saleId)
                .Sum(r => (decimal?)r.GrandTotal) ?? 0;

            decimal remainingRefundable = (sale.NetTotalAmount ?? 0) - totalAlreadyReturned;

            if (returnAmount > remainingRefundable)
            {
                return (false, $"Montant demandé ({MoneyHelper.Format(returnAmount)}) dépasse le montant remboursable restant ({MoneyHelper.Format(remainingRefundable)}). Total déjà remboursé : {MoneyHelper.Format(totalAlreadyReturned)}.");
            }

            // VALIDATION 5: Délai de remboursement (configurable, ex: 30 jours)
            const int MAX_RETURN_DAYS = 30;

            if (sale.SaleDate.HasValue)
            {
                var daysSinceSale = (DateTime.Now - sale.SaleDate.Value).Days;

                if (daysSinceSale > MAX_RETURN_DAYS)
                {
                    return (false, $"Délai de remboursement dépassé. La vente date de {daysSinceSale} jours (limite : {MAX_RETURN_DAYS} jours).");
                }
            }

            // VALIDATION 6: Vérifier que la vente n'est pas déjà annulée
            if (sale.SaleStatus == "Cancelled" || sale.SaleStatus == "Refunded")
            {
                return (false, $"La vente est déjà dans le statut '{sale.SaleStatus}'. Impossible de rembourser.");
            }

            // VALIDATION 7: Maximum de remboursements par vente (prévention abus)
            const int MAX_RETURNS_PER_SALE = 10;

            var returnCount = context.Returns.Count(r => r.SaleId == saleId);

            if (returnCount >= MAX_RETURNS_PER_SALE)
            {
                return (false, $"Nombre maximum de remboursements atteint ({MAX_RETURNS_PER_SALE}) pour cette vente.");
            }

            // Toutes les validations passées
            return (true, string.Empty);
        }

        /// <summary>
        /// Calcule le montant maximum remboursable pour une vente
        /// </summary>
        public static decimal GetMaxRefundableAmount(int saleId, AppDbContext context)
        {
            var sale = context.Sales.FirstOrDefault(s => s.Id == saleId);

            if (sale == null || !sale.NetTotalAmount.HasValue)
                return 0;

            var totalAlreadyReturned = context.Returns
                .Where(r => r.SaleId == saleId)
                .Sum(r => (decimal?)r.GrandTotal) ?? 0;

            return MoneyHelper.Round((sale.NetTotalAmount.Value - totalAlreadyReturned));
        }

        /// <summary>
        /// Vérifie si une vente est éligible au remboursement
        /// </summary>
        public static bool IsSaleRefundable(int saleId, AppDbContext context)
        {
            var sale = context.Sales.FirstOrDefault(s => s.Id == saleId);

            if (sale == null)
                return false;

            // Conditions d'éligibilité
            bool hasAmount = sale.NetTotalAmount.HasValue && sale.NetTotalAmount.Value > 0;
            bool notCancelled = sale.SaleStatus != "Cancelled" && sale.SaleStatus != "Refunded";
            bool withinTimelimit = !sale.SaleDate.HasValue || (DateTime.Now - sale.SaleDate.Value).Days <= 30;
            bool hasRefundableAmount = GetMaxRefundableAmount(saleId, context) > 0;

            return hasAmount && notCancelled && withinTimelimit && hasRefundableAmount;
        }

        /// <summary>
        /// Valide les quantités de produits retournés
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateReturnQuantities(
            int saleId,
            System.Collections.Generic.Dictionary<int, decimal> returnedQuantities,
            AppDbContext context)
        {
            var saleDetails = context.SaleDetails
                .Where(sd => sd.SaleId == saleId)
                .ToList();

            foreach (var kvp in returnedQuantities)
            {
                int productId = kvp.Key;
                decimal returnQuantity = kvp.Value;

                // VALIDATION 1: Quantité retournée > 0
                if (returnQuantity <= 0)
                {
                    return (false, $"La quantité retournée doit être positive pour le produit #{productId}.");
                }

                // VALIDATION 2: Produit dans la vente originale
                var saleDetail = saleDetails.FirstOrDefault(sd => sd.ProductId == productId);

                if (saleDetail == null)
                {
                    return (false, $"Le produit #{productId} n'apparaît pas dans la vente originale.");
                }

                // VALIDATION 3: Quantité retournée <= Quantité vendue
                if (returnQuantity > saleDetail.SaleQuantity)
                {
                    return (false, $"Quantité retournée ({returnQuantity}) dépasse la quantité vendue ({saleDetail.SaleQuantity}) pour le produit #{productId}.");
                }

                // VALIDATION 4: Vérifier quantité déjà retournée
                // Note: Si ReturnDetail n'existe pas, cette validation est simplifiée
                decimal alreadyReturned = 0; // TODO: Implémenter si table ReturnDetail existe

                decimal remainingQuantity = (saleDetail.SaleQuantity ?? 0) - alreadyReturned;

                if (returnQuantity > remainingQuantity)
                {
                    return (false, $"Quantité retournée ({returnQuantity}) dépasse la quantité restante ({remainingQuantity}) pour le produit #{productId}. Déjà retourné : {alreadyReturned}.");
                }
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Crée un log d'audit pour un remboursement
        /// </summary>
        public static void LogReturn(int saleId, decimal returnAmount, int userId, string reason, AppDbContext context)
        {
            var auditLog = new AuditTrail
            {
                UserId = userId,
                TableName = "Return",
                ActionType = "Return Created",
                KeyValues = saleId.ToString(),
                OldValues = null,
                NewValues = $"Remboursement de {MoneyHelper.Format(returnAmount)} pour vente #{saleId}. Raison: {reason}",
                ChangeTime = DateTime.Now
            };

            context.AuditTrails.Add(auditLog);
        }

        /// <summary>
        /// Vérifie si l'utilisateur a dépassé la limite de remboursements quotidiens
        /// Prévention de fraude interne
        /// </summary>
        public static (bool isAllowed, string errorMessage) CheckDailyReturnLimit(int userId, AppDbContext context)
        {
            const int MAX_RETURNS_PER_DAY = 20; // Limite configurable
            const decimal MAX_RETURN_AMOUNT_PER_DAY = 50_000; // 50 000 DA/jour

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var todayReturns = context.Returns
                .Where(r => r.UserId == userId && r.CreatedAt >= today && r.CreatedAt < tomorrow)
                .ToList();

            int returnCount = todayReturns.Count;
            decimal totalReturnAmount = todayReturns.Sum(r => r.GrandTotal ?? 0);

            if (returnCount >= MAX_RETURNS_PER_DAY)
            {
                return (false, $"Limite quotidienne de remboursements atteinte ({MAX_RETURNS_PER_DAY}). Contactez votre manager.");
            }

            if (totalReturnAmount >= MAX_RETURN_AMOUNT_PER_DAY)
            {
                return (false, $"Limite quotidienne de montant de remboursements atteinte ({MoneyHelper.Format(MAX_RETURN_AMOUNT_PER_DAY)}). Contactez votre manager.");
            }

            return (true, string.Empty);
        }
    }
}
