using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Setting")]
[Index("PrinterDocumentId", Name = "IX_Setting_PrinterDocumentId")]
[Index("PrinterRecieptId", Name = "IX_Setting_PrinterRecieptId")]
public partial class Setting
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string WhatsAppStatus { get; set; }

    [Column("accountSid")]
    [StringLength(150)]
    public string AccountSid { get; set; }

    [Column("authToken")]
    [StringLength(150)]
    public string AuthToken { get; set; }

    [StringLength(50)]
    public string FromPhoneNumber { get; set; }

    [StringLength(50)]
    public string WhatsAppMaintStatus { get; set; }

    [StringLength(50)]
    public string WhatsAppMaintInvoiceStatus { get; set; }

    [StringLength(50)]
    public string WhatsAppPhoneCode { get; set; }

    [StringLength(150)]
    public string MailMailer { get; set; }

    [StringLength(150)]
    public string MailStatus { get; set; }

    [StringLength(150)]
    public string MailAllowHtml { get; set; }

    [StringLength(150)]
    public string MailHost { get; set; }

    public int? MailPort { get; set; }

    [StringLength(150)]
    public string MailUsername { get; set; }

    [StringLength(150)]
    public string MailPassword { get; set; }

    [StringLength(150)]
    public string PurchaseCodeCondition { get; set; }

    [StringLength(250)]
    public string PurchaseCode { get; set; }

    [Column(TypeName = "image")]
    public byte[] Logo { get; set; }

    public bool? IsLockScreen { get; set; }

    [Column(TypeName = "image")]
    public byte[] LockScreenImg { get; set; }

    [StringLength(250)]
    public string Company { get; set; }

    [Column(TypeName = "text")]
    public string Description { get; set; }

    [Column(TypeName = "text")]
    public string Title { get; set; }

    [Column(TypeName = "text")]
    public string SubTitle { get; set; }

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

    public bool? IsSoundAdded { get; set; }

    public bool? IsSoundDeleted { get; set; }

    public bool? IsSoundSelected { get; set; }

    public bool? IsSoundDenied { get; set; }

    public bool? IsSoundWrong { get; set; }

    public bool? IsQuantityPopUp { get; set; }

    public bool? PosTopBanner { get; set; }

    public bool? PosBottomBanner { get; set; }

    public bool? PosCategories { get; set; }

    public bool? PosCategoriesWithoutImgs { get; set; }

    public bool? PosProductsWithoutImgs { get; set; }

    public bool? PoslayoutControlGroupLatestOrders { get; set; }

    public bool? PoslayoutControlGroupLatestCustomers { get; set; }

    public bool? PoslayoutControlGroupLatestSuppliers { get; set; }

    public bool? PoslayoutControlGroupSalesReturns { get; set; }

    public bool? PoslayoutControlGroupHold { get; set; }

    public bool? PoslayoutControlGroupUnpaidOrders { get; set; }

    [StringLength(250)]
    public string PrinterReciept { get; set; }

    public int? PrinterRecieptId { get; set; }

    [StringLength(250)]
    public string PrinterDocument { get; set; }

    public int? PrinterDocumentId { get; set; }

    [StringLength(250)]
    public string Lang { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PrinterDocumentId")]
    [InverseProperty("SettingPrinterDocumentNavigations")]
    public virtual Printer PrinterDocumentNavigation { get; set; }

    [ForeignKey("PrinterRecieptId")]
    [InverseProperty("SettingPrinterRecieptNavigations")]
    public virtual Printer PrinterRecieptNavigation { get; set; }
}
