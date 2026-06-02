using DevExpress.DataAccess.Native;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using Pos.Forms.Overlay;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pos.Forms.Product.Warranty;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Sale;
using Pos.Forms.Statistics;
using Pos.Forms.Purchase;
using PosScreen =  Pos.Forms.Screen.Pos;

namespace Pos.Forms.Dashboard
{
    public partial class QuickActions : DevExpress.XtraEditors.XtraForm
    {
        private OverlayForm overlay;

        public QuickActions()
        {
            InitializeComponent();
            this.toRtl();
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

        private void QuickDashboard_Load(object sender, EventArgs e)
        {
            //
        }
        
        public void ApplyCustomFont(float fontSize = 14.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            // Apply the custom font to the form
            this.Font = customFont;

            // Apply the custom font to buttons named Btn1 to Btn20
            for (int i = 1; i <= 20; i++)
            {
                Control btn = this.Controls.Find("Btn" + i, true).FirstOrDefault();
                Control simpleButton = this.Controls.Find("simpleButton" + i, true).FirstOrDefault();
                
                if (btn != null)
                {
                    btn.Font = customFont;
                }
                
                if(simpleButton != null)
                {
                    simpleButton.Font = customFont;
                }
            }
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            Btn1.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn1.Enabled = true;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Btn2.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("Add Sale"))
            {
                Sale.Sales sales = new Sale.Sales();
                sales.StartPosition = FormStartPosition.CenterScreen; // Center the form
                sales.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                sales.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                sales.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn2.Enabled = true;
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            Btn3.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Purchases"))
            {
                //Purchase.Purchases purchases = new Purchase.Purchases();
                //purchases.StartPosition = FormStartPosition.CenterScreen; // Center the form
                //purchases.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                //purchases.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                //purchases.ShowDialog();
                AddEditPurchase addEditPurchase = new AddEditPurchase();
                addEditPurchase.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn3.Enabled = true;
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            Btn4.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Inventories"))
            {
                Inventory.Inventories inventories = new Inventory.Inventories();
                inventories.StartPosition = FormStartPosition.CenterScreen; // Center the form
                inventories.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                inventories.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                inventories.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn4.Enabled = true;
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            Btn5.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Products"))
            {
                Product.Products products = new Product.Products();
                products.StartPosition = FormStartPosition.CenterScreen; // Center the form
                products.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                products.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                products.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn5.Enabled = true;
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            Btn6.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Suppliers"))
            {
                Supplier.Suppliers suppliers = new Supplier.Suppliers();
                suppliers.StartPosition = FormStartPosition.CenterScreen; // Center the form
                suppliers.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                suppliers.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                suppliers.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn6.Enabled = true;
        }

        private void Btn11_Click(object sender, EventArgs e)
        {
            Btn11.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Warehouses"))
            {
                Warehouses warehouses = new Warehouses();
                warehouses.StartPosition = FormStartPosition.CenterScreen; // Center the form
                warehouses.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                warehouses.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                warehouses.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn11.Enabled = true;
        }

        private void Btn12_Click(object sender, EventArgs e)
        {
            Btn12.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Stocks"))
            {
                Stock.Stocks stocks = new Stock.Stocks();
                stocks.StartPosition = FormStartPosition.CenterScreen; // Center the form
                stocks.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                stocks.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                stocks.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn12.Enabled = true;
        }

        private void Btn13_Click(object sender, EventArgs e)
        {
            Btn13.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Warranties"))
            {
                Warranties warranties = new Warranties();
                warranties.StartPosition = FormStartPosition.CenterScreen; // Center the form
                warranties.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                warranties.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                warranties.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn13.Enabled = true;
        }

        private void Btn14_Click(object sender, EventArgs e)
        {
            Btn14.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Promotions"))
            {
                Product.Promotion.Promotions promotions = new Product.Promotion.Promotions();
                promotions.StartPosition = FormStartPosition.CenterScreen; // Center the form
                promotions.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                promotions.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                promotions.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn14.Enabled = true;
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            Btn7.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Payrolls"))
            {
                Forms.Employee.Payroll.Payrolls payrolls = new Employee.Payroll.Payrolls();
                payrolls.StartPosition = FormStartPosition.CenterScreen; // Center the form
                payrolls.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                payrolls.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                payrolls.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn7.Enabled = true;
        }

        private void Btn10_Click(object sender, EventArgs e)
        {
            Btn10.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Attendances"))
            {
                Forms.Employee.Attendance.Attendances attendances = new Employee.Attendance.Attendances();
                attendances.StartPosition = FormStartPosition.CenterScreen; // Center the form
                attendances.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                attendances.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                attendances.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn10.Enabled = true;
        }

        private void Btn17_Click(object sender, EventArgs e)
        {
            Btn17.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Employees"))
            {
                Forms.Employee.Employees employees = new Employee.Employees();
                employees.StartPosition = FormStartPosition.CenterScreen; // Center the form
                employees.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                employees.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                employees.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn17.Enabled = true;
        }

        private void Btn16_Click(object sender, EventArgs e)
        {
            Btn16.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Purchase Returns"))
            {
                Purchase.Return.Returns returns = new Purchase.Return.Returns();
                returns.StartPosition = FormStartPosition.CenterScreen; // Center the form
                returns.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                returns.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                returns.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn16.Enabled = true;
        }

        private void Btn15_Click(object sender, EventArgs e)
        {
            Btn15.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("List Sale Returns"))
            {
                Sale.Return.Returns returns = new Sale.Return.Returns();
                returns.StartPosition = FormStartPosition.CenterScreen; // Center the form
                returns.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                returns.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                returns.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn15.Enabled = true;
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            Btn8.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("Audit Trail"))
            {
                AuditTrail.AuditTrail auditTrail = new AuditTrail.AuditTrail();
                auditTrail.StartPosition = FormStartPosition.CenterScreen; // Center the form
                auditTrail.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                auditTrail.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                auditTrail.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn8.Enabled = true;
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            Btn9.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("Today Summary"))
            {
                ShowOverlay();
                Forms.Alert.TodaySummary todaySummary = new Forms.Alert.TodaySummary();
                todaySummary.ShowDialog();
                HideOverlay();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn9.Enabled = true;
        }

        private void Btn20_Click(object sender, EventArgs e)
        {
            Btn20.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("Alert Cases"))
            {
                ShowOverlay();
                Forms.Alert.AlertQuantity alertQuantity = new Forms.Alert.AlertQuantity();
                alertQuantity.ShowDialog();
                HideOverlay();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn20.Enabled = true;
        }

        private void Btn19_Click(object sender, EventArgs e)
        {
            Btn19.Enabled = false;

            if (Function.Permission.HasPermissionWithoutAlert("TodoList"))
            {
                TodoList.TodoLists todoLists = new TodoList.TodoLists(); // we need to move out the report from Maintenance folder
                todoLists.StartPosition = FormStartPosition.CenterScreen; // Center the form
                todoLists.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                todoLists.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                todoLists.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn19.Enabled = true;
        }

        private void Btn18_Click(object sender, EventArgs e)
        {
            Btn18.Enabled = false;

            if (Function.Permission.HasPermission("Statistics"))
            {
                Forms.Statistics.Statistics statistics = new Statistics.Statistics();
                statistics.StartPosition = FormStartPosition.CenterScreen; // Center the form
                statistics.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                statistics.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                statistics.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn18.Enabled = true;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Btn9.Enabled = false;

            if (Function.Permission.HasPermission("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn9.Enabled = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Btn1.Enabled = false;

            if (Function.Permission.HasPermission("Adjustments"))
            {
                Forms.Stock.Adjustment.Adjustments adjustments = new Stock.Adjustment.Adjustments();
                adjustments.StartPosition = FormStartPosition.CenterScreen; // Center the form
                adjustments.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                adjustments.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                adjustments.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn1.Enabled = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Btn2.Enabled = false;

            if (Function.Permission.HasPermission("Transfers"))
            {
                Forms.Stock.Transfer.Transfers transfers = new Stock.Transfer.Transfers();
                transfers.StartPosition = FormStartPosition.CenterScreen; // Center the form
                transfers.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                transfers.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                transfers.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn2.Enabled = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Btn3.Enabled = false;

            if (Function.Permission.HasPermission("Categories"))
            {
                Forms.Product.Category.Categories categories = new Product.Category.Categories();
                categories.StartPosition = FormStartPosition.CenterScreen; // Center the form
                categories.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                categories.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                categories.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn3.Enabled = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Btn4.Enabled = false;

            if (Function.Permission.HasPermission("Taxes"))
            {
                Forms.Tax.Taxes taxes = new Tax.Taxes();
                taxes.StartPosition = FormStartPosition.CenterScreen; // Center the form
                taxes.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                taxes.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                taxes.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn4.Enabled = true;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Btn6.Enabled = false;

            if (Function.Permission.HasPermission("Expenses"))
            {
                Forms.Expense.Expenses expenses = new Expense.Expenses();
                expenses.StartPosition = FormStartPosition.CenterScreen; // Center the form
                expenses.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                expenses.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                expenses.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn6.Enabled = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Btn5.Enabled = false;

            if (Function.Permission.HasPermission("Currencies"))
            {
                Forms.Currency.Currencies currencies = new Currency.Currencies();
                currencies.StartPosition = FormStartPosition.CenterScreen; // Center the form
                currencies.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                currencies.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                currencies.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn5.Enabled = true;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Btn8.Enabled = false;

            if (Function.Permission.HasPermission("Backups"))
            {
                Forms.Setting.Backups backups = new Setting.Backups();
                backups.StartPosition = FormStartPosition.CenterScreen; // Center the form
                backups.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                backups.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                backups.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn8.Enabled = true;
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Btn7.Enabled = false;

            if (Function.Permission.HasPermission("Settings"))
            {
                Forms.Setting.Settings settings = new Setting.Settings();
                settings.StartPosition = FormStartPosition.CenterScreen; // Center the form
                settings.Width = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.9); // 90% of the screen width
                settings.Height = (int)(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9); // 90% of the screen height
                settings.ShowDialog();
            }
            else
            {
                ShowOverlay();
                Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                accessDenied.ShowDialog();
                HideOverlay();
            }

            Btn7.Enabled = true;
        }
    }
}