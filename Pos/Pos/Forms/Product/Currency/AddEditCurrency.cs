using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
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

namespace Pos.Forms.Product.Currency
{
    public partial class AddEditCurrency : DevExpress.XtraEditors.XtraForm
    {
        public Currencies currencies = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditCurrency()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
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
            if (this.currencies != null || currentItemId != 0)
            {
                this.currentItemId = this.currencies != null ? this.currencies.currency_id : currentItemId;

                Models.Currency currency = Shared.db.Currencies.Find(this.currentItemId);

                if (currency != null)
                {
                    txtCurrency.Text = currency.Name;
                    txtCode.Text = currency.Code;
                    txtExchangeRate.EditValue = currency.ExchangeRate;
                    txtStatus.EditValue = currency.IsActive;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        private void btnSaveCurrency_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderCurrency.Validate())
            {
                Models.Currency currency;

                if (this.type == "Add")
                {
                    currency = new Models.Currency();
                    currency.Name = txtCurrency.Text;
                    currency.Code = txtCode.Text;
                    currency.ExchangeRate = decimal.Parse(txtExchangeRate.EditValue.ToString());
                    //currency.IsActive = txtStatus.Text;
                    currency.CreatedAt = DateTime.Now;
                    currency.UpdatedAt = DateTime.Now;

                    Shared.db.Currencies.Add(currency);

                    txtCurrency.Text = "";
                }
                else
                {
                    if (this.currencies != null)
                        this.currentItemId = this.currencies.currency_id;

                    currency = Shared.db.Currencies.Find(this.currentItemId);

                    if (currency != null)
                    {
                        currency.Name = txtCurrency.Text;
                        currency.Code = txtCode.Text;
                        currency.ExchangeRate = decimal.Parse(txtExchangeRate.EditValue.ToString());
                        //currency.IsActive = txtStatus.Text;
                        currency.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(currency).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (txtStatus.Text == "Active")
                {
                    this.changeStatusToInactive(currency.Id);
                    Properties.Settings.Default.DefaultCurrency = txtCode.Text;
                }

                if (this.currencies != null)
                    this.currencies.loadCurrencies();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public void changeStatusToInactive(int id)
        {
            var currencies = Shared.db.Currencies.Where(c => c.Id != id).ToList();

            foreach (var currency in currencies)
            {
                //currency.IsActive = "InActive";
            }

            Shared.db.SaveChanges();
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
            if (currentItemId != 0)
                return Shared.db.Currencies.Find(currentItemId);
            else
                return null;
        }

        private void DisplayCurrentItem()
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
            int minId = Shared.db.Currencies.Min(b => b.Id);
            int maxId = Shared.db.Currencies.Max(b => b.Id);

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
                ////txtStatus.Text = currentItem.IsActive;
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Currencies.Min(b => (int?)b.Id);
                if (minId.HasValue)
                {
                    currentItemId = minId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the first item: " + ex.Message);
            }
        }

        private void MoveToLast()
        {
            try
            {
                int? maxId = Shared.db.Currencies.Max(b => (int?)b.Id);
                if (maxId.HasValue)
                {
                    currentItemId = maxId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the last item: " + ex.Message);
            }
        }

        private void MoveToNext()
        {
            if (currentItemId == 0)
            {
                this.MoveToFirst();
            }

            var nextItem = Shared.db.Currencies.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
            if (nextItem != null)
            {
                currentItemId = nextItem.Id;
                DisplayCurrentItem();
            }
        }

        private void MoveToPrevious()
        {
            if (currentItemId != 0)
            {
                var prevItem = Shared.db.Currencies.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                if (prevItem != null)
                {
                    currentItemId = prevItem.Id;
                    DisplayCurrentItem();
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
            searchLookUpEdit.Properties.DataSource = Shared.db.Currencies.ToList();
            searchLookUpEdit.Properties.DisplayMember = "Name";
            searchLookUpEdit.Properties.ValueMember = "Id";

            searchLookUpEdit.Properties.View.Columns.Clear();
            searchLookUpEdit.Properties.View.Columns.AddVisible("Name", "Name");

            searchLookUpEdit.Properties.NullText = "Write something...";

            searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit.EditValue != null)
            {
                if (int.TryParse(searchLookUpEdit.EditValue.ToString(), out int itemId))
                {
                    this.currentItemId = itemId;
                    this.edit();
                }
                else
                {
                    Console.WriteLine("Selected value is not a valid integer");
                }
            }
            else
            {
                Console.WriteLine("No item selected.");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}