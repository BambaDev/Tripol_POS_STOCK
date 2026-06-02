using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Sale.Return;
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

namespace Pos.Forms.Purchase.Return
{
    public partial class AddEditReturn : DevExpress.XtraEditors.XtraForm
    {
        public Returns returns = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public int return_id = 0;
        public int row_idex = 0;
        public bool tbIsEmpty = true;
        public int currentItemId = 0;

        public AddEditReturn(bool maximized = true)
        {
            InitializeComponent();

            if (maximized)
                this.WindowState = FormWindowState.Maximized;
        }

        public void setReturnsObject(Returns returns)
        {
            this.returns = returns;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void ReferenceNo()
        {
            int lastId = Shared.db.ReturnPurchases.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("P-RET", lastId);
        }

        public void edit()
        {
            if (this.returns != null || currentItemId != 0)
            {
                this.currentItemId = this.returns != null ? this.returns.return_id : currentItemId;

                Models.ReturnPurchase _return = Shared.db.ReturnPurchases.Find(this.currentItemId);

                if (_return != null)
                {
                    this.return_id = _return.Id;

                    txtReferenceNo.Text = _return.ReferenceNo;
                    txtReturnNote.Text = _return.ReturnNote;
                    //txtNetTotalAmount.Text = _return.NetTotalAmount.ToString();
                    txtPurchase.EditValue = _return.PurchaseId;
                    txtSupplier.EditValue = _return.SupplierId;
                    txtAction.Text= _return.Action;

                    txtWarehouse.EditValue = Int32.Parse(_return.WarehouseId.ToString());
                    txtWarehouse.ReadOnly = true;
                    this.getReturnItems(this.return_id);

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getReturnItems(int id)
        {
            var products = Shared.db.ProductPurchaseReturns.Where(m => m.ReturnId == id).Include(p=>p.Product).ToList();

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

                DataRow NewRow = dt.NewRow();
                NewRow["Id"] = item.Id;
                NewRow["ProductId"] = item.ProductId;
                NewRow["ProductName"] = item.Product.ProductName;
                NewRow["PurchaseQuantity"] = item.Qty;
                NewRow["UnitCostBd"] = item.Product.PurchasePriceExcTax ;
                NewRow["DiscountPercent"] = 0;
                NewRow["UnitCostBt"] = item.Product.PurchasePriceExcTax;
                NewRow["LineTotal"] = Convert.ToDecimal(NewRow["PurchaseQuantity"]) * Convert.ToDecimal(item.Product.SellingPrice) * (1 - 0 / 100);
                NewRow["ProfitMargin"] = item.Product.Xmargin;
                NewRow["UnitSellingPrice"] = item.Product.SellingPrice;

                dt.Rows.Add(NewRow);

                gridControlProducts.DataSource = dt;
            }
        }

        public void getPurchaseItems(int id)
        {
            if (Shared.db.PurchaseDetails.Where(m => m.PurchaseId == id).Count() > 0)
                this.tbIsEmpty = false;

            gridControlProducts.DataSource = Shared.db.PurchaseDetails.Where(m => m.PurchaseId == id).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.ReturnPurchase _return;

                if (tbIsEmpty && dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }

                if (this.type == "Add")
                {
                    _return = new Models.ReturnPurchase();

                    _return.ReferenceNo = txtReferenceNo.Text;
                    _return.ReturnNote = txtReturnNote.Text;
                    _return.StaffNote = txtStaffNote.Text;
                    _return.PurchaseId = Int32.Parse(txtPurchase.EditValue.ToString());
                    _return.SupplierId = Int32.Parse(txtSupplier.EditValue.ToString());
                    _return.Item = gridViewProducts.RowCount;
                    _return.Action=txtAction.Text;
                    _return.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                    if (Properties.Settings.Default.CashRegisterId!=0)
                    {
                        _return.CashRegisterId = Properties.Settings.Default.CashRegisterId;
                    }
                    
                    _return.UserId = Properties.Settings.Default.userId;
                    _return.CreatedAt = DateTime.Now;
                    _return.UpdatedAt = DateTime.Now;

                    Shared.db.ReturnPurchases.Add(_return);

                    Shared.db.SaveChanges();

                    List<ProductPurchaseReturn> productReturn = new List<ProductPurchaseReturn>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        ProductPurchaseReturn productReturnList = new ProductPurchaseReturn();
                        productReturnList.ProductId = Convert.ToInt32(this.dt.Rows[i]["ProductId"]);
                        productReturnList.ReturnId = _return.Id;
                        productReturnList.Qty = Convert.ToInt32(this.dt.Rows[i]["PurchaseQuantity"]);
                        productReturnList.Total = Convert.ToInt32(this.dt.Rows[i]["LineTotal"]);
                        //.Status = txtAction.Text;
                        productReturnList.CreatedAt = DateTime.Now;
                        productReturnList.UpdatedAt = DateTime.Now;
                        productReturn.Add(productReturnList);

                        this.returnProduct(Convert.ToInt32(productReturnList.ProductId), Convert.ToInt32(_return.WarehouseId), Convert.ToInt32(this.dt.Rows[i]["PurchaseQuantity"]));
                    }

                    Shared.db.ProductPurchaseReturns.AddRange(productReturn);
                    Shared.db.SaveChanges();

                    _return.TotalQty = Shared.db.ProductPurchaseReturns.Where(a => a.ReturnId == _return.Id).Sum(p => p.Qty);
                    _return.GrandTotal = this.calculeTotal();

                    Shared.db.Entry(_return).State = EntityState.Modified;

                    Shared.db.SaveChanges();

                    txtReferenceNo.Text = "";
                    txtNetTotalAmount.Text = "00000000.00 DA";
                }
                else
                {
                    if (this.returns != null)
                        this.currentItemId = this.returns.return_id;

                    _return = Shared.db.ReturnPurchases.Find(this.currentItemId);

                    if (_return != null)
                    {
                        _return.ReferenceNo = txtReferenceNo.Text;
                        _return.ReturnNote = txtReturnNote.Text;
                        _return.StaffNote = txtStaffNote.Text;
                        _return.PurchaseId = Int32.Parse(txtPurchase.EditValue.ToString());
                        _return.SupplierId = Int32.Parse(txtSupplier.EditValue.ToString());
                        _return.Item = gridViewProducts.RowCount;
                        //_return.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        _return.UpdatedAt = DateTime.Now;
                        _return.Action = txtAction.Text;
                        Shared.db.Entry(_return).State = EntityState.Modified;

                        // Suppression des anciens produits retournés
                        var previousProductReturns = Shared.db.ProductPurchaseReturns.Where(p => p.ReturnId == _return.Id).ToList();
                        foreach (var productReturn in previousProductReturns)
                        {
                            this.UpdateProductWarehouse(productReturn.ProductId ?? 0, _return.WarehouseId ?? 0, -productReturn.Qty ?? 0);

                            Shared.db.ProductPurchaseReturns.Remove(productReturn);
                        }
                        Shared.db.SaveChanges();

                        // Ajout et modification des nouveaux produits retournés
                        List<ProductPurchaseReturn> updatedProductReturns = new List<ProductPurchaseReturn>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ProductPurchaseReturn productReturnList = new ProductPurchaseReturn();
                            productReturnList.ProductId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                            productReturnList.ReturnId = _return.Id;
                            productReturnList.Qty = Convert.ToInt32(dt.Rows[i]["PurchaseQuantity"]);
                            productReturnList.Total = Convert.ToInt32(dt.Rows[i]["LineTotal"]);
                            productReturnList.CreatedAt = DateTime.Now;
                            productReturnList.UpdatedAt = DateTime.Now;
                            updatedProductReturns.Add(productReturnList);

                            // Mise à jour du stock dans ProductWarehouse
                            this.UpdateProductWarehouse(productReturnList.ProductId ?? 0, _return.WarehouseId ?? 0, productReturnList.Qty ?? 0);
                        }

                        Shared.db.ProductPurchaseReturns.AddRange(updatedProductReturns);
                        Shared.db.SaveChanges();

                        // Mise à jour des totaux
                        _return.TotalQty = Shared.db.ProductPurchaseReturns.Where(a => a.ReturnId == _return.Id).Sum(p => p.Qty);
                        _return.GrandTotal = this.calculeTotal();
                        Shared.db.Entry(_return).State = EntityState.Modified;
                        Shared.db.SaveChanges();
                        //_return.ReferenceNo = txtReferenceNo.Text;
                        //_return.ReturnNote = txtReturnNote.Text;
                        //_return.StaffNote = txtStaffNote.Text;
                        //_return.PurchaseId = Int32.Parse(txtPurchase.EditValue.ToString());
                        //_return.SupplierId = Int32.Parse(txtSupplier.EditValue.ToString());
                        //_return.Item = gridViewProducts.RowCount;
                        //_return.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        //_return.UpdatedAt = DateTime.Now;
                        //_return.Action = txtAction.Text;
                        //Shared.db.Entry(_return).State = EntityState.Modified;

                        //Shared.db.SaveChanges();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Function.Sound.Added();

                if (this.returns != null)
                    this.returns.loadReturns();
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.ReferenceNo();

            SplashScreenManager.CloseForm();
        }
      

        // Méthode pour mettre à jour les quantités dans ProductWarehouse
        private void UpdateProductWarehouse(int productId, int warehouseId, int qtyChange)
        {
            var productWarehouse = Shared.db.ProductWarehouses
                .FirstOrDefault(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId);

            if (productWarehouse != null)
            {
                productWarehouse.Qty -= qtyChange;

                // Empêcher une quantité négative
                if (productWarehouse.Qty < 0)
                {
                    productWarehouse.Qty = 0;
                }

                Shared.db.Entry(productWarehouse).State = EntityState.Modified;
                Shared.db.SaveChanges();
            }
        }

        public void changeNumberItems(int return_id)
        {
            Models.ReturnPurchase _return = Shared.db.ReturnPurchases.Find(return_id);

            if (_return != null)
            {
                if (type == "Add")
                {
                    _return.Item = dt.Rows.Count;
                }
                else
                {
                    _return.Item = gridViewProducts.RowCount;
                    //adjustment.NetTotalAmount = this.calculeTotal();
                }

                _return.UpdatedAt = DateTime.Now;

                Shared.db.Entry(_return).State = EntityState.Modified;

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

        private void AddEditReturn_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
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

            txtPurchase.Properties.DataSource = Shared.db.Purchases.ToList();
            txtPurchase.Properties.DisplayMember = "ReferenceNo"; // Set display member
            txtPurchase.Properties.ValueMember = "Id"; // Set value member

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtSupplier.Properties.DataSource = Shared.db.Suppliers.ToList();
            txtSupplier.Properties.DisplayMember = "FirstName"; // Set display member
            txtSupplier.Properties.ValueMember = "Id"; // Set value member

            txtProducts.Properties.DataSource = Shared.db.Products.ToList();
            txtProducts.Properties.DisplayMember = "ProductName"; // Set display member
            txtProducts.Properties.ValueMember = "Id"; // Set value member

            Models.Warehouse warehouse = Shared.db.Warehouses.FirstOrDefault();

            if (warehouse != null)
                txtWarehouse.EditValue = warehouse.Id;

            Models.Supplier supplier = Shared.db.Suppliers.FirstOrDefault();

            if (supplier != null)
                txtSupplier.EditValue = supplier.Id;

            this.ReferenceNo();
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
                    purchaseDetail.PurchaseId = this.return_id;
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

                this.changeNumberItems(this.return_id);

                this.getReturnItems(this.return_id);
            }

            txtNetTotalAmount.Text = this.calculeTotal().ToString();

            Function.Sound.Added();
        }

        private void repDeleteItem_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridViewProducts.FocusedRowHandle;
            object productId = gridViewProducts.GetRowCellValue(focusedRowHandle, "ProductId");
            object detailsId = gridViewProducts.GetRowCellValue(focusedRowHandle, "Id");

            if (XtraMessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.type == "Add")
                {
                    DeleteItemFromDataTable(focusedRowHandle);
                }
                else
                {
                    DeleteItemFromDatabase(detailsId, focusedRowHandle);
                }

                Function.Sound.Deleted();
            }
        }

        private void DeleteItemFromDataTable(int rowIndex)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(rowIndex);
                dt.AcceptChanges();
                gridViewProducts.RefreshData();
            }
        }

