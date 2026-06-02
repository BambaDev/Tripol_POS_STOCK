using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductFieldValue")]
[Index("ProductFieldId", Name = "IX_ProductFieldValue_ProductFieldId")]
public partial class ProductFieldValue
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "text")]
    public string Value { get; set; }

    public int? ProductFieldId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductFieldId")]
    [InverseProperty("ProductFieldValues")]
    public virtual ProductField ProductField { get; set; }
}
