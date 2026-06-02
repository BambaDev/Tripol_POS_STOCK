using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Pos.Forms.Overlay;
using Pos.Forms.TodoList;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Supplier
{
    public partial class Suppliers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int supplier_id = 0;

        public Suppliers()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewSuppliers.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewSuppliers.RowStyle += gridViewSuppliers_RowStyle;
            gridViewSuppliers.FocusedRowChanged += gridViewSuppliers_FocusedRowChanged;
            gridViewSuppliers.CustomDrawCell += gridViewSuppliers_CustomDrawCell;
            gridViewSuppliers.RowHeight = Function.Helper.RowHeight;
            gridViewSuppliers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewSuppliers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewSuppliers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewSuppliers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewSuppliers.FocusedRowHandle && e.Column == gridViewSuppliers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewSuppliers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewSuppliers.RowCount > 0 && gridViewSuppliers.FocusedRowHandle >= 0)
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
            supplierEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Supplier"))
            {
                AddEditSupplier supplier = new AddEditSupplier();
                supplier.setSuppliersObject(this);
                supplier.setTypeOperation("Add");
                supplier.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            supplierDelete();
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

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewSuppliers.Columns)
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

        private void Suppliers_Load(object sender, EventArgs e)
        {
            btnSupplierEdit.Enabled = false;
            btnSupplierDelete.Enabled = false;
            this.loadSuppliers();
        }

        public void loadSuppliers()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Suppliers
                .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm))
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
                var currentPageData = context.Suppliers
                    .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlSuppliers.DataSource = currentPageData;
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

        public void supplierEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditSupplier supplier = new AddEditSupplier();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            supplier.FormClosed += (s, args) => overlay.Close();

            supplier.setSuppliersObject(this);
            supplier.setTypeOperation("Edit");
            supplier.Show();
            supplier.TopMost = true;
        }

        public void supplierDelete()
        {
            btnSupplierEdit.Enabled = true;
            btnSupplierDelete.Enabled = true;

            this.supplier_id = int.Parse(gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, "Id").ToString());

            Models.Supplier supplier = Shared.db.Suppliers.Find(this.supplier_id);

            if (supplier != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var relatedPurchases = Shared.db.Purchases.Where(p => p.SupplierId == supplier.Id).ToList();
                foreach (var purchase in relatedPurchases)
                {
                    purchase.SupplierId = null; // Or assign to another Supplier
                    Shared.db.Purchases.Update(purchase);
                    Shared.db.SaveChanges();
                }
                var relatedReturnPurchase = Shared.db.ReturnPurchases.Where(p => p.SupplierId == supplier.Id).ToList();
                foreach (var purchase in relatedReturnPurchase)
                {
                    purchase.SupplierId = null; // Or assign to another Supplier
                    Shared.db.ReturnPurchases.Update(purchase);
                    Shared.db.SaveChanges();
                }
                
                Shared.db.Suppliers.Remove(supplier);
                Shared.db.SaveChanges();
                this.loadSuppliers();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnAddSupplier_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditSupplier supplier = new AddEditSupplier();
            supplier.setSuppliersObject(this);
            supplier.setTypeOperation("Add");
            supplier.ShowDialog();
        }

        private void btnSupplierEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.supplierEdit();
        }

        private void btnSupplierDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.supplierDelete();
        }

        private void btnSupplierRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnSupplierEdit.Enabled = false;
            btnSupplierDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadSuppliers();
        }

        private void gridViewTechnicals_DoubleClick(object sender, EventArgs e)
        {
            btnSupplierEdit.Enabled = true;
            btnSupplierDelete.Enabled = true;

            this.supplier_id = int.Parse(gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.supplierEdit();
        }

        private void gridViewTechnicals_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnSupplierEdit.Enabled = true;
            btnSupplierDelete.Enabled = true;

            this.supplier_id = int.Parse(gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void repEditSupplier_Click(object sender, EventArgs e)
        {
            btnSupplierEdit.Enabled = true;
            btnSupplierDelete.Enabled = true;

            this.supplier_id = int.Parse(gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.supplierEdit();
        }

        private void repDeleteSupplier_Click(object sender, EventArgs e)
        {
            this.supplierDelete();
        }

        private void btnPrintSuppliers_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlSuppliers.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditSupplier supplier = new AddEditSupplier();
            supplier.setSuppliersObject(this);
            supplier.setTypeOperation("Add");
            supplier.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.supplierEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.supplierDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlSuppliers.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnSupplierEdit.Enabled = false;
            btnSupplierDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadSuppliers();
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
            this.loadSuppliers();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSuppliers, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSuppliers, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlSuppliers, "xlsx");
        }
    }
}