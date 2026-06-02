using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductBatch")]
public partial class ProductBatch
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Qty { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiredDate { get; set; }

    [StringLength(250)]
    public string BatchNo { get; set; }

    public int? ProductId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("ProductBatch")]
    public virtual ICollection<ProductPurchaseReturn> ProductPurchaseReturns { get; set; } = new List<ProductPurchaseReturn>();

    [InverseProperty("ProductBatch")]
    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = new List<ProductReturn>();

    [InverseProperty("ProductBatch")]
    public virtual ICollection<ProductTransfer> ProductTransfers { get; set; } = new List<ProductTransfer>();

    [InverseProperty("ProductBatch")]
    public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
}
