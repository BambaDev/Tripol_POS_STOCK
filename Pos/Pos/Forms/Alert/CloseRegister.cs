using DevExpress.XtraEditors;
using Pos.Function;
using Pos.Models;
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

namespace Pos.Forms.Alert
{
    public partial class CloseRegister : DevExpress.XtraEditors.XtraForm
    {
        public CloseRegister()
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

        private void btnCloseRegister_Click(object sender, EventArgs e)
        {
            Function.Helper.CloseRegister(
               Properties.Settings.Default.userId,
               Properties.Settings.Default.BusinessLocation
            );

            if (Function.Helper.canSendMessageViaWhatsUp())
            {
                Models.Setting setting = Function.Helper.getSetting();

                string message = Function.Helper.CreateCloseRegisterMessage(Properties.Settings.Default.BusinessLocation);

                WhatsAppMessageSender whatsAppMessageSender = new Function.WhatsAppMessageSender(
                    setting.AccountSid,
                    setting.AuthToken
                );

                whatsAppMessageSender.SendMessageAsync(
                    setting.FromPhoneNumber,
                    "782394356",
                    message
                );
            }

            Sound.Added();

            Register.OpenRegister openRegister = new Register.OpenRegister(this);
            openRegister.ShowDialog();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}