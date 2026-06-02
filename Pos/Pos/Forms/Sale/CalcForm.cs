using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Sale.Return
{
    public partial class CalcForm : DevExpress.XtraEditors.XtraForm
    {
        public CalcForm()
        {
            InitializeComponent();
        }
        private bool isFirst = true;

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is SimpleButton button)
            {
                string buttonName = button.Name;

                if (buttonName.StartsWith("btn"))
                {
                    string action = buttonName["btn".Length..];

                    switch (action)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            {
                                if (isFirst)
                                {
                                    txtValue.Text = action;
                                    isFirst = false;
                                }
                                else
                                {
                                    txtValue.Text += action;
                                }
                                break;
                            }

                        case "C":
                            txtValue.Text = string.Empty;
                            break;

                        case "CE":
                            if (!string.IsNullOrEmpty(txtValue.Text))
                            {
                                txtValue.Text = txtValue.Text[..^1];
                            }
                            break;

                        //case "Dot":
                        //    if (!txtDiscount.Text.Contains('.'))
                        //    {
                        //        txtDiscount.Text += ".";
                        //    }
                        //    break;

                        //case "Comma":
                        //    txtDiscount.Text += ",";
                        //    break;

                        //case "Lower":
                        //    if (!string.IsNullOrEmpty(txtDiscount.Text))
                        //    {
                        //        txtDiscount.Text = txtDiscount.Text.Remove(txtDiscount.Text.Length - 1);
                        //    }
                        //    break;

                        //case "X":
                        //    txtDiscount.Text = "Custom Logic for X";
                        //    break;

                        default:
                            break;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private int Value;
        private void btnSave_Click(object sender, EventArgs e)
        {
            Value = Convert.ToInt32(txtValue.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public int getValue()
        {
            return Value;
        }

        private void CalcForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }

    }
}