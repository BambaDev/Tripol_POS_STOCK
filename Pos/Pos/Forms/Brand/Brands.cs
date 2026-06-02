using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Pos.Forms.Overlay;
using Pos.Forms.Supplier;
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

namespace Pos.Forms.Brand
{
    public partial class Brands : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int brand_id = 0;

        public Brands()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewBrands.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewBrands.RowStyle += gridViewBrands_RowStyle;
            gridViewBrands.FocusedRowChanged += gridViewBrands_FocusedRowChanged;
            gridViewBrands.CustomDrawCell += gridViewBrands_CustomDrawCell;
            gridViewBrands.RowHeight = Function.Helper.RowHeight;
            gridViewBrands.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewBrands_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewBrands.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewBrands_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewBrands.FocusedRowHandle && e.Column == gridViewBrands.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewBrands_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewBrands.RowCount > 0 && gridViewBrands.FocusedRowHandle >= 0)
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
            brandEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Brand"))
            {
                AddEditBrand brand = new AddEditBrand();
                brand.setBrandsObject(this);
                brand.setTypeOperation("Add");
                brand.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            brandDelete();
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
            //layoutControlGroup4.AppearanceGroup.Font = customFont10;
            //layoutControlGroup5.AppearanceGroup.Font = customFont10;
            //layoutControlGroup6.AppearanceGroup.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;
            layoutControlItem10.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewBrands.Columns)
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

        private void Brands_Load(object sender, EventArgs e)
        {
            btnBrandEdit.Enabled = false;
            btnBrandDelete.Enabled = false;
            this.loadBrands();
        }

        public void loadBrands()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Brands
                .Where(p => p.Name.Contains(searchTerm))
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
                var currentPageData = context.Brands
                    .Where(p => p.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlBrands.DataSource = currentPageData;
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

        private void btnAddBrand_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Brand"))
            {
                AddEditBrand brand = new AddEditBrand();
                brand.setBrandsObject(this);
                brand.setTypeOperation("Add");
                brand.ShowDialog();
            }
        }

        private void btnBrandEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                this.brandEdit();
            }
        }

        private void btnBrandDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Delete Brand"))
            {
                this.brandDelete();
            }
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnBrandEdit.Enabled = true;
            btnBrandDelete.Enabled = true;

            this.brand_id = int.Parse(gridViewBrands.GetRowCellValue(gridViewBrands.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewBrands_DoubleClick(object sender, EventArgs e)
        {
            btnBrandEdit.Enabled = true;
            btnBrandDelete.Enabled = true;

            this.brand_id = int.Parse(gridViewBrands.GetRowCellValue(gridViewBrands.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.brandEdit();
        }

        private void repEditBrand_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                btnBrandEdit.Enabled = true;
                btnBrandDelete.Enabled = true;

                this.brand_id = int.Parse(gridViewBrands.GetRowCellValue(gridViewBrands.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.brandEdit();
            }
        }

        private void repDeleteBrand_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Delete Brand"))
            {
                this.brandDelete();
            }
        }

        private void gridViewBrands_RowClick(object sender, EventArgs e)
        {
            btnBrandEdit.Enabled = true;
            btnBrandDelete.Enabled = true;

            this.brand_id = int.Parse(gridViewBrands.GetRowCellValue(gridViewBrands.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void brandEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditBrand role = new AddEditBrand();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            role.FormClosed += (s, args) => overlay.Close();

            role.setBrandsObject(this);
            role.setTypeOperation("Edit");
            role.Show();
            role.TopMost = true;
        }

        public void brandDelete()
        {
            btnBrandEdit.Enabled = true;
            btnBrandDelete.Enabled = true;

            this.brand_id = int.Parse(gridViewBrands.GetRowCellValue(gridViewBrands.FocusedRowHandle, "Id").ToString());

            Models.Brand brand = Shared.db.Brands.Find(this.brand_id);

            if (brand != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Brands.Remove(brand);
                Shared.db.SaveChanges();
                this.loadBrands();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnBrandRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnBrandEdit.Enabled = false;
            btnBrandDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadBrands();
        }

        private void btnPrintBrands_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlBrands.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnBrandEdit.Enabled = false;
            btnBrandDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadBrands();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Brand"))
            {
                AddEditBrand brand = new AddEditBrand();
                brand.setBrandsObject(this);
                brand.setTypeOperation("Add");
                brand.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                this.brandEdit();
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Delete Brand"))
            {
                this.brandDelete();
            }
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlBrands.ShowPrintPreview();
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
                    int totalItems = context.Brands.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadBrands();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlBrands, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlBrands, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlBrands, "xlsx");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}