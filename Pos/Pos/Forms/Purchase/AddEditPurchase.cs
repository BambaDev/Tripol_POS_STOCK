using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Overlay;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Supplier;
using Pos.Function;
using Pos.Models;
using Pos.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Filtering.ExcelFilterOptions;

namespace Pos.Forms.Purchase
{
    public partial class AddEditPurchase : DevExpress.XtraEditors.XtraForm
    {
        private OverlayForm overlay;
        public Purchases purchases = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public string purchaseType = "Carry in";
        public int purchase_id = 0;
        public int row_idex = 0;
        public int currentItemId = 0;

        public AddEditPurchase(bool maximized = true)
        {
            InitializeComponent();

            if (maximized)
                this.WindowState = FormWindowState.Maximized;
        }

        private void ShowOverlay()
        {
            if (overlay == null)
            {
                overlay = new OverlayForm(this);
                overlay.Show();
            }
        }

        private void HideOverlay()
        {
            if (overlay != null)
            {
                overlay.Close();
                overlay.Dispose();
                overlay = null;
            }
        }

        public void setPurchasesObject(Purchases purchases)
        {
            this.purchases = purchases;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void ReferenceNo()
        {
            int lastId = Shared.db.Purchases.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("PU", lastId);
        }

        public void edit()
        {
            if (this.purchases != null || currentItemId != 0)
            {
                this.currentItemId = this.purchases != null ? this.purchases.purchase_id : currentItemId;

                Models.Purchase purchase = Shared.db.Purchases.Find(this.currentItemId);

                if (purchase != null)
                {
                    this.purchase_id = purchase.Id;

                    if (purchase.PurchaseType == "Carry in")
                    {
                        txtPurchaseType.SelectedIndex = 0;
                    }
                    else if (purchase.PurchaseType == "Pick up")
                    {
                        txtPurchaseType.SelectedIndex = 1;
                    }
                    else
                    {
                        txtPurchaseType.SelectedIndex = 2;
                    }

                    txtPurchaseDate.EditValue = DateTime.Parse(purchase.PurchaseDate.ToString());
                    txtReferenceNo.Text = purchase.ReferenceNo;
                    txtDiscountType.EditValue = purchase.DiscountType;
                    txtPurchaseType.EditValue = purchase.PurchaseType;
                    txtPurchaseStatus.EditValue = purchase.PurchaseStatus;
                    txtPaymentStatus.EditValue = purchase.PaymentSatus;
                    txtDiscountAmount.EditValue = decimal.Parse(purchase.DiscountAmount.ToString());
                    txtAdditionalNotes.Text = purchase.AdditionalNotes;
                    txtShippingDetails.Text = purchase.ShippingDetails;
                    txtAdditionalShippingCharges.EditValue = decimal.Parse(purchase.AdditionalShippingCharges.ToString());
                    txtPaidAmount.EditValue = decimal.Parse(purchase.PaidAmount.ToString());
                    txtTotalTax.EditValue = decimal.Parse(purchase.TotalTax.ToString());
                    txtTotalDiscount.EditValue = decimal.Parse(purchase.TotalDiscount.ToString());
                    txtDue.EditValue = decimal.Parse(purchase.Due.ToString());
                    txtReturnAmount.EditValue = decimal.Parse(purchase.ReturnAmount.ToString());
                    txtNetTotalAmount.Text = purchase.NetTotalAmount.ToString();
                    txtBussLocation.EditValue = Int32.Parse(purchase.BusinessLocationId.ToString());
                    txtSupplier.EditValue = Int32.Parse(purchase.SupplierId.ToString());
                    txtWarehouse.EditValue = Int32.Parse(purchase.WarehouseId.ToString());

                    txtSuppFirstName.Text = purchase.Supplier.FirstName;
                    txtSuppLastName.Text = purchase.Supplier.LastName;
                    totalDue.Text = Function.Helper.FormatAmount(purchase.Supplier.Purchases.Sum(d => d.Due).ToString());

                    gridControlPurchases.DataSource = purchase.Supplier.Purchases;

                    this.getPurchaseItems(this.purchase_id);

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getPurchaseItems(int id)
        {
            gridControlProducts.DataSource = Shared.db.PurchaseDetails.Where(m => m.PurchaseId == id).ToList();
        }

        private void btnSaveMaintenance_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderPurchase.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Purchase purchase;

                    if (dt.Rows.Count == 0)
                    {
                        Sound.Wrong();
                        XtraMessageBox.Show("Please add at least one items.");
                        return;
                    }

                    if (this.type == "Add")
                    {
                        purchase = new Models.Purchase();

                        Models.Supplier supplier = new Models.Supplier();
                        bool newSupplier = false;

                        if (txtSuppFirstName.Text.Length > 0 && txtSuppLastName.Text.Length > 0)
                        {
                            supplier.FirstName = txtSuppFirstName.Text;
                            supplier.LastName = txtSuppLastName.Text;
                            supplier.CreatedAt = DateTime.Now;
                            supplier.UpdatedAt = DateTime.Now;

                            Shared.db.Suppliers.Add(supplier);

                            Shared.db.SaveChanges();

                            newSupplier = true;
                        }

                        decimal due = 0;
                        decimal returnAmount = 0;

                        if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                        {
                            returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                        }
                        else
                        {
                            due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                        }

                        purchase.PurchaseDate = DateTime.Parse(txtPurchaseDate.EditValue.ToString());
                        purchase.ReferenceNo = txtReferenceNo.Text;
                        purchase.DiscountType = txtDiscountType.Text;
                        purchase.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                        purchase.PurchaseStatus = txtPurchaseStatus.Text;
                        purchase.PaymentSatus = txtPaymentStatus.Text;
                        purchase.AdditionalNotes = txtAdditionalNotes.Text;
                        purchase.ShippingDetails = txtShippingDetails.Text;
                        purchase.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                        purchase.NetTotalAmount = this.calculeTotal();
                        purchase.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                        purchase.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                        purchase.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                        purchase.Due = due;
                        purchase.ReturnAmount = returnAmount;
                        purchase.NumberItems = dt.Rows.Count;
                        purchase.BusinessLocationId = txtBussLocation.EditValue == null ? Properties.Settings.Default.BusinessLocationId : Int32.Parse(txtBussLocation.EditValue.ToString());
                        purchase.SupplierId = newSupplier ? supplier.Id : Int32.Parse(txtSupplier.EditValue.ToString());
                        purchase.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        purchase.UserId = Properties.Settings.Default.userId;
                        purchase.PurchaseMonth = DateTime.Now.Month;
                        purchase.PurchaseYear = DateTime.Now.Year;
                        purchase.CreatedAt = DateTime.Now;
                        purchase.UpdatedAt = DateTime.Now;

                        context.Purchases.Add(purchase);

                        context.SaveChanges();

                        List<PurchaseDetail> purchaseDetail = new List<PurchaseDetail>();

                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            PurchaseDetail PurchaseDetailList = new PurchaseDetail();
                            PurchaseDetailList.ProductId = Convert.ToInt32(this.dt.Rows[i]["ProductId"]);
                            PurchaseDetailList.PurchaseId = purchase.Id;
                            PurchaseDetailList.ProductName = this.dt.Rows[i]["ProductName"].ToString();
                            PurchaseDetailList.PurchaseQuantity = Convert.ToInt32(this.dt.Rows[i]["PurchaseQuantity"]);
                            //PurchaseDetailList.UnitCostBd = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBd"]);
                            PurchaseDetailList.DiscountPercent = Convert.ToDecimal(this.dt.Rows[i]["DiscountPercent"]);
                            PurchaseDetailList.UnitCostBt = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBt"]);
                            PurchaseDetailList.LineTotal = Convert.ToDecimal(this.dt.Rows[i]["LineTotal"]);
                            PurchaseDetailList.ProfitMargin = Convert.ToDecimal(this.dt.Rows[i]["UnitSellingPrice"]) - Convert.ToDecimal(this.dt.Rows[i]["UnitCostBt"]);
                            PurchaseDetailList.UnitSellingPrice = Convert.ToDecimal(this.dt.Rows[i]["UnitSellingPrice"]);
                            PurchaseDetailList.CreatedAt = DateTime.Now;
                            PurchaseDetailList.UpdatedAt = DateTime.Now;

                            purchaseDetail.Add(PurchaseDetailList);

                            //using (AppDbContext AppDb = new AppDbContext())
                            //{
                            Models.Product product = context.Products.SingleOrDefault(x => x.Id == PurchaseDetailList.ProductId);
                            product.SellingPrice = PurchaseDetailList.UnitSellingPrice;
                            product.PurchasePriceExcTax = PurchaseDetailList.UnitCostBt;
                            context.Products.Update(product);
                            context.SaveChanges();

                            this.PurchaseProductAsync(Convert.ToInt32(PurchaseDetailList.ProductId), Convert.ToInt32(product.UnitId), Convert.ToInt32(purchase.WarehouseId), Convert.ToInt32(PurchaseDetailList.PurchaseQuantity), Convert.ToInt32(PurchaseDetailList.LineTotal));
                            //}
                        }

                        context.PurchaseDetails.AddRange(purchaseDetail);
                        context.SaveChanges();

                        txtDiscountAmount.EditValue = 0;
                        txtAdditionalShippingCharges.EditValue = 0;
                        txtAdditionalNotes.Text = "";
                        txtShippingDetails.Text = "";
                        txtReferenceNo.Text = "";
                        txtNetTotalAmount.Text = "00000000.00 DA";
                    }
                    else
                    {
                        if (this.purchases != null)
                            this.currentItemId = this.purchases.purchase_id;

                        purchase = context.Purchases.Find(this.currentItemId);

                        decimal due = 0;
                        decimal returnAmount = 0;

                        if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                        {
                            returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                        }
                        else
                        {
                            due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                        }

                        if (purchase != null)
                        {
                            purchase.PurchaseDate = DateTime.Parse(txtPurchaseDate.EditValue.ToString());
                            purchase.ReferenceNo = txtReferenceNo.Text;
                            purchase.DiscountType = txtDiscountType.Text;
                            purchase.PurchaseStatus = txtPurchaseStatus.Text;
                            purchase.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                            purchase.PaymentSatus = txtPaymentStatus.Text;
                            purchase.AdditionalNotes = txtAdditionalNotes.Text;
                            purchase.ShippingDetails = txtShippingDetails.Text;
                            purchase.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                            purchase.NetTotalAmount = this.calculeTotal();
                            purchase.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                            purchase.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                            purchase.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                            purchase.ReturnAmount = returnAmount;
                            purchase.Due = due;
                            purchase.NumberItems = gridViewProducts.RowCount;
                            purchase.BusinessLocationId = Int32.Parse(txtBussLocation.EditValue.ToString());
                            purchase.SupplierId = Int32.Parse(txtSupplier.EditValue.ToString());
                            purchase.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                            purchase.UpdatedAt = DateTime.Now;

                            context.Entry(purchase).State = EntityState.Modified;

                            context.SaveChanges();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show("Please select item !");
                        }
                    }

                    Function.Sound.Added();

                    if (this.purchases != null)
                        this.purchases.loadPurchases();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.ReferenceNo();
            this.getTodaysSupplier();
            //this.todayTotalProfit();

            SplashScreenManager.CloseForm();
        }

        public void changeNumberItems(int purchase_id)
        {
            Models.Purchase purchase = Shared.db.Purchases.Find(purchase_id);

            if (purchase != null)
            {
                if (type == "Add")
                {
                    purchase.NumberItems = dt.Rows.Count;
                }
                else
                {
                    purchase.NumberItems = gridViewProducts.RowCount;
                    purchase.NetTotalAmount = this.calculeTotal();
                }

                purchase.UpdatedAt = DateTime.Now;

                Shared.db.Entry(purchase).State = EntityState.Modified;

                Shared.db.SaveChanges();
            }
        }

        public decimal calculeTotal()
        {
            decimal total = 0;

            if (type == "Add")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total += decimal.Parse(dt.Rows[i]["LineTotal"].ToString());
                }
            }
            else
            {
                for (int i = 0; i < gridViewProducts.RowCount; i++)
                {
                    total += decimal.Parse(gridViewProducts.GetRowCellValue(i, "LineTotal").ToString());
                }
            }

            txtTotal.Text = total.ToString();

            return total;
        }

