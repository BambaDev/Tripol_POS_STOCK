using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Company")]
public partial class Company
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "image")]
    public byte[] Logo { get; set; }

    [Column("Company")]
    [StringLength(250)]
    public string Company1 { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [StringLength(150)]
    public string Email { get; set; }

    [StringLength(250)]
    public string Website { get; set; }

    [Column(TypeName = "text")]
    public string Address { get; set; }

    [StringLength(150)]
    public string Tel { get; set; }

    [StringLength(150)]
    public string Fax { get; set; }

    [StringLength(150)]
    public string Compte { get; set; }

    [StringLength(150)]
    public string Rib { get; set; }

    [StringLength(150)]
    public string Nis { get; set; }

    [StringLength(150)]
    public string Rc { get; set; }

    [StringLength(150)]
    public string Ai { get; set; }

    [StringLength(150)]
    public string IdFiscal { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Companies")]
    public virtual Customer Customer { get; set; }
}
