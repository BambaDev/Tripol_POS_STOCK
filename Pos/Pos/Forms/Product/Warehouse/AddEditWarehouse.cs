using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Expense;
using Pos.Forms.Purchase;
using Pos.Forms.Stock.Transfer;
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
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Product.Warehouse
{
    public partial class AddEditWarehouse : DevExpress.XtraEditors.XtraForm
    {
        public Warehouses warehouses = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;
        PosScreen localpos;
        bool isPos = false;
        AddEditPurchase addEditPurchase;
        bool isaddEditPurchase = false;
        AddEditTransfer localaddEditTransfer;
        bool isaddEditTransfer = false;
        public void setObject(Object obj)
        {
            this.obj = obj;
        }
        public AddEditWarehouse(AddEditTransfer addEditTransfer)
        {
            InitializeComponent();
            isaddEditTransfer = true;
            localaddEditTransfer = addEditTransfer;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
        }

        public AddEditWarehouse(AddEditPurchase addEditPurchas)
        {
            InitializeComponent();
            isaddEditPurchase = true;
            addEditPurchase=addEditPurchas;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
        }
        public AddEditWarehouse(PosScreen pos)
        {
            InitializeComponent();
            localpos = pos;
            isPos = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
        }
        public AddEditWarehouse()
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

        public void setWarehousesObject(Warehouses warehouses)
        {
            this.warehouses = warehouses;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.warehouses != null || currentItemId != 0)
            {
                this.currentItemId = this.warehouses != null ? this.warehouses.warehouse_id : currentItemId;

                Models.Warehouse warehouse = Shared.db.Warehouses.Find(this.currentItemId);

                if (warehouse != null)
                {
                    txtWarehouse.Text = warehouse.Name;
                    txtPhone.Text = warehouse.Phone;
                    txtEmail.Text = warehouse.Email;
                    txtStatus.Text = warehouse.Status;
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

        private void btnSaveWarehouse_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderWarehouse.Validate())
            {
                Models.Warehouse warehouse;

                if (this.type == "Add")
                {
                    warehouse = new Models.Warehouse();
                    warehouse.Name = txtWarehouse.Text;
                    warehouse.Phone = txtPhone.Text;
                    warehouse.Email = txtEmail.Text;
                    warehouse.Status = txtStatus.Text;
                    warehouse.CreatedAt = DateTime.Now;
                    warehouse.UpdatedAt = DateTime.Now;

                    Shared.db.Warehouses.Add(warehouse);

                    txtWarehouse.Text = "";
                }
                else
                {
                    if (this.warehouses != null)
                        this.currentItemId = this.warehouses.warehouse_id;

                    warehouse = Shared.db.Warehouses.Find(this.currentItemId);

                    if (warehouse != null)
                    {
                        warehouse.Name = txtWarehouse.Text;
                        warehouse.Phone = txtPhone.Text;
                        warehouse.Email = txtEmail.Text;
                        warehouse.Status = txtStatus.Text;
                        warehouse.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(warehouse).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();
                if (isPos)
                {
                    if (warehouse.Status== "Active")
                    {
                        localpos.SetWarehouse(warehouse.Id);
                      
                    }
                    else
                    {
                        localpos.SetWarehouse(0);
                    }
                    this.Close();
                }

                if (isaddEditPurchase)
                {
                    if (warehouse.Status == "Active")
                    {
                        addEditPurchase.setWarehouse(warehouse.Id);

                    }
                    else
                    {
                        addEditPurchase.setWarehouse(0);
                    }
                    this.Close();
                }

                if (isaddEditTransfer)
                {
                    localaddEditTransfer.setWarehouse(warehouse.Id);
                    this.Close();
                }
                
                if (this.warehouses != null)
                    this.warehouses.loadWarehouses();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditWarehouse_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Warehouse GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Warehouses.Find(currentItemId);
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
            int minId = Shared.db.Warehouses.Min(b => b.Id);
            int maxId = Shared.db.Warehouses.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Warehouse currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtWarehouse.Text = currentItem.Name;
                txtPhone.Text = currentItem.Phone;
                txtEmail.Text = currentItem.Email;
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
                int? minId = Shared.db.Warehouses.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Warehouses.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Warehouses.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Warehouses.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtWarehouse.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtStatus.Text = string.Empty;

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
            searchLookUpEdit.Properties.DataSource = Shared.db.Warehouses.ToList();
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
                Sound.Selected();
                this.Close();
            }
        }
    }
}