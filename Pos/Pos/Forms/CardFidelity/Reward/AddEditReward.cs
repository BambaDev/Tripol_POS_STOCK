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

namespace Pos.Forms.CardFidelity.Reward
{
    public partial class AddEditReward : DevExpress.XtraEditors.XtraForm
    {
        public Rewards rewards = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditReward()
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

        public void setRewardsObject(Rewards rewards)
        {
            this.rewards = rewards;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.rewards != null || currentItemId != 0)
            {
                this.currentItemId = this.rewards != null ? this.rewards.reward_id : currentItemId;

                Models.Reward reward = Shared.db.Rewards.Find(this.currentItemId);

                if (reward != null)
                {
                    txtName.Text = reward.Name;
                    txtExpirationDate.EditValue = reward.ExpirationDate;
                    txtDescription.Text = reward.Description;
                    pictureEdit.EditValue = reward.Image;
                    txtPointsRequired.EditValue = reward.PointsRequired;
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

            if (dxValidationProvider.Validate())
            {
                Models.Reward reward;

                Image image = pictureEdit.Image;

                byte[] imageBytes;

                using (var memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Png);
                    imageBytes = memoryStream.ToArray();
                }

                if (this.type == "Add")
                {
                    reward = new Models.Reward();
                    reward.Name = txtName.Text;
                    reward.Description = txtDescription.Text;
                    reward.Image = imageBytes;
                    reward.ExpirationDate = DateTime.Parse(txtExpirationDate.EditValue.ToString());
                    reward.PointsRequired = decimal.Parse(txtPointsRequired.Text); 
                    reward.CreatedAt = DateTime.Now;
                    reward.UpdatedAt = DateTime.Now;

                    Shared.db.Rewards.Add(reward);

                    txtName.Text = "";
                }
                else
                {
                    if (this.rewards != null)
                        this.currentItemId = this.rewards.reward_id;

                    reward = Shared.db.Rewards.Find(this.currentItemId);

                    if (reward != null)
                    {
                        reward.Name = txtName.Text;
                        reward.Description = txtDescription.Text;
                        reward.Image = imageBytes;
                        reward.ExpirationDate = DateTime.Parse(txtExpirationDate.EditValue.ToString());
                        reward.PointsRequired = decimal.Parse(txtPointsRequired.Text);
                        reward.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(reward).State = EntityState.Modified;
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

                if (this.rewards != null)
                    this.rewards.loadRewards();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditReward_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }


        private Models.Reward GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Rewards.Find(currentItemId);
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
            int minId = Shared.db.Rewards.Min(b => b.Id);
            int maxId = Shared.db.Rewards.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Reward currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtName.Text = currentItem.Name;
                txtExpirationDate.EditValue = currentItem.ExpirationDate;
                txtDescription.Text = currentItem.Description;
                pictureEdit.EditValue = currentItem.Image;
                txtPointsRequired.EditValue = currentItem.PointsRequired;
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
                int? minId = Shared.db.Rewards.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Rewards.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Rewards.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Rewards.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            searchLookUpEdit.Properties.DataSource = Shared.db.Rewards.ToList();
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