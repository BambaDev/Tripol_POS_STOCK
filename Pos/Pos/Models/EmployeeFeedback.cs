using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("EmployeeFeedback")]
[Index("EmployeeId", Name = "IX_EmployeeFeedback_EmployeeId")]
[Index("FeedbackBy", Name = "IX_EmployeeFeedback_FeedbackBy")]
[Index("UserId", Name = "IX_EmployeeFeedback_UserId")]
public partial class EmployeeFeedback
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? FeedbackBy { get; set; }

    public int? UserId { get; set; }

    [StringLength(50)]
    public string FeedbackType { get; set; }

    [StringLength(255)]
    public string Comments { get; set; }

    public DateOnly? DateGiven { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeFeedbackEmployees")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("FeedbackBy")]
    [InverseProperty("EmployeeFeedbackFeedbackByNavigations")]
    public virtual Employee FeedbackByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("EmployeeFeedbacks")]
    public virtual User User { get; set; }
}
