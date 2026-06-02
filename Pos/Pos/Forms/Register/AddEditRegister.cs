using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Accessibility;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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

namespace Pos.Forms.Register
{
    public partial class AddEditRegister : DevExpress.XtraEditors.XtraForm
    {
        public Registers registers = null;
        public string type = "Add";
        public int currentItemId = 0;
        public OpenRegister openRegister =null;
        public AddEditRegister()
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
        public void setRegistersObject(OpenRegister openRegister)
        {
            this.openRegister = openRegister;
        }
        public void setRegistersObject(Registers registers)
        {
            this.registers = registers;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.registers != null || currentItemId != 0)
            {
                this.currentItemId = this.registers != null ? this.registers.register_id : currentItemId;

                Models.Register register = Shared.db.Registers.Find(this.currentItemId);

                if (register != null)
                {
                    txtName.Text = register.Name;
                    txtCode.Text = register.Code;
                    txtOpened.EditValue = register.Opened;
                    txtTerminalId.Text = register.TerminalId;
                    txtDeviceId.Text = register.DeviceId;
                    txtBusinessLocationId.EditValue = register.BusinessLocationId;
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        private void btnSaveRegister_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderRegister.Validate())
            {
                Models.Register register;

                if (this.type == "Add")
                {
                    register = new Models.Register();
                    register.Name = txtName.Text;
                    register.Code = txtCode.Text;
                    register.Opened = txtOpened.Text;
                    register.TerminalId = txtTerminalId.Text;
                    register.DeviceId = txtDeviceId.Text;
                    register.BusinessLocationId = int.Parse(txtBusinessLocationId.EditValue.ToString());
                    register.CreatedAt = DateTime.Now;
                    register.UpdatedAt = DateTime.Now;

                    Shared.db.Registers.Add(register);

                    txtName.Text = "";
                }
                else
                {
                    if (this.registers != null)
                        this.currentItemId = this.registers.register_id;

                    register = Shared.db.Registers.Find(this.registers.register_id);

                    if (register != null)
                    {
                        register.Name = txtName.Text;
                        register.Code = txtCode.Text;
                        register.Opened = txtOpened.Text;
                        register.TerminalId = txtTerminalId.Text;
                        register.DeviceId = txtDeviceId.Text;
                        register.BusinessLocationId = int.Parse(txtBusinessLocationId.EditValue.ToString());
                        register.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(register).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (this.registers != null)
                    this.registers.loadRegisters();
                if (this.openRegister != null)
                {
                    this.openRegister.loadRegisters(register.Id);
                }
              
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditRegister_Load(object sender, EventArgs e)
        {
            this.getBusinessLocations();
            txtOpened.EditValue = "Closed";

            if (this.type != "Add")
            {
                this.edit();
            }

            txtBusinessLocationId.EditValue = Properties.Settings.Default.BusinessLocation;

            this.InitializeSearchLookUpEdit();
        }

        public void getBusinessLocations()
        {
            txtBusinessLocationId.Properties.DataSource = Shared.db.BusinessLocations.ToList();
            txtBusinessLocationId.Properties.DisplayMember = "Name"; // Set display member
            txtBusinessLocationId.Properties.ValueMember = "Id"; // Set value member
        }

        private void btnQuickBussLocation_Click(object sender, EventArgs e)
        {
            BusinessLocation.AddEditBusinessLocation addEditBusinessLocation = new BusinessLocation.AddEditBusinessLocation();
            addEditBusinessLocation.setObject(this);
            addEditBusinessLocation.ShowDialog();
        }

        public void selectBusinessLocation(int id)
        {
            this.getBusinessLocations();
            txtBusinessLocationId.EditValue = id;
        }

        private Models.Register GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Registers.Find(currentItemId);
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
            int minId = Shared.db.Registers.Min(b => b.Id);
            int maxId = Shared.db.Registers.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Register currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtName.Text = currentItem.Name;
                txtCode.Text = currentItem.Code;
                txtOpened.EditValue = currentItem.Opened;
                txtTerminalId.Text = currentItem.TerminalId;
                txtDeviceId.Text = currentItem.DeviceId;
                txtBusinessLocationId.EditValue = currentItem.BusinessLocationId;
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Registers.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Registers.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Registers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Registers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtCode.Text = string.Empty;
            txtOpened.EditValue = "Closed";

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
            searchLookUpEdit.Properties.DataSource = Shared.db.Registers.ToList();
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
    }
}