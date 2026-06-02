using DevExpress.XtraEditors;
using Pos.Function;
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
    public partial class AccessDenied : DevExpress.XtraEditors.XtraForm
    {
        public AccessDenied()
        {
            InitializeComponent();
            Sound.Denied();
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

        private void btnAccessDenied_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.Close();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}