using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_EmployeeRewards_EmployeeId")]
[Index("UserId", Name = "IX_EmployeeRewards_UserId")]
public partial class EmployeeReward
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(100)]
    public string RewardType { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public DateOnly? DateAwarded { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeRewards")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeRewards")]
    public virtual User User { get; set; }
}
