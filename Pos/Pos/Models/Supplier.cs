using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Supplier")]
[Index("UserId", Name = "IX_Supplier_UserID")]
public partial class Supplier
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string FirstName { get; set; }

    [StringLength(250)]
    public string LastName { get; set; }

    [StringLength(250)]
    public string Email { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [StringLength(250)]
    public string Gender { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Supplier")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("Supplier")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [ForeignKey("UserId")]
    [InverseProperty("Suppliers")]
    public virtual User User { get; set; }
}
