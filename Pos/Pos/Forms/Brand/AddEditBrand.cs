using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Product;
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

namespace Pos.Forms.Brand
{
    public partial class AddEditBrand : DevExpress.XtraEditors.XtraForm
    {
        public Brands brands = null;
        public string type = "Add";
        public Object obj = null;
        public int currentBrandId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditBrand()
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

        public void setBrandsObject(Brands brands)
        {
            this.brands = brands;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.brands != null || this.currentBrandId != 0)
            {
                this.currentBrandId = this.brands != null ? this.brands.brand_id : this.currentBrandId;

                Models.Brand brand = Shared.db.Brands.Find(this.currentBrandId);

                if (brand != null)
                {
                    txtBrand.Text = brand.Name;
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

        private void btnSaveBrand_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderBrand.Validate())
            {
                Models.Brand brand;

                if (this.type == "Add")
                {
                    brand = new Models.Brand();
                    brand.Name = txtBrand.Text;
                    brand.CreatedAt = DateTime.Now;
                    brand.UpdatedAt = DateTime.Now;

                    Shared.db.Brands.Add(brand);

                    txtBrand.Text = "";
                }
                else
                {
                    if (this.brands != null)
                        this.currentBrandId = this.brands.brand_id;

                    brand = Shared.db.Brands.Find(this.currentBrandId);

                    if (brand != null)
                    {
                        brand.Name = txtBrand.Text;
                        brand.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(brand).State = EntityState.Modified;
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
                        addEditProduct.selectBrands(brand.Id);
                    }
                }

                if (this.brands != null)
                    this.brands.loadBrands();
            }
            else
            {
                Function.Sound.Wrong();
            }

            // send notifictions
            Function.NotificationAction.Create("Add", "User", txtBrand.Text, Properties.Settings.Default.userId);

            SplashScreenManager.CloseForm();
        }

        private void AddEditBrand_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Brand GetCurrentData()
        {
            if (currentBrandId != 0)
                return Shared.db.Brands.Find(currentBrandId);
            else
                return null;
        }

        private void DisplayCurrentBrand()
        {
            Sound.Selected();

            if (currentBrandId == 0)
            {
                // If no current selection, disable all navigation buttons.
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnStart.Enabled = false;
                btnEnd.Enabled = false;
                return;
            }

            // Retrieve the minimum and maximum ID values from the Brands dataset.
            int minId = Shared.db.Brands.Min(b => b.Id);
            int maxId = Shared.db.Brands.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentBrandId > minId; // Disable if on the first item
            btnNext.Enabled = currentBrandId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentBrandId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentBrandId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Brand currentBrand = GetCurrentData();

            if (currentBrand != null)
            {
                this.currentBrandId = currentBrand.Id;
                txtBrand.Text = currentBrand.Name + " " + this.currentBrandId;
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
                int? minId = Shared.db.Brands.Min(b => (int?)b.Id);
                if (minId.HasValue)
                {
                    currentBrandId = minId.Value;
                    DisplayCurrentBrand();
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
                int? maxId = Shared.db.Brands.Max(b => (int?)b.Id);
                if (maxId.HasValue)
                {
                    currentBrandId = maxId.Value;
                    DisplayCurrentBrand();
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
            if (currentBrandId == 0)
            {
                this.MoveToFirst();
            }

            var nextBrand = Shared.db.Brands.Where(b => b.Id > currentBrandId).OrderBy(b => b.Id).FirstOrDefault();
            if (nextBrand != null)
            {
                currentBrandId = nextBrand.Id;
                DisplayCurrentBrand();
            }
        }

        private void MoveToPrevious()
        {
            if (currentBrandId != 0)
            {
                var prevBrand = Shared.db.Brands.Where(b => b.Id < currentBrandId).OrderByDescending(b => b.Id).FirstOrDefault();
                if (prevBrand != null)
                {
                    currentBrandId = prevBrand.Id;
                    DisplayCurrentBrand();
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
            txtBrand.Text = string.Empty;
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
            // Assuming Shared.db.Brands returns a DbSet<Brand>
            searchLookUpEdit.Properties.DataSource = Shared.db.Brands.ToList();
            searchLookUpEdit.Properties.DisplayMember = "Name";  // Display member is the Brand Name
            searchLookUpEdit.Properties.ValueMember = "Id";  // Value member is the Brand Id

            // This sets up the columns you want to display in the dropdown
            searchLookUpEdit.Properties.View.Columns.Clear();
            searchLookUpEdit.Properties.View.Columns.AddVisible("Name", "Brand Name");

            searchLookUpEdit.Properties.NullText = "Write something...";

            // Enable auto-search functionality
            searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit.EditValue != null)
            {
                if (int.TryParse(searchLookUpEdit.EditValue.ToString(), out int itemId))
                {
                    this.currentBrandId = itemId;
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
                    addEditProduct.selectBrands(this.currentBrandId);
                }

                Sound.Selected();
                this.Close();
            }
        }
    }
}