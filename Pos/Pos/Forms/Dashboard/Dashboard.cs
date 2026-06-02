using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Purchase;
using Pos.Forms.Register;
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
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Dashboard
{
    public partial class Dashboard : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt = new DataTable();
        private OverlayForm overlay;

        public Dashboard()
        {
            InitializeComponent();

            this.toRtl();

            // style the grid view
            gridViewProducts.RowStyle += gridViewCases_RowStyle;
            gridViewProducts.FocusedRowChanged += gridViewCases_FocusedRowChanged;
            gridViewProducts.CustomDrawCell += gridViewCases_CustomDrawCell;
            gridViewProducts.RowHeight = Function.Helper.RowHeight;
            gridViewProducts.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewCustomers.RowStyle += gridViewCustomers_RowStyle;
            gridViewCustomers.FocusedRowChanged += gridViewCustomers_FocusedRowChanged;
            gridViewCustomers.CustomDrawCell += gridViewCustomers_CustomDrawCell;
            gridViewCustomers.RowHeight = Function.Helper.RowHeight;
            gridViewCustomers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewPayments.RowStyle += gridViewPayments_RowStyle;
            gridViewPayments.FocusedRowChanged += gridViewPayments_FocusedRowChanged;
            gridViewPayments.CustomDrawCell += gridViewPayments_CustomDrawCell;
            gridViewPayments.RowHeight = Function.Helper.RowHeight;
            gridViewPayments.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewTodaySummary.RowStyle += gridViewTodaySummary_RowStyle;
            gridViewTodaySummary.FocusedRowChanged += gridViewTodaySummary_FocusedRowChanged;
            gridViewTodaySummary.CustomDrawCell += gridViewTodaySummary_CustomDrawCell;
            gridViewTodaySummary.RowHeight = Function.Helper.RowHeight;
            gridViewTodaySummary.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCases_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml("#34495e");
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewProducts.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCases_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewProducts.FocusedRowHandle && e.Column == gridViewProducts.FocusedColumn)
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

        // style the grid view
        private void gridViewCustomers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml("#34495e");
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCustomers.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCustomers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCustomers.FocusedRowHandle && e.Column == gridViewCustomers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCustomers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewPayments_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml("#34495e");
                    //e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPayments.FocusedRowHandle)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    //e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPayments_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPayments.FocusedRowHandle && e.Column == gridViewPayments.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPayments_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewTodaySummary_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#34495e");
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewTodaySummary.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewTodaySummary_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPayments.FocusedRowHandle && e.Column == gridViewPayments.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewTodaySummary_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
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

            btnProducts.Font = customFont;
            btnBrands.Font = customFont;
            btnActionCurrencies.Font = customFont;
            btnCategories.Font = customFont;
            btnActionExpenses.Font = customFont;
            btnRegisters.Font = customFont;
            btnPromotions.Font = customFont;
            btnWastes.Font = customFont;
            btnSuppliers.Font = customFont;
            btnActionUsers.Font = customFont;
            btnSales.Font = customFont;
            btnSalesHistories.Font = customFont;
            btnPurchases.Font = customFont;
            btnCustomers.Font = customFont;
            btnExpenses.Font = customFont;
            btnPurchaseHistories.Font = customFont;
            btnWarehouses.Font = customFont;
            btnAttendances.Font = customFont;
            btnLockScreen.Font = customFont;
            btnStocks.Font = customFont;
            btnCloseRegister.Font = customFont;
            btnTodaysSummary.Font = customFont;
            btnQuantityAlert.Font = customFont;
            btnPos.Font = customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceTabPage.Header.Font = customFont;
            layoutControlGroup12.AppearanceTabPage.Header.Font = customFont;
            layoutControlGroup17.AppearanceGroup.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            //layoutControlGroup10.AppearanceGroup.Font = customFont10;
            //layoutControlGroup11.AppearanceGroup.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewProducts.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewCustomers.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewPayments.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
        }

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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.loadStatistics();
            this.loadProducts();
            this.loadPayments();
            this.loadCustomers();
            this.loadTodaySummary();
        }

        public void loadStatistics()
        {
            using (var context = new AppDbContext())
            {
                string total = context.SalePayments.Sum(e => e.Amount).ToString();
                string totalExpense = context.Expenses.Sum(e => e.Amount).ToString();

                txtCustTotal.Text = context.Customers.Count().ToString();
                txtPurchasesTotal.Text = Function.Helper.FormatAmount(context.Purchases.Sum(e => e.PaidAmount).ToString());
                txtSalesTotal.Text = Function.Helper.FormatAmount(context.SalePayments.Sum(e => e.Amount).ToString());
                txtUserTotal.Text = context.Users.Count().ToString();
                txtTotal.Text = Function.Helper.FormatAmount(total);
                txtTotalExpense.Text = Function.Helper.FormatAmount(totalExpense);
            }
        }

        public void loadProducts()
        {
            using (var context = new AppDbContext())
            {
                gridControlProducts.DataSource = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .Take(15)
                    .ToList();
            }
        }

        public void loadPayments()
        {
            using (var context = new AppDbContext())
            {
                gridControlPayments.DataSource = context.SalePayments
                    .OrderByDescending(p => p.Id)
                    .Take(15)
                    .ToList();
            }
        }

        public void loadCustomers()
        {
            using (var context = new AppDbContext())
            {
                gridControlCustomers.DataSource = context.Customers
                    .OrderByDescending(p => p.Id)
                    .Take(15)
                    .ToList();
            }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Customer"))
            {
                Customer.AddEditCustomer customer = new Customer.AddEditCustomer();
                customer.ShowDialog();
            }
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense"))
            {
                Expense.AddEditExpense addEditExpense = new Expense.AddEditExpense();
                addEditExpense.ShowDialog();
            }
        }

        public void loadTodaySummary()
        {
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Section", typeof(string));
                dt.Columns.Add("Value", typeof(string));
            }

            // Clear existing rows to avoid duplication
            dt.Rows.Clear();

            // Calculate date ranges using DateOnly
            var today = DateOnly.FromDateTime(DateTime.Today);
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateOnly(today.Year, today.Month, 1);
            var startOfYear = new DateOnly(today.Year, 1, 1);

            var endOfLastWeek = startOfWeek.AddDays(-1);
            var startOfLastWeek = endOfLastWeek.AddDays(-6);

            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var endOfLastMonth = startOfMonth.AddDays(-1);

            var startOfLastYear = startOfYear.AddYears(-1);
            var endOfLastYear = startOfYear.AddDays(-1);

            using (var context = new AppDbContext())
            {
                // Fetch and calculate totals
                var totalAmountToday = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && DateOnly.FromDateTime(c.CreatedAt.Value) == today)
                    .Sum(c => c.Paid);

                var totalAmountLastWeek = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastWeek &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastWeek)
                    .Sum(c => c.Paid);

                var totalAmountLastMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastMonth &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastMonth)
                    .Sum(c => c.Paid);

                var totalAmountLastYear = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastYear &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastYear)
                    .Sum(c => c.Paid);

                var totalDueToday = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && DateOnly.FromDateTime(c.CreatedAt.Value) == today)
                    .Sum(c => c.Due);

                var totalDueLastWeek = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastWeek &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastWeek)
                    .Sum(c => c.Due);

                var totalDueLastMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastMonth &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastMonth)
                    .Sum(c => c.Due);

                var totalDueLastYear = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) >= startOfLastYear &&
                                DateOnly.FromDateTime(c.CreatedAt.Value) <= endOfLastYear)
                    .Sum(c => c.Due);
            }
        }

        private void btnActionExpenses_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Expenses"))
            {
                ShowOverlay();
                Forms.Expense.Expenses expenses = new Forms.Expense.Expenses();
                expenses.ShowDialog();
                HideOverlay();
            }
        }

        private void btnActionLocations_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Locations"))
            {
                ShowOverlay();
                Forms.Location.Locations locations = new Forms.Location.Locations();
                locations.ShowDialog();
                HideOverlay();
            }
        }

        private void btnActionCurrencies_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Currencies"))
            {
                ShowOverlay();
                Forms.Currency.Currencies currencies = new Forms.Currency.Currencies();
                currencies.ShowDialog();
                HideOverlay();
            }
        }

        private void btnActionUsers_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Users"))
            {
                ShowOverlay();
                Forms.User.Users users = new Forms.User.Users();
                users.ShowDialog();
                HideOverlay();
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Sales"))
            {
                Sale.Sales sales = new Sale.Sales();
                sales.ShowDialog();
            }
        }

        private void btnPurchases_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Purchases"))
            {
                Purchase.Purchases purchases = new Purchase.Purchases();
                purchases.ShowDialog();
            }
        }

        private void btnQuantityAlert_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Alert Quantity"))
            {
                Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();
                alertQuantity.ShowDialog();
            }
        }

        private void btnAttendances_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Attendances"))
            {
                Employee.Attendance.Attendances attendances = new Employee.Attendance.Attendances();
                attendances.ShowDialog();
            }
        }

        private void btnWarehouses_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Warehouses"))
            {
                Product.Warehouse.Warehouses warehouses = new Product.Warehouse.Warehouses();
                warehouses.ShowDialog();
            }
        }

        private void btnStocks_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Stocks"))
            {
                Stock.Stocks stocks = new Stock.Stocks();
                stocks.ShowDialog();
            }
        }

        private void btnTodaysSummary_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Today Summary"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Alert.TodaySummary todaySummary = new Alert.TodaySummary();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                todaySummary.FormClosed += (s, args) => overlay.Close();

                todaySummary.Show();
                todaySummary.TopMost = true;
            }
        }

        private void btnCloseRegister_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Close Register"))
            {
                int currentBusinessLocationId = Properties.Settings.Default.BusinessLocation;

                if (Function.Helper.IsRegisterOpen(currentBusinessLocationId))
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.CloseRegister closeRegister = new Alert.CloseRegister();

                    // This ensures the overlay form is displayed behind the modal form but above the parent form
                    closeRegister.FormClosed += (s, args) => overlay.Close();

                    closeRegister.Show();
                    closeRegister.TopMost = true;
                }
                else
                {
                    XtraMessageBox.Show("No open register record found for the specified business location.");
                }
            }
        }

        private void btnLockScreen_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsLockScreen)
            {
                Sound.Selected();
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();
                Auth.LockScreen lockScreen = new Auth.LockScreen();
                lockScreen.FormClosed += (s, args) => overlay.Close();
                lockScreen.Show();
                lockScreen.TopMost = true;
            }
            else
            {
                AccessDenied accessDenied = new AccessDenied();
                accessDenied.ShowDialog();
            }
        }

        private void btnSalesHistories_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Sale History"))
            {
                Sale.SaleHistory saleHistory = new Sale.SaleHistory();
                saleHistory.ShowDialog();
            }
        }

        private void btnPurchaseHistories_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Purchase History"))
            {
                Purchase.PurchaseHistory purchaseHistory = new Purchase.PurchaseHistory();
                purchaseHistory.ShowDialog();
            }
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Products"))
            {
                Product.Products products = new Product.Products();
                products.ShowDialog();
            }
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Categories"))
            {
                Product.Category.Categories categories = new Product.Category.Categories();
                categories.ShowDialog();
            }
        }

        private void btnRegisters_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Registers"))
            {
                Register.Registers registers = new Register.Registers();
                registers.ShowDialog();
            }
        }

        private void btnWastes_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Wastes"))
            {
                Waste.Wastes wastes = new Waste.Wastes();
                wastes.ShowDialog();
            }
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Suppliers"))
            {
                Supplier.Suppliers suppliers = new Supplier.Suppliers();
                suppliers.ShowDialog();
            }
        }

        private void btnPromotions_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Promotions"))
            {
                Product.Promotion.Promotions promotions = new Product.Promotion.Promotions();
                promotions.ShowDialog();
            }
        }
    }
}