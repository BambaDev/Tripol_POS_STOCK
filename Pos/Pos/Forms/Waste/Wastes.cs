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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Columns;
using Pos.Forms.Alert;
using Pos.Forms.Printer;

namespace Pos.Forms.Waste
{
    public partial class Wastes : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int waste_id = 0;

        public Wastes()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewWasteItems.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewWasteItems.RowStyle += gridViewWasteItems_RowStyle;
            gridViewWasteItems.FocusedRowChanged += gridViewWasteItems_FocusedRowChanged;
            gridViewWasteItems.CustomDrawCell += gridViewWasteItems_CustomDrawCell;
            gridViewWasteItems.RowHeight = Function.Helper.RowHeight;
            gridViewWasteItems.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewWasteItems_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewWasteItems.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewWasteItems_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewWasteItems.FocusedRowHandle && e.Column == gridViewWasteItems.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewWasteItems_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewWasteItems.RowCount > 0 && gridViewWasteItems.FocusedRowHandle >= 0)
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
            wasteEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Waste"))
            {
                AddEditWaste waste = new AddEditWaste();
                waste.setWastesObject(this);
                waste.setTypeOperation("Add");
                waste.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            wasteDelete();
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
            btnCloseFrm.Font = customFont;
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
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewWasteItems.Columns)
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

        private void Wastes_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            txtTotal.Text = Function.Helper.FormatAmount(this.getTotalAmount().ToString());

            this.loadWastes();
            this.getBusinessLocations();
            this.getEmployees();
        }

        public void getBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBusinessLocation.Properties.DataSource = context.BusinessLocations.ToList();
                txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
                txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getEmployees()
        {
            using (var context = new AppDbContext())
            {
                txtEmployee.Properties.DataSource = context.Employees.ToList();
                txtEmployee.Properties.DisplayMember = "FirstName"; // Set display member
                txtEmployee.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void loadWastes()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Wastes
                .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Note.Contains(searchTerm))
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
                var currentPageData = context.Wastes.Include(u => u.User).Include(b => b.BusinessLocation)
                    .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                                p.Note.Contains(searchTerm))
                    .Include(p => p.WasteItems)
                    .Include(p => p.Employee)
                    .Include(p => p.BusinessLocation)
                    .Include(p => p.User)
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                // Transform the data into the view model
                var wasteViewModels = currentPageData.Select(w => new WasteViewModel
                {
                    Id = w.Id,
                    ReferenceNo = w.ReferenceNo,
                    Date = w.Date,
                    TotalLoss = w.TotalLoss,
                    Note = w.Note,
                    Items = w.Items,
                    EmployeeName = w.Employee.FirstName,
                    BusinessLocationName = w.BusinessLocation.Name,
                    UserName = w.User.FullName,
                  
                    WasteItems = w.WasteItems.Select(wi => new WasteItemViewModel
                    {
                        ItemName = wi.ItemName,
                        WasteAmount = wi.WasteAmount,
                        LastPurchasePrice = wi.LastPurchasePrice,
                        LossAmount = wi.LossAmount,
                        Qty = wi.Qty
                    }).ToList()
                }).ToList();

                gridControlWasteItems.DataSource = wasteViewModels;
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

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Waste"))
            {
                AddEditWaste waste = new AddEditWaste();
                waste.setWastesObject(this);
                waste.setTypeOperation("Add");
                waste.ShowDialog();
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.wasteEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.wasteDelete();
        }

        private void gridViewWasteItems_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewWasteItems.RowCount > 0 && gridViewWasteItems.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.waste_id = int.Parse(gridViewWasteItems.GetRowCellValue(gridViewWasteItems.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.wasteEdit();
            }
        }

        private void repEditWasteItem_Click(object sender, EventArgs e)
        {
            if (gridViewWasteItems.RowCount > 0 && gridViewWasteItems.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.waste_id = int.Parse(gridViewWasteItems.GetRowCellValue(gridViewWasteItems.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.wasteEdit();
            }
        }

        private void repDeleteWasteItem_Click(object sender, EventArgs e)
        {
            this.wasteDelete();
        }

        private void gridViewWasteItems_RowClick(object sender, EventArgs e)
        {
            if (gridViewWasteItems.RowCount > 0 && gridViewWasteItems.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.waste_id = int.Parse(gridViewWasteItems.GetRowCellValue(gridViewWasteItems.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void wasteEdit()
        {
            if (Function.Permission.HasPermission("Edit Waste"))
            {
                string lang = Properties.Settings.Default.Lang;

                string pleaseSelectItem;

                if (lang == "en")
                {
                    pleaseSelectItem = "Please select item !";
                }
                else if (lang == "fr")
                {
                    pleaseSelectItem = "Veuillez sélectionner l'article !";
                }
                else
                {
                    pleaseSelectItem = "الرجاء تحديد العنصر!";
                }

                if (this.waste_id != 0)
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    AddEditWaste waste = new AddEditWaste();

                    // This ensures the overlay form is displayed behind the modal form but above the parent form
                    waste.FormClosed += (s, args) => overlay.Close();

                    waste.setWastesObject(this);
                    waste.setTypeOperation("Edit");
                    waste.Show();
                    waste.TopMost = true;
                }
                else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void wasteDelete()
        {
            if (Function.Permission.HasPermission("Delete Waste"))
            {
                if (gridViewWasteItems.RowCount > 0 && gridViewWasteItems.FocusedRowHandle >= 0)
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    string lang = Properties.Settings.Default.Lang;

                    string confirmation;
                    string areYouSure;

                    if (lang == "en")
                    {
                        confirmation = "Confirmation";
                        areYouSure = "Are you sure want to delete Item ?";
                    }
                    else if (lang == "fr")
                    {
                        confirmation = "Confirmation";
                        areYouSure = "Etes-vous sûr de vouloir supprimer l'élément ?";
                    }
                    else
                    {
                        confirmation = "التأكيد";
                        areYouSure = "هل أنت متأكد من رغبتك في حذف العنصر؟";
                    }

                    using (var context = new AppDbContext())
                    {
                        this.waste_id = int.Parse(gridViewWasteItems.GetRowCellValue(gridViewWasteItems.FocusedRowHandle, "Id").ToString());

                        Models.Waste waste = context.Wastes.Include(w => w.WasteItems).FirstOrDefault(w => w.Id == this.waste_id);

                        if (waste != null && XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            // Supprimer tous les WasteItems associés
                            if (waste.WasteItems.Any())
                            {
                                context.WasteItems.RemoveRange(waste.WasteItems);
                            }

                            // Supprimer le Waste
                            context.Wastes.Remove(waste);
                            context.SaveChanges();

                            this.loadWastes();
                            Function.Sound.Deleted();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                        }
                    }
                }
            }
        }
        private void txtBusinessLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBusinessLocation.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    gridControlWasteItems.DataSource = context.Wastes.Where(w => w.BusinessLocationId == int.Parse(txtBusinessLocation.EditValue.ToString()))
                        .Include(u=>u.User).Include(b=>b.BusinessLocation)
                        .ToList();
                }
            }

            Sound.Selected();
        }

        private void txtEmployee_EditValueChanged(object sender, EventArgs e)
        {
            if (txtEmployee.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    gridControlWasteItems.DataSource = context.Wastes.Where(w => w.EmployeeId == int.Parse(txtEmployee.EditValue.ToString()))
                        .Include(u => u.User).Include(b => b.BusinessLocation).ToList();
                }
            }

            Sound.Selected();
        }

        private void txtSortBy_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSortBy.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    if (txtSortBy.Text == "Newest")
                    {
                        gridControlWasteItems.DataSource = context.Wastes.OrderByDescending(p => p.Id)
                            .Include(u => u.User).Include(b => b.BusinessLocation).ToList();
                    }
                    else
                    {
                        gridControlWasteItems.DataSource = context.Wastes.Include(u => u.User).Include(b => b.BusinessLocation).ToList();
                    }
                }
            }

            Sound.Selected();
        }

        private void btnQuickWaste_Click(object sender, EventArgs e)
        {
            Waste.AddEditWaste addEditWaste = new Waste.AddEditWaste();
            addEditWaste.ShowDialog();
            Sound.Selected();
        }

        public decimal getTotalAmount()
        {
            using (var context = new AppDbContext())
            {
                DateTime today = DateTime.Today;

                var totalAmount = context.Wastes
                    .Where(p => p.Date == DateOnly.FromDateTime(today))
                    .Sum(p => p.TotalLoss);

                return decimal.Parse(totalAmount.ToString());
            }
        }

        private void btnPurchaseHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            // do somthing
        }

        private void btnRefresh_ItemClick(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            this.loadWastes();
            Sound.Added();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlWasteItems.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Waste"))
            {
                AddEditWaste waste = new AddEditWaste();
                waste.setTypeOperation("Add");
                waste.setWastesObject(this);
                waste.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.wasteEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.wasteDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlWasteItems);
            customPrint.PrintGridControl(gridViewWasteItems);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            txtSortBy.Text = string.Empty;
            this.loadWastes();
            Sound.Added();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void txtTotal_Click(object sender, EventArgs e)
        {

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

        private void perPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.Wastes.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadWastes();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlWasteItems, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlWasteItems, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlWasteItems, "xlsx");
        }
    }

    public class WasteViewModel
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public DateOnly? Date { get; set; }
        public decimal? TotalLoss { get; set; }
        public string Note { get; set; }
        public int? Items { get; set; }
        public string EmployeeName { get; set; }
        public string BusinessLocationName { get; set; }
        public string UserName { get; set; }
        public List<WasteItemViewModel> WasteItems { get; set; }
    }

    public class WasteItemViewModel
    {
        public string ItemName { get; set; }
        public decimal? WasteAmount { get; set; }
        public decimal? LastPurchasePrice { get; set; }
        public decimal LossAmount { get; set; }
        public decimal? Qty { get; set; }
    }

}