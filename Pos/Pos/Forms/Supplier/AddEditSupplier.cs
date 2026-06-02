using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Purchase;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pos.Forms.Supplier
{
    public partial class AddEditSupplier : DevExpress.XtraEditors.XtraForm
    {
        public Suppliers suppliers = null;
        public string type = "Add";
        public int currentItemId = 0;
        AddEditPurchase addEditPurchase;
        bool isaddEditPurchase = false;

        public AddEditSupplier(AddEditPurchase addEditPurchas)
        {
            InitializeComponent();
            isaddEditPurchase = true;
            addEditPurchase = addEditPurchas;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
        }
        public AddEditSupplier()
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

        public void setSuppliersObject(Suppliers suppliers)
        {
            this.suppliers = suppliers;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.suppliers != null || currentItemId != 0)
            {
                this.currentItemId = this.suppliers != null ? this.suppliers.supplier_id : currentItemId;

                Models.Supplier supplier = Shared.db.Suppliers.Find(this.suppliers.supplier_id);

                if (supplier != null)
                {
                    txtFirstName.Text = supplier.FirstName;
                    txtLastName.Text = supplier.LastName;
                    txtEmail.Text = supplier.Email;
                    txtImage.EditValue = supplier.Image;
                    txtStatus.EditValue = supplier.Status;
                    txtGender.EditValue = supplier.Gender;
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

        private void btnSaveSupplier_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderSupplier.Validate())
            {
                byte[] imageBytes;
                if (txtImage.Image != null)
                {
                    Image image = txtImage.Image;

                

                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Png);
                        imageBytes = memoryStream.ToArray();
                    }

                    // Vous pouvez maintenant utiliser imageBytes pour stocker l'image en base de données, etc.
                }
                else
                {
                    // Gérez le cas où l'image est null (par exemple, afficher un message ou ignorer cette partie)
                    imageBytes = null; // ou toute autre valeur par défaut selon vos besoins
                }

                Models.Supplier supplier;

                if (this.type == "Add")
                {
                    supplier = new Models.Supplier();
                    supplier.FirstName = txtFirstName.Text;
                    supplier.LastName = txtLastName.Text;
                    supplier.Email = txtEmail.Text;
                    supplier.Image = imageBytes;
                    supplier.Status = txtStatus.Text;
                    supplier.Gender = txtGender.Text;
                    supplier.CreatedAt = DateTime.Now;
                    supplier.UpdatedAt = DateTime.Now;

                    Shared.db.Suppliers.Add(supplier);

                    txtFirstName.Text = "";
                }
                else
                {
                    if (this.suppliers != null)
                        this.currentItemId = this.suppliers.supplier_id;

                    supplier = Shared.db.Suppliers.Find(this.currentItemId);

                    supplier.FirstName = txtFirstName.Text;
                    supplier.LastName = txtLastName.Text;
                    supplier.Email = txtEmail.Text;
                    supplier.Image = imageBytes;
                    supplier.Status = txtStatus.Text;
                    supplier.Gender = txtGender.Text;
                    supplier.UpdatedAt = DateTime.Now;

                    Shared.db.Entry(supplier).State = EntityState.Modified;
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (isaddEditPurchase)
                {
                    addEditPurchase.setSupplier(supplier.Id);
                    this.Close();
                }
                if (this.suppliers != null)
                    this.suppliers.loadSuppliers();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditSupplier_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private void btnSupplierReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtImage.EditValue = null;
            txtStatus.EditValue = "Active";
            txtGender.EditValue = "Man";

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;

            Function.Sound.Added();
        }

        private Models.Supplier GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Suppliers.Find(currentItemId);
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
            int minId = Shared.db.Suppliers.Min(b => b.Id);
            int maxId = Shared.db.Suppliers.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Supplier currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtFirstName.Text = currentItem.FirstName;
                txtLastName.Text = currentItem.LastName;
                txtEmail.Text = currentItem.Email;
                txtImage.EditValue = currentItem.Image;
                txtStatus.EditValue = currentItem.Status;
                txtGender.EditValue = currentItem.Gender;
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
                int? minId = Shared.db.Suppliers.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Suppliers.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Suppliers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Suppliers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void InitializeSearchLookUpEdit()
        {
            searchLookUpEdit.Properties.DataSource = Shared.db.Suppliers.ToList();
            searchLookUpEdit.Properties.DisplayMember = "FirstName";
            searchLookUpEdit.Properties.ValueMember = "Id";

            searchLookUpEdit.Properties.View.Columns.Clear();
            searchLookUpEdit.Properties.View.Columns.AddVisible("FirstName", "First Name");
            searchLookUpEdit.Properties.View.Columns.AddVisible("LastName", "Last Name");

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