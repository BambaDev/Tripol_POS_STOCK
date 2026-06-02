using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Drawing;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Alert;
using Pos.Forms.Expense.Category;
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
using Microsoft.EntityFrameworkCore;

namespace Pos.Forms.Expense
{
    public partial class Expenses : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int expense_id = 0;
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

        public Expenses()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewExpenses.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewExpenses.RowStyle += gridViewExpenses_RowStyle;
            gridViewExpenses.FocusedRowChanged += gridViewExpenses_FocusedRowChanged;
            gridViewExpenses.CustomDrawCell += gridViewExpenses_CustomDrawCell;
            gridViewExpenses.RowHeight = Function.Helper.RowHeight;
            gridViewExpenses.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewExpenses_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewExpenses.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewExpenses_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewExpenses.FocusedRowHandle && e.Column == gridViewExpenses.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewExpenses_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
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
            expenseEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense"))
            {
                AddEditExpense addEditExpense = new AddEditExpense();
                addEditExpense.setExpensesObject(this);
                addEditExpense.setTypeOperation("Add");
                addEditExpense.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            expenseDelete();
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
            simpleLabelItem2.AppearanceItemCaption.Font = customFont;
            simpleLabelItem6.AppearanceItemCaption.Font = customFont;
            simpleLabelItem7.AppearanceItemCaption.Font = customFont;
            simpleLabelItem8.AppearanceItemCaption.Font = customFont;
            simpleLabelItem9.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            layoutControlItem19.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewExpenses.Columns)
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
            ribbonExpenses.Visible = show;
        }

        private void Expenses_Load(object sender, EventArgs e)
        {
            btnExpenseEdit.Enabled = false;
            btnExpenseDelete.Enabled = false;
            this.loadExpenses();
            this.switchBtns();
        }

        public void switchBtns()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);
            var sameDayLastYear = today.AddYears(-1);

            using (var context = new AppDbContext())
            {
                btnTotalLastMonth.Text = Function.Helper.FormatAmount(
                    context.Expenses
                    .Where(c => c.CreatedAt.Value.Date >= startOfLastMonth && c.CreatedAt.Value.Date <= endOfLastMonth)
                    .Sum(p => p.Amount)
                    .ToString()
                );

                btnTotalOfTheMonth.Text = Function.Helper.FormatAmount(
                    context.Expenses
                    .Where(c => c.CreatedAt.Value.Date >= startOfMonth && c.CreatedAt.Value.Date < endOfMonth)
                    .Sum(p => p.Amount)
                    .ToString()
                );

                btnTotalOfTheWeek.Text = Function.Helper.FormatAmount(
                    context.Expenses
                    .Where(c => c.CreatedAt.Value.Date >= startOfWeek && c.CreatedAt.Value.Date < endOfWeek)
                    .Sum(p => p.Amount)
                    .ToString()
                );

                btnTodayTotal.Text = Function.Helper.FormatAmount(
                    context.Expenses
                    .Where(c => c.CreatedAt.Value.Date == DateTime.Today.Date)
                    .Sum(p => p.Amount)
                    .ToString()
                );

                btnLastYearTotal.Text = Function.Helper.FormatAmount(
                    context.Expenses
                    .Where(c => c.CreatedAt.Value.Date == sameDayLastYear)
                    .Sum(p => p.Amount)
                    .ToString()
                );
            }
        }

        public void loadExpenses()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Expenses
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
                var currentPageData = context.Expenses.Include(x=>x.ExpenseCategory).Include(w=>w.Warehouse)
                    .Where(p => p.ReferenceNo.Contains(searchTerm) ||
                        p.Note.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlExpenses.DataSource = currentPageData;
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

        private void btnAddExpense_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense"))
            {
                AddEditExpense addEditExpense = new AddEditExpense();
                addEditExpense.setExpensesObject(this);
                addEditExpense.setTypeOperation("Add");
                addEditExpense.ShowDialog();
            }
        }

        private void btnExpenseEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.expenseEdit();
        }

        private void btnExpenseDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.expenseDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
            {
                btnExpenseEdit.Enabled = true;
                btnExpenseDelete.Enabled = true;

                this.expense_id = int.Parse(gridViewExpenses.GetRowCellValue(gridViewExpenses.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewExpenses_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
            {
                btnExpenseEdit.Enabled = true;
                btnExpenseDelete.Enabled = true;

                this.expense_id = int.Parse(gridViewExpenses.GetRowCellValue(gridViewExpenses.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.expenseEdit();
            }
        }

        private void repEditExpense_Click(object sender, EventArgs e)
        {
            if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
            {
                btnExpenseEdit.Enabled = true;
                btnExpenseDelete.Enabled = true;

                this.expense_id = int.Parse(gridViewExpenses.GetRowCellValue(gridViewExpenses.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.expenseEdit();
            }
        }

        private void repDeleteExpense_Click(object sender, EventArgs e)
        {
            this.expenseDelete();
        }

        private void gridViewExpenses_RowClick(object sender, EventArgs e)
        {
            if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
            {
                btnExpenseEdit.Enabled = true;
                btnExpenseDelete.Enabled = true;

                this.expense_id = int.Parse(gridViewExpenses.GetRowCellValue(gridViewExpenses.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void expenseEdit()
        {
            if (Function.Permission.HasPermission("Edit Expense"))
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

                if (this.expense_id != 0)
                {
                    AddEditExpense expense = new AddEditExpense();
                    expense.setExpensesObject(this);
                    expense.setTypeOperation("Edit");
                    expense.ShowDialog();
                }
                else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void expenseDelete()
        {
            if (Function.Permission.HasPermission("Delete Expense"))
            {
                if (gridViewExpenses.RowCount > 0 && gridViewExpenses.FocusedRowHandle >= 0)
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

                    if (Function.Permission.HasPermission("Delete Expense"))
                    {
                        using (var context = new AppDbContext())
                        {
                            btnExpenseEdit.Enabled = true;
                            btnExpenseDelete.Enabled = true;

                            this.expense_id = int.Parse(gridViewExpenses.GetRowCellValue(gridViewExpenses.FocusedRowHandle, "Id").ToString());

                            Models.Expense expense = context.Expenses.Find(this.expense_id);

                            if (expense != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                context.Expenses.Remove(expense);
                                context.SaveChanges();
                                this.loadExpenses();
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
            }
        }

        private void btnExpenseRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnExpenseEdit.Enabled = false;
            btnExpenseDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadExpenses();
        }

        private void btnPrintExpenses_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlExpenses.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense"))
            {
                AddEditExpense addEditExpense = new AddEditExpense();
                addEditExpense.setExpensesObject(this);
                addEditExpense.setTypeOperation("Add");
                addEditExpense.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.expenseEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.expenseDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlExpenses);
            customPrint.PrintGridControl(gridViewExpenses);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnExpenseEdit.Enabled = false;
            btnExpenseDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadExpenses();
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
            this.loadExpenses();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlExpenses, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlExpenses, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlExpenses, "xlsx");
        }
    }
}