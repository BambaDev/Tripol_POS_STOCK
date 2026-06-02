using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("WasteItem")]
[Index("WasteId", Name = "IX_WasteItem_WasteId")]
public partial class WasteItem
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ItemName { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? WasteAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LastPurchasePrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal LossAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Qty { get; set; }

    public int? WasteId { get; set; }

    public int? ProductId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("WasteId")]
    [InverseProperty("WasteItems")]
    public virtual Product Waste { get; set; }

    [ForeignKey("WasteId")]
    [InverseProperty("WasteItems")]
    public virtual Waste WasteNavigation { get; set; }
}
