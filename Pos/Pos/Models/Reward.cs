using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Reward")]
public partial class Reward
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointsRequired { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Reward")]
    public virtual ICollection<Redemption> Redemptions { get; set; } = new List<Redemption>();

    [InverseProperty("Reward")]
    public virtual ICollection<RewardHistory> RewardHistories { get; set; } = new List<RewardHistory>();
}
