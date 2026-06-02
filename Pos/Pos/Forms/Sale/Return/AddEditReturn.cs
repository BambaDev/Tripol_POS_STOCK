using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Purchase.Return;
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

namespace Pos.Forms.Sale.Return
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
            int lastId = Shared.db.Returns.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("S-RET", lastId);
        }

        public void edit()
        {
            try
            {
                // Check if a return has been selected or an ID is available
                if (this.returns != null )
                {
                    // Set currentItemId to the selected return's ID, if available
                    this.currentItemId = this.returns != null ? this.returns.return_id : currentItemId;

                    // Retrieve the return from the database
                    Models.Return _return = Shared.db.Returns.Find(this.returns.return_id);

                    if (_return != null)
                    {
                        // Populate the form fields with the return data
                        this.return_id = _return.Id;

                        // Assign values to the text fields safely
                        txtReferenceNo.Text = _return.ReferenceNo ?? string.Empty;
                        txtReturnNote.Text = _return.ReturnNote ?? string.Empty;
                        txtSale.EditValue = _return.SaleId;

                        // Parse the WarehouseId, ensuring it is valid
                        if (int.TryParse(_return.WarehouseId.ToString(), out int warehouseId))
                        {
                            txtWarehouse.EditValue = warehouseId;
                        }
                        else
                        {
                            XtraMessageBox.Show("Invalid Warehouse ID.");
                        }

                        // Allow editing on specific fields
                        txtWarehouse.ReadOnly = false;
                        txtSale.ReadOnly = false;
                        txtCustomer.ReadOnly = false;

                        // Fetch and display return items
                        this.getReturnItems(this.return_id);

                        // Calculate and display the net total amount
                        txtNetTotalAmount.Text = this.calculeTotal().ToString();
                    }
                    else
                    {
                        // Play error sound and show message if return is not found
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Return not found. Please select a valid item.");
                    }
                }
                else
                {
                    // Handle case when no return is selected
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select an item to edit.");
                }
            }
            catch (Exception ex)
            {
                // Log or display the error message
                XtraMessageBox.Show($"An error occurred while loading the return: {ex.Message}");
            }
        }

        public void getReturnItems(int id)
        {
            var products = Shared.db.ProductReturns.Where(m => m.ReturnId == id).ToList();

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

        public void getSaleItems(int id)
        {
            var saleDetails = Shared.db.SaleDetails.Where(m => m.SaleId == id).ToList();

            // Check if there are any items in the sale
            if (saleDetails.Count > 0)
                this.tbIsEmpty = false;

            // Set the grid data source to the list of sale details
            gridControlProducts.DataSource = saleDetails;

            // Refresh the grid view to display the data
            gridViewProducts.RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.Return _return;

                if (tbIsEmpty && dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }

                if (this.type == "Add")
                {
                    _return = new Models.Return();

                    _return.ReferenceNo = txtReferenceNo.Text;
                    _return.ReturnNote = txtReturnNote.Text;
                    _return.StaffNote = txtStaffNote.Text;
                    _return.SaleId = Int32.Parse(txtSale.EditValue.ToString());
                    _return.CustomerId = Int32.Parse(txtCustomer.EditValue.ToString());
                    _return.Item = gridViewProducts.RowCount;
                    _return.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                    //_return.CashRegisterId = Properties.Settings.Default.CashRegisterId;
                    _return.UserId = Properties.Settings.Default.userId;
                    _return.CreatedAt = DateTime.Now;
                    _return.UpdatedAt = DateTime.Now;
                    _return.Action = txtAction.Text;
                    Shared.db.Returns.Add(_return);

                    Shared.db.SaveChanges();

                    List<ProductReturn> productReturn = new List<ProductReturn>();

                    for (int i = 0; i < gridViewProducts.RowCount; i++) // Loop through gridViewProducts
                    {
                        // Ensure the row is valid and not a new row
                        if (!gridViewProducts.IsNewItemRow(i))
                        {
                            ProductReturn productReturnList = new ProductReturn();

                            // Retrieve values from the grid's rows using GetRowCellValue
                            productReturnList.ProductId = Convert.ToInt32(gridViewProducts.GetRowCellValue(i, "ProductId"));
                            productReturnList.ReturnId = _return.Id;
                            productReturnList.Qty = Convert.ToInt32(gridViewProducts.GetRowCellValue(i, "PurchaseQuantity"));
                            productReturnList.Total = Convert.ToInt32(gridViewProducts.GetRowCellValue(i, "LineTotal"));
                            // Optionally set the status or other fields if needed
                            // productReturnList.Status = txtAction.Text;
                            productReturnList.CreatedAt = DateTime.Now;
                            productReturnList.UpdatedAt = DateTime.Now;

                            // Add to the product return list
                            productReturn.Add(productReturnList);

                            // Process the return product (Inventory management or other actions)
                            this.returnProduct(
                                Convert.ToInt32(productReturnList.ProductId),
                                Convert.ToInt32(_return.WarehouseId),
                                Convert.ToInt32(gridViewProducts.GetRowCellValue(i, "PurchaseQuantity"))
                            );
                        }
                    }

                    // Save the list of product returns to the database
                    Shared.db.ProductReturns.AddRange(productReturn);
                    Shared.db.SaveChanges();

                    _return.TotalQty = Shared.db.ProductReturns.Where(a => a.ReturnId == _return.Id).Sum(p => p.Qty);
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

                    _return = Shared.db.Returns.Find(this.currentItemId);

                    if (_return != null)
                    {
                        _return.ReferenceNo = txtReferenceNo.Text;
                        _return.ReturnNote = txtReturnNote.Text;
                        _return.StaffNote = txtStaffNote.Text;
                        //_return.SaleId = Int32.Parse(txtSale.EditValue.ToString());
                        //_return.CustomerId = Int32.Parse(txtCustomer.EditValue.ToString());
                        _return.Item = gridViewProducts.RowCount;
                        //_return.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        _return.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(_return).State = EntityState.Modified;

                        Shared.db.SaveChanges();
                        // Gestion des modifications sur les produits retournés
                        List<ProductReturn> existingProductReturns = Shared.db.ProductReturns.Where(p => p.ReturnId == _return.Id).ToList();
                        foreach (DataRow row in dt.Rows)
                        {
                            int productId = Convert.ToInt32(row["ProductId"]);
                            var productReturn = existingProductReturns.FirstOrDefault(pr => pr.ProductId == productId);

                            if (productReturn != null)
                            {
                                // Mise à jour des produits existants
                                int oldQty = (int)productReturn.Qty;
                                productReturn.Qty = Convert.ToInt32(row["PurchaseQuantity"]);
                                oldQty = (int)productReturn.Qty- oldQty ;
                                productReturn.Total = Convert.ToInt32(row["LineTotal"]);
                                productReturn.UpdatedAt = DateTime.Now;
                                Shared.db.Entry(productReturn).State = EntityState.Modified;

                                // Mise à jour des stocks
                                this.returnProduct(productId, Convert.ToInt32(_return.WarehouseId), oldQty);
                            }
                            else
                            {
                                // Ajout de nouveaux produits
                                ProductReturn newProductReturn = new ProductReturn
                                {
                                    ProductId = productId,
                                    ReturnId = _return.Id,
                                    Qty = Convert.ToInt32(row["PurchaseQuantity"]),
                                    Total = Convert.ToInt32(row["LineTotal"]),
                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now
                                };
                                Shared.db.ProductReturns.Add(newProductReturn);

                                // Mise à jour des stocks
                                this.returnProduct(productId, Convert.ToInt32(_return.WarehouseId), Convert.ToInt32(row["PurchaseQuantity"]));
                            }
                        }

                        // Suppression des produits qui ne sont plus dans la liste
                        var productsToRemove = existingProductReturns.Where(pr => !dt.AsEnumerable().Any(row => Convert.ToInt32(row["ProductId"]) == pr.ProductId)).ToList();

                        if (productsToRemove.Any())
                        {
                            foreach (var product in productsToRemove)
                            {
                                // Supprimer chaque produit un par un
                                Shared.db.ProductReturns.Remove(product);

                                // Mettre à jour les stocks pour chaque produit supprimé
                                this.returnProduct((int)product.ProductId, (int)_return.WarehouseId, (int)-product.Qty);

                                // Sauvegarder les changements après chaque suppression
                                Shared.db.SaveChanges();
                            }
                        }
                        // Mise à jour des totaux
                        _return.TotalQty = Shared.db.ProductReturns.Where(a => a.ReturnId == _return.Id).Sum(p => p.Qty);
                        _return.GrandTotal = this.calculeTotal();
                        Shared.db.SaveChanges();
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

            txtSale.Clear();
            gridControlProducts.DataSource = null;

            this.ReferenceNo();

            SplashScreenManager.CloseForm();
        }

        public void changeNumberItems(int return_id)
        {
            Models.Return _return = Shared.db.Returns.Find(return_id);

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
                for (int i = 0; i < gridViewProducts.RowCount; i++)
                {
                    // Ensure the row is valid and not a new row (in case your grid allows new row addition)
                    if (!gridViewProducts.IsNewItemRow(i))
                    {
                        // Retrieve the LineTotal value from the grid's row and sum it up
                        object lineTotalValue = gridViewProducts.GetRowCellValue(i, "LineTotal");

                        if (lineTotalValue != null && decimal.TryParse(lineTotalValue.ToString(), out decimal lineTotal))
                        {
                            total += lineTotal;
                        }
                    }
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

            txtAction.Text = "Damage";

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

            txtSale.Properties.DataSource = Shared.db.Sales.ToList();
            txtSale.Properties.DisplayMember = "ReferenceNo"; // Set display member
            txtSale.Properties.ValueMember = "Id"; // Set value member

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtCustomer.Properties.DataSource = Shared.db.Customers.ToList();
            txtCustomer.Properties.DisplayMember = "FirstName"; // Set display member
            txtCustomer.Properties.ValueMember = "Id"; // Set value member

            txtProducts.Properties.DataSource = Shared.db.Products.ToList();
            txtProducts.Properties.DisplayMember = "ProductName"; // Set display member
            txtProducts.Properties.ValueMember = "Id"; // Set value member

            Models.Warehouse warehouse = Shared.db.Warehouses.FirstOrDefault();

            if (warehouse != null)
                txtWarehouse.EditValue = warehouse.Id;

            Models.Customer customer = Shared.db.Customers.FirstOrDefault();

            if (customer != null)
                txtCustomer.EditValue = customer.Id;

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
                    DeleteItemFromGrid(focusedRowHandle);
                }
                else
                {
                    DeleteItemFromDatabase(detailsId, focusedRowHandle);
                }

                Function.Sound.Deleted();
            }
        }

        private void DeleteItemFromGrid(int rowIndex)
        {
            // Get the current data source of the grid
            var dataSource = gridControlProducts.DataSource as List<Models.SaleDetail>;

            if (dataSource != null && dataSource.Count > 0 && rowIndex >= 0 && rowIndex < dataSource.Count)
            {
                // Remove the item from the list at the specified index
                dataSource.RemoveAt(rowIndex);

                // Optionally, you can remove the item from the database here if necessary
                // var itemToRemove = dataSource[rowIndex];
                // Shared.db.SaleDetails.Remove(itemToRemove);
                // Shared.db.SaveChanges();

                // Rebind the updated list to the grid
                gridControlProducts.DataSource = null; // Clear the existing data source
                gridControlProducts.DataSource = dataSource; // Reassign the updated list

                // Refresh the grid to reflect the changes
                gridViewProducts.RefreshData();
            }
            else
            {
                XtraMessageBox.Show("Invalid item selected or no data available.");
            }
        }

        private void DeleteItemFromDatabase(object detailsId, int rowIndex)
        {
            try
            {
                Models.ProductReturn productReturn = Shared.db.ProductReturns.Find(Convert.ToInt32(detailsId));
                if (productReturn != null)
                {
                    Shared.db.ProductReturns.Remove(productReturn);
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
            decimal saleQuantity = 0;
            decimal discountPercent = 0;

            // Get the focused row index
            int rowIndex = e.RowHandle;

            if (rowIndex >= 0) // Ensure a valid row index
            {
                // Retrieve the current values from the grid
                saleQuantity = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "SaleQuantity"));
                discountPercent = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "DiscountPercent"));

                // Check which column is being edited and update the corresponding value
                if (e.Column.FieldName == "DiscountPercent")
                {
                    discountPercent = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                if (e.Column.FieldName == "SaleQuantity")
                {
                    saleQuantity = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                // Retrieve the UnitSellingPrice from the grid
                decimal unitSellingPrice = Convert.ToDecimal(gridViewProducts.GetRowCellValue(rowIndex, "UnitSellingPrice"));

                // Calculate the new LineTotal based on SaleQuantity and DiscountPercent
                decimal lineTotal = saleQuantity * unitSellingPrice * (1 - discountPercent / 100);

                // Temporarily disable the event handler to prevent recursion
                gridViewProducts.CellValueChanged -= gridViewProducts_CellValueChanged;

                try
                {
                    // Update the LineTotal in the grid view
                    gridViewProducts.SetRowCellValue(rowIndex, "LineTotal", lineTotal);
                }
                finally
                {
                    // Re-enable the event handler
                    gridViewProducts.CellValueChanged += gridViewProducts_CellValueChanged;
                }

                // Update the net total amount (recalculate)
                txtNetTotalAmount.Text = this.calculeTotal().ToString();

                // Optionally, update the database if required (if you're persisting changes to a database)

                // Play the selection sound (if required)
                Sound.Selected();
            }
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
            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    productInWarehouse.Qty += quantity;
                    productInWarehouse.UpdatedAt = DateTime.Now;
                }

                context.SaveChanges();
            }
        }

        private void txtSale_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSale.EditValue != null)
            {
                this.getSaleItems(Convert.ToInt32(txtSale.EditValue.ToString()));

                txtNetTotalAmount.Text = Function.Helper.FormatAmount(this.calculeTotal().ToString());
            }
        }

        private Models.Return GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Returns.Find(currentItemId);
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
            int minId = Shared.db.Returns.Min(b => b.Id);
            int maxId = Shared.db.Returns.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Return currentItem = GetCurrentData();

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
                int? minId = Shared.db.Returns.Min(b => (int?)b.Id);
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
                int? maxId = Shared.db.Returns.Max(b => (int?)b.Id);
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

            var nextItem = Shared.db.Returns.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Returns.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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