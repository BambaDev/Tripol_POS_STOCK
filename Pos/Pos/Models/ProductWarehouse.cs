using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductWarehouse")]
[Index("ProductBatchId", Name = "IX_ProductWarehouse_ProductBatchId")]
[Index("ProductId", Name = "IX_ProductWarehouse_ProductId")]
[Index("VariantId", Name = "IX_ProductWarehouse_VariantId")]
[Index("WarehouseId", Name = "IX_ProductWarehouse_WarehouseId")]
public partial class ProductWarehouse
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ImeiNumber { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? ProductId { get; set; }

    public int? ProductBatchId { get; set; }

    public int? VariantId { get; set; }

    public int? WarehouseId { get; set; }

    public int? UnitId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductWarehouses")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductBatchId")]
    [InverseProperty("ProductWarehouses")]
    public virtual ProductBatch ProductBatch { get; set; }

    [ForeignKey("VariantId")]
    [InverseProperty("ProductWarehouses")]
    public virtual Variation Variant { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("ProductWarehouses")]
    public virtual Warehouse Warehouse { get; set; }
}
