using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RewardHistory")]
[Index("CustomerId", Name = "IX_RewardHistory_CustomerId")]
[Index("RewardId", Name = "IX_RewardHistory_RewardId")]
public partial class RewardHistory
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateClaimed { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? CustomerId { get; set; }

    public int? RewardId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RewardHistories")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("RewardId")]
    [InverseProperty("RewardHistories")]
    public virtual Reward Reward { get; set; }
}
