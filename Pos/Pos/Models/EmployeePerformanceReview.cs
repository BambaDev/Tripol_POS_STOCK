using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("EmployeePerformanceReview")]
[Index("EmployeeReviewId", Name = "IX_EmployeePerformanceReview_EmployeeReviewId")]
[Index("EvaluationId", Name = "IX_EmployeePerformanceReview_EvaluationId")]
public partial class EmployeePerformanceReview
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string EvaluationName { get; set; }

    public int? EvaluationId { get; set; }

    public bool? Box1 { get; set; }

    public bool? Box2 { get; set; }

    public bool? Box3 { get; set; }

    public bool? Box4 { get; set; }

    public bool? Box5 { get; set; }

    public int? EmployeeReviewId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeReviewId")]
    [InverseProperty("EmployeePerformanceReviews")]
    public virtual EmployeeReview EmployeeReview { get; set; }

    [ForeignKey("EvaluationId")]
    [InverseProperty("EmployeePerformanceReviews")]
    public virtual Evaluation Evaluation { get; set; }
}
