using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Purchase")]
[Index("BusinessLocationId", Name = "IX_Purchase_BusinessLocationId")]
[Index("SupplierId", Name = "IX_Purchase_SupplierId")]
[Index("UserId", Name = "IX_Purchase_UserId")]
[Index("WarehouseId", Name = "IX_Purchase_WarehouseId")]
public partial class Purchase
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string PurchaseType { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PurchaseDate { get; set; }

    [StringLength(250)]
    public string PurchaseStatus { get; set; }

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

    public int? PurchaseYear { get; set; }

    public int? PurchaseMonth { get; set; }

    public int? SupplierId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? WarehouseId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Purchases")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [InverseProperty("Purchase")]
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();

    [InverseProperty("Purchase")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Purchases")]
    public virtual Supplier Supplier { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Purchases")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Purchases")]
    public virtual Warehouse Warehouse { get; set; }
}
