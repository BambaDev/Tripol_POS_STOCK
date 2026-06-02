using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductPurchaseReturn")]
[Index("ProductBatchId", Name = "IX_ProductPurchaseReturn_ProductBatchId")]
[Index("ProductId", Name = "IX_ProductPurchaseReturn_ProductId")]
[Index("ReturnId", Name = "IX_ProductPurchaseReturn_ReturnId")]
public partial class ProductPurchaseReturn
{
    [Key]
    public int Id { get; set; }

    public int? ReturnId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductBatchId { get; set; }

    public int? VariantId { get; set; }

    [StringLength(250)]
    public string ImeiNumber { get; set; }

    public int? Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetUnitPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Discount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TaxRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Tax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Total { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductPurchaseReturns")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductBatchId")]
    [InverseProperty("ProductPurchaseReturns")]
    public virtual ProductBatch ProductBatch { get; set; }

    [ForeignKey("ReturnId")]
    [InverseProperty("ProductPurchaseReturns")]
    public virtual ReturnPurchase Return { get; set; }
}
