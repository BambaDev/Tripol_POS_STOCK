using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("ProjectId", Name = "IX_ProjectDocuments_ProjectId")]
[Index("UserId", Name = "IX_ProjectDocuments_UserId")]
public partial class ProjectDocument
{
    [Key]
    public int Id { get; set; }

    public int? ProjectId { get; set; }

    [StringLength(100)]
    public string DocumentName { get; set; }

    [StringLength(50)]
    public string DocumentType { get; set; }

    [StringLength(255)]
    public string DocumentPath { get; set; }

    public DateOnly? UploadDate { get; set; }

    public int? UploadedBy { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectDocuments")]
    public virtual Project Project { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("ProjectDocuments")]
    public virtual User User { get; set; }
}
