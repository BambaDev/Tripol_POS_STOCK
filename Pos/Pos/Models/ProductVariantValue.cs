using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductVariantValue")]
[Index("ProductId", Name = "IX_ProductVariantValue_ProductId")]
[Index("VariationId", Name = "IX_ProductVariantValue_VariationId")]
public partial class ProductVariantValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Sku { get; set; }

    [StringLength(250)]
    public string Value { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PePriceExc { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PePriceInc { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }

    public int? ProductId { get; set; }

    public int? VariationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductVariantValues")]
    public virtual Product Product { get; set; }

    [ForeignKey("VariationId")]
    [InverseProperty("ProductVariantValues")]
    public virtual Variation Variation { get; set; }
}
