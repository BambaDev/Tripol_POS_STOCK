using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("SaleHold")]
[Index("BusinessLocationId", Name = "IX_SaleHold_BusinessLocationId")]
[Index("CustomerId", Name = "IX_SaleHold_CustomerId")]
[Index("UserId", Name = "IX_SaleHold_UserId")]
[Index("WarehouseId", Name = "IX_SaleHold_WarehouseId")]
public partial class SaleHold
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string SaleType { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SaleDate { get; set; }

    [StringLength(250)]
    public string SaleStatus { get; set; }

    [StringLength(250)]
    public string DiscountType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "text")]
    public string AdditionalNotes { get; set; }

    [Column(TypeName = "text")]
    public string ShippingDetails { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AdditionalShippingCharges { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetTotalAmount { get; set; }

    [StringLength(250)]
    public string PaymentSatus { get; set; }

    public int? NumberItems { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PaidAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalDiscount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Due { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ReturnAmount { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public int? SaleYear { get; set; }

    public int? SaleMonth { get; set; }

    public int? CustomerId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? WarehouseId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("SaleHolds")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("SaleHolds")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("Sale")]
    public virtual ICollection<SaleDetailHold> SaleDetailHolds { get; set; } = new List<SaleDetailHold>();

    [ForeignKey("UserId")]
    [InverseProperty("SaleHolds")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("SaleHolds")]
    public virtual Warehouse Warehouse { get; set; }
}
