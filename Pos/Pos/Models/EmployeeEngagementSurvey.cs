using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

public partial class EmployeeEngagementSurvey
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? SurveyDate { get; set; }

    public int? EngagementScore { get; set; }

    [StringLength(255)]
    public string Comments { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }
}
