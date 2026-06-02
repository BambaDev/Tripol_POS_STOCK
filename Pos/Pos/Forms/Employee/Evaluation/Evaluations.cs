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

namespace Pos.Forms.Employee.Evaluation
{
    public partial class Evaluations : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int evaluation_id = 0;

        public Evaluations()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewEvaluations.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewEvaluations.RowStyle += gridViewEvaluations_RowStyle;
            gridViewEvaluations.FocusedRowChanged += gridViewEvaluations_FocusedRowChanged;
            gridViewEvaluations.CustomDrawCell += gridViewEvaluations_CustomDrawCell;
            gridViewEvaluations.RowHeight = Function.Helper.RowHeight;
            gridViewEvaluations.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewEvaluations_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewEvaluations.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewEvaluations_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewEvaluations.FocusedRowHandle && e.Column == gridViewEvaluations.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewEvaluations_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
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
                if (gridViewEvaluations.RowCount > 0 && gridViewEvaluations.FocusedRowHandle >= 0)
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
            evaluationEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Evaluation"))
            {
                AddEditEvaluation evaluation = new AddEditEvaluation();
                evaluation.setEvaluationsObject(this);
                evaluation.setTypeOperation("Add");
                evaluation.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            evaluationDelete();
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
            foreach (GridColumn column in gridViewEvaluations.Columns)
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

        private void Evaluations_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadEvaluations();
        }

        public void loadEvaluations()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Evaluations
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
                var currentPageData = context.Evaluations
                    .Where(p => p.Name.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlEvaluations.DataSource = currentPageData;
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
            if (Function.Permission.HasPermission("Add Evaluation"))
            {
                AddEditEvaluation department = new AddEditEvaluation();
                department.setEvaluationsObject(this);
                department.setTypeOperation("Add");
                department.ShowDialog();
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.evaluationEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.evaluationDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewEvaluations.RowCount > 0 && gridViewEvaluations.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.evaluation_id = int.Parse(gridViewEvaluations.GetRowCellValue(gridViewEvaluations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
        }
    }

        private void gridViewEvaluations_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewEvaluations.RowCount > 0 && gridViewEvaluations.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.evaluation_id = int.Parse(gridViewEvaluations.GetRowCellValue(gridViewEvaluations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.evaluationEdit();
        }
    }

        private void repEditEvaluation_Click(object sender, EventArgs e)
        {
            if (gridViewEvaluations.RowCount > 0 && gridViewEvaluations.FocusedRowHandle >= 0)
            {
                if (Function.Permission.HasPermission("Edit Evaluation"))
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    this.evaluation_id = int.Parse(gridViewEvaluations.GetRowCellValue(gridViewEvaluations.FocusedRowHandle, "Id").ToString());
                    Function.Sound.Selected();
                    this.evaluationEdit();
                }
        }
    }

        private void repDeleteEvaluation_Click(object sender, EventArgs e)
        {
            this.evaluationDelete();
        }

        private void gridViewEvaluations_RowClick(object sender, EventArgs e)
        {
            if (gridViewEvaluations.RowCount > 0 && gridViewEvaluations.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.evaluation_id = int.Parse(gridViewEvaluations.GetRowCellValue(gridViewEvaluations.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void evaluationEdit()
        {
            
            if (this.evaluation_id!=0)
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                AddEditEvaluation skill = new AddEditEvaluation();
                skill.FormClosed += (s, args) => overlay.Close();

                skill.setEvaluationsObject(this);
                skill.setTypeOperation("Edit");
                skill.Show();
                skill.TopMost = true;

            }
            else
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
                XtraMessageBox.Show(pleaseSelectItem);
            }
            // This ensures the overlay form is displayed behind the modal form but above the parent form

        }

        public void evaluationDelete()
        {
            //if (gridViewEvaluations.FocusedRowHandle > 0)
            //{
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
                    this.evaluation_id = int.Parse(gridViewEvaluations.GetRowCellValue(gridViewEvaluations.FocusedRowHandle, "Id").ToString());

                    Models.Evaluation evaluation = context.Evaluations.Find(this.evaluation_id);

                    if (evaluation != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.Evaluations.Remove(evaluation);
                        context.SaveChanges();
                        this.loadEvaluations();
                        Function.Sound.Deleted();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                //}
            }
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadEvaluations();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlEvaluations);
            customPrint.PrintGridControl(gridViewEvaluations);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Evaluation"))
            {
                AddEditEvaluation evaluation = new AddEditEvaluation();
                evaluation.setEvaluationsObject(this);
                evaluation.setTypeOperation("Add");
                evaluation.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.evaluationEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.evaluationDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlEvaluations.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.loadEvaluations();
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
                    int totalItems = context.Evaluations.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadEvaluations();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEvaluations, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEvaluations, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEvaluations, "xlsx");
        }
    }
}