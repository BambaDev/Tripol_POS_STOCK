using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[PrimaryKey("EmployeeId", "FoodServiceId")]
[Index("FoodServiceId", Name = "IX_EmployeeFoodServices_FoodServiceId")]
public partial class EmployeeFoodService
{
    [Key]
    public int EmployeeId { get; set; }

    [Key]
    public int FoodServiceId { get; set; }

    public DateOnly? SubscriptionDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeFoodServices")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("FoodServiceId")]
    [InverseProperty("EmployeeFoodServices")]
    public virtual FoodService FoodService { get; set; }
}
