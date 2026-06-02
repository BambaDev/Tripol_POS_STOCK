using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("PurchaseDetail")]
[Index("PurchaseId", Name = "IX_PurchaseDetail_PurchaseId")]
public partial class PurchaseDetail
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ProductName { get; set; }

    public int? PurchaseQuantity { get; set; }

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

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ManufacturingDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LineTotal { get; set; }

    public int? ProductId { get; set; }

    public int? PurchaseId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PurchaseId")]
    [InverseProperty("PurchaseDetails")]
    public virtual Purchase Purchase { get; set; }
}
