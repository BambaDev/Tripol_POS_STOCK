using DevExpress.XtraEditors;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Pos.Function;
using Pos.Forms.BusinessLocation;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraBars;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraGrid.Columns;
using System.Runtime.InteropServices;
using DevExpress.XtraLayout;

namespace Pos.Forms.OrderReport
{
    public partial class Report : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;

        public Report()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
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
            System.Drawing.Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;

            btnOrdersOfTheMonth.Font = customFont;
            btnOrdersLastMonth.Font = customFont;
            btnOrdersOfTheWeek.Font = customFont;
            btnTodayOrders.Font = customFont;
            btnTomorrowOrders.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            System.Drawing.Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem31.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            layoutControlItem12.AppearanceItemCaption.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            January.Font = customFont10;
            February.Font = customFont10;
            March.Font = customFont10;
            April.Font = customFont10;
            May.Font = customFont10;
            June.Font = customFont10;
            July.Font = customFont10;
            August.Font = customFont10;
            September.Font = customFont10;
            October.Font = customFont10;
            November.Font = customFont10;
            December.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            System.Drawing.Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridView.Columns)
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

        private void Report_Load(object sender, EventArgs e)
        {
            this.switchBtns();

            this.btnSwitchMonths();

            this.getCustomers();
            this.getWarehouses();
            this.getBusinessLocations();

            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        public void getBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                BusinessLocationId.Properties.DataSource = context.BusinessLocations.ToList();
                BusinessLocationId.Properties.DisplayMember = "Name"; // Set display member
                BusinessLocationId.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getWarehouses()
        {
            using (var context = new AppDbContext())
            {
                WarehouseId.Properties.DataSource = context.Warehouses.ToList();
                WarehouseId.Properties.DisplayMember = "Name"; // Set display member
                WarehouseId.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getCustomers()
        {
            using (var context = new AppDbContext())
            {
                CustomerId.Properties.DataSource = context.Customers.ToList();
                CustomerId.Properties.DisplayMember = "FullName"; // Set display member
                CustomerId.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private List<SaleViewModel> GetCombinedData()
        {
            // Set default items per page
            perPage.SelectedIndex = 2;

            int startRecord = (currentPage - 1) * itemsPerPage;

            var today = DateTime.Today;
            var targetDate = DateTime.Today.AddMonths(-3); // Example target date
            using (var context = new AppDbContext())
            {
                var query = context.Sales
                    .Where(c => c.CreatedAt >= targetDate && c.CreatedAt <= today)
                    //.Include(c => c.Payments)
                    .Include(c => c.Customer)
                    .AsQueryable();

                if (BusinessLocationId.Text != "")
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (CustomerId.Text != "")
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                var data = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        //SaleDate = c.SaleDate, // Sale Date
                        //CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        //SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetTotalAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        Payments = c.SalePayments.Select(p => new PaymentViewModel
                        {
                            //Amount = p.Amount,
                            //PaymentDate = p.PaymentDate,
                            //PaymentType = p.PaymentMethod
                        }).ToList()
                    })
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                return data;
            }
        }

        private void ConfigureGridControl(List<SaleViewModel> data)
        {
            gridControl.DataSource = data;
        }

        public void loadSales()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;
                var query = context.Sales
                        .Where(p => p.ReferenceNo.Contains(searchTerm))
                        .Include(c => c.SalePayments)
                        .Include(c => c.Customer)
                        .AsQueryable();

                // Apply filters
                if (BusinessLocationId.Text != "")
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (CustomerId.Text != "")
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                int totalItems = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        //SaleDate = c.SaleDate, // Sale Date
                        //CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        //SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetTotalAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        Payments = c.SalePayments.Select(p => new PaymentViewModel
                        {
                            //Amount = p.Amount,
                            //PaymentDate = p.PaymentDate,
                            //PaymentType = p.PaymentMethod
                        }).ToList()
                    })
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

                var query = context.Sales
                        .Where(p => p.ReferenceNo.Contains(searchTerm))
                        .Include(c => c.SalePayments)
                        .Include(c => c.Customer)
                        .AsQueryable();

                // Apply filters
                if (BusinessLocationId.Text != "")
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (CustomerId.Text != "")
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                var currentPageData = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        //SaleDate = c.SaleDate, // Sale Date
                        //CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        //SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetTotalAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        Payments = c.SalePayments.Select(p => new PaymentViewModel
                        {
                            //Amount = (decimal)p.Amount,
                            //PaymentDate = p.PaymentDate,
                            //PaymentType = p.PaymentMethod
                        }).ToList()
                    })
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControl.DataSource = currentPageData;
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

        public void switchBtns()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var tomorrow = today.AddDays(1);
            var startOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddDays(-1);
            var startOfMonth = new DateOnly(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            int countbtnIssuesLastMonth = 0;
            int countbtnIssuesOfTheMonth = 0;
            int countbtnIssuesOfTheWeek = 0;
            int countbtnTodayIssues = 0;
            int countbtnTomorrowIssues = 0;

            string lang = Properties.Settings.Default.Lang;

            string msgCountbtnIssuesLastMonth;
            string msgCountbtnIssuesOfTheMonth;
            string msgCountbtnIssuesOfTheWeek;
            string msgCountbtnTodayIssues;
            string msgCountbtnTomorrowIssues;

            if (lang == "en")
            {
                msgCountbtnIssuesLastMonth = "Cases Last Month";
                msgCountbtnIssuesOfTheMonth = "Cases Of The Month";
                msgCountbtnIssuesOfTheWeek = "Cases Of The Week";
                msgCountbtnTodayIssues = "Today's Cases";
                msgCountbtnTomorrowIssues = "Tomorrow's Cases";
            }
            else if (lang == "fr")
            {
                msgCountbtnIssuesLastMonth = "Requêtes le mois dernier";
                msgCountbtnIssuesOfTheMonth = "Cas du mois";
                msgCountbtnIssuesOfTheWeek = "Cas de la semaine";
                msgCountbtnTodayIssues = "Cas du jour";
                msgCountbtnTomorrowIssues = "Les cas de demain";
            }
            else
            {
                msgCountbtnIssuesLastMonth = "الحالات في الشهر الماضي";
                msgCountbtnIssuesOfTheMonth = "حالات الشهر";
                msgCountbtnIssuesOfTheWeek = "حالات الأسبوع";
                msgCountbtnTodayIssues = "حالات اليوم";
                msgCountbtnTomorrowIssues = "حالات الغد";
            }

            using (var context = new AppDbContext())
            {
                var baseQuery = context.Sales.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(BusinessLocationId.Text))
                    baseQuery = baseQuery.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (!string.IsNullOrEmpty(CustomerId.Text))
                    baseQuery = baseQuery.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                // Count cases for each button
                //countbtnIssuesLastMonth = baseQuery
                //    .Where(c => c.CreatedAt >= startOfLastMonth && c.UpdateAt <= endOfLastMonth)
                //    .Count();

                //countbtnIssuesOfTheMonth = baseQuery
                //    .Where(c => c.CreatedAt >= startOfMonth && c.UpdateAt < endOfMonth)
                //    .Count();

                //countbtnIssuesOfTheWeek = baseQuery
                //    .Where(c => c.CreatedAt >= startOfWeek && c.UpdateAt < endOfWeek)
                //    .Count();

                //countbtnTodayIssues = baseQuery
                //    .Where(c => c.CreatedAt == today)
                //    .Count();

                //countbtnTomorrowIssues = baseQuery
                //    .Where(c => c.CreatedAt == tomorrow)
                //    .Count();
            }

            btnOrdersLastMonth.Text = $"( {countbtnIssuesLastMonth} ) {msgCountbtnIssuesLastMonth}";
            btnOrdersOfTheMonth.Text = $"( {countbtnIssuesOfTheMonth} ) {msgCountbtnIssuesOfTheMonth}";
            btnOrdersOfTheWeek.Text = $"( {countbtnIssuesOfTheWeek} ) {msgCountbtnIssuesOfTheWeek}";
            btnTodayOrders.Text = $"( {countbtnTodayIssues} ) {msgCountbtnTodayIssues}";
            btnTomorrowOrders.Text = $"( {countbtnTomorrowIssues} ) {msgCountbtnTomorrowIssues}";
        }

        private void btnIssuesOfTheWeek_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7);

            LoadData(startOfWeek, endOfWeek);
        }

        private void btnIssuesOfTheMonth_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfMonth = new DateOnly(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            LoadData(startOfMonth, endOfMonth);
        }

        private void btnTomorrowIssues_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var tomorrow = today.AddDays(1);

            LoadData(tomorrow, tomorrow);
        }

        private void btnTodayIssues_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            var today = DateOnly.FromDateTime(DateTime.Today);

            LoadData(today, today);
        }

        private void btnIssuesLastMonth_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateOnly(today.Year, today.Month, 1).AddDays(-1); // Last day of last month

            LoadData(startOfLastMonth, endOfLastMonth);
        }

        private void LoadData(DateOnly startDate, DateOnly endDate)
        {
            using (var context = new AppDbContext())
            {
                var query = context.Sales
                    //.Where(c => c.CreatedAt >= startDate && c.UpdatedAt <= endDate)
                    .Include(c => c.SalePayments)
                    .Include(c => c.Customer)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(BusinessLocationId.Text))
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (!string.IsNullOrEmpty(CustomerId.Text))
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                var data = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        //SaleDate = c.SaleDate, // Sale Date
                        //CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        //SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetTotalAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        //Payments = c.SalePayments.Select(p => new PaymentViewModel
                        //{
                        //    Amount = p.Amount,
                        //    PaymentDate = p.PaymentDate,
                        //    PaymentType = p.PaymentMethod
                        //}).ToList()
                    })
                    .ToList();

                gridControl.DataSource = data;
            }
        }

        private void LoadDataForMonth(int month)
        {
            Sound.Selected();

            var year = DateTime.Now.Year; // Get the current year
            var startOfMonth = new DateTime(year, month, 1); // First day of the specified month
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1); // Last moment of the month

            using (var context = new AppDbContext())
            {
                var query = context.Sales
                    .Where(c => c.CreatedAt >= startOfMonth && c.UpdatedAt < endOfMonth)
                    .Include(c => c.SalePayments)
                    .Include(c => c.Customer)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(BusinessLocationId.Text))
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (!string.IsNullOrEmpty(CustomerId.Text))
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                var data = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        //SaleDate = c.SaleDate, // Sale Date
                        //CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        //SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetTotalAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        //Payments = c.Payments.Select(p => new PaymentViewModel
                        //{
                        //    Amount = p.Amount,
                        //    PaymentDate = p.PaymentDate,
                        //    PaymentType = p.PaymentMethod
                        //}).ToList()
                    })
                    .ToList();

                gridControl.DataSource = data;
            }
        }

        private void January_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(1);
        }

        private void February_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(2);
        }

        private void March_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(3);
        }

        private void April_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(4);
        }

        private void May_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(5);
        }

        private void June_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(6);
        }

        private void July_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(7);
        }

        private void August_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(8);
        }

        private void September_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(9);
        }

        private void October_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(10);
        }

        private void November_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(11);
        }

        private void December_Click(object sender, EventArgs e)
        {
            LoadDataForMonth(12);
        }

        private int getCountDataForMonth(int month)
        {
            Sound.Selected(); 
            
            var year = DateTime.Now.Year; // Get the current year
            var startOfMonth = new DateTime(year, month, 1); // First day of the specified month
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1); // Last moment of the month

            using (var context = new AppDbContext())
            {
                var query = context.Sales
                    .Where(c => c.CreatedAt >= startOfMonth && c.CreatedAt <= endOfMonth)
                    .Include(c => c.SalePayments)
                    .Include(c => c.Customer)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(BusinessLocationId.Text))
                    query = query.Where(c => c.BusinessLocationId == int.Parse(BusinessLocationId.EditValue.ToString()));

                if (!string.IsNullOrEmpty(CustomerId.Text))
                    query = query.Where(c => c.CustomerId == int.Parse(CustomerId.EditValue.ToString()));

                var data = query
                    .Select(c => new SaleViewModel
                    {
                        Id = c.Id, // Sale ID
                        ReferenceNo = c.ReferenceNo, // Sale Reference Number
                        SaleDate = (DateTime)c.SaleDate, // Sale Date
                        CustomerName = c.Customer.FullName, // Assuming Customer has a FullName property
                        SaleStatus = c.SaleStatus, // Status of the sale
                        //TotalAmount = c.TotalAmount, // Total amount for the sale
                        //TotalDiscount = c.TotalDiscount, // Total discount applied to the sale
                        //TotalTax = c.TotalTax, // Total tax applied
                        //NetAmount = c.NetAmount, // Net total after tax and discount

                        // Sale Details projection
                        SaleDetails = c.SaleDetails.Select(d => new SaleDetailViewModel
                        {
                            //ProductId = d.ProductId,
                            //ProductName = d.ProductName,
                            //Quantity = d.SaleQuantity,
                            //UnitPrice = d.UnitSellingPrice,
                            //Discount = d.DiscountPercent,
                            //LineTotal = d.LineTotal
                        }).ToList(),

                        // Payments projection
                        //Payments = c.Payments.Select(p => new PaymentViewModel
                        //{
                        //    Amount = p.Amount,
                        //    PaymentDate = p.PaymentDate,
                        //    PaymentType = p.PaymentMethod
                        //}).ToList()
                    })
                    .Count();

                return data;
            }
        }

        private void btnSwitchMonths()
        {
            string lang = Properties.Settings.Default.DefaultCurrency;

            string _January = "January";
            string _February = "February";
            string _March = "March";
            string _April = "April";
            string _May = "May";
            string _June = "June";
            string _July = "July";
            string _August = "August";
            string _September = "September";
            string _October = "October";
            string _November = "November";
            string _December = "December";

            if (lang == "fr")
            {
                _January = "Janvier";
                _February = "Février";
                _March = "Mars";
                _April = "Avril";
                _May = "Mai";
                _June = "Juin";
                _July = "Juillet";
                _August = "Août";
                _September = "Septembre";
                _October = "Octobre";
                _November = "Novembre";
                _December = "Décembre";
            }
            else if (lang == "ar")
            {
                _January = "يناير";
                _February = "فبراير";
                _March = "مارس";
                _April = "أبريل";
                _May = "مايو";
                _June = "يونيو";
                _July = "يوليو";
                _August = "أغسطس";
                _September = "سبتمبر";
                _October = "أكتوبر";
                _November = "نوفمبر";
                _December = "ديسمبر";
            }

            January.Text = "(" + getCountDataForMonth(1) + ") " + _January;
            February.Text = "(" + getCountDataForMonth(2) + ") " + _February;
            March.Text = "(" + getCountDataForMonth(3) + ") " + _March;
            April.Text = "(" + getCountDataForMonth(4) + ") " + _April;
            May.Text = "(" + getCountDataForMonth(5) + ") " + _May;
            June.Text = "(" + getCountDataForMonth(6) + ") " + _June;
            July.Text = "(" + getCountDataForMonth(7) + ") " + _July;
            August.Text = "(" + getCountDataForMonth(8) + ") " + _August;
            September.Text = "(" + getCountDataForMonth(9) + ") " + _September;
            October.Text = "(" + getCountDataForMonth(10) + ") " + _October;
            November.Text = "(" + getCountDataForMonth(11) + ") " + _November;
            December.Text = "(" + getCountDataForMonth(12) + ") " + _December;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            BusinessLocationId.Clear();
            CustomerId.Clear();
            BusinessLocationId.Clear();
            WarehouseId.Clear();
            SaleType.Clear();
            IsFavorite.Clear();

            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
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
                    int totalItems = context.Sales.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadSales();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControl);
            customPrint.PrintGridControl(gridView);
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Expenses"))
            {
                Forms.Expense.Expenses expenses = new Expense.Expenses();
                expenses.ShowDialog();
            }
        }

        private void CustomerId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void CustomerCharacterId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void SectionId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void SessionLocationId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void JudicialAuthorityId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void BusinessLocationId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void CaseTypeId_EditValueChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void IsFavorite_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void IsWin_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sound.Selected();
            var combinedData = GetCombinedData();
            this.ConfigureGridControl(combinedData);
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class SaleViewModel
    {
        public int Id { get; set; } // Sale ID

        public string ReferenceNo { get; set; } // Reference number of the sale

        public DateTime SaleDate { get; set; } // Date of the sale

        public string CustomerName { get; set; } // Customer's name

        public string SaleStatus { get; set; } // Status of the sale (e.g., Completed, Pending, etc.)

        public decimal TotalAmount { get; set; } // Total amount for the sale

        public decimal TotalDiscount { get; set; } // Total discount applied to the sale

        public decimal TotalTax { get; set; } // Total tax applied

        public decimal NetAmount { get; set; } // Net total after tax and discount

        public List<SaleDetailViewModel> SaleDetails { get; set; } // List of sale details (products sold)

        public List<PaymentViewModel> Payments { get; set; } // List of payments made for the sale
    }

    public class SaleDetailViewModel
    {
        public int ProductId { get; set; } // Product ID

        public string ProductName { get; set; } // Name of the product

        public decimal Quantity { get; set; } // Quantity sold

        public decimal UnitPrice { get; set; } // Unit price of the product

        public decimal Discount { get; set; } // Discount on the line item

        public decimal LineTotal { get; set; } // Total for the line item (Quantity * UnitPrice - Discount)
    }

    public class PaymentViewModel
    {
        public string PaymentType { get; set; } // Type of payment (e.g., Cash, Card)

        public decimal Amount { get; set; } // Payment amount

        public DateTime PaymentDate { get; set; } // Payment date
    }

}
