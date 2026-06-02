using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("CardType")]
public partial class CardType
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointsPerCurrency { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrencyPerPoint { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("CardType")]
    public virtual ICollection<LoyaltyCard> LoyaltyCards { get; set; } = new List<LoyaltyCard>();

    [InverseProperty("Type")]
    public virtual ICollection<PointsTransaction> PointsTransactions { get; set; } = new List<PointsTransaction>();
}
