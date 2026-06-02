using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("CustomerTier")]
public partial class CustomerTier
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ThresholdPoints { get; set; }

    [Column(TypeName = "text")]
    public string Benefits { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointMultiplier { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountRate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("TierLevel")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
