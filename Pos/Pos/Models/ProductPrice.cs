using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductPrice")]
[Index("PriceGroupId", Name = "IX_ProductPrice_PriceGroupId")]
[Index("ProductId", Name = "IX_ProductPrice_ProductId")]
public partial class ProductPrice
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string GroupPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? ProductId { get; set; }

    public int? PriceGroupId { get; set; }

    public int? UnitId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PriceGroupId")]
    [InverseProperty("ProductPrices")]
    public virtual PriceGroup PriceGroup { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductPrices")]
    public virtual Product Product { get; set; }
}
