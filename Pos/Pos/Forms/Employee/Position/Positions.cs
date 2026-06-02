using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
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
using DevExpress.XtraGrid.Columns;

namespace Pos.Forms.Employee.Position
{
    public partial class Positions : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int position_id = 0;

        public Positions()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewPositions.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewPositions.RowStyle += gridViewPositions_RowStyle;
            gridViewPositions.FocusedRowChanged += gridViewPositions_FocusedRowChanged;
            gridViewPositions.CustomDrawCell += gridViewPositions_CustomDrawCell;
            gridViewPositions.RowHeight = Function.Helper.RowHeight;
            gridViewPositions.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPositions_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPositions.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPositions_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPositions.FocusedRowHandle && e.Column == gridViewPositions.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPositions_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewPositions.RowCount > 0 && gridViewPositions.FocusedRowHandle >= 0)
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
            positionEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Position"))
            {
                AddEditPosition department = new AddEditPosition();
                department.setPositionsObject(this);
                department.setTypeOperation("Add");
                department.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            positionDelete();
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
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;
            layoutControlItem10.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewPositions.Columns)
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

        private void Positions_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadPositions();
        }

        public void loadPositions()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Positions
                .Where(p => p.Title.Contains(searchTerm))
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
                var currentPageData = context.Positions
                    .Where(p => p.Title.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlPositions.DataSource = currentPageData;
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
            if (Function.Permission.HasPermission("Add Position"))
            {
                AddEditPosition department = new AddEditPosition();
                department.setPositionsObject(this);
                department.setTypeOperation("Add");
                department.ShowDialog();
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.positionEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.positionDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewPositions.RowCount > 0 && gridViewPositions.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.position_id = int.Parse(gridViewPositions.GetRowCellValue(gridViewPositions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewPositions_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewPositions.RowCount > 0 && gridViewPositions.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.position_id = int.Parse(gridViewPositions.GetRowCellValue(gridViewPositions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.positionEdit();
            }
        }

        private void repEditPosition_Click(object sender, EventArgs e)
        {
            if (gridViewPositions.RowCount > 0 && gridViewPositions.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.position_id = int.Parse(gridViewPositions.GetRowCellValue(gridViewPositions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.positionEdit();
            }
        }

        private void repDeletePosition_Click(object sender, EventArgs e)
        {
            this.positionDelete();
        }

        private void gridViewPositions_RowClick(object sender, EventArgs e)
        {
            if (gridViewPositions.RowCount > 0 && gridViewPositions.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.position_id = int.Parse(gridViewPositions.GetRowCellValue(gridViewPositions.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void positionEdit()
        {
            if (Function.Permission.HasPermission("Edit Position"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                AddEditPosition position = new AddEditPosition();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                position.FormClosed += (s, args) => overlay.Close();

                position.setPositionsObject(this);
                position.setTypeOperation("Edit");
                position.Show();
                position.TopMost = true;
            }
        }

        public void positionDelete()
        {
            if (Function.Permission.HasPermission("Delete Position"))
            {
                if (gridViewPositions.FocusedRowHandle > 0)
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
                        this.position_id = int.Parse(gridViewPositions.GetRowCellValue(gridViewPositions.FocusedRowHandle, "Id").ToString());

                        Models.Position position = context.Positions.Find(this.position_id);

                        if (position != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.Positions.Remove(position);
                            context.SaveChanges();
                            this.loadPositions();
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

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPositions();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlPositions);
            customPrint.PrintGridControl(gridViewPositions);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Position"))
            {
                AddEditPosition department = new AddEditPosition();
                department.setTypeOperation("Add");
                department.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.positionEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.positionDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlPositions.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.loadPositions();
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
                    int totalItems = context.Positions.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadPositions();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPositions, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPositions, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlPositions, "xlsx");
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}