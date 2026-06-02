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

namespace Pos.Forms.Product.Unit
{
    public partial class AddEditUnit : DevExpress.XtraEditors.XtraForm
    {
        public Units units = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditUnit()
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

        public void setUnitsObject(Units units)
        {
            this.units = units;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.units != null || currentItemId != 0)
            {
                this.currentItemId = this.units != null ? this.units.unit_id : currentItemId;

                Models.Unit unit = Shared.db.Units.Find(this.currentItemId);

                if (unit != null)
                {
                    txtUnit.Text = unit.Name;
                    txtDescription.Text = unit.Description;
                    btnSelect.Enabled = true;
                    IsDivisible.EditValue = unit.IsDivisible;
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

            if (dxValidationProviderUnit.Validate())
            {
                Models.Unit unit;

                if (this.type == "Add")
                {
                    unit = new Models.Unit();
                    unit.Name = txtUnit.Text;
                    unit.Symbol = txtSymbol.Text;
                    unit.Description = txtDescription.Text;
                    unit.CreatedAt = DateTime.Now;
                    unit.UpdatedAt = DateTime.Now;

                    string selectedValue = IsDivisible.EditValue?.ToString();

                    if (selectedValue == "Yes")
                    {
                        unit.IsDivisible = true;
                    }
                    else if (selectedValue == "No")
                    {
                        unit.IsDivisible = false;
                    }
                    else
                    {
                        // Handle other cases, such as "Unknown" or null
                        unit.IsDivisible = false; // Default value
                    }

                    Shared.db.Units.Add(unit);

                    txtUnit.Text = "";
                }
                else
                {
                    if (this.units != null)
                        this.currentItemId = this.units.unit_id;

                    unit = Shared.db.Units.Find(this.currentItemId);

                    if (unit != null)
                    {
                        unit.Name = txtUnit.Text;
                        unit.Description = txtDescription.Text;
                        unit.UpdatedAt = DateTime.Now;

                        string selectedValue = IsDivisible.EditValue?.ToString();

                        if (selectedValue == "Yes")
                        {
                            unit.IsDivisible = true;
                        }
                        else if (selectedValue == "No")
                        {
                            unit.IsDivisible = false;
                        }
                        else
                        {
                            // Handle other cases, such as "Unknown" or null
                            unit.IsDivisible = false; // Default value
                        }

                        Shared.db.Entry(unit).State = EntityState.Modified;
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
                        addEditProduct.selectUnit(unit.Id);
                    }
                }

                if (this.units != null)
                    this.units.loadUnits();
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

        private Models.Unit GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Units.Find(currentItemId);
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
            int minId = Shared.db.Units.Min(b => b.Id);
            int maxId = Shared.db.Units.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Unit currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtUnit.Text = currentItem.Name;
                txtDescription.Text = currentItem.Description;
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
                int? minId = Shared.db.Units.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Units.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Units.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Units.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtUnit.Text = string.Empty;
            txtDescription.Text = string.Empty;

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
            searchLookUpEdit.Properties.DataSource = Shared.db.Units.ToList();
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
                    addEditProduct.selectUnit(this.currentItemId);
                }

                Sound.Selected();
                this.Close();
            }
        }
    }
}