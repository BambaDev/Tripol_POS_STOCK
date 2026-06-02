using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("PointsTransaction")]
public partial class PointsTransaction
{
    [Key]
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public int? LoyaltyCardId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public int? SaleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("LoyaltyCardId")]
    [InverseProperty("PointsTransactions")]
    public virtual LoyaltyCard LoyaltyCard { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("PointsTransactions")]
    public virtual Sale Sale { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("PointsTransactions")]
    public virtual CardType Type { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("PointsTransactions")]
    public virtual User User { get; set; }
}
