using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("BusinessLocation")]
public partial class BusinessLocation
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string LocationId { get; set; }

    [StringLength(250)]
    public string Landmark { get; set; }

    [StringLength(250)]
    public string City { get; set; }

    [StringLength(250)]
    public string ZipCode { get; set; }

    [StringLength(250)]
    public string State { get; set; }

    [StringLength(250)]
    public string Country { get; set; }

    [StringLength(250)]
    public string Mobile { get; set; }

    [StringLength(250)]
    public string AlternateContactNumber { get; set; }

    [StringLength(250)]
    public string Email { get; set; }

    [StringLength(250)]
    public string Website { get; set; }

    [StringLength(250)]
    public string IsDefault { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<RegisterRecord> RegisterRecords { get; set; } = new List<RegisterRecord>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Register> Registers { get; set; } = new List<Register>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<RepairInvoicePayment> RepairInvoicePayments { get; set; } = new List<RepairInvoicePayment>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<RepairInvoice> RepairInvoices { get; set; } = new List<RepairInvoice>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<RepairPayment> RepairPayments { get; set; } = new List<RepairPayment>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<SaleHold> SaleHolds { get; set; } = new List<SaleHold>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<SalePayment> SalePayments { get; set; } = new List<SalePayment>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [InverseProperty("BusinessLocation")]
    public virtual ICollection<Waste> Wastes { get; set; } = new List<Waste>();
}
