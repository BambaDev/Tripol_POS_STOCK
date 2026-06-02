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

namespace Pos.Forms.CardFidelity.Redemption
{
    public partial class AddEditRedemption : DevExpress.XtraEditors.XtraForm
    {
        public Redemptions redemptions = null;
        public string type = "Add";
        public Object obj = null;
        public decimal pointsSpent = 0;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditRedemption()
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

        public void setRedemptionsObject(Redemptions redemptions)
        {
            this.redemptions = redemptions;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.redemptions != null || currentItemId != 0)
            {
                this.currentItemId = this.redemptions != null ? this.redemptions.redemption_id : currentItemId;

                Models.Redemption redemption = Shared.db.Redemptions.Find(this.currentItemId);

                if (redemption != null)
                {
                    txtRedemptionDate.EditValue = redemption.RedemptionDate;
                    txtPointsSpent.EditValue = redemption.PointsSpent;
                    txtCustomerId.EditValue = redemption.CustomerId;
                    txtRewardId.EditValue = redemption.RewardId;
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.Redemption redemption;

                if (this.type == "Add")
                {
                    redemption = new Models.Redemption();
                    redemption.RedemptionDate = DateTime.Parse(txtRedemptionDate.EditValue.ToString());
                    redemption.PointsSpent = decimal.Parse(txtPointsSpent.EditValue.ToString());
                    redemption.CustomerId = int.Parse(txtCustomerId.EditValue.ToString());
                    redemption.RewardId = int.Parse(txtRewardId.EditValue.ToString());
                    redemption.CreatedAt = DateTime.Now;
                    redemption.UpdatedAt = DateTime.Now;

                    Shared.db.Redemptions.Add(redemption);
                }
                else
                {
                    if (this.redemptions != null)
                        this.currentItemId = this.redemptions.redemption_id;

                    redemption = Shared.db.Redemptions.Find(this.currentItemId);

                    if (redemption != null)
                    {
                        redemption.RedemptionDate = DateTime.Parse(txtRedemptionDate.EditValue.ToString());
                        redemption.PointsSpent = decimal.Parse(txtPointsSpent.EditValue.ToString());
                        redemption.CustomerId = int.Parse(txtCustomerId.Text);
                        redemption.RewardId = int.Parse(txtRewardId.Text);
                        redemption.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(redemption).State = EntityState.Modified;
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

                if (this.redemptions != null)
                    this.redemptions.loadRedemptions();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditRedemption_Load(object sender, EventArgs e)
        {
            txtRedemptionDate.EditValue = DateTime.Now;

            if (this.type != "Add")
            {
                this.edit();
            }

            this.getCustomers();
            this.getRewards();
        }

        public void getCustomers()
        {
            txtCustomerId.Properties.DataSource = Shared.db.Customers.ToList();
            txtCustomerId.Properties.DisplayMember = "FirstName"; // Set display member
            txtCustomerId.Properties.ValueMember = "Id"; // Set value member
        }

        public void getRewards()
        {
            txtRewardId.Properties.DataSource = Shared.db.Rewards.ToList();
            txtRewardId.Properties.DisplayMember = "Name"; // Set display member
            txtRewardId.Properties.ValueMember = "Id"; // Set value member
        }

        private Models.Redemption GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Redemptions.Find(currentItemId);
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
            int minId = Shared.db.Redemptions.Min(b => b.Id);
            int maxId = Shared.db.Redemptions.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Redemption currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtRedemptionDate.EditValue = currentItem.RedemptionDate;
                txtPointsSpent.EditValue = currentItem.PointsSpent;
                txtCustomerId.EditValue = currentItem.CustomerId;
                txtRewardId.EditValue = currentItem.RewardId;
            }
        }


        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Redemptions.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Redemptions.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Redemptions.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Redemptions.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtRedemptionDate.Text = string.Empty;
            txtPointsSpent.EditValue = 0;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRewardId_EditValueChanged(object sender, EventArgs e)
        {
            if(txtRewardId.Text != string.Empty)
            {
                Models.Reward reward = Shared.db.Rewards.Find(int.Parse(txtRewardId.EditValue.ToString()));

                if(reward != null)
                {
                    txtPointsSpent.EditValue = reward.PointsRequired.Value;
                } else
                {
                    txtPointsSpent.EditValue = 0;
                }
            }
        }
    }
}