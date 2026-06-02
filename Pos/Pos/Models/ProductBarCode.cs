using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("ProductBarCode")]
[Index("ProductId", Name = "IX_ProductBarCode_ProductId")]
public partial class ProductBarCode
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Value { get; set; }

    public int? ProductId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductBarCodes")]
    public virtual Product Product { get; set; }
}
