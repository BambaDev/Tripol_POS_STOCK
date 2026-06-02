using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductTransfer")]
[Index("ProductBatchId", Name = "IX_ProductTransfer_ProductBatchId")]
[Index("ProductId", Name = "IX_ProductTransfer_ProductId")]
[Index("TransferId", Name = "IX_ProductTransfer_TransferId")]
[Index("VariantId", Name = "IX_ProductTransfer_VariantId")]
public partial class ProductTransfer
{
    [Key]
    public int Id { get; set; }

    public int? TransferId { get; set; }

    public int? ProductId { get; set; }

    public int? ProductBatchId { get; set; }

    public int? VariantId { get; set; }

    [Column(TypeName = "text")]
    public string ImeiNumber { get; set; }

    public int? Qty { get; set; }

    public int? PurchaseUnitId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetUnitCost { get; set; }

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
    [InverseProperty("ProductTransfers")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductBatchId")]
    [InverseProperty("ProductTransfers")]
    public virtual ProductBatch ProductBatch { get; set; }

    [ForeignKey("TransferId")]
    [InverseProperty("ProductTransfers")]
    public virtual Transfer Transfer { get; set; }

    [ForeignKey("VariantId")]
    [InverseProperty("ProductTransfers")]
    public virtual Variation Variant { get; set; }
}
