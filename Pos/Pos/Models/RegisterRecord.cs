using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RegisterRecord")]
[Index("BusinessLocationId", Name = "IX_RegisterRecord_BusinessLocationId")]
[Index("ClosedById", Name = "IX_RegisterRecord_ClosedById")]
[Index("RegisterId", Name = "IX_RegisterRecord_RegisterId")]
[Index("TransferredToId", Name = "IX_RegisterRecord_TransferredToId")]
[Index("UserId", Name = "IX_RegisterRecord_UserId")]
public partial class RegisterRecord
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? RegisterId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalCashAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalCashSubmitted { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalCheques { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalChequesAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalChequesSubmitted { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalOtherAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalRefundsAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalExpensesAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalGiftCardAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalReturnOrdersAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CashInHand { get; set; }

    public int? ClosedById { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClosedAt { get; set; }

    public int? TransferredToId { get; set; }

    [Column(TypeName = "text")]
    public string Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("RegisterRecords")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("ClosedById")]
    [InverseProperty("RegisterRecordClosedBies")]
    public virtual User ClosedBy { get; set; }

    [ForeignKey("RegisterId")]
    [InverseProperty("RegisterRecords")]
    public virtual Register Register { get; set; }

    [InverseProperty("CashRegister")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [InverseProperty("CashRegister")]
    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    [ForeignKey("TransferredToId")]
    [InverseProperty("RegisterRecordTransferredTos")]
    public virtual User TransferredTo { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("RegisterRecordUsers")]
    public virtual User User { get; set; }

    [InverseProperty("RegisterRecord")]
    public virtual ICollection<CashDiscrepancy> CashDiscrepancies { get; set; } = new List<CashDiscrepancy>();
}
