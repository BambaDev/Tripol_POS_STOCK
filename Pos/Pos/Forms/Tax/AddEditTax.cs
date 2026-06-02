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

namespace Pos.Forms.Tax
{
    public partial class AddEditTax : DevExpress.XtraEditors.XtraForm
    {
        public Taxes taxes = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditTax()
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

        public void setTaxesObject(Taxes taxes)
        {
            this.taxes = taxes;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.taxes != null || currentItemId != 0)
            {
                this.currentItemId = this.taxes != null ? this.taxes.tax_id : currentItemId;

                Models.Tax tax = Shared.db.Taxes.Find(this.currentItemId);

                if (tax != null)
                {
                    txtName.Text = tax.Name;
                    txtRate.EditValue = tax.Rate;
                    txtStatus.Text = tax.Status;
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

            if (dxValidationProvider.Validate())
            {
                Models.Tax tax;

                if (this.type == "Add")
                {
                    tax = new Models.Tax();
                    tax.Name = txtName.Text;
                    tax.Rate = decimal.Parse(txtRate.EditValue.ToString());
                    tax.Status = txtStatus.Text;
                    tax.CreatedAt = DateTime.Now;
                    tax.UpdatedAt = DateTime.Now;

                    Shared.db.Taxes.Add(tax);

                    txtName.Text = "";
                }
                else
                {
                    if (this.taxes != null)
                        this.currentItemId = this.taxes.tax_id;

                    tax = Shared.db.Taxes.Find(this.currentItemId);

                    if (tax != null)
                    {
                        tax.Name = txtName.Text;
                        tax.Rate = decimal.Parse(txtRate.EditValue.ToString());
                        tax.Status = txtStatus.Text;
                        tax.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(tax).State = EntityState.Modified;
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
                    this.changeStatusToInactive(tax.Id);
                    Properties.Settings.Default.DefaultCurrency = txtName.Text;
                }

                if (this.taxes != null)
                    this.taxes.loadTaxes();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public void changeStatusToInactive(int id)
        {
            var taxes = Shared.db.Taxes.Where(c => c.Id != id).ToList();

            foreach (var tax in taxes)
            {
                tax.Status = "InActive";
            }

            Shared.db.SaveChanges();
        }

        private void AddEditTax_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Tax GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Taxes.Find(currentItemId);
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
            int minId = Shared.db.Taxes.Min(b => b.Id);
            int maxId = Shared.db.Taxes.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Tax currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtName.Text = currentItem.Name;
                txtRate.EditValue = currentItem.Rate;
                txtStatus.Text = currentItem.Status;
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
                int? minId = Shared.db.Taxes.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Taxes.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Taxes.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Taxes.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtName.Text = string.Empty;
            txtRate.EditValue = 0;

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