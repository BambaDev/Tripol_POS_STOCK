using DevExpress.XtraEditors;
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
    public partial class ShowCustomAlert : XtraForm
    {
        private Timer timer;
        public bool hideMessage = false;
        public AlertIcon icon = AlertIcon.Loading;
        public AlertPosition position = AlertPosition.Center;
        string msgSuccess = "Operation accomplished successfully.";

        public ShowCustomAlert(string message = "", int interval = 2000,bool canInitializeTimer = true)
        {
            InitializeComponent();

            this.toRtl();

            simpleLabelItemMessage.Text = message;

            if(canInitializeTimer)
            {
                InitializeTimer(interval);
            }

            CustomizeForm();
        }

        public void trnsMsgSuccess()
        {
            string lang = Properties.Settings.Default.Lang;

            simpleLabelItemMessage.Text = msgSuccess;

            if (lang == "fr")
            {
                simpleLabelItemMessage.Text = "Opération accomplie avec succès.";
            } else if(lang == "ar")
            {
                simpleLabelItemMessage.Text = "تمت العملية بنجاح.";
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
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            simpleLabelItemMessage.AppearanceItemCaption.Font = customFont;
        }

        private void InitializeTimer(int interval)
        {
            this.timer = new Timer { Interval = interval };
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void CustomizeForm()
        {
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.timer.Dispose();
            this.Close();
        }

        private void ShowCustomAlert_Load(object sender, EventArgs e)
        {
            if (this.hideMessage)
                simpleLabelItemMessage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            SetPosition();
            SetIcon();
        }

        public void SetPosition(AlertPosition position = AlertPosition.Center, int margin = 20)
        {
            this.position = position;
            this.StartPosition = FormStartPosition.Manual;

            int x = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            switch (position)
            {
                case AlertPosition.Center:
                    this.StartPosition = FormStartPosition.CenterScreen;
                    return;
                case AlertPosition.Top:
                case AlertPosition.TopCenter:
                    y = margin;
                    break;
                case AlertPosition.TopRight:
                    x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width - margin;
                    y = margin;
                    break;
                case AlertPosition.TopLeft:
                    x = margin;
                    y = margin;
                    break;
                case AlertPosition.Right:
                    x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width - margin;
                    break;
                case AlertPosition.Left:
                    x = margin;
                    break;
                case AlertPosition.Bottom:
                case AlertPosition.BottomCenter:
                    y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height - margin;
                    break;
                case AlertPosition.BottomLeft:
                    x = margin;
                    y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height - margin;
                    break;
                case AlertPosition.BottomRight:
                    x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width - margin;
                    y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height - margin;
                    break;
                default:
                    this.StartPosition = FormStartPosition.CenterScreen;
                    return;
            }

            this.Location = new Point(x, y);
        }


        public void SetIcon(AlertIcon icon = AlertIcon.Success)
        {
            this.icon = icon;

            switch (icon)
            {
                case AlertIcon.Loading:
                    alertIcon.Image = Properties.Resources.spinner200px200px;
                    break;
                case AlertIcon.Success:
                    alertIcon.Image = Properties.Resources.spinner200px200pxSaved;
                    break;
                case AlertIcon.Error:
                    alertIcon.Image = Properties.Resources.alertErrorGif;
                    break;
                case AlertIcon.Warning:
                    alertIcon.Image = Properties.Resources.alertErrorGif;
                    break;
                case AlertIcon.Info:
                    alertIcon.Image = Properties.Resources.alertErrorGif;
                    break;
                default:
                    alertIcon.Image = Properties.Resources.spinner200px200px;
                    break;
            }
        }

        public void SetIcon(Image icon)
        {
            alertIcon.Image = icon;
        }

        public void HideMessage()
        {
            simpleLabelItemMessage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new System.IO.MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        public enum AlertIcon
        {
            Loading,
            Success,
            Error,
            Warning,
            Info
        }

        public enum AlertPosition
        {
            Center,
            Top,
            TopCenter,
            TopRight,
            TopLeft,
            Right,
            Left,
            Bottom,
            BottomCenter,
            BottomLeft,
            BottomRight
        }
    }
}