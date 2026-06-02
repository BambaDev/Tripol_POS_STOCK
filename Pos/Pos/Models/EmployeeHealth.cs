using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("EmployeeHealth")]
[Index("UserId", Name = "IX_EmployeeHealth_UserId")]
public partial class EmployeeHealth
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(255)]
    public string HealthCondition { get; set; }

    public DateOnly? DateDiagnosed { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeHealths")]
    public virtual User User { get; set; }
}
