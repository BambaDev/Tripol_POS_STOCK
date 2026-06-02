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
    public partial class AlertQuantity : DevExpress.XtraEditors.XtraForm
    {
        public DataTable dt = new DataTable();

        public AlertQuantity()
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

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AlertQuantity_Load(object sender, EventArgs e)
        {
            this.loadProducts();
        }

        public void loadProducts()
        {
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Image", typeof(byte[]));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("AlertQuantity", typeof(int));
                dt.Columns.Add("Quantity", typeof(decimal));
                dt.Columns.Add("SellingPrice", typeof(decimal));
                dt.Columns.Add("Total", typeof(decimal));
            }

            var prducts = from p in Shared.db.Products
                          join pw in Shared.db.ProductWarehouses on p.Id equals pw.ProductId
                          where pw.Qty <= p.AlertQuantity
                          select new
                          {
                              p.Id,
                              p.Image,
                              p.ProductName,
                              p.AlertQuantity,
                              Quantity = pw.Qty,
                              p.SellingPrice
                          };

            foreach (var product in prducts.ToList())
            {
                DataRow NewRow = dt.NewRow();
                NewRow["Id"] = product.Id;
                NewRow["Image"] = product.Image;
                NewRow["ProductName"] = product.ProductName;
                NewRow["AlertQuantity"] = product.AlertQuantity;
                NewRow["Quantity"] = product.Quantity;
                NewRow["SellingPrice"] = product.SellingPrice;
                NewRow["Total"] = Convert.ToDecimal(NewRow["Quantity"]) * Convert.ToDecimal(product.SellingPrice);

                dt.Rows.Add(NewRow);
            }

            gridControlProducts.DataSource = dt;
        }
    }
}