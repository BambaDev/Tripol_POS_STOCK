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

namespace Pos.Forms.Product.Variation
{
    public partial class Variations : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int variation_id = 0;

        public Variations()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewVariations.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewVariations.RowStyle += gridViewVariations_RowStyle;
            gridViewVariations.FocusedRowChanged += gridViewVariations_FocusedRowChanged;
            gridViewVariations.CustomDrawCell += gridViewVariations_CustomDrawCell;
            gridViewVariations.RowHeight = Function.Helper.RowHeight;
            gridViewVariations.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewVariations_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewVariations.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewVariations_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewVariations.FocusedRowHandle && e.Column == gridViewVariations.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewVariations_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewVariations.RowCount > 0 && gridViewVariations.FocusedRowHandle >= 0)
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
            variationEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Variation"))
            {
                AddEditVariation variation = new AddEditVariation();
                variation.setVariationsObject(this);
                variation.setTypeOperation("Add");
                variation.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            variationDelete();
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
            foreach (GridColumn column in gridViewVariations.Columns)
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

        private void Variations_Load(object sender, EventArgs e)
        {
            btnVariationEdit.Enabled = false;
            btnVariationDelete.Enabled = false;
            this.loadVariations();
        }

        public void loadVariations()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Variations
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
                var currentPageData = context.Variations
                    .Where(p => p.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlVariations.DataSource = currentPageData;
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

        private void btnAddVariation_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditVariation variation = new AddEditVariation();
            variation.setVariationsObject(this);
            variation.setTypeOperation("Add");
            variation.ShowDialog();
        }

        private void btnVariationEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.variationEdit();
        }

        private void btnVariationDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.variationDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnVariationEdit.Enabled = true;
            btnVariationDelete.Enabled = true;

            this.variation_id = int.Parse(gridViewVariations.GetRowCellValue(gridViewVariations.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewVariations_DoubleClick(object sender, EventArgs e)
        {
            btnVariationEdit.Enabled = true;
            btnVariationDelete.Enabled = true;

            this.variation_id = int.Parse(gridViewVariations.GetRowCellValue(gridViewVariations.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.variationEdit();
        }

        private void repEditVariation_Click(object sender, EventArgs e)
        {
            btnVariationEdit.Enabled = true;
            btnVariationDelete.Enabled = true;

            this.variation_id = int.Parse(gridViewVariations.GetRowCellValue(gridViewVariations.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.variationEdit();
        }

        private void repDeleteVariation_Click(object sender, EventArgs e)
        {
            this.variationDelete();
        }

        private void gridViewVariations_RowClick(object sender, EventArgs e)
        {
            btnVariationEdit.Enabled = true;
            btnVariationDelete.Enabled = true;

            this.variation_id = int.Parse(gridViewVariations.GetRowCellValue(gridViewVariations.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void variationEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditVariation variation = new AddEditVariation();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            variation.FormClosed += (s, args) => overlay.Close();

            variation.setVariationsObject(this);
            variation.setTypeOperation("Edit");
            variation.Show();
            variation.TopMost = true;
        }

        public void variationDelete()
        {
            btnVariationEdit.Enabled = true;
            btnVariationDelete.Enabled = true;

            this.variation_id = int.Parse(gridViewVariations.GetRowCellValue(gridViewVariations.FocusedRowHandle, "Id").ToString());

            Models.Variation variation = Shared.db.Variations.Find(this.variation_id);

            if (variation != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Variations.Remove(variation);
                Shared.db.SaveChanges();
                this.loadVariations();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnVariationRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnVariationEdit.Enabled = false;
            btnVariationDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadVariations();
        }

        private void btnPrintVariations_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlVariations.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditVariation variation = new AddEditVariation();
            //variation.setVariationsObject(this);
            variation.setTypeOperation("Add");
            variation.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.variationEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.variationDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlVariations.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnVariationEdit.Enabled = false;
            btnVariationDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadVariations();
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
                    int totalItems = context.Variations.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadVariations();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlVariations, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlVariations, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlVariations, "xlsx");
        }
    }
}