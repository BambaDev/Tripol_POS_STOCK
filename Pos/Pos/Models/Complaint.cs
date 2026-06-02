using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Complaint")]
public partial class Complaint
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [StringLength(250)]
    public string Priority { get; set; }

    public int? EmployeeId { get; set; }

    public int? UserId { get; set; }

    public int? ComplaintCateId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ComplaintCateId")]
    [InverseProperty("Complaints")]
    public virtual ComplaintCategory ComplaintCate { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Complaints")]
    public virtual Employee Employee { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Complaints")]
    public virtual User User { get; set; }
}
