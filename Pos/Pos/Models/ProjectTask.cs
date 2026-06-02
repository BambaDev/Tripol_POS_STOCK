using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("AssignedTo", Name = "IX_ProjectTasks_AssignedTo")]
[Index("ProjectId", Name = "IX_ProjectTasks_ProjectId")]
[Index("UserId", Name = "IX_ProjectTasks_UserId")]
public partial class ProjectTask
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public int? AssignedTo { get; set; }

    public DateOnly? DueDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    [StringLength(50)]
    public string Priority { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("ProjectTasks")]
    public virtual Employee AssignedToNavigation { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectTasks")]
    public virtual Project Project { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ProjectTasks")]
    public virtual User User { get; set; }
}
