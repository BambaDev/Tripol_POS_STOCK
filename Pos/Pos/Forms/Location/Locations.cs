using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
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

namespace Pos.Forms.Location
{
    public partial class Locations : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        public string currentTab = "layoutControlGroupCountries";
        public int country_id = 0;
        public int state_id = 0;
        public int city_id = 0;
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

        public Locations()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewCountries.MouseUp += gridView_MouseUpCountry;
            gridViewStates.MouseUp += gridView_MouseUp;
            gridViewCities.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewCountries.RowStyle += gridViewCountries_RowStyle;
            gridViewCountries.FocusedRowChanged += gridViewCountries_FocusedRowChanged;
            gridViewCountries.CustomDrawCell += gridViewCountries_CustomDrawCell;
            gridViewCountries.RowHeight = Function.Helper.RowHeight;
            gridViewCountries.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            gridViewStates.RowStyle += gridViewStates_RowStyle;
            gridViewStates.FocusedRowChanged += gridViewStates_FocusedRowChanged;
            gridViewStates.CustomDrawCell += gridViewStates_CustomDrawCell;
            gridViewStates.RowHeight = Function.Helper.RowHeight;
            gridViewStates.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            gridViewCities.RowStyle += gridViewCities_RowStyle;
            gridViewCities.FocusedRowChanged += gridViewCities_FocusedRowChanged;
            gridViewCities.CustomDrawCell += gridViewCities_CustomDrawCell;
            gridViewCities.RowHeight = Function.Helper.RowHeight;
            gridViewCities.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCountries_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCountries.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCountries_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCountries.FocusedRowHandle && e.Column == gridViewCountries.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCountries_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewStates_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewStates.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewStates_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewStates.FocusedRowHandle && e.Column == gridViewStates.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewStates_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewCities_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCities.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCities_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCities.FocusedRowHandle && e.Column == gridViewCities.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCities_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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

        private void gridView_MouseUpCountry(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridViewCountries.RowCount > 0 && gridViewCountries.FocusedRowHandle >= 0)
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
        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
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

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            edit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Location"))
            {
                AddEditLocation addEditLocation = new AddEditLocation();
                addEditLocation.setLocationsObject(this);
                addEditLocation.setTypeOperation("Add");
                addEditLocation.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            delete();
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
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            simpleButton1.Font = customFont;
            btnDelete.Font = customFont;
            btnPrint.Font = customFont;
            btnEdit.Font = customFont;
            btnRefresh.Font = customFont;
            btnAdd.Font = customFont;
            btnCloseFrm.Font = customFont;

            layoutControlGroupCountries.AppearanceTabPage.Header.Font = customFont;
            layoutControlGroupStates.AppearanceTabPage.Header.Font = customFont;
            layoutControlGroupCities.AppearanceTabPage.Header.Font = customFont;

            foreach (Control control in this.Controls)
            {
                control.Font = customFont;
            }

            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewCountries.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }

            foreach (GridColumn column in gridViewStates.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }

            foreach (GridColumn column in gridViewCities.Columns)
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

        private void Locations_Load(object sender, EventArgs e)
        {
            loadItems(layoutControlGroupCountries.Name.ToString());
        }

