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

namespace Pos.Forms.Product.Variation
{
    public partial class AddEditVariation : DevExpress.XtraEditors.XtraForm
    {
        public Variations variations = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditVariation()
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

        public void setVariationsObject(Variations variations)
        {
            this.variations = variations;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.variations != null || currentItemId != 0)
            {
                this.currentItemId = this.variations != null ? this.variations.variation_id : currentItemId;

                Models.Variation variation = Shared.db.Variations.Find(this.currentItemId);

                if (variation != null)
                {
                    txtVariation.Text = variation.Name;
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

        private void btnSaveVariation_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationVariation.Validate())
            {
                Models.Variation variation;

                if (this.type == "Add")
                {
                    variation = new Models.Variation();
                    variation.Name = txtVariation.Text;
                    variation.CreatedAt = DateTime.Now;
                    variation.UpdatedAt = DateTime.Now;

                    Shared.db.Variations.Add(variation);

                    txtVariation.Text = "";
                }
                else
                {
                    if (this.variations != null)
                        this.currentItemId = this.variations.variation_id;

                    variation = Shared.db.Variations.Find(this.currentItemId);

                    if (variation != null)
                    {
                        variation.Name = txtVariation.Text;
                        variation.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(variation).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (this.variations != null)
                    this.variations.loadVariations();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditVariation_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.getVariationValues();
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private void btnAddValues_Click(object sender, EventArgs e)
        {
            if (txtValue.Text != "")
            {
                Models.VariationValue variationValue = new Models.VariationValue();
                variationValue.Value = txtValue.Text;
                variationValue.VariationId = this.variations.variation_id;
                variationValue.CreatedAt = DateTime.Now;
                variationValue.UpdatedAt = DateTime.Now;

                Shared.db.VariationValues.Add(variationValue);

                txtValue.Text = "";
                Shared.db.SaveChanges();

                this.getVariationValues();

                Function.Sound.Added();
            }
            else
            {
                Function.Sound.Wrong();
                XtraMessageBox.Show("Value can't be empty !");
            }
        }

        private void repDeleteVariationValue_Click(object sender, EventArgs e)
        {
            int variation_value_id = int.Parse(gridViewVariationValues.GetRowCellValue(gridViewVariationValues.FocusedRowHandle, "Id").ToString());

            Models.VariationValue variationValue = Shared.db.VariationValues.Find(variation_value_id);

            if (variationValue != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.VariationValues.Remove(variationValue);
                Shared.db.SaveChanges();
                this.getVariationValues();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        public void getVariationValues()
        {
            this.currentItemId = this.variations != null ? this.variations.variation_id : currentItemId;

            gridControlVariationValues.DataSource = Shared.db.VariationValues.Where(c => c.VariationId == this.currentItemId).ToList();
        }

        private Models.Variation GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Variations.Find(currentItemId);
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
            int minId = Shared.db.Variations.Min(b => b.Id);
            int maxId = Shared.db.Variations.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Variation currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtVariation.Text = currentItem.Name;

                this.getVariationValues();
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Variations.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Variations.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Variations.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Variations.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtVariation.Text = string.Empty;

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
            searchLookUpEdit.Properties.DataSource = Shared.db.Variations.ToList();
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