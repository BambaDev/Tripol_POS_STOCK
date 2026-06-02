using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Printer")]
[Index("BusinessLocationId", Name = "IX_Printer_BusinessLocationId")]
public partial class Printer
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; }

    [StringLength(250)]
    public string Type { get; set; }

    [StringLength(250)]
    public string CharactersPerLine { get; set; }

    [StringLength(250)]
    public string PrinterIpAddress { get; set; }

    public int? PrinterPort { get; set; }

    public int? BusinessLocationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BusinessLocationId")]
    [InverseProperty("Printers")]
    public virtual BusinessLocation BusinessLocation { get; set; }

    [InverseProperty("PrinterDocumentNavigation")]
    public virtual ICollection<Setting> SettingPrinterDocumentNavigations { get; set; } = new List<Setting>();

    [InverseProperty("PrinterRecieptNavigation")]
    public virtual ICollection<Setting> SettingPrinterRecieptNavigations { get; set; } = new List<Setting>();
}
