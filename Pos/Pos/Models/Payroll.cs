using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

[Table("Payroll")]
[Index("EmployeeId", Name = "IX_Payroll_EmployeeId")]
[Index("UserId", Name = "IX_Payroll_UserId")]
[SensitiveEntity(SensitivityLevel.Confidential, "Employee payroll and salary data")]
public partial class Payroll
{
    [Key]
    [NoEncryption("Primary key - technical field")]
    public int Id { get; set; }

    [NoEncryption("Foreign key reference")]
    public int? EmployeeId { get; set; }

    [NoEncryption("Foreign key reference")]
    public int? UserId { get; set; }

    [NoEncryption("Period dates - not PII")]
    public DateOnly? PayPeriodStart { get; set; }

    [NoEncryption("Period dates - not PII")]
    public DateOnly? PayPeriodEnd { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Financial,
        RequiresEncryption = true,
        Description = "Gross pay amount",
        LegalBasis = "Contract",
        SubjectToErasure = false,
        RetentionDays = 3650)] // 10 years for accounting/tax
    public decimal? GrossPay { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Financial,
        RequiresEncryption = true,
        Description = "Net pay amount",
        LegalBasis = "Contract",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public decimal? NetPay { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Financial,
        RequiresEncryption = true,
        Description = "Total deductions",
        LegalBasis = "Legal Obligation",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public decimal? Deductions { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Financial,
        RequiresEncryption = true,
        Description = "Overtime pay",
        LegalBasis = "Contract",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public decimal? Overtime { get; set; }

    [NoEncryption("Technical timestamp")]
    public DateTime? CreatedAt { get; set; }

    [NoEncryption("Technical timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Payrolls")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payrolls")]
    public virtual User User { get; set; }
}
