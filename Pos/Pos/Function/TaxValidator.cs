using Pos.Models;
using System;
using System.Linq;

namespace Pos.Function
{
    /// <summary>
    /// Valide les taxes appliquées aux ventes
    /// Assure conformité fiscale et détecte manipulations
    /// </summary>
    public static class TaxValidator
    {
        /// <summary>
        /// Taux de taxe par défaut (TVA Algérie : 19%)
        /// </summary>
        public const decimal DEFAULT_TAX_RATE = 19.0m;

        /// <summary>
        /// Taux de taxe minimum acceptable
        /// </summary>
        public const decimal MIN_TAX_RATE = 0m;

        /// <summary>
        /// Taux de taxe maximum acceptable
        /// </summary>
        public const decimal MAX_TAX_RATE = 50m;

        /// <summary>
        /// Valide qu'un montant de taxe est cohérent avec le total de vente
        /// </summary>
        /// <param name="saleTotal">Montant total de la vente (hors taxe)</param>
        /// <param name="taxAmount">Montant de taxe appliqué</param>
        /// <param name="expectedTaxRate">Taux de taxe attendu (optionnel)</param>
        /// <returns>Tuple (isValid, errorMessage, calculatedRate)</returns>
        public static (bool isValid, string errorMessage, decimal calculatedRate) ValidateTaxAmount(
            decimal saleTotal,
            decimal taxAmount,
            decimal? expectedTaxRate = null)
        {
            // VALIDATION 1: Montants positifs
            if (saleTotal < 0)
            {
                return (false, "Le total de vente ne peut pas être négatif.", 0);
            }

            if (taxAmount < 0)
            {
                return (false, "Le montant de taxe ne peut pas être négatif.", 0);
            }

            // VALIDATION 2: Si vente = 0, taxe doit être 0
            if (saleTotal == 0)
            {
                if (taxAmount != 0)
                {
                    return (false, "La taxe doit être 0 si le total de vente est 0.", 0);
                }
                return (true, string.Empty, 0);
            }

            // VALIDATION 3: Calculer le taux appliqué
            decimal calculatedRate = MoneyHelper.Round((taxAmount / saleTotal) * 100);

            // VALIDATION 4: Vérifier que le taux est dans les limites raisonnables
            if (calculatedRate < MIN_TAX_RATE || calculatedRate > MAX_TAX_RATE)
            {
                return (false, $"Taux de taxe calculé ({calculatedRate:F2}%) hors limites acceptables ({MIN_TAX_RATE}% - {MAX_TAX_RATE}%).", calculatedRate);
            }

            // VALIDATION 5: Si taux attendu fourni, vérifier cohérence (tolérance ±0.5%)
            if (expectedTaxRate.HasValue)
            {
                decimal difference = Math.Abs(calculatedRate - expectedTaxRate.Value);

                if (difference > 0.5m)
                {
                    return (false, $"Taux de taxe calculé ({calculatedRate:F2}%) diffère du taux attendu ({expectedTaxRate.Value:F2}%). Écart : {difference:F2}%.", calculatedRate);
                }
            }

            return (true, string.Empty, calculatedRate);
        }

        /// <summary>
        /// Calcule le montant de taxe correct pour un total donné
        /// </summary>
        public static decimal CalculateTax(decimal totalBeforeTax, decimal taxRate)
        {
            if (totalBeforeTax < 0)
                throw new ArgumentException("Le total ne peut pas être négatif", nameof(totalBeforeTax));

            if (taxRate < MIN_TAX_RATE || taxRate > MAX_TAX_RATE)
                throw new ArgumentException($"Taux de taxe ({taxRate}%) hors limites ({MIN_TAX_RATE}% - {MAX_TAX_RATE}%)", nameof(taxRate));

            return MoneyHelper.Multiply(totalBeforeTax, taxRate / 100m);
        }

        /// <summary>
        /// Calcule le total TTC (Toutes Taxes Comprises) depuis un montant HT
        /// </summary>
        public static decimal CalculateTotalWithTax(decimal totalBeforeTax, decimal taxRate)
        {
            decimal taxAmount = CalculateTax(totalBeforeTax, taxRate);
            return MoneyHelper.Add(totalBeforeTax, taxAmount);
        }

        /// <summary>
        /// Extrait le montant HT depuis un montant TTC
        /// </summary>
        public static decimal ExtractTotalBeforeTax(decimal totalWithTax, decimal taxRate)
        {
            if (taxRate < MIN_TAX_RATE || taxRate > MAX_TAX_RATE)
                throw new ArgumentException($"Taux de taxe ({taxRate}%) hors limites", nameof(taxRate));

            // Formule: HT = TTC / (1 + taux)
            decimal divisor = 1 + (taxRate / 100m);
            return MoneyHelper.Divide(totalWithTax, divisor);
        }

