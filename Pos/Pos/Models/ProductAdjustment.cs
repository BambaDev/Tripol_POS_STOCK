using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductAdjustment")]
[Index("AdjustmentId", Name = "IX_ProductAdjustment_AdjustmentId")]
[Index("ProductId", Name = "IX_ProductAdjustment_ProductId")]
[Index("VariantId", Name = "IX_ProductAdjustment_VariantId")]
public partial class ProductAdjustment
{
    [Key]
    public int Id { get; set; }

    public int? Qty { get; set; }

    [StringLength(250)]
    public string Action { get; set; }

    public int? AdjustmentId { get; set; }

    public int? ProductId { get; set; }

    public int? VariantId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AdjustmentId")]
    [InverseProperty("ProductAdjustments")]
    public virtual Adjustment Adjustment { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductAdjustments")]
    public virtual Product Product { get; set; }

    [ForeignKey("VariantId")]
    [InverseProperty("ProductAdjustments")]
    public virtual Variation Variant { get; set; }
}
