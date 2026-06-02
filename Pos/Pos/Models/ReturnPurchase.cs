using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ReturnPurchase")]
[Index("CashRegisterId", Name = "IX_ReturnPurchase_CashRegisterId")]
[Index("PurchaseId", Name = "IX_ReturnPurchase_PurchaseId")]
[Index("SupplierId", Name = "IX_ReturnPurchase_SupplierId")]
[Index("UserId", Name = "IX_ReturnPurchase_UserId")]
[Index("WarehouseId", Name = "IX_ReturnPurchase_WarehouseId")]
public partial class ReturnPurchase
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [StringLength(250)]
    public string Action { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? UserId { get; set; }

    public int? PurchaseId { get; set; }

    public int? CashRegisterId { get; set; }

    public int? SupplierId { get; set; }

    public int? WarehouseId { get; set; }

    public int? Item { get; set; }

    public int? TotalQty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalDiscount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OrderTaxRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OrderTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GrandTotal { get; set; }

    [Column(TypeName = "text")]
    public string Doc { get; set; }

    [Column(TypeName = "text")]
    public string ReturnNote { get; set; }

    [Column(TypeName = "text")]
    public string StaffNote { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CashRegisterId")]
    [InverseProperty("ReturnPurchases")]
    public virtual RegisterRecord CashRegister { get; set; }

    [InverseProperty("Return")]
    public virtual ICollection<ProductPurchaseReturn> ProductPurchaseReturns { get; set; } = new List<ProductPurchaseReturn>();

    [ForeignKey("PurchaseId")]
    [InverseProperty("ReturnPurchases")]
    public virtual Purchase Purchase { get; set; }

    [ForeignKey("SupplierId")]
    [InverseProperty("ReturnPurchases")]
    public virtual Supplier Supplier { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ReturnPurchases")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("ReturnPurchases")]
    public virtual Warehouse Warehouse { get; set; }
}
