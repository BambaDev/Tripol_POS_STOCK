using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("AuditTrail")]
[Index("UserId", Name = "IX_AuditTrail_UserId")]
public partial class AuditTrail
{
    [Key]
    public int Id { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "text")]
    public string TableName { get; set; }

    [Column(TypeName = "text")]
    public string ActionType { get; set; }

    [Column(TypeName = "text")]
    public string KeyValues { get; set; }

    [Column(TypeName = "text")]
    public string OldValues { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string NewValues { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ChangeTime { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AuditTrails")]
    public virtual User User { get; set; }
}
