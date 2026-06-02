using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductVariant")]
[Index("ProductId", Name = "IX_ProductVariant_ProductId")]
[Index("VariationId", Name = "IX_ProductVariant_VariationId")]
public partial class ProductVariant
{
    [Key]
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? VariationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductVariants")]
    public virtual Product Product { get; set; }

    [ForeignKey("VariationId")]
    [InverseProperty("ProductVariants")]
    public virtual Variation Variation { get; set; }
}
