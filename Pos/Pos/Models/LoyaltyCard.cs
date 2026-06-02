using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("LoyaltyCard")]
public partial class LoyaltyCard
{
    [Key]
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? CardTypeId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Points { get; set; }

    [StringLength(255)]
    public string Sku { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CardTypeId")]
    [InverseProperty("LoyaltyCards")]
    public virtual CardType CardType { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("LoyaltyCards")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("LoyaltyCard")]
    public virtual ICollection<PointsTransaction> PointsTransactions { get; set; } = new List<PointsTransaction>();
}
