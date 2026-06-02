using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Tax")]
public partial class Tax
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Rate { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }
}
