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
using System.Runtime.InteropServices;
using Pos.Forms.Product.Warehouse;

namespace Pos.Forms.Stock.Adjustment
{
    public partial class Adjustments : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int adjustment_id = 0;

        public Adjustments()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewAdjustments.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewAdjustments.RowStyle += gridViewAdjustments_RowStyle;
            gridViewAdjustments.FocusedRowChanged += gridViewAdjustments_FocusedRowChanged;
            gridViewAdjustments.CustomDrawCell += gridViewAdjustments_CustomDrawCell;
            gridViewAdjustments.RowHeight = Function.Helper.RowHeight;
            gridViewAdjustments.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewAdjustments_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewAdjustments.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewAdjustments_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewAdjustments.FocusedRowHandle && e.Column == gridViewAdjustments.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewAdjustments_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewAdjustments.RowCount > 0 && gridViewAdjustments.FocusedRowHandle >= 0)
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
            adjustmentEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Adjustment"))
            {
                AddEditAdjustment adjustment = new AddEditAdjustment();
                adjustment.setAdjustmentsObject(this);
                adjustment.setTypeOperation("Add");
                adjustment.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            adjustmentDelete();
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
            foreach (GridColumn column in gridViewAdjustments.Columns)
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
            btnAdjustmentEdit.Enabled = false;
            btnAdjustmentDelete.Enabled = false;

            this.loadAdjustments();

            txtPurchaseTotal.Text = this.getTotalAmount() + " DA";

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadAdjustments()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Adjustments
                .Where(p => p.Warehouse.Name.Contains(searchTerm) ||
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
                var currentPageData = context.Adjustments.Include(w=>w.Warehouse)
                    .Where(p => p.Warehouse.Name.Contains(searchTerm) ||
                        p.ReferenceNo.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlAdjustments.DataSource = currentPageData;
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

        private void btnAddAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditAdjustment adjustment = new AddEditAdjustment();
            adjustment.setAdjustmentsObject(this);
            adjustment.setTypeOperation("Add");
            adjustment.ShowDialog();
        }

        private void btnAdjustmentEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.adjustmentEdit();
        }

        private void btnAdjustmentDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.adjustmentDelete();
        }

        private void gridViewAdjustments_DoubleClick(object sender, EventArgs e)
        {
            btnAdjustmentEdit.Enabled = true;
            btnAdjustmentDelete.Enabled = true;

            this.adjustment_id = int.Parse(gridViewAdjustments.GetRowCellValue(gridViewAdjustments.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.adjustmentEdit();
        }

        private void repEditAdjustment_Click(object sender, EventArgs e)
        {
            btnAdjustmentEdit.Enabled = true;
            btnAdjustmentDelete.Enabled = true;

            this.adjustment_id = int.Parse(gridViewAdjustments.GetRowCellValue(gridViewAdjustments.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.adjustmentEdit();
        }

        private void repDeleteAdjustment_Click(object sender, EventArgs e)
        {
            this.adjustmentDelete();
        }

        private void gridViewAdjustments_RowClick(object sender, EventArgs e)
        {
            btnAdjustmentEdit.Enabled = true;
            btnAdjustmentDelete.Enabled = true;

            this.adjustment_id = int.Parse(gridViewAdjustments.GetRowCellValue(gridViewAdjustments.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void adjustmentEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditAdjustment adjustment = new AddEditAdjustment();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            adjustment.FormClosed += (s, args) => overlay.Close();

            adjustment.setAdjustmentsObject(this);
            adjustment.setTypeOperation("Edit");
            adjustment.Show();
            adjustment.TopMost = true;
        }

        public void adjustmentDelete()
        {
            btnAdjustmentEdit.Enabled = true;
            btnAdjustmentDelete.Enabled = true;
            using (AppDbContext AppDb = new AppDbContext())
            {
                // Récupérer l'ID de l'ajustement sélectionné
                if (int.TryParse(gridViewAdjustments.GetRowCellValue(gridViewAdjustments.FocusedRowHandle, "Id")?.ToString(), out this.adjustment_id))
                {
                    // Trouver l'ajustement dans la base de données
                    Models.Adjustment adjustment = AppDb.Adjustments.Find(this.adjustment_id);

                    // Vérifier si l'ajustement existe
                    if (adjustment != null && XtraMessageBox.Show("Are you sure want to delete Item?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Récupérer tous les ProductAdjustments liés à cet Adjustment
                        var productAdjustments = AppDb.ProductAdjustments
                                                           .Where(pa => pa.AdjustmentId == this.adjustment_id)
                                                           .ToList();

                        // Supprimer tous les ProductAdjustments associés
                        foreach (var productAdjustment in productAdjustments)
                        {
                            Models.ProductWarehouse productInWarehouse = AppDb.ProductWarehouses
                                  .FirstOrDefault(p => p.ProductId == productAdjustment.ProductId && p.WarehouseId == adjustment.WarehouseId);
                            if (adjustment.Action!= "Addition")
                            {
                                if (productInWarehouse != null)
                                {
                                    productInWarehouse.Qty += productAdjustment.Qty;
                                    productInWarehouse.UpdatedAt = DateTime.Now;
                                }
                            }
                            else
                            {
                                if (productInWarehouse != null)
                                {
                                    productInWarehouse.Qty -= productAdjustment.Qty;
                                    productInWarehouse.UpdatedAt = DateTime.Now;
                                }
                            }
                            AppDb.ProductWarehouses.Update(productInWarehouse);
                            AppDb.ProductAdjustments.Remove(productAdjustment);
                            AppDb.SaveChanges(); 
                        }

                        // Supprimer l'ajustement lui-même
                        AppDb.Adjustments.Remove(adjustment);
                        AppDb.SaveChanges();

                        // Recharger la liste des ajustements
                        this.loadAdjustments();
                        Function.Sound.Deleted();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Invalid adjustment ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void btnAdjustmentRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnAdjustmentEdit.Enabled = false;
            btnAdjustmentDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadAdjustments();
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                gridControlAdjustments.DataSource = Shared.db.Adjustments.Where(w => w.WarehouseId == int.Parse(txtWarehouse.EditValue.ToString())).Include(w => w.Warehouse).ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlAdjustments.DataSource = Shared.db.Adjustments.OrderByDescending(p => p.Id).Include(w => w.Warehouse).ToList();
                }
                else
                {
                    gridControlAdjustments.DataSource = Shared.db.Adjustments.Include(w => w.Warehouse).ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickAdjustment_Click(object sender, EventArgs e)
        {
            Adjustment.AddEditAdjustment addEditAdjustment = new Adjustment.AddEditAdjustment();
            addEditAdjustment.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Adjustments
                //.Where(p => p.PurchaseDate == today)
                .Sum(p => p.TotalQty);

            return decimal.Parse(totalAmount.ToString());
        }

        private void btnPurchaseHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            // do somthing
        }

        private void btnFilterRefresh_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            this.loadAdjustments();
            Sound.Added();
        }

        private void btnPrintAdjustments_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlAdjustments.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditAdjustment adjustment = new AddEditAdjustment();
            adjustment.setAdjustmentsObject(this);
            adjustment.setTypeOperation("Add");
            adjustment.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.adjustmentEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.adjustmentDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlAdjustments.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            this.loadAdjustments();
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
                    int totalItems = context.Adjustments.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadAdjustments();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAdjustments, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAdjustments, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAdjustments, "xlsx");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}