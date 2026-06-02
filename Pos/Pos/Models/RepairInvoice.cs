using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RepairInvoice")]
[Index("BrandId", Name = "IX_RepairInvoice_BrandId")]
[Index("BusinessLocationId", Name = "IX_RepairInvoice_BusinessLocationId")]
[Index("CustomerId", Name = "IX_RepairInvoice_CustomerId")]
[Index("DeviceId", Name = "IX_RepairInvoice_DeviceId")]
[Index("ModelId", Name = "IX_RepairInvoice_ModelId")]
[Index("TechnicalId", Name = "IX_RepairInvoice_TechnicalId")]
[Index("UserId", Name = "IX_RepairInvoice_UserId")]
[Index("WarehouseId", Name = "IX_RepairInvoice_WarehouseId")]
public partial class RepairInvoice
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string InvoiceType { get; set; }

    [StringLength(250)]
    public string ReferenceNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InvoiceDate { get; set; }

    [StringLength(250)]
    public string InvoiceStatus { get; set; }

    [StringLength(250)]
    public string DiscountType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "text")]
    public string AdditionalNotes { get; set; }

    [Column(TypeName = "text")]
    public string ShippingDetails { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AdditionalShippingCharges { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NetTotalAmount { get; set; }

    [StringLength(250)]
    public string PaymentSatus { get; set; }

    public int? NumberItems { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PaidAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalDiscount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Due { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ReturnAmount { get; set; }

    [Column(TypeName = "text")]
    public string Notes { get; set; }

    public int? InvoiceYear { get; set; }

    public int? InvoiceMonth { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RepairCompletedOn { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [StringLength(250)]
    public string PartsUsed { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SaleTotalAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SalePaidAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SaleDue { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RepairEstimatedCost { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RepairPaidAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? RepairDue { get; set; }

    [StringLength(250)]
    public string Status { get; set; }

    [Column(TypeName = "text")]
    public string Address { get; set; }

    [StringLength(250)]
    public string SerialNumber { get; set; }

    [StringLength(250)]
    public string PasswordPatternLock { get; set; }

    [Column(TypeName = "text")]
    public string CommentByTechnician { get; set; }

    [Column(TypeName = "text")]
    public string ProductConfiguration { get; set; }

    [Column(TypeName = "text")]
    public string ProblemReportedByTheCustomer { get; set; }

    [Column(TypeName = "text")]
    public string ConditionOfTheProduct { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateOfReceipt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpectedDateOfExamination { get; set; }

    [Column(TypeName = "text")]
    public string ExplainProblem { get; set; }

    [Column(TypeName = "image")]
    public byte[] Image { get; set; }

    [Column(TypeName = "text")]
    public string MaintenanceRepairChecklist { get; set; }

    public int? CustomerId { get; set; }

    public int? TechnicalId { get; set; }

    public int? BusinessLocationId { get; set; }

    public int? WarehouseId { get; set; }

    public int? BrandId { get; set; }

    public int? DeviceId { get; set; }

    public int? ModelId { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("RepairInvoices")]
    public virtual Brand Brand { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("RepairInvoices")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RepairInvoices")]
    public virtual Customer Customer { get; set; }

    [InverseProperty("RepairInvoice")]
    public virtual ICollection<RepairInvoiceDetail> RepairInvoiceDetails { get; set; } = new List<RepairInvoiceDetail>();

    [InverseProperty("RepairInvoice")]
    public virtual ICollection<RepairInvoicePayment> RepairInvoicePayments { get; set; } = new List<RepairInvoicePayment>();

    [InverseProperty("RepairInvoice")]
    public virtual ICollection<RepairPayment> RepairPayments { get; set; } = new List<RepairPayment>();

    [ForeignKey("UserId")]
    [InverseProperty("RepairInvoices")]
    public virtual User User { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("RepairInvoices")]
    public virtual Warehouse Warehouse { get; set; }
}
