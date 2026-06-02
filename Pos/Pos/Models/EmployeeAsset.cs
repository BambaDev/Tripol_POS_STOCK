using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[PrimaryKey("EmployeeId", "AssetId")]
[Index("AssetId", Name = "IX_EmployeeAssets_AssetId")]
public partial class EmployeeAsset
{
    [Key]
    public int EmployeeId { get; set; }

    [Key]
    public int AssetId { get; set; }

    public DateOnly? AssignmentDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("AssetId")]
    [InverseProperty("EmployeeAssets")]
    public virtual Asset Asset { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeAssets")]
    public virtual Employee Employee { get; set; }
}
