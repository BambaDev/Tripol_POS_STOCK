using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ComplaintCategory")]
public partial class ComplaintCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [InverseProperty("ComplaintCate")]
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}
