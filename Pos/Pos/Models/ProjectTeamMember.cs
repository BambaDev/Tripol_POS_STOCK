using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_ProjectTeamMembers_EmployeeId")]
[Index("ProjectId", Name = "IX_ProjectTeamMembers_ProjectId")]
[Index("UserId", Name = "IX_ProjectTeamMembers_UserId")]
public partial class ProjectTeamMember
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(50)]
    public string Role { get; set; }

    public DateOnly? JoinDate { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("ProjectTeamMembers")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectTeamMembers")]
    public virtual Project Project { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ProjectTeamMembers")]
    public virtual User User { get; set; }
}
