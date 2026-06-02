using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Warranty")]
public partial class Warranty
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Description { get; set; }

    public int? Duration { get; set; }

    [StringLength(250)]
    public string Type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Warranty")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
