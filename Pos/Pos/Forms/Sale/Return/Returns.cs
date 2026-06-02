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

namespace Pos.Forms.Sale.Return
{
    public partial class Returns : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int return_id = 0;

        public Returns()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewReturns.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewReturns.RowStyle += gridViewReturns_RowStyle;
            gridViewReturns.FocusedRowChanged += gridViewReturns_FocusedRowChanged;
            gridViewReturns.CustomDrawCell += gridViewReturns_CustomDrawCell;
            gridViewReturns.RowHeight = Function.Helper.RowHeight;
            gridViewReturns.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewReturns_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewReturns.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewReturns_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewReturns.FocusedRowHandle && e.Column == gridViewReturns.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewReturns_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewReturns.RowCount > 0 && gridViewReturns.FocusedRowHandle >= 0)
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
            returnEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Sale Return"))
            {
                AddEditReturn addEditReturn = new AddEditReturn();
                addEditReturn.setReturnsObject(this);
                addEditReturn.setTypeOperation("Add");
                addEditReturn.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            returnDelete();
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
            foreach (GridColumn column in gridViewReturns.Columns)
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

        private void Returns_Load(object sender, EventArgs e)
        {
            btnReturnEdit.Enabled = false;
            btnReturnDelete.Enabled = false;

            this.loadReturns();

            txtPurchaseTotal.Text = this.getTotalAmount() + " DA";

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtSale.Properties.DataSource = Shared.db.Sales.ToList();
            txtSale.Properties.DisplayMember = "ReferenceNo"; // Set display member
            txtSale.Properties.ValueMember = "Id"; // Set value member

            txtCustomer.Properties.DataSource = Shared.db.Customers.ToList();
            txtCustomer.Properties.DisplayMember = "FirstName"; // Set display member
            txtCustomer.Properties.ValueMember = "Id"; // Set value member
        }

        public void loadReturns()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Returns
                .Include(w => w.Warehouse)
                .Include(w => w.Customer)
                .Include(w => w.Sale)
                .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Customer.FullName.Contains(searchTerm) ||
                        p.Warehouse.Name.Contains(searchTerm))
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
                var currentPageData = context.Returns.Include(w => w.Warehouse).Include(c => c.Customer)
                    .Include(w => w.Warehouse)
                    .Include(w => w.Sale)
                    .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Customer.FullName.Contains(searchTerm) ||
                        p.Warehouse.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlReturns.DataSource = currentPageData;
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

        private void btnAddReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditReturn addEditReturn = new AddEditReturn();
            addEditReturn.setReturnsObject(this);
            addEditReturn.setTypeOperation("Add");
            addEditReturn.ShowDialog();
        }

        private void btnReturnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.returnEdit();
        }

        private void btnReturnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.returnDelete();
        }

        private void gridViewReturns_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                // Check if a valid row is selected
                if (gridViewReturns.FocusedRowHandle >= 0)
                {
                    // Get the selected row's "Id" value safely
                    object selectedId = gridViewReturns.GetRowCellValue(gridViewReturns.FocusedRowHandle, "Id");

                    // Ensure the selectedId is not null and can be parsed into an integer
                    if (selectedId != null && int.TryParse(selectedId.ToString(), out int parsedId))
                    {
                        // Set the return_id to the parsed value
                        this.return_id = parsedId;

                        // Enable buttons
                        btnReturnEdit.Enabled = true;
                        btnReturnDelete.Enabled = true;

                        // Play selection sound
                        Function.Sound.Selected();

                        // Call the return edit function
                        this.returnEdit();
                    }
                    else
                    {
                        // Handle the case where the ID is invalid
                        XtraMessageBox.Show("Invalid item selected. Please try again.");
                        btnReturnEdit.Enabled = false;
                        btnReturnDelete.Enabled = false;
                    }
                }
                else
                {
                    // No valid row selected, disable the buttons
                    XtraMessageBox.Show("No item selected. Please select a row.");
                    btnReturnEdit.Enabled = false;
                    btnReturnDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions and log them if necessary
                XtraMessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void repEditAdjustment_Click(object sender, EventArgs e)
        {
            btnReturnEdit.Enabled = true;
            btnReturnDelete.Enabled = true;

            this.return_id = int.Parse(gridViewReturns.GetRowCellValue(gridViewReturns.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.returnEdit();
        }

        private void repDeleteAdjustment_Click(object sender, EventArgs e)
        {
            this.returnDelete();
        }

        private void gridViewReturns_RowClick(object sender, EventArgs e)
        {
            btnReturnEdit.Enabled = true;
            btnReturnDelete.Enabled = true;

            this.return_id = int.Parse(gridViewReturns.GetRowCellValue(gridViewReturns.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void returnEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditReturn addEditReturn = new AddEditReturn();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            addEditReturn.FormClosed += (s, args) => overlay.Close();

            addEditReturn.setReturnsObject(this);
            addEditReturn.setTypeOperation("Edit");
            addEditReturn.Show();
            addEditReturn.TopMost = true;
        }
        public void returnProduct(int productId, int warehouseId, int quantity)
        {
            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    productInWarehouse.Qty += quantity;
                    productInWarehouse.UpdatedAt = DateTime.Now;
                }

                context.SaveChanges();
            }
        }
        public void returnDelete()
        {
            // Activer les boutons d'édition et de suppression
            btnReturnEdit.Enabled = false;
            btnReturnDelete.Enabled = false;
            using (AppDbContext AppDb = new AppDbContext())
            {// Récupérer l'ID du retour sélectionné
                this.return_id = int.Parse(gridViewReturns.GetRowCellValue(gridViewReturns.FocusedRowHandle, "Id").ToString());

                // Trouver le retour correspondant à cet ID
                Models.Return _return = AppDb.Returns.Find(this.return_id);

                // Vérifier si le retour existe et afficher une boîte de confirmation
                if (_return != null && XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Récupérer tous les ProductReturns associés à ce retour
                    List<Models.ProductReturn> productReturns = AppDb.ProductReturns
                        .Where(p => p.ReturnId == this.return_id).ToList();

                    // Supprimer chaque ProductReturn associé
                    foreach (var productReturn in productReturns)
                    {
                        this.returnProduct((int)productReturn.ProductId, (int)_return.WarehouseId, (int)-productReturn.Qty);
                        AppDb.ProductReturns.Remove(productReturn);
                    }

                    // Enregistrer les modifications (suppression des ProductReturns)
                    AppDb.SaveChanges();

                    // Ensuite, supprimer le retour
                    AppDb.Returns.Remove(_return);

                    // Enregistrer les modifications (suppression du Return)
                    AppDb.SaveChanges();

                    // Recharger la liste des retours pour mettre à jour l'interface utilisateur
                    this.loadReturns();

                    // Jouer un son de confirmation de suppression
                    Function.Sound.Deleted();
                }
                else
                {
                    // Jouer un son d'erreur si l'utilisateur annule ou si le retour n'existe pas
                    Function.Sound.Wrong();
                }
            }

        }

        private void btnReturnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnReturnEdit.Enabled = false;
            btnReturnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadReturns();
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                gridControlReturns.DataSource = Shared.db.Returns.Where(w => w.WarehouseId == int.Parse(txtWarehouse.EditValue.ToString())).Include(w => w.Warehouse).Include(c => c.Customer).ToList();
            }

            Sound.Selected();
        }

        private void txtSale_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSale.EditValue != null)
            {
                gridControlReturns.DataSource = Shared.db.Returns.Where(w => w.SaleId == int.Parse(txtSale.EditValue.ToString())).Include(w => w.Warehouse).Include(c => c.Customer).ToList();
            }

            Sound.Selected();
        }

        private void txtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCustomer.EditValue != null)
            {
                gridControlReturns.DataSource = Shared.db.Returns.Where(w => w.CustomerId == int.Parse(txtCustomer.EditValue.ToString())).Include(w => w.Warehouse).Include(c => c.Customer).ToList();
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                if (txtSortBy.Text == "Newest")
                {
                    gridControlReturns.DataSource = Shared.db.Returns.OrderByDescending(p => p.Id).Include(w => w.Warehouse).Include(c => c.Customer).ToList();
                }
                else
                {
                    gridControlReturns.DataSource = Shared.db.Returns.Include(w => w.Warehouse).Include(c => c.Customer).ToList();
                }
            }

            Sound.Selected();
        }

        private void btnQuickAdjustment_Click(object sender, EventArgs e)
        {
            Return.AddEditReturn addEditAdjustment = new Return.AddEditReturn();
            addEditAdjustment.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            DateTime today = DateTime.Today;

            var totalAmount = Shared.db.Returns
                //.Where(p => p.PurchaseDate == today)
                .Sum(p => p.GrandTotal);

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
            this.loadReturns();
            Sound.Added();
        }

        private void btnPrintReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlReturns.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditReturn addEditReturn = new AddEditReturn();
            addEditReturn.setReturnsObject(this);
            addEditReturn.setTypeOperation("Add");
            addEditReturn.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.returnEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.returnDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlReturns.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            txtWarehouse.Text = string.Empty;
            this.loadReturns();
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
                    int totalItems = context.Returns.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadReturns();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlReturns, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlReturns, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlReturns, "xlsx");
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}