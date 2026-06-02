using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Inventory")]
[Index("UserId", Name = "IX_Inventory_UserId")]
[Index("WarehouseId", Name = "IX_Inventory_WarehouseId")]
public partial class Inventory
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? NbrProducts { get; set; }

    public int? InventoryMonth { get; set; }

    public int? InventoryYear { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiffAmount { get; set; }

    public int? Gap { get; set; }

    public int? WarehouseId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [InverseProperty("Inventory")]
    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    [ForeignKey("UserId")]
    [InverseProperty("Inventories")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Inventories")]
    public virtual Warehouse Warehouse { get; set; }
}
