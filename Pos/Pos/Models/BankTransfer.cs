using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("BankTransfer")]
[Index("CurrentBankId", Name = "IX_BankTransfer_CurrentBankId")]
[Index("ToBankId", Name = "IX_BankTransfer_ToBankId")]
[Index("UserId", Name = "IX_BankTransfer_UserId")]
public partial class BankTransfer
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Total { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? CurrentBankId { get; set; }

    public int? ToBankId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("BankTransfers")]
    public virtual User User { get; set; }
}
