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
using DevExpress.XtraSplashScreen;
using DevExpress.XtraWaitForm;
using Pos.Forms.Alert;
using System.Diagnostics;
using Pos.Forms.Json;
using Pos.Forms.User;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Pos.Forms.AuditTrail
{
    public partial class AuditTrail : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private PagedDataSource<object> userDataSource;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;

        public AuditTrail()
        {
            InitializeComponent();

            InitializePagination();
            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewAuditTrail.RowStyle += gridViewAuditTrail_RowStyle;
            gridViewAuditTrail.FocusedRowChanged += gridViewAuditTrail_FocusedRowChanged;
            gridViewAuditTrail.CustomDrawCell += gridViewAuditTrail_CustomDrawCell;
            gridViewAuditTrail.RowHeight = Function.Helper.RowHeight;
            gridViewAuditTrail.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewAuditTrail_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewAuditTrail.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewAuditTrail_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewAuditTrail.FocusedRowHandle && e.Column == gridViewAuditTrail.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewAuditTrail_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
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

            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlItem14.AppearanceItemCaption.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem14.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Appearance.Font = customFont10;

            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            foreach (GridColumn column in gridViewAuditTrail.Columns)
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

        private void AuditTrail_Load(object sender, EventArgs e)
        {
            this.UpdateMonthsButtonLabels();
            this.UpdateButtonLabels();
            this.loadAuditTrails();
            this.loadUsers();
        }

        public void loadUsers()
        {
            txtUser.Properties.DisplayMember = "FirstName"; // Set display member
            txtUser.Properties.ValueMember = "Id"; // Set value member
            txtUser.Properties.DataSource = userDataSource.GetDataPage();
        }

        private void InitializePagination()
        {
            // Initialize the PagedDataSource with your data fetching logic
            userDataSource = new PagedDataSource<object>(10, FetchUserData, GetTotalUserCount);

            // Add pagination controls to the LookUpEdit
            LookUpEditPaginator.AddPagination(txtUser, userDataSource);

            // Initial load of data
            loadUsers();
        }

        private List<object> FetchUserData(int skip, int take)
        {
            using (var context = new AppDbContext())
            {
                // Replace with your actual data fetching logic
                return context.Users.OrderBy(u => u.Id).Skip(skip).Take(take).ToList<object>();
            }
        }

        private int GetTotalUserCount()
        {
            using (var context = new AppDbContext())
            {
                // Replace with your actual logic to get the total count of users
                return context.Users.Count();
            }
        }

        public void loadAuditTrails()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.AuditTrails
                //.Where(p => p.TableName.Contains(searchTerm) ||
                //        p.User.FullName.Contains(searchTerm))
                .Count();
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // Load the first page of data
                BindDataToGrid(currentPage);
            }
        }

        private void BindDataToGrid(int pageNumber, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * itemsPerPage;

                // Fetch the data for the current page from the database
                var query = context.AuditTrails.AsQueryable();

                // Apply search term filter
                //if (!string.IsNullOrEmpty(searchTerm))
                //{
                //    query = query.Where(p => p.TableName.Contains(searchTerm) ||
                //                             p.User.FullName.Contains(searchTerm));
                //}

                // Apply date filter if provided
                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(p => p.ChangeTime >= startDate && p.ChangeTime < endDate);
                }

                query = query.Include(u => u.User);

                var currentPageData = query
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlAuditTrail.DataSource = currentPageData;
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

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUser.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    gridControlAuditTrail.DataSource = context.AuditTrails.Where(w => w.UserId == int.Parse(txtUser.EditValue.ToString())).ToList();
                }
            }

            Sound.Selected();
        }

        private void btnPrintInvoices_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlAuditTrail.ShowPrintPreview();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (Function.Helper.canSendEmail())
            {
                Sound.Added();

                SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

                //Function.Email.SendEmail(
                //    "bb.team.bougaoua@gmail.com",
                //    "Audit Trails Report",
                //    Function.Helper.CreateMaintenanceInvoiceTemplateHtmlMessage()
                //);

                SplashScreenManager.CloseForm();
            }
            else
            {
                Alert.FeatureDisabled featureDisabled = new Alert.FeatureDisabled();
                featureDisabled.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.loadAuditTrails();
        }

        private void gridViewAuditTrail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (!(sender is DevExpress.XtraGrid.Views.Grid.GridView gridView))
                return;  // Safe-guard to prevent null access if the sender is not a GridView

            // Ensuring we're only altering the appearance for the column that needs coloring
            // This part is crucial if you have multiple columns and only need specific ones colored
            if (e.Column.FieldName != "ActionType")
                return;

            // Retrieving the value of ActionType safely
            var actionTypeObj = gridView.GetRowCellValue(e.RowHandle, "ActionType");

            if (actionTypeObj != DBNull.Value && actionTypeObj != null)
            {
                string actionType = actionTypeObj.ToString().Trim();  // Using Trim() to avoid hidden character issues that might affect comparison

                // Switch case to assign colors based on action type
                switch (actionType)
                {
                    case "Added":
                        e.Appearance.BackColor = Color.Green;
                        break;
                    case "Deleted":
                        e.Appearance.BackColor = Color.Red;
                        break;
                    case "Modified":
                        e.Appearance.BackColor = Color.Yellow;
                        break;
                    default:
                        e.Appearance.BackColor = Color.Transparent;  // Optionally clear any color for undefined types
                        break;
                }
            }
            else
            {
                // Optionally handle or log cases where there is no valid action type
                Debug.WriteLine("Action type is null or DB null for row " + e.RowHandle);
                e.Appearance.BackColor = Color.Transparent;  // Clearing color if the condition is not met
            }
        }

        private void gridViewAuditTrail_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gridViewAuditTrail.GridControl.PointToClient(MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = gridViewAuditTrail.CalcHitInfo(pt);
            if (info.InRowCell && (info.Column.FieldName == "OldValues" || info.Column.FieldName == "NewValues"))
            {
                string json = gridViewAuditTrail.GetRowCellValue(info.RowHandle, info.Column).ToString();
                ShowJsonInPopup(json);
            }
        }

        private void ShowJsonInPopup(string json)
        {
            // Assume frmJsonViewer is a Form designed to display JSON prettily
            Json.jsonViewer viewer = new jsonViewer();
            viewer.Json = json;
            viewer.ShowDialog();
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
                    int totalItems = context.AuditTrails.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadAuditTrails();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAuditTrail, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAuditTrail, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlAuditTrail, "xlsx");
        }

        private int GetRecordCount(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new AppDbContext())
            {
                var query = context.AuditTrails.AsQueryable();

                if (startDate.HasValue && endDate.HasValue)
                {
                    query = query.Where(p => p.ChangeTime >= startDate && p.ChangeTime < endDate);
                }

                return query.Count();
            }
        }

        private void UpdateButtonLabels()
        {
            string lang = Properties.Settings.Default.Lang;

            string todayText, yesterdayText, thisWeekText, thisMonthText, lastMonthText;

            if (lang == "en")
            {
                todayText = "Today's";
                yesterdayText = "Yesterday's";
                thisWeekText = "This Week's";
                thisMonthText = "This Month's";
                lastMonthText = "Last Month's";
            }
            else if (lang == "fr")
            {
                todayText = "Aujourd'hui";
                yesterdayText = "Hier";
                thisWeekText = "Cette semaine";
                thisMonthText = "Ce mois-ci";
                lastMonthText = "Le mois dernier";
            }
            else
            {
                todayText = "اليوم";
                yesterdayText = "أمس";
                thisWeekText = "هذا الأسبوع";
                thisMonthText = "هذا الشهر";
                lastMonthText = "الشهر الماضي";
            }

            btnToday.Text = $"({GetRecordCount(DateTime.Today, DateTime.Today.AddDays(1))}) {todayText}";
            btnYesterday.Text = $"({GetRecordCount(DateTime.Today.AddDays(-1), DateTime.Today)}) {yesterdayText}";
            btnThisWeek.Text = $"({GetRecordCount(DateTime.Now.StartOfWeek(DayOfWeek.Monday), DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(7))}) {thisWeekText}";
            btnThisMonth.Text = $"({GetRecordCount(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1))}) {thisMonthText}";
            btnLastMonth.Text = $"({GetRecordCount(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))}) {lastMonthText}";
        }

        private void btnThisWeek_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
            DateTime endDate = startDate.AddDays(7);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddDays(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today;
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void UpdateMonthsButtonLabels()
        {
            string lang = Properties.Settings.Default.Lang;

            string[] monthNames;
            if (lang == "en")
            {
                monthNames = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
            else if (lang == "fr")
            {
                monthNames = new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };
            }
            else
            {
                monthNames = new string[] { "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر" };
            }

            // Update button labels with counts
            for (int i = 0; i < monthNames.Length; i++)
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, i + 1, 1);
                DateTime endDate = startDate.AddMonths(1);
                int count = GetRecordCount(startDate, endDate);

                switch (i)
                {
                    case 0:
                        btnJanuary.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 1:
                        btnFebruary.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 2:
                        btnMarch.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 3:
                        btnApril.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 4:
                        btnMay.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 5:
                        btnJune.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 6:
                        btnJuly.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 7:
                        btnAugust.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 8:
                        btnSeptember.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 9:
                        btnOctober.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 10:
                        btnNovember.Text = $"({count}) {monthNames[i]}";
                        break;
                    case 11:
                        btnDecember.Text = $"({count}) {monthNames[i]}";
                        break;
                }
            }
        }

        private void btnJanuary_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnFebruary_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 2, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnMarch_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 3, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnApril_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 4, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnMay_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 5, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnJune_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 6, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnJuly_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 7, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnAugust_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 8, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnSeptember_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 9, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnOctober_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 10, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnNovember_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 11, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnDecember_Click(object sender, EventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 12, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}