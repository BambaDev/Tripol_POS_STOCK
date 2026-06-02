using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Forms.Printer;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.BusinessLocation
{
    public partial class BusinessLocations : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 40;
        private string searchTerm = string.Empty;
        public int business_location_id = 0;
        private OverlayForm overlay;

        private void ShowOverlay()
        {
            if (overlay == null)
            {
                overlay = new OverlayForm(this);
                overlay.Show();
            }
        }

        private void HideOverlay()
        {
            if (overlay != null)
            {
                overlay.Close();
                overlay.Dispose();
                overlay = null;
            }
        }

        public BusinessLocations()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewbusinessLocations.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewbusinessLocations.RowStyle += gridViewbusinessLocations_RowStyle;
            gridViewbusinessLocations.FocusedRowChanged += gridViewbusinessLocations_FocusedRowChanged;
            gridViewbusinessLocations.CustomDrawCell += gridViewbusinessLocations_CustomDrawCell;
            gridViewbusinessLocations.RowHeight = Function.Helper.RowHeight;
            gridViewbusinessLocations.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewbusinessLocations_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewbusinessLocations.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewbusinessLocations_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewbusinessLocations.FocusedRowHandle && e.Column == gridViewbusinessLocations.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewbusinessLocations_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewbusinessLocations.RowCount > 0 && gridViewbusinessLocations.FocusedRowHandle >= 0)
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
            businessLocationEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Business Location"))
            {
                AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation();
                addEditBusinessLocation.setBusinessLocationsObject(this);
                addEditBusinessLocation.setTypeOperation("Add");
                addEditBusinessLocation.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            businessLocationDelete();
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

            btnCloseFrm.Font = customFont;
            btnRefreshItems.Font = customFont;
            btnDeleteItem.Font = customFont;
            btnPrintItems.Font = customFont;
            btnEditItem.Font = customFont;
            btnAddItem.Font = customFont;
            simpleButton1.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewbusinessLocations.Columns)
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

        public void ShowRibbon(bool show = false)
        {
            ribbonBussinessLocation.Visible = show;
        }

        private void BusinessLocations_Load(object sender, EventArgs e)
        {
            btnBusinessLocationEdit.Enabled = false;
            btnBusinessLocationDelete.Enabled = false;
            this.loadBusinessLocations();
        }

        public void loadBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.BusinessLocations
                .Where(p => p.Name.Contains(searchTerm) ||
                        p.LocationId.Contains(searchTerm) ||
                        p.Landmark.Contains(searchTerm) ||
                        p.City.Contains(searchTerm) ||
                        p.ZipCode.Contains(searchTerm) ||
                        p.State.Contains(searchTerm) ||
                        p.Country.Contains(searchTerm) ||
                        p.Mobile.Contains(searchTerm) ||
                        p.AlternateContactNumber.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm) ||
                        p.Website.Contains(searchTerm))
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
                var currentPageData = context.BusinessLocations
                    .Where(p => p.Name.Contains(searchTerm) ||
                        p.LocationId.Contains(searchTerm) ||
                        p.Landmark.Contains(searchTerm) ||
                        p.City.Contains(searchTerm) ||
                        p.ZipCode.Contains(searchTerm) ||
                        p.State.Contains(searchTerm) ||
                        p.Country.Contains(searchTerm) ||
                        p.Mobile.Contains(searchTerm) ||
                        p.AlternateContactNumber.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm) ||
                        p.Website.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlbusinessLocations.DataSource = currentPageData;
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

        private void btnAddBusinessLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation();
            addEditBusinessLocation.setBusinessLocationsObject(this);
            addEditBusinessLocation.setTypeOperation("Add");
            addEditBusinessLocation.ShowDialog();
        }

        private void btnBusinessLocationEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.businessLocationEdit();
        }

        private void btnBusinessLocationDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.businessLocationDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewbusinessLocations.RowCount > 0 && gridViewbusinessLocations.FocusedRowHandle >= 0)
            {
                btnBusinessLocationEdit.Enabled = true;
                btnBusinessLocationDelete.Enabled = true;

                this.business_location_id = int.Parse(gridViewbusinessLocations.GetRowCellValue(gridViewbusinessLocations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewBrands_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewbusinessLocations.RowCount > 0 && gridViewbusinessLocations.FocusedRowHandle >= 0)
            {
                btnBusinessLocationEdit.Enabled = true;
                btnBusinessLocationDelete.Enabled = true;

                this.business_location_id = int.Parse(gridViewbusinessLocations.GetRowCellValue(gridViewbusinessLocations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.businessLocationEdit();
            }
        }

        private void repEditBusinessLocation_Click(object sender, EventArgs e)
        {
            if (gridViewbusinessLocations.RowCount > 0 && gridViewbusinessLocations.FocusedRowHandle >= 0)
            {
                btnBusinessLocationEdit.Enabled = true;
                btnBusinessLocationDelete.Enabled = true;

                this.business_location_id = int.Parse(gridViewbusinessLocations.GetRowCellValue(gridViewbusinessLocations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.businessLocationEdit();
            }
        }

        private void repDeleteBusinessLocation_Click(object sender, EventArgs e)
        {
            this.businessLocationDelete();
        }

        private void gridViewBrands_RowClick(object sender, EventArgs e)
        {
            if (gridViewbusinessLocations.RowCount > 0 && gridViewbusinessLocations.FocusedRowHandle >= 0)
            {
                btnBusinessLocationEdit.Enabled = true;
                btnBusinessLocationDelete.Enabled = true;

                this.business_location_id = int.Parse(gridViewbusinessLocations.GetRowCellValue(gridViewbusinessLocations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void businessLocationEdit()
        {
            if (Function.Permission.HasPermission("Edit Business Location"))
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

                if (this.business_location_id != 0)
                {
                    AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation();
                    addEditBusinessLocation.setBusinessLocationsObject(this);
                    addEditBusinessLocation.setTypeOperation("Edit");
                    addEditBusinessLocation.ShowDialog();
                }
                else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void businessLocationDelete()
        {
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

            if (Function.Permission.HasPermission("Delete Business Location"))
            {
                using (var context = new AppDbContext())
                {
                    btnBusinessLocationEdit.Enabled = true;
                    btnBusinessLocationDelete.Enabled = true;

                    this.business_location_id = int.Parse(gridViewbusinessLocations.GetRowCellValue(gridViewbusinessLocations.FocusedRowHandle, "Id").ToString());

                    Models.BusinessLocation business_location = context.BusinessLocations.Find(this.business_location_id);

                    if (business_location != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.BusinessLocations.Remove(business_location);
                        context.SaveChanges();
                        this.loadBusinessLocations();
                        Function.Sound.Deleted();

                        ShowOverlay();
                        ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                        showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                        showCustomAlert.trnsMsgSuccess();
                        showCustomAlert.ShowDialog();
                        HideOverlay();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                }
            }
        }

        private void btnBusinessLocationRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnBusinessLocationEdit.Enabled = false;
            btnBusinessLocationDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadBusinessLocations();
        }

        private void btnPrintBussLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlbusinessLocations.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Business Location"))
            {
                AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation();
                addEditBusinessLocation.setBusinessLocationsObject(this);
                addEditBusinessLocation.setTypeOperation("Add");
                addEditBusinessLocation.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a Business Location to update.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un emplacement de travaille pour la mise à jour";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد موقع العمل للتحديث";
            }

            if (this.business_location_id != 0)
            {
                this.businessLocationEdit();
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }
        
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a Business Location to delete.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un emplacement de travaille a supprimer ";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد موقع العمل لحذفه";
            }

            if (this.business_location_id != 0)
            {
                this.businessLocationDelete();
                this.business_location_id = 0;
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }
           
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlbusinessLocations);
            customPrint.PrintGridControl(gridViewbusinessLocations);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnBusinessLocationEdit.Enabled = false;
            btnBusinessLocationDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadBusinessLocations();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            Sound.Selected();
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

        private void perPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.BusinessLocations.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadBusinessLocations();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlbusinessLocations, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlbusinessLocations, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlbusinessLocations, "xlsx");
        }
    }
}