using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

public partial class Benefit
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string BenefitName { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [StringLength(100)]
    public string Provider { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Benefit")]
    public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; } = new List<EmployeeBenefit>();
}
