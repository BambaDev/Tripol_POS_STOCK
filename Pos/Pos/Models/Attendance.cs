using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Attendance")]
[Index("EmployeeId", Name = "IX_Attendance_EmployeeId")]
[Index("UserId", Name = "IX_Attendance_UserId")]
public partial class Attendance
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public TimeOnly? TimeIn { get; set; }

    public TimeOnly? TimeOut { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? HoursWorked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Attendances")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Attendances")]
    public virtual User User { get; set; }
}
