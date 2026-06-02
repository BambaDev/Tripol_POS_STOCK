using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Transfer")]
[Index("FromWarehouseId", Name = "IX_Transfer_FromWarehouseId")]
[Index("ToWarehouseId", Name = "IX_Transfer_ToWarehouseId")]
[Index("UserId", Name = "IX_Transfer_UserId")]
public partial class Transfer
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [StringLength(250)]
    public string Action { get; set; }

    public int? UserId { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? FromWarehouseId { get; set; }

    public int? ToWarehouseId { get; set; }

    public int? Item { get; set; }

    public int? TotalQty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ShippingCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GrandTotal { get; set; }

    [Column(TypeName = "text")]
    public string Doc { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("FromWarehouseId")]
    [InverseProperty("TransferFromWarehouses")]
    public virtual Warehouse FromWarehouse { get; set; }

    [InverseProperty("Transfer")]
    public virtual ICollection<ProductTransfer> ProductTransfers { get; set; } = new List<ProductTransfer>();

    [ForeignKey("ToWarehouseId")]
    [InverseProperty("TransferToWarehouses")]
    public virtual Warehouse ToWarehouse { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Transfers")]
    public virtual User User { get; set; }
}
