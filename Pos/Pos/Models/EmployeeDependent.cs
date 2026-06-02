using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Index("EmployeeId", Name = "IX_EmployeeDependents_EmployeeId")]
public partial class EmployeeDependent
{
    [Key]
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Relationship { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeDependents")]
    public virtual Employee Employee { get; set; }
}
