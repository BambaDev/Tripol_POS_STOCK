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

namespace Pos.Forms.Inventory
{
    public partial class InventoryHistory : DevExpress.XtraEditors.XtraForm
    {
        public InventoryHistory()
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

        private void InventoryHistory_Load(object sender, EventArgs e)
        {
            this.GetInventoriesHistories();
        }

        public void GetInventoriesHistories()
        {
            gridControlInventoryHistories.DataSource = Shared.db.Inventories
            .GroupBy(e => new { Year = e.InventoryYear, Month = e.InventoryMonth })
            .Select(group => new
            {
                Year = group.Key.Year,
                Month = Function.Helper.MonthNameByNumber(int.Parse(group.Key.Month.ToString())),
                TotalAmount = group.Sum(e => e.Gap) + " DA"
            })
            .ToList();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}