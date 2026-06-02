using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Forms.Report;
using Pos.Function;
using Pos.Models;
using Pos.Report.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio.TwiML.Voice;

namespace Pos.Forms.Customer
{
    public partial class Customers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemEditCustomer;
        private ToolStripMenuItem menuItemDeleteCustomer;
        private ToolStripMenuItem menuItemViewDetails;
        private ToolStripMenuItem menuItemFeePayment;
        private ToolStripMenuItem menuItemFeeMail;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int customer_id = 0;
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

        public Customers()
        {
            InitializeComponent();
            InitializeContextMenu();

            this.toRtl();

            // Setup the timer
            searchTimer.Interval = 300; // Delay for 300 milliseconds
            searchTimer.Tick += SearchTimer_Tick;

            gridViewCustomers.MouseUp += gridView_MouseUp;

            this.BorderStyle();

            // style the grid view
            gridViewCustomers.RowStyle += gridViewCustomers_RowStyle;
            gridViewCustomers.FocusedRowChanged += gridViewCustomers_FocusedRowChanged;
            gridViewCustomers.CustomDrawCell += gridViewCustomers_CustomDrawCell;
            gridViewCustomers.RowHeight = Function.Helper.RowHeight;
            gridViewCustomers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCustomers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    //e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
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
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
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

            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup11.AppearanceGroup.Font = customFont;
            layoutControlGroup18.AppearanceGroup.Font = customFont;
            layoutControlGroup17.AppearanceGroup.Font = customFont;

            btnBadgePrint.Font = customFont;
            btnPayDue.Font = customFont;
            btnRefreshItems.Font = customFont;
            btnDeleteItem.Font = customFont;
            btnPrintItems.Font = customFont;
            btnEditItem.Font = customFont;
            btnAddItem.Font = customFont;
            btnCloseFrm.Font = customFont;
            btnFeePayment.Font = customFont;
            btnViewDetails.Font = customFont;
            btnSendMailToCustomers.Font = customFont;

            btnCustomersLastMonth.Font = customFont;
            btnCustomersOfTheMonth.Font = customFont;
            btnCustomersOfTheWeek.Font = customFont;
            btnTodayCustomers.Font = customFont;
            btnCustomersThisYear.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
            toggleSwitchBarcode.Font = customFont10;
            layoutControlItem35.AppearanceItemCaption.Font = customFont10;
            layoutControlItem24.AppearanceItemCaption.Font = customFont10;
            layoutControlItem23.AppearanceItemCaption.Font = customFont10;
            layoutControlItem22.AppearanceItemCaption.Font = customFont10;
            layoutControlItem21.AppearanceItemCaption.Font = customFont10;
            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewCustomers.Columns)
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

        private void Customers_Load(object sender, EventArgs e)
        {
            this.switchBtns();
            this.loadCustomers();
        }

