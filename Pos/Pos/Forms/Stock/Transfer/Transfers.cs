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

namespace Pos.Forms.Stock.Transfer
{
    public partial class Transfers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int transfer_id = 0;

        public Transfers()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewTransfers.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewTransfers.RowStyle += gridViewTransfers_RowStyle;
            gridViewTransfers.FocusedRowChanged += gridViewTransfers_FocusedRowChanged;
            gridViewTransfers.CustomDrawCell += gridViewTransfers_CustomDrawCell;
            gridViewTransfers.RowHeight = Function.Helper.RowHeight;
            gridViewTransfers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewTransfers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewTransfers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewTransfers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewTransfers.FocusedRowHandle && e.Column == gridViewTransfers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewTransfers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewTransfers.RowCount > 0 && gridViewTransfers.FocusedRowHandle >= 0)
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
            transferEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Transfer"))
            {
                AddEditTransfer addEditTransfer = new AddEditTransfer();
                addEditTransfer.setTransfersObject(this);
                addEditTransfer.setTypeOperation("Add");
                addEditTransfer.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            transferDelete();
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
            foreach (GridColumn column in gridViewTransfers.Columns)
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

        private void Transfers_Load(object sender, EventArgs e)
        {
            btnTransferEdit.Enabled = false;
            btnTransferDelete.Enabled = false;

            this.loadTransfers();

            txtTotal.Text = this.getTotalAmount() + " DA";

            txtFromWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtFromWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtFromWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtToWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtToWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtToWarehouse.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadTransfers()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Transfers
                .Where(p => p.FromWarehouse.Name.Contains(searchTerm) ||
                        p.ToWarehouse.Name.Contains(searchTerm))
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
                var currentPageData = context.Transfers.Include(w=>w.FromWarehouse).Include(ww=>ww.ToWarehouse)
                    .Where(p => p.FromWarehouse.Name.Contains(searchTerm) ||
                            p.ToWarehouse.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlTransfers.DataSource = currentPageData;
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

        private void btnAddTransfer_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditTransfer addEditTransfer = new AddEditTransfer();
            addEditTransfer.setTransfersObject(this);
            addEditTransfer.setTypeOperation("Add");
            addEditTransfer.ShowDialog();
        }

        private void btnTransferEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.transferEdit();
        }

        private void btnTransferDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.transferDelete();
        }

        private void gridViewTransfers_DoubleClick(object sender, EventArgs e)
        {
            btnTransferEdit.Enabled = true;
            btnTransferDelete.Enabled = true;

            this.transfer_id = int.Parse(gridViewTransfers.GetRowCellValue(gridViewTransfers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.transferEdit();
        }

        private void repEditTransfer_Click(object sender, EventArgs e)
        {
            btnTransferEdit.Enabled = true;
            btnTransferDelete.Enabled = true;

            this.transfer_id = int.Parse(gridViewTransfers.GetRowCellValue(gridViewTransfers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.transferEdit();
        }

        private void repDeleteTransfer_Click(object sender, EventArgs e)
        {
            this.transferDelete();
        }

        private void gridViewTransfers_RowClick(object sender, EventArgs e)
        {
            btnTransferEdit.Enabled = true;
            btnTransferDelete.Enabled = true;

            this.transfer_id = int.Parse(gridViewTransfers.GetRowCellValue(gridViewTransfers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void transferEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditTransfer transfer = new AddEditTransfer();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            transfer.FormClosed += (s, args) => overlay.Close();

            transfer.setTransfersObject(this);
            transfer.setTypeOperation("Edit");
            transfer.Show();
            transfer.TopMost = true;
        }

        public void transferDelete()
        {
            btnTransferEdit.Enabled = true;
            btnTransferDelete.Enabled = true;

            this.transfer_id = int.Parse(gridViewTransfers.GetRowCellValue(gridViewTransfers.FocusedRowHandle, "Id").ToString());
            using (AppDbContext AppDb = new AppDbContext())
            {
                Models.Transfer transfer = AppDb.Transfers.SingleOrDefault(x=>x.Id==this.transfer_id);

                if (transfer != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    var productTransfers = AppDb.ProductTransfers.Where(pt => pt.TransferId == this.transfer_id).ToList();

                    // Suppression de tous les ProductTransfers liés
                    foreach (var productTransfer in productTransfers)
                    {
                        Models.ProductWarehouse fromproductWarehouse = AppDb.ProductWarehouses.Where(x => x.WarehouseId == transfer.FromWarehouseId && x.ProductId == productTransfer.ProductId)
                            .FirstOrDefault();
                        fromproductWarehouse.Qty += productTransfer.Qty;
                        Models.ProductWarehouse toproductWarehouse = AppDb.ProductWarehouses.Where(x => x.WarehouseId == transfer.ToWarehouseId && x.ProductId == productTransfer.ProductId)
                            .FirstOrDefault();
                        toproductWarehouse.Qty-= productTransfer.Qty;
                        AppDb.ProductWarehouses.Update(fromproductWarehouse);
                        AppDb.ProductWarehouses.Update(toproductWarehouse);
                        AppDb.ProductTransfers.Remove(productTransfer);
                        AppDb.SaveChanges();
                    }

                    AppDb.Transfers.Remove(transfer);
                    AppDb.SaveChanges();
                    this.loadTransfers();
                    Function.Sound.Deleted();
                }
                else
                {
                    Function.Sound.Wrong();
                }
            }
         
        }

        private void btnFiltersRefresh_Click(object sender, ItemClickEventArgs e)
        {
            btnTransferEdit.Enabled = false;
            btnTransferDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadTransfers();
        }

        private void txtFromWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtFromWarehouse.EditValue != null)
            {
                gridControlTransfers.DataSource = Shared.db.Transfers.Where(w => w.FromWarehouseId == int.Parse(txtFromWarehouse.EditValue.ToString())).ToList();
            }

            Sound.Selected();
        }

        private void txtToWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtToWarehouse.EditValue != null)
            {
                gridControlTransfers.DataSource = Shared.db.Transfers.Where(w => w.ToWarehouseId == int.Parse(txtToWarehouse.EditValue.ToString())).ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlTransfers.DataSource = Shared.db.Transfers.OrderByDescending(p => p.Id).ToList();
                }
                else
                {
                    gridControlTransfers.DataSource = Shared.db.Transfers.ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickTransfer_Click(object sender, EventArgs e)
        {
            Transfer.AddEditTransfer addEditTransfer = new Transfer.AddEditTransfer();
            addEditTransfer.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Transfers
                //.Where(p => p.PurchaseDate == today)
                .Sum(p => p.GrandTotal);

            return decimal.Parse(totalAmount.ToString());
        }

        private void btnPurchaseHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            // do somthing
        }

        private void btnTransferRefresh_ItemClick(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtFromWarehouse.Text = string.Empty;
            this.loadTransfers();
            Sound.Added();
        }

        private void btnPrintTransfers_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlTransfers.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditTransfer addEditTransfer = new AddEditTransfer();
            addEditTransfer.setTransfersObject(this);
            addEditTransfer.setTypeOperation("Add");
            addEditTransfer.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.transferEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.transferDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlTransfers.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtFromWarehouse.Text = string.Empty;
            this.loadTransfers();
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
                    int totalItems = context.Suppliers.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadTransfers();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlTransfers, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlTransfers, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlTransfers, "xlsx");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}