        private void gridViewCountries_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewCountries.FocusedRowHandle >= 0)
            {
                this.country_id = int.Parse(gridViewCountries.GetRowCellValue(gridViewCountries.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void repEditCountry_Click(object sender, EventArgs e)
        {
            if (gridViewCountries.FocusedRowHandle >= 0)
            {
                this.country_id = int.Parse(gridViewCountries.GetRowCellValue(gridViewCountries.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void repDeleteCountry_Click(object sender, EventArgs e)
        {
            this.delete();
        }

        private void gridViewCountries_RowClick(object sender, EventArgs e)
        {
            if (gridViewCountries.FocusedRowHandle >= 0)
            {
                this.country_id = int.Parse(gridViewCountries.GetRowCellValue(gridViewCountries.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void edit()
        {
            if (Function.Permission.HasPermission("Edit Location"))
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

                if (this.country_id != 0 || this.state_id != 0 || this.city_id != 0)
                {
                    AddEditLocation addEditLocation = new AddEditLocation();
                    addEditLocation.setLocationsObject(this);
                    addEditLocation.setTypeOperation("Edit");
                    addEditLocation.ShowDialog();
                }
                else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void delete()
        {
            using (var context = new AppDbContext())
            {
                string lang = Properties.Settings.Default.Lang;

                string confirmation;
                string areYouSure;
                string anErrorOccurred;

                if (lang == "en")
                {
                    confirmation = "Confirmation";
                    areYouSure = "Are you sure want to delete Item ?";
                    anErrorOccurred = "An error occurred while deleting:";
                }
                else if (lang == "fr")
                {
                    confirmation = "Confirmation";
                    areYouSure = "Etes-vous sûr de vouloir supprimer l'élément ?";
                    anErrorOccurred = "Une erreur s'est produite lors de la suppression :";
                }
                else
                {
                    confirmation = "التأكيد";
                    areYouSure = "هل أنت متأكد من رغبتك في حذف العنصر؟";
                    anErrorOccurred = "حدث خطأ أثناء الحذف:";
                }

                if (Function.Permission.HasPermission("Delete Location"))
                {
                    int id = -1;

                    if (this.currentTab == "layoutControlGroupCountries")
                    {
                        id = int.Parse(gridViewCountries.GetRowCellValue(gridViewCountries.FocusedRowHandle, "Id").ToString());
                        Models.Country item = context.Countries.Find(id);

                        if (item != null && XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.Countries.Remove(item);
                        }
                    }
                    else if (this.currentTab == "layoutControlGroupStates")
                    {
                        id = int.Parse(gridViewStates.GetRowCellValue(gridViewStates.FocusedRowHandle, "Id").ToString());
                        Models.State item = context.States.Find(id);

                        if (item != null && XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.States.Remove(item);
                        }
                    }
                    else if (this.currentTab == "layoutControlGroupCities")
                    {
                        id = int.Parse(gridViewCities.GetRowCellValue(gridViewCities.FocusedRowHandle, "Id").ToString());
                        Models.City item = context.Cities.Find(id);

                        if (item != null && XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.Cities.Remove(item);
                        }
                    }

                    if (id != -1)
                    {
                        try
                        {
                            context.SaveChanges();
                            this.loadItems(this.currentTab);
                            Function.Sound.Deleted();

                            ShowOverlay();
                            ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                            showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                            showCustomAlert.trnsMsgSuccess();
                            showCustomAlert.ShowDialog();
                            HideOverlay();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show($"{anErrorOccurred} {ex.Message}");
                            Function.Sound.Wrong();
                        }
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Location"))
            {
                AddEditLocation addEditLocation = new AddEditLocation();
                addEditLocation.setLocationsObject(this);
                addEditLocation.setTypeOperation("Add");
                addEditLocation.ShowDialog();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.delete();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Sound.Added();

            if (this.currentTab == "layoutControlGroupCountries")
            {
                CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlCountries);
                customPrint.PrintGridControl(gridViewCountries);
            }
            else if (this.currentTab == "layoutControlGroupStates")
            {
                CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlStates);
                customPrint.PrintGridControl(gridViewStates);
            }
            else if (this.currentTab == "layoutControlGroupCities")
            {
                CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlCities);
                customPrint.PrintGridControl(gridViewCities);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Function.Sound.Selected();
            this.loadItems(this.currentTab);
        }

        public void loadItems(string tab)
        {
            using (var context = new AppDbContext())
            {
                if (tab == "layoutControlGroupCountries")
                {
                    gridControlCountries.DataSource = context.Countries.OrderByDescending(p => p.Id).ToList();
                }
                else if (tab == "layoutControlGroupStates")
                {
                    gridControlStates.DataSource = context.States.OrderByDescending(p => p.Id).ToList();
                }
                else if (tab == "layoutControlGroupCities")
                {
                    gridControlCities.DataSource = context.Cities.OrderByDescending(p => p.Id).ToList();
                }
            }
        }

        private void gridViewStates_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewStates.FocusedRowHandle >= 0)
            {
                this.state_id = int.Parse(gridViewStates.GetRowCellValue(gridViewStates.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void gridViewCities_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewStates.FocusedRowHandle >= 0)
            {
                this.city_id = int.Parse(gridViewStates.GetRowCellValue(gridViewStates.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void gridViewStates_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewStates.FocusedRowHandle >= 0)
            {
                this.state_id = int.Parse(gridViewStates.GetRowCellValue(gridViewStates.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewCities_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewCities.FocusedRowHandle >= 0)
            {
                this.city_id = int.Parse(gridViewCities.GetRowCellValue(gridViewCities.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void tabbedControlGroup_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            var selectedPage = tabbedControlGroup.SelectedTabPage;
            this.currentTab = selectedPage.Name;
            loadItems(this.currentTab);
        }

        private void repoItemDeleteCity_Click(object sender, EventArgs e)
        {
            this.delete();
        }

        private void repoItemDeleteState_Click(object sender, EventArgs e)
        {
            this.delete();
        }

        private void repoItemEditCity_Click(object sender, EventArgs e)
        {
            if (gridViewCities.FocusedRowHandle >= 0)
            {
                this.city_id = int.Parse(gridViewCities.GetRowCellValue(gridViewCities.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void repoItemEditState_Click(object sender, EventArgs e)
        {
            if (gridViewStates.FocusedRowHandle >= 0)
            {
                this.state_id = int.Parse(gridViewStates.GetRowCellValue(gridViewStates.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.edit();
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.Close();
        }
    }
}