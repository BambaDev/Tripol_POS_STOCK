using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

[Table("Company")]
[SensitiveEntity(SensitivityLevel.Confidential, "Company financial and contact information")]
public partial class Company
{
    [Key]
    [NoEncryption("Primary key - technical field")]
    public int Id { get; set; }

    [Column(TypeName = "image")]
    [NoEncryption("Company logo - not PII")]
    public byte[] Logo { get; set; }

    [Column("Company")]
    [StringLength(250)]
    [NoEncryption("Company name - public information")]
    public string Company1 { get; set; }

    [Column(TypeName = "text")]
    [NoEncryption("Company description - public information")]
    public string Description { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Company email address")]
    public string Email { get; set; }

    [StringLength(250)]
    [NoEncryption("Website - public information")]
    public string Website { get; set; }

    [Column(TypeName = "text")]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Company address")]
    public string Address { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Company phone number")]
    public string Tel { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.Contact,
        Description = "Company fax number")]
    public string Fax { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Classified, DataCategory.Financial,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Bank account number",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public string Compte { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Classified, DataCategory.Financial,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Bank RIB (Relevé d'Identité Bancaire)",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public string Rib { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Legal,
        RequiresEncryption = true,
        Description = "NIS (Numéro d'Identification Statistique)",
        SubjectToErasure = false)]
    public string Nis { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Legal,
        RequiresEncryption = true,
        Description = "RC (Registre de Commerce)",
        SubjectToErasure = false)]
    public string Rc { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Legal,
        Description = "AI (Article d'Imposition)")]
    public string Ai { get; set; }

    [StringLength(150)]
    [SensitiveData(SensitivityLevel.Classified, DataCategory.Financial,
        RequiresEncryption = true,
        LogAccessAttempts = true,
        Description = "Tax ID / Fiscal identifier",
        LegalBasis = "Legal Obligation",
        SubjectToErasure = false,
        RetentionDays = 3650)]
    public string IdFiscal { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Companies")]
    public virtual Customer Customer { get; set; }
}
