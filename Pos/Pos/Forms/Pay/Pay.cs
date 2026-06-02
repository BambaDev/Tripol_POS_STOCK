using DevExpress.XtraEditors;
using Pos.Forms.Sale;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
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

namespace Pos.Forms.Pay.Pay
{
    public partial class Pay : DevExpress.XtraEditors.XtraForm
    {
        public PosScreen pos;

        public Pay(Object obj)
        {
            InitializeComponent();

            if (obj is PosScreen)
            {
                this.pos = (PosScreen)obj;
            }

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

        #region MyRegion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //add 5 to paid amount
        private void btnChange1_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(5);
        }
        //add 10 to paid amount
        private void btnChange2_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(10);
        }

        //add 20 to paid amount
        private void btnChange3_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(20);
        }
        //add 50 to paid amount
        private void btnChange4_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(50);
        }
        //add 100 to paid amount
        private void btnChange5_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(100);
        }
        //add 200 to paid amount
        private void btnChange6_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(200);
        }
        //add 500 to paid amount
        private void btnPaper1_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(500);
        }
        //add 1000 to paid amount
        private void btnPaper2_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(1000);
        }
        //add 2000 to paid amount
        private void btnPaper3_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(2000);
        }

        //add 10 to paid amount
        private void btnMoney1_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(10);
        }
        //add 20 to paid amount
        private void btnMoney2_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(20);
        }
        //add 50 to paid amount
        private void btnMoney3_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(50);
        }
        //add 100 to paid amount
        private void btnMoney4_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(100);
        }
        //add 200 to paid amount
        private void btnMoney5_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(200);
        }
        //add 500 to paid amount
        private void btnMoney6_Click(object sender, EventArgs e)
        {
            UpdatePaidAmount(500);
        }
        //from btn1 to btn9 its like a calculator where the user types directly the amount given by the customer and puts the numers in txtPaid and atuomaticly calculates all the txtfileds related to the payment
        private void btn1_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "8";

        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtPaid.Text += "0";
        }

        #endregion

        //validate the payment and save it into the database alongside with the order and orderitems , basicly it does the same job as btnPay_Click
        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pos != null)
                    this.pos.pay();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //to make it realtime each changeis done here is reflected in txtreturn and txtLeftAmount
        private void txtPaid_EditValueChanged(object sender, EventArgs e)
        {
            // Logic to update txtReturn and txtLeftAmount based on txtPaid's value
            // Assuming txtOrderTotal is available and contains the total amount to be paid
            decimal.TryParse(txtPaid.Text, out decimal paidAmount);
            decimal.TryParse(txtOrderTotal.Text, out decimal totalAmount);
            decimal leftAmount = totalAmount - paidAmount;
            decimal returnAmount = paidAmount - totalAmount;

            txtLeftAmount.Text = leftAmount > 0 ? leftAmount.ToString() : "0";
            txtReturn.Text = returnAmount > 0 ? returnAmount.ToString() : "0";

            if (this.pos != null)
            {
                this.pos.updatePaidAmoutByPay(paidAmount);
            }
        }

        // Update the paid amount based on the button pressed
        private void UpdatePaidAmount(decimal amount)
        {
            if (decimal.TryParse(txtPaid.Text, out decimal paidAmount))
            {
                txtPaid.Text = (paidAmount + amount).ToString();
            }
            else
            {
                txtPaid.Text = amount.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPaid.Clear();
        }

        private void txtOrderTotal_DoubleClick(object sender, EventArgs e)
        {
            // Set the value of txtPaid to be the same as txtOrderTotal
            txtPaid.Text = txtOrderTotal.Text;
        }

        private void Pay_Load(object sender, EventArgs e)
        {
            if (this.pos != null)
            {
                txtOrderTotal.Text = this.pos.calculeTotal().ToString();
                txtPaid.Text = this.pos.getPaidAmount().ToString();
                txtLeftAmount.Text = (this.pos.calculeTotal() - this.pos.getPaidAmount()).ToString();
                this.pos.SetTotalDueAmount();
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {

        }

        private void txtOrderTotal_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
