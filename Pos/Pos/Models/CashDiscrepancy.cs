using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models
{
    /// <summary>
    /// Enregistre les écarts de caisse détectés lors de la fermeture du registre
    /// Permet la traçabilité et la détection de fraudes internes
    /// </summary>
    [Table("CashDiscrepancy")]
    public partial class CashDiscrepancy
    {
        /// <summary>
        /// ID unique de l'écart
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// RegisterRecord associé (fermeture de caisse)
        /// </summary>
        public int RegisterRecordId { get; set; }

        /// <summary>
        /// Montant attendu (calculé depuis les Sales)
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal ExpectedAmount { get; set; }

        /// <summary>
        /// Montant réel déclaré par l'employé
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// Écart = ActualAmount - ExpectedAmount
        /// Négatif = Manque (vol potentiel)
        /// Positif = Excédent (erreur de rendu)
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Discrepancy { get; set; }

        /// <summary>
        /// Raison fournie par l'employé
        /// </summary>
        [Column(TypeName = "text")]
        public string Reason { get; set; }

        /// <summary>
        /// Utilisateur qui a déclaré l'écart
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Date/heure de signalement
        /// </summary>
        [Column(TypeName = "datetime")]
        [Required]
        public DateTime ReportedAt { get; set; }

        /// <summary>
        /// Écart résolu (approuvé par manager)
        /// </summary>
        [Required]
        public bool IsResolved { get; set; }

        /// <summary>
        /// Manager qui a approuvé la résolution
        /// </summary>
        public int? ResolvedById { get; set; }

        /// <summary>
        /// Date de résolution
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? ResolvedAt { get; set; }

        /// <summary>
        /// Commentaire du manager
        /// </summary>
        [Column(TypeName = "text")]
        public string ManagerComment { get; set; }

        /// <summary>
        /// Type d'écart (pour catégorisation)
        /// "Missing" = Manque
        /// "Excess" = Excédent
        /// "CountingError" = Erreur de comptage
        /// "Theft" = Vol confirmé
        /// </summary>
        [StringLength(50)]
        public string DiscrepancyType { get; set; }

        /// <summary>
        /// Sévérité (Low, Medium, High, Critical)
        /// </summary>
        [StringLength(20)]
        public string Severity { get; set; }

        // Navigation properties
        [ForeignKey("RegisterRecordId")]
        [InverseProperty("CashDiscrepancies")]
        public virtual RegisterRecord RegisterRecord { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("CashDiscrepanciesReported")]
        public virtual User User { get; set; }

        [ForeignKey("ResolvedById")]
        [InverseProperty("CashDiscrepanciesResolved")]
        public virtual User ResolvedBy { get; set; }

        /// <summary>
        /// Calcule automatiquement la sévérité basée sur le montant de l'écart
        /// </summary>
        public void CalculateSeverity()
        {
            decimal absDiscrepancy = Math.Abs(Discrepancy);

            if (absDiscrepancy <= Function.BusinessLimits.CASH_DISCREPANCY_THRESHOLD)
            {
                Severity = "Low"; // <= 10 DA
            }
            else if (absDiscrepancy <= Function.BusinessLimits.CASH_DISCREPANCY_ALERT_THRESHOLD)
            {
                Severity = "Medium"; // 10-100 DA
            }
            else if (absDiscrepancy <= 500)
            {
                Severity = "High"; // 100-500 DA
            }
            else
            {
                Severity = "Critical"; // > 500 DA
            }
        }

        /// <summary>
        /// Détermine le type d'écart automatiquement
        /// </summary>
        public void DetermineDiscrepancyType()
        {
            if (Discrepancy < 0)
            {
                DiscrepancyType = "Missing"; // Manque
            }
            else if (Discrepancy > 0)
            {
                DiscrepancyType = "Excess"; // Excédent
            }
            else
            {
                DiscrepancyType = "None"; // Pas d'écart
            }
        }
    }
}
