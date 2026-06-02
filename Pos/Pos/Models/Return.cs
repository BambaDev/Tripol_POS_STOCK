using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Return")]
[Index("CashRegisterId", Name = "IX_Return_CashRegisterId")]
[Index("CustomerId", Name = "IX_Return_CustomerId")]
[Index("SaleId", Name = "IX_Return_SaleId")]
[Index("UserId", Name = "IX_Return_UserId")]
[Index("WarehouseId", Name = "IX_Return_WarehouseId")]
public partial class Return
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [StringLength(250)]
    public string Action { get; set; }

    public int? UserId { get; set; }

    public int? SaleId { get; set; }

    public int? CashRegisterId { get; set; }

    public int? CustomerId { get; set; }

    public int? WarehouseId { get; set; }

    public int? Item { get; set; }

    public int? TotalQty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalDiscount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalPrice { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OrderTaxRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OrderTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GrandTotal { get; set; }

    [Column(TypeName = "text")]
    public string Doc { get; set; }

    [Column(TypeName = "text")]
    public string ReturnNote { get; set; }

    [Column(TypeName = "text")]
    public string StaffNote { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CashRegisterId")]
    [InverseProperty("Returns")]
    public virtual RegisterRecord CashRegister { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Returns")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("Return")]
    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = new List<ProductReturn>();

    [ForeignKey("SaleId")]
    [InverseProperty("Returns")]
    public virtual Sale Sale { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Returns")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Returns")]
    public virtual Warehouse Warehouse { get; set; }
}
