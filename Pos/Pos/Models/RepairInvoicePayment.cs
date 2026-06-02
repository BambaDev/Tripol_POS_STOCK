using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RepairInvoicePayment")]
[Index("BusinessLocationId", Name = "IX_RepairInvoicePayment_BusinessLocationId")]
[Index("CustomerId", Name = "IX_RepairInvoicePayment_CustomerId")]
[Index("RepairInvoiceId", Name = "IX_RepairInvoicePayment_RepairInvoiceId")]
[Index("UserId", Name = "IX_RepairInvoicePayment_UserId")]
public partial class RepairInvoicePayment
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
    [InverseProperty("RepairInvoicePayments")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RepairInvoicePayments")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("RepairInvoiceId")]
    [InverseProperty("RepairInvoicePayments")]
    public virtual RepairInvoice RepairInvoice { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("RepairInvoicePayments")]
    public virtual User User { get; set; }
}
