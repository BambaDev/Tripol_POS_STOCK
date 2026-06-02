using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Customer")]
[Index("PriceGroup", Name = "IX_Customer_PriceGroup")]
[Index("TierLevelId", Name = "IX_Customer_TierLevelId")]
[Index("UserId", Name = "IX_Customer_UserID")]
public partial class Customer
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string FirstName { get; set; }

    [StringLength(250)]
    public string LastName { get; set; }

    [StringLength(250)]
    public string FullName { get; set; }

    [StringLength(250)]
    public string Email { get; set; }

    [StringLength(250)]
    public string Phone { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [StringLength(250)]
    public string Gender { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column(TypeName = "text")]
    public string Address { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalPointsAccumulated { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrentPoints { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrentDue { get; set; }

    [StringLength(250)]
    public string Code { get; set; }

    public int? TierLevelId { get; set; }

    public int? PriceGroup { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<LoyaltyCard> LoyaltyCards { get; set; } = new List<LoyaltyCard>();

    [InverseProperty("Customer")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [ForeignKey("PriceGroup")]
    [InverseProperty("Customers")]
    public virtual PriceGroup PriceGroupNavigation { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Customer")]
    public virtual ICollection<Redemption> Redemptions { get; set; } = new List<Redemption>();

    [InverseProperty("Customer")]
    public virtual ICollection<RepairInvoicePayment> RepairInvoicePayments { get; set; } = new List<RepairInvoicePayment>();

    [InverseProperty("Customer")]
    public virtual ICollection<RepairInvoice> RepairInvoices { get; set; } = new List<RepairInvoice>();

    [InverseProperty("Customer")]
    public virtual ICollection<RepairPayment> RepairPayments { get; set; } = new List<RepairPayment>();

    [InverseProperty("Customer")]
    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    [InverseProperty("Customer")]
    public virtual ICollection<RewardHistory> RewardHistories { get; set; } = new List<RewardHistory>();

    [InverseProperty("Customer")]
    public virtual ICollection<SaleHold> SaleHolds { get; set; } = new List<SaleHold>();

    [InverseProperty("Customer")]
    public virtual ICollection<SalePayment> SalePayments { get; set; } = new List<SalePayment>();

    [InverseProperty("Customer")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [ForeignKey("TierLevelId")]
    [InverseProperty("Customers")]
    public virtual CustomerTier TierLevel { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    [ForeignKey("UserId")]
    [InverseProperty("Customers")]
    public virtual User User { get; set; }
}
