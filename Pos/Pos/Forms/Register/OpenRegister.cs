using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Accessibility;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
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
    public partial class OpenRegister : DevExpress.XtraEditors.XtraForm
    {
        public Registers registers = null;
        public string type = "Add";

        public OpenRegister(CloseRegister closeRegister = null)
        {
            InitializeComponent();

            if (closeRegister != null)
                closeRegister.Dispose();

            this.toRtl();
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                this.ApplyCustomFont();

                // Set the form to use RTL
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 14.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            // Apply the custom font to the form
            this.Font = customFont;

            simpleLabelItem1.AppearanceItemCaption.Font = customFont;
            layoutControlItem3.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem7.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;
            layoutControlGroup1.AppearanceGroup.Font = customFont;
            //layoutControlGroup3.AppearanceGroup.Font = customFont;
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
            if (this.registers != null)
            {
                Models.RegisterRecord registerRecord = Shared.db.RegisterRecords.Find(this.registers.register_id);

                if (registerRecord != null)
                {
                    txtRegisterId.EditValue = registerRecord.RegisterId;
                    txtBusinessLocationId.EditValue = registerRecord.BusinessLocationId;
                    //txtTotalCashAmount.EditValue = registerRecord.TotalCashAmount;
                    //txtTotalCashSubmitted.EditValue = registerRecord.TotalCashSubmitted;
                    //txtTotalCheques.EditValue = registerRecord.TotalCheques;
                    //txtTotalChequesAmount.EditValue = registerRecord.TotalChequesAmount;
                    //txtTotalChequesSubmitted.EditValue = registerRecord.TotalChequesSubmitted;
                    //txtTotalOtherAmount.EditValue = registerRecord.TotalOtherAmount;
                    //txtTotalRefundsAmount.EditValue = registerRecord.TotalRefundsAmount;
                    //txtTotalExpensesAmount.EditValue = registerRecord.TotalExpensesAmount;
                    //txtTotalGiftCardAmount.EditValue = registerRecord.TotalGiftCardAmount;
                    //txtTotalReturnOrdersAmount.EditValue = registerRecord.TotalReturnOrdersAmount;
                    txtCashInHand.EditValue = registerRecord.CashInHand;
                    txtComment.Text = registerRecord.Comment;
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            if (dxValidationProviderRegister.Validate())
            {
                Models.RegisterRecord registerRecord;

                if (this.type == "Add")
                {
                    registerRecord = new Models.RegisterRecord();

                    registerRecord.UserId = Properties.Settings.Default.userId;
                    registerRecord.RegisterId = int.Parse(txtRegisterId.EditValue.ToString());
                    registerRecord.BusinessLocationId = int.Parse(txtBusinessLocationId.EditValue.ToString());
                    registerRecord.TotalCashAmount = 0;
                    registerRecord.TotalCashSubmitted = 0;
                    registerRecord.TotalCheques = 0;
                    registerRecord.TotalChequesAmount = 0;
                    registerRecord.TotalChequesSubmitted = 0;
                    registerRecord.TotalOtherAmount = 0;
                    registerRecord.TotalRefundsAmount = 0;
                    registerRecord.TotalExpensesAmount = 0;
                    registerRecord.TotalGiftCardAmount = 0;
                    registerRecord.TotalReturnOrdersAmount = 0;
                    registerRecord.CashInHand = decimal.Parse(txtCashInHand.EditValue.ToString());
                    registerRecord.Comment = txtComment.Text;
                    registerRecord.CreatedAt = DateTime.Now;
                    registerRecord.UpdatedAt = DateTime.Now;

                    Shared.db.RegisterRecords.Add(registerRecord);
                }
                else
                {
                    registerRecord = Shared.db.RegisterRecords.Find(this.registers.register_id);

                    if (registerRecord != null)
                    {
                        registerRecord.UserId = Properties.Settings.Default.userId;
                        registerRecord.RegisterId = int.Parse(txtRegisterId.EditValue.ToString());
                        registerRecord.BusinessLocationId = int.Parse(txtBusinessLocationId.EditValue.ToString());
                        registerRecord.TotalCashAmount = 0;
                        registerRecord.TotalCashSubmitted = 0;
                        registerRecord.TotalCheques = 0;
                        registerRecord.TotalChequesAmount = 0;
                        registerRecord.TotalChequesSubmitted = 0;
                        registerRecord.TotalOtherAmount = 0;
                        registerRecord.TotalRefundsAmount = 0;
                        registerRecord.TotalExpensesAmount = 0;
                        registerRecord.TotalGiftCardAmount = 0;
                        registerRecord.TotalReturnOrdersAmount = 0;
                        registerRecord.CashInHand = decimal.Parse(txtCashInHand.EditValue.ToString());
                        registerRecord.Comment = txtComment.Text;
                        registerRecord.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(registerRecord).State = EntityState.Modified;
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

                this.Hide();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void OpenRegister_Load(object sender, EventArgs e)
        {
            this.getBusinessLocations();
            this.getRegisters();

            txtBusinessLocationId.EditValue = Properties.Settings.Default.BusinessLocation;

            if (this.type != "Add")
            {
                this.edit();
            }

            if (Properties.Settings.Default.isAdmin != "Admin")
            {
                layoutControlItemRegister.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemBusinessLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                txtBusinessLocationId.ReadOnly = true;
                txtRegisterId.ReadOnly = true;
            }
        }

        public void getBusinessLocations()
        {
            txtBusinessLocationId.Properties.DataSource = Shared.db.BusinessLocations.ToList();
            txtBusinessLocationId.Properties.DisplayMember = "Name"; // Set display member
            txtBusinessLocationId.Properties.ValueMember = "Id"; // Set value member
        }

        public void getRegisters()
        {
            txtRegisterId.Properties.DataSource = Shared.db.Registers.ToList();
            txtRegisterId.Properties.DisplayMember = "Name"; // Set display member
            txtRegisterId.Properties.ValueMember = "Id"; // Set value member
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OpenRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (Function.Helper.hasAlertQty())
            //{
            //    OverlayForm overlay = new OverlayForm(this);
            //    overlay.Show();

            //    Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();

            //    alertQuantity.FormClosed += (s, args) => overlay.Close();

            //    alertQuantity.Show();
            //    alertQuantity.TopMost = true;
            //}
        }
        public void loadRegisters(int Id)
        {
            txtRegisterId.Properties.DataSource = Shared.db.Registers.ToList();
            txtRegisterId.EditValue = Id;
        }
        private void btnQuickRegister_Click(object sender, EventArgs e)
        {
            AddEditRegister addEditRegister = new AddEditRegister();
            addEditRegister.setTypeOperation("Add");
            addEditRegister.setRegistersObject(this);
            addEditRegister.ShowDialog();
        }
    }
}