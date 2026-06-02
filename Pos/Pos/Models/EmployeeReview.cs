using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("EmployeeReview")]
[Index("EmployeeId", Name = "IX_EmployeeReview_EmployeeId")]
[Index("InterviewerId", Name = "IX_EmployeeReview_InterviewerId")]
[Index("UserId", Name = "IX_EmployeeReview_UserId")]
public partial class EmployeeReview
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? InterviewerId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewPeriod { get; set; }

    [Column(TypeName = "text")]
    public string Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeReviews")]
    public virtual Employee Employee { get; set; }

    [InverseProperty("EmployeeReview")]
    public virtual ICollection<EmployeePerformanceReview> EmployeePerformanceReviews { get; set; } = new List<EmployeePerformanceReview>();

    [ForeignKey("InterviewerId")]
    [InverseProperty("EmployeeReviewInterviewers")]
    public virtual User Interviewer { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeReviewUsers")]
    public virtual User User { get; set; }
}