        private void AddEditMaintenance_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }
            else
            {
                txtPaymentStatus.EditValue = "UnPaid";
                txtDiscountType.EditValue = "None";
                txtPurchaseStatus.EditValue = "Received";
                txtPurchaseDate.EditValue = DateTime.Now;
            }

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("ProductId", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("PurchaseQuantity", typeof(int));
                dt.Columns.Add("UnitCostBd", typeof(decimal));
                dt.Columns.Add("DiscountPercent", typeof(decimal));
                dt.Columns.Add("UnitCostBt", typeof(decimal));
                dt.Columns.Add("LineTotal", typeof(decimal));
                dt.Columns.Add("ProfitMargin", typeof(decimal));
                dt.Columns.Add("UnitSellingPrice", typeof(decimal));
            }

            this.getBusinessLocations();
            this.getWarehouses();
            this.getSuppliers();
            this.getProducts();

            txtBussLocation.EditValue = Properties.Settings.Default.BusinessLocationId;

            Models.Warehouse warehouse = Shared.db.Warehouses.FirstOrDefault();

            if (warehouse != null)
                txtWarehouse.EditValue = warehouse.Id;

            Models.Supplier supplier = Shared.db.Suppliers.FirstOrDefault();

            if (supplier != null)
                txtSupplier.EditValue = supplier.Id;

