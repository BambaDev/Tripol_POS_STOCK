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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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

namespace Pos.Forms.Product
{
    public partial class Products : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int product_id = 0;

        public Products()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewProducts.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewProducts.RowStyle += gridViewProducts_RowStyle;
            gridViewProducts.FocusedRowChanged += gridViewProducts_FocusedRowChanged;
            gridViewProducts.CustomDrawCell += gridViewProducts_CustomDrawCell;
            gridViewProducts.RowHeight = Function.Helper.RowHeight;
            gridViewProducts.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewProducts_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewProducts.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewProducts_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewProducts.FocusedRowHandle && e.Column == gridViewProducts.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewProducts_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewProducts.RowCount > 0 && gridViewProducts.FocusedRowHandle >= 0)
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
            productEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Product"))
            {
                AddEditProduct product = new AddEditProduct();
                product.setProductsObject(this);
                product.setTypeOperation("Add");
                product.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            productDelete();
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
            foreach (GridColumn column in gridViewProducts.Columns)
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

        private void Products_Load(object sender, EventArgs e)
        {
            btnProductEdit.Enabled = false;
            btnProductDelete.Enabled = false;
            this.loadProducts();
        }

        public void loadProducts()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Products
                .Include(b => b.Brand)
                .Include(c => c.Category)
                .Where(p => !p.IsArchived &&  // FILTRE: Exclure produits archivés
                           (p.ProductName.Contains(searchTerm) ||
                            p.Brand.Name.Contains(searchTerm) ||
                            p.Category.Name.Contains(searchTerm)))
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
                var currentPageData = context.Products
                    .Include(b => b.Brand)
                    .Include(c => c.Category)
                    .Where(p => !p.IsArchived &&  // FILTRE: Exclure produits archivés
                               (p.ProductName.Contains(searchTerm) ||
                                p.Brand.Name.Contains(searchTerm) ||
                                p.Category.Name.Contains(searchTerm)))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlProducts.DataSource = currentPageData;
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

        private void btnAddProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditProduct product = new AddEditProduct();
            product.setProductsObject(this);
            product.setTypeOperation("Add");
            product.ShowDialog();
        }

        private void btnProductEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.productEdit();
        }

        private void btnProductDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.productDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnProductEdit.Enabled = true;
            btnProductDelete.Enabled = true;

            this.product_id = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewProducts_DoubleClick(object sender, EventArgs e)
        {
            btnProductEdit.Enabled = true;
            btnProductDelete.Enabled = true;

            this.product_id = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.productEdit();
        }

        private void repEditProduct_Click(object sender, EventArgs e)
        {
            btnProductEdit.Enabled = true;
            btnProductDelete.Enabled = true;

            this.product_id = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.productEdit();
        }

        private void repDeleteProduct_Click(object sender, EventArgs e)
        {
            this.productDelete();
        }

        private void gridViewProducts_RowClick(object sender, EventArgs e)
        {
            btnProductEdit.Enabled = true;
            btnProductDelete.Enabled = true;

            this.product_id = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void productEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditProduct product = new AddEditProduct();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            product.FormClosed += (s, args) => overlay.Close();

            product.setProductsObject(this);
            product.setTypeOperation("Edit");
            product.Show();
            product.TopMost = true;
        }

        public void productDelete()
        {
            btnProductEdit.Enabled = true;
            btnProductDelete.Enabled = true;

            this.product_id = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());

            Models.Product product = Shared.db.Products.Find(this.product_id);

            if (product == null)
            {
                XtraMessageBox.Show("Product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Function.Sound.Wrong();
                return;
            }

            // Vérifier si le produit est déjà archivé
            if (product.IsArchived)
            {
                // Proposer de restaurer ou supprimer définitivement
                var result = XtraMessageBox.Show(
                    $"This product is already archived.\n\n" +
                    $"Archived on: {product.ArchivedAt:yyyy-MM-dd HH:mm}\n" +
                    $"Reason: {product.ArchiveReason ?? "Not specified"}\n\n" +
                    $"Do you want to RESTORE it?\n\n" +
                    $"Click 'Yes' to restore, 'No' to cancel.",
                    "Product Already Archived",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    var restoreResult = Function.ProductArchiveManager.RestoreProduct(
                        this.product_id,
                        Properties.Settings.Default.userId,
                        Shared.db
                    );

                    if (restoreResult.success)
                    {
                        XtraMessageBox.Show(restoreResult.message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Function.Sound.Added();
                        this.loadProducts();
                    }
                    else
                    {
                        XtraMessageBox.Show(restoreResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Function.Sound.Wrong();
                    }
                }
                else
                {
                    Function.Sound.Wrong();
                }

                return;
            }

            // SÉCURITÉ: Proposer archivage au lieu de suppression définitive
            var archiveOrDelete = XtraMessageBox.Show(
                $"Do you want to ARCHIVE or DELETE this product?\n\n" +
                $"Product: {product.ProductName}\n\n" +
                $"📦 ARCHIVE (Recommended):\n" +
                $"   - Product hidden from lists\n" +
                $"   - Keeps sales history\n" +
                $"   - Can be restored later\n\n" +
                $"🗑️ DELETE (Permanent):\n" +
                $"   - Cannot be undone\n" +
                $"   - May fail if product has dependencies\n\n" +
                $"Click 'Yes' to ARCHIVE (Recommended)\n" +
                $"Click 'No' to attempt PERMANENT DELETE\n" +
                $"Click 'Cancel' to abort",
                "Archive or Delete?",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (archiveOrDelete == DialogResult.Cancel)
            {
                Function.Sound.Wrong();
                return;
            }

            if (archiveOrDelete == DialogResult.Yes)
            {
                // ARCHIVAGE (RECOMMANDÉ)
                string reason = Microsoft.VisualBasic.Interaction.InputBox(
                    "Reason for archiving (optional):",
                    "Archive Product",
                    "Discontinued / Out of stock / Obsolete",
                    -1,
                    -1
                );

                if (!string.IsNullOrWhiteSpace(reason))
                {

                    var archiveResult = Function.ProductArchiveManager.ArchiveProduct(
                        this.product_id,
                        Properties.Settings.Default.userId,
                        reason,
                        Shared.db
                    );

                    if (archiveResult.success)
                    {
                        XtraMessageBox.Show(
                            archiveResult.message + "\n\nYou can restore it later from 'Archived Products' view.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        Function.Sound.Deleted();
                        this.loadProducts();
                    }
                    else
                    {
                        XtraMessageBox.Show(archiveResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Function.Sound.Wrong();
                    }
                }
                else
                {
                    Function.Sound.Wrong();
                }
            }
            else
            {
                // SUPPRESSION DÉFINITIVE
                var canDelete = Function.ProductArchiveManager.CanPermanentlyDelete(this.product_id, Shared.db);

                if (!canDelete.canDelete)
                {
                    XtraMessageBox.Show(
                        $"❌ Cannot permanently delete this product:\n\n{canDelete.reason}\n\n" +
                        $"💡 Recommendation: Archive it instead to preserve history.",
                        "Cannot Delete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    Function.Sound.Wrong();
                    return;
                }

                // Confirmation finale pour suppression définitive
                var confirmDelete = XtraMessageBox.Show(
                    $"⚠️ PERMANENT DELETION WARNING ⚠️\n\n" +
                    $"Product: {product.ProductName}\n\n" +
                    $"This will PERMANENTLY DELETE the product.\n" +
                    $"This action CANNOT be undone.\n\n" +
                    $"Are you absolutely sure?",
                    "Confirm Permanent Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation
                );

                if (confirmDelete == DialogResult.Yes)
                {
                    var deleteResult = Function.ProductArchiveManager.PermanentlyDeleteProduct(
                        this.product_id,
                        Properties.Settings.Default.userId,
                        Shared.db
                    );

                    if (deleteResult.success)
                    {
                        XtraMessageBox.Show(deleteResult.message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Function.Sound.Deleted();
                        this.loadProducts();
                    }
                    else
                    {
                        XtraMessageBox.Show(deleteResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Function.Sound.Wrong();
                    }
                }
                else
                {
                    Function.Sound.Wrong();
                }
            }
        }

        private void btnProductRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnProductEdit.Enabled = false;
            btnProductDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadProducts();
        }

        private void btnPrintProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlProducts.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditProduct product = new AddEditProduct();
            product.setProductsObject(this);
            product.setTypeOperation("Add");
            product.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.productEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.productDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlProducts.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnProductEdit.Enabled = false;
            btnProductDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadProducts();
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
                    int totalItems = context.Products.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadProducts();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlProducts, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlProducts, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlProducts, "xlsx");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}