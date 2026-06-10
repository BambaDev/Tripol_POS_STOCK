using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Pos.Function;

namespace Pos.Models;

[Table("Supplier")]
[Index("UserId", Name = "IX_Supplier_UserID")]
[SensitiveEntity(SensitivityLevel.Confidential, "Supplier contact information")]
public partial class Supplier
{
    [Key]
    [NoEncryption("Primary key - technical field")]
    public int Id { get; set; }

    [StringLength(250)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Supplier first name")]
    public string FirstName { get; set; }

    [StringLength(250)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.PII,
        RequiresEncryption = true,
        Description = "Supplier last name")]
    public string LastName { get; set; }

    [StringLength(250)]
    [SensitiveData(SensitivityLevel.Confidential, DataCategory.Contact,
        RequiresEncryption = true,
        Description = "Supplier email address")]
    public string Email { get; set; }

    [Column(TypeName = "image")]
    [SensitiveData(SensitivityLevel.Restricted, DataCategory.Biometric,
        RequiresEncryption = true,
        Description = "Supplier photo")]
    public byte[] Image { get; set; }

    [StringLength(250)]
    [SensitiveData(SensitivityLevel.Internal, DataCategory.PII,
        Description = "Supplier gender")]
    public string Gender { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("Supplier")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [ForeignKey("UserId")]
    [InverseProperty("Suppliers")]
    public virtual User User { get; set; }
}
