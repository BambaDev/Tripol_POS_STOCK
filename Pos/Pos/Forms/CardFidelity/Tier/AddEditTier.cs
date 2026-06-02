using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Expense;
using Pos.Forms.Product.Warehouse;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.CardFidelity.Tier
{
    public partial class AddEditTier : DevExpress.XtraEditors.XtraForm
    {
        public Tiers tiers = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditTier()
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

        public void setTiersObject(Tiers tiers)
        {
            this.tiers = tiers;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.tiers != null || currentItemId != 0)
            {
                this.currentItemId = this.tiers != null ? this.tiers.tier_id : currentItemId;

                Models.CustomerTier customerTier = Shared.db.CustomerTiers.Find(this.currentItemId);

                if (customerTier != null)
                {
                    txtName.Text = customerTier.Name;
                    txtDescription.Text = customerTier.Description;
                    txtBenefits.Text = customerTier.Benefits;
                    txtThresholdPoints.EditValue = customerTier.ThresholdPoints;
                    txtPointMultiplier.EditValue = customerTier.PointMultiplier;
                    txtDiscountRate.EditValue = customerTier.DiscountRate;
                    picture.EditValue = customerTier.Image;
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

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            Image image = picture.Image;

            byte[] imageBytes;

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                imageBytes = memoryStream.ToArray();
            }

            if (dxValidationProvider.Validate())
            {
                Models.CustomerTier customerTier;

                if (this.type == "Add")
                {
                    customerTier = new Models.CustomerTier();
                    customerTier.Name = txtName.Text;
                    customerTier.Description = txtDescription.Text;
                    customerTier.Benefits = txtBenefits.Text;
                    customerTier.ThresholdPoints = decimal.Parse(txtThresholdPoints.Text);
                    customerTier.PointMultiplier = decimal.Parse(txtPointMultiplier.Text);
                    customerTier.DiscountRate = decimal.Parse(txtDiscountRate.Text);
                    customerTier.Image = imageBytes;
                    customerTier.CreatedAt = DateTime.Now;
                    customerTier.UpdatedAt = DateTime.Now;

                    Shared.db.CustomerTiers.Add(customerTier);

                    txtName.Text = "";
                }
                else
                {
                    if (this.tiers != null)
                        this.currentItemId = this.tiers.tier_id;

                    customerTier = Shared.db.CustomerTiers.Find(this.currentItemId);

                    if (customerTier != null)
                    {
                        customerTier.Name = txtName.Text;
                        customerTier.Description = txtDescription.Text;
                        customerTier.Benefits = txtBenefits.Text;
                        customerTier.ThresholdPoints = decimal.Parse(txtThresholdPoints.Text);
                        customerTier.PointMultiplier = decimal.Parse(txtPointMultiplier.Text);
                        customerTier.DiscountRate = decimal.Parse(txtDiscountRate.Text);
                        customerTier.Image = imageBytes;
                        customerTier.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(customerTier).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                //if (this.obj != null)
                //{
                //    if (this.obj is AddEditExpense)
                //    {
                //        AddEditProduct addEditProduct = (AddEditProduct)this.obj;
                //        addEditProduct.selectCategory(category.Id);
                //    }
                //}

                if (this.tiers != null)
                    this.tiers.loadTiers();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditTier_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }


        private Models.CustomerTier GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.CustomerTiers.Find(currentItemId);
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
            int minId = Shared.db.CustomerTiers.Min(b => b.Id);
            int maxId = Shared.db.CustomerTiers.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.CustomerTier currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtName.Text = currentItem.Name;
                txtDescription.Text = currentItem.Description;
                txtBenefits.Text = currentItem.Benefits;
                txtThresholdPoints.EditValue = currentItem.ThresholdPoints;
                txtPointMultiplier.EditValue = currentItem.PointMultiplier;
                txtDiscountRate.EditValue = currentItem.DiscountRate;
                picture.EditValue = currentItem.Image;
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
                int? minId = Shared.db.CustomerTiers.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.CustomerTiers.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.CustomerTiers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.CustomerTiers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtDescription.EditValue = string.Empty;

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
            searchLookUpEdit.Properties.DataSource = Shared.db.CustomerTiers.ToList();
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
            //if (this.obj != null)
            //{
            //    if (this.obj is AddEditExpense)
            //    {
            //        AddEditProduct addEditProduct = (AddEditProduct)this.obj;
            //        addEditProduct.selectCategory(this.currentItemId);
            //    }

            //    Sound.Selected();
            //    this.Close();
            //}
        }
    }
}