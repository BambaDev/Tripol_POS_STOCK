using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Promotion")]
[Index("ProductId", Name = "IX_Promotion_ProductId")]
[Index("UserId", Name = "IX_Promotion_UserId")]
public partial class Promotion
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [StringLength(250)]
    public string Type { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Qty { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Discount { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [StringLength(250)]
    public string PromotionCode { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Promotions")]
    public virtual Product Product { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Promotions")]
    public virtual User User { get; set; }
}
