using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Notification")]
public partial class Notification
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Type { get; set; }

    [StringLength(250)]
    public string NotifiableType { get; set; }

    [Column(TypeName = "text")]
    public string Data { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReadAt { get; set; }

    public int? NotifiableId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }
}
