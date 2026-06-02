using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RepairInvoiceDetail")]
[Index("ProductId", Name = "IX_RepairInvoiceDetail_ProductId")]
[Index("RepairInvoiceId", Name = "IX_RepairInvoiceDetail_RepairInvoiceId")]
public partial class RepairInvoiceDetail
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ProductName { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitCostBd { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountPercent { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitCostBt { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitSellingPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ProfitMargin { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LineTotal { get; set; }

    public int? ProductId { get; set; }

    public int? RepairInvoiceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("RepairInvoiceDetails")]
    public virtual Product Product { get; set; }

    [ForeignKey("RepairInvoiceId")]
    [InverseProperty("RepairInvoiceDetails")]
    public virtual RepairInvoice RepairInvoice { get; set; }
}
