using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("SaleDetailHold")]
[Index("ProductId", Name = "IX_SaleDetailHold_ProductId")]
[Index("SaleId", Name = "IX_SaleDetailHold_SaleId")]
public partial class SaleDetailHold
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ProductName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SaleQuantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitCostBd { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountPercent { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitCostBt { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitSellingPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ProfitMargin { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LineTotal { get; set; }

    public int? ProductId { get; set; }

    public int? SaleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("SaleDetailHolds")]
    public virtual Product Product { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("SaleDetailHolds")]
    public virtual SaleHold Sale { get; set; }
}
