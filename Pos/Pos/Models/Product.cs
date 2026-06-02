using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Product")]
[Index("BrandId", Name = "IX_Product_BrandId")]
[Index("CategoryId", Name = "IX_Product_CategoryId")]
[Index("DeviceId", Name = "IX_Product_DeviceId")]
[Index("UnitId", Name = "IX_Product_UnitId")]
[Index("UserId", Name = "IX_Product_UserId")]
[Index("WarrantyId", Name = "IX_Product_WarrantyId")]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ProductName { get; set; }

    [StringLength(250)]
    public string Sku { get; set; }

    [StringLength(250)]
    public string Barcode { get; set; }

    [StringLength(250)]
    public string BarcodeType { get; set; }

    [Column(TypeName = "text")]
    public string ProductDescription { get; set; }

    public int? UnitId { get; set; }

    public int? BrandId { get; set; }

    public int? CategoryId { get; set; }

    public int? DeviceId { get; set; }

    public int? WarrantyId { get; set; }

    public int? UserId { get; set; }

    public bool? IsDefault { get; set; }

    [StringLength(250)]
    public string SerialNumber { get; set; }

    [StringLength(250)]
    public string ProductType { get; set; }

    public int? InitialQuantity { get; set; }

    public int? AlertQuantity { get; set; }

    public byte[] Image { get; set; }

    public byte[] Thumbnail { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PurchasePriceExcTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PurchasePriceIncTax { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Xmargin { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? SellingPrice { get; set; }

    [StringLength(250)]
    public string SellingPriceTaxType { get; set; }

    [StringLength(250)]
    public string Upc { get; set; }

    [StringLength(250)]
    public string Isbn { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? UnitPerBox { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NumberOfBox { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PricePerUnit { get; set; }

    public bool IsDivisible { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand Brand { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductAdjustment> ProductAdjustments { get; set; } = new List<ProductAdjustment>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductBarCode> ProductBarCodes { get; set; } = new List<ProductBarCode>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductHasField> ProductHasFields { get; set; } = new List<ProductHasField>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductPurchaseReturn> ProductPurchaseReturns { get; set; } = new List<ProductPurchaseReturn>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductReturn> ProductReturns { get; set; } = new List<ProductReturn>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductTransfer> ProductTransfers { get; set; } = new List<ProductTransfer>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVariantValue> ProductVariantValues { get; set; } = new List<ProductVariantValue>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    [InverseProperty("Product")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("Product")]
    public virtual ICollection<RepairInvoiceDetail> RepairInvoiceDetails { get; set; } = new List<RepairInvoiceDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<SaleDetailHold> SaleDetailHolds { get; set; } = new List<SaleDetailHold>();

    [InverseProperty("Product")]
    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    [ForeignKey("UnitId")]
    [InverseProperty("Products")]
    public virtual Unit Unit { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Products")]
    public virtual User User { get; set; }

    [ForeignKey("WarrantyId")]
    [InverseProperty("Products")]
    public virtual Warranty Warranty { get; set; }

    [InverseProperty("Waste")]
    public virtual ICollection<WasteItem> WasteItems { get; set; } = new List<WasteItem>();
}
