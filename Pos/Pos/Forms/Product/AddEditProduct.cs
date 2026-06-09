using DevExpress.Mvvm.POCO;
using DevExpress.Utils.About;
using DevExpress.XtraEditors;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraVerticalGrid;
using Pos.Forms.Alert;
using Pos.Forms.Brand;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Purchase;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using Pos.Forms.Overlay;
using Pos.Forms.Product.PriceGroup;
using System.IO.Packaging;
using DevExpress.XtraPrinting.BarCode;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Base;
using Pos.Forms.Sale;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Product
{
    public partial class AddEditProduct : DevExpress.XtraEditors.XtraForm
    {
        public Products products = null;
        public PosScreen pos = null;
        public string type = "Add";
        public int variation_id;
        public DataTable fields = new DataTable();
        public DataTable variationValues = new DataTable();
        public DataTable barcodes = new DataTable();
        public DataTable prices = new DataTable();
        public DataTable productsWarehouses = new DataTable();
        public Object obj = null;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }
        public void setProductNameAtSearch(string productName)
        {
            // Set the product name in the text field
            txtProduct.Text = productName;

            // Set focus to the product name input field
            txtProduct.Focus();

            // Move the caret (cursor) to the end of the text without selecting the content
            txtProduct.SelectionStart = txtProduct.Text.Length;
            txtProduct.SelectionLength = 0;  // Ensure nothing is selected
        }
        public AddEditProduct()
        {
            InitializeComponent();

            // style the grid view
            gridViewPrices.RowStyle += gridViewPrices_RowStyle;
            gridViewPrices.FocusedRowChanged += gridViewPrices_FocusedRowChanged;
            gridViewPrices.CustomDrawCell += gridViewPrices_CustomDrawCell;
            gridViewPrices.RowHeight = Function.Helper.RowHeight;
            gridViewPrices.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewProductsWarehouses.RowStyle += gridViewProductsWarehouses_RowStyle;
            gridViewProductsWarehouses.FocusedRowChanged += gridViewProductsWarehouses_FocusedRowChanged;
            gridViewProductsWarehouses.CustomDrawCell += gridViewProductsWarehouses_CustomDrawCell;
            gridViewProductsWarehouses.RowHeight = Function.Helper.RowHeight;
            gridViewProductsWarehouses.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPrices_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPrices.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPrices_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPrices.FocusedRowHandle && e.Column == gridViewPrices.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPrices_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewProductsWarehouses_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewProductsWarehouses.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewProductsWarehouses_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewProductsWarehouses.FocusedRowHandle && e.Column == gridViewProductsWarehouses.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewProductsWarehouses_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        public void setProductsObject(Products products)
        {
            this.products = products;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.products != null || currentItemId != 0)
            {
                this.currentItemId = this.products != null ? this.products.product_id : currentItemId;

                txtInitialQuantity.Enabled = false;

                Models.Product product = Shared.db.Products.Find(this.currentItemId);

                if (product != null)
                {
                    txtProduct.Text = product.ProductName;
                    txtSku.Text = product.Sku;
                    txtBarcode.Text = product.Barcode;
                    txtBarcodeType.EditValue = product.BarcodeType;
                    txtDescription.Text = product.ProductDescription;
                    txtSerialNumber.Text = product.SerialNumber;
                    //txtProductType.EditValue = product.ProductType;
                    txtAlertQuantity.Text = product.AlertQuantity.ToString();
                    txtInitialQuantity.Text = product.InitialQuantity.ToString();
                    txtImage.EditValue = product.Thumbnail;
                    txtPurchasePriceExcTax.Text = product.PurchasePriceExcTax.ToString();
                    txtPurchasePriceIncTax.Text = product.PurchasePriceIncTax.ToString();
                    txtPricePerUnit.Text = product.PricePerUnit.ToString();
                    txtXmargin.Text = product.Xmargin.ToString();
                    txtSellingPrice.Text = product.SellingPrice.ToString();
                    txtSellingPriceTaxType.EditValue = product.SellingPriceTaxType;
                    txtUnit.EditValue = product.UnitId;
                    txtBrand.EditValue = product.BrandId;
                    txtCategory.EditValue = product.CategoryId;
                    txtWarranty.EditValue = product.WarrantyId;
                    IsDivisible.EditValue = product.IsDivisible;
                    txtUnit.EditValue = product.UnitId;

                    this.getProductsWarehouses(product.Id);
                    this.getFieldsValues(product.Id);
                    this.getBarCodes(product.Id);
                    this.getPrices(product.Id);
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getFieldsValues(int ProductId)
        {
            barcodes = new DataTable();

            if (barcodes.Columns.Count == 0)
            {
                barcodes.Columns.Add("Id", typeof(int));
                barcodes.Columns.Add("Value", typeof(string));
                barcodes.Columns.Add("CreatedAt", typeof(DateTime));
            }

            gridControlFields.DataSource = Shared.db.ProductHasFields.Where(b => b.ProductId == ProductId).ToList();
        }

        public void getBarCodes(int ProductId)
        {
            barcodes = new DataTable();

            if (barcodes.Columns.Count == 0)
            {
                barcodes.Columns.Add("Id", typeof(int));
                barcodes.Columns.Add("Value", typeof(string));
                barcodes.Columns.Add("CreatedAt", typeof(DateTime));
            }

            gridControlBarCodes.DataSource = Shared.db.ProductBarCodes.Where(b => b.ProductId == ProductId).ToList();
        }

        public void getProductsWarehouses(int ProductId)
        {
            // Initialize the DataTable for products' warehouses
            DataTable productsWarehouses = new DataTable();

            // Define the columns in the DataTable
            if (productsWarehouses.Columns.Count == 0)
            {
                productsWarehouses.Columns.Add("Id", typeof(int));
                productsWarehouses.Columns.Add("Name", typeof(string));
                productsWarehouses.Columns.Add("Stock", typeof(int));  // Renamed for clarity
            }

            // Fetch the product warehouse data from the database along with product info
            var query = from pw in Shared.db.ProductWarehouses
                        join p in Shared.db.Products on pw.ProductId equals p.Id
                        where pw.ProductId == ProductId
                        group pw by new { pw.WarehouseId, pw.Warehouse.Name } into g
                        select new
                        {
                            WarehouseId = g.Key.WarehouseId,
                            WarehouseName = g.Key.Name,
                            TotalStock = g.Sum(x => x.Qty)  // Summing the stock quantities
                        };

            // Execute the query and populate the DataTable with the results
            foreach (var item in query.ToList())
            {
                DataRow row = productsWarehouses.NewRow();
                row["Id"] = item.WarehouseId;
                row["Name"] = item.WarehouseName;
                row["Stock"] = item.TotalStock;
                productsWarehouses.Rows.Add(row);
            }

            // Bind the DataTable to the grid control
            gridControlProductsWarehouses.DataSource = productsWarehouses;
        }

        public void getPrices(int ProductId)
        {
            prices = new DataTable();

            if (prices.Columns.Count == 0)
            {
                prices.Columns.Add("Id", typeof(int));
                prices.Columns.Add("PriceGroupId", typeof(int));
                prices.Columns.Add("GroupPrice", typeof(string));
                prices.Columns.Add("Price", typeof(decimal));
                prices.Columns.Add("CreatedAt", typeof(DateTime));
            }

            gridControlPrices.DataSource = Shared.db.ProductPrices.Where(b => b.ProductId == ProductId).ToList();
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            // ===== PHASE 3E: INPUT VALIDATION =====
            if (!Function.FormValidationHelper.ValidateProductForm(
                txtProduct,
                txtSku,
                txtDescription))
            {
                return; // Validation échouée
            }
            // ===== FIN VALIDATION =====

            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderProduct.Validate())
            {
                Models.Product product;

                System.Drawing.Image image = txtImage.Image;

                byte[] imageBytes;
                byte[] thumbnailBytes;

                using (var memoryStream = new MemoryStream())
                {
                    if (image != null)
                    {
                        // Determine the image format and save accordingly (JPEG or PNG)
                        if (ImageFormat.Png.Equals(image.RawFormat))
                        {
                            image.Save(memoryStream, ImageFormat.Png); // Save as PNG
                        }
                        else if (ImageFormat.Jpeg.Equals(image.RawFormat))
                        {
                            image.Save(memoryStream, ImageFormat.Jpeg); // Save as JPEG
                        }
                        else
                        {
                            // Default to PNG if unknown format
                            image.Save(memoryStream, ImageFormat.Png);
                        }

                        imageBytes = memoryStream.ToArray();

                        // Generate thumbnail bytes from the image
                        thumbnailBytes = Function.Helper.GetOptimizedImageBytes(image);
                    }
                    else
                    {
                        // If the image is null, use a default image (e.g., an embedded PNG or JPEG resource)
                        using (var defaultImageStream = new MemoryStream(Function.Helper.GetOptimizedImageBytes(Properties.Resources.box_package))) // Assuming box_package is your embedded image resource
                        {
                            using (System.Drawing.Image defaultImage = System.Drawing.Image.FromStream(defaultImageStream))
                            {
                                // Determine the format of the default image and save accordingly
                                if (ImageFormat.Png.Equals(defaultImage.RawFormat))
                                {
                                    defaultImage.Save(memoryStream, ImageFormat.Png); // Save as PNG
                                }
                                else if (ImageFormat.Jpeg.Equals(defaultImage.RawFormat))
                                {
                                    defaultImage.Save(memoryStream, ImageFormat.Jpeg); // Save as JPEG
                                }
                                else
                                {
                                    // Default to PNG if unknown format
                                    defaultImage.Save(memoryStream, ImageFormat.Png);
                                }

                                imageBytes = memoryStream.ToArray();

                                // Generate thumbnail bytes from the default image
                                thumbnailBytes = Function.Helper.GetOptimizedImageBytes(defaultImage);
                            }
                        }
                    }
                }

                if (this.type == "Add")
                {
                    using (var appDbContext = new AppDbContext())
                    {
                        product = new Models.Product();

                        product.ProductName = txtProduct.Text;
                        product.Sku = txtSku.Text;
                        product.Barcode = txtBarcode.Text;
                        product.BarcodeType = txtBarcodeType.Text;
                        product.ProductDescription = txtDescription.Text;
                        product.SerialNumber = txtSerialNumber.Text;
                        //product.ProductType = txtProductType.Text;
                        product.AlertQuantity = Int32.Parse(txtAlertQuantity.EditValue.ToString());
                        product.InitialQuantity = Int32.Parse(txtInitialQuantity.EditValue.ToString());
                        product.Image = imageBytes;
                        product.Thumbnail = thumbnailBytes;
                        product.PurchasePriceExcTax = decimal.Parse(txtPurchasePriceExcTax.Text);
                        product.PurchasePriceIncTax = decimal.Parse(txtPurchasePriceIncTax.Text);
                        product.PricePerUnit = decimal.Parse(txtPricePerUnit.Text);
                        product.Xmargin = decimal.Parse(txtXmargin.Text);
                        product.SellingPrice = decimal.Parse(txtSellingPrice.Text);
                        product.SellingPriceTaxType = txtSellingPriceTaxType.Text;
                        product.UnitId = txtUnit.EditValue != null && Int32.TryParse(txtUnit.EditValue.ToString(), out int unitId) ? unitId : (int?)null;
                        product.BrandId = txtBrand.EditValue != null && Int32.TryParse(txtBrand.EditValue.ToString(), out int brandId) ? brandId : (int?)null;
                        product.CategoryId = txtCategory.EditValue != null && Int32.TryParse(txtCategory.EditValue.ToString(), out int categoryId) ? categoryId : (int?)null;


                        if (txtWarranty.EditValue != null)
                            product.WarrantyId = Int32.Parse(txtWarranty.EditValue.ToString());

                        product.UserId = Properties.Settings.Default.userId;
                        product.CreatedAt = DateTime.Now;
                        product.UpdatedAt = DateTime.Now;

                        product.IsDivisible = IsDivisible.Checked;

                        appDbContext.Products.Add(product);

                        appDbContext.SaveChanges();
                    }

                    this.InitialQuantityProduct(product.Id, Convert.ToInt32(txtWarehouse.EditValue.ToString()), Convert.ToInt32(product.InitialQuantity), Convert.ToDecimal(product.SellingPrice));

                    using (var appDbContext = new AppDbContext())
                    {
                        List<ProductPrice> productPrices = new List<ProductPrice>();

                        for (int i = 0; prices.Rows.Count > i; i++)
                        {
                            ProductPrice productPrice = new ProductPrice();
                            productPrice.GroupPrice = this.prices.Rows[i]["GroupPrice"].ToString();
                            productPrice.PriceGroupId = int.Parse(this.prices.Rows[i]["PriceGroupId"].ToString());
                            productPrice.Price = Convert.ToDecimal(this.prices.Rows[i]["Price"]);
                            productPrice.ProductId = product.Id;
                            productPrice.CreatedAt = DateTime.Now;
                            productPrice.UpdatedAt = DateTime.Now;
                            productPrices.Add(productPrice);
                        }

                        appDbContext.ProductPrices.AddRange(productPrices);
                        appDbContext.SaveChanges();
                    }

                    using (var appDbContext = new AppDbContext())
                    {
                        List<ProductBarCode> productBarCodes = new List<ProductBarCode>();

                        for (int i = 0; barcodes.Rows.Count > i; i++)
                        {
                            ProductBarCode productBarCode = new ProductBarCode();
                            productBarCode.Value = this.barcodes.Rows[i]["Value"].ToString();
                            productBarCode.ProductId = product.Id;
                            productBarCode.CreatedAt = DateTime.Now;
                            productBarCode.UpdatedAt = DateTime.Now;
                            productBarCodes.Add(productBarCode);
                        }

                        appDbContext.ProductBarCodes.AddRange(productBarCodes);
                        appDbContext.SaveChanges();
                    }

                    using (var appDbContext = new AppDbContext())
                    {
                        List<ProductHasField> productHasFields = new List<ProductHasField>();

                        for (int i = 0; fields.Rows.Count > i; i++)
                        {
                            ProductHasField productBarCode = new ProductHasField();
                            productBarCode.Name = this.fields.Rows[i]["Name"].ToString();
                            productBarCode.Value = this.fields.Rows[i]["Value"].ToString();
                            productBarCode.ProductFieldId = Convert.ToInt16(this.fields.Rows[i]["FieldId"].ToString());
                            productBarCode.ProductId = product.Id;
                            productBarCode.CreatedAt = DateTime.Now;
                            productBarCode.UpdatedAt = DateTime.Now;
                            productHasFields.Add(productBarCode);
                        }

                        appDbContext.ProductHasFields.AddRange(productHasFields);
                        appDbContext.SaveChanges();
                    }

                    using (var appDbContext = new AppDbContext())
                    {
                        List<ProductVariantValue> productVariantValue = new List<ProductVariantValue>();

                        for (int i = 0; variationValues.Rows.Count > i; i++)
                        {
                            ProductVariantValue variantValue = new ProductVariantValue();
                            variantValue.Sku = this.variationValues.Rows[i]["Sku"].ToString();
                            variantValue.Value = this.variationValues.Rows[i]["Value"].ToString();
                            variantValue.PePriceExc = decimal.Parse(this.variationValues.Rows[i]["Pe Price Exc"].ToString());
                            variantValue.PePriceInc = decimal.Parse(this.variationValues.Rows[i]["Pe Price Inc"].ToString());
                            variantValue.SellingPrice = decimal.Parse(this.variationValues.Rows[i]["Selling Price"].ToString());
                            variantValue.ProductId = product.Id;
                            variantValue.VariationId = this.variation_id;
                            variantValue.CreatedAt = DateTime.Now;
                            variantValue.UpdatedAt = DateTime.Now;
                            productVariantValue.Add(variantValue);
                        }

                        appDbContext.ProductVariantValues.AddRange(productVariantValue);
                        appDbContext.SaveChanges();
                    }
                }
                else
                {
                    if (this.products != null)
                        this.currentItemId = this.products.product_id;

                    product = Shared.db.Products.Find(this.currentItemId);

                    if (product != null)
                    {
                        product.ProductName = txtProduct.Text;
                        product.Sku = txtSku.Text;
                        product.Barcode = txtBarcode.Text;
                        product.BarcodeType = txtBarcodeType.Text;
                        product.ProductDescription = txtDescription.Text;
                        product.SerialNumber = txtSerialNumber.Text;
                        //product.ProductType = txtProductType.Text;
                        product.AlertQuantity = Int32.Parse(txtAlertQuantity.EditValue.ToString());
                        product.InitialQuantity = Int32.Parse(txtInitialQuantity.EditValue.ToString());
                        product.Image = imageBytes;
                        product.Thumbnail = thumbnailBytes;
                        product.PurchasePriceExcTax = decimal.Parse(txtPurchasePriceExcTax.Text);
                        product.PurchasePriceIncTax = decimal.Parse(txtPurchasePriceIncTax.Text);
                        product.PricePerUnit = decimal.Parse(txtPricePerUnit.Text);
                        product.Xmargin = decimal.Parse(txtXmargin.Text);
                        product.SellingPrice = decimal.Parse(txtSellingPrice.Text);
                        product.SellingPriceTaxType = txtSellingPriceTaxType.Text;
                        product.UnitId = txtUnit.EditValue != null && Int32.TryParse(txtUnit.EditValue.ToString(), out int unitId) ? unitId : (int?)null;
                        product.BrandId = txtBrand.EditValue != null && Int32.TryParse(txtBrand.EditValue.ToString(), out int brandId) ? brandId : (int?)null;
                        product.CategoryId = txtCategory.EditValue != null && Int32.TryParse(txtCategory.EditValue.ToString(), out int categoryId) ? categoryId : (int?)null;


                        if (txtWarranty.EditValue != null)
                            product.WarrantyId = Int32.Parse(txtWarranty.EditValue.ToString());

                        product.UpdatedAt = DateTime.Now;

                        product.IsDivisible = IsDivisible.Checked;

                        Shared.db.Entry(product).State = EntityState.Modified;

                        using (var appDbContext = new AppDbContext())
                        {
                            // Handle Product Prices (Insert/Update)
                            List<ProductPrice> productPrices = new List<ProductPrice>();

                            for (int i = 0; prices.Rows.Count > i; i++)
                            {
                                int priceGroupId = int.Parse(this.prices.Rows[i]["PriceGroupId"].ToString());
                                ProductPrice productPrice = appDbContext.ProductPrices
                                    .FirstOrDefault(pp => pp.ProductId == product.Id && pp.PriceGroupId == priceGroupId);

                                if (productPrice == null) // If no price exists, create a new one
                                {
                                    productPrice = new ProductPrice
                                    {
                                        GroupPrice = this.prices.Rows[i]["GroupPrice"].ToString(),
                                        PriceGroupId = priceGroupId,
                                        Price = Convert.ToDecimal(this.prices.Rows[i]["Price"]),
                                        ProductId = product.Id,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    };
                                    appDbContext.ProductPrices.Add(productPrice);
                                }
                                else // If price exists, update it
                                {
                                    productPrice.GroupPrice = this.prices.Rows[i]["GroupPrice"].ToString();
                                    productPrice.Price = Convert.ToDecimal(this.prices.Rows[i]["Price"]);
                                    productPrice.UpdatedAt = DateTime.Now;
                                    appDbContext.Entry(productPrice).State = EntityState.Modified;
                                }
                            }

                            // Handle Product BarCodes (Insert/Update)
                            List<ProductBarCode> productBarCodes = new List<ProductBarCode>();

                            for (int i = 0; barcodes.Rows.Count > i; i++)
                            {
                                string barCodeValue = this.barcodes.Rows[i]["Value"].ToString();
                                ProductBarCode productBarCode = appDbContext.ProductBarCodes
                                    .FirstOrDefault(pb => pb.ProductId == product.Id && pb.Value == barCodeValue);

                                if (productBarCode == null) // If no barcode exists, create a new one
                                {
                                    productBarCode = new ProductBarCode
                                    {
                                        Value = barCodeValue,
                                        ProductId = product.Id,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    };
                                    appDbContext.ProductBarCodes.Add(productBarCode);
                                }
                                else // If barcode exists, update it
                                {
                                    productBarCode.Value = barCodeValue;
                                    productBarCode.UpdatedAt = DateTime.Now;
                                    appDbContext.Entry(productBarCode).State = EntityState.Modified;
                                }
                            }

                            // Handle Product Has Fields (Insert/Update)
                            List<ProductHasField> productHasFields = new List<ProductHasField>();

                            for (int i = 0; fields.Rows.Count > i; i++)
                            {
                                int fieldId = Convert.ToInt16(this.fields.Rows[i]["FieldId"].ToString());
                                ProductHasField productField = appDbContext.ProductHasFields
                                    .FirstOrDefault(pf => pf.ProductId == product.Id && pf.ProductFieldId == fieldId);

                                if (productField == null) // If no field exists, create a new one
                                {
                                    productField = new ProductHasField
                                    {
                                        Name = this.fields.Rows[i]["Name"].ToString(),
                                        Value = this.fields.Rows[i]["Value"].ToString(),
                                        ProductFieldId = fieldId,
                                        ProductId = product.Id,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    };
                                    appDbContext.ProductHasFields.Add(productField);
                                }
                                else // If field exists, update it
                                {
                                    productField.Name = this.fields.Rows[i]["Name"].ToString();
                                    productField.Value = this.fields.Rows[i]["Value"].ToString();
                                    productField.UpdatedAt = DateTime.Now;
                                    appDbContext.Entry(productField).State = EntityState.Modified;
                                }
                            }

                            appDbContext.SaveChanges(); // Save all changes at once
                        }

                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();


                if (txtSuppFirstName.Text.Length > 0 && txtSupLastName.Text.Length > 0)
                {
                    using (var context = new AppDbContext())
                    {
                        Models.Purchase purchase = new Models.Purchase();

                        Models.Supplier supplier = new Models.Supplier();

                        supplier.FirstName = txtSuppFirstName.Text;
                        supplier.LastName = txtSupLastName.Text;
                        supplier.CreatedAt = DateTime.Now;
                        supplier.UpdatedAt = DateTime.Now;

                        context.Suppliers.Add(supplier);

                        context.SaveChanges();

                        decimal due = 0;
                        decimal returnAmount = 0;

                        purchase.PurchaseDate = DateTime.Now;
                        purchase.ReferenceNo = getPurchaseReferenceNo();
                        //purchase.DiscountType = txtDiscountType.Text;
                        //purchase.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                        //purchase.PurchaseStatus = txtPurchaseStatus.Text;
                        //purchase.PaymentSatus = txtPaymentStatus.Text;
                        ////purchase.AdditionalNotes = txtAdditionalNotes.Text;
                        ////purchase.ShippingDetails = txtShippingDetails.Text;
                        ////purchase.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                        //purchase.NetTotalAmount = this.calculeTotal();
                        purchase.PaidAmount = decimal.Parse(txtPurchasePriceExcTax.EditValue.ToString()) * decimal.Parse(txtInitialQuantity.Text);
                        purchase.TotalTax = 0;
                        purchase.TotalDiscount = 0;
                        purchase.Due = due;
                        purchase.ReturnAmount = returnAmount;
                        purchase.NumberItems = 1;
                        purchase.BusinessLocationId = Properties.Settings.Default.BusinessLocationId;
                        purchase.SupplierId = supplier.Id;
                        purchase.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        purchase.UserId = Properties.Settings.Default.userId;
                        purchase.PurchaseMonth = DateTime.Now.Month;
                        purchase.PurchaseYear = DateTime.Now.Year;
                        purchase.CreatedAt = DateTime.Now;
                        purchase.UpdatedAt = DateTime.Now;

                        context.Purchases.Add(purchase);

                        context.SaveChanges();

                        List<PurchaseDetail> purchaseDetail = new List<PurchaseDetail>();

                        PurchaseDetail PurchaseDetailList = new PurchaseDetail();
                        PurchaseDetailList.ProductId = product.Id;
                        PurchaseDetailList.PurchaseId = purchase.Id;
                        PurchaseDetailList.ProductName = product.ProductName;
                        PurchaseDetailList.PurchaseQuantity = int.Parse(txtInitialQuantity.Text);
                        //PurchaseDetailList.UnitCostBd = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBd"]);
                        PurchaseDetailList.DiscountPercent = 0;
                        PurchaseDetailList.UnitCostBt = decimal.Parse(txtPurchasePriceExcTax.EditValue.ToString());
                        PurchaseDetailList.LineTotal = decimal.Parse(txtPurchasePriceExcTax.EditValue.ToString()) * decimal.Parse(txtInitialQuantity.Text);
                        PurchaseDetailList.ProfitMargin = decimal.Parse(txtSellingPrice.EditValue.ToString()) - decimal.Parse(txtPurchasePriceExcTax.EditValue.ToString());
                        PurchaseDetailList.UnitSellingPrice = decimal.Parse(txtSellingPrice.EditValue.ToString());
                        PurchaseDetailList.CreatedAt = DateTime.Now;
                        PurchaseDetailList.UpdatedAt = DateTime.Now;

                        purchaseDetail.Add(PurchaseDetailList);

                        context.PurchaseDetails.AddRange(purchaseDetail);
                        context.SaveChanges();
                    }
                }

                Function.Sound.Added();

                if (this.obj != null)
                {
                    if (this.obj is AddEditPurchase)
                    {
                        AddEditPurchase addEditPurchase = (AddEditPurchase)this.obj;
                        addEditPurchase.setProductNameAtSearch(txtProduct.Text, product.Id);
                    }
                    else if (this.obj is PosScreen)
                    {
                        PosScreen pos = (PosScreen)this.obj;
                        pos.getProducts();
                    }
                }

                if (this.products != null)
                    this.products.loadProducts();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public string getPurchaseReferenceNo()
        {
            int lastId = Shared.db.Purchases.Count() + 1;
            return Function.Helper.generateRefNo("PU", lastId);
        }

        private void AddEditProduct_Load(object sender, EventArgs e)
        {
            txtSku.Text = Function.Helper.generateSku();
            txtBarcodeType.EditValue = "EAN-13";
            txtSellingPriceTaxType.EditValue = "Exclusive";

            if (this.type != "Add")
            {
                txtSku.ReadOnly = true;
                this.edit();
            }
            else
            {
                this.loadTablePrices();
                // Set the barcode control to display '123456789' when creating a new product
                barCodeControl.Text = "123456789";  // Set this to your default barcode
            }

            if (fields.Columns.Count == 0)
            {
                fields.Columns.Add("Id", typeof(int));
                fields.Columns.Add("Name", typeof(string));
                fields.Columns.Add("Value", typeof(string));
                fields.Columns.Add("FieldId", typeof(string));
                fields.Columns.Add("CreatedAt", typeof(DateTime));
            }

            if (variationValues.Columns.Count == 0)
            {
                variationValues.Columns.Add("Id", typeof(int));
                variationValues.Columns.Add("Sku", typeof(string));
                variationValues.Columns.Add("Value", typeof(string));
                variationValues.Columns.Add("Pe Price Exc", typeof(decimal));
                variationValues.Columns.Add("Pe Price Inc", typeof(decimal));
                variationValues.Columns.Add("Selling Price", typeof(decimal));
            }

            if (barcodes.Columns.Count == 0)
            {
                barcodes.Columns.Add("Id", typeof(int));
                barcodes.Columns.Add("Value", typeof(string));
                barcodes.Columns.Add("CreatedAt", typeof(DateTime));
                barcodes.Columns.Add("Barcode", typeof(string));
            }

            this.getWarehouses();
            this.getFields();
            this.getBrands();
            this.getWarranties();
            this.getCategories();
            this.getUnits();

            //txtVariations.Properties.DataSource = Shared.db.Variations.ToList();
            //txtVariations.Properties.DisplayMember = "Name"; // Set display member
            //txtVariations.Properties.ValueMember = "Id"; // Set value member

            repLookUpEditValue.DataSource = Shared.db.ProductFieldValues
                          .Select(p => new
                          {
                              p.Id,
                              p.Value
                          })
                          .ToList();
            repLookUpEditValue.DisplayMember = "Value";
            repLookUpEditValue.ValueMember = "Id";

            txtProduct.Select();
            txtProduct.Focus();
        }

        public void getWarehouses()
        {
            var warehouses = Shared.db.Warehouses.ToList();

            txtWarehouse.Properties.DataSource = warehouses;
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            if (warehouses.Any())
            {
                txtWarehouse.EditValue = warehouses.First().Id;
            }
        }

        public void getWarranties()
        {
            txtWarranty.Properties.DataSource = Shared.db.Warranties.ToList();
            txtWarranty.Properties.DisplayMember = "Name"; // Set display member
            txtWarranty.Properties.ValueMember = "Id"; // Set value member
        }

        public void getFields()
        {
            txtFields.Properties.DataSource = Shared.db.ProductFields.ToList();
            txtFields.Properties.DisplayMember = "Name"; // Set display member
            txtFields.Properties.ValueMember = "Id"; // Set value member
        }

        public void getBrands()
        {
            txtBrand.Properties.DataSource = Shared.db.Brands.ToList();
            txtBrand.Properties.DisplayMember = "Name"; // Set display member
            txtBrand.Properties.ValueMember = "Id"; // Set value member
        }

        public void getCategories()
        {
            txtCategory.Properties.DataSource = Shared.db.Categories.ToList();
            txtCategory.Properties.DisplayMember = "Name"; // Set display member
            txtCategory.Properties.ValueMember = "Id"; // Set value member
        }
        public void getUnits()
        {
            var units = Shared.db.Units.ToList();  // Retrieve all units
            txtUnit.Properties.DataSource = units;
            txtUnit.Properties.DisplayMember = "Name";  // Set display member
            txtUnit.Properties.ValueMember = "Id";  // Set value member

            // Find the unit with name "Piece"
            var pieceUnit = units.FirstOrDefault(u => u.Name == "Piece");

            // If "Piece" exists, set it as the default selected value
            if (pieceUnit != null)
            {
                txtUnit.EditValue = pieceUnit.Id;  // Set the default selected value to "Piece"
            }
        }

        private void btnAddVariationTable_Click(object sender, EventArgs e)
        {
            DataRow NewRow = variationValues.NewRow();

            NewRow["Id"] = variationValues.Rows.Count + 1;
            NewRow["Sku"] = Function.Helper.generateSku();
            NewRow["Value"] = "Not Set";
            NewRow["Pe Price Exc"] = 0;
            NewRow["Pe Price Inc"] = 0;
            NewRow["Selling Price"] = 0;

            variationValues.Rows.Add(NewRow);

            Function.Sound.Added();
        }

        private void txtQuickBusinessLocation_Click(object sender, EventArgs e)
        {
            BusinessLocation.AddEditBusinessLocation addEditBusinessLocation = new BusinessLocation.AddEditBusinessLocation();
            addEditBusinessLocation.setObject(this);
            addEditBusinessLocation.ShowDialog();
        }

        public void SetPos(PosScreen pos)
        {
            this.pos = pos;
        }

        public void selectWarranty(int id)
        {
            this.getWarranties();
            txtWarranty.EditValue = id;
        }

        public void selectUnit(int id)
        {
            this.getUnits();
            txtUnit.EditValue = id;
        }

        public void selectCategory(int id)
        {
            this.getCategories();
            txtCategory.EditValue = id;
        }

        public void selectBrands(int id)
        {
            this.getBrands();
            txtBrand.EditValue = id;
        }

        private void txtQuickUnit_Click(object sender, EventArgs e)
        {
            Unit.AddEditUnit addEditUnit = new Unit.AddEditUnit();
            addEditUnit.Enabled = true;
            addEditUnit.setObject(this);
            addEditUnit.ShowDialog();
        }

        private void txtQuickBrand_Click(object sender, EventArgs e)
        {
            Brand.AddEditBrand addEditBrand = new Brand.AddEditBrand();
            addEditBrand.setObject(this);
            addEditBrand.ShowDialog();
        }

        private void txtQuickCategory_Click(object sender, EventArgs e)
        {
            Category.AddEditCategory addEditCategory = new Category.AddEditCategory();
            addEditCategory.setObject(this);
            addEditCategory.ShowDialog();
        }

        private Models.Product GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Products.Find(currentItemId);
            else
                return null;
        }

        private void DisplayCurrentItem()
        {
            Sound.Selected();

            if (currentItemId == 0)
            {
                // If no current selection, disable all navigation buttons.
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnStart.Enabled = false;
                btnEnd.Enabled = false;
                return;
            }

            // Retrieve the minimum and maximum ID values from the Brands dataset.
            int minId = Shared.db.Products.Min(b => b.Id);
            int maxId = Shared.db.Products.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Product currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtProduct.Text = currentItem.ProductName;
                txtSku.Text = currentItem.Sku;
                txtBarcode.Text = currentItem.Barcode;
                txtBarcodeType.EditValue = currentItem.BarcodeType;
                txtDescription.Text = currentItem.ProductDescription;
                txtSerialNumber.Text = currentItem.SerialNumber;
                //txtProductType.EditValue = currentItem.ProductType;
                txtAlertQuantity.Text = currentItem.AlertQuantity.ToString();
                txtInitialQuantity.Text = currentItem.InitialQuantity.ToString();
                txtImage.EditValue = currentItem.Image;
                txtPurchasePriceExcTax.Text = currentItem.PurchasePriceExcTax.ToString();
                txtPurchasePriceIncTax.Text = currentItem.PurchasePriceIncTax.ToString();
                txtXmargin.Text = currentItem.Xmargin.ToString();
                txtSellingPrice.Text = currentItem.SellingPrice.ToString();
                txtSellingPriceTaxType.EditValue = currentItem.SellingPriceTaxType;
                txtUnit.EditValue = currentItem.UnitId;
                txtBrand.EditValue = currentItem.BrandId;
                txtCategory.EditValue = currentItem.CategoryId;
                txtWarranty.EditValue = currentItem.WarrantyId;

                this.getProductsWarehouses(currentItem.Id);
                this.getFieldsValues(currentItem.Id);
                this.getBarCodes(currentItem.Id);
                this.getPrices(currentItem.Id);

                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Products.Min(b => (int?)b.Id);
                if (minId.HasValue)
                {
                    currentItemId = minId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the first item: " + ex.Message);
            }
        }

        private void MoveToLast()
        {
            try
            {
                int? maxId = Shared.db.Products.Max(b => (int?)b.Id);
                if (maxId.HasValue)
                {
                    currentItemId = maxId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the last item: " + ex.Message);
            }
        }

        private void MoveToNext()
        {
            if (currentItemId == 0)
            {
                this.MoveToFirst();
            }

            var nextItem = Shared.db.Products.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
            if (nextItem != null)
            {
                currentItemId = nextItem.Id;
                DisplayCurrentItem();
            }
        }

        private void MoveToPrevious()
        {
            if (currentItemId != 0)
            {
                var prevItem = Shared.db.Products.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                if (prevItem != null)
                {
                    currentItemId = prevItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";

            txtInitialQuantity.Enabled = true;

            //txtProduct.Text = product.ProductName;
            //txtSku.Text = product.Sku;
            //txtBarcode.Text = product.Barcode;
            //txtBarcodeType.EditValue = product.BarcodeType;
            //txtDescription.Text = product.ProductDescription;
            //txtSerialNumber.Text = product.SerialNumber;
            //txtProductType.EditValue = product.ProductType;
            //txtAlertQuantity.Text = product.AlertQuantity.ToString();
            //txtImage.EditValue = product.Image;
            //txtPurchasePriceExcTax.Text = product.PurchasePriceExcTax.ToString();
            //txtPurchasePriceIncTax.Text = product.PurchasePriceIncTax.ToString();
            //txtXmargin.Text = product.Xmargin.ToString();
            //txtSellingPrice.Text = product.SellingPrice.ToString();
            //txtSellingPriceTaxType.EditValue = product.SellingPriceTaxType;
            //txtUnit.EditValue = product.UnitId;
            //txtBrand.EditValue = product.BrandId;
            //txtCategory.EditValue = product.CategoryId;
            //txtBusinessLocation.EditValue = product.BusinessLocationId;
            //txtDevice.EditValue = product.DeviceId;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.obj != null)
            {
                Sound.Selected();
                this.Close();
            }
        }

        public void InitialQuantityProduct(int productId, int warehouseId, int quantity, decimal price)
        {
            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    // If the product exists in the warehouse, increase the quantity
                    productInWarehouse.Qty += quantity;
                    productInWarehouse.UpdatedAt = DateTime.Now;
                }
                else
                {
                    // If the product does not exist, create a new record
                    context.ProductWarehouses.Add(new ProductWarehouse
                    {
                        ProductId = productId,
                        WarehouseId = warehouseId,
                        Qty = quantity,
                        Price = price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        // Initialize other necessary fields...
                    });
                }

                context.SaveChanges();
            }
        }

        private void txtInitialQuantity_EditValueChanged(object sender, EventArgs e)
        {
            int quantity;

            if (int.TryParse(txtInitialQuantity.Text, out quantity))
            {
                if (quantity >= 0)
                {
                    // Handle valid non-negative quantity
                }
                else
                {
                    txtInitialQuantity.EditValue = 1;
                    XtraMessageBox.Show("Please enter a non-negative number");
                }
            }
            else
            {
                XtraMessageBox.Show("Please enter a valid number");
            }
        }

        private void btnAddBarCode_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "")
            {
                XtraMessageBox.Show("Please enter a barCode.");
                return;
            }

            // Check if the price already exists
            if (DoesBarCodeExist(txtBarcode.Text))
            {
                XtraMessageBox.Show("This BarCode already exists.");
                return;
            }

            DataRow row = barcodes.NewRow();
            row["Id"] = barcodes.Rows.Count + 1; // Assume this method computes the next ID
            row["Value"] = txtBarcode.Text; // Assume txtBarcodeValue is a TextBox for user input
            row["CreatedAt"] = DateTime.Now;
            barcodes.Rows.Add(row);
            barCodeControl.Text = txtBarcode.Text;
            gridControlBarCodes.DataSource = barcodes;

            // Clear the barcode input field
            txtBarcode.Text = string.Empty;

            // Refocus on the txtBarcode field
            txtBarcode.Focus();
        }

        private bool DoesBarCodeExist(string value)
        {
            return barcodes.AsEnumerable().Any(row => row.Field<string>("Value") == value);
        }

        private void loadTablePrices()
        {
            if (prices.Columns.Count == 0)
            {
                prices.Columns.Add("Id", typeof(int));
                prices.Columns.Add("PriceGroupId", typeof(int));
                prices.Columns.Add("GroupPrice", typeof(string));
                prices.Columns.Add("Price", typeof(decimal));
                prices.Columns.Add("CreatedAt", typeof(DateTime));
            }

            var priceGroups = Shared.db.PriceGroups.ToList();

            foreach (var item in priceGroups)
            {
                DataRow row = prices.NewRow();
                row["Id"] = prices.Rows.Count + 1; // Similar ID computation
                row["PriceGroupId"] = item.Id; // User input for price
                row["GroupPrice"] = item.Name; // User input for price
                row["Price"] = 0; // User input for price
                row["CreatedAt"] = DateTime.Now;
                prices.Rows.Add(row);
            }

            gridControlPrices.DataSource = prices;
        }

        private bool DoesPriceExist(decimal priceValue)
        {
            return prices.AsEnumerable().Any(row => row.Field<decimal>("Price") == priceValue);
        }

        private void txtFields_EditValueChanged(object sender, EventArgs e)
        {
            // Check if the text box is not empty
            if (string.IsNullOrEmpty(txtFields.Text))
                return;  // Do nothing if the text box is empty

            // Convert DataTable to Enumerable to check if the entered name already exists
            if (fields.AsEnumerable().Any(row => row["Name"].ToString().Equals(txtFields.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("This field name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Exit the method if a duplicate is found
            }

            // Generate a new unique ID based on the maximum ID currently in the DataTable
            int newId = fields.Rows.Count > 0 ? fields.AsEnumerable().Max(r => Convert.ToInt32(r["Id"])) + 1 : 1;

            // Create a new DataRow and fill it with values
            DataRow newRow = fields.NewRow();
            newRow["Id"] = newId;  // Set the unique ID
            newRow["Name"] = txtFields.Text;  // Set the name from the text box
            newRow["Value"] = "";  // Initialize with empty string or default value
            newRow["FieldId"] = Convert.ToInt16(txtFields.EditValue.ToString());  // Initialize with empty string or default value
            newRow["CreatedAt"] = DateTime.Now;  // Set the creation date and time

            // Add the new row to the DataTable
            fields.Rows.Add(newRow);

            // Update the DataSource of the grid to reflect the new data
            gridControlFields.DataSource = fields;
        }

        private void btnLoadAllFields_Click(object sender, EventArgs e)
        {
            var fields = Shared.db.ProductFields.ToList();

            gridControlFields.DataSource = fields;
        }

        private void repoDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridViewFields.FocusedRowHandle;
                if (rowHandle < 0) return;

                object fieldId = gridViewFields.GetRowCellValue(rowHandle, "Id");
                if (fieldId == null || !int.TryParse(fieldId.ToString(), out int id))
                {
                    MessageBox.Show("Invalid item selected.", "Error");
                    return;
                }

                if (XtraMessageBox.Show("Are you sure want to delete this item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (type == "Add")
                    {
                        //if (fields.Rows.Count == 1)
                        //{
                        //    XtraMessageBox.Show("Can't remove this item.");
                        //    return;
                        //}

                        DataRow row = fields.Rows[rowHandle];
                        if (row != null)
                        {
                            row.Delete();
                            fields.AcceptChanges();
                            gridControlFields.RefreshDataSource();
                        }
                    }
                    else
                    {
                        DeleteItemFromDatabase(id);
                    }

                    Function.Sound.Deleted();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the item: " + ex.Message, "Error");
            }
        }

        private void DeleteItemFromDatabase(int itemId)
        {
            // Your database deletion logic here
            using (var context = new AppDbContext())
            {
                var item = context.ProductHasFields.Find(itemId);
                if (item != null)
                {
                    context.ProductHasFields.Remove(item);
                    context.SaveChanges();
                }
            }
        }

        private void repoBrCodeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridViewBarCodes.FocusedRowHandle;
                if (rowHandle < 0) return;

                object fieldId = gridViewBarCodes.GetRowCellValue(rowHandle, "Id");
                if (fieldId == null || !int.TryParse(fieldId.ToString(), out int id))
                {
                    MessageBox.Show("Invalid item selected.", "Error");
                    return;
                }

                if (XtraMessageBox.Show("Are you sure want to delete this item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (type == "Add")
                    {
                        //if (barcodes.Rows.Count == 1)
                        //{
                        //    XtraMessageBox.Show("Can't remove this item.");
                        //    return;
                        //}

                        DataRow row = barcodes.Rows[rowHandle];
                        if (row != null)
                        {
                            row.Delete();
                            barcodes.AcceptChanges();
                            gridControlBarCodes.RefreshDataSource();
                        }
                    }
                    else
                    {
                        // Your database deletion logic here
                        using (var context = new AppDbContext())
                        {
                            var item = context.ProductBarCodes.Find(id);
                            if (item != null)
                            {
                                context.ProductBarCodes.Remove(item);
                                context.SaveChanges();
                            }
                        }
                    }

                    Function.Sound.Deleted();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the item: " + ex.Message, "Error");
            }
        }

        private void repoPriceDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gridViewPrices.FocusedRowHandle;
                if (rowHandle < 0) return;

                object fieldId = gridViewPrices.GetRowCellValue(rowHandle, "Id");
                if (fieldId == null || !int.TryParse(fieldId.ToString(), out int id))
                {
                    MessageBox.Show("Invalid item selected.", "Error");
                    return;
                }

                if (XtraMessageBox.Show("Are you sure want to delete this item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (type == "Add")
                    {
                        //if (prices.Rows.Count == 1)
                        //{
                        //    XtraMessageBox.Show("Can't remove this item.");
                        //    return;
                        //}

                        DataRow row = prices.Rows[rowHandle];
                        if (row != null)
                        {
                            row.Delete();
                            prices.AcceptChanges();
                            gridControlPrices.RefreshDataSource();
                        }
                    }
                    else
                    {
                        // Your database deletion logic here
                        using (var context = new AppDbContext())
                        {
                            var item = context.ProductPrices.Find(id);
                            if (item != null)
                            {
                                context.ProductPrices.Remove(item);
                                context.SaveChanges();
                            }
                        }
                    }

                    Function.Sound.Deleted();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete the item: " + ex.Message, "Error");
            }
        }

        private void btnAddWarranty_Click(object sender, EventArgs e)
        {
            Warranty.AddEditWarranty addEditWarranty = new Warranty.AddEditWarranty();
            addEditWarranty.setObject(this);
            addEditWarranty.ShowDialog();
        }

        private void UpdateTotalStock()
        {
            if (decimal.TryParse(txtUnitPerBox.EditValue.ToString(), out decimal unitPerBox) &&
                decimal.TryParse(txtNumberOfBoxes.EditValue.ToString(), out decimal numberOfBoxes))
            {
                decimal totalStock = unitPerBox * numberOfBoxes;
                txtQuantityStock.EditValue = totalStock;
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values.");
            }
        }

        private void txtUnitPerBox_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUnitPerBox.EditValue != null && txtNumberOfBoxes.EditValue != null)
            {
                UpdateTotalStock();
            }
        }

        private void txtNumberOfBoxes_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNumberOfBoxes.EditValue != null && txtUnitPerBox.EditValue != null)
            {
                UpdateTotalStock();
            }
        }

        private void txtQuantityStock_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtQuantityStock.EditValue.ToString(), out decimal currentStock))
            {
                UpdateTotalStock();
            }
            else
            {
                MessageBox.Show("Please enter a valid stock quantity.");
                txtQuantityStock.EditValue = 0; // Reset to a default value
            }
        }

        private void btnLockScreen_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Auth.LockScreen lockScreen = new Auth.LockScreen();
            lockScreen.FormClosed += (s, args) => overlay.Close();
            lockScreen.Show();
            lockScreen.TopMost = true;
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            XtraMessageBox.Show("Refresh All");
        }

        private void btnCloseRegister_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Close Register"))
            {
                int currentBusinessLocationId = Properties.Settings.Default.BusinessLocation;

                if (Function.Helper.IsRegisterOpen(currentBusinessLocationId))
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.CloseRegister closeRegister = new Alert.CloseRegister();

                    // This ensures the overlay form is displayed behind the modal form but above the parent form
                    closeRegister.FormClosed += (s, args) => overlay.Close();

                    closeRegister.Show();
                    closeRegister.TopMost = true;
                }
                else
                {
                    XtraMessageBox.Show("No open register record found for the specified business location.");
                }
            }
        }

        private void btnPriceGroup_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            AddEditPriceGroup addEditPriceGroup = new AddEditPriceGroup();
            addEditPriceGroup.ShowDialog();
        }

        private void gridViewBarCodes_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                // Get the currently selected row's data
                int rowHandle = e.RowHandle; // Get the row handle from the event arguments

                if (rowHandle >= 0) // Ensure the row is valid
                {
                    // Retrieve barcode value and type from the row
                    var barCodeValueObj = gridViewBarCodes.GetRowCellValue(rowHandle, "Value"); // Replace "Value" with your actual column name for barcode value

                    if (barCodeValueObj != null)
                    {
                        string barCodeValue = barCodeValueObj.ToString(); // Convert the object to string
                        string barCodeType = txtBarcodeType.Text; // Convert the barcode type object to string

                        // Set the barcode value in the barCodeControl
                        barCodeControl.Text = barCodeValue;

                        // Dynamically adjust the size of the control based on barcode length
                        int barcodeLength = barCodeValue.Length;
                        barCodeControl.Width = barcodeLength * 15; // Example scaling factor, adjust as needed
                        barCodeControl.Height = 100; // Adjust height if necessary

                        // Set the barcode type (symbology)
                        switch (barCodeType)
                        {
                            case "C128":
                                barCodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code128Generator(); // Code 128
                                break;
                            case "C39":
                                barCodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.Code39Generator(); // Code 39
                                break;
                            case "EAN-13":
                                barCodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN13Generator(); // EAN-13
                                break;
                            case "EAN-8":
                                barCodeControl.Symbology = new DevExpress.XtraPrinting.BarCode.EAN8Generator(); // EAN-8
                                break;
                            default:
                                MessageBox.Show("Unknown barcode type.");
                                break;
                        }

                        // Ensure the control is visible
                        barCodeControl.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Barcode value or type not found for the selected row.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid row selected.");
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message for debugging
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void RepositoryItemBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the clicked row's barcode value (assuming it's in a grid or control)
                string barCodeValue = "123456789012"; // Replace with the actual barcode value you want to print

                // Specify barcode type (for example, Code 128)
                var barcodeGenerator = new Code128Generator();

                // Create a new PrintDocument
                PrintDocument printDocument = new PrintDocument();

                // Assign event handler for printing
                printDocument.PrintPage += (s, ev) =>
                {
                    // Set barcode size and positioning
                    int barcodeWidth = 300;
                    int barcodeHeight = 100;

                    // Create barcode image
                    Bitmap barcodeBitmap = GenerateBarcodeImage(barCodeValue, barcodeGenerator, barcodeWidth, barcodeHeight);

                    // Draw the barcode image on the print page
                    ev.Graphics.DrawImage(barcodeBitmap, new Point(100, 100)); // Adjust the positioning (100, 100) as needed
                };

                // Show PrintDialog to choose printer
                System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Print the document
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        // Method to generate a barcode image using DevExpress barcode generator
        private Bitmap GenerateBarcodeImage(string barCodeValue, BarCodeGeneratorBase barcodeGenerator, int width, int height)
        {
            // Create a BarCodeControl to generate the barcode
            using (var barCodeControl = new DevExpress.XtraEditors.BarCodeControl())
            {
                barCodeControl.Text = barCodeValue;
                barCodeControl.Symbology = barcodeGenerator;
                barCodeControl.AutoModule = true;
                barCodeControl.Size = new Size(width, height);

                // Draw the barcode into a bitmap
                Bitmap bitmap = new Bitmap(width, height);
                barCodeControl.DrawToBitmap(bitmap, new Rectangle(0, 0, width, height));

                return bitmap;
            }
        }

        private void txtUPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Prevent the default behavior of the Tab key
                e.SuppressKeyPress = true;

                // Set focus to txtBarcode
                txtBarcode.Focus();
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab && txtUPC.Focused)
            {
                // Set focus to txtBarcode and prevent default tab behavior
                txtBarcode.Focus();
                return true;  // Return true to indicate the key press has been handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtXmargin_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPurchasePriceExcTax.Text, out decimal costPrice) && decimal.TryParse(txtXmargin.Text, out decimal margin))
            {
                // Calculate the selling price based on margin
                decimal sellingPrice = costPrice + (costPrice * margin / 100);
                txtSellingPrice.EditValue = sellingPrice;
            }
        }


        private void txtSellingPrice_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPurchasePriceExcTax.Text, out decimal costPrice) && decimal.TryParse(txtSellingPrice.Text, out decimal sellingPrice))
            {
                // Calculate the margin based on the selling price
                decimal margin = ((sellingPrice - costPrice) / costPrice) * 100;
                txtXmargin.EditValue = margin;
            }
        }

        private void txtImage_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;

                        // SÉCURITÉ: Validation complète de l'image
                        var validation = Function.FileUploadValidator.ValidateImage(filePath);

                        if (!validation.isValid)
                        {
                            XtraMessageBox.Show(
                                $"Invalid image file:\n\n{validation.errorMessage}\n\nPlease select a valid image (JPG, PNG, BMP).",
                                "Upload Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return;
                        }

                        // Charger l'image validée
                        txtImage.Image = System.Drawing.Image.FromFile(filePath);

                        // Log upload (pour audit)
                        System.Diagnostics.Debug.WriteLine(
                            $"Image uploaded: {validation.sanitizedFileName} ({validation.fileSizeKB} KB) by User {Properties.Settings.Default.userId}"
                        );
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Image upload error: {ex.Message}");
                        XtraMessageBox.Show(
                            "Failed to load image. Please ensure it's a valid image file.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        //private void txtSellingPrice_Enter(object sender, EventArgs e)
        //{
        //    // Disable the EditValueChanged event for txtXmargin while editing the selling price
        //    txtXmargin.EditValueChanged -= txtXmargin_EditValueChanged;
        //}
        //private void txtSellingPrice_Leave(object sender, EventArgs e)
        //{
        //    // Re-enable the EditValueChanged event for txtXmargin after leaving the selling price field
        //    txtXmargin.EditValueChanged += txtXmargin_EditValueChanged;
        //}

    }
}