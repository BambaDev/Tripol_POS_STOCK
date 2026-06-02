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

namespace Pos.Forms.Product.Field
{
    public partial class AddEditField : DevExpress.XtraEditors.XtraForm
    {
        public Fields fields = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditField()
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

        public void setFieldsObject(Fields fields)
        {
            this.fields = fields;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.fields != null || currentItemId != 0)
            {
                this.currentItemId = this.fields != null ? this.fields.field_id : currentItemId;

                Models.ProductField productField = Shared.db.ProductFields.Find(this.currentItemId);

                if (productField != null)
                {
                    txtName.Text = productField.Name;
                    txtDescription.Text = productField.Description;
                    txtStatus.Text = productField.Status;

                    this.loadValues(productField.Id);

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.ProductField productField;

                if (this.type == "Add")
                {
                    productField = new Models.ProductField();
                    productField.Name = txtName.Text;
                    productField.Status = txtStatus.Text;
                    productField.CreatedAt = DateTime.Now;
                    productField.UpdatedAt = DateTime.Now;

                    Shared.db.ProductFields.Add(productField);

                    txtName.Text = "";
                }
                else
                {
                    if (this.fields != null)
                        this.currentItemId = this.fields.field_id;

                    productField = Shared.db.ProductFields.Find(this.currentItemId);

                    if (productField != null)
                    {
                        productField.Name = txtName.Text;
                        productField.Description = txtDescription.Text;
                        productField.Status = txtStatus.Text;
                        productField.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(productField).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                //if (this.fields != null)
                //    this.fields.loadCurrencies();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditField_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        public void loadValues(int ProductFieldId)
        {
            gridControlValues.DataSource = Shared.db.ProductFieldValues.Where(f => f.ProductFieldId == ProductFieldId).ToList();
        }

        private Models.ProductField GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.ProductFields.Find(currentItemId);
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
            int minId = Shared.db.ProductFields.Min(b => b.Id);
            int maxId = Shared.db.ProductFields.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.ProductField currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtName.Text = currentItem.Name;
                txtDescription.Text = currentItem.Description;
                txtStatus.Text = currentItem.Status;

                this.loadValues(currentItem.Id);

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
                int? minId = Shared.db.ProductFields.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.ProductFields.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.ProductFields.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.ProductFields.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            searchLookUpEdit.Properties.DataSource = Shared.db.ProductFields.ToList();
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

        private void repoItemDelete_Click(object sender, EventArgs e)
        {
            // Check if there is a selected row
            int[] selectedRows = gridViewValues.GetSelectedRows();
            if (selectedRows.Length > 0)
            {
                // Get the selected row handle
                int selectedRowHandle = selectedRows[0];

                // Get the item from the selected row
                var selectedItem = (ProductFieldValue)gridViewValues.GetRow(selectedRowHandle);

                if (selectedItem != null)
                {
                    // Remove the item from the database
                    Shared.db.ProductFieldValues.Remove(selectedItem);

                    // Save changes to the database
                    Shared.db.SaveChanges();

                    // Refresh the GridControl
                    gridControlValues.DataSource = Shared.db.ProductFieldValues.Where(f => f.ProductFieldId == selectedItem.ProductFieldId).ToList();

                    // Optionally, show a message to the user
                    Sound.Added();
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValue.Text))
            {
                // Get the ProductFieldId (assuming you have a way to get this ID in your form)
                int productFieldId = fields.field_id; // Replace with your method of getting the ProductFieldId

                // Create a new ProductFieldValue
                var newValue = new ProductFieldValue
                {
                    ProductFieldId = productFieldId,
                    Value = txtValue.Text
                    // Set other properties if needed
                };

                // Add the new value to the database
                Shared.db.ProductFieldValues.Add(newValue);

                // Save changes to the database
                Shared.db.SaveChanges();

                // Refresh the GridControl
                loadValues(productFieldId);

                // Optionally, clear the text box and show a message
                txtValue.Text = string.Empty;

                Sound.Added();
            }
            else
            {
                MessageBox.Show("Please enter a value.");
            }
        }

    }
}