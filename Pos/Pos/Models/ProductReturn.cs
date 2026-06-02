using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductReturn")]
[Index("ProductBatchId", Name = "IX_ProductReturn_ProductBatchId")]
[Index("ProductId", Name = "IX_ProductReturn_ProductId")]
[Index("ReturnId", Name = "IX_ProductReturn_ReturnId")]
[Index("VariantId", Name = "IX_ProductReturn_VariantId")]
public partial class ProductReturn
{
    [Key]
    public int Id { get; set; }

    public int? ReturnId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductBatchId { get; set; }

    public int? VariantId { get; set; }

    [Column(TypeName = "text")]
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
    [InverseProperty("ProductReturns")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductBatchId")]
    [InverseProperty("ProductReturns")]
    public virtual ProductBatch ProductBatch { get; set; }

    [ForeignKey("ReturnId")]
    [InverseProperty("ProductReturns")]
    public virtual Return Return { get; set; }

    [ForeignKey("VariantId")]
    [InverseProperty("ProductReturns")]
    public virtual Variation Variant { get; set; }
}
