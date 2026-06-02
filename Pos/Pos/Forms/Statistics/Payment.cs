using DevExpress.XtraEditors;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.Statistics
{
    public partial class Payment : DevExpress.XtraEditors.XtraForm
    {
        public int customerId = 0;
        public decimal currentDue = 0;
        public Customer.Customers customers = null;

        public Payment(int customerId)
        {
            InitializeComponent();

            this.toRtl();

            this.customerId = customerId;
            this.BorderStyle();
        }

        public void BorderStyle()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang != "ar")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            }
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

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            simpleLabelItem2.AppearanceItemCaption.Font = customFont;
            layoutControlItem6.AppearanceItemCaption.Font = customFont;
            btnPay.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
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

        public void setObject(Customer.Customers customers)
        {
            this.customers = customers;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider.Validate())
            {
                decimal amountPaid = decimal.Parse(txtAmountPaid.EditValue.ToString());

                if (this.customers != null)
                    ProcessPayment(this.customerId, amountPaid);

                Sound.Added();
                this.Close();
            }
        }

        public void ProcessPayment(int customerId, decimal paymentAmount)
        {
            //using (var context = new AppDbContext()) // Assuming Entity Framework for DB Context
            //{
            //    var customer = context.Customers.Include(c => c.Sale).FirstOrDefault(c => c.Id == customerId);
            //    if (customer == null) return;

            //    decimal remainingPayment = paymentAmount;

            //    // Process each case, ordered by some priority, e.g., due date
            //    foreach (var _case in customer.Sale.Where(c => c.Due > 0).OrderBy(c => c.DateOfReceipt))
            //    {
            //        if (remainingPayment <= 0) break;

            //        decimal amountToPay = Math.Min(decimal.Parse(_case.Due.ToString()), remainingPayment);

            //        _case.Due -= amountToPay;
            //        remainingPayment -= amountToPay;

            //        // Record each payment applied to a case
            //        var payment = new Models.SalePayment
            //        {
            //            UserId = Properties.Settings.Default.userId,
            //            CustomerId = customerId,
            //            //CaseId = _case.Id,
            //            Amount = amountToPay,
            //            Due = _case.Due, // Remaining due after payment
            //            CreatedAt = DateTime.Now,
            //            UpdatedAt = DateTime.Now,
            //        };

            //        context.SalePayments.Add(payment);
            //    }

            //    // Update the customer's current due
            //    customer.CurrentDue = customer.Sale.Sum(c => c.Due);

            //    context.SaveChanges();
            //}
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                txtAmountPaid.Select();
                txtAmountPaid.EditValue = 1;

                var customer = context.Customers.Find(this.customerId);

                if (customer != null)
                {
                    //txtFullName.Text = customer.FullName;
                    txtEmail.Text = customer.Email;
                    txtGender.Text = customer.Gender;
                    txtCustomerImage.EditValue = customer.Image;
                    txtTotalDue.EditValue = customer.CurrentDue;

                    this.currentDue = decimal.Parse(customer.CurrentDue.ToString());
                }
            }
        }

        private void txtAmountPaid_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmountPaid.Text, out decimal amountPaid) &&
                decimal.TryParse(txtTotalDue.Text, out decimal totalDue))
            {
                decimal returnAmount = amountPaid - totalDue;
                txtReturnAmount.Text = returnAmount > 0 ? returnAmount.ToString("0.##") : "0.00";

                // Calculate the new current due by subtracting the amount paid from the total due.
                decimal currentDue = totalDue - amountPaid;

                // Update the txtCurrentDue textbox with the new current due amount.
                // If the current due is less than zero, display zero instead (no negative dues).
                txtCurrentDue.Text = currentDue > 0 ? currentDue.ToString("0.##") : "0.00";
            }
            else
            {
                txtReturnAmount.Text = "0.00";
            }
        }

        private void txtTotalDue_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTotalDue.Text, out decimal totalDue) &&
                decimal.TryParse(txtAmountPaid.Text, out decimal amountPaid))
            {
                decimal currentDue = totalDue - amountPaid;
                txtCurrentDue.Text = currentDue >= 0 ? currentDue.ToString("0.##") : "0.00";
                txtReturnAmount.Text = (amountPaid - totalDue > 0 ? amountPaid - totalDue : 0).ToString("0.##");
            }
        }

        private void txtReturnAmount_EditValueChanged(object sender, EventArgs e)
        {
            // Optionally add validation or additional logic here if needed
        }

        private void txtCurrentDue_EditValueChanged(object sender, EventArgs e)
        {
            // Example: Update UI to reflect changes in due status
            if (decimal.TryParse(txtCurrentDue.Text, out decimal currentDue))
            {
                if (currentDue <= 0)
                {
                    // Change UI to show no outstanding payments, or disable payment controls
                }
            }
        }

    }
}