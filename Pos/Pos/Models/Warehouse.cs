using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Warehouse")]
public partial class Warehouse
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string Phone { get; set; }

    [StringLength(250)]
    public string Email { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Warehouse")]
    public virtual ICollection<Adjustment> Adjustments { get; set; } = new List<Adjustment>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<RepairInvoice> RepairInvoices { get; set; } = new List<RepairInvoice>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<ReturnPurchase> ReturnPurchases { get; set; } = new List<ReturnPurchase>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<SaleHold> SaleHolds { get; set; } = new List<SaleHold>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("FromWarehouse")]
    public virtual ICollection<Transfer> TransferFromWarehouses { get; set; } = new List<Transfer>();

    [InverseProperty("ToWarehouse")]
    public virtual ICollection<Transfer> TransferToWarehouses { get; set; } = new List<Transfer>();
}
