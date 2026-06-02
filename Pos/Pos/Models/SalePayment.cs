using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("SalePayment")]
[Index("BusinessLocationId", Name = "IX_SalePayment_BusinessLocationId")]
[Index("CustomerId", Name = "IX_SalePayment_CustomerId")]
[Index("SaleId", Name = "IX_SalePayment_SaleId")]
[Index("UserId", Name = "IX_SalePayment_UserId")]
public partial class SalePayment
{
    [Key]
    public int Id { get; set; }

    public int? SaleId { get; set; }

    public int? CustomerId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Remaining { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Paid { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Due { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DueDate { get; set; }

    public bool? IsPaid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("SalePayments")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("SalePayments")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("SalePayments")]
    public virtual Sale Sale { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SalePayments")]
    public virtual User User { get; set; }
}
