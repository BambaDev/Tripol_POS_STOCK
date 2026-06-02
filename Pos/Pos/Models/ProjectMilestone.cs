using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("ProjectId", Name = "IX_ProjectMilestones_ProjectId")]
[Index("UserId", Name = "IX_ProjectMilestones_UserId")]
public partial class ProjectMilestone
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public DateOnly? DueDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectMilestones")]
    public virtual Project Project { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ProjectMilestones")]
    public virtual User User { get; set; }
}
