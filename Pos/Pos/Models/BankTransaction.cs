using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("BankTransaction")]
[Index("BankId", Name = "IX_BankTransaction_BankId")]
[Index("UserId", Name = "IX_BankTransaction_UserId")]
public partial class BankTransaction
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Total { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? GrandTotal { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    public int? BankId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("BankTransactions")]
    public virtual User User { get; set; }
}
