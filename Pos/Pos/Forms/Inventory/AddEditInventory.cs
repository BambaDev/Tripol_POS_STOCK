using DevExpress.CodeParser;
using DevExpress.PivotGrid.PivotTable;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
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

namespace Pos.Forms.Inventory
{
    public partial class AddEditInventory : DevExpress.XtraEditors.XtraForm
    {
        public Inventories inventories = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public string purchaseType = "Carry in";
        public int inventory_id = 0;
        public int row_idex = 0;
        public int currentItemId = 0;
        public int gap = 0;

        public AddEditInventory(bool maximized = true)
        {
            InitializeComponent();

            if (maximized)
                this.WindowState = FormWindowState.Maximized;
        }

        public void setInventoriesObject(Inventories inventories)
        {
            this.inventories = inventories;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void ReferenceNo()
        {
            int lastId = Shared.db.Inventories.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("INV", lastId);
        }

        public void edit()
        {
            if (this.inventories != null || currentItemId != 0)
            {
                this.currentItemId = this.inventories != null ? this.inventories.inventory_id : currentItemId;

                Models.Inventory inventory = Shared.db.Inventories.Find(this.currentItemId);

                if (inventory != null)
                {
                    this.inventory_id = inventory.Id;

                    if (inventory.Status == "Pending")
                    {
                        txtStatus.SelectedIndex = 0;
                    }
                    else if (inventory.Status == "Canceled")
                    {
                        txtStatus.SelectedIndex = 1;
                    }
                    else
                    {
                        txtStatus.SelectedIndex = 2;
                    }

                    txtName.Text = inventory.Name;
                    txtReferenceNo.Text = inventory.ReferenceNo;
                    txtGap.EditValue = inventory.Gap;
                    txtNbrProducts.EditValue = inventory.NbrProducts;
                    txtStatus.EditValue = inventory.Status;
                    txtNetTotalAmount.Text = inventory.Gap.ToString();
                    txtWarehouse.EditValue = Int32.Parse(inventory.WarehouseId.ToString());

                    this.getInventoryItems(this.inventory_id);

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getInventoryItems(int id)
        {
            // Clear existing rows to prepare for new data
            dt.Rows.Clear();

            // Initialize DataTable columns only once, consider moving this to a constructor or initializer
            InitializeDataTableColumns();

            try
            {
                // Efficiently fetch the data from the database
                var inventoryItems = Shared.db.InventoryItems
                                            .Where(m => m.InventoryId == id)
                                            .Select(m => new
                                            {
                                                m.Id,
                                                m.ItemId,
                                                m.Name,
                                                m.Price,
                                                m.TheoricalStock,
                                                m.PhysicalStock,
                                                m.Gap,
                                                m.DiffAmount
                                            }).ToList();

                // Populate the DataTable with fetched data
                foreach (var item in inventoryItems)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = item.ItemId;
                    newRow["Name"] = item.Name;
                    newRow["Price"] = item.Price ?? 0;
                    newRow["TheoricalStock"] = item.TheoricalStock;
                    newRow["PhysicalStock"] = item.PhysicalStock;
                    newRow["Gap"] = item.Gap;
                    newRow["DiffAmount"] = item.DiffAmount;

                    dt.Rows.Add(newRow);
                }

                // Bind the DataTable to the grid control
                gridControlProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load inventory items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataTableColumns()
        {
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("TheoricalStock", typeof(int));
                dt.Columns.Add("PhysicalStock", typeof(int));
                dt.Columns.Add("Gap", typeof(int));
                dt.Columns.Add("DiffAmount", typeof(decimal));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);
            int totalgap = 0;
            if (dxValidationProviderPurchase.Validate())
            {
                Models.Inventory inventory;

                if (dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }

                if (this.type == "Add")
                {
                    inventory = new Models.Inventory();

                    inventory.Name = txtName.Text;
                    inventory.ReferenceNo = txtReferenceNo.Text;

                    inventory.DiffAmount = this.calculeTotal();
                    inventory.NbrProducts = dt.Rows.Count;
                    inventory.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                    inventory.UserId = Properties.Settings.Default.userId;
                    inventory.InventoryMonth = DateTime.Now.Month;
                    inventory.InventoryYear = DateTime.Now.Year;
                    inventory.CreatedAt = DateTime.Now;
                    inventory.UpdatedAt = DateTime.Now;
                    DateTime dateValue;
                    if (DateTime.TryParse(txtDate.Text, out dateValue))
                    {
                        inventory.Date = dateValue;
                    }

                    Shared.db.Inventories.Add(inventory);

                    Shared.db.SaveChanges();

                    List<InventoryItem> inventoryItems = new List<InventoryItem>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        InventoryItem inventoryItem = new InventoryItem();
                        inventoryItem.ItemId = Convert.ToInt32(this.dt.Rows[i]["Id"]);
                        inventoryItem.InventoryId = inventory.Id;
                        inventoryItem.Name = this.dt.Rows[i]["Name"].ToString();
                        inventoryItem.Price = decimal.Parse(this.dt.Rows[i]["Price"].ToString());
                        inventoryItem.TheoricalStock = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]);
                        inventoryItem.PhysicalStock = Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]);
                        inventoryItem.Gap = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]) - Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]);
                        inventoryItem.DiffAmount = Convert.ToDecimal(this.dt.Rows[i]["Gap"]) * Convert.ToDecimal(this.dt.Rows[i]["Price"]);
                        inventoryItem.CreatedAt = DateTime.Now;
                        inventoryItem.UpdatedAt = DateTime.Now;
                        totalgap += inventoryItem.Gap ?? 0;
                        inventoryItems.Add(inventoryItem);

