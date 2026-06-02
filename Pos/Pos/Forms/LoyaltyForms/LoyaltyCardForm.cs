using DevExpress.Utils;
using DevExpress.XtraBars;
using Microsoft.EntityFrameworkCore;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;

namespace Pos.Forms.LoyaltyForms
{
    public partial class LoyaltyCardForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private int Id;
        public LoyaltyCardForm()
        {
            InitializeComponent();
        }

        private void PurchaseItemsForm_Load(object sender, EventArgs e)
        {
            txtPoints.Text = "0";
            txtSKU.Text = GenerateSku();

            if (Properties.Settings.Default.AppRibbon == true)
            {
                ribbon.Visible = true;
                layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                ribbon.Visible = false;
                layoutControlItem17.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            AttachEventHandlers();
            LoadData();
            LoadLoyaltyCards();
        }
        
        private void AttachEventHandlers()
        {
            // Attach event handlers
            dtStart.EditValueChanged += FilterCriteriaChanged;
            dtEnd.EditValueChanged += FilterCriteriaChanged;
            cbxCardType.EditValueChanged += FilterCriteriaChanged;
            // Add more handlers here for other controls like a LookUpEdit for 'City' or CheckBox for 'Status'
        }

        void ResetInputs()
        {
            cbxCustomer.Clear();
            cbxType.Clear();
            txtPoints.Text = "0";
            txtSKU.Text = GenerateSku();
            checkIsActive.Checked = false;
            SetButtons(false);
        }

        private void LoadData()
        {
            ResetInputs();

            cbxDate.Properties.DataSource = Shared.DateRanges();
            cbxCardType.Properties.DataSource = Shared.db.CardTypes.ToList();

            cbxDate.EditValue = 7;
            cbxCardType.EditValue = 0;

            Filter(dtStart.DateTime, dtEnd.DateTime, null);

            DataTable dt = new DataTable();

            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            cbxType.Properties.DataSource = Shared.db.CardTypes.ToList();

            using (var context = new AppDbContext())
            {
                cbxCustomer.Properties.DataSource = context.Customers.Include(c => c.Sales).Include(c => c.SalePayments).Include(c => c.LoyaltyCards).ToList();
                cbxCustomer.Properties.DisplayMember = "FullName"; // Set display member
                cbxCustomer.Properties.ValueMember = "Id"; // Set value member
            }
                //cbxType.EditValue = LoyaltyCardService.GetCardTypes().First().Id;

                //cbxCustomer.Properties.DataSource = Shared.db.Customers.Include(c => c.Sales).Include(c => c.SalePayments).Include(c => c.LoyaltyCards).ToList();
                //textEdit1.Properties.DataSource = Shared.db.
            
        }

        private void BtnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void GrvCategory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grvMain.GetRowCellValue(grvMain.FocusedRowHandle, "Id") == null)
            {
                return;
            }

            Id = Convert.ToInt32(grvMain.GetRowCellValue(grvMain.FocusedRowHandle, "Id"));
            SetButtons(true);

