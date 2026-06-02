using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using Pos.Forms.Alert;
using Pos.Forms.Brand;
using Pos.Forms.LoyaltyForms;
using Pos.Forms.Overlay;
using Pos.Forms.Stock;
using Pos.Forms.TodoList;
using Pos.Forms.Waste;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms
{
    public partial class MainFrm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainFrm()
        {
            InitializeComponent();
            this.toRtl();
            LanguageChangedNotifier.LanguageChanged += OnLanguageChanged;

        }

        private void OnLanguageChanged()
        {
            ReloadForm();
        }

        public void ReloadForm()
        {
            string exePath = Application.ExecutablePath;

            // Check if an instance of the application is already running
            Process currentProcess = Process.GetCurrentProcess();
            Process[] runningProcesses = Process.GetProcessesByName(currentProcess.ProcessName);

            // Ensure only one instance of the application runs
            if (runningProcesses.Length <= 1)
            {
                // Start a new instance of the application
                Process.Start(exePath);
            }

            // Close the current application instance
            Application.Exit();
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                Function.CustomArabicFont.LoadCustomFont();
                ApplyCustomArabicFontToBarItems(10.0F, true);

                // Set the form to use RTL
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
            else
            {
                Function.CustomFont.LoadCustomFont();
                ApplyCustomFontToBarItems(8.0F, true);
            }
        }

        public void ApplyCustomArabicFontToBarItems(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            // Iterate through all the BarItems in the RibbonControl and apply the custom font
            foreach (BarItem item in ribbon.Items)
            {
                // Apply font to BarButtonItem
                if (item is BarButtonItem barButtonItem)
                {
                    barButtonItem.ItemAppearance.Normal.Font = customFont;
                    barButtonItem.ItemAppearance.Hovered.Font = customFont;
                    barButtonItem.ItemAppearance.Pressed.Font = customFont;
                }
                // Apply font to BarSubItem and its subitems
                else if (item is BarSubItem barSubItem)
                {
                    // Apply font to the BarSubItem itself
                    barSubItem.ItemAppearance.Normal.Font = customFont;
                    barSubItem.ItemAppearance.Hovered.Font = customFont;
                    barSubItem.ItemAppearance.Pressed.Font = customFont;

                    // Apply the custom font to all its subitems
                    foreach (BarItemLink link in barSubItem.ItemLinks)
                    {
                        if (link.Item is BarButtonItem subButtonItem)
                        {
                            subButtonItem.ItemAppearance.Normal.Font = customFont;
                            subButtonItem.ItemAppearance.Hovered.Font = customFont;
                            subButtonItem.ItemAppearance.Pressed.Font = customFont;
                        }
                    }
                }
            }
        }

        public void ApplyCustomFontToBarItems(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomFont.customFont;

            // Iterate through all the BarItems in the RibbonControl and apply the custom font
            foreach (BarItem item in ribbon.Items)
            {
                // Apply font to BarButtonItem
                if (item is BarButtonItem barButtonItem)
                {
                    barButtonItem.ItemAppearance.Normal.Font = customFont;
                    barButtonItem.ItemAppearance.Hovered.Font = customFont;
                    barButtonItem.ItemAppearance.Pressed.Font = customFont;
                }
                // Apply font to BarSubItem and its subitems
                else if (item is BarSubItem barSubItem)
                {
                    // Apply font to the BarSubItem itself
                    barSubItem.ItemAppearance.Normal.Font = customFont;
                    barSubItem.ItemAppearance.Hovered.Font = customFont;
                    barSubItem.ItemAppearance.Pressed.Font = customFont;

                    // Apply the custom font to all its subitems
                    foreach (BarItemLink link in barSubItem.ItemLinks)
                    {
                        if (link.Item is BarButtonItem subButtonItem)
                        {
                            subButtonItem.ItemAppearance.Normal.Font = customFont;
                            subButtonItem.ItemAppearance.Hovered.Font = customFont;
                            subButtonItem.ItemAppearance.Pressed.Font = customFont;
                        }
                    }
                }
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            if (!Function.Helper.IsRegisterOpen(Properties.Settings.Default.BusinessLocation))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Register.OpenRegister openRegister = new Register.OpenRegister();

                openRegister.FormClosed += (s, args) => overlay.Close();

                openRegister.ShowDialog();
            }
            else
            {
                if (Function.Helper.hasAlertQty())
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();

                    alertQuantity.FormClosed += (s, args) => overlay.Close();

                    alertQuantity.ShowDialog();
                }
            }

            if (Function.Permission.HasPermission("Dashboard"))
            {
                openMdiChildForm(typeof(Dashboard.QuickActions));
            }

            barEditItemCurrentUser.EditValue = Properties.Settings.Default.CurrentUserFullName;
            barEditItemCurrentTime.EditValue = DateTime.Now;
        }

        private bool IsFormAlreadyOpen(Type formType)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == formType)
                {
                    form.Activate();
                    return true;
                }
            }
            return false;
        }

        private void openMdiChildForm(Type formType)
        {
            if (!IsFormAlreadyOpen(formType))
            {
                Form form = (Form)Activator.CreateInstance(formType);
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Settings"))
            {
                openMdiChildForm(typeof(Setting.Settings));
            }
        }

        private void btnCustomers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Customers"))
            {
                openMdiChildForm(typeof(Customer.Customers));
            }
        }

        private void btnAddCustomer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Customer"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Customer.AddEditCustomer customer = new Customer.AddEditCustomer();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                customer.FormClosed += (s, args) => overlay.Close();

                customer.Show();
                customer.TopMost = true;
            }
        }

        private void btnSuppliers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                openMdiChildForm(typeof(Supplier.Suppliers));
            }
        }

        private void btnAddSupplier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Supplier.AddEditSupplier supplier = new Supplier.AddEditSupplier();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                supplier.FormClosed += (s, args) => overlay.Close();

                supplier.Show();
                supplier.TopMost = true;
            }
        }

        private void btnAddBusinessLocation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Business Location"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                BusinessLocation.AddEditBusinessLocation businessLocation = new BusinessLocation.AddEditBusinessLocation();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                businessLocation.FormClosed += (s, args) => overlay.Close();

                businessLocation.Show();
                businessLocation.TopMost = true;
            }
        }

        private void btnBusinessLocations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Business Locations"))
            {
                openMdiChildForm(typeof(BusinessLocation.BusinessLocations));
            }
        }

        private void btnCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Categories"))
            {
                openMdiChildForm(typeof(Product.Category.Categories));
            }
        }

        private void btnAddCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Category"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Category.AddEditCategory category = new Product.Category.AddEditCategory();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                category.FormClosed += (s, args) => overlay.Close();

                category.Show();
                category.TopMost = true;
            }
        }

        private void btnProdBrands_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Brands"))
            {
                openMdiChildForm(typeof(Brands));
            }
        }

        private void btnProdAddBrand_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Brand"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                AddEditBrand brand = new AddEditBrand();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                brand.FormClosed += (s, args) => overlay.Close();

                brand.Show();
                brand.TopMost = true;
            }
        }

        private void btnWarranties_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Warranties"))
            {
                openMdiChildForm(typeof(Product.Warranty.Warranties));
            }
        }

        private void btnAddWarranty_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Warranty"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Warranty.AddEditWarranty warranty = new Product.Warranty.AddEditWarranty();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                warranty.FormClosed += (s, args) => overlay.Close();

                warranty.Show();
                warranty.TopMost = true;
            }
        }

        private void btnPriceGroups_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Price Groups"))
            {
                openMdiChildForm(typeof(Product.PriceGroup.PriceGroups));
            }
        }

        private void btnAddPriceGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Price Group"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.PriceGroup.AddEditPriceGroup priceGroup = new Product.PriceGroup.AddEditPriceGroup();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                priceGroup.FormClosed += (s, args) => overlay.Close();

                priceGroup.Show();
                priceGroup.TopMost = true;
            }
        }

        private void btnVariations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Variations"))
            {
                openMdiChildForm(typeof(Product.Variation.Variations));
            }
        }

        private void btnAddVariation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Variation"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Variation.AddEditVariation variation = new Product.Variation.AddEditVariation();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                variation.FormClosed += (s, args) => overlay.Close();

                variation.Show();
                variation.TopMost = true;
            }
        }

        private void btnProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Products"))
            {
                openMdiChildForm(typeof(Product.Products));
            }
        }

        private void btnAddProduct_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Product"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.AddEditProduct product = new Product.AddEditProduct();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                product.FormClosed += (s, args) => overlay.Close();

                product.Show();
                product.TopMost = true;
            }
        }

        private void btnProdUnits_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Units"))
            {
                openMdiChildForm(typeof(Product.Unit.Units));
            }
        }

        private void btnProdAddUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Unit"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Unit.AddEditUnit unit = new Product.Unit.AddEditUnit();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                unit.FormClosed += (s, args) => overlay.Close();

                unit.Show();
                unit.TopMost = true;
            }
        }

        private void btnWarehouses_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Warehouses"))
            {
                openMdiChildForm(typeof(Product.Warehouse.Warehouses));
            }
        }

        private void btnAddWarehouse_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Warehouse"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Warehouse.AddEditWarehouse warehouse = new Product.Warehouse.AddEditWarehouse();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                warehouse.FormClosed += (s, args) => overlay.Close();

                warehouse.Show();
                warehouse.TopMost = true;
            }
        }

        private void btnCurrencies_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Currencies"))
            {
                openMdiChildForm(typeof(Product.Currency.Currencies));
            }
        }

        private void btnAddCurrency_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Currency"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Currency.AddEditCurrency currency = new Product.Currency.AddEditCurrency();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                currency.FormClosed += (s, args) => overlay.Close();

                currency.Show();
                currency.TopMost = true;
            }
        }

        private void btnPurchases_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                openMdiChildForm(typeof(Purchase.Purchases));
            }
        }

        private void btnAddPurchase_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Purchase.AddEditPurchase purchase = new Purchase.AddEditPurchase(true);

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                purchase.FormClosed += (s, args) => overlay.Close();

                purchase.Show();
                purchase.TopMost = true;
            }
        }

        private void btnSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                openMdiChildForm(typeof(Sale.Sales));
            }
        }

        private void btnAddSale_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Edit Brand"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                PosScreen sale = new PosScreen();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                sale.FormClosed += (s, args) => overlay.Close();

                sale.Show();
                sale.TopMost = true;
            }
        }

        private void btnExpCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Expense Categories"))
            {
                openMdiChildForm(typeof(Expense.Category.ExpenseCategories));
            }
        }

        private void btnExpCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense Category"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Expense.Category.AddEditExpenseCategory category = new Expense.Category.AddEditExpenseCategory();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                category.FormClosed += (s, args) => overlay.Close();

                category.Show();
                category.TopMost = true;
            }
        }

        private void btnAddExpense_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Expense"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Expense.AddEditExpense expense = new Expense.AddEditExpense();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                expense.FormClosed += (s, args) => overlay.Close();

                expense.Show();
                expense.TopMost = true;
            }
        }

        private void btnExpenses_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Expenses"))
            {
                openMdiChildForm(typeof(Expense.Expenses));
            }
        }

        private void btnRegisters_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Registers"))
            {
                openMdiChildForm(typeof(Register.Registers));
            }
        }

        private void btnAddRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Register"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Register.AddEditRegister register = new Register.AddEditRegister();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                register.FormClosed += (s, args) => overlay.Close();

                register.Show();
                register.TopMost = true;
            }
        }

        private void btnDashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Dashboard"))
            {
                openMdiChildForm(typeof(Dashboard.Dashboard));
            }
        }

        private void btnRaccProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Products"))
            {
                openMdiChildForm(typeof(Product.Products));
            }
        }

        private void btnRaccCustomers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Customers"))
            {
                openMdiChildForm(typeof(Customer.Customers));
            }
        }

        private void btnUserRoles_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Roles"))
            {
                openMdiChildForm(typeof(Role.Roles));
            }
        }

        private void btnUserPermissions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Permissions"))
            {
                openMdiChildForm(typeof(Permission.Permissions));
            }
        }

        public void closeRegister()
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

        private void btnCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void todaySummary()
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

        private void btnTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnAllTasks_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Tasks"))
            {
                openMdiChildForm(typeof(TodoList.TodoLists));
            }
        }

        private void btnPos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
            }
        }

        private void btnStocks_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Stocks"))
            {
                openMdiChildForm(typeof(Stock.Stocks));
            }
        }

        private void btnReportStocks_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Stocks Report"))
            {
                openMdiChildForm(typeof(Stock.Stocks));
            }
        }

        private void btnAllAdjustments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Adjustments"))
            {
                openMdiChildForm(typeof(Stock.Adjustment.Adjustments));
            }
        }

        private void btnAddAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Stock.Adjustment.AddEditAdjustment adjustment = new Stock.Adjustment.AddEditAdjustment();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                adjustment.FormClosed += (s, args) => overlay.Close();

                adjustment.Show();
                adjustment.TopMost = true;
            }
        }

        private void btnTransfers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Transfers"))
            {
                openMdiChildForm(typeof(Stock.Transfer.Transfers));
            }
        }

        private void btnAddTransfer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Transfer"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Stock.Transfer.AddEditTransfer transfer = new Stock.Transfer.AddEditTransfer();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                transfer.FormClosed += (s, args) => overlay.Close();

                transfer.Show();
                transfer.TopMost = true;
            }
        }

        private void btnReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Returns"))
            {
                openMdiChildForm(typeof(Sale.Return.Returns));
            }
        }

        private void btnQuickReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Return"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Sale.Return.AddEditReturn addEditReturn = new Sale.Return.AddEditReturn(true);

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditReturn.FormClosed += (s, args) => overlay.Close();

                addEditReturn.Show();
                addEditReturn.TopMost = true;
            }
        }

        private void btnPurchaseReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Purchase Returns"))
            {
                openMdiChildForm(typeof(Purchase.Return.Returns));
            }
        }

        private void btnAddReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Purchase Return"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Purchase.Return.AddEditReturn addEditReturn = new Purchase.Return.AddEditReturn(true);

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditReturn.FormClosed += (s, args) => overlay.Close();

                addEditReturn.Show();
                addEditReturn.TopMost = true;
            }
        }

        private void btnAuditTrails_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Audit Trails"))
            {
                openMdiChildForm(typeof(AuditTrail.AuditTrail));
            }
        }

        private void btnAddUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add User"))
            {
                openMdiChildForm(typeof(User.AddEditUser));
            }
        }

        private void btnAlertQuantity_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Alert Quantity"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                alertQuantity.FormClosed += (s, args) => overlay.Close();

                alertQuantity.Show();
                alertQuantity.TopMost = true;
            }
        }

        private void btnAllInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Inventories"))
            {
                openMdiChildForm(typeof(Inventory.Inventories));
            }
        }

        private void btnInventoriesHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Inventories Histories"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Inventory.InventoryHistory inventoryHistory = new Inventory.InventoryHistory();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                inventoryHistory.FormClosed += (s, args) => overlay.Close();

                inventoryHistory.Show();
                inventoryHistory.TopMost = true;
            }
        }

        private void btnAddInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Inventory"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Inventory.AddEditInventory addEditInventory = new Inventory.AddEditInventory();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditInventory.FormClosed += (s, args) => overlay.Close();

                addEditInventory.Show();
                addEditInventory.TopMost = true;
            }
        }

        private void btnAllTiers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Customer Tiers"))
            {
                openMdiChildForm(typeof(CardFidelity.Tier.Tiers));
            }
        }

        private void btnAddTier_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Customer Tier"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                CardFidelity.Tier.AddEditTier addEditTier = new CardFidelity.Tier.AddEditTier();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditTier.FormClosed += (s, args) => overlay.Close();

                addEditTier.Show();
                addEditTier.TopMost = true;
            }
        }

        private void btnAllRewards_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Rewards"))
            {
                openMdiChildForm(typeof(CardFidelity.Reward.Rewards));
            }
        }

        private void btnAddReward_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Reward"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                CardFidelity.Reward.AddEditReward addEditReward = new CardFidelity.Reward.AddEditReward();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditReward.FormClosed += (s, args) => overlay.Close();

                addEditReward.Show();
                addEditReward.TopMost = true;
            }
        }

        private void btnAllActivityLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Activity Logs"))
            {
                openMdiChildForm(typeof(CardFidelity.ActivityLog.ActivityLogs));
            }
        }

        private void btnAllRedemptions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Redemptions"))
            {
                openMdiChildForm(typeof(CardFidelity.Redemption.Redemptions));
            }
        }

        private void btnAddRedemption_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Redemption"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                CardFidelity.Redemption.AddEditRedemption addEditRedemption = new CardFidelity.Redemption.AddEditRedemption();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditRedemption.FormClosed += (s, args) => overlay.Close();

                addEditRedemption.Show();
                addEditRedemption.TopMost = true;
            }
        }

        private void btnAllRewardHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Reward History"))
            {
                openMdiChildForm(typeof(CardFidelity.RewardHistory.RewardHistories));
            }
        }

        private void btnAllTransactions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Card Fidelity Transaction"))
            {
                openMdiChildForm(typeof(CardFidelity.Transaction.Transactions));
            }
        }

        private void btnAllTaxes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Taxes"))
            {
                openMdiChildForm(typeof(Tax.Taxes));
            }
        }

        private void btnAddTax_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Tax"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Tax.AddEditTax addEditTax = new Tax.AddEditTax();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditTax.FormClosed += (s, args) => overlay.Close();

                addEditTax.Show();
                addEditTax.TopMost = true;
            }
        }

        private void btnAllPrinters_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Printers"))
            {
                openMdiChildForm(typeof(Printer.Printers));
            }
        }

        private void btnAddPrinter_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Printer"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Printer.AddEditPrinter addEditPrinter = new Printer.AddEditPrinter();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditPrinter.FormClosed += (s, args) => overlay.Close();

                addEditPrinter.Show();
                addEditPrinter.TopMost = true;
            }
        }

        private void btnAllPromotions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Promotions"))
            {
                openMdiChildForm(typeof(Product.Promotion.Promotions));
            }
        }

        private void btnAddPromotion_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Promotion"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Promotion.AddEditPromotion addEditPromotion = new Product.Promotion.AddEditPromotion();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditPromotion.FormClosed += (s, args) => overlay.Close();

                addEditPromotion.Show();
                addEditPromotion.TopMost = true;
            }
        }

        private void btnAllDepartments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Departments"))
            {
                openMdiChildForm(typeof(Employee.Department.Departments));
            }
        }

        private void btnAddDepartment_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Department"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Department.AddEditDepartment addEditDepartment = new Employee.Department.AddEditDepartment();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditDepartment.FormClosed += (s, args) => overlay.Close();

                addEditDepartment.Show();
                addEditDepartment.TopMost = true;
            }
        }

        private void btnAllPositions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Positions"))
            {
                openMdiChildForm(typeof(Employee.Position.Positions));
            }
        }

        private void btnAddPosition_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Position"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Position.AddEditPosition addEditPosition = new Employee.Position.AddEditPosition();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditPosition.FormClosed += (s, args) => overlay.Close();

                addEditPosition.Show();
                addEditPosition.TopMost = true;
            }
        }

        private void btnAllEmployees_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Employees"))
            {
                openMdiChildForm(typeof(Employee.Employees));
            }
        }

        private void btnAddEmployee_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.AddEditEmployee addEditEmployee = new Employee.AddEditEmployee();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditEmployee.FormClosed += (s, args) => overlay.Close();

                addEditEmployee.Show();
                addEditEmployee.TopMost = true;
            }
        }

        private void btnAllAttendances_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Attendances"))
            {
                openMdiChildForm(typeof(Employee.Attendance.Attendances));
            }
        }

        private void btnAddAttendance_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Attendance"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Attendance.AddEditAttendance addEditAttendance = new Employee.Attendance.AddEditAttendance();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditAttendance.FormClosed += (s, args) => overlay.Close();

                addEditAttendance.Show();
                addEditAttendance.TopMost = true;
            }
        }

        private void btnAllPayrolls_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Payrolls"))
            {
                openMdiChildForm(typeof(Employee.Payroll.Payrolls));
            }
        }

        private void btnAddPayroll_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Payroll"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Payroll.AddEditPayroll addEditPayroll = new Employee.Payroll.AddEditPayroll();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditPayroll.FormClosed += (s, args) => overlay.Close();

                addEditPayroll.Show();
                addEditPayroll.TopMost = true;
            }
        }

        private void btnAllWastes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Wastes"))
            {
                openMdiChildForm(typeof(Waste.Wastes));
            }
        }

        private void btnAllFields_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Fields"))
            {
                openMdiChildForm(typeof(Product.Field.Fields));
            }
        }

        private void btnAddField_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Field"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Product.Field.AddEditField addEditField = new Product.Field.AddEditField();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditField.FormClosed += (s, args) => overlay.Close();

                addEditField.Show();
                addEditField.TopMost = true;
            }
        }

        private void btnAddWaste_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Waste"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Waste.AddEditWaste addEditWaste = new Waste.AddEditWaste();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditWaste.FormClosed += (s, args) => overlay.Close();

                addEditWaste.Show();
                addEditWaste.TopMost = true;
            }
        }

        private void btnAllSkills_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Skills"))
            {
                openMdiChildForm(typeof(Employee.Skill.Skills));
            }
        }

        private void btnAddSkill_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Skill"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Skill.AddEditSkill addEditSkill = new Employee.Skill.AddEditSkill();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditSkill.FormClosed += (s, args) => overlay.Close();

                addEditSkill.Show();
                addEditSkill.TopMost = true;
            }
        }

        private void btnAllEvaluations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Skills"))
            {
                openMdiChildForm(typeof(Employee.Evaluation.Evaluations));
            }
        }

        private void btnAddEvaluation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Skill"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.Evaluation.AddEditEvaluation addEditEvaluation = new Employee.Evaluation.AddEditEvaluation();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditEvaluation.FormClosed += (s, args) => overlay.Close();

                addEditEvaluation.Show();
                addEditEvaluation.TopMost = true;
            }
        }

        private void btnAllReviews_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Employees Reviews"))
            {
                openMdiChildForm(typeof(Employee.EmployeePerformanceReview.EmployeeReviews));
            }
        }

        private void btnAddReview_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee Reviews"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Employee.EmployeePerformanceReview.AddEditEmployeeReview addEditEmployeeReview = new Employee.EmployeePerformanceReview.AddEditEmployeeReview();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                addEditEmployeeReview.FormClosed += (s, args) => overlay.Close();

                addEditEmployeeReview.Show();
                addEditEmployeeReview.TopMost = true;
            }
        }

        private void btnAllLocations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Locations"))
            {
                openMdiChildForm(typeof(Location.Locations));
            }
        }

        public void lockScreen()
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

        private void btnLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnELockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnALockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnBLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnCLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnDLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnFLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnGLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnHLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnILockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnMLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnRLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnSLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnYLockScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.lockScreen();
        }

        private void btnACloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnBCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnCCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnDCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnECloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnFCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnGCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnHCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnICloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnMCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnRCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnSCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnYCloseRegister_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.closeRegister();
        }

        private void btnATodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnBTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnCTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnDTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnETodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnFTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnGTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnHTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnITodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnMTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnRTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnSTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnYTodaySummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.todaySummary();
        }

        private void btnSelectBusinessLocationsLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void btnAuditsTrails_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Audit Trail"))
            {
                openMdiChildForm(typeof(AuditTrail.AuditTrail));
            }
        }

        private void bbiEmployees_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Employees"))
            {
                openMdiChildForm(typeof(Employee.Employees));
            }
        }

        private void bbiAttendances_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Attendances"))
            {
                openMdiChildForm(typeof(Employee.Attendance.AddEditAttendance));
            }
        }

        private void bbiPayrolls_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Payrolls"))
            {
                openMdiChildForm(typeof(Employee.Payroll.Payrolls));
            }
        }

        private void bbiDepartments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Departments"))
            {
                openMdiChildForm(typeof(Employee.Department.Departments));
            }
        }

        private void bbiEmployeePerformanceReview_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Employee Reviews"))
            {
                openMdiChildForm(typeof(Employee.EmployeePerformanceReview.EmployeeReviews));
            }
        }

        private void bbiEvaluations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Evaluations"))
            {
                openMdiChildForm(typeof(Employee.Evaluation.Evaluations));
            }
        }

        private void bbiPositions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Positions"))
            {
                openMdiChildForm(typeof(Employee.Position.Positions));
            }
        }

        private void bbiSkills_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Skills"))
            {
                openMdiChildForm(typeof(Employee.Skill.Skills));
            }
        }

        private void bbiListAttendances_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Attendances"))
            {
                openMdiChildForm(typeof(Employee.Attendance.Attendances));
            }
        }

        private void btnUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Users"))
            {
                openMdiChildForm(typeof(User.Users));
            }
        }

        private void bbiExpenses_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Expenses"))
            {
                openMdiChildForm(typeof(Expense.Expenses));
            }
        }

        private void bbiCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Expense Categories"))
            {
                openMdiChildForm(typeof(Expense.Category.ExpenseCategories));
            }
        }

        private void bbiWastes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Wastes"))
            {
                openMdiChildForm(typeof(Waste.Wastes));
            }
        }

        private void btnAllUserRoles_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Roles"))
            {
                openMdiChildForm(typeof(Role.Roles));
            }
        }

        private void btnAllUserPermissions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Permissions"))
            {
                openMdiChildForm(typeof(Permission.Permissions));
            }
        }

        private void bbiAllSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Settings"))
            {
                openMdiChildForm(typeof(Setting.Settings));
            }
        }

        private void btnSettingCurrencies_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Currencies"))
            {
                openMdiChildForm(typeof(Forms.Currency.Currencies));
            }
        }

        private void btnSettingRegisters_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Registers"))
            {
                openMdiChildForm(typeof(Register.Registers));
            }
        }

        private void btnSettingPrinters_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Printers"))
            {
                openMdiChildForm(typeof(Printer.Printers));
            }
        }

        private void btnSettingLocations_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Locations"))
            {
                openMdiChildForm(typeof(Location.Locations));
            }
        }

        private void bbiReminder_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Alert Quantity"))
            {
                Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();
                alertQuantity.ShowDialog();
            }
        }

        private void btnLogout_ItemClick(object sender, ItemClickEventArgs e)
        {
            Logout();
        }

        private void bbiStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Statistics"))
            {
                openMdiChildForm(typeof(Statistics.Statistics));
            }
        }

        private void bbiReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Reports"))
            {
                openMdiChildForm(typeof(OrderReport.Report));
            }
        }

        private void bbiPos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                PosScreen pos = new PosScreen();
                pos.ShowDialog();
            }
        }

        private void bbiSales_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Sales"))
            {
                openMdiChildForm(typeof(Sale.Sales));
            }
        }

        private void bbiMoreSalesHistories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Pos"))
            {
                Sale.SaleHistory saleHistory = new Sale.SaleHistory();
                saleHistory.ShowDialog();
            }
        }

        private void bbiMoreSalesReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Sale Returns"))
            {
                openMdiChildForm(typeof(Sale.Return.Returns));
            }
        }

        private void bbiPurchases_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Purchases"))
            {
                openMdiChildForm(typeof(Purchase.Purchases));
            }
        }

        private void bbiMorePurchaseHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Purchase History"))
            {
                Purchase.PurchaseHistory purchaseHistory = new Purchase.PurchaseHistory();
                purchaseHistory.ShowDialog();
            }
        }

        private void bbiMorePurchasesReturns_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Purchases Returns"))
            {
                openMdiChildForm(typeof(Purchase.Return.Returns));
            }
        }

        private void bbiProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Products"))
            {
                openMdiChildForm(typeof(Product.Products));
            }
        }

        private void bbiMoreCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Categories"))
            {
                openMdiChildForm(typeof(Product.Category.Categories));
            }
        }

        private void bbiMoreBrands_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Brands"))
            {
                openMdiChildForm(typeof(Brand.Brands));
            }
        }

        private void bbiMorePriceGroups_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Price Groups"))
            {
                openMdiChildForm(typeof(Product.PriceGroup.PriceGroups));
            }
        }

        private void bbiMoreFields_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Fields"))
            {
                openMdiChildForm(typeof(Product.Field.Fields));
            }
        }

        private void bbiMorePromotions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Promotions"))
            {
                openMdiChildForm(typeof(Product.Promotion.Promotions));
            }
        }

        private void bbiMoreUnits_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Units"))
            {
                openMdiChildForm(typeof(Product.Unit.Units));
            }
        }

        private void bbiMoreWarranties_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Warranties"))
            {
                openMdiChildForm(typeof(Product.Warranty.Warranties));
            }
        }

        private void bbiSuppliers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Suppliers"))
            {
                openMdiChildForm(typeof(Supplier.Suppliers));
            }
        }

        private void bbiStocks_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Stocks"))
            {
                openMdiChildForm(typeof(Stock.Stocks));
            }
        }

        private void bbiMoreAdjustments_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Adjustments"))
            {
                openMdiChildForm(typeof(Stock.Adjustment.Adjustments));
            }
        }

        private void bbiMoreTransfers_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Transfers"))
            {
                openMdiChildForm(typeof(Stock.Transfer.Transfers));
            }
        }

        private void bbiInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Inventories"))
            {
                openMdiChildForm(typeof(Inventory.Inventories));
            }
        }

        private void bbiWarehouse_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Warehouses"))
            {
                openMdiChildForm(typeof(Product.Warehouse.Warehouses));
            }
        }

        private void bbiMoreBackups_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Backups"))
            {
                openMdiChildForm(typeof(Setting.Backups));
            }
        }

        private void bbiTaxes_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Taxes"))
            {
                openMdiChildForm(typeof(Tax.Taxes));
            }
        }

        private void BtnItemComplaints_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Complaints"))
            {
                openMdiChildForm(typeof(Employee.Complaints.Complaints));
            }
        }

        private void BtnItemComplaintsCategories_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("List Complaints Categories"))
            {
                openMdiChildForm(typeof(Employee.Complaints.Category.ComplaintsCategories));
            }
        }

        private void BtnItemLoyaltyCards_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Loyalty Card"))
            {
                openMdiChildForm(typeof(LoyaltyForms.LoyaltyCardForm));
            }
        }

        /// <summary>
        /// Déconnexion sécurisée de l'utilisateur
        /// </summary>
        public void Logout()
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmLogout = lang == "en" ? "Are you sure you want to logout?"
                                 : lang == "fr" ? "Êtes-vous sûr de vouloir vous déconnecter?"
                                 : "هل أنت متأكد أنك تريد تسجيل الخروج؟";

            string logout = lang == "en" ? "Logout"
                          : lang == "fr" ? "Déconnexion"
                          : "تسجيل خروج";

            var result = DevExpress.XtraEditors.XtraMessageBox.Show(
                confirmLogout,
                logout,
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question
            );

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                // Révoquer la session
                Function.SessionManager.RevokeCurrentSession();

                // Fermer MainFrm
                this.Close();

                // Retourner au Login
                Forms.Auth.Login loginForm = new Forms.Auth.Login();
                loginForm.ShowDialog();

                // Fermer l'application si le login est annulé
                Application.Exit();
            }
        }
    }
}