using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[PrimaryKey("EmployeeId", "TransportId")]
[Table("EmployeeTransport")]
[Index("TransportId", Name = "IX_EmployeeTransport_TransportId")]
public partial class EmployeeTransport
{
    [Key]
    public int EmployeeId { get; set; }

    [Key]
    public int TransportId { get; set; }

    [StringLength(255)]
    public string PickupLocation { get; set; }

    [StringLength(255)]
    public string DropLocation { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeTransports")]
    public virtual Employee Employee { get; set; }
}