            this.ReferenceNo();

            this.getTodaysSupplier();
            //this.todayTotalProfit();
        }

        public void getBusinessLocations()
        {
            txtBussLocation.Properties.DataSource = Shared.db.BusinessLocations.ToList();
            txtBussLocation.Properties.DisplayMember = "Name"; // Set display member
            txtBussLocation.Properties.ValueMember = "Id"; // Set value member
        }

        public void getWarehouses()
        {
            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member
        }

        public void getSuppliers()
        {
            txtSupplier.Properties.DataSource = Shared.db.Suppliers.ToList();
            txtSupplier.Properties.DisplayMember = "FirstName"; // Set display member
            txtSupplier.Properties.ValueMember = "Id"; // Set value member
        }

        public void getProducts()
        {
            txtProducts.Properties.DataSource = Shared.db.Products.ToList();
            txtProducts.Properties.DisplayMember = "ProductName"; // Set display member
            txtProducts.Properties.ValueMember = "Id"; // Set value member
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtPurchaseType.SelectedIndex == 0)
            {
                purchaseType = "Carry in";
            }
            else if (txtPurchaseType.SelectedIndex == 1)
            {
                purchaseType = "Pick up";
            }
            else
            {
                purchaseType = "On site";
            }

            Function.Sound.Selected();
        }

        public void setProductNameAtSearch(string name, int id)
        {
            this.getProducts();
            txtProducts.Text = name;
            // Manually trigger the EditValueChanged event
            //txtProducts_EditValueChanged(txtProducts, EventArgs.Empty);
            txtProducts.EditValue = id;
        }

        private void txtProducts_EditValueChanged(object sender, EventArgs e)
        {
            object value = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "ProductId");

            int productId = int.Parse(txtProducts.EditValue.ToString());

            if (type == "Add")
            {
                if (txtProducts.EditValue != null)
                {
                    int index = dt.Rows.Count == 0 ? -1 : this.getIndex(productId.ToString());
                    Models.Product product = Shared.db.Products.Find(productId);

                    if (index == -1)
                    {
                        DataRow NewRow = dt.NewRow();
                        NewRow["ProductId"] = productId;
                        NewRow["ProductName"] = txtProducts.Text;
                        NewRow["PurchaseQuantity"] = 1;
                        NewRow["UnitCostBd"] = product.PurchasePriceExcTax;
                        NewRow["DiscountPercent"] = 0;
                        NewRow["UnitCostBt"] = product.PurchasePriceExcTax;
                        NewRow["LineTotal"] = Convert.ToDecimal(NewRow["PurchaseQuantity"]) * Convert.ToDecimal(NewRow["UnitCostBt"]);
                        NewRow["ProfitMargin"] = product.Xmargin;
                        NewRow["UnitSellingPrice"] = product.SellingPrice;

                        dt.Rows.Add(NewRow);
                    }
                    else
                    {
                        int qty = int.Parse(dt.Rows[index]["PurchaseQuantity"].ToString());

                        dt.Rows[index].SetField("PurchaseQuantity", qty + 1);
                        dt.AcceptChanges();
                    }

                    gridControlProducts.DataSource = null;
                    gridControlProducts.DataSource = dt;
                }
            }
            else
            {
                object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

                Models.Product product = Shared.db.Products.Find(productId);

                Models.PurchaseDetail purchaseDetail = Shared.db.PurchaseDetails.Find(int.Parse(DetailsId.ToString()));

                if (purchaseDetail != null)
                {
                    purchaseDetail.PurchaseQuantity = purchaseDetail.PurchaseQuantity + 1;
                    purchaseDetail.LineTotal = Convert.ToDecimal(purchaseDetail.PurchaseQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - purchaseDetail.DiscountPercent / 100);

                    Shared.db.Entry(purchaseDetail).State = EntityState.Modified;

                    Shared.db.SaveChanges();

                }
                else
                {
                    purchaseDetail.ProductId = productId;
                    purchaseDetail.PurchaseId = this.purchase_id;
                    purchaseDetail.ProductName = txtProducts.Text;
                    purchaseDetail.PurchaseQuantity = 1;
                    purchaseDetail.UnitCostBd = product.PurchasePriceExcTax;
                    purchaseDetail.DiscountPercent = 0;
                    purchaseDetail.UnitCostBt = product.PurchasePriceExcTax;
                    purchaseDetail.LineTotal = Convert.ToDecimal(purchaseDetail.PurchaseQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                    purchaseDetail.ProfitMargin = product.Xmargin;
                    purchaseDetail.UnitSellingPrice = product.SellingPrice;

                    Shared.db.PurchaseDetails.Add(purchaseDetail);

                    Shared.db.SaveChanges();
                }

                this.changeNumberItems(this.purchase_id);

                this.getPurchaseItems(this.purchase_id);
            }

            txtNetTotalAmount.Text = this.calculeTotal().ToString();

            Function.Sound.Added();
        }

        private void repDeleteItem_Click(object sender, EventArgs e)
        {
            object ProductId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "ProductId");
            object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");
            int id = Convert.ToInt32(ProductId);

            if (XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = this.getIndex(ProductId.ToString());

                if (this.type == "Add")
                {
                    if (dt.Rows.Count == 1)
                    {
                        XtraMessageBox.Show("Can't remove this item");
                    }
                    else if (index != -1)
                    {
                        DataRow dr = dt.Rows[index];
                        dr.Delete();
                        dt.AcceptChanges();
                        gridControlProducts.DataSource = null;
                        gridControlProducts.DataSource = dt;
                    }
                }
                else
                {
                    Models.PurchaseDetail purchaseDetail = Shared.db.PurchaseDetails.Find(Convert.ToInt32(DetailsId));

                    if (gridViewProducts.RowCount == 1)
                    {
                        XtraMessageBox.Show("Can't remove this item");
                    }
                    else if (purchaseDetail != null)
                    {
                        Shared.db.PurchaseDetails.Remove(purchaseDetail);
                        Shared.db.SaveChanges();
                        this.getPurchaseItems(this.purchase_id);
                    }

                    this.changeNumberItems(this.purchase_id);
                }

                txtNetTotalAmount.Text = this.calculeTotal().ToString();

                Function.Sound.Deleted();
            }
        }

        public int getIndex(string value)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == value)
                    return i;
            }
            return -1;
        }

        private void gridViewProducts_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // Early exit if row index is out of range
            int rowIndex = e.RowHandle;
            if (rowIndex < 0 || rowIndex >= dt.Rows.Count)
            {
                return;
            }

            // Initialize local variables  UnitCostBt
            decimal purchaseBt = Convert.ToDecimal(dt.Rows[rowIndex]["UnitCostBt"]);
            decimal purchaseQuantity = Convert.ToDecimal(dt.Rows[rowIndex]["PurchaseQuantity"]);
            decimal discountPercent = Convert.ToDecimal(dt.Rows[rowIndex]["DiscountPercent"]);
            decimal unitSellingPrice = Convert.ToDecimal(dt.Rows[rowIndex]["UnitSellingPrice"]);

            // Update values based on the column that was edited
            if (e.Column.FieldName == "DiscountPercent")
            {
                discountPercent = Convert.ToDecimal(e.Value);
            }
            else if (e.Column.FieldName == "Quantity")
            {
                purchaseQuantity = Convert.ToDecimal(e.Value);
            }

            // Calculate the line total
            decimal lineTotal = purchaseQuantity * purchaseBt * (1 - discountPercent / 100);
            dt.Rows[rowIndex]["LineTotal"] = lineTotal;

            // If not in "Add" mode, update the database record
            if (type != "Add")
            {
                PurchaseDetail detail = Shared.db.PurchaseDetails.Find(Convert.ToInt32(dt.Rows[rowIndex]["Id"]));
                if (detail != null)
                {
                    detail.PurchaseQuantity = (int)purchaseQuantity;
                    detail.DiscountPercent = discountPercent;
                    detail.LineTotal = lineTotal;
                    detail.UnitCostBd = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "UnitCostBd"));
                    detail.UnitCostBt = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "UnitCostBt"));
                    detail.ProfitMargin = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "ProfitMargin"));
                    detail.UnitSellingPrice = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "UnitSellingPrice"));

                    Shared.db.Entry(detail).State = EntityState.Modified;
                    Shared.db.SaveChanges();
                }
            }
            else
            {
                dt.AcceptChanges();
            }

            // Update the net total amount displayed on the form
            txtNetTotalAmount.Text = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["LineTotal"])).ToString("F2");
            txtTotal.Text = txtNetTotalAmount.Text;
            Sound.Selected();
        }

        private void gridViewProducts_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            this.row_idex = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "ProductId").ToString());
        }

        public void CalculePayement()

        {
            decimal paid = decimal.Parse(txtPaidAmount.EditValue.ToString()) + decimal.Parse(txtTotalDiscount.EditValue.ToString());
            decimal total = paid - this.calculeTotal();
            total = total - decimal.Parse(txtTotalTax.EditValue.ToString());
            if (total >= 0)
            {
                txtDue.Text = "0";
                txtReturnAmount.Text = total.ToString();
                txtPaymentStatus.EditValue = "Paid";
            }
            else
            {
                if (decimal.Parse(txtPaidAmount.EditValue.ToString()) == 0)
                {
                    txtPaymentStatus.EditValue = "UnPaid";
                }
                else
                {
                    txtPaymentStatus.EditValue = "Partial";
                }
                txtReturnAmount.Text = "0";
                txtDue.Text = Math.Abs(total).ToString();
            }


        }
        private void txtPaidAmount_EditValueChanged(object sender, EventArgs e)
        {
            CalculePayement();
        }

        public void PurchaseProductAsync(int productId, int unitId, int warehouseId, int quantity, decimal price)
        {
            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    // If the product exists in the warehouse, increase the quantity
                    productInWarehouse.Qty += quantity;
                    productInWarehouse.UpdatedAt = DateTime.Now;
                }
                else
                {
                    // If the product does not exist, create a new record
                    context.ProductWarehouses.Add(new ProductWarehouse
                    {
                        ProductId = productId,
                        WarehouseId = warehouseId,
                        UnitId = unitId,
                        Qty = quantity,
                        Price = price,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        // Initialize other necessary fields...
                    });
                }

                context.SaveChanges();
            }
        }

        private Models.Purchase GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Purchases.Find(currentItemId);
            else
                return null;
        }

        private void DisplayCurrentItem()
        {
            Sound.Selected();

            if (currentItemId == 0)
            {
                // If no current selection, disable all navigation buttons.
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnStart.Enabled = false;
                btnEnd.Enabled = false;
                return;
            }

            // Retrieve the minimum and maximum ID values from the Brands dataset.
            int minId = Shared.db.Purchases.Min(b => b.Id);
            int maxId = Shared.db.Purchases.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Purchase currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;

                if (currentItem.PurchaseType == "Carry in")
                {
                    txtPurchaseType.SelectedIndex = 0;
                }
                else if (currentItem.PurchaseType == "Pick up")
                {
                    txtPurchaseType.SelectedIndex = 1;
                }
                else
                {
                    txtPurchaseType.SelectedIndex = 2;
                }

                txtPurchaseDate.EditValue = currentItem.PurchaseDate != null
                ? DateTime.Parse(currentItem.PurchaseDate.ToString())
                : (object)DBNull.Value;

                txtReferenceNo.Text = currentItem.ReferenceNo ?? string.Empty;

                txtDiscountType.EditValue = currentItem.DiscountType ?? (object)DBNull.Value;
                txtPurchaseType.EditValue = currentItem.PurchaseType ?? (object)DBNull.Value;
                txtPurchaseStatus.EditValue = currentItem.PurchaseStatus ?? (object)DBNull.Value;
                txtPaymentStatus.EditValue = currentItem.PaymentSatus ?? (object)DBNull.Value;

                txtDiscountAmount.EditValue = currentItem.DiscountAmount != null
                    ? decimal.Parse(currentItem.DiscountAmount.ToString())
                    : (object)DBNull.Value;

                txtAdditionalNotes.Text = currentItem.AdditionalNotes ?? string.Empty;
                txtShippingDetails.Text = currentItem.ShippingDetails ?? string.Empty;

                txtAdditionalShippingCharges.EditValue = currentItem.AdditionalShippingCharges != null
                    ? decimal.Parse(currentItem.AdditionalShippingCharges.ToString())
                    : (object)DBNull.Value;

                txtPaidAmount.EditValue = currentItem.PaidAmount != null
                    ? decimal.Parse(currentItem.PaidAmount.ToString())
                    : (object)DBNull.Value;

                txtTotalTax.EditValue = currentItem.TotalTax != null
                    ? decimal.Parse(currentItem.TotalTax.ToString())
                    : (object)DBNull.Value;

                txtTotalDiscount.EditValue = currentItem.TotalDiscount != null
                    ? decimal.Parse(currentItem.TotalDiscount.ToString())
                    : (object)DBNull.Value;

                txtDue.EditValue = currentItem.Due != null
                    ? decimal.Parse(currentItem.Due.ToString())
                    : (object)DBNull.Value;

                txtReturnAmount.EditValue = currentItem.ReturnAmount != null
                    ? decimal.Parse(currentItem.ReturnAmount.ToString())
                    : (object)DBNull.Value;

                txtNetTotalAmount.Text = currentItem.NetTotalAmount?.ToString() ?? string.Empty;

                txtBussLocation.EditValue = currentItem.BusinessLocationId != null
                    ? Int32.Parse(currentItem.BusinessLocationId.ToString())
                    : (object)DBNull.Value;

                txtSupplier.EditValue = currentItem.SupplierId != null
                    ? Int32.Parse(currentItem.SupplierId.ToString())
                    : (object)DBNull.Value;

                txtWarehouse.EditValue = currentItem.WarehouseId != null
                    ? Int32.Parse(currentItem.WarehouseId.ToString())
                    : (object)DBNull.Value;

                txtSuppFirstName.Text = currentItem.Supplier.FirstName;
                txtSuppLastName.Text = currentItem.Supplier.LastName;
                totalDue.Text = Function.Helper.FormatAmount(currentItem.Supplier.Purchases.Sum(d => d.Due).ToString());

                gridControlPurchases.DataSource = currentItem.Supplier.Purchases;

                this.getPurchaseItems(this.currentItemId);

                txtNetTotalAmount.Text = this.calculeTotal().ToString();
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Purchases.Min(b => (int?)b.Id);
                if (minId.HasValue)
                {
                    currentItemId = minId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the first item: " + ex.Message);
            }
        }

        private void MoveToLast()
        {
            try
            {
                int? maxId = Shared.db.Purchases.Max(b => (int?)b.Id);
                if (maxId.HasValue)
                {
                    currentItemId = maxId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the last item: " + ex.Message);
            }
        }

        private void MoveToNext()
        {
            if (currentItemId == 0)
            {
                this.MoveToFirst();
            }

            var nextItem = Shared.db.Purchases.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
            if (nextItem != null)
            {
                currentItemId = nextItem.Id;
                DisplayCurrentItem();
            }
        }

        private void MoveToPrevious()
        {
            if (currentItemId != 0)
            {
                var prevItem = Shared.db.Purchases.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                if (prevItem != null)
                {
                    currentItemId = prevItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnMtDataReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";

            //if (purchase.PurchaseType == "Carry in")
            //{
            //    txtPurchaseType.SelectedIndex = 0;
            //}
            //else if (purchase.PurchaseType == "Pick up")
            //{
            //    txtPurchaseType.SelectedIndex = 1;
            //}
            //else
            //{
            //    txtPurchaseType.SelectedIndex = 2;
            //}

            //txtPurchaseDate.EditValue = DateTime.Parse(purchase.PurchaseDate.ToString());
            //txtReferenceNo.Text = purchase.ReferenceNo;
            //txtDiscountType.EditValue = purchase.DiscountType;
            //txtPurchaseType.EditValue = purchase.PurchaseType;
            //txtPurchaseStatus.EditValue = purchase.PurchaseStatus;
            //txtPaymentStatus.EditValue = purchase.PaymentSatus;
            //txtDiscountAmount.EditValue = decimal.Parse(purchase.DiscountAmount.ToString());
            //txtAdditionalNotes.Text = purchase.AdditionalNotes;
            //txtShippingDetails.Text = purchase.ShippingDetails;
            //txtAdditionalShippingCharges.EditValue = decimal.Parse(purchase.AdditionalShippingCharges.ToString());
            //txtPaidAmount.EditValue = decimal.Parse(purchase.PaidAmount.ToString());
            //txtTotalTax.EditValue = decimal.Parse(purchase.TotalTax.ToString());
            //txtTotalDiscount.EditValue = decimal.Parse(purchase.TotalDiscount.ToString());
            //txtDue.EditValue = decimal.Parse(purchase.Due.ToString());
            //txtReturnAmount.EditValue = decimal.Parse(purchase.ReturnAmount.ToString());
            //txtNetTotalAmount.Text = purchase.NetTotalAmount.ToString();
            //txtBussLocation.EditValue = Int32.Parse(purchase.BusinessLocationId.ToString());
            //txtSupplier.EditValue = Int32.Parse(purchase.SupplierId.ToString());
            //txtWarehouse.EditValue = Int32.Parse(purchase.WarehouseId.ToString());

            //this.getPurchaseItems(this.purchase_id);

            //txtNetTotalAmount.Text = this.calculeTotal().ToString();

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMtPrint_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalDiscount_EditValueChanged(object sender, EventArgs e)
        {
            CalculePayement();
        }

        private void txtTotalTax_EditValueChanged(object sender, EventArgs e)
        {
            CalculePayement();
        }

        private void txtSupplier_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddSupplier")
            {
                // Show the message

                AddEditSupplier supplier = new AddEditSupplier(this);
                supplier.ShowDialog();
            }
        }

        public void setSupplier(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtSupplier.Properties.DataSource = AppDb.Suppliers.ToList();
                    txtSupplier.EditValue = id;
                }
            }
            catch { }

        }

        private void txtBussLocation_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddBusinessLocation")
            {
                // Show the message

                AddEditBusinessLocation businessLocation = new AddEditBusinessLocation(this);
                businessLocation.ShowDialog();
            }
        }
        public void setBussLocation(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtBussLocation.Properties.DataSource = AppDb.BusinessLocations.ToList();
                    txtBussLocation.EditValue = id;
                }
            }
            catch { }

        }

        private void txtWarehouse_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddWarehouse")
            {
                // Show the message

                AddEditWarehouse warehouse = new AddEditWarehouse(this);
                warehouse.ShowDialog();
            }
        }

        public void setWarehouse(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtWarehouse.Properties.DataSource = AppDb.Warehouses.Where(x => x.Status == "Active").ToList();
                    if (id != 0)
                    {
                        txtWarehouse.EditValue = id;
                    }

                }
            }
            catch { }
        }

        private void gridViewPurchases_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                // Safely retrieve and parse the "Id" value from the clicked row
                if (int.TryParse(gridViewPurchases.GetRowCellValue(gridViewPurchases.FocusedRowHandle, "Id")?.ToString(), out int itemId))
                {
                    this.currentItemId = itemId;
                    this.edit();  // Proceed with the edit operation
                }
                else
                {
                    // Handle cases where the "Id" is null or not an integer
                    MessageBox.Show("Invalid item ID. Please select a valid row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Log the error or show a message to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridViewSuppliers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                // Safely retrieve and parse the "Id" value from the clicked row
                if (int.TryParse(gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, "Id")?.ToString(), out int itemId))
                {
                    using (var context = new AppDbContext())
                    {
                        Models.Supplier supplier = context.Suppliers.Where(i => i.Id == itemId).Include(e => e.Purchases).First();

                        if (supplier != null)
                        {
                            txtSuppFirstName.Text = supplier.FirstName;
                            txtSuppLastName.Text = supplier.LastName;
                            totalDue.Text = Function.Helper.FormatAmount(supplier.Purchases.Sum(d => d.Due).ToString());
                            gridControlPurchases.DataSource = supplier.Purchases;
                        }
                    }
                }
                else
                {
                    // Handle cases where the "Id" is null or not an integer
                    MessageBox.Show("Invalid item ID. Please select a valid row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Log the error or show a message to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getTodaysSupplier()
        {
            using (var context = new AppDbContext())
            {
                var today = DateTime.Today;

                // Fetch data for customers who have placed orders today
                var currentPageData = context.Suppliers
                    .OrderByDescending(p => p.Id)
                    .GroupJoin(
                        context.Purchases
                            .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0), // Filter sales by today's date
                        supplier => supplier.Id,
                        purchase => purchase.SupplierId,
                        (supplier, purchase) => new
                        {
                            Id = supplier.Id,
                            FullName = supplier.FirstName + " " + supplier.LastName,
                            Total = purchase.Sum(s => s.PaidAmount) ?? 0,
                            CurrentDue = supplier.Purchases.Sum(d => d.Due)
                        })
                    //.Where(c => c.Total > 0) // Only include customers with orders today
                    .ToList();

                // Bind the data to the grid
                gridControlSuppliers.DataSource = currentPageData;
            }
        }

        public void todayTotalProfit()
        {
            //using (var context = new AppDbContext())
            //{
            //    var today = DateTime.Today;

            //    string total = context.Purchases
            //        .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0)
            //        .Sum(s => s.NetTotalAmount)
            //        .ToString();

            //    todaysProfit.Text = Function.Helper.FormatAmount(total);
            //}
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {

        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {

        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {

        }

        private void newProduct_Click(object sender, EventArgs e)
        {
            ShowOverlay();
            Product.AddEditProduct addEditProduct = new Product.AddEditProduct();
            addEditProduct.setObject(this);
            addEditProduct.ShowDialog();
            HideOverlay();
        }
    }
}