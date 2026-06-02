using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Adjustment")]
[Index("UserId", Name = "IX_Adjustment_UserId")]
[Index("WarehouseId", Name = "IX_Adjustment_WarehouseId")]
public partial class Adjustment
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [StringLength(250)]
    public string Doc { get; set; }

    [StringLength(250)]
    public string Action { get; set; }

    public int? TotalQty { get; set; }

    public int? Item { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    public int? WarehouseId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Adjustment")]
    public virtual ICollection<ProductAdjustment> ProductAdjustments { get; set; } = new List<ProductAdjustment>();

    [ForeignKey("UserId")]
    [InverseProperty("Adjustments")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Adjustments")]
    public virtual Warehouse Warehouse { get; set; }
}
