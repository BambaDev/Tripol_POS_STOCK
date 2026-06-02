using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("VariationValue")]
[Index("VariationId", Name = "IX_VariationValue_VariationID")]
public partial class VariationValue
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Value { get; set; }

    [Column("VariationID")]
    public int? VariationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("VariationId")]
    [InverseProperty("VariationValues")]
    public virtual Variation Variation { get; set; }
}
