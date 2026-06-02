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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Pos.Forms.Supplier;
using System.Runtime.InteropServices;

namespace Pos.Forms.Purchase
{
    public partial class Purchases : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int purchase_id = 0;

        public Purchases()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewPurchases.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewPurchases.RowStyle += gridViewPurchases_RowStyle;
            gridViewPurchases.FocusedRowChanged += gridViewPurchases_FocusedRowChanged;
            gridViewPurchases.CustomDrawCell += gridViewPurchases_CustomDrawCell;
            gridViewPurchases.RowHeight = Function.Helper.RowHeight;
            gridViewPurchases.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPurchases_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPurchases.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPurchases_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPurchases.FocusedRowHandle && e.Column == gridViewPurchases.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPurchases_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewPurchases.RowCount > 0 && gridViewPurchases.FocusedRowHandle >= 0)
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
            purchaseEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Purchase"))
            {
                AddEditPurchase purchase = new AddEditPurchase();
                purchase.setPurchasesObject(this);
                purchase.setTypeOperation("Add");
                purchase.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            purchaseDelete();
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
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
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
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
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

            // Apply custom font to Purchases columns
            foreach (GridColumn column in gridViewPurchases.Columns)
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


        private void Purchases_Load(object sender, EventArgs e)
        {
            btnPurchaseEdit.Enabled = false;
            btnPurchaseDelete.Enabled = false;

            this.loadPurchases();

            this.DisplayTotalPurchases();
            this.DisplayTotalRevenue();
            this.DisplayTopPurchasesProducts();
            this.DisplayPurchasesByRegion();
            this.DisplayPurchasesByDateRange();

            txtPurchaseTotal.Text = this.getTotalAmount() + " DA";

            txtBusinessLocation.Properties.DataSource = Shared.db.BusinessLocations.ToList();
            txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
            txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtSupplier.Properties.DataSource = Shared.db.Suppliers.ToList();
            txtSupplier.Properties.DisplayMember = "FirstName"; // Set display member
            txtSupplier.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadPurchases()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Purchases
                //.Include(w => w.WarehouseId)
                //.Include(l => l.BusinessLocation)
                //.Include(s => s.SupplierId)
                .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Supplier.FirstName.Contains(searchTerm) ||
                        p.Supplier.LastName.Contains(searchTerm))
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
                var currentPageData = context.Purchases.Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                    //.Include(w => w.WarehouseId)
                    //.Include(l => l.BusinessLocation)
                    //.Include(s => s.SupplierId)
                    .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Supplier.FirstName.Contains(searchTerm) ||
                        p.Supplier.LastName.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlPurchases.DataSource = currentPageData;
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

        // Function to display total Purchases count on button text
        private void DisplayTotalPurchases()
        {
            string purchasesText;
            string lang = Properties.Settings.Default.Lang;

            if (lang == "en")
            {
                purchasesText = "Total Purchases";
            }
            else if (lang == "fr")
            {
                purchasesText = "Total des achats";
            }
            else
            {
                purchasesText = "إجمالي المشتريات";
            }

            using (var context = new AppDbContext())
            {
                var totalPurchases = context.Purchases.Count();
                btnTotalPurchases.Text = $"{purchasesText} : {totalPurchases}";
            }
        }

        // Function to display total revenue on button text
        private void DisplayTotalRevenue()
        {
            string revenueText;
            string lang = Properties.Settings.Default.Lang;

            if (lang == "en")
            {
                revenueText = "Total Revenue";
            }
            else if (lang == "fr")
            {
                revenueText = "Revenu total";
            }
            else
            {
                revenueText = "إجمالي الإيرادات";
            }

            using (var context = new AppDbContext())
            {
                var totalRevenue = context.Purchases.Sum(s => s.NetTotalAmount ?? 0);
                btnTotalRevenue.Text = $"{revenueText} : {totalRevenue:C}";
            }
        }

        // Function to display top-Purchases products on button text
        private void DisplayTopPurchasesProducts()
        {
            string topProductText;
            string noTopProductsText;
            string lang = Properties.Settings.Default.Lang;

            if (lang == "en")
            {
                topProductText = "Top Product";
                noTopProductsText = "No Top Products";
            }
            else if (lang == "fr")
            {
                topProductText = "Produit le plus acheté";
                noTopProductsText = "Pas de meilleurs produits";
            }
            else
            {
                topProductText = "المنتج الأكثر شراءً";
                noTopProductsText = "لا توجد منتجات رائدة";
            }

            using (var context = new AppDbContext())
            {
                var topProducts = context.PurchaseDetails
                    .GroupBy(sd => sd.ProductId)
                    .OrderByDescending(g => g.Sum(sd => sd.PurchaseQuantity))
                    .Take(5)
                    .Select(g => new { ProductId = g.Key, TotalQuantity = g.Sum(sd => sd.PurchaseQuantity) })
                    .ToList();

                // Display only the top product or format for button text
                if (topProducts.Count > 0)
                {
                    var topProduct = topProducts.First();
                    btnTopPurchasesProducts.Text = $"{topProductText} : {topProduct.ProductId} ({topProduct.TotalQuantity} purchased)";
                }
                else
                {
                    btnTopPurchasesProducts.Text = noTopProductsText;
                }
            }
        }

        // Function to display Purchases by region on button text
        private void DisplayPurchasesByRegion()
        {
            string regionsText;
            string topRegionText;
            string lang = Properties.Settings.Default.Lang;

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
                var PurchasesByRegion = context.Purchases
                    .GroupBy(s => s.BusinessLocationId)
                    .Select(g => new { RegionId = g.Key, TotalPurchases = g.Count() })
                    .ToList();

                // Display a summary or the total for all regions
                var totalRegions = PurchasesByRegion.Count;
                btnPurchasesByRegion.Text = $"{regionsText} : {totalRegions}";

                // Optionally display the region with the most Purchases
                var topRegion = PurchasesByRegion.OrderByDescending(r => r.TotalPurchases).FirstOrDefault();
                if (topRegion != null)
                {
                    btnPurchasesByRegion.Text = $"{topRegionText} : {topRegion.RegionId} ({topRegion.TotalPurchases} Purchases)";
                }
            }
        }

        // Function to display Purchases count by a specified date range on button text
        private void DisplayPurchasesByDateRange()
        {
            string purchasesText;
            string lang = Properties.Settings.Default.Lang;

            if (lang == "en")
            {
                purchasesText = "Purchases";
            }
            else if (lang == "fr")
            {
                purchasesText = "Achats";
            }
            else
            {
                purchasesText = "المشتريات";
            }

            using (var context = new AppDbContext())
            {
                // Get the current year
                int currentYear = DateTime.Now.Year;

                // Set the start date to the beginning of the current year
                DateTime startDate = new DateTime(currentYear, 1, 1);

                // Set the end date to the end of the current year
                DateTime endDate = new DateTime(currentYear, 12, 31);

                // Query purchases within the current year
                var PurchasesByDateRange = context.Purchases
                    .Where(s => s.PurchaseDate >= startDate && s.PurchaseDate <= endDate)
                    .Count();

                // Display the result on the button
                btnPurchasesByDateRange.Text = $"{purchasesText} ({currentYear}) : {PurchasesByDateRange}";
            }
        }

        private void btnAddPurchase_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditPurchase purchase = new AddEditPurchase();
            purchase.setPurchasesObject(this);
            purchase.setTypeOperation("Add");
            purchase.ShowDialog();
        }

        private void btnPurchaseEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.purchaseEdit();
        }

        private void btnPurchaseDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.purchaseDelete();
        }

        private void gridViewPurchases_DoubleClick(object sender, EventArgs e)
        {
            btnPurchaseEdit.Enabled = true;
            btnPurchaseDelete.Enabled = true;

            this.purchase_id = int.Parse(gridViewPurchases.GetRowCellValue(gridViewPurchases.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.purchaseEdit();
        }

        private void repEditPurchase_Click(object sender, EventArgs e)
        {
            btnPurchaseEdit.Enabled = true;
            btnPurchaseDelete.Enabled = true;

            this.purchase_id = int.Parse(gridViewPurchases.GetRowCellValue(gridViewPurchases.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.purchaseEdit();
        }

        private void repDeletePurchase_Click(object sender, EventArgs e)
        {
            this.purchaseDelete();
        }

        private void gridViewPurchases_RowClick(object sender, EventArgs e)
        {
            btnPurchaseEdit.Enabled = true;
            btnPurchaseDelete.Enabled = true;

            this.purchase_id = int.Parse(gridViewPurchases.GetRowCellValue(gridViewPurchases.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void purchaseEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditPurchase purchase = new AddEditPurchase();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            purchase.FormClosed += (s, args) => overlay.Close();

            purchase.setPurchasesObject(this);
            purchase.setTypeOperation("Edit");
            purchase.Show();
            purchase.TopMost = true;
        }

        public void purchaseDelete()
        {
            //btnPurchaseEdit.Enabled = true;
            //btnPurchaseDelete.Enabled = true;

            //this.purchase_id = int.Parse(gridViewPurchases.GetRowCellValue(gridViewPurchases.FocusedRowHandle, "Id").ToString());

            //Models.Purchase purchase = Shared.db.Purchases.Find(this.purchase_id);

            //if (purchase != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    Shared.db.Purchases.Remove(purchase);
            //    Shared.db.SaveChanges();
            //    this.loadPurchases();
            //    Function.Sound.Deleted();
            //}
            //else
            //{
            //    Function.Sound.Wrong();
            //}
        }

        private void btnPurchaseRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnPurchaseEdit.Enabled = false;
            btnPurchaseDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPurchases();
        }

        private void repositoryItemPrint_Click(object sender, EventArgs e)
        {
            Report.FormView formView = new Report.FormView();
            formView.setId(this.purchase_id);
            formView.ShowDialog();
            Sound.Added();
        }

        private void txtPurchaseStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPurchaseStatus.EditValue != null)
            {
                gridControlPurchases.DataSource = Shared.db.Purchases.Where(w => w.PurchaseStatus == txtPurchaseStatus.Text).
                    Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                    .ToList();
            }

            Sound.Selected();
        }

        private void txtSupplier_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSupplier.EditValue != null)
            {
                gridControlPurchases.DataSource = Shared.db.Purchases.Where(w => w.SupplierId == int.Parse(txtSupplier.EditValue.ToString())).
                    Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                    .ToList();
            }

            Sound.Selected();
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                gridControlPurchases.DataSource = Shared.db.Purchases.Where(w => w.WarehouseId == int.Parse(txtWarehouse.EditValue.ToString()))
                    .Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                    .ToList();
            }

            Sound.Selected();
        }

        private void txtBusinessLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBusinessLocation.EditValue != null)
            {
                gridControlPurchases.DataSource = Shared.db.Purchases.Where(w => w.BusinessLocationId == int.Parse(txtBusinessLocation.EditValue.ToString()))
                    .Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                    .ToList();
            }

            Sound.Selected();
        }

        private void txtPaymentStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPaymentStatus.EditValue != null)
            {
                gridControlPurchases.DataSource = Shared.db.Purchases.Where(w => w.PaymentSatus == txtPaymentStatus.Text)
                    .Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation).
                    ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlPurchases.DataSource = Shared.db.Purchases.OrderByDescending(p => p.Id)
                        .Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                        .ToList();
                }
                else
                {
                    gridControlPurchases.DataSource = Shared.db.Purchases.Include(s => s.Supplier).Include(w => w.Warehouse).Include(b => b.BusinessLocation)
                        .ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickPurchase_Click(object sender, EventArgs e)
        {
            Purchase.AddEditPurchase addEditPurchase = new Purchase.AddEditPurchase();
            addEditPurchase.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Purchases
                //.Where(p => p.PurchaseDate == today)
                .Sum(p => p.NetTotalAmount);

            return decimal.Parse(totalAmount.ToString());
        }

        private void btnPurchaseHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            Purchase.PurchaseHistory history = new Purchase.PurchaseHistory();
            history.ShowDialog();
            Sound.Selected();
        }

        private void btnFilterRefresh_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtPaymentStatus.Text = string.Empty;
            txtBusinessLocation.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtSupplier.Text = string.Empty;
            txtPurchaseStatus.Text = string.Empty;
            this.loadPurchases();
            Sound.Added();
        }

        private void btnPrintPurchases_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlPurchases.ShowPrintPreview();
        }

        private void btnHistories_Click(object sender, EventArgs e)
        {
            Purchase.PurchaseHistory history = new Purchase.PurchaseHistory();
            history.ShowDialog();
            Sound.Selected();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditPurchase purchase = new AddEditPurchase();
            //purchase.setPurchasesObject(this);
            purchase.setTypeOperation("Add");
            purchase.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.purchaseEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.purchaseDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlPurchases.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtPaymentStatus.Text = string.Empty;
            txtBusinessLocation.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtSupplier.Text = string.Empty;
            txtPurchaseStatus.Text = string.Empty;
            this.loadPurchases();
            Sound.Added();
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
                    int totalItems = context.Purchases.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadPurchases();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPurchases, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPurchases, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPurchases, "xlsx");
        }

        private void formExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}