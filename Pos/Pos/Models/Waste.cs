using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Waste")]
[Index("BusinessLocationId", Name = "IX_Waste_BusinessLocationId")]
[Index("EmployeeId", Name = "IX_Waste_EmployeeId")]
[Index("UserId", Name = "IX_Waste_UserId")]
public partial class Waste
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    public DateOnly? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalLoss { get; set; }

    [Column(TypeName = "text")]
    public string Note { get; set; }

    public int? Items { get; set; }

    public int? EmployeeId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Wastes")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Wastes")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Wastes")]
    public virtual User User { get; set; }

    [InverseProperty("WasteNavigation")]
    public virtual ICollection<WasteItem> WasteItems { get; set; } = new List<WasteItem>();
}
