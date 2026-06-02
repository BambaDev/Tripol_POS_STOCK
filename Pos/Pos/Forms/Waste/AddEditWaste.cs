using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Employee;
using Pos.Forms.Employee.Attendance;
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
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Columns;

namespace Pos.Forms.Waste
{
    public partial class AddEditWaste : DevExpress.XtraEditors.XtraForm
    {
        public Wastes wastes = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public int waste_id = 0;
        public int row_idex = 0;
        public bool tbIsEmpty = true;
        public int currentItemId = 0;

        public AddEditWaste(bool maximized = true)
        {
            InitializeComponent();

            //if (maximized)
            //    this.WindowState = FormWindowState.Maximized;

            this.toRtl();
            this.BorderStyle();
        }

        public void BorderStyle()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang != "ar")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            }
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                this.ApplyCustomFont();

                // Set the form to use RTL
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            lbl.AppearanceItemCaption.Font = customFont;
            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup10.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlItem14.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem15.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem14.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem12.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem30.AppearanceItemCaption.Font = customFont10;
            layoutControlItem35.AppearanceItemCaption.Font = customFont10;

            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            foreach (GridColumn column in gridViewProducts.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
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

        public void setWastesObject(Wastes wastes)
        {
            this.wastes = wastes;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void ReferenceNo()
        {
            using (var context = new AppDbContext())
            {
                int lastId = context.Wastes.Count() + 1;
                txtReferenceNo.Text = Function.Helper.generateRefNo("WS", lastId);
            }
        }

        public void edit()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item !";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'article !";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
            }

            if (this.wastes != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.wastes.waste_id != 0)
                        this.currentItemId = this.wastes.waste_id;

                    this.wastes.waste_id = 0;

                    Models.Waste waste = context.Wastes.Find(this.currentItemId);

                    if (waste != null)
                    {
                        this.waste_id = waste.Id;

                        txtReferenceNo.Text = waste.ReferenceNo;
                        txtNote.Text = waste.Note;
                        txtDate.EditValue = waste.Date;
                        txtEmployee.EditValue = Int32.Parse(waste.EmployeeId.ToString());
                        txtBusinessLocation.EditValue = Int32.Parse(waste.BusinessLocationId.ToString());

                        this.getWasteItems(this.waste_id);

                        txtNetTotalAmount.Text = Function.Helper.FormatAmount(waste.TotalLoss.ToString());
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        public void getWasteItems(int id)
        {
            using (var context = new AppDbContext())
            {
                if (context.WasteItems.Where(m => m.WasteId == id).Count() > 0)
                    this.tbIsEmpty = false;

                gridControlProducts.DataSource = context.WasteItems.Where(m => m.WasteId == id).ToList();
            }
        }

        public void calculeTotalLoss()
        {
            using (var context = new AppDbContext())
            {
                Models.Waste waste = context.Wastes.Find(this.currentItemId);

                if (waste != null)
                {
                    waste.TotalLoss = calculeTotal();
                    waste.UpdatedAt = DateTime.Now;

                    context.Entry(waste).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Waste waste;

                    if (tbIsEmpty && dt.Rows.Count == 0)
                    {
                        Sound.Wrong();
                        XtraMessageBox.Show("Please add at least one items.");
                        return;
                    }

                    if (this.type == "Add")
                    {
                        using (var contextWaste = new AppDbContext())
                        {
                            waste = new Models.Waste();

                            waste.ReferenceNo = txtReferenceNo.Text;
                            waste.Note = txtNote.Text;
                            waste.TotalLoss = this.calculeTotal();
                            waste.Items = gridViewProducts.RowCount;

                            if (txtDate.EditValue is DateTime DateValue)
                            {
                                waste.Date = DateOnly.FromDateTime(DateValue);
                            }

                            waste.EmployeeId = Int32.Parse(txtEmployee.EditValue.ToString());
                            waste.BusinessLocationId = Int32.Parse(txtBusinessLocation.EditValue.ToString());
                            waste.UserId = Properties.Settings.Default.userId;
                            waste.CreatedAt = DateTime.Now;
                            waste.UpdatedAt = DateTime.Now;

                            contextWaste.Wastes.Add(waste);

                            contextWaste.SaveChanges();

                            using (var contextWasteItems = new AppDbContext())
                            {
                                List<WasteItem> wasteItems = new List<WasteItem>();

                                for (int i = 0; dt.Rows.Count > i; i++)
                                {
                                    WasteItem wasteItemList = new WasteItem();

                                    wasteItemList.ItemName = dt.Rows[i]["ItemName"].ToString();
                                    wasteItemList.WasteAmount = Decimal.Parse(this.dt.Rows[i]["WasteAmount"].ToString());
                                    wasteItemList.LastPurchasePrice = Decimal.Parse(this.dt.Rows[i]["LastPurchasePrice"].ToString());
                                    wasteItemList.LossAmount = Decimal.Parse(this.dt.Rows[i]["LossAmount"].ToString());
                                    wasteItemList.Qty = Convert.ToInt32(this.dt.Rows[i]["Qty"]);
                                    wasteItemList.WasteId = waste.Id;
                                    wasteItemList.CreatedAt = DateTime.Now;
                                    wasteItemList.UpdatedAt = DateTime.Now;
                                    wasteItemList.ProductId = int.Parse(this.dt.Rows[i]["ItemId"].ToString());
                                    wasteItems.Add(wasteItemList);

                                }

                                contextWasteItems.WasteItems.AddRange(wasteItems);
                                contextWasteItems.SaveChanges();

                                txtNetTotalAmount.Text = Function.Helper.FormatAmount("00000000.00");
                            }
                        }
                    }
                    else
                    {
                        if (this.wastes != null)
                            this.currentItemId = this.wastes.waste_id;

                        waste = context.Wastes.Find(this.currentItemId);

                        if (waste != null)
                        {
                            waste.ReferenceNo = txtReferenceNo.Text;
                            waste.Note = txtNote.Text;
                            waste.TotalLoss = decimal.Parse(txtNetTotalAmount.Text);
                            waste.Items = gridViewProducts.RowCount;

                            if (txtDate.EditValue is DateTime DateValue)
                            {
                                waste.Date = DateOnly.FromDateTime(DateValue);
                            }

                            waste.EmployeeId = Int32.Parse(txtEmployee.EditValue.ToString());
                            waste.BusinessLocationId = Int32.Parse(txtBusinessLocation.EditValue.ToString());
                            waste.UpdatedAt = DateTime.Now;

                            context.Entry(waste).State = EntityState.Modified;
                            context.SaveChanges();

                            // Mettre à jour les WasteItems associés
                            var existingWasteItems = context.WasteItems.Where(wi => wi.WasteId == waste.Id).ToList();

                            // Supprimer les WasteItems qui ne sont plus dans la liste
                            var itemsToRemove = existingWasteItems.Where(wi => !dt.AsEnumerable().Any(row => Convert.ToInt32(row["ItemId"]) == wi.ProductId)).ToList();
                            foreach (var item in itemsToRemove)
                            {
                                context.WasteItems.Remove(item);
                                context.SaveChanges();
                            }

                            // Ajouter ou mettre à jour les WasteItems
                            foreach (DataRow row in dt.Rows)
                            {
                                var existingItem = existingWasteItems.FirstOrDefault(wi => wi.Id == Convert.ToInt32(row["ItemId"]));
                                if (existingItem == null)
                                {
                                    WasteItem newItem = new WasteItem()
                                    {
                                        ItemName = row["ItemName"].ToString(),
                                        WasteAmount = Decimal.Parse(row["WasteAmount"].ToString()),
                                        LastPurchasePrice = Decimal.Parse(row["LastPurchasePrice"].ToString()),
                                        LossAmount = Decimal.Parse(row["LossAmount"].ToString()),
                                        Qty = Convert.ToInt32(row["Qty"]),
                                        WasteId = waste.Id,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    };
                                    context.WasteItems.Add(newItem);
                                }
                                else
                                {
                                    // Mettre à jour l'élément existant
                                    existingItem.ItemName = row["ItemName"].ToString();
                                    existingItem.WasteAmount = Decimal.Parse(row["WasteAmount"].ToString());
                                    existingItem.LastPurchasePrice = Decimal.Parse(row["LastPurchasePrice"].ToString());
                                    existingItem.LossAmount = Decimal.Parse(row["LossAmount"].ToString());
                                    existingItem.Qty = Convert.ToInt32(row["Qty"]);
                                    existingItem.UpdatedAt = DateTime.Now;

                                    context.Entry(existingItem).State = EntityState.Modified;
                                }

                                context.SaveChanges();
                            }
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show("Please select item !");
                        }
                    }

                    Function.Sound.Added();

                    if (this.wastes != null)
                        this.wastes.loadWastes();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.ReferenceNo();

            SplashScreenManager.CloseForm();
        }

        public void changeNumberItems(int waste_id)
        {
            using (var context = new AppDbContext())
            {
                Models.Waste waste = context.Wastes.Find(waste_id);

                if (waste != null)
                {
                    if (type == "Add")
                    {
                        waste.Items = dt.Rows.Count;
                    }
                    else
                    {
                        waste.Items = gridViewProducts.RowCount;
                    }

                    waste.UpdatedAt = DateTime.Now;

                    context.Entry(waste).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
        }

        public decimal calculeTotal()
        {
            decimal total = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total += decimal.Parse(dt.Rows[i]["LossAmount"].ToString());
                }
            return total;
        }

        private void AddEditWaste_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("WasteAmount", typeof(decimal));
                dt.Columns.Add("LastPurchasePrice", typeof(decimal));
                dt.Columns.Add("LossAmount", typeof(decimal));
                dt.Columns.Add("Qty", typeof(decimal));
                dt.Columns.Add("ItemId", typeof(int));

            }
            using (AppDbContext AppDb = new AppDbContext())
            {
                txtItemName.Properties.DataSource = AppDb.Products.ToList();
            }

            this.getBusinessLocations();
            this.getEmployees();

            this.ReferenceNo();
        }

        public void getBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBusinessLocation.Properties.DataSource = context.BusinessLocations.ToList();
                txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
                txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getEmployees()
        {
            using (var context = new AppDbContext())
            {
                txtEmployee.Properties.DataSource = context.Employees.ToList();
                txtEmployee.Properties.DisplayMember = "FirstName"; // Set display member
                txtEmployee.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void repDeleteItem_Click(object sender, EventArgs e)
        {
            object ItemId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");
            int id = Convert.ToInt32(ItemId);

            if (XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = this.getIndex(id.ToString());

                //if (this.type == "Add")
                //{
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
                //}
                //else
                //{
                //    using (var context = new AppDbContext())
                //    {
                //        Models.WasteItem wasteItem = context.WasteItems.Find(id);

                //        if (gridViewProducts.RowCount == 1)
                //        {
                //            XtraMessageBox.Show("Can't remove this item");
                //        }
                //        else if (wasteItem != null)
                //        {
                //            context.WasteItems.Remove(wasteItem);
                //            context.SaveChanges();

                //            this.calculeTotalLoss();

                //            this.getWasteItems(this.currentItemId);
                //        }

                //        this.changeNumberItems(this.currentItemId);
                //    }
                //}

                txtNetTotalAmount.Text = Function.Helper.FormatAmount(this.calculeTotal().ToString());

                Function.Sound.Deleted();
            }
        }

        public int getIndex(string value)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ItemName"].ToString() == value)
                    return i;
            }
            return -1;
        }

        private void gridViewProducts_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal Qty = 0;

            if (type == "Add")
            {
                Qty = Convert.ToDecimal(dt.Rows[row_idex]["Qty"]);

                if (e.Column.ToString() == "Qty")
                {
                    Qty = Convert.ToDecimal(gridViewProducts.EditingValue.ToString());
                }

                decimal LossAmount = Qty * Convert.ToDecimal(dt.Rows[row_idex]["LastPurchasePrice"]);
                dt.Rows[row_idex].SetField("LossAmount", LossAmount);
                dt.AcceptChanges();
            }
            else
            {
                using (var context = new AppDbContext())
                {
                    int wasteItemId = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "Id").ToString());

                    WasteItem wasteItem = context.WasteItems.Find(wasteItemId);

                    if (wasteItem != null)
                    {
                        wasteItem.WasteAmount = int.Parse(gridViewProducts.GetRowCellValue(row_idex, "WasteAmount").ToString());
                        wasteItem.LastPurchasePrice = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "LastPurchasePrice"));
                        wasteItem.LossAmount = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "LossAmount")) * Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "Qty"));
                        wasteItem.Qty = Convert.ToDecimal(gridViewProducts.GetRowCellValue(row_idex, "Qty"));

                        context.Entry(wasteItem).State = EntityState.Modified;

                        context.SaveChanges();
                    }
                }
            }

            txtNetTotalAmount.Text = this.calculeTotal().ToString();

            Sound.Selected();
        }

        private void gridViewProducts_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewProducts.FocusedRowHandle > 0)
            {
                this.row_idex = int.Parse(gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "Id").ToString());
            }
        }

        private Models.Waste GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Wastes.Find(currentItemId);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
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
                int minId = context.Wastes.Min(b => b.Id);
                int maxId = context.Wastes.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Waste currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;

                    txtReferenceNo.Text = currentItem.ReferenceNo;
                    txtNote.Text = currentItem.Note;
                    txtDate.EditValue = currentItem.Date;
                    txtEmployee.EditValue = Int32.Parse(currentItem.EmployeeId.ToString());
                    txtBusinessLocation.EditValue = Int32.Parse(currentItem.BusinessLocationId.ToString());

                    this.getWasteItems(currentItem.Id);

                    txtNetTotalAmount.Text = Function.Helper.FormatAmount(currentItem.TotalLoss.ToString());
                }
            }
        }

        private void MoveToFirst()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the first item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du premier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأول:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? minId = context.Wastes.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        currentItemId = minId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToLast()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the last item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du dernier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأخير:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? maxId = context.Wastes.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        currentItemId = maxId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToNext()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.Wastes.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    currentItemId = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    var prevItem = context.Wastes.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        currentItemId = prevItem.Id;
                        DisplayCurrentItem();
                    }
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

            txtNote.Text = string.Empty;
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
            Sound.Selected();
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlProducts);
            customPrint.PrintGridControl(gridViewProducts);
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == "Add")
                {
                    AddOrUpdateItemInDataTable();
                }
                else
                {
                    AddOrUpdateItemInDatabase();
                }

                //this.getWasteItems(this.currentItemId);
                UpdateNetTotalAmount();
                Function.Sound.Added();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it and show a message to the user)
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void AddOrUpdateItemInDataTable()
        {
            if (dxValidationProviderItem.Validate())
            {
                int index = dt.Rows.Count == 0 ? -1 : this.getIndex(txtItemName.Text);

                if (index == -1)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = dt.Rows.Count + 1;
                    newRow["ItemName"] = txtItemName.Text;
                    newRow["LastPurchasePrice"] = spinLastPurchasePrice.EditValue;
                    newRow["Qty"] = spinQty.EditValue;
                    newRow["WasteAmount"] = spinWasteAmount.EditValue;
                    newRow["LossAmount"] = spinLossAmount.EditValue;
                    newRow["ItemId"] = int.Parse(txtItemName.EditValue.ToString());

                    dt.Rows.Add(newRow);
                }
                else
                {
                    int qty = int.Parse(dt.Rows[index]["Qty"].ToString());
                    dt.Rows[index].SetField("Qty", qty + 1);
                    dt.AcceptChanges();
                }

                gridControlProducts.DataSource = null;
                gridControlProducts.DataSource = dt;
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void AddOrUpdateItemInDatabase()
        {
            using (var context = new AppDbContext())
            {
                var wasteItem = context.WasteItems
                .FirstOrDefault(w => w.WasteId == this.currentItemId && w.ItemName == txtItemName.Text);

                if (wasteItem != null)
                {
                    UpdateExistingWasteItem(wasteItem);
                }
                else
                {
                    AddNewWasteItem();
                }

                context.SaveChanges();
                this.changeNumberItems(this.waste_id);
                this.getWasteItems(this.waste_id);
            }
        }

        private void UpdateExistingWasteItem(Models.WasteItem wasteItem)
        {
            using (var context = new AppDbContext())
            {
                wasteItem.ItemName = txtItemName.Text;
                wasteItem.WasteAmount = decimal.Parse(spinWasteAmount.EditValue.ToString());
                wasteItem.LastPurchasePrice = decimal.Parse(spinLastPurchasePrice.EditValue.ToString());
                wasteItem.LossAmount = decimal.Parse(spinLossAmount.EditValue.ToString());
                wasteItem.Qty = decimal.Parse(spinQty.EditValue.ToString());
                wasteItem.UpdatedAt = DateTime.Now;

                context.Entry(wasteItem).State = EntityState.Modified;
            }
        }

        private void AddNewWasteItem()
        {
            using (var context = new AppDbContext())
            {
                var wasteItem = new Models.WasteItem
                {
                    ItemName = txtItemName.Text,
                    WasteAmount = decimal.Parse(spinWasteAmount.EditValue.ToString()),
                    LastPurchasePrice = decimal.Parse(spinLastPurchasePrice.EditValue.ToString()),
                    LossAmount = decimal.Parse(spinLossAmount.EditValue.ToString()),
                    Qty = decimal.Parse(spinQty.EditValue.ToString()),
                    WasteId = this.currentItemId,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                };

                context.WasteItems.Add(wasteItem);
            }
        }

        private void UpdateNetTotalAmount()
        {
            txtNetTotalAmount.Text = Function.Helper.FormatAmount(this.calculeTotal().ToString());
        }

        private void txtItemName_EditValueChanged(object sender, EventArgs e)
        {
            if (txtItemName.EditValue != null)
            {
                // Get the selected value (e.g., ProductId) from the LookupEdit
                var selectedValue = txtItemName.EditValue;

                // Assuming the LookupEdit is bound to a DataTable or a List
                var dataSource = txtItemName.Properties.DataSource as List<Models.Product>; // Or use List<YourModel>

                if (dataSource != null)
                {
                    // Find the corresponding row in the DataSource (assuming ProductId is used as the value)
                    var selectedProduct = dataSource.AsEnumerable()
                                        .FirstOrDefault(product => product.Id.ToString() == selectedValue.ToString());

                    if (selectedProduct != null)
                    {
                        // Get the PurchasePriceExcTax column value
                        var price = selectedProduct.PurchasePriceExcTax; ;
                        spinLastPurchasePrice.Value = price != null ? Convert.ToDecimal(price) : 0;
                    }
                }
            }
        }

        private void spinQty_EditValueChanged(object sender, EventArgs e)
        {
            spinLossAmount.Value =spinQty.Value * spinLastPurchasePrice.Value;
        }
    }
}