        public void switchBtns()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1).AddDays(1).AddTicks(-1); // Last second of the previous month
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1); // End of the current month
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1); // End of the week

            int countbtnCustomersLastMonth = 0;
            int countbtnCustomersOfTheMonth = 0;
            int countbtnCustomersOfTheWeek = 0;
            int countbtnTodayCustomers = 0;
            int countbtnCustomersThisYear = 0;

            string lang = Properties.Settings.Default.Lang;

            // Message localization based on the language setting
            string msgCountbtnCustomersLastMonth;
            string msgCountbtnCustomersOfTheMonth;
            string msgCountbtnCustomersOfTheWeek;
            string msgCountbtnTodayCustomers;
            string msgCountbtnCustomersThisYear;

            if (lang == "en")
            {
                msgCountbtnCustomersLastMonth = "Customers Last Month";
                msgCountbtnCustomersOfTheMonth = "Customers Of The Month";
                msgCountbtnCustomersOfTheWeek = "Customers Of The Week";
                msgCountbtnTodayCustomers = "Today's Customers";
                msgCountbtnCustomersThisYear = "Customers This Year";
            }
            else if (lang == "fr")
            {
                msgCountbtnCustomersLastMonth = "Clients le mois dernier";
                msgCountbtnCustomersOfTheMonth = "Clients du mois";
                msgCountbtnCustomersOfTheWeek = "Clients de la semaine";
                msgCountbtnTodayCustomers = "Clients du jour";
                msgCountbtnCustomersThisYear = "Clients cette année";
            }
            else
            {
                msgCountbtnCustomersLastMonth = "الشهر الماضي للعملاء";
                msgCountbtnCustomersOfTheMonth = "عملاء الشهر";
                msgCountbtnCustomersOfTheWeek = "عملاء الأسبوع";
                msgCountbtnTodayCustomers = "عملاء اليوم";
                msgCountbtnCustomersThisYear = "العملاء هذا العام";
            }

            using (var context = new AppDbContext())
            {
                // Count customers from last month
                countbtnCustomersLastMonth = context.Customers
                    .Where(c => c.CreatedAt >= startOfLastMonth && c.CreatedAt <= endOfLastMonth)
                    .Count();
                btnCustomersLastMonth.Text = $"( {countbtnCustomersLastMonth} ) {msgCountbtnCustomersLastMonth}";

                // Count customers from the current month
                countbtnCustomersOfTheMonth = context.Customers
                    .Where(c => c.CreatedAt >= startOfMonth && c.CreatedAt <= endOfMonth)
                    .Count();
                btnCustomersOfTheMonth.Text = $"( {countbtnCustomersOfTheMonth} ) {msgCountbtnCustomersOfTheMonth}";

                // Count customers from the current week
                countbtnCustomersOfTheWeek = context.Customers
                    .Where(c => c.CreatedAt >= startOfWeek && c.CreatedAt <= endOfWeek)
                    .Count();
                btnCustomersOfTheWeek.Text = $"( {countbtnCustomersOfTheWeek} ) {msgCountbtnCustomersOfTheWeek}";

                // Count customers created today
                countbtnTodayCustomers = context.Customers
                    .Where(c => c.CreatedAt >= today && c.CreatedAt < tomorrow)
                    .Count();
                btnTodayCustomers.Text = $"( {countbtnTodayCustomers} ) {msgCountbtnTodayCustomers}";

                // Count customers for tomorrow (this year logic should be updated as needed)
                countbtnCustomersThisYear = context.Customers
                    .Where(c => c.CreatedAt >= tomorrow && c.CreatedAt < tomorrow.AddDays(1))
                    .Count();
                btnCustomersThisYear.Text = $"( {countbtnCustomersThisYear} ) {msgCountbtnCustomersThisYear}";
            }
        }

        public void loadCustomers()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Customers
                .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.FullName.Contains(searchTerm) ||
                        p.Phone.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm) ||
                        p.Gender.Contains(searchTerm) ||
                        p.Status.Contains(searchTerm) ||
                        p.Address.Contains(searchTerm))
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
                var currentPageData = context.Customers
                    .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.FullName.Contains(searchTerm) ||
                        p.Phone.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm) ||
                        p.Gender.Contains(searchTerm) ||
                        p.Status.Contains(searchTerm) ||
                        p.Address.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlCustomers.DataSource = currentPageData;
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

        public void customerEdit()
        {
            if (Function.Permission.HasPermission("Edit Customer"))
            {
                AddEditCustomer customer = new AddEditCustomer();
                customer.setCustomersObject(this);
                customer.setTypeOperation("Edit");
                customer.ShowDialog();
            }
        }

        public void customerDelete()
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

            if (Function.Permission.HasPermission("Delete Customer"))
            {
                using (var context = new AppDbContext())
                {
                    this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());

                    Models.Customer customer = context.Customers.Find(this.customer_id);

                    if (customer != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.Customers.Remove(customer);
                        context.SaveChanges();
                        this.loadCustomers();
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

        private void gridViewCustomers_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewCustomers.RowCount > 0 && gridViewCustomers.FocusedRowHandle >= 0)
            {
                Function.Sound.Selected();
                this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());
                this.customerEdit();
            }
        }

        private void gridViewCustomers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Function.Sound.Selected();

            if (gridViewCustomers.RowCount > 0 && gridViewCustomers.FocusedRowHandle >= 0)
            {
                this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());
                this.loadCsutomer();
            }
        }

        private void repEditCustomer_Click(object sender, EventArgs e)
        {
            Function.Sound.Selected();


            this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());
            this.customerEdit();

        }

        private void repDeleteCustomer_Click(object sender, EventArgs e)
        {
            this.customerDelete();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Customer"))
            {
                AddEditCustomer customer = new AddEditCustomer();
                customer.setCustomersObject(this);
                customer.setTypeOperation("Add");
                customer.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a customer.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un client.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد العميل.";
            }

            if (this.customer_id != 0)
            {
                this.customerEdit();
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
                pleaseSelect = "Please select a customer.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un client.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد العميل.";
            }

            if (this.customer_id != 0)
            {
                this.customerDelete();
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlCustomers);
            customPrint.PrintGridControl(gridViewCustomers);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            Function.Sound.Selected();
            this.loadCustomers();
        }

        private void btnFeePayment_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Fee Payment"))
            {
                string lang = Properties.Settings.Default.Lang;

                string pleaseSelect;

                if (lang == "en")
                {
                    pleaseSelect = "Please select a customer.";
                }
                else if (lang == "fr")
                {
                    pleaseSelect = "Veuillez sélectionner un client.";
                }
                else
                {
                    pleaseSelect = "الرجاء تحديد العميل.";
                }

                if (this.customer_id != 0)
                {
                    Payment payment = new Payment(this.customer_id);
                    payment.setObject(this);
                    payment.ShowDialog();
                }
                else
                {
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                    alertMessage.ShowDialog();
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("View Customer Details"))
            {
                string lang = Properties.Settings.Default.Lang;

                string pleaseSelect;

                if (lang == "en")
                {
                    pleaseSelect = "Please select a customer.";
                }
                else if (lang == "fr")
                {
                    pleaseSelect = "Veuillez sélectionner un client.";
                }
                else
                {
                    pleaseSelect = "الرجاء تحديد العميل.";
                }

                if (this.customer_id != 0)
                {
                    CustomerDetails details = new CustomerDetails(this.customer_id);
                    details.ShowDialog();
                }
                else
                {
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                    alertMessage.ShowDialog();
                }
            }
        }

        private void InitializeContextMenu()
        {
            string lang = Properties.Settings.Default.Lang;

            string msgMenuItemEditCustomer;
            string msgMenuItemDeleteCustomer;
            string msgMenuItemViewDetails;
            string msgMenuItemFeePayment;
            string msgMenuItemFeeMail;

            if (lang == "en")
            {
                msgMenuItemEditCustomer = "Edit Customer";
                msgMenuItemDeleteCustomer = "Delete Customer";
                msgMenuItemViewDetails = "View Details";
                msgMenuItemFeePayment = "Fee Payment";
                msgMenuItemFeeMail = "Send mail about the fees";
            }
            else if (lang == "fr")
            {
                msgMenuItemEditCustomer = "Modifier le client";
                msgMenuItemDeleteCustomer = "Supprimer le client";
                msgMenuItemViewDetails = "Afficher les détails";
                msgMenuItemFeePayment = "Paiement des frais";
                msgMenuItemFeeMail = "Envoyer un mail concernant les frais";
            }
            else
            {
                msgMenuItemEditCustomer = "تحرير العميل";
                msgMenuItemDeleteCustomer = "حذف العميل";
                msgMenuItemViewDetails = "عرض التفاصيل";
                msgMenuItemFeePayment = "دفع الرسوم";
                msgMenuItemFeeMail = "أرسل بريدًا بخصوص الرسوم";
            }

            contextMenu = new ContextMenuStrip();
            menuItemEditCustomer = new ToolStripMenuItem(msgMenuItemEditCustomer);
            menuItemDeleteCustomer = new ToolStripMenuItem(msgMenuItemDeleteCustomer);
            menuItemViewDetails = new ToolStripMenuItem(msgMenuItemViewDetails);
            menuItemFeePayment = new ToolStripMenuItem(msgMenuItemFeePayment);
            menuItemFeeMail = new ToolStripMenuItem(msgMenuItemFeeMail);

            menuItemEditCustomer.Image = Properties.Resources.rightclick_suitcase;
            menuItemDeleteCustomer.Image = Properties.Resources.rightclick_remove;
            menuItemViewDetails.Image = Properties.Resources.rightclick_preview_pane;
            menuItemFeePayment.Image = Properties.Resources.rightclick_money_bag;
            menuItemFeeMail.Image = Properties.Resources.rightclick_mail;

            contextMenu.Items.AddRange(new ToolStripItem[] { menuItemEditCustomer, menuItemDeleteCustomer, menuItemViewDetails, menuItemFeePayment, menuItemFeeMail });

            menuItemEditCustomer.Click += MenuItemEditCustomer_Click;
            menuItemDeleteCustomer.Click += MenuItemDeleteCustomer_Click;
            menuItemViewDetails.Click += MenuItemViewDetails_Click;
            menuItemFeePayment.Click += MenuItemFeePayment_Click;
            menuItemFeeMail.Click += MenuItemFeeMail_Click;
        }

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridViewCustomers.RowCount > 0 && gridViewCustomers.FocusedRowHandle >= 0)
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

        private void MenuItemEditCustomer_Click(object sender, EventArgs e)
        {
            this.customerEdit();
        }

        private void MenuItemDeleteCustomer_Click(object sender, EventArgs e)
        {
            this.customerDelete();
        }

        private void MenuItemViewDetails_Click(object sender, EventArgs e)
        {
            Details details = new Details(this.customer_id);
            details.setObject(this);
            details.ShowDialog();
        }

        private void MenuItemFeePayment_Click(object sender, EventArgs e)
        {
            Payment payment = new Payment(this.customer_id);
            payment.setObject(this);
            payment.ShowDialog();
        }

        private void MenuItemFeeMail_Click(object sender, EventArgs e)
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

            Sound.Selected();

            this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());

            var setting = Function.Helper.getSetting();

            if (setting != null)
            {
                using (var context = new AppDbContext())
                {
                    Models.Customer customer = context.Customers.Find(this.customer_id);

                    if (customer != null)
                    {
                        if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //string bodyMail = Function.Helper.CreateInvoiceEmailBody(customer, Function.Helper.ConvertToBase64String(setting.Logo), setting.Company, setting.Address);
                            //Function.Email.SendEmail(customer.Email, "Invoice Due : " + customer.FullName, bodyMail);
                            //Sound.Selected();
                            //MessageBox.Show(messagesSentSuccessfully);
                        }
                        else
                        {
                            Sound.Wrong();
                        }
                    }
                }
            }
        }

        private void btnSendMailToCustomers_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string messagesSentSuccessfully;
            string thereNoDueToPay;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to Send emails to all customers ?";
                messagesSentSuccessfully = "Messages sent successfully!";
                thereNoDueToPay = "There No Due To Pay";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Voulez-vous vraiment envoyer des e-mails à tous les clients ?";
                messagesSentSuccessfully = "Messages envoyés avec succès !";
                thereNoDueToPay = "Il n'y a aucun dû à payer";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في إرسال رسائل البريد الإلكتروني إلى كافة العملاء؟";
                messagesSentSuccessfully = "تم إرسال الرسائل بنجاح!";
                thereNoDueToPay = "ليس هناك مستحقات للدفع";
            }

            if (Function.Permission.HasPermission("Send emails to all customers"))
            {
                if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        var customers = context.Customers.Where(d => d.CurrentDue > 0).ToList();

                        if (customers.Any())
                        {
                            var setting = Function.Helper.getSetting();

                            foreach (var customer in customers)
                            {
                                //string bodyMail = Function.Helper.CreateInvoiceEmailBody(customer, Function.Helper.ConvertToBase64String(setting.Logo), setting.Company, setting.Address);
                                //Function.Email.SendEmail(customer.Email, invoiceDue + customer.FullName, bodyMail);
                            }

                            Sound.Selected();
                            MessageBox.Show(messagesSentSuccessfully);
                        }
                        else
                        {
                            Sound.Selected();
                            AlertMessageBox alertMessage = new AlertMessageBox(thereNoDueToPay);
                            alertMessage.ShowDialog();
                        }
                    }
                }
                else
                {
                    Sound.Wrong();
                }
            }
        }

        private void repBagdePrint_Click(object sender, EventArgs e)
        {
            this.customer_id = int.Parse(gridViewCustomers.GetRowCellValue(gridViewCustomers.FocusedRowHandle, "Id").ToString());
            this.printCustomerBadge();
        }

        public void printbadge(int customer_id)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string printerError;
            string isNotValid;
            string printer;
            string anErrorOccurred;
            string validationError;
            string validatedData;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to print the badge ?";
                printerError = "Printer Error";
                isNotValid = "is Not Valid";
                printer = "Printer";
                anErrorOccurred = "An error occurred while printing:";
                validationError = "Validation Error";
                validatedData = "You can't print without validated data.";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir imprimer le badge ?";
                printerError = "Erreur d'imprimante";
                isNotValid = "n'est pas valide";
                printer = "Imprimante";
                anErrorOccurred = "Une erreur s'est produite lors de l'impression :";
                validationError = "Erreur de validation";
                validatedData = "Vous ne pouvez pas imprimer sans données validées.";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في طباعة الشارة؟";
                printerError = "خطأ في الطابعة";
                isNotValid = "غير صحيح";
                printer = "طابعة";
                anErrorOccurred = "حدث خطأ أثناء الطباعة:";
                validationError = "خطأ في التحقق";
                validatedData = "لا يمكنك الطباعة بدون بيانات تم التحقق من صحتها.";
            }

            if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterReciept;

                // Initialize the TechnicalRepiarA4 report with the maintenance_id
                CustomerCard customerCard = new CustomerCard(customer_id);
                customerCard.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"{printer} \"{printerName}\" {isNotValid}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(customerCard);

                // Set the printer name in the PrintTool
                printTool.PrinterSettings.PrinterName = printerName;

                try
                {
                    // Print the document using the specified printer
                    printTool.Print(printerName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{anErrorOccurred} {ex.Message}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Display the report using a form with a DocumentViewer
                FormView documentViewerForm = new FormView();
                documentViewerForm.CustomerBadge(customerCard);
                documentViewerForm.Show();
            }
            else
            {
                XtraMessageBox.Show(validatedData, validationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.Close();
        }

        private void btnCustomersOfTheWeek_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7);

                gridControlCustomers.DataSource = context.Customers
                    .Where(c => c.CreatedAt >= startOfWeek && c.CreatedAt < endOfWeek)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnCustomersOfTheMonth_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1);

                gridControlCustomers.DataSource = context.Customers
                    .Where(c => c.CreatedAt >= startOfMonth && c.CreatedAt < endOfMonth)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnCustomersThisYear_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                // Define the start and end of the current year
                var startOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                var endOfYear = new DateTime(DateTime.Today.Year, 12, 31).AddDays(1); // Adding one day to include the last day of the year

                // Fetch customers created within the current year
                gridControlCustomers.DataSource = context.Customers
                    .Where(c => c.CreatedAt >= startOfYear && c.CreatedAt < endOfYear)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnTodayCustomers_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                gridControlCustomers.DataSource = context.Customers
                    .Where(c => c.CreatedAt >= today && c.CreatedAt < tomorrow)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnCustomersLastMonth_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                var endOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1).AddDays(1).AddTicks(-1); // End of last month

                gridControlCustomers.DataSource = context.Customers
                    .Where(c => c.CreatedAt >= startOfLastMonth && c.CreatedAt < endOfLastMonth)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.loadCustomers();
        }

        private void searchControlCustomers_EditValueChanged(object sender, EventArgs e)
        {
            if (toggleSwitchBarcode.IsOn)
            {
                searchTimer.Stop();
                searchTimer.Start();
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            if (toggleSwitchBarcode.IsOn)
            {
                searchTimer.Stop();
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            string code = searchControlCustomers.Text.Trim();

            if (!code.IsNullOrEmpty())
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                        Models.Customer customer = context.Customers.FirstOrDefault(c => c.Code == code);

                        if (customer != null)
                        {
                            Sound.Selected();

                            this.customer_id = customer.Id;

                            // we need to do somthing here !!!!!

                            //Files files = new Files();
                            //files.setCustomerId(this.customer_id);
                            //files.ShowDialog(this);

                            searchControlCustomers.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                searchControlCustomers.Clear();
            }
        }

        public void loadCsutomer()
        {
            if (this.customer_id != 0)
            {
                using (var context = new AppDbContext())
                {
                    Models.Customer customer = context.Customers.Find(this.customer_id);

                    if (customer != null)
                    {
                        txtFullName.Text = customer.FirstName;
                        txtGender.Text = customer.Gender;
                        txtEmail.Text = customer.Email;
                        txtPhone.Text = customer.Phone;
                        customerImage.EditValue = customer.Image;
                        txtGender.Text = customer.Gender;
                        simpleLabelItemCurrentDue.Text = Function.Helper.FormatAmount(customer.CurrentDue.ToString());
                    }
                }
            }
        }

        private void btnPayDue_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a customer.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un client.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد العميل.";
            }

            if (Function.Permission.HasPermission("Fee Payment"))
            {
                if (this.customer_id != 0)
                {
                    Customer.Payment payment = new Customer.Payment(this.customer_id);
                    payment.setObject(this);
                    payment.ShowDialog();
                    this.loadCsutomer();
                }
                else
                {
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                    alertMessage.ShowDialog();
                }
            }
        }

        private Models.Customer GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (this.customer_id != 0)
                    return context.Customers.Find(this.customer_id);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                if (this.customer_id == 0)
                {
                    // If no current selection, disable all navigation buttons.
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    return;
                }

                // Retrieve the minimum and maximum ID values from the Brands dataset.
                int minId = context.Customers.Min(b => b.Id);
                int maxId = context.Customers.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = this.customer_id > minId; // Disable if on the first item
                btnNext.Enabled = this.customer_id < maxId; // Disable if on the last item
                btnEnd.Enabled = this.customer_id > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = this.customer_id < maxId; // Disable if on the last item (end of the list)

                Models.Customer currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.customer_id = currentItem.Id;
                    this.loadCsutomer();
                }
            }
        }

        private void MoveToFirst()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the first item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du premier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأول:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? minId = context.Customers.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        this.customer_id = minId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToLast()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the last item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du dernier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأخير:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? maxId = context.Customers.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        this.customer_id = maxId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToNext()
        {
            using (var context = new AppDbContext())
            {
                if (this.customer_id == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.Customers.Where(b => b.Id > this.customer_id).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    this.customer_id = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (this.customer_id != 0)
                {
                    var prevItem = context.Customers.Where(b => b.Id < this.customer_id).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        this.customer_id = prevItem.Id;
                        DisplayCurrentItem();
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnBadgePrint_Click(object sender, EventArgs e)
        {
            this.printCustomerBadge();
        }

        public void printCustomerBadge()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a customer.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un client.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد العميل.";
            }

            if (Function.Permission.HasPermission("Customer badge printing"))
            {
                if (this.customer_id != 0)
                {
                    Sound.Selected();
                    printbadge(this.customer_id);

                    ShowOverlay();
                    ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                    showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                    showCustomAlert.trnsMsgSuccess();
                    showCustomAlert.ShowDialog();
                    HideOverlay();
                }
                else
                {
                    Sound.Wrong();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                    alertMessage.ShowDialog();
                }
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
            using (var context = new AppDbContext())
            {
                if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.Customers.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControlCustomers_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControlCustomers.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadCustomers();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlCustomers, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlCustomers, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlCustomers, "xlsx");
        }
    }
}