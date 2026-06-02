using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Payroll")]
[Index("EmployeeId", Name = "IX_Payroll_EmployeeId")]
[Index("UserId", Name = "IX_Payroll_UserId")]
public partial class Payroll
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? PayPeriodStart { get; set; }

    public DateOnly? PayPeriodEnd { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? GrossPay { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? NetPay { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Deductions { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Overtime { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Payrolls")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Payrolls")]
    public virtual User User { get; set; }
}
