using DevExpress.XtraEditors;
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
using System.Runtime.InteropServices;
using System.Windows.Controls;
using Pos.Forms.Customer;
using Pos.Forms.Alert;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.Statistics
{
    public partial class Statistics : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int customerId = 0;
        public decimal currentDue = 0;
        public Customers customers = null;

        public Statistics()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewSalePayment.RowStyle += gridViewCases_RowStyle;
            gridViewSalePayment.FocusedRowChanged += gridViewCases_FocusedRowChanged;
            gridViewSalePayment.CustomDrawCell += gridViewCases_CustomDrawCell;
            gridViewSalePayment.RowHeight = Function.Helper.RowHeight;
            gridViewSalePayment.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCases_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewSalePayment.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCases_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewSalePayment.FocusedRowHandle && e.Column == gridViewSalePayment.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCases_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
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
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    control.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            simpleLabelItem1.AppearanceItemCaption.Font = customFont;
            simpleLabelItem3.AppearanceItemCaption.Font = customFont;
            simpleLabelItem4.AppearanceItemCaption.Font = customFont;
            simpleLabelItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem11.AppearanceItemCaption.Font = customFont;
            btnSalePaymentsThisYear.Font = customFont;
            btnSalePaymentsLastMonth.Font = customFont;
            btnSalePaymentsOfTheMonth.Font = customFont;
            btnSalePaymentsOfTheWeek.Font = customFont;
            btnTodaySalePayments.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup8.AppearanceGroup.Font = customFont10;
            layoutControlGroup9.AppearanceGroup.Font = customFont10;
            layoutControlGroup10.AppearanceGroup.Font = customFont10;
            layoutControlGroup12.AppearanceGroup.Font = customFont10;
            layoutControlGroup13.AppearanceGroup.Font = customFont10;
            layoutControlGroup14.AppearanceGroup.Font = customFont10;
            layoutControlGroup15.AppearanceGroup.Font = customFont10;
            layoutControlGroup16.AppearanceGroup.Font = customFont10;
            layoutControlGroup17.AppearanceGroup.Font = customFont10;
            layoutControlGroup18.AppearanceGroup.Font = customFont10;
            layoutControlGroup19.AppearanceGroup.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem18.AppearanceItemCaption.Font = customFont9;
            layoutControlItem17.AppearanceItemCaption.Font = customFont9;
            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewSalePayment.Columns)
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

        public void setObject(Customers customers)
        {
            this.customers = customers;
        }

        public void loadSalePayments()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.SalePayments
                    .Include(s => s.Sale)
                    .Include(s => s.Customer)
                    .Include(s => s.BusinessLocation)
                    .Where(p => p.Sale.ReferenceNo.Contains(searchTerm) ||
                            p.Customer.FullName.Contains(searchTerm))
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
                var currentPageData = context.SalePayments
                    .Include(s => s.Sale)
                    .Include(s => s.Customer)
                    .Include(s => s.BusinessLocation)
                    .Where(p => p.Sale.ReferenceNo.Contains(searchTerm) ||
                            p.Customer.FullName.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlSalePayment.DataSource = currentPageData;
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

        public void loadAscendingSalePayments()
        {
            using (var context = new AppDbContext())
            {
                gridControlSalePayment.DataSource = context.SalePayments.OrderBy(p => p.Id).ToList();
            }
        }

        private void loadStatistics()
        {
            using (var context = new AppDbContext())
            {
                string totalCaseFees = context.SalePayments.Sum(f => f.Amount).ToString();
                string totalAmountsPaid = context.SalePayments.Sum(f => f.Amount).ToString();
                string totalAmountsOwed = context.SalePayments.Sum(f => f.Due).ToString();

                simpleLabelItemNumberOfCases.Text = context.SalePayments.Count().ToString();
                simpleLabelItemTotalCaseFees.Text = Function.Helper.FormatAmount(totalCaseFees);
                simpleLabelItemTotalAmountsPaid.Text = Function.Helper.FormatAmount(totalAmountsPaid);
                simpleLabelItemTotalAmountsOwed.Text = Function.Helper.FormatAmount(totalAmountsOwed);

                Function.Sound.Selected();
            }
        }


        private void txtSort_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();

            if (txtSort.EditValue != null)
            {
                if (txtSort.EditValue.ToString() == "Ascending")
                {
                    this.loadAscendingSalePayments();
                }
                else if (txtSort.EditValue.ToString() == "Descending")
                {
                    this.loadSalePayments();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.loadSalePayments();
            txtSort.Clear();
        }

        private void txtSort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            this.switchBtns();
            this.loadSalePayments();
            this.loadStatistics();
        }

        public void switchBtns()
        {
            // Retrieve the language setting (en, fr, or ar)
            string lang = Properties.Settings.Default.Lang;

            // Initialize the language-specific variables
            string salePaymentsLastMonth;
            string salePaymentsOfTheMonth;
            string salePaymentsOfTheWeek;
            string todaySalePayments;
            string salePaymentsThisYear;

            // Set language-specific strings based on the current language setting
            if (lang == "en")
            {
                salePaymentsLastMonth = "Sale Payments Last Month";
                salePaymentsOfTheMonth = "Sale Payments Of The Month";
                salePaymentsOfTheWeek = "Sale Payments Of The Week";
                todaySalePayments = "Today's Sale Payments";
                salePaymentsThisYear = "Sale Payments This Year";
            }
            else if (lang == "fr")
            {
                salePaymentsLastMonth = "Paiements de vente du mois dernier";
                salePaymentsOfTheMonth = "Paiements de vente du mois";
                salePaymentsOfTheWeek = "Paiements de vente de la semaine";
                todaySalePayments = "Paiements de vente d'aujourd'hui";
                salePaymentsThisYear = "Paiements de vente cette année";
            }
            else // Arabic (ar)
            {
                salePaymentsLastMonth = "مدفوعات المبيعات الشهر الماضي";
                salePaymentsOfTheMonth = "مدفوعات المبيعات لهذا الشهر";
                salePaymentsOfTheWeek = "مدفوعات المبيعات لهذا الأسبوع";
                todaySalePayments = "مدفوعات مبيعات اليوم";
                salePaymentsThisYear = "مدفوعات المبيعات هذه السنة";
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var tomorrow = today.AddDays(1);
            var startOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddDays(-1);
            var startOfMonth = new DateOnly(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);
            var startOfYear = new DateOnly(today.Year, 1, 1); // Start of the current year

            int countbtnSalePaymentsLastMonth = 0;
            int countbtnSalePaymentsOfTheMonth = 0;
            int countbtnSalePaymentsOfTheWeek = 0;
            int countbtnTodaySalePayments = 0;
            int countbtnSalePaymentsThisYear = 0;

            using (var context = new AppDbContext())
            {
                // Sale payments for last month
                countbtnSalePaymentsLastMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastMonth &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastMonth)
                    .Count();
                btnSalePaymentsLastMonth.Text = $"( {countbtnSalePaymentsLastMonth} ) {salePaymentsLastMonth}";

                // Sale payments for the current month
                countbtnSalePaymentsOfTheMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfMonth &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) < endOfMonth)
                    .Count();
                btnSalePaymentsOfTheMonth.Text = $"( {countbtnSalePaymentsOfTheMonth} ) {salePaymentsOfTheMonth}";

                // Sale payments for the current week
                countbtnSalePaymentsOfTheWeek = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfWeek &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) < endOfWeek)
                    .Count();
                btnSalePaymentsOfTheWeek.Text = $"( {countbtnSalePaymentsOfTheWeek} ) {salePaymentsOfTheWeek}";

                // Sale payments for today
                countbtnTodaySalePayments = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) == today)
                    .Count();
                btnTodaySalePayments.Text = $"( {countbtnTodaySalePayments} ) {todaySalePayments}";

                // Sale payments for the current year
                countbtnSalePaymentsThisYear = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfYear &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= today)
                    .Count();
                btnSalePaymentsThisYear.Text = $"( {countbtnSalePaymentsThisYear} ) {salePaymentsThisYear}";
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
                    int totalItems = context.SalePayments.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadSalePayments();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}