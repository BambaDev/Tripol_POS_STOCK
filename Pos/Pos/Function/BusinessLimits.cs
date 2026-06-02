using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    /// <summary>
    /// Limites business pour la validation des montants financiers
    /// Prévient les fraudes et erreurs de saisie
    /// </summary>
    public static class BusinessLimits
    {
        /// <summary>
        /// Montant maximum d'une vente : 100 millions DA
        /// </summary>
        public const decimal MAX_SALE_AMOUNT = 100_000_000m;

        /// <summary>
        /// Pourcentage de remise maximum : 99%
        /// </summary>
        public const decimal MAX_DISCOUNT_PERCENTAGE = 99m;

        /// <summary>
        /// Taux de taxe maximum : 50%
        /// </summary>
        public const decimal MAX_TAX_RATE = 50m;

        /// <summary>
        /// Montant minimum pour une transaction : 0.01 DA (1 centime)
        /// </summary>
        public const decimal MIN_AMOUNT = 0.01m;

        /// <summary>
        /// Seuil d'écart de caisse nécessitant justification : 10 DA
        /// </summary>
        public const decimal CASH_DISCREPANCY_THRESHOLD = 10.00m;

        /// <summary>
        /// Seuil d'écart de caisse nécessitant alerte manager : 100 DA
        /// </summary>
        public const decimal CASH_DISCREPANCY_ALERT_THRESHOLD = 100.00m;

        /// <summary>
        /// Nombre de décimales pour les montants monétaires
        /// </summary>
        public const int DECIMAL_PLACES = 2;

        /// <summary>
        /// Mode d'arrondi : Away from zero (0.5 → 1, -0.5 → -1)
        /// </summary>
        public const MidpointRounding ROUNDING_MODE = MidpointRounding.AwayFromZero;
    }
}
