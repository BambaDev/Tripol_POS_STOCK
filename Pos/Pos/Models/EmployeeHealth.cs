using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

[Table("EmployeeHealth")]
[Index("UserId", Name = "IX_EmployeeHealth_UserId")]
[SensitiveEntity(SensitivityLevel.Restricted, "Employee health data - Article 9 GDPR special category")]
public partial class EmployeeHealth
{
    [Key]
    [NoEncryption("Primary key - technical field")]
    public int Id { get; set; }

    [NoEncryption("Foreign key reference")]
    public int? EmployeeId { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Health,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        MaskInAuditTrail = true,
        Description = "Health condition - Article 9 GDPR special category",
        LegalBasis = "Explicit Consent",
        SubjectToErasure = true,
        RetentionDays = 1825)] // 5 years after end of employment
    public string HealthCondition { get; set; }

    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Health,
        LogAccessAttempts = true,
        Description = "Date of diagnosis - Article 9 GDPR",
        LegalBasis = "Explicit Consent",
        SubjectToErasure = true)]
    public DateOnly? DateDiagnosed { get; set; }

    [StringLength(255)]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Health,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        MaskInAuditTrail = true,
        Description = "Medical notes - Article 9 GDPR",
        LegalBasis = "Explicit Consent",
        SubjectToErasure = true,
        RetentionDays = 1825)]
    public string Notes { get; set; }

    [NoEncryption("Foreign key reference")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    [NoEncryption("Technical timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    [NoEncryption("Technical timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeHealths")]
    public virtual User User { get; set; }
}
