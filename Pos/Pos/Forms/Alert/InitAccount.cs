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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using System.Diagnostics;

namespace Pos.Forms.Alert
{
    public partial class InitAccount : DevExpress.XtraEditors.XtraForm
    {
        public string purchaseCode = "";

        public InitAccount()
        {
            InitializeComponent();

            this.toRtl();
            Sound.Denied();
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

            simpleLabelItem1.AppearanceItemCaption.Font = customFont;
            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dxValidationProviderPurchaseCode.Validate())
            {
                btnSave.Enabled = false;

                using(var context = new AppDbContext())
                {
                    // Save user and activation state to the database
                    Models.User user = new Models.User
                    {
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        FullName = $"{txtFirstName.Text} {txtLastName.Text}",
                        Email = txtEmail.Text,
                        UserLogin = txtUserLogin.Text,
                        Password = Function.PasswordHelper.HashPassword(txtPassword.Text),
                        IsAdmin = "Admin",
                        BusinessLocationId = 1,
                        PinOne = "0",
                        PinTwo = "0",
                        PinThree = "0",
                        PinFour = "0",
                        Status = "Active",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    Properties.Settings.Default.SuperAdminPassword = txtPassword.Text;
                    Properties.Settings.Default.Save();

                    context.Users.Add(user);

                    context.SaveChanges();

                    this.ReloadForm();
                }
            }
            else
            {
                Sound.Wrong();
            }
        }

        public void ReloadForm()
        {
            // Get the path to the current executable
            string exePath = Application.ExecutablePath;

            // Start a new instance of the application
            Process.Start(exePath);

            // Close the current application instance
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Sound.Wrong();
            System.Windows.Forms.Application.Exit();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}