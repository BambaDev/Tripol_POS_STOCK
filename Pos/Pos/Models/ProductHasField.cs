using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductHasField")]
[Index("ProductFieldId", Name = "IX_ProductHasField_ProductFieldId")]
[Index("ProductId", Name = "IX_ProductHasField_ProductId")]
public partial class ProductHasField
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Value { get; set; }

    public int? ProductFieldId { get; set; }

    public int? ProductId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductHasFields")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductFieldId")]
    [InverseProperty("ProductHasFields")]
    public virtual ProductField ProductField { get; set; }
}