                        //this.PurchaseProductAsync(Convert.ToInt32(inventoryItem.ItemId), Convert.ToInt32(inventory.WarehouseId), Convert.ToInt32(inventoryItem.TheoricalStock), Convert.ToInt32(PurchaseDetailList.LineTotal));
                    }
                    inventory.Gap = totalgap;
                    Shared.db.Update(inventory);
                    Shared.db.InventoryItems.AddRange(inventoryItems);
                    Shared.db.SaveChanges();

                    txtReferenceNo.Text = "";
                    txtNetTotalAmount.Text = "00000000.00 DA";
                }
                else
                {
                    if (this.inventories != null)
                        this.currentItemId = this.inventories.inventory_id;

                    inventory = Shared.db.Inventories.Find(this.currentItemId);

                    if (inventory != null)
                    {
                        inventory.Name = txtName.Text;

                        inventory.DiffAmount = this.calculeTotal();
                        inventory.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        inventory.UserId = Properties.Settings.Default.userId;
                        inventory.UpdatedAt = DateTime.Now;

                        // Obtenir les InventoryItems existants de l'inventaire en cours
                        var existingInventoryItems = Shared.db.InventoryItems.Where(ii => ii.InventoryId == inventory.Id).ToList();

                        List<int> dtItemIds = dt.AsEnumerable().Select(row => Convert.ToInt32(row["Id"])).ToList();

                        // 1. Mettre à jour ou ajouter des nouveaux InventoryItems
                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            int itemId = Convert.ToInt32(this.dt.Rows[i]["Id"]);
                            var existingItem = existingInventoryItems.FirstOrDefault(ii => ii.ItemId == itemId);

                            if (existingItem != null)
                            {
                                // Si l'InventoryItem existe déjà, on le met à jour
                                existingItem.Name = this.dt.Rows[i]["Name"].ToString();
                                existingItem.Price = decimal.Parse(this.dt.Rows[i]["Price"].ToString());
                                existingItem.TheoricalStock = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]);
                                existingItem.PhysicalStock = Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]);
                                existingItem.Gap = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]) - Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]);
                                existingItem.DiffAmount = Convert.ToDecimal(this.dt.Rows[i]["Gap"]) * Convert.ToDecimal(this.dt.Rows[i]["Price"]);
                                existingItem.UpdatedAt = DateTime.Now;
                                totalgap += existingItem.Gap ?? 0;
                                Shared.db.Entry(existingItem).State = EntityState.Modified;
                            }
                            else
                            {
                                // Si l'InventoryItem n'existe pas, on l'ajoute
                                InventoryItem newInventoryItem = new InventoryItem
                                {
                                    ItemId = itemId,
                                    InventoryId = inventory.Id,
                                    Name = this.dt.Rows[i]["Name"].ToString(),
                                    Price = decimal.Parse(this.dt.Rows[i]["Price"].ToString()),
                                    TheoricalStock = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]),
                                    PhysicalStock = Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]),
                                    Gap = Convert.ToInt32(this.dt.Rows[i]["TheoricalStock"]) - Convert.ToInt32(this.dt.Rows[i]["PhysicalStock"]),
                                    DiffAmount = Convert.ToDecimal(this.dt.Rows[i]["Gap"]) * Convert.ToDecimal(this.dt.Rows[i]["DiffAmount"]),
                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now
                                };
                                totalgap += newInventoryItem.Gap ?? 0;
                                Shared.db.InventoryItems.Add(newInventoryItem);
                            }
                        }

                        // 2. Supprimer les InventoryItems qui ne sont plus dans le DataTable
                        foreach (var existingItem in existingInventoryItems)
                        {
                            if (existingItem.ItemId.HasValue && !dtItemIds.Contains(existingItem.ItemId.Value))
                            {
                                // Supprimer l'item s'il ne figure plus dans le DataTable
                                Shared.db.InventoryItems.Remove(existingItem);
                            }
                        }
                        inventory.Gap = totalgap;
                        inventory.DiffAmount = this.calculeTotal();
                        Shared.db.Entry(inventory).State = EntityState.Modified;

                        Shared.db.SaveChanges();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Function.Sound.Added();

                if (this.inventories != null)
                    this.inventories.loadInventories();
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.gap = 0;

            this.ReferenceNo();

            SplashScreenManager.CloseForm();
        }

        public void changeNumberItems(int inventory_id)
        {
            Models.Inventory inventory = Shared.db.Inventories.Find(inventory_id);

            if (inventory != null)
            {
                if (type == "Add")
                {
                    inventory.NbrProducts = dt.Rows.Count;
                }
                else
                {
                    inventory.NbrProducts = gridViewProducts.RowCount;
                    inventory.Gap = this.gap;
                }

                inventory.UpdatedAt = DateTime.Now;
                Shared.db.Entry(inventory).State = EntityState.Modified;

                Shared.db.SaveChanges();
            }
        }

        public decimal calculeTotal()
        {
            decimal total = 0;
            decimal totalPysical = 0;
            decimal totalTheorical = 0;
            int nbrgab = 0;
            int i = 0;
            if (type == "Add")
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    total += decimal.Parse(dt.Rows[i]["DiffAmount"].ToString());
                    totalTheorical += decimal.Parse(dt.Rows[i]["TheoricalStock"].ToString()) * decimal.Parse(dt.Rows[i]["Price"].ToString());
                    totalPysical += decimal.Parse(dt.Rows[i]["PhysicalStock"].ToString()) * decimal.Parse(dt.Rows[i]["Price"].ToString()); ;
                    this.gap += int.Parse(dt.Rows[i]["Gap"].ToString());
                    nbrgab += int.Parse(dt.Rows[i]["Gap"].ToString());
                }
            }
            else
            {
                for (i = 0; i < gridViewProducts.RowCount; i++)
                {
                    nbrgab += int.Parse(dt.Rows[i]["Gap"].ToString());
                    if (decimal.TryParse(dt.Rows[i]["TheoricalStock"]?.ToString(), out decimal theoricalStock) &&
                          decimal.TryParse(dt.Rows[i]["Price"]?.ToString(), out decimal price))
                    {
                        totalTheorical += theoricalStock * price;
                    }
                    else
                    {
                        // Handle parse failure if necessary, e.g., log a warning or set a default value
                        Console.WriteLine($"Failed to parse TheoricalStock or Price at row {i}");
                    }

                    // Safely parse the "PhysicalStock" and "Price" column values
                    if (decimal.TryParse(dt.Rows[i]["PhysicalStock"]?.ToString(), out decimal physicalStock) &&
                        decimal.TryParse(dt.Rows[i]["Price"]?.ToString(), out decimal priceP))
                    {
                        totalPysical += physicalStock * priceP;
                    }
                    else
                    {
                        // Handle parse failure if necessary
                        Console.WriteLine($"Failed to parse PhysicalStock or Price at row {i}");
                    }
                    // Safely parse the "Amount" column values
                    if (decimal.TryParse(gridViewProducts.GetRowCellValue(i, "DiffAmount")?.ToString(), out decimal amount))
                    {
                        total += amount;
                    }
                    else
                    {
                        // Handle parse failure if necessary, e.g., log a warning or set a default value
                        Console.WriteLine($"Failed to parse Amount at row {i}");
                    }

                    // Safely parse the "Gap" column values
                    if (int.TryParse(gridViewProducts.GetRowCellValue(i, "Gap")?.ToString(), out int gap))
                    {
                        this.gap += gap;
                    }
                    else
                    {
                        // Handle parse failure if necessary
                        Console.WriteLine($"Failed to parse Gap at row {i}");
                    }
                }

            }

            txtNetTotalAmount.Text = total.ToString();
            txtTotalTheorecalAmount.Text = totalTheorical.ToString();
            txtTotalPhysicalAmount.Text = totalPysical.ToString();
            txtGap.Text = nbrgab.ToString();
            txtNbrProducts.Text = i.ToString();
            return total;
        }

        private void AddEditInventory_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }
            else
            {
                txtStatus.EditValue = "Pending";
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("TheoricalStock", typeof(int));
                dt.Columns.Add("PhysicalStock", typeof(int));
                dt.Columns.Add("Gap", typeof(int));
                dt.Columns.Add("DiffAmount", typeof(decimal));
            }

            txtWarehouse.Properties.DataSource = Shared.db.Warehouses.ToList();
            txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
            txtWarehouse.Properties.ValueMember = "Id"; // Set value member

            txtProducts.Properties.DataSource = Shared.db.Products.ToList();
            txtProducts.Properties.DisplayMember = "ProductName"; // Set display member
            txtProducts.Properties.ValueMember = "Id"; // Set value member

            Models.Warehouse warehouse = Shared.db.Warehouses.FirstOrDefault();

            if (warehouse != null)
                txtWarehouse.EditValue = warehouse.Id;

            this.ReferenceNo();
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtStatus.SelectedIndex == 0)
            {
                purchaseType = "Pending";
            }
            else if (txtStatus.SelectedIndex == 1)
            {
                purchaseType = "Canceled";
            }
            else
            {
                purchaseType = "Done";
            }

            Function.Sound.Selected();
        }
        bool isEmptied = false;
        private void txtProducts_EditValueChanged(object sender, EventArgs e)
        {
            if (isEmptied)
            {
                isEmptied = false; return;
            }
            object value = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

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
                        NewRow["Id"] = productId;
                        NewRow["Name"] = txtProducts.Text;
                        NewRow["Price"] = product.SellingPrice;
                        NewRow["TheoricalStock"] = this.getProductQtyByWarehouse(productId, Convert.ToInt32(txtWarehouse.EditValue.ToString()));
                        NewRow["PhysicalStock"] = 0;
                        NewRow["Gap"] = Convert.ToInt32(NewRow["TheoricalStock"]) - Convert.ToInt32(NewRow["PhysicalStock"]);
                        NewRow["DiffAmount"] = Convert.ToDecimal(NewRow["Gap"]) * Convert.ToDecimal(product.SellingPrice);

                        dt.Rows.Add(NewRow);
                    }
                    else
                    {
                        int qty = int.Parse(dt.Rows[index]["PhysicalStock"].ToString());

                        dt.Rows[index].SetField("PhysicalStock", qty + 1);
                        dt.AcceptChanges();
                    }

                    gridControlProducts.DataSource = dt;
                }
            }
            else
            {
                object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

                Models.Product product = Shared.db.Products.Find(productId);

                Models.InventoryItem inventoryItem = Shared.db.InventoryItems.Find(int.Parse(DetailsId.ToString()));

                if (inventoryItem != null)
                {
                    inventoryItem.PhysicalStock = inventoryItem.PhysicalStock + 1;

                    Shared.db.Entry(inventoryItem).State = EntityState.Modified;

                    Shared.db.SaveChanges();
                }
                else
                {
                    inventoryItem.ItemId = productId;
                    inventoryItem.Name = txtProducts.Text;
                    inventoryItem.Price = product.SellingPrice;
                    inventoryItem.TheoricalStock = this.getProductQtyByWarehouse(productId, Convert.ToInt32(txtWarehouse.EditValue.ToString()));
                    inventoryItem.PhysicalStock = 0;
                    inventoryItem.Gap = int.Parse(inventoryItem.TheoricalStock.ToString()) - int.Parse(inventoryItem.PhysicalStock.ToString());
                    inventoryItem.DiffAmount = inventoryItem.Gap * product.SellingPrice;

                    Shared.db.InventoryItems.Add(inventoryItem);

                    Shared.db.SaveChanges();
                }

                this.changeNumberItems(this.inventory_id);

                this.getInventoryItems(this.inventory_id);
            }

            //// After adding the product, set the index to the newly added row
            //if (gridViewProducts.RowCount > 0)
            //{
            //    // Set the index to the last row (which is the newly added row)
            //    this.index = gridViewProducts.RowCount - 1;
            //    this.row_index = int.Parse(gridViewProducts.GetRowCellValue(this.index, "ProductId").ToString());

            //    // Optionally, you can scroll to the newly added row and select it
            //    gridViewProducts.FocusedRowHandle = this.index;
            //    gridViewProducts.MakeRowVisible(this.index);
            //}

            // Get the correct row index for the last added row
            int rowcount = gridViewProducts.RowCount - 1;

            // Focus the last added row and "SaleQuantity" column
            gridViewProducts.FocusedRowHandle = rowcount; // Focus the correct last row
            gridViewProducts.FocusedColumn = gridViewProducts.Columns["PhysicalStock"]; // Focus the "SaleQuantity" column

            txtNetTotalAmount.Text = this.calculeTotal().ToString();

            Function.Sound.Added();
            // Clear the product selection after adding
            isEmptied = true;
            txtProducts.EditValue = null; // Clear the txtProducts

        }

        private void repDeleteItem_Click(object sender, EventArgs e)
        {
            object ProductId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

            int id = Convert.ToInt32(ProductId);

            if (XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = this.getIndex(id.ToString());

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
                    Models.InventoryItem inventoryItem = Shared.db.InventoryItems.Find(Convert.ToInt32(id));

                    if (gridViewProducts.RowCount == 1)
                    {
                        XtraMessageBox.Show("Can't remove this item");
                    }
                    else if (inventoryItem != null)
                    {
                        Shared.db.InventoryItems.Remove(inventoryItem);
                        Shared.db.SaveChanges();
                        this.getInventoryItems(this.inventory_id);
                    }

                    this.changeNumberItems(this.inventory_id);
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
            try
            {
                int rowIndex = gridViewProducts.GetDataSourceRowIndex(e.RowHandle);
                int newTheoricalStock = int.Parse(dt.Rows[rowIndex]["TheoricalStock"].ToString());
                int gap = 0;
                decimal diffAmount = 0;

                gap = int.Parse(dt.Rows[rowIndex]["TheoricalStock"].ToString()) - int.Parse(dt.Rows[rowIndex]["PhysicalStock"].ToString());
                diffAmount = gap * decimal.Parse(dt.Rows[rowIndex]["Price"].ToString());

                dt.Rows[rowIndex]["Gap"] = gap;
                dt.Rows[rowIndex]["DiffAmount"] = diffAmount;
                gridViewProducts.RefreshRow(rowIndex);

                if (type != "Add")
                {
                    InventoryItem inventoryItem = Shared.db.InventoryItems.Find(Convert.ToInt32(dt.Rows[rowIndex]["Id"]));
                    if (inventoryItem != null)
                    {
                        inventoryItem.Gap = (int)gap;
                        inventoryItem.DiffAmount = diffAmount;
                        Shared.db.Entry(inventoryItem).State = EntityState.Modified;
                    }

                    Shared.db.SaveChanges();
                }

                Invoke(new Action(() =>
                {
                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                    Sound.Selected();
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void gridViewProducts_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // Ensure the row handle points to a valid data row
            if (e.RowHandle < 0)
            {
                MessageBox.Show("Invalid row selected.");
                return;
            }

            // Safely retrieve and parse the ID value from the selected row
            object idValue = gridViewProducts.GetRowCellValue(e.RowHandle, "Id");
            if (idValue != null && int.TryParse(idValue.ToString(), out int rowId))
            {
                this.row_idex = rowId;
            }
            else
            {
                MessageBox.Show("Failed to retrieve a valid ID.");
            }
        }

        public int getProductQtyByWarehouse(int productId, int warehouseId)
        {
            using (var context = new AppDbContext())
            {
                var productInWarehouse = context.ProductWarehouses
                .FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);

                if (productInWarehouse != null)
                {
                    // If the product exists in the warehouse, increase the quantity
                    return (int)productInWarehouse.Qty;
                }

                return 0;
            }

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

        private Models.Inventory GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Inventories.Find(currentItemId);
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
            int minId = Shared.db.Inventories.Min(b => b.Id);
            int maxId = Shared.db.Inventories.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Inventory currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;

                if (currentItem.Status == "Pending")
                {
                    txtStatus.SelectedIndex = 0;
                }
                else if (currentItem.Status == "Canceled")
                {
                    txtStatus.SelectedIndex = 1;
                }
                else
                {
                    txtStatus.SelectedIndex = 2;
                }

                txtName.Text = currentItem.Name;
                txtReferenceNo.Text = currentItem.ReferenceNo;
                txtNbrProducts.EditValue = currentItem.NbrProducts;
                txtGap.EditValue = currentItem.Gap;
                txtStatus.EditValue = currentItem.Status;
                txtNetTotalAmount.Text = currentItem.Gap.ToString();
                txtWarehouse.EditValue = Int32.Parse(currentItem.WarehouseId.ToString());

                this.getInventoryItems(this.currentItemId);

                txtNetTotalAmount.Text = this.calculeTotal().ToString();
            }
        }

        private void MoveToFirst()
        {
            // Check if the Inventories table is empty
            if (Shared.db.Inventories.Any())
            {
                // Safe to use Max because we know there is at least one element
                currentItemId = Shared.db.Inventories.Min(b => b.Id);
                DisplayCurrentItem();
            }
            else
            {
                // Handle the case where there are no items
                currentItemId = -1;  // Use -1 or any other appropriate default/fallback value
                MessageBox.Show("There are no entries in the inventory.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Optionally, you could disable certain UI elements or provide other feedback
            }
        }

        private void MoveToLast()
        {
            // Check if the Inventories table is empty
            if (Shared.db.Inventories.Any())
            {
                // Safe to use Max because we know there is at least one element
                currentItemId = Shared.db.Inventories.Max(b => b.Id);
                DisplayCurrentItem();
            }
            else
            {
                // Handle the case where there are no items
                currentItemId = -1;  // Use -1 or any other appropriate default/fallback value
                MessageBox.Show("There are no entries in the inventory.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Optionally, you could disable certain UI elements or provide other feedback
            }
        }

        private void MoveToNext()
        {
            if (currentItemId == 0)
            {
                this.MoveToFirst();
            }

            var nextItem = Shared.db.Inventories.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                var prevItem = Shared.db.Inventories.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

        private void gridViewProducts_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Enter)
            {
                // Check if the focused column is "UnitSellingPrice"
                if (gridViewProducts.FocusedColumn.FieldName == "PhysicalStock")
                {
                    // Set focus to txtProducts
                    txtProducts.Focus();
                    e.Handled = true; // Prevent further processing of the Enter key
                }
            }
            // Check if the Enter key is pressed
            if (e.KeyCode == Keys.Tab)
            {
                // Check if the focused column is "UnitSellingPrice"
                if (gridViewProducts.FocusedColumn.FieldName == "PhysicalStock")
                {
                    // Set focus to txtProducts
                    txtProducts.Focus();
                    e.Handled = true; // Prevent further processing of the Enter key
                }
            }
        }
    }
}