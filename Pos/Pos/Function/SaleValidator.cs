using Pos.Models;
using System;

namespace Pos.Function
{
    /// <summary>
    /// Valide les montants d'une vente avant sauvegarde
    /// Prévient overflow/underflow et valeurs absurdes
    /// </summary>
    public static class SaleValidator
    {
        /// <summary>
        /// Valide tous les montants d'une vente
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateSale(Sale sale)
        {
            if (sale == null)
                return (false, "Sale ne peut pas être null");

            // VALIDATION 1: NetTotalAmount >= 0
            if (sale.NetTotalAmount.HasValue && sale.NetTotalAmount.Value < 0)
            {
                return (false, $"NetTotalAmount ne peut pas être négatif: {sale.NetTotalAmount.Value}");
            }

            // VALIDATION 2: NetTotalAmount <= MAX_SALE_AMOUNT
            if (sale.NetTotalAmount.HasValue && sale.NetTotalAmount.Value > BusinessLimits.MAX_SALE_AMOUNT)
            {
                return (false, $"NetTotalAmount ({MoneyHelper.Format(sale.NetTotalAmount.Value)}) dépasse la limite maximale ({MoneyHelper.Format(BusinessLimits.MAX_SALE_AMOUNT)})");
            }

            // VALIDATION 3: PaidAmount >= 0
            if (sale.PaidAmount.HasValue && sale.PaidAmount.Value < 0)
            {
                return (false, $"PaidAmount ne peut pas être négatif: {sale.PaidAmount.Value}");
            }

            // VALIDATION 4: TotalTax >= 0
            if (sale.TotalTax.HasValue && sale.TotalTax.Value < 0)
            {
                return (false, $"TotalTax ne peut pas être négatif: {sale.TotalTax.Value}");
            }

            // VALIDATION 5: TotalDiscount >= 0
            if (sale.TotalDiscount.HasValue && sale.TotalDiscount.Value < 0)
            {
                return (false, $"TotalDiscount ne peut pas être négatif: {sale.TotalDiscount.Value}");
            }

            // VALIDATION 6: Due >= 0
            if (sale.Due.HasValue && sale.Due.Value < 0)
            {
                return (false, $"Due ne peut pas être négatif: {sale.Due.Value}");
            }

            // VALIDATION 7: ReturnAmount >= 0
            if (sale.ReturnAmount.HasValue && sale.ReturnAmount.Value < 0)
            {
                return (false, $"ReturnAmount ne peut pas être négatif: {sale.ReturnAmount.Value}");
            }

            // VALIDATION 8: NumberItems > 0
            if (sale.NumberItems.HasValue && sale.NumberItems.Value <= 0)
            {
                return (false, "NumberItems doit être supérieur à 0");
            }

            // VALIDATION 9: PaidAmount <= NetTotalAmount + ReturnAmount
            if (sale.NetTotalAmount.HasValue && sale.PaidAmount.HasValue)
            {
                decimal maxPaid = sale.NetTotalAmount.Value + (sale.ReturnAmount ?? 0);

                if (sale.PaidAmount.Value > maxPaid && sale.ReturnAmount.GetValueOrDefault() == 0)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"ATTENTION: PaidAmount ({sale.PaidAmount.Value}) > NetTotalAmount ({sale.NetTotalAmount.Value}) sans ReturnAmount"
                    );
                }
            }

            // VALIDATION 10: Due = NetTotalAmount - PaidAmount (avec tolérance)
            if (sale.NetTotalAmount.HasValue && sale.PaidAmount.HasValue && sale.Due.HasValue)
            {
                decimal expectedDue = MoneyHelper.Round(sale.NetTotalAmount.Value - sale.PaidAmount.Value);
                expectedDue = Math.Max(0, expectedDue);

                if (!MoneyHelper.AreEqual(sale.Due.Value, expectedDue, 0.02m))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"ALERTE: Due calculé ({expectedDue}) != Due enregistré ({sale.Due.Value})"
                    );
                }
            }

            // VALIDATION 11: ReturnAmount cohérent
            if (sale.NetTotalAmount.HasValue && sale.PaidAmount.HasValue && sale.ReturnAmount.HasValue && sale.ReturnAmount.Value > 0)
            {
                if (sale.PaidAmount.Value <= sale.NetTotalAmount.Value)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"ALERTE: ReturnAmount ({sale.ReturnAmount.Value}) présent alors que PaidAmount <= NetTotalAmount"
                    );
                }
                else
                {
                    decimal expectedReturn = MoneyHelper.Round(sale.PaidAmount.Value - sale.NetTotalAmount.Value);

                    if (!MoneyHelper.AreEqual(sale.ReturnAmount.Value, expectedReturn, 0.02m))
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"ALERTE: ReturnAmount calculé ({expectedReturn}) != ReturnAmount enregistré ({sale.ReturnAmount.Value})"
                        );
                    }
                }
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Arrondit tous les montants d'une vente à 2 décimales
        /// </summary>
        public static void RoundAllAmounts(Sale sale)
        {
            if (sale.NetTotalAmount.HasValue)
                sale.NetTotalAmount = MoneyHelper.Round(sale.NetTotalAmount.Value);

            if (sale.PaidAmount.HasValue)
                sale.PaidAmount = MoneyHelper.Round(sale.PaidAmount.Value);

            if (sale.TotalTax.HasValue)
                sale.TotalTax = MoneyHelper.Round(sale.TotalTax.Value);

            if (sale.TotalDiscount.HasValue)
                sale.TotalDiscount = MoneyHelper.Round(sale.TotalDiscount.Value);

            if (sale.Due.HasValue)
                sale.Due = MoneyHelper.Round(sale.Due.Value);

            if (sale.ReturnAmount.HasValue)
                sale.ReturnAmount = MoneyHelper.Round(sale.ReturnAmount.Value);

            if (sale.AdditionalShippingCharges.HasValue)
                sale.AdditionalShippingCharges = MoneyHelper.Round(sale.AdditionalShippingCharges.Value);

            if (sale.DiscountAmount.HasValue)
                sale.DiscountAmount = MoneyHelper.Round(sale.DiscountAmount.Value);

            if (sale.RemainingBalance.HasValue)
                sale.RemainingBalance = MoneyHelper.Round(sale.RemainingBalance.Value);
        }

        /// <summary>
        /// Valide qu'un montant individuel est acceptable
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateAmount(decimal? amount, string fieldName, bool allowNegative = false)
        {
            if (!amount.HasValue)
                return (true, string.Empty);

            decimal value = amount.Value;

            // Pas de valeur négative (sauf si explicitement autorisé)
            if (!allowNegative && value < 0)
            {
                return (false, $"{fieldName} ne peut pas être négatif. Valeur: {value}");
            }

            // Maximum raisonnable
            if (value > BusinessLimits.MAX_SALE_AMOUNT)
            {
                return (false, $"{fieldName} dépasse la limite maximale ({MoneyHelper.Format(BusinessLimits.MAX_SALE_AMOUNT)}). Valeur: {MoneyHelper.Format(value)}");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Valide et arrondit un montant
        /// </summary>
        public static decimal ValidateAndRound(decimal amount, string fieldName, bool allowNegative = false)
        {
            var validation = ValidateAmount(amount, fieldName, allowNegative);

            if (!validation.isValid)
            {
                throw new ArgumentException(validation.errorMessage, fieldName);
            }

            return MoneyHelper.Round(amount);
        }
    }
}
