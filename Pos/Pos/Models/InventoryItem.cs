using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("InventoryItem")]
[Index("InventoryId", Name = "IX_InventoryItem_InventoryId")]
[Index("ItemId", Name = "IX_InventoryItem_ItemId")]
public partial class InventoryItem
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? TheoricalStock { get; set; }

    public int? PhysicalStock { get; set; }

    public int? Gap { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiffAmount { get; set; }

    public int? ItemId { get; set; }

    public int? InventoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("InventoryId")]
    [InverseProperty("InventoryItems")]
    public virtual Inventory Inventory { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("InventoryItems")]
    public virtual Product Item { get; set; }
}
