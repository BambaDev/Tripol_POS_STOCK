using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[PrimaryKey("EmployeeId", "TrainingProgramId")]
[Table("EmployeeTraining")]
[Index("TrainingProgramId", Name = "IX_EmployeeTraining_TrainingProgramId")]
public partial class EmployeeTraining
{
    [Key]
    public int EmployeeId { get; set; }

    [Key]
    public int TrainingProgramId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public DateOnly? CompletionDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeTrainings")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("TrainingProgramId")]
    [InverseProperty("EmployeeTrainings")]
    public virtual TrainingProgram TrainingProgram { get; set; }
}
