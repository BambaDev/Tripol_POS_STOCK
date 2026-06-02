using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models
{
    /// <summary>
    /// Classe partielle Product pour système d'archivage (Soft Delete)
    /// Permet de "supprimer" sans perdre l'historique des ventes
    /// </summary>
    public partial class Product
    {
        /// <summary>
        /// Indique si le produit est archivé (soft delete)
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Date et heure d'archivage
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? ArchivedAt { get; set; }

        /// <summary>
        /// ID de l'utilisateur qui a archivé le produit
        /// </summary>
        public int? ArchivedBy { get; set; }

        /// <summary>
        /// Raison de l'archivage (optionnel)
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string ArchiveReason { get; set; }

        /// <summary>
        /// Relation vers l'utilisateur qui a archivé
        /// </summary>
        [ForeignKey("ArchivedBy")]
        [InverseProperty("ArchivedProducts")]
        public virtual User ArchivedByUser { get; set; }

        /// <summary>
        /// Archive le produit (soft delete)
        /// </summary>
        public void Archive(int userId, string reason = null)
        {
            IsArchived = true;
            ArchivedAt = DateTime.Now;
            ArchivedBy = userId;
            ArchiveReason = reason ?? "Product archived by user";
        }

        /// <summary>
        /// Restaure le produit archivé
        /// </summary>
        public void Restore()
        {
            IsArchived = false;
            ArchivedAt = null;
            ArchivedBy = null;
            ArchiveReason = null;
        }

        /// <summary>
        /// Vérifie si le produit est actif (non archivé)
        /// </summary>
        [NotMapped]
        public bool IsActive => !IsArchived;
    }
}
