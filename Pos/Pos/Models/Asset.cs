using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

public partial class Asset
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string AssetName { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [StringLength(100)]
    public string SerialNumber { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public DateOnly? WarrantyDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Asset")]
    public virtual ICollection<EmployeeAsset> EmployeeAssets { get; set; } = new List<EmployeeAsset>();
}
