using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using Pos.Report;
using Pos.Forms.Overlay;
using Pos.Forms.Report;
using Pos.Report.Sales;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Pos.Forms.Supplier;
using System.Runtime.InteropServices;
using System.Windows;
using System.Drawing.Drawing2D;
using Pos.Function.Styling;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Sale
{
    public partial class Sales : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int sale_id = 0;

        public Sales()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewSales.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewSales.RowStyle += gridViewSales_RowStyle;
            gridViewSales.FocusedRowChanged += gridViewSales_FocusedRowChanged;
            gridViewSales.CustomDrawCell += gridViewSales_CustomDrawCell;
            gridViewSales.RowHeight = Function.Helper.RowHeight;
            gridViewSales.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewSales_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewSales.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewSales_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewSales.FocusedRowHandle && e.Column == gridViewSales.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewSales_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        private void InitializeContextMenu()
        {
            string lang = Properties.Settings.Default.Lang;

            string msgMenuItemEdit;
            string msgMenuItemAdd;
            string msgMenuItemDelete;

            if (lang == "en")
            {
                msgMenuItemEdit = "Edit";
                msgMenuItemAdd = "Add";
                msgMenuItemDelete = "Delete";
            }
            else if (lang == "fr")
            {
                msgMenuItemEdit = "Modifier";
                msgMenuItemAdd = "Ajouter";
                msgMenuItemDelete = "Supprimer";
            }
            else
            {
                msgMenuItemEdit = "تحرير";
                msgMenuItemAdd = "أضف";
                msgMenuItemDelete = "حذف";
            }

            contextMenu = new ContextMenuStrip();
            menuItemEdit = new ToolStripMenuItem(msgMenuItemEdit);
            menuItemAdd = new ToolStripMenuItem(msgMenuItemAdd);
            menuItemDelete = new ToolStripMenuItem(msgMenuItemDelete);

            menuItemEdit.Image = Properties.Resources.rightclick_suitcase;
            menuItemAdd.Image = Properties.Resources.rightclick_case_study;
            menuItemDelete.Image = Properties.Resources.rightclick_remove;

            contextMenu.Items.AddRange(new ToolStripItem[] { menuItemEdit, menuItemAdd, menuItemDelete });

            menuItemEdit.Click += MenuItemEdit_Click;
            menuItemAdd.Click += MenuItemAdd_Click;
            menuItemDelete.Click += MenuItemDelete_Click;
        }

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridViewSales.RowCount > 0 && gridViewSales.FocusedRowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

                    if (hitInfo.InRow)
                    {
                        view.FocusedRowHandle = hitInfo.RowHandle;
                        contextMenu.Show(view.GridControl, e.Location);
                    }

                }

            }
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            saleEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Sale"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
                Sound.Selected();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            saleDelete();
        }

        public void BorderStyle()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang != "ar")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            }
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                this.ApplyCustomFont();

                // Set the form to use RTL
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            simpleButton1.Font = customFont;
            btnRefreshItems.Font = customFont;
            btnDeleteItem.Font = customFont;
            btnPrintItems.Font = customFont;
            btnEditItem.Font = customFont;
            btnAddItem.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            //layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;
            layoutControlItem10.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewSales.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private Color InterpolateColor(Color startColor, Color endColor, float progress)
        {
            int r = (int)(startColor.R + (endColor.R - startColor.R) * progress);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * progress);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * progress);
            return Color.FromArgb(r, g, b);
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            //// In your form constructor or initialization code
            //ButtonStyleHelper helper = new ButtonStyleHelper();

            //// Configure and apply the effect to btnAddItem with hover border color
            //ButtonEffectConfig addItemConfig = new ButtonEffectConfig
            //{
            //    Button = btnAddItem,
            //    DefaultBackColor = Color.White,
            //    HoverBackColor = Color.Red,
            //    DefaultTextColor = Color.Red,
            //    HoverTextColor = Color.White,
            //    DefaultBorderColor = Color.Red,  // Default border color (before hover)
            //    HoverBorderColor = Color.Transparent,  // Border color when hovered
            //    EasingSpeed = 0.03f,             // Custom easing speed
            //    BorderRadius = 5,
            //    EnableBorderRadius = true,       // Enable rounded corners
            //    BorderWidth = 5
            //};
            //helper.ApplyEasingEffectToButton(addItemConfig);

            //// Configure and apply the effect to btnEditItem with hover border color
            //ButtonEffectConfig editItemConfig = new ButtonEffectConfig
            //{
            //    Button = btnEditItem,
            //    DefaultBackColor = Color.White,
            //    HoverBackColor = Color.Blue,
            //    DefaultTextColor = Color.Black,
            //    HoverTextColor = Color.White,
            //    DefaultBorderColor = Color.Black,  // Default border color (before hover)
            //    HoverBorderColor = Color.White,    // Border color when hovered
            //    EasingSpeed = 0.05f,               // Custom easing speed
            //    BorderRadius = 10,
            //    EnableBorderRadius = true,         // Enable rounded corners
            //    BorderWidth = 2
            //};
            //helper.ApplyEasingEffectToButton(editItemConfig);


            btnSaleEdit.Enabled = false;
            btnSaleDelete.Enabled = false;

            this.DisplayTotalSales();
            this.DisplayTotalRevenue();
            this.DisplayTopSellingProducts();
            this.DisplaySalesByRegion();
            this.DisplaySalesByDateRange();

            this.loadSales();

            txtSaleTotal.Text = this.getTotalAmount() + " DA";

            txtBusinessLocation.Properties.DataSource = Shared.db.BusinessLocations.ToList();
            txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
            txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtCustomer.Properties.DataSource = Shared.db.Customers.ToList();
            txtCustomer.Properties.DisplayMember = "FirstName"; // Set display member
            txtCustomer.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadSales()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Sales
                //.Include(w => w.WarehouseId)
                //.Include(l => l.BusinessLocation)
                //.Include(s => s.SupplierId)
                .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Customer.FullName.Contains(searchTerm))
                .Count();
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // Load the first page of data
                BindDataToGrid(currentPage);
            }
        }

        private void BindDataToGrid(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * itemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.Sales.Include(c => c.Customer)
                //.Include(w => w.WarehouseId)
                //.Include(l => l.BusinessLocation)
                //.Include(s => s.SupplierId)
                    .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Customer.FullName.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlSales.DataSource = currentPageData;
                UpdatePageLabel();
                UpdateNavigationButtons();
            }
        }

        private void UpdatePageLabel()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            currentPageLabel.Text = $"{page} {currentPage} {of} {totalPages}";
        }

        private void UpdateNavigationButtons()
        {
            NavPrevPage.Enabled = currentPage > 1;
            NavFirstPage.Enabled = currentPage > 1;
            NavNextPage.Enabled = currentPage < totalPages;
            NavLastPage.Enabled = currentPage < totalPages;
        }

        // Function to display total sales count on button text
        private void DisplayTotalSales()
        {
            string lang = Properties.Settings.Default.Lang;

            string totalSalesText;

            if (lang == "en")
            {
                totalSalesText = "Total Sales";
            }
            else if (lang == "fr")
            {
                totalSalesText = "Total des ventes";
            }
            else
            {
                totalSalesText = "إجمالي المبيعات";
            }

            using (var context = new AppDbContext())
            {
                var totalSales = context.Sales.Count();
                btnTotalSales.Text = $"{totalSalesText}: {totalSales}";
            }
        }

        // Function to display total revenue on button text
        private void DisplayTotalRevenue()
        {
            string lang = Properties.Settings.Default.Lang;

            string totalRevenueText;

            if (lang == "en")
            {
                totalRevenueText = "Total Revenue";
            }
            else if (lang == "fr")
            {
                totalRevenueText = "Revenu total";
            }
            else
            {
                totalRevenueText = "إجمالي الإيرادات";
            }

            using (var context = new AppDbContext())
            {
                var totalRevenue = context.Sales.Sum(s => s.NetTotalAmount ?? 0);
                btnTotalRevenue.Text = $"{totalRevenueText}: {totalRevenue:C}";
            }
        }

        // Function to display top-selling products on button text
        private void DisplayTopSellingProducts()
        {
            string lang = Properties.Settings.Default.Lang;

            string topProductText;
            string noTopProductsText;

            if (lang == "en")
            {
                topProductText = "Top Product";
                noTopProductsText = "No Top Products";
            }
            else if (lang == "fr")
            {
                topProductText = "Meilleur produit";
                noTopProductsText = "Pas de meilleurs produits";
            }
            else
            {
                topProductText = "المنتج الأكثر مبيعًا";
                noTopProductsText = "لا توجد منتجات رائدة";
            }

            using (var context = new AppDbContext())
            {
                var topProducts = context.SaleDetails
                    .GroupBy(sd => sd.ProductId)
                    .OrderByDescending(g => g.Sum(sd => sd.SaleQuantity))
                    .Take(5)
                    .Select(g => new { ProductId = g.Key, TotalQuantity = g.Sum(sd => sd.SaleQuantity) })
                    .ToList();

                // Display only the top product or format for button text
                if (topProducts.Count > 0)
                {
                    var topProduct = topProducts.First();
                    btnTopSellingProducts.Text = $"{topProductText}: {topProduct.ProductId} ({topProduct.TotalQuantity} sold)";
                }
                else
                {
                    btnTopSellingProducts.Text = noTopProductsText;
                }
            }
        }

        // Function to display sales by region on button text
        private void DisplaySalesByRegion()
        {
            string lang = Properties.Settings.Default.Lang;

            string regionsText;
            string topRegionText;

            if (lang == "en")
            {
                regionsText = "Regions";
                topRegionText = "Top Region";
            }
            else if (lang == "fr")
            {
                regionsText = "Régions";
                topRegionText = "Région supérieure";
            }
            else
            {
                regionsText = "المناطق";
                topRegionText = "المنطقة الأولى";
            }

            using (var context = new AppDbContext())
            {
                var salesByRegion = context.Sales
                    .GroupBy(s => s.BusinessLocationId)
                    .Select(g => new { RegionId = g.Key, TotalSales = g.Count() })
                    .ToList();

                // Display a summary or the total for all regions
                var totalRegions = salesByRegion.Count;
                btnSalesByRegion.Text = $"{regionsText} : {totalRegions}";

                // Optionally display the region with the most sales
                var topRegion = salesByRegion.OrderByDescending(r => r.TotalSales).FirstOrDefault();
                if (topRegion != null)
                {
                    btnSalesByRegion.Text = $"{topRegionText} : {topRegion.RegionId} ({topRegion.TotalSales} sales)";
                }
            }
        }

        // Function to display sales count by a specified date range on button text
        private void DisplaySalesByDateRange()
        {
            string lang = Properties.Settings.Default.Lang;

            string salesText;

            if (lang == "en")
            {
                salesText = "Sales";
            }
            else if (lang == "fr")
            {
                salesText = "Ventes";
            }
            else
            {
                salesText = "المبيعات";
            }

            using (var context = new AppDbContext())
            {
                // Get the current year
                int currentYear = DateTime.Now.Year;

                // Set the start date to the beginning of the current year
                DateTime startDate = new DateTime(currentYear, 1, 1);

                // Set the end date to the end of the current year
                DateTime endDate = new DateTime(currentYear, 12, 31);

                // Query sales within the current year
                var salesByDateRange = context.Sales
                    .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                    .Count();

                // Display the result on the button
                btnSalesByDateRange.Text = $"{salesText} ({currentYear}) : {salesByDateRange}";
            }
        }

        private void btnAddSale_ItemClick(object sender, ItemClickEventArgs e)
        {
            PosScreen sale = new PosScreen();
            sale.setSalesObject(this);
            sale.setTypeOperation("Add");
            sale.ShowDialog();
        }

        private void btnSaleEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.saleEdit();
        }

        private void btnSaleDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.saleDelete();
        }

        private void gridViewSales_DoubleClick(object sender, EventArgs e)
        {
            btnSaleEdit.Enabled = true;
            btnSaleDelete.Enabled = true;

            this.sale_id = int.Parse(gridViewSales.GetRowCellValue(gridViewSales.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.saleEdit();
        }

        private void repEditSale_Click(object sender, EventArgs e)
        {
            btnSaleEdit.Enabled = true;
            btnSaleDelete.Enabled = true;

            this.sale_id = int.Parse(gridViewSales.GetRowCellValue(gridViewSales.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.saleEdit();
        }

        private void repDeleteSale_Click(object sender, EventArgs e)
        {
            this.saleDelete();
        }

        private void gridViewSales_RowClick(object sender, EventArgs e)
        {
            btnSaleEdit.Enabled = true;
            btnSaleDelete.Enabled = true;

            this.sale_id = int.Parse(gridViewSales.GetRowCellValue(gridViewSales.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void saleEdit()
        {
            //OverlayForm overlay = new OverlayForm(this);
            //overlay.Show();
            //using (AppDbContext AppDb = new AppDbContext())
            //{
            //    Models.Sale sale1 = AppDb.Sales.Include(a => a.SaleDetails).SingleOrDefault(x => x.Id == this.sale_id);
            //    Forms.Pos.Pos pos = new Forms.Pos.Pos(sale1);
            //    pos.Show();
            //}

            PosScreen sale = new PosScreen();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            //sale.FormClosed += (s, args) => overlay.Close();

            sale.setSalesObject(this);
            sale.setTypeOperation("Edit");
            sale.ShowDialog();
            //sale.TopMost = true;
        }

        public void saleDelete()
        {
            btnSaleEdit.Enabled = false;
            btnSaleDelete.Enabled = false;

            using (AppDbContext AppDb = new AppDbContext())
            {
                // Récupérer l'ID de la vente à partir de la sélection utilisateur
                int sale_id = int.Parse(gridViewSales.GetRowCellValue(gridViewSales.FocusedRowHandle, "Id").ToString());

                // Récupérer l'entité Sale
                Models.Sale sale = AppDb.Sales.Find(sale_id);

                if (sale != null && XtraMessageBox.Show("Are you sure you want to delete this sale?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Récupérer tous les SaleDetails associés à cette Sale
                    var saleDetails = AppDb.SaleDetails.Where(sd => sd.SaleId == sale_id).ToList();

                    // Supprimer tous les SaleDetails associés

                    foreach (var saleDetail in saleDetails)
                    {
                        Models.ProductWarehouse productWarehouse = AppDb.ProductWarehouses.FirstOrDefault(x => x.WarehouseId == sale.WarehouseId && x.ProductId == saleDetail.ProductId);
                        productWarehouse.Qty += saleDetail.SaleQuantity;
                        AppDb.ProductWarehouses.Update(productWarehouse);
                        AppDb.SaleDetails.Remove(saleDetail);
                        AppDb.SaveChanges();
                    }


                    // Récupérer tous les SalePayments associés à cette Sale
                    var salePayments = AppDb.SalePayments.Where(sp => sp.SaleId == sale_id).ToList();

                    // Supprimer tous les SalePayments associés
                    if (salePayments.Any())
                    {
                        AppDb.SalePayments.RemoveRange(salePayments);
                    }

                    // Supprimer la vente elle-même
                    Models.Customer customer = AppDb.Customers.SingleOrDefault(x => x.Id == sale.CustomerId);
                    customer.CurrentDue = customer.CurrentDue - sale.Due;
                    AppDb.Customers.Update(customer);
                    AppDb.Sales.Remove(sale);

                    // Sauvegarder les changements
                    AppDb.SaveChanges();

                    // Recharger les ventes après la suppression
                    this.loadSales();
                    Function.Sound.Deleted();
                }
                else
                {
                    Function.Sound.Wrong();
                }
            }
        }

        private void btnSaleRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnSaleEdit.Enabled = false;
            btnSaleDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadSales();
        }

        private void repositoryItemPrint_Click(object sender, EventArgs e)
        {
            if (gridViewSales.FocusedRowHandle >= 0)
            {
                if (gridViewSales.FocusedRowHandle >= 0)
                {
                    int sale_id = int.Parse(gridViewSales.GetRowCellValue(gridViewSales.FocusedRowHandle, "Id").ToString());

                    // Initialize the Salesx80mm report with the new report ID or Sale ID
                    this.printSalesX80mm(sale_id); // Adjust parameter as required
                    Sound.Selected();
                }
            }
        }

        public void printSalesX80mm(int saleId)
        {
            if (saleId != 0)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterReciept;

                // Initialize the TechnicalRepiarA4 report with the maintenance_id
                Salesx80mm report80mm = new Salesx80mm(saleId);
                report80mm.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"Printer \"{printerName}\" is not valid.", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(report80mm);

                // Set the printer name in the PrintTool
                printTool.PrinterSettings.PrinterName = printerName;

                try
                {
                    // Print the document using the specified printer
                    printTool.Print(printerName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"An error occurred while printing: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Display the report using a form with a DocumentViewer
                FormView reportViewerFrm = new FormView(report80mm);
                reportViewerFrm.Show();
            }
            else
            {
                XtraMessageBox.Show("You can't print without validated data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSaleStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSaleStatus.EditValue != null)
            {
                var sale = Shared.db.Sales.Where(w => w.SaleStatus == txtSaleStatus.Text).Include(c => c.Customer).ToList();
                gridControlSales.DataSource = sale;
                txtSaleTotal.Text = getFilterTotalAmount(sale).ToString();
            }

            Sound.Selected();
        }

        public decimal getFilterTotalAmount(List<Models.Sale> sale)
        {
            decimal totalAmount = (decimal)sale.Sum(p => p.NetTotalAmount);

            return totalAmount;

        }
        private void txtSupplier_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCustomer.EditValue != null)
            {
                var sale = Shared.db.Sales.Where(w => w.CustomerId == int.Parse(txtCustomer.EditValue.ToString())).Include(c => c.Customer).ToList();
                gridControlSales.DataSource = sale;
                txtSaleTotal.Text = getFilterTotalAmount(sale).ToString();
            }

            Sound.Selected();
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                var sale = Shared.db.Sales.Where(w => w.WarehouseId == int.Parse(txtWarehouse.EditValue.ToString())).Include(c => c.Customer).ToList();
                gridControlSales.DataSource = sale;
                txtSaleTotal.Text = getFilterTotalAmount(sale).ToString();
            }

            Sound.Selected();
        }

        private void txtBusinessLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBusinessLocation.EditValue != null)
            {
                var sale = Shared.db.Sales.Where(w => w.BusinessLocationId == int.Parse(txtBusinessLocation.EditValue.ToString())).Include(c => c.Customer).ToList();
                gridControlSales.DataSource = sale;
                txtSaleTotal.Text = getFilterTotalAmount(sale).ToString();
            }

            Sound.Selected();
        }

        private void txtPaymentStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPaymentStatus.EditValue != null)
            {
                gridControlSales.DataSource = Shared.db.Sales.Where(w => w.PaymentSatus == txtPaymentStatus.Text).Include(c => c.Customer).ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlSales.DataSource = Shared.db.Sales.OrderByDescending(p => p.Id).Include(c => c.Customer).ToList();
                }
                else
                {
                    gridControlSales.DataSource = Shared.db.Sales.Include(c => c.Customer).ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickPurchase_Click(object sender, EventArgs e)
        {
            PosScreen addEditSale = new PosScreen();
            addEditSale.ShowDialog();
            Sound.Selected();
        }

        //public decimal getFilterTotalAmount()
        //{
        //    DateTime today = DateTime.Today;

        //    var totalAmount = Shared.db.Sales
        //        //.Where(p => p.SaleDate == today)
        //        .Sum(p => p.NetTotalAmount);

        //    return decimal.Parse(totalAmount.ToString());
        //}
        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Sales
                //.Where(p => p.SaleDate == today)
                .Sum(p => p.NetTotalAmount);

            return decimal.Parse(totalAmount.ToString());
        }

        private void btnSaleHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sale.SaleHistory sale = new Sale.SaleHistory();
            sale.ShowDialog();
            Sound.Selected();
        }

        private void btnFilterRefresh_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtPaymentStatus.Text = string.Empty;
            txtBusinessLocation.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtSaleStatus.Text = string.Empty;
            this.loadSales();
            Sound.Added();
        }

        private void btnPrintSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlSales.ShowPrintPreview();
        }

        private void btnHistories_Click(object sender, EventArgs e)
        {
            Sale.SaleHistory sale = new Sale.SaleHistory();
            sale.ShowDialog();
            Sound.Selected();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.setSalesObject(this);
                pos.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.saleEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.saleDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlSales.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnSaleEdit.Enabled = false;
            btnSaleDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadSales();
        }

        private void NavPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                BindDataToGrid(currentPage);
            }
        }

        private void NavFirstPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                BindDataToGrid(currentPage);
            }
        }

        private void NavLastPage_Click(object sender, EventArgs e)
        {
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                BindDataToGrid(currentPage);
            }
        }

        private void NavNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                BindDataToGrid(currentPage);
            }
        }

        private void perPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.Sales.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadSales();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSales, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSales, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSales, "xlsx");
        }

        private void formExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}