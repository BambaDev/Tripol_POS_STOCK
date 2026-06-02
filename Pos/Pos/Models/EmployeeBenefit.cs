using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[PrimaryKey("EmployeeId", "BenefitId")]
[Index("BenefitId", Name = "IX_EmployeeBenefits_BenefitId")]
public partial class EmployeeBenefit
{
    [Key]
    public int EmployeeId { get; set; }

    [Key]
    public int BenefitId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BenefitId")]
    [InverseProperty("EmployeeBenefits")]
    public virtual Benefit Benefit { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeBenefits")]
    public virtual Employee Employee { get; set; }
}
