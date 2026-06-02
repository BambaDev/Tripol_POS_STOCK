using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Register")]
[Index("BusinessLocationId", Name = "IX_Register_BusinessLocationId")]
public partial class Register
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Code { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Opened { get; set; }

    public int? BusinessLocationId { get; set; }

    [StringLength(250)]
    public string DeviceId { get; set; }

    [StringLength(250)]
    public string TerminalId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Registers")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [InverseProperty("Register")]
    public virtual ICollection<RegisterRecord> RegisterRecords { get; set; } = new List<RegisterRecord>();
}
