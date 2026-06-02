using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ActivityLog")]
[Index("CustomerId", Name = "IX_ActivityLog_CustomerId")]
public partial class ActivityLog
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [StringLength(250)]
    public string PointsChanged { get; set; }

    public int? CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ActivityLogs")]
    public virtual Customer Customer { get; set; }
}
