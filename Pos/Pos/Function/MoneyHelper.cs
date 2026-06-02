using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    /// <summary>
    /// Utilitaires pour calculs monétaires avec arrondi cohérent
    /// Prévient les erreurs d'accumulation décimale
    /// </summary>
    public static class MoneyHelper
    {
        /// <summary>
        /// Arrondit un montant selon les standards business (2 décimales, away from zero)
        /// </summary>
        public static decimal Round(decimal value)
        {
            return Math.Round(value, BusinessLimits.DECIMAL_PLACES, BusinessLimits.ROUNDING_MODE);
        }

        /// <summary>
        /// Multiplication de montants avec arrondi cohérent
        /// Exemple: 10.125 * 3 = 30.38 (arrondi, pas 30.375)
        /// </summary>
        public static decimal Multiply(decimal a, decimal b)
        {
            return Round(a * b);
        }

        /// <summary>
        /// Division de montants avec arrondi cohérent
        /// Lève DivideByZeroException si diviseur = 0
        /// </summary>
        public static decimal Divide(decimal a, decimal b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division par zéro impossible dans un calcul monétaire");

            return Round(a / b);
        }

        /// <summary>
        /// Addition de montants avec arrondi final
        /// </summary>
        public static decimal Add(decimal a, decimal b)
        {
            return Round(a + b);
        }

        /// <summary>
        /// Soustraction de montants avec arrondi final
        /// </summary>
        public static decimal Subtract(decimal a, decimal b)
        {
            return Round(a - b);
        }

        /// <summary>
        /// Calcule un pourcentage d'un montant
        /// Exemple: CalculatePercentage(1000, 19.5) = 195.00
        /// </summary>
        public static decimal CalculatePercentage(decimal amount, decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Le pourcentage doit être entre 0 et 100");

            return Multiply(amount, percentage / 100m);
        }

        /// <summary>
        /// Vérifie si un montant est valide (>= MIN_AMOUNT, <= MAX_SALE_AMOUNT)
        /// </summary>
        public static bool IsValidAmount(decimal amount)
        {
            return amount >= BusinessLimits.MIN_AMOUNT && amount <= BusinessLimits.MAX_SALE_AMOUNT;
        }

        /// <summary>
        /// Valide qu'un montant n'est pas négatif
        /// Lève ArgumentException si négatif
        /// </summary>
        public static void ValidateNonNegative(decimal amount, string paramName)
        {
            if (amount < 0)
                throw new ArgumentException($"{paramName} ne peut pas être négatif. Valeur: {amount}", paramName);
        }

        /// <summary>
        /// Valide qu'un montant est strictement positif
        /// Lève ArgumentException si <= 0
        /// </summary>
        public static void ValidatePositive(decimal amount, string paramName)
        {
            if (amount <= 0)
                throw new ArgumentException($"{paramName} doit être positif. Valeur: {amount}", paramName);
        }

        /// <summary>
        /// Formate un montant pour affichage (2 décimales, séparateur milliers)
        /// Exemple: 12345.67 → "12,345.67"
        /// </summary>
        public static string Format(decimal amount, string currencyCode = "DA")
        {
            return $"{amount:N2} {currencyCode}";
        }

        /// <summary>
        /// Compare deux montants avec tolérance d'arrondi (0.01)
        /// </summary>
        public static bool AreEqual(decimal amount1, decimal amount2, decimal tolerance = 0.01m)
        {
            return Math.Abs(amount1 - amount2) <= tolerance;
        }
    }
}