            using (var context = new AppDbContext()) // Replace with your actual DbContext
            {
                LoyaltyCard card = context.LoyaltyCards.FirstOrDefault(c => c.Id == Id);

                cbxCustomer.EditValue = card.CustomerId;
                cbxType.EditValue = card.CardTypeId;
                txtPoints.Text = card.Points.ToString();
                txtSKU.Text = card.Sku;
                checkIsActive.Checked = (bool)card.IsActive;
            }
        }

        private void SetButtons(bool v)
        {
            btnAdd.Enabled = !v;
            btnEdit.Enabled = v;
            btnDelete.Enabled = v;
            tileBarItem1.Enabled = !v;
            tileBarItem2.Enabled = v;
            tileBarItem3.Enabled = v;
        }

        private void BtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sync();
        }

        private void BtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            CreateItem();
        }

        private void BtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateItem();
        }

        private void BtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
        }

        private void CbxDate_EditValueChanged(object sender, EventArgs e)
        {
            Shared.SetRanges(dtStart, dtEnd, cbxDate);
        }

        private void DtStart_EditValueChanged(object sender, EventArgs e)
        {
            if (dtStart.DateTime > dtEnd.DateTime) dtEnd.DateTime = dtStart.DateTime.AddDays(1);
        }

        private void DtEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (dtEnd.DateTime < dtStart.DateTime) dtStart.DateTime = dtEnd.DateTime.AddDays(-1);
        }

        #region Export
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            grcMain.ShowPrintPreview();
        }

        private void BtnExportXLSX_Click(object sender, EventArgs e)
        {
            // Export sécurisé avec permission + audit
            int userId = Properties.Settings.Default.userId;
            ExportManager.SecureExportToExcel(
                grvMain,
                "LoyaltyCards",
                userId,
                "Export Loyalty Cards",
                Shared.db
            );
        }

        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            // Export sécurisé avec CSV injection protection
            int userId = Properties.Settings.Default.userId;
            ExportManager.SecureExportToCSV(
                grvMain,
                "LoyaltyCards",
                userId,
                "Export Loyalty Cards",
                Shared.db
            );
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            // Export sécurisé avec permission + audit
            int userId = Properties.Settings.Default.userId;
            ExportManager.SecureExportToPDF(
                grvMain,
                "LoyaltyCards",
                userId,
                "Export Loyalty Cards",
                Shared.db
            );
        }

        #endregion

        private void FilterCriteriaChanged(object sender, EventArgs e)
        {
            int? typeId = cbxCardType.EditValue == null || int.Parse(cbxCardType.EditValue.ToString()) == 0 ? null : int.Parse(cbxCardType.EditValue.ToString());

            DateTime? startDate = string.IsNullOrEmpty(dtStart.Text) ? null : dtStart.DateTime;
            DateTime? endDate = string.IsNullOrEmpty(dtEnd.Text) ? null : dtEnd.DateTime;

            if (startDate != null && endDate != null)
            {
                if (endDate < startDate) endDate = startDate;
                if (startDate > endDate) startDate = endDate;
            }
            else
            {
                startDate = null;
                endDate = null;
            }

            Filter(startDate, endDate, typeId);
        }

        private void Filter(DateTime? start, DateTime? end, int? typeId)
        {
            using AppDbContext db = new();

            // Initialize the base query with includes for related entities
            IQueryable<LoyaltyCard> query = db.LoyaltyCards
                .Include(o => o.Customer)
                .Include(o => o.CardType)
                .AsQueryable();

            // Apply date range filter
            if (start.HasValue && end.HasValue)
            {
                query = query.Where(oi => oi.CreatedAt >= start && oi.CreatedAt <= end);
            }

            // Apply type filter if typeId is provided
            if (typeId.HasValue)
            {
                query = query.Where(oi => oi.CardTypeId == typeId);
            }

            // Execute the query and order by CreatedAt descending
            var filteredList = query
                .OrderByDescending(o => o.CreatedAt)
                .ToList();  // Convert to a list to execute the query

            // Set the DataTable as the data source for the grid control
            grcMain.DataSource = filteredList;
        }

        private void LoadLoyaltyCards()
        {
            using AppDbContext db = new();

            grcMain.DataSource = db.LoyaltyCards
                .Include(o => o.Customer)
                .Include(o => o.CardType)
                .ToList();
        }

        private void CreateItem()
        {
            // Validate form inputs before proceeding
            if (!dxValidationProvider1.Validate())
            {
                return;
            }

            try
            {
                // Create a new LoyaltyCard entity
                LoyaltyCard loyaltyCard = new LoyaltyCard
                {
                    CustomerId = int.Parse(cbxCustomer.EditValue.ToString()), // Assuming EditValue holds the ID of the customer
                    Sku = txtSKU.Text,
                    Points = 0,  // New card starts with 0 points
                    IsActive = checkIsActive.Checked,  // Checkbox for IsActive status
                    CardTypeId = int.Parse(cbxType.EditValue.ToString()), // Assuming EditValue holds the ID of the card type
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                // Insert the new LoyaltyCard into the database
                using (var context = new AppDbContext()) // Replace 'AppDbContext' with your actual DbContext class
                {
                    context.LoyaltyCards.Add(loyaltyCard); // Adds the entity to the DbSet
                    context.SaveChanges();                 // Saves changes to the database
                }

                // Reload the LoyaltyCards to update the grid or UI
                LoadLoyaltyCards();

                // Optionally, clear form fields after successful creation
                ResetInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while creating the loyalty card: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateSku()
        {
            return "SKU-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        private void UpdateItem()
        {
            // Validate form inputs before proceeding
            if (!dxValidationProvider1.Validate())
            {
                return;
            }

            // Get the LoyaltyCard by its ID (you'll need to replace `selectedCardId` with the actual way you're tracking the card ID)
            using (var context = new AppDbContext()) // Replace with your actual DbContext
            {
                // Find the card by its ID
                LoyaltyCard card = context.LoyaltyCards.FirstOrDefault(c => c.Id == Id);

                if (card != null)
                {
                    // Update the card properties
                    card.CustomerId = (int)cbxCustomer.EditValue;
                    card.Points = decimal.Parse(txtPoints.Text);
                    card.IsActive = checkIsActive.Checked;
                    card.CardTypeId = (int)cbxType.EditValue;

                    // Save changes to the database
                    context.SaveChanges();

                    // Reload or refresh the data grid or other UI components
                    LoadLoyaltyCards();
                }
                else
                {
                    // Handle case where the card was not found
                    MessageBox.Show("Loyalty card not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteItem()
        {
            // Confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete this Loyalty Card?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var context = new AppDbContext()) // Replace with your actual DbContext
                {
                    // Find the card by ID
                    LoyaltyCard card = context.LoyaltyCards.FirstOrDefault(c => c.Id == Id);

                    if (card != null)
                    {
                        // Remove the card from the DbSet
                        context.LoyaltyCards.Remove(card);

                        // Save changes to the database
                        context.SaveChanges();

                        // Reload or refresh the UI data (e.g., grid or list)
                        LoadData();

                        MessageBox.Show("Loyalty Card deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Loyalty Card not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Sync()
        {
            Sound.Selected();
            LoadData();
        }

        private void TileBtnSync_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Sync();
        }

        private void TileBtnDel_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            DeleteItem();
        }

        private void TileBtnEdit_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            UpdateItem();
        }

        private void TileBtnAdd_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            CreateItem();
        }

        private void CbxType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "btnAdd")
            {
                LoyaltyCardTypeForm form = new();
                form.Ribbon.Visible = true;
                form.ShowDialog();
                LoadData();
            }
        }

        private void LoyaltyCardForm_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}