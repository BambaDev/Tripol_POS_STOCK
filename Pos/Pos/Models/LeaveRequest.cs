using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_LeaveRequests_EmployeeId")]
public partial class LeaveRequest
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(50)]
    public string LeaveType { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [StringLength(255)]
    public string Reason { get; set; }

    [StringLength(50)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("LeaveRequests")]
    public virtual Employee Employee { get; set; }
}
