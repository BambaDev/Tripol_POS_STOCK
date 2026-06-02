using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Product.Warranty
{
    public partial class AddEditWarranty : DevExpress.XtraEditors.XtraForm
    {
        public Warranties warranties = null;
        public string type = "Add";
        public int currentItemId = 0;
        public Object obj = null;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditWarranty()
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

        public void setWarrantiesObject(Warranties warranties)
        {
            this.warranties = warranties;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.warranties != null || currentItemId != 0)
            {
                this.currentItemId = this.warranties != null ? this.warranties.warranty_id : currentItemId;

                Models.Warranty warranty = Shared.db.Warranties.Find(this.currentItemId);

                if (warranty != null)
                {
                    txtWarranty.Text = warranty.Name;
                    txtDescription.Text = warranty.Description;
                    txtDuration.Text = warranty.Duration.ToString();
                    txtType.EditValue = warranty.Type;
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

        private void btnSaveUnit_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderWarranty.Validate())
            {
                Models.Warranty warranty;

                if (this.type == "Add")
                {
                    warranty = new Models.Warranty();
                    warranty.Name = txtWarranty.Text;
                    warranty.Description = txtDescription.Text;
                    warranty.Duration = Convert.ToInt32(txtDuration.Text);
                    warranty.Type = txtType.Text;
                    warranty.CreatedAt = DateTime.Now;
                    warranty.UpdatedAt = DateTime.Now;

                    Shared.db.Warranties.Add(warranty);

                    txtWarranty.Text = "";
                }
                else
                {
                    if (this.warranties != null)
                        this.currentItemId = this.warranties.warranty_id;

                    warranty = Shared.db.Warranties.Find(this.currentItemId);

                    if (warranty != null)
                    {
                        warranty.Name = txtWarranty.Text;
                        warranty.Description = txtDescription.Text;
                        warranty.Duration = Convert.ToInt32(txtDuration.Text);
                        warranty.Type = txtType.Text;
                        warranty.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(warranty).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (this.obj != null)
                {
                    if (this.obj is AddEditProduct)
                    {
                        AddEditProduct addEditProduct = (AddEditProduct)this.obj;
                        addEditProduct.selectWarranty(warranty.Id);
                    }
                }

                if (this.warranties != null)
                    this.warranties.loadWarranties();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditUnit_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Warranty GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Warranties.Find(currentItemId);
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
            int minId = Shared.db.Warranties.Min(b => b.Id);
            int maxId = Shared.db.Warranties.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Warranty currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtWarranty.Text = currentItem.Name;
                txtDescription.Text = currentItem.Description;
                txtDuration.Text = currentItem.Duration.ToString();
                txtType.EditValue = currentItem.Type;
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
                int? minId = Shared.db.Warranties.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Warranties.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Warranties.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Warranties.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtWarranty.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtDuration.Text = "1";
            txtType.EditValue = "Months";

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
            searchLookUpEdit.Properties.DataSource = Shared.db.Warranties.ToList();
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
            if (this.obj != null)
            {
                if (this.obj is AddEditProduct)
                {
                    AddEditProduct addEditProduct = (AddEditProduct)this.obj;
                    addEditProduct.selectWarranty(this.currentItemId);
                }

                Sound.Selected();
                this.Close();
            }
        }
    }
}