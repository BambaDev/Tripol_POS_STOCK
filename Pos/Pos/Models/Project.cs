using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("CustomerId", Name = "IX_Projects_CustomerId")]
[Index("ManagerId", Name = "IX_Projects_ManagerId")]
[Index("UserId", Name = "IX_Projects_UserId")]
public partial class Project
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Budget { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    public int? ManagerId { get; set; }

    public int? CustomerId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Projects")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("ManagerId")]
    [InverseProperty("Projects")]
    public virtual Employee Manager { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; } = new List<ProjectDocument>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectMilestone> ProjectMilestones { get; set; } = new List<ProjectMilestone>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; } = new List<ProjectTeamMember>();

    [ForeignKey("UserId")]
    [InverseProperty("Projects")]
    public virtual User User { get; set; }
}
