using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
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

namespace Pos.Forms.Permission
{
    public partial class Permissions : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int permission_id = 0;
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

        public Permissions()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewPermissions.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewPermissions.RowStyle += gridViewPermissions_RowStyle;
            gridViewPermissions.FocusedRowChanged += gridViewPermissions_FocusedRowChanged;
            gridViewPermissions.CustomDrawCell += gridViewPermissions_CustomDrawCell;
            gridViewPermissions.RowHeight = Function.Helper.RowHeight;
            gridViewPermissions.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPermissions_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPermissions.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPermissions_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPermissions.FocusedRowHandle && e.Column == gridViewPermissions.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPermissions_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewPermissions.RowCount > 0 && gridViewPermissions.FocusedRowHandle >= 0)
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
            permissionEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Permission"))
            {
                AddEditPermission permission = new AddEditPermission();
                permission.setPermissionsObject(this);
                permission.setTypeOperation("Add");
                permission.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            permissionDelete();
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
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewPermissions.Columns)
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
            ribbonPermissions.Visible = show;
        }

        private void Permissions_Load(object sender, EventArgs e)
        {
            btnPermissionEdit.Enabled = false;
            btnPermissionDelete.Enabled = false;
            this.loadPermissions();
        }

        public void loadPermissions()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Permissions
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
                var currentPageData = context.Permissions
                    .Where(p => p.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlPermissions.DataSource = currentPageData;
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

        public void permissionEdit()
        {
            if (Function.Permission.HasPermission("Edit Permission"))
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

                if (this.permission_id != 0)
                {
                    AddEditPermission permission = new AddEditPermission();
                    permission.setPermissionsObject(this);
                    permission.setTypeOperation("Edit");
                    permission.ShowDialog();
                }
                else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void permissionDelete()
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

            if (Function.Permission.HasPermission("Delete Permission"))
            {
                using (var context = new AppDbContext())
                {
                    btnPermissionEdit.Enabled = true;
                    btnPermissionDelete.Enabled = true;

                    this.permission_id = int.Parse(gridViewPermissions.GetRowCellValue(gridViewPermissions.FocusedRowHandle, "Id").ToString());

                    Models.Permission permission = context.Permissions.Find(this.permission_id);

                    if (permission != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.Permissions.Remove(permission);
                        context.SaveChanges();
                        this.loadPermissions();
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

        private void gridViewPermissions_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewPermissions.RowCount > 0 && gridViewPermissions.FocusedRowHandle >= 0)
            {
                btnPermissionEdit.Enabled = true;
                btnPermissionDelete.Enabled = true;

                this.permission_id = int.Parse(gridViewPermissions.GetRowCellValue(gridViewPermissions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.permissionEdit();
            }
        }

        private void gridViewPermissions_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewPermissions.RowCount > 0 && gridViewPermissions.FocusedRowHandle >= 0)
            {
                btnPermissionEdit.Enabled = true;
                btnPermissionDelete.Enabled = true;

                this.permission_id = int.Parse(gridViewPermissions.GetRowCellValue(gridViewPermissions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void btnAddPermission_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditPermission permission = new AddEditPermission();
            permission.setPermissionsObject(this);
            permission.setTypeOperation("Add");
            permission.ShowDialog();
        }

        private void btnPermissionEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.permissionEdit();
        }

        private void btnPermissionDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.permissionDelete();
        }

        private void btnPermissionRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnPermissionEdit.Enabled = false;
            btnPermissionDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPermissions();
        }

        private void repDeletePermission_Click(object sender, EventArgs e)
        {
            this.permissionDelete();
        }

        private void repEditPermission_Click(object sender, EventArgs e)
        {
            if (gridViewPermissions.RowCount > 0 && gridViewPermissions.FocusedRowHandle >= 0)
            {
                btnPermissionEdit.Enabled = true;
                btnPermissionDelete.Enabled = true;

                this.permission_id = int.Parse(gridViewPermissions.GetRowCellValue(gridViewPermissions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.permissionEdit();
            }
        }

        private void btnPrintPermissions_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlPermissions.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Permission"))
            {
                AddEditPermission permission = new AddEditPermission();
                permission.setPermissionsObject(this);
                permission.setTypeOperation("Add");
                permission.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.permissionEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.permissionDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlPermissions);
            customPrint.PrintGridControl(gridViewPermissions);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnPermissionEdit.Enabled = false;
            btnPermissionDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPermissions();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetPermissions_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string success;
            string error;
            string anErrorOccurred;
            string msgPermissions;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to reset permissions ?";
                success = "Success";
                error = "Error";
                msgPermissions = "Permissions have been reset and new permissions have been added successfully.";
                anErrorOccurred = "An error occurred while resetting and adding permissions:";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir réinitialiser les autorisations ?";
                success = "Succès";
                error = "Erreur";
                msgPermissions = "Les autorisations ont été réinitialisées et de nouvelles autorisations ont été ajoutées avec succès.";
                anErrorOccurred = "Une erreur s'est produite lors de la réinitialisation et de l'ajout d'autorisations :";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في إعادة تعيين الأذونات؟";
                success = "النجاح";
                error = "خطأ";
                msgPermissions = "تمت إعادة تعيين الأذونات وتمت إضافة أذونات جديدة بنجاح.";
                anErrorOccurred = "حدث خطأ أثناء إعادة تعيين الأذونات وإضافتها:";
            }

            if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                        // Remove all RoleHasPermission entries
                        var rolePermissions = context.RoleHasPermissions.ToList();
                        if (rolePermissions.Any())
                        {
                            context.RoleHasPermissions.RemoveRange(rolePermissions);
                            context.SaveChanges();
                        }

                        // Remove all permissions
                        var permissions = context.Permissions.ToList();
                        if (permissions.Any())
                        {
                            context.Permissions.RemoveRange(permissions);
                            context.SaveChanges();
                        }

                        // Insert predefined permissions
                        var predefinedPermissions = ListPermission.GetPredefinedPermissions();
                        context.Permissions.AddRange(predefinedPermissions);
                        context.SaveChanges();

                        ShowOverlay();
                        ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                        showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                        showCustomAlert.trnsMsgSuccess();
                        showCustomAlert.ShowDialog();
                        HideOverlay();
                    }

                    Sound.Selected();
                    MessageBox.Show(msgPermissions, success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{anErrorOccurred} {ex.Message}", error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Sound.Wrong();
            }
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
                    int totalItems = context.Permissions.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            loadPermissions();
        }
    }
}