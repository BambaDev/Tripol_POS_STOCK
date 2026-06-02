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

namespace Pos.Forms.Json
{
    public partial class jsonViewer : DevExpress.XtraEditors.XtraForm
    {
        private string _json;

        public jsonViewer()
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

        public string Json
        {
            get { return _json; }
            set
            {
                _json = value;
                memoEdit.Text = PrettyPrintJson(_json);
            }
        }

        // Pretty print JSON
        private string PrettyPrintJson(string jsonStr)
        {
            try
            {
                if (string.IsNullOrEmpty(jsonStr)) return string.Empty;
                var obj = Newtonsoft.Json.Linq.JToken.Parse(jsonStr);
                return obj.ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                return jsonStr;  // Return original string if parsing fails
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}