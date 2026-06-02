using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_EmployeeShiftSchedules_EmployeeId")]
public partial class EmployeeShiftSchedule
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? ShiftDate { get; set; }

    public TimeOnly? ShiftStartTime { get; set; }

    public TimeOnly? ShiftEndTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeShiftSchedules")]
    public virtual Employee Employee { get; set; }
}
