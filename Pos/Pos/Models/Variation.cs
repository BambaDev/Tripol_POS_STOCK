using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Variation")]
public partial class Variation
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Variant")]
    public virtual ICollection<ProductAdjustment> ProductAdjustments { get; set; } = new List<ProductAdjustment>();

    [InverseProperty("Variant")]
    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = new List<ProductReturn>();

    [InverseProperty("Variant")]
    public virtual ICollection<ProductTransfer> ProductTransfers { get; set; } = new List<ProductTransfer>();

    [InverseProperty("Variation")]
    public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; } = new List<ProductVariantValue>();

    [InverseProperty("Variation")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    [InverseProperty("Variant")]
    public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    [InverseProperty("Variation")]
    public virtual ICollection<VariationValue> VariationValues { get; set; } = new List<VariationValue>();
}
