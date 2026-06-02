using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

public partial class FoodService
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string ServiceName { get; set; }

    [StringLength(50)]
    public string ServiceType { get; set; }

    [StringLength(255)]
    public string Menu { get; set; }

    [StringLength(255)]
    public string Schedule { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("FoodService")]
    public virtual ICollection<EmployeeFoodService> EmployeeFoodServices { get; set; } = new List<EmployeeFoodService>();
}
