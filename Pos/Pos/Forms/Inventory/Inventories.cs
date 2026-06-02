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

namespace Pos.Forms.Inventory
{
    public partial class Inventories : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int inventory_id = 0;

        public Inventories()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewInventories.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewInventories.RowStyle += gridViewInventories_RowStyle;
            gridViewInventories.FocusedRowChanged += gridViewInventories_FocusedRowChanged;
            gridViewInventories.CustomDrawCell += gridViewInventories_CustomDrawCell;
            gridViewInventories.RowHeight = Function.Helper.RowHeight;
            gridViewInventories.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewInventories_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewInventories.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewInventories_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewInventories.FocusedRowHandle && e.Column == gridViewInventories.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewInventories_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewInventories.RowCount > 0 && gridViewInventories.FocusedRowHandle >= 0)
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
            inventoryEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Inventory"))
            {
                AddEditInventory addEditInventory = new AddEditInventory();
                addEditInventory.setInventoriesObject(this);
                addEditInventory.setTypeOperation("Add");
                addEditInventory.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            inventoryDelete();
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
            foreach (GridColumn column in gridViewInventories.Columns)
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

        private void Inventories_Load(object sender, EventArgs e)
        {
            btnInventoryEdit.Enabled = false;
            btnInventoryDelete.Enabled = false;

            this.loadInventories();

            txtPurchaseTotal.Text = this.getTotalAmount() + " DA";

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadInventories()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Inventories
                .Where(p => p.Name.Contains(searchTerm) ||
                        p.ReferenceNo.Contains(searchTerm))
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
                var currentPageData = context.Inventories.Include(w => w.Warehouse)
                    .Where(p => p.Name.Contains(searchTerm) ||
                        p.ReferenceNo.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlInventories.DataSource = currentPageData;
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

        private void btnAddInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditInventory addEditInventory = new AddEditInventory();
            addEditInventory.setInventoriesObject(this);
            addEditInventory.setTypeOperation("Add");
            addEditInventory.ShowDialog();
        }

        private void btnInventoryEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.inventoryEdit();
        }

        private void btnInventoryDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.inventoryDelete();
        }

        private void gridViewInventories_DoubleClick(object sender, EventArgs e)
        {
            btnInventoryEdit.Enabled = true;
            btnInventoryDelete.Enabled = true;

            this.inventory_id = int.Parse(gridViewInventories.GetRowCellValue(gridViewInventories.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.inventoryEdit();
        }

        private void repEditInventory_Click(object sender, EventArgs e)
        {
            btnInventoryEdit.Enabled = true;
            btnInventoryDelete.Enabled = true;

            this.inventory_id = int.Parse(gridViewInventories.GetRowCellValue(gridViewInventories.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.inventoryEdit();
        }

        private void repDeleteInventory_Click(object sender, EventArgs e)
        {
            this.inventoryDelete();
        }

        private void gridViewInventories_RowClick(object sender, EventArgs e)
        {
            btnInventoryEdit.Enabled = true;
            btnInventoryDelete.Enabled = true;

            this.inventory_id = int.Parse(gridViewInventories.GetRowCellValue(gridViewInventories.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void inventoryEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditInventory purchase = new AddEditInventory();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            purchase.FormClosed += (s, args) => overlay.Close();

            purchase.setInventoriesObject(this);
            purchase.setTypeOperation("Edit");
            purchase.Show();
            purchase.TopMost = true;
        }

        public void inventoryDelete()
        {
            btnEditItem.Enabled = false;
            btnDeleteItem.Enabled = false;
            using (AppDbContext AppDb = new AppDbContext())
            {
                this.inventory_id = int.Parse(gridViewInventories.GetRowCellValue(gridViewInventories.FocusedRowHandle, "Id").ToString());

                Models.Inventory inventory = AppDb.Inventories.Find(this.inventory_id);

                if (inventory != null & XtraMessageBox.Show("Are you sure want to delete Inventory ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var inventoryItems = AppDb.InventoryItems.Where(item => item.InventoryId == this.inventory_id).ToList();
                    // Supprimer tous les InventoryItems associés
                    if (inventoryItems.Any())
                    {
                        AppDb.InventoryItems.RemoveRange(inventoryItems);
                    }
                    AppDb.Inventories.Remove(inventory);
                    AppDb.SaveChanges();
                    this.loadInventories();
                    Function.Sound.Deleted();
                }
                else
                {
                    Function.Sound.Wrong();
                }
            }

        }

        private void repositoryItemPrint_Click(object sender, EventArgs e)
        {
            Report.FormView formView = new Report.FormView();
            formView.setId(this.inventory_id);
            formView.ShowDialog();
            Sound.Added();
        }

        private void txtInventoryStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (txtInventoryStatus.EditValue != null)
            {
                gridControlInventories.DataSource = Shared.db.Purchases.Where(w => w.PurchaseStatus == txtInventoryStatus.Text).Include(w => w.Warehouse).ToList();
            }

            Sound.Selected();
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                gridControlInventories.DataSource = Shared.db.Purchases.Where(w => w.WarehouseId == int.Parse(txtWarehouse.EditValue.ToString())).Include(w => w.Warehouse).ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlInventories.DataSource = Shared.db.Purchases.OrderByDescending(p => p.Id).Include(w => w.Warehouse).ToList();
                }
                else
                {
                    gridControlInventories.DataSource = Shared.db.Purchases.Include(w => w.Warehouse).ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickPurchase_Click(object sender, EventArgs e)
        {
            Inventory.AddEditInventory addEditInventory = new Inventory.AddEditInventory();
            addEditInventory.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Inventories
                //.Where(p => p.PurchaseDate == today)
                .Sum(p => p.DiffAmount);

            return decimal.Parse(totalAmount.ToString());
        }

        private void btnInventoryHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            Inventory.InventoryHistory history = new Inventory.InventoryHistory();
            history.ShowDialog();
            Sound.Selected();
        }

        private void btnFilterRefresh_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtInventoryStatus.Text = string.Empty;
            this.loadInventories();
            Sound.Added();
        }

        private void btnPrintInventories_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlInventories.ShowPrintPreview();
        }

        private void btnInventoryRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnHistories_Click(object sender, EventArgs e)
        {
            Inventory.InventoryHistory history = new Inventory.InventoryHistory();
            history.ShowDialog();
            Sound.Selected();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditInventory addEditInventory = new AddEditInventory();
            addEditInventory.setInventoriesObject(this);
            addEditInventory.setTypeOperation("Add");
            addEditInventory.ShowDialog();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.inventoryDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlInventories.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            txtInventoryStatus.Text = string.Empty;
            this.loadInventories();
            Sound.Added();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.inventoryEdit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    int totalItems = context.Inventories.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadInventories();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlInventories, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlInventories, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlInventories, "xlsx");
        }

        private void gridViewInventories_RowClick(object sender, RowClickEventArgs e)
        {

        }
    }
}