        /// <summary>
        /// Extrait le montant de taxe depuis un montant TTC
        /// </summary>
        public static decimal ExtractTaxAmount(decimal totalWithTax, decimal taxRate)
        {
            decimal totalBeforeTax = ExtractTotalBeforeTax(totalWithTax, taxRate);
            return MoneyHelper.Subtract(totalWithTax, totalBeforeTax);
        }

        /// <summary>
        /// Récupère le taux de taxe configuré en base de données
        /// (Pour implémentation future avec table TaxRate)
        /// </summary>
        public static decimal GetConfiguredTaxRate(AppDbContext context, int? businessLocationId = null)
        {
            // TODO: Implémenter récupération depuis DB quand table TaxRate sera créée
            // Pour l'instant, retourner le taux par défaut

            // Exemple futur:
            // var taxRate = context.TaxRates
            //     .Where(tr => tr.IsActive && (tr.BusinessLocationId == businessLocationId || tr.BusinessLocationId == null))
            //     .OrderByDescending(tr => tr.EffectiveDate)
            //     .FirstOrDefault();
            //
            // return taxRate?.Rate ?? DEFAULT_TAX_RATE;

            return DEFAULT_TAX_RATE;
        }

        /// <summary>
        /// Valide une vente complète (total, taxe, remise)
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateSaleTotals(
            decimal subtotal,
            decimal discount,
            decimal taxAmount,
            decimal netTotal,
            decimal? expectedTaxRate = null)
        {
            // VALIDATION 1: Subtotal >= 0
            if (subtotal < 0)
            {
                return (false, "Le sous-total ne peut pas être négatif.");
            }

            // VALIDATION 2: Discount <= Subtotal
            if (discount > subtotal)
            {
                return (false, $"La remise ({MoneyHelper.Format(discount)}) ne peut pas dépasser le sous-total ({MoneyHelper.Format(subtotal)}).");
            }

            // VALIDATION 3: Calculer montant après remise
            decimal totalAfterDiscount = MoneyHelper.Subtract(subtotal, discount);

            // VALIDATION 4: Valider montant de taxe
            var taxValidation = ValidateTaxAmount(totalAfterDiscount, taxAmount, expectedTaxRate);

            if (!taxValidation.isValid)
            {
                return (false, taxValidation.errorMessage);
            }

            // VALIDATION 5: Vérifier NetTotal = (Subtotal - Discount) + Tax
            decimal expectedNetTotal = MoneyHelper.Add(totalAfterDiscount, taxAmount);

            if (!MoneyHelper.AreEqual(netTotal, expectedNetTotal, 0.02m))
            {
                return (false, $"Le montant net ({MoneyHelper.Format(netTotal)}) ne correspond pas au calcul attendu ({MoneyHelper.Format(expectedNetTotal)}). Écart : {MoneyHelper.Format(Math.Abs(netTotal - expectedNetTotal))}.");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Détecte si une taxe semble anormale (potentielle fraude)
        /// </summary>
        public static bool IsAnomalousTax(decimal saleTotal, decimal taxAmount)
        {
            if (saleTotal == 0)
                return taxAmount != 0;

            decimal calculatedRate = (taxAmount / saleTotal) * 100;

            // Anomalie si:
            // 1. Taux < 0% ou > 50%
            // 2. Taux entre 0% et 5% (suspicieusement bas)
            // 3. Taux entre 25% et 50% (suspicieusement haut)

            if (calculatedRate < 0 || calculatedRate > MAX_TAX_RATE)
                return true;

            if (calculatedRate > 0 && calculatedRate < 5)
                return true; // TVA normalement >= 5%

            if (calculatedRate > 25)
                return true; // TVA rarement > 25%

            return false;
        }

        /// <summary>
        /// Log une anomalie de taxe détectée
        /// </summary>
        public static void LogTaxAnomaly(AppDbContext context, int saleId, decimal saleTotal, decimal taxAmount, int userId)
        {
            decimal calculatedRate = saleTotal > 0 ? (taxAmount / saleTotal) * 100 : 0;

            string details = $"Anomalie taxe détectée: Vente #{saleId}, Total: {MoneyHelper.Format(saleTotal)}, Taxe: {MoneyHelper.Format(taxAmount)}, Taux: {calculatedRate:F2}%";

            AuditLogger.LogAnomaly(context, "Tax Anomaly", details, userId);
        }
    }
}
