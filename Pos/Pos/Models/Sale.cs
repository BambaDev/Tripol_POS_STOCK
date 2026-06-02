using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Sale")]
[Index("BusinessLocationId", Name = "IX_Sale_BusinessLocationId")]
[Index("CustomerId", Name = "IX_Sale_CustomerId")]
[Index("WarehouseId", Name = "IX_Sale_WarehouseId")]
public partial class Sale
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
    public decimal? RemainingBalance { get; set; }

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
    [InverseProperty("Sales")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [InverseProperty("Sale")]
    public virtual ICollection<PointsTransaction> PointsTransactions { get; set; } = new List<PointsTransaction>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Sales")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("Sale")]
    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    [InverseProperty("Sale")]
    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    [InverseProperty("Sale")]
    public virtual ICollection<SalePayment> SalePayments { get; set; } = new List<SalePayment>();

    [ForeignKey("WarehouseId")]
    [InverseProperty("Sales")]
    public virtual Warehouse Warehouse { get; set; }
}
