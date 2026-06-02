using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("TodoList")]
[Index("TechnicalId", Name = "IX_TodoList_TechnicalId")]
[Index("UserId", Name = "IX_TodoList_UserId")]
public partial class TodoList
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    public int? TechnicalId { get; set; }

    public int? UserId { get; set; }

    public int? AssignedTo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TodoLists")]
    public virtual User User { get; set; }
}
