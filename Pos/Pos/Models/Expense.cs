using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Expense")]
[Index("ExpenseCategoryId", Name = "IX_Expense_ExpenseCategoryId")]
[Index("UserId", Name = "IX_Expense_UserId")]
[Index("WarehouseId", Name = "IX_Expense_WarehouseId")]
public partial class Expense
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    public int? ExpenseCategoryId { get; set; }

    public int? WarehouseId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ExpenseCategoryId")]
    [InverseProperty("Expenses")]
    public virtual ExpenseCategory ExpenseCategory { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Expenses")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Expenses")]
    public virtual Warehouse Warehouse { get; set; }
}
