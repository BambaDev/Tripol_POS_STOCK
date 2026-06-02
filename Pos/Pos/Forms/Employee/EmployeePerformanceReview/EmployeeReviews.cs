using DevExpress.CodeParser.VB.Preprocessor;
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
using Stripe;
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
using Pos.Forms.Alert;

namespace Pos.Forms.Employee.EmployeePerformanceReview
{
    public partial class EmployeeReviews : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int review_id = 0;

        public EmployeeReviews()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewEmployeeReviews.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewEmployeeReviews.RowStyle += gridViewEmployeeReviews_RowStyle;
            gridViewEmployeeReviews.FocusedRowChanged += gridViewEmployeeReviews_FocusedRowChanged;
            gridViewEmployeeReviews.CustomDrawCell += gridViewEmployeeReviews_CustomDrawCell;
            gridViewEmployeeReviews.RowHeight = Function.Helper.RowHeight;
            gridViewEmployeeReviews.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewEmployeeReviews_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewEmployeeReviews.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewEmployeeReviews_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewEmployeeReviews.FocusedRowHandle && e.Column == gridViewEmployeeReviews.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewEmployeeReviews_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
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
            reviewEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee Review"))
            {
                AddEditEmployeeReview review = new AddEditEmployeeReview();
                review.setEmployeeReviewsObject(this);
                review.setTypeOperation("Add");
                review.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            reviewDelete();
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

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewEmployeeReviews.Columns)
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

        private void EmployeeReviews_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadReviews();
        }

        public void loadReviews()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.EmployeeReviews
                .Where(p => p.Comment.Contains(searchTerm))
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
                var currentPageData = context.EmployeeReviews
                    .Where(p => p.Comment.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlEmployeeReviews.DataSource = currentPageData;
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
            if (Function.Permission.HasPermission("Add Employee Review"))
            {
                AddEditEmployeeReview review = new AddEditEmployeeReview();
                review.setEmployeeReviewsObject(this);
                review.setTypeOperation("Add");
                review.ShowDialog();
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.reviewEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.reviewDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.review_id = int.Parse(gridViewEmployeeReviews.GetRowCellValue(gridViewEmployeeReviews.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewEmployeeReviews_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.review_id = int.Parse(gridViewEmployeeReviews.GetRowCellValue(gridViewEmployeeReviews.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.reviewEdit();
            }
        }

        private void repEditEmployeeReviews_Click(object sender, EventArgs e)
        {
            if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
            {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    this.review_id = int.Parse(gridViewEmployeeReviews.GetRowCellValue(gridViewEmployeeReviews.FocusedRowHandle, "Id").ToString());
                    Function.Sound.Selected();
                    this.reviewEdit();
            }
        }

        private void repDeleteEmployeeReviews_Click(object sender, EventArgs e)
        {
            this.reviewDelete();
        }

        private void gridViewEmployeeReviews_RowClick(object sender, EventArgs e)
        {
            if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.review_id = int.Parse(gridViewEmployeeReviews.GetRowCellValue(gridViewEmployeeReviews.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void reviewEdit()
        {
            if (Function.Permission.HasPermission("Edit Employee Review"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                AddEditEmployeeReview review = new AddEditEmployeeReview();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                review.FormClosed += (s, args) => overlay.Close();

                review.setEmployeeReviewsObject(this);
                review.setTypeOperation("Edit");
                review.Show();
                review.TopMost = true;
            }
        }

        public void reviewDelete()
        {
            if (gridViewEmployeeReviews.RowCount > 0 && gridViewEmployeeReviews.FocusedRowHandle >= 0)
            {
                using (var context = new AppDbContext())
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

                    this.review_id = int.Parse(gridViewEmployeeReviews.GetRowCellValue(gridViewEmployeeReviews.FocusedRowHandle, "Id").ToString());

                    Models.EmployeeReview review = context.EmployeeReviews.Find(this.review_id);

                    if (review != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.EmployeeReviews.Remove(review);
                        context.SaveChanges();
                        this.loadReviews();
                        Function.Sound.Deleted();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadReviews();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlEmployeeReviews.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee Review"))
            {
                AddEditEmployeeReview review = new AddEditEmployeeReview();
                review.setEmployeeReviewsObject(this);
                review.setTypeOperation("Add");
                review.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a review to update";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner une revue pour la mise à jour.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد مراجعة للتحديث";
            }

            if ( this.review_id != 0)
            {
                this.reviewEdit();
                
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
                pleaseSelect = "Please select a review to delete";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner une revue pour a supprimer.";
            }
            else
            {
                pleaseSelect = "قم باختيار مراجعة للحذف";
            }

            if (this.review_id != 0)
            {
                this.reviewDelete();
                this.review_id = 0;
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }
          
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlEmployeeReviews);
            customPrint.PrintGridControl(gridViewEmployeeReviews);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.loadReviews();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
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

        private void perPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.EmployeeReviews.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadReviews();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployeeReviews, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployeeReviews, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployeeReviews, "xlsx");
        }
    }
}