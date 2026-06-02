using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Product.Warehouse;
using Pos.Function;
using Pos.Models;
using Pos.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace Pos.Forms.Stock.Transfer
{
    public partial class AddEditTransfer : DevExpress.XtraEditors.XtraForm
    {
        public Transfers transfers = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public int transfer_id = 0;
        public int row_idex = 0;
        public int currentItemId = 0;

        public AddEditTransfer()
        {
            InitializeComponent();
        }

        public void setTransfersObject(Transfers transfers)
        {
            this.transfers = transfers;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void ReferenceNo()
        {
            int lastId = Shared.db.Transfers.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("TR", lastId);
        }

        public void edit()
        {
            if (this.transfers != null || currentItemId != 0)
            {
                this.currentItemId = this.transfers != null ? this.transfers.transfer_id : currentItemId;

                Models.Transfer transfer = Shared.db.Transfers.Find(this.transfers.transfer_id);

                if (transfer != null)
                {
                    this.transfer_id = transfer.Id;

                    txtReferenceNo.Text = transfer.ReferenceNo;
                    txtNotes.Text = transfer.Note;
                    //txtNetTotalAmount.Text = adjustment.NetTotalAmount.ToString();
                    txtFromWarehouse.EditValue = transfer.FromWarehouseId;
                    txtToWarehouse.EditValue = transfer.ToWarehouseId;
                    txtAction.Text = transfer.Action;
                    this.getTransferItems(this.transfer_id);

                    txtProducts.ReadOnly = true;
                    gridViewProducts.OptionsBehavior.ReadOnly = true;
                    txtToWarehouse.ReadOnly = true;
                    txtFromWarehouse.ReadOnly = true;

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getTransferItems(int id)
        {
            var products = Shared.db.ProductTransfers.Where(m => m.TransferId == id).ToList();

            dt = new DataTable();

            gridControlProducts.DataSource = null;

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

            foreach (var item in products)
            {

                Models.Product product = Shared.db.Products.Find(item.ProductId);

                DataRow NewRow = dt.NewRow();
                NewRow["ProductId"] = product.Id;
                NewRow["ProductName"] = product.ProductName;
                NewRow["PurchaseQuantity"] = 1;
                NewRow["UnitCostBd"] = product.PurchasePriceExcTax;
                NewRow["DiscountPercent"] = 0;
                NewRow["UnitCostBt"] = product.PurchasePriceExcTax;
                NewRow["LineTotal"] = Convert.ToDecimal(NewRow["PurchaseQuantity"]) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                NewRow["ProfitMargin"] = product.Xmargin;
                NewRow["UnitSellingPrice"] = product.SellingPrice;

                dt.Rows.Add(NewRow);

                gridControlProducts.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.Transfer transfer;

                if (dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }

                if (this.type == "Add")
                {
                    transfer = new Models.Transfer();

                    transfer.ReferenceNo = txtReferenceNo.Text;
                    transfer.Note = txtNotes.Text;
                    transfer.Item = gridViewProducts.RowCount;
                    transfer.FromWarehouseId = Int32.Parse(txtFromWarehouse.EditValue.ToString());
                    transfer.ToWarehouseId = Int32.Parse(txtToWarehouse.EditValue.ToString());
                    transfer.UserId = Properties.Settings.Default.userId;
                    transfer.CreatedAt = DateTime.Now;
                    transfer.UpdatedAt = DateTime.Now;
                    transfer.Action = txtAction.Text;
                    Shared.db.Transfers.Add(transfer);

                    Shared.db.SaveChanges();

                    List<ProductTransfer> productTransfer = new List<ProductTransfer>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        ProductTransfer productTransferList = new ProductTransfer();
                        productTransferList.ProductId = Convert.ToInt32(this.dt.Rows[i]["ProductId"]);
                        productTransferList.TransferId = transfer.Id;
                        productTransferList.Qty = Convert.ToInt32(this.dt.Rows[i]["PurchaseQuantity"]);
                        productTransferList.Total = Convert.ToDecimal(this.dt.Rows[i]["LineTotal"]);
                        productTransferList.CreatedAt = DateTime.Now;
                        productTransferList.UpdatedAt = DateTime.Now;
                        productTransfer.Add(productTransferList);

                        this.TransferProduct(Convert.ToInt32(productTransferList.ProductId), Convert.ToInt32(transfer.FromWarehouseId), Convert.ToInt32(transfer.ToWarehouseId), Convert.ToInt32(this.dt.Rows[i]["PurchaseQuantity"]));
                    }

                    Shared.db.ProductTransfers.AddRange(productTransfer);
                    Shared.db.SaveChanges();

                    transfer.TotalQty = Shared.db.ProductTransfers.Where(a => a.TransferId == transfer.Id).Sum(p => p.Qty);
                    transfer.GrandTotal = this.calculeTotal();

                    Shared.db.Entry(transfer).State = EntityState.Modified;

                    Shared.db.SaveChanges();

                    txtReferenceNo.Text = "";
                    txtNetTotalAmount.Text = "00000000.00 DA";
                }
                else
                {
                    if (this.transfers != null)
                        this.currentItemId = this.transfers.transfer_id;

                    transfer = Shared.db.Transfers.Find(this.currentItemId);

                    if (transfer != null)
                    {
                        transfer.ReferenceNo = txtReferenceNo.Text;
                        transfer.Note = txtNotes.Text;
                        //transfer.Item = gridViewProducts.RowCount;
                        //transfer.FromWarehouseId = Int32.Parse(txtFromWarehouse.EditValue.ToString());
                        //transfer.ToWarehouseId = Int32.Parse(txtToWarehouse.EditValue.ToString());
                        transfer.UserId = Properties.Settings.Default.userId;
                        transfer.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(transfer).State = EntityState.Modified;

                        Shared.db.SaveChanges();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Function.Sound.Added();

                if (this.transfers != null)
                    this.transfers.loadTransfers();
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.ReferenceNo();

            SplashScreenManager.CloseForm();
        }

        public void changeNumberItems(int adjustment_id)
        {
            Models.Transfer transfer = Shared.db.Transfers.Find(adjustment_id);

            if (transfer != null)
            {
                if (type == "Add")
                {
                    transfer.Item = dt.Rows.Count;
                }
                else
                {
                    transfer.Item = gridViewProducts.RowCount;
                    //adjustment.NetTotalAmount = this.calculeTotal();
                }

                transfer.UpdatedAt = DateTime.Now;

                Shared.db.Entry(transfer).State = EntityState.Modified;

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

            return total;
        }

        private void AddEditTransfer_Load(object sender, EventArgs e)
        {


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

            txtFromWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtFromWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtFromWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtToWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtToWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtToWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtProducts.Properties.DataSource = Shared.db.Products.ToList();
            txtProducts.Properties.DisplayMember = "ProductName"; // Set display member
            txtProducts.Properties.ValueMember = "Id"; // Set value member

            Models.Warehouse warehouse = Shared.db.Warehouses.FirstOrDefault();

            if (warehouse != null)
                txtFromWarehouse.EditValue = warehouse.Id;

            this.ReferenceNo();
            if (this.type != "Add")
            {
                this.edit();
            }
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
                        NewRow["LineTotal"] = Convert.ToDecimal(NewRow["PurchaseQuantity"]) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
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
                    purchaseDetail.PurchaseId = this.transfer_id;
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

                this.changeNumberItems(this.transfer_id);

                this.getTransferItems(this.transfer_id);
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
                        this.getTransferItems(this.transfer_id);
                    }

                    this.changeNumberItems(this.transfer_id);
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
            decimal purchaseQuantity = 0;
            decimal discountPercent = 0;

            if (type == "Add")
            {
                purchaseQuantity = Convert.ToDecimal(dt.Rows[row_idex]["PurchaseQuantity"]);
                discountPercent = Convert.ToDecimal(dt.Rows[row_idex]["DiscountPercent"]);

                if (e.Column.ToString() == "Discount Percent")
                {
                    discountPercent = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                if (e.Column.ToString() == "Quantity")
                {
                    purchaseQuantity = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                decimal lineTotal = purchaseQuantity * Convert.ToDecimal(dt.Rows[row_idex]["UnitSellingPrice"]) * (1 - discountPercent / 100);
                dt.Rows[row_idex].SetField("LineTotal", lineTotal);
                dt.AcceptChanges();
            }
            else
            {
                purchaseQuantity = decimal.Parse(gridViewProducts.GetRowCellValue(row_idex, "PurchaseQuantity").ToString());
                discountPercent = decimal.Parse(gridViewProducts.GetRowCellValue(row_idex, "DiscountPercent").ToString());

                if (e.Column.ToString() == "Discount Percent")
                {
                    discountPercent = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                if (e.Column.ToString() == "Quantity")
                {
                    purchaseQuantity = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                int purchaseDetailId = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "Id").ToString());

                PurchaseDetail purchaseDetail = Shared.db.PurchaseDetails.Find(purchaseDetailId);

                if (purchaseDetail != null)
                {
                    decimal lineTotal = purchaseQuantity * Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitSellingPrice")) * (1 - discountPercent / 100);

                    purchaseDetail.PurchaseQuantity = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "PurchaseQuantity").ToString());
                    purchaseDetail.UnitCostBd = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitCostBd"));
                    purchaseDetail.DiscountPercent = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "DiscountPercent"));
                    purchaseDetail.UnitCostBt = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitCostBt"));
                    purchaseDetail.LineTotal = lineTotal;
                    purchaseDetail.ProfitMargin = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "ProfitMargin"));
                    purchaseDetail.UnitSellingPrice = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitSellingPrice"));

                    Shared.db.Entry(purchaseDetail).State = EntityState.Modified;

                    Shared.db.SaveChanges();
                }
            }

            txtNetTotalAmount.Text = this.calculeTotal().ToString();

            Sound.Selected();
        }

        private void gridViewProducts_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            this.row_idex = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "ProductId").ToString());
        }

        public void PurchaseProductAsync(int productId, int warehouseId, int quantity, decimal price)
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

        public void TransferProduct(int productId, int fromWarehouseId, int toWarehouseId, decimal quantity)//, int variantId)
        {
            using (var context = new AppDbContext())
            {
                var fromWarehouse = context.ProductWarehouses
                                           .Where(pw => pw.ProductId == productId && pw.WarehouseId == fromWarehouseId) //&& pw.VariantId == variantId)
                                           .FirstOrDefault();

                if (fromWarehouse == null || fromWarehouse.Qty < quantity)
                    throw new InvalidOperationException("Insufficient stock in the source warehouse.");

                // Decrease quantity from the source warehouse
                fromWarehouse.Qty -= quantity;

                // Attempt to find a matching record in the destination warehouse
                var toWarehouse = context.ProductWarehouses
                                         .Where(pw => pw.ProductId == productId && pw.WarehouseId == toWarehouseId)// && pw.VariantId == variantId)
                                         .FirstOrDefault();

                if (toWarehouse == null)
                {
                    // Create new record if none exists
                    toWarehouse = new ProductWarehouse
                    {
                        ProductId = productId,
                        WarehouseId = toWarehouseId,
                        Qty = quantity,
                        //VariantId = variantId,
                        Price = fromWarehouse.Price, // Use the same price as the source warehouse
                        CreatedAt = DateTime.Now
                    };
                    context.ProductWarehouses.Add(toWarehouse);
                }
                else
                {
                    // Update existing record
                    toWarehouse.Qty += quantity;
                    toWarehouse.UpdatedAt = DateTime.Now;
                    // Optionally update the price if needed:
                    toWarehouse.Price = fromWarehouse.Price;
                }

                // Commit changes
                context.SaveChanges();
            }
        }

        private Models.Transfer GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Transfers.Find(currentItemId);
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
            int minId = Shared.db.Transfers.Min(b => b.Id);
            int maxId = Shared.db.Transfers.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Transfer currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;

                txtReferenceNo.Text = currentItem.ReferenceNo;
                txtNotes.Text = currentItem.Note;
                //txtNetTotalAmount.Text = currentItem.NetTotalAmount.ToString();
                txtFromWarehouse.EditValue = currentItem.FromWarehouseId;
                txtToWarehouse.EditValue = currentItem.ToWarehouseId;

                this.getTransferItems(this.currentItemId);

                txtNetTotalAmount.Text = this.calculeTotal().ToString();
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Transfers.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Transfers.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Transfers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Transfers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            this.ReferenceNo();
            txtNotes.Text = string.Empty;

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

        public void setWarehouse(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtToWarehouse.Properties.DataSource = AppDb.Warehouses.ToList();
                    txtToWarehouse.EditValue = id;
                }
            }
            catch { }
        }
        private void txtToWarehouse_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
             if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddToWarehouse")
            {
                // Show the message

                AddEditWarehouse addEditWarehouse = new AddEditWarehouse(this);
                addEditWarehouse.ShowDialog();
            }
        }
    }
}