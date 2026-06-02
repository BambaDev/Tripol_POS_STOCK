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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Pos.Forms.Sale;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Alert
{
    public partial class UpdateQty : DevExpress.XtraEditors.XtraForm
    {
        public int productId = 0;
        public decimal sellingPrice = 0;
        public PosScreen pos = null;
        public int CustomerId = 0;
        public bool IsDivisible = false;

        public UpdateQty(int productId, int customerId)
        {
            InitializeComponent();

            this.productId = productId;
            this.CustomerId = customerId;
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

        public void setObject(PosScreen pos)
        {
            this.pos = pos;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider.Validate())
            {
                decimal newQty = decimal.Parse(txtQty.EditValue.ToString());

                if (this.pos != null)
                {
                    this.pos.updateQtyByModal(this.pos.currentExistingRow, newQty, this.productId, decimal.Parse(txtSellingPrice.Text));
                    this.pos.SetNetTotalAmount();
                }

                Sound.Added();
                this.Close();
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateQty_Load(object sender, EventArgs e)
        {
            txtQty.Select();

            using (var context = new AppDbContext())
            {
                var product = context.Products.Include(u => u.Unit).Where(p => p.Id == this.productId).First();

                if (product != null)
                {
                    txtAlertQuantity.EditValue = product.AlertQuantity;
                    txtProductImage.EditValue = product.Image;
                    txtProductName.Text = product.ProductName;
                    txtSellingPrice.EditValue = product.SellingPrice;
                    txtUpdateQtyTotal.EditValue = product.SellingPrice;
                    txtUnit.EditValue = product.Unit.Name;
                    txtPricePerUnit.EditValue = product.PricePerUnit;

                    IsDivisible = product.IsDivisible;

                    if (!product.IsDivisible)
                    {
                        //layoutControlGroupIsDiv.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }

                    if (product != null) // Ensure product is not null before using it
                    {
                        decimal quantity = context.ProductWarehouses
                                           .Where(q => q.ProductId == product.Id)
                                           .Sum(q => (decimal?)q.Qty) ?? 0; // Use (decimal?) to handle cases where there are no matching records

                        // Display quantity in TextBox, using ToString with a format if needed
                        txtAlertQuantity.Text = quantity.ToString("N2"); // Optional: "N2" for 2 decimal places, modify as needed
                    }
                    else
                    {
                        MessageBox.Show("Product is not selected or invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.sellingPrice = decimal.Parse(product.SellingPrice.ToString());

                    using (AppDbContext AppDb = new AppDbContext())
                    {
                        var recentSaleDetail = AppDb.SaleDetails
                            .Include(sd => sd.Sale) // Include the related Sale entity
                                                    //.Where(sd => sd.ProductId == this.productId && sd.Sale.CustomerId == this.CustomerId)
                            .Where(sd => sd.ProductId == this.productId)
                            .OrderByDescending(sd => sd.Id) // Order by the most recent SaleDetail by Id
                            .Select(sd => new
                            {
                                sd.UnitSellingPrice,
                                sd.UnitCostBd
                            }) // Select UnitSellingPrice and UnitCostBd
                            .FirstOrDefault(); // Get the most recent sale detail

                        if (recentSaleDetail != null)
                        {
                            // Use recentSaleDetail to populate the text boxes
                            txtLastSellingPrice.Text = recentSaleDetail.UnitSellingPrice.ToString();
                            lastPurchasePrice.Text = recentSaleDetail.UnitCostBd.ToString();
                        }
                        else
                        {
                            // No result found, use fallback value for selling price
                            txtLastSellingPrice.Text = txtSellingPrice.Text;
                        }

                        var recentPurchaseDetail = AppDb.PurchaseDetails
                            .Include(sd => sd.Purchase) // Include the related Sale entity
                                                        //.Where(sd => sd.ProductId == this.productId && sd.Purchase.CustomerId == this.CustomerId)
                            .Where(sd => sd.ProductId == this.productId)
                            .OrderByDescending(sd => sd.Id) // Order by the most recent SaleDetail by Id
                            .Select(sd => new
                            {
                                sd.UnitCostBt
                            }) // Select UnitSellingPrice and UnitCostBd
                            .FirstOrDefault(); // Get the most recent sale detail

                        if (recentPurchaseDetail != null)
                        {
                            // Use recentSaleDetail to populate the text boxes
                            lastPurchasePrice.Text = recentPurchaseDetail.UnitCostBt.ToString();
                        }
                    }

                    gridControlCharacteristics.DataSource = context.ProductHasFields.Where(p => p.ProductId == this.productId).ToList();

                    gridControlPurchases.DataSource = context.PurchaseDetails
                        .Include(pd => pd.Purchase.Supplier) // Load related Supplier data
                        .Where(pd => pd.ProductId == product.Id) // Filter by product ID
                        .OrderByDescending(pd => pd.Id) // Order by PurchaseDetail ID, most recent first
                        .Select(pd => new
                        {
                            pd.Id,
                            pd.ProductId,
                            pd.PurchaseQuantity,
                            pd.LineTotal,
                            pd.UnitCostBt, // Assuming you want to display unit cost
                            SupplierName = pd.Purchase.Supplier.FirstName + " " + pd.Purchase.Supplier.LastName, // Assuming Supplier has a Name property
                            PurchaseDate = pd.Purchase.PurchaseDate // Assuming Purchase has a PurchaseDate field
                        })
                        .ToList();
                }
            }
        }

        private void txtQty_EditValueChanged(object sender, EventArgs e)
        {
            if (txtQty.EditValue != null && this.sellingPrice > 0)
            {
                // Try to parse the quantity as a decimal
                if (decimal.TryParse(txtQty.EditValue.ToString(), out decimal quantity) && quantity > 0)
                {
                    // Calculate the total based on whether the item is divisible or not
                    if (this.IsDivisible)
                    {
                        //txtUpdateQtyTotal.EditValue = quantity * this.sellingPrice;
                        txtUpdateQtyTotal.EditValue = quantity * decimal.Parse(txtSellingPrice.Text);
                    }
                    else
                    {
                        txtQty.Text = Math.Floor(quantity).ToString();
                        // If not divisible, we assume quantity should be treated as an integer
                        txtUpdateQtyTotal.EditValue = Math.Floor(quantity) * decimal.Parse(txtSellingPrice.Text);
                    }
                }
                else
                {
                    // Handle cases where parsing fails or quantity is less than or equal to zero
                    txtUpdateQtyTotal.EditValue = 0;
                }
            }

            if (this.pos != null)
                this.pos.CalculateTotalBenefit();
        }

        public void setQty(decimal qty = 0)
        {
            txtQty.EditValue = qty;
        }

        private void txtSellingPrice_EditValueChanged(object sender, EventArgs e)
        {
            if (txtQty.EditValue != null && decimal.Parse(txtSellingPrice.Text) > 0)
            {
                // Try to parse the quantity as a decimal
                if (decimal.TryParse(txtQty.EditValue.ToString(), out decimal quantity) && quantity > 0)
                {
                    // Calculate the total based on whether the item is divisible or not
                    if (this.IsDivisible)
                    {
                        //txtUpdateQtyTotal.EditValue = quantity * this.sellingPrice;
                        txtUpdateQtyTotal.EditValue = quantity * decimal.Parse(txtSellingPrice.Text);
                    }
                    else
                    {
                        txtQty.Text = Math.Floor(quantity).ToString();
                        // If not divisible, we assume quantity should be treated as an integer
                        txtUpdateQtyTotal.EditValue = Math.Floor(quantity) * decimal.Parse(txtSellingPrice.Text);
                    }
                }
                else
                {
                    // Handle cases where parsing fails or quantity is less than or equal to zero
                    txtUpdateQtyTotal.EditValue = 0;
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSellingPrice.Focus(); // Move focus to txtSellingPrice
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtSellingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick(); // Trigger the btnOk click
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}