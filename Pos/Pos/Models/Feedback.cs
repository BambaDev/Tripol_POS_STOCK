using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    public int Id { get; set; }

    public int DeliveryId { get; set; }

    public int CustomerId { get; set; }

    public int Rating { get; set; }

    [StringLength(255)]
    public string Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Feedbacks")]
    public virtual Customer Customer { get; set; }
}
