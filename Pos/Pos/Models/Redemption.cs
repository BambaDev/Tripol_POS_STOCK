using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Redemption")]
[Index("CustomerId", Name = "IX_Redemption_CustomerId")]
[Index("RewardId", Name = "IX_Redemption_RewardId")]
public partial class Redemption
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RedemptionDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PointsSpent { get; set; }

    public int? CustomerId { get; set; }

    public int? RewardId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Redemptions")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("RewardId")]
    [InverseProperty("Redemptions")]
    public virtual Reward Reward { get; set; }
}
