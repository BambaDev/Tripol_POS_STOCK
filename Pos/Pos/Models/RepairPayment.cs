using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RepairPayment")]
[Index("BusinessLocationId", Name = "IX_RepairPayment_BusinessLocationId")]
[Index("CustomerId", Name = "IX_RepairPayment_CustomerId")]
[Index("RepairInvoiceId", Name = "IX_RepairPayment_RepairInvoiceId")]
[Index("UserId", Name = "IX_RepairPayment_UserId")]
public partial class RepairPayment
{
    [Key]
    public int Id { get; set; }

    public int? RepairInvoiceId { get; set; }

    public int? CustomerId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

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
    [InverseProperty("RepairPayments")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RepairPayments")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("RepairInvoiceId")]
    [InverseProperty("RepairPayments")]
    public virtual RepairInvoice RepairInvoice { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("RepairPayments")]
    public virtual User User { get; set; }
}
