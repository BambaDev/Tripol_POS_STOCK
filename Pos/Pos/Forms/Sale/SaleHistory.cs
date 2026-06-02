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

namespace Pos.Forms.Sale
{
    public partial class SaleHistory : DevExpress.XtraEditors.XtraForm
    {
        public SaleHistory()
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

        private void PurchaseHistory_Load(object sender, EventArgs e)
        {
            this.GetSaleHistories();
        }

        public void GetSaleHistories()
        {
            gridControlSaleHistories.DataSource = Shared.db.Sales
            .GroupBy(e => new { Year = e.SaleYear, Month = e.SaleMonth })
            .Select(group => new
            {
                Year = group.Key.Year,
                Month = Function.Helper.MonthNameByNumber(int.Parse(group.Key.Month.ToString())),
                TotalAmount = group.Sum(e => e.NetTotalAmount) + " DA"
            })
            .ToList();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}