using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_EmployeeExpenses_EmployeeId")]
[Index("UserId", Name = "IX_EmployeeExpenses_UserId")]
public partial class EmployeeExpense
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(50)]
    public string ExpenseType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    public DateOnly? ExpenseDate { get; set; }

    [StringLength(255)]
    public string Notes { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeExpenses")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeExpenses")]
    public virtual User User { get; set; }
}
