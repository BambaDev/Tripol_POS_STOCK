using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Xml.Linq;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.Currency
{
    public partial class AddEditCurrency : DevExpress.XtraEditors.XtraForm
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Currencies currencies = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditCurrency()
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
            Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem14.AppearanceItemCaption.Font = customFont10;
            toggleSwitchDirection.Font = customFont10;
            txtStatus.Font = customFont10;
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

        public void setCurrenciesObject(Currencies currencies)
        {
            this.currencies = currencies;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
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

            if (this.currencies != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.currencies.currency_id != 0)
                        this.currentItemId = this.currencies.currency_id;

                    Models.Currency currency = context.Currencies.Find(this.currentItemId);

                    if (currency != null)
                    {
                        txtCurrency.Text = currency.Name;
                        txtCode.Text = currency.Code;
                        txtExchangeRate.EditValue = currency.ExchangeRate;
                        txtStatus.EditValue = currency.IsActive;
                        toggleSwitchDirection.EditValue = currency.Direction;
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        private void btnSaveCurrency_Click(object sender, EventArgs e)
        {
            // Get the language setting
            string lang = Properties.Settings.Default.Lang;

            // Initialize messages based on the language
            string nameAlreadyExists;
            string codeAlreadyExists;
            string pleaseSelectItem;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item !";
                nameAlreadyExists = "A Currency with this name already exists.";
                codeAlreadyExists = "A Currency with this code already exists.";
            }
            else if (lang == "fr")
            {
                nameAlreadyExists = "Une devise portant ce nom existe déjà.";
                codeAlreadyExists = "Une devise portant ce code existe déjà.";
                pleaseSelectItem = "Veuillez sélectionner l'article !";
            }
            else
            {
                nameAlreadyExists = "توجد عملة بهذا الاسم بالفعل.";
                codeAlreadyExists = "توجد عملة بهذا الرمز بالفعل.";
                pleaseSelectItem = "الرجاء تحديد العنصر!";
            }

            // Show splash screen
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            // Validate the input
            if (dxValidationProviderCurrency.Validate())
            {
                // Prepare currency object
                Models.Currency currency;

                if (this.type == "Add")
                {
                    // Check if the currency name is unique
                    if (_context.Currencies.Any(c => c.Name == txtCurrency.Text))
                    {
                        AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                        alertMessage.ShowDialog();
                        SplashScreenManager.CloseForm();
                        return;
                    }

                    // Check if the currency code is unique
                    if (_context.Currencies.Any(c => c.Code == txtCode.Text))
                    {
                        AlertMessageBox alertMessage = new AlertMessageBox(codeAlreadyExists);
                        alertMessage.ShowDialog();
                        SplashScreenManager.CloseForm();
                        return;
                    }

                    // Add new currency
                    currency = new Models.Currency
                    {
                        Name = txtCurrency.Text,
                        Code = txtCode.Text,
                        ExchangeRate = decimal.Parse(txtExchangeRate.EditValue.ToString()),
                        IsActive = txtStatus.IsOn,
                        Direction = toggleSwitchDirection.IsOn,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _context.Currencies.Add(currency);
                    _context.SaveChanges();
                    txtCurrency.Text = "";
                }
                else
                {
                    if (this.currencies != null)
                        this.currentItemId = this.currencies.currency_id;

                    // Use a fresh context for update to avoid tracking issues
                    using (var updateContext = new AppDbContext())
                    {
                        currency = updateContext.Currencies.Find(this.currentItemId);

                        if (currency != null)
                        {
                            // Check if the new currency name is unique excluding the current record
                            if (updateContext.Currencies.Any(c => c.Name == txtCurrency.Text && c.Id != currency.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                SplashScreenManager.CloseForm();
                                return;
                            }

                            // Check if the new currency code is unique excluding the current record
                            if (updateContext.Currencies.Any(c => c.Code == txtCode.Text && c.Id != currency.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(codeAlreadyExists);
                                alertMessage.ShowDialog();
                                SplashScreenManager.CloseForm();
                                return;
                            }

                            // Update existing currency
                            currency.Name = txtCurrency.Text;
                            currency.Code = txtCode.Text;
                            currency.ExchangeRate = decimal.Parse(txtExchangeRate.EditValue.ToString());
                            currency.IsActive = txtStatus.IsOn;
                            currency.Direction = toggleSwitchDirection.IsOn;
                            currency.UpdatedAt = DateTime.Now;

                            updateContext.Entry(currency).State = EntityState.Modified;
                            updateContext.SaveChanges();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            SplashScreenManager.CloseForm();
                            return;
                        }
                    }

                    // Assign currency for later use (default currency check)
                    currency = new Models.Currency
                    {
                        Id = this.currentItemId,
                        Code = txtCode.Text
                    };
                }
                Function.Sound.Added();

                // Set default currency and direction if the status is active
                if (txtStatus.IsOn)
                {
                    this.changeStatusToInactive(currency.Id);
                    Properties.Settings.Default.DefaultCurrency = txtCode.Text;
                    Properties.Settings.Default.DefaultCurrencyDirection = toggleSwitchDirection.IsOn;
                    Properties.Settings.Default.Save();
                }

                // Reload currencies
                if (this.currencies != null)
                    this.currencies.loadCurrencies();
            }
            else
            {
                Function.Sound.Wrong();
            }

            // Close splash screen
            SplashScreenManager.CloseForm();
        }

        public void changeStatusToInactive(int id)
        {
            using (var context = new AppDbContext())
            {
                var currencies = context.Currencies.Where(c => c.Id != id).ToList();

                foreach (var currency in currencies)
                {
                    currency.IsActive = false;
                }

                context.SaveChanges();
            }
        }

        private void AddEditCurrency_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Currency GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Currencies.Find(currentItemId);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                if (currentItemId == 0)
                {
                    // If no current selection, disable all navigation buttons.
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    return;
                }

                // Retrieve the minimum and maximum ID values from the Brands dataset.
                int minId = context.Currencies.Min(b => b.Id);
                int maxId = context.Currencies.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Currency currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtCurrency.Text = currentItem.Name;
                    txtCode.Text = currentItem.Code;
                    txtExchangeRate.EditValue = currentItem.ExchangeRate;
                    txtStatus.EditValue = currentItem.IsActive;
                    toggleSwitchDirection.EditValue = currentItem.Direction;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
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
                    int? minId = context.Currencies.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        currentItemId = minId.Value;
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
                    int? maxId = context.Currencies.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        currentItemId = maxId.Value;
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
                if (currentItemId == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.Currencies.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    currentItemId = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    var prevItem = context.Currencies.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        currentItemId = prevItem.Id;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";

            txtCurrency.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtExchangeRate.EditValue = 0;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeSearchLookUpEdit()
        {
            string lang = Properties.Settings.Default.Lang;

            string name;
            string writeSomething;

            if (lang == "en")
            {
                name = "Name";
                writeSomething = "Write something...";
            }
            else if (lang == "fr")
            {
                name = "nom";
                writeSomething = "Écris quelque chose...";
            }
            else
            {
                name = "اسم";
                writeSomething = "أكتب شيئا...";
            }

            using (var context = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = context.Currencies.ToList();
                searchLookUpEdit.Properties.DisplayMember = "Name";
                searchLookUpEdit.Properties.ValueMember = "Id";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("Name", name);

                searchLookUpEdit.Properties.NullText = writeSomething;

                searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string selected;
            string noItemSelected;

            if (lang == "en")
            {
                selected = "Selected value is not a valid integer";
                noItemSelected = "No item selected.";
            }
            else if (lang == "fr")
            {
                selected = "La valeur sélectionnée n'est pas un entier valide";
                noItemSelected = "Aucun élément sélectionné.";
            }
            else
            {
                selected = "القيمة المحددة ليست عددًا صحيحًا صالحًا";
                noItemSelected = "لم يتم تحديد أي عنصر.";
            }

            if (searchLookUpEdit.EditValue != null)
            {
                if (int.TryParse(searchLookUpEdit.EditValue.ToString(), out int itemId))
                {
                    this.currentItemId = itemId;
                    this.edit();
                }
                else
                {
                    Console.WriteLine(selected);
                }
            }
            else
            {
                Console.WriteLine(noItemSelected);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}