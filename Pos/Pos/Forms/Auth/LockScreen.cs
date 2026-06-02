using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Pos.Forms.Auth
{
    public partial class LockScreen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int BusinessLocationID = 0;
        public bool areCredentialsValid = false;
        bool forceclose = false;

        public LockScreen()
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

        private void LockScreen_Load(object sender, EventArgs e)
        {
            this.lockScreenImg();
        }

        public void lockScreenImg()
        {
            Models.Setting setting = Shared.db.Settings.FirstOrDefault();

            if (setting != null)
            {
                LockScreenImg.EditValue = setting.LockScreenImg;
            }
        }

        private void txtPin1_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPin1.EditValue != null)
            {
                txtPin2.Focus();
            }
        }

        private void txtPin2_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPin2.EditValue != null)
            {
                txtPin3.Focus();
            }
        }

        private void txtPin3_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPin3.EditValue != null)
            {
                txtPin4.Focus();
            }
        }

        private void txtPin4_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPin4.EditValue != null)
            {
                Models.User user = Shared.db.Users.FirstOrDefault(
                    p => p.PinOne == txtPin1.Text &&
                    p.PinTwo == txtPin2.Text &&
                    p.PinThree == txtPin3.Text &&
                    p.PinFour == txtPin4.Text
                );

                if (user != null)
                {
                    Sound.Added();
                    this.areCredentialsValid = true;
                    this.Hide();
                }
                else
                {
                    Sound.Wrong();
                    txtPin1.Text = "";
                    txtPin2.Text = "";
                    txtPin3.Text = "";
                    txtPin4.Text = "";
                    txtPin1.Focus();
                }
            }
        }

        private void txtPin1_Enter(object sender, EventArgs e)
        {
            txtPin1.Text = string.Empty;
        }

        private void txtPin2_Enter(object sender, EventArgs e)
        {
            txtPin2.Text = string.Empty;
        }

        private void txtPin3_Enter(object sender, EventArgs e)
        {
            txtPin3.Text = string.Empty;
        }

        private void txtPin4_Enter(object sender, EventArgs e)
        {
            txtPin4.Text = string.Empty;
        }

        private void LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (forceclose!=true)
            {
                if (!this.areCredentialsValid)
                {
                    // Cancel the closing if credentials are not valid
                    e.Cancel = true;
                    MessageBox.Show("Invalid credentials. Please try again.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // Check if the credentials are valid
          
        }
    }
}