        private void DeleteItemFromDatabase(object detailsId, int rowIndex)
        {
            try
            {
                Models.PurchaseDetail purchaseDetail = Shared.db.PurchaseDetails.Find(Convert.ToInt32(detailsId));
                if (purchaseDetail != null)
                {
                    Shared.db.PurchaseDetails.Remove(purchaseDetail);
                    Shared.db.SaveChanges();
                    gridViewProducts.DeleteRow(rowIndex);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error while deleting item: " + ex.Message);
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

            //if (type == "Add")
            //{
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
            //}
            //else
            //{
                
            //    purchaseQuantity = decimal.Parse(gridViewProducts.GetRowCellValue(row_idex, "PurchaseQuantity").ToString());
            //    discountPercent = decimal.Parse(gridViewProducts.GetRowCellValue(row_idex, "DiscountPercent").ToString());

            //    if (e.Column.ToString() == "Discount Percent")
            //    {
            //        discountPercent = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
            //    }

            //    if (e.Column.ToString() == "Quantity")
            //    {
            //        purchaseQuantity = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
            //    }

            //    int purchaseDetailId = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "Id").ToString());

            //    ProductPurchaseReturn purchaseDetail = Shared.db.ProductPurchaseReturns.Find(purchaseDetailId);

            //    if (purchaseDetail != null)
            //    {
            //        decimal lineTotal = purchaseQuantity * Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitSellingPrice")) * (1 - discountPercent / 100);
            //        dt.Rows[row_idex].SetField("LineTotal", lineTotal);
            //        purchaseDetail.PurchaseQuantity = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "PurchaseQuantity").ToString());
            //        purchaseDetail.UnitCostBd = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitCostBd"));
            //        purchaseDetail.DiscountPercent = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "DiscountPercent"));
            //        purchaseDetail.UnitCostBt = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitCostBt"));
            //        purchaseDetail.LineTotal = lineTotal;
            //        purchaseDetail.ProfitMargin = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "ProfitMargin"));
            //        purchaseDetail.UnitSellingPrice = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "UnitSellingPrice"));

            //        //Shared.db.Entry(purchaseDetail).State = EntityState.Modified;

            //        //Shared.db.SaveChanges();
            //    }
            //}

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

        public void returnProduct(int productId, int warehouseId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    productInWarehouse.Qty -= quantity;
                    productInWarehouse.UpdatedAt = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        private void txtPurchase_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPurchase.EditValue != null)
            {
                this.getPurchaseItems(Convert.ToInt32(txtPurchase.EditValue.ToString()));
            }
        }

        private Models.ReturnPurchase GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.ReturnPurchases.Find(currentItemId);
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
            int minId = Shared.db.ReturnPurchases.Min(b => b.Id);
            int maxId = Shared.db.ReturnPurchases.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.ReturnPurchase currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;

                txtReferenceNo.Text = currentItem.ReferenceNo;
                txtReturnNote.Text = currentItem.ReturnNote;
                //txtNetTotalAmount.Text = currentItem.NetTotalAmount.ToString();
                txtWarehouse.EditValue = Int32.Parse(currentItem.WarehouseId.ToString());

                this.getReturnItems(this.currentItemId);

                txtNetTotalAmount.Text = this.calculeTotal().ToString();
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.ReturnPurchases.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.ReturnPurchases.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.ReturnPurchases.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.ReturnPurchases.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtReturnNote.Text = string.Empty;
            this.ReferenceNo();

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
    }
}