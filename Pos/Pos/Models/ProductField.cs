using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductField")]
public partial class ProductField
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("ProductField")]
    public virtual ICollection<ProductFieldValue> ProductFieldValues { get; set; } = new List<ProductFieldValue>();

    [InverseProperty("ProductField")]
    public virtual ICollection<ProductHasField> ProductHasFields { get; set; } = new List<ProductHasField>();
}
