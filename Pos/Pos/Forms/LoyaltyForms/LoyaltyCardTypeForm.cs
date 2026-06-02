using DevExpress.XtraBars;
using Pos.Function;
using Pos.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Pos.Forms.LoyaltyForms
{
    public partial class LoyaltyCardTypeForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public LoyaltyCardTypeForm()
        {
            InitializeComponent();
        }

        private void GlassTypeForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void ResetInputs()
        {
            txtName.Clear();
            txtCurrencyPerPoint.Text = "100";
            txtPointsPerCurrency.Text = "1";

            SetButtons(false);
        }
        private void LoadData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            gridControl.DataSource = Shared.db.CardTypes.ToList();

            ResetInputs();
        }

        private void BtnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void GrvCategory_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // Ensure the selected row has a valid Id
            if (gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id") == null)
            {
                return;
            }

            // Parse the Id from the selected row
            int id = int.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id").ToString());

            // Retrieve the CardType entity from the database or data source using the Id
            using (var context = new AppDbContext()) // Replace with your actual DbContext
            {
                CardType cardType = context.CardTypes.FirstOrDefault(c => c.Id == id);

                if (cardType != null)
                {
                    // Populate the form fields with the selected cardType data
                    txtName.Text = cardType.Name;
                    txtCurrencyPerPoint.Text = cardType.CurrencyPerPoint.ToString();
                    txtPointsPerCurrency.Text = cardType.PointsPerCurrency.ToString();

                    // Enable or set the buttons
                    SetButtons(true);
                }
                else
                {
                    MessageBox.Show("Card type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SetButtons(bool v)
        {
            btnAdd.Enabled = !v;
            btnEdit.Enabled = v;
            btnDelete.Enabled = v;
        }

        private void BtnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Selected();
            LoadData();
        }

        private void BtnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Validate form inputs
            if (!dxValidationProvider1.Validate())
            {
                return;
            }

            // Create a new CardType entity
            CardType newCardType = new CardType
            {
                Name = txtName.Text,
                CurrencyPerPoint = decimal.Parse(txtCurrencyPerPoint.Text),
                PointsPerCurrency = decimal.Parse(txtPointsPerCurrency.Text)
            };

            // Add the new entity to the database
            using (var context = new AppDbContext()) // Replace with your actual DbContext
            {
                context.CardTypes.Add(newCardType);
                context.SaveChanges(); // Save changes to the database
            }

            // Reload data or refresh the grid
            LoadData();

            // Optionally clear the input fields after adding the new card type
            txtName.Text = string.Empty;
            txtCurrencyPerPoint.Text = string.Empty;
            txtPointsPerCurrency.Text = string.Empty;

            MessageBox.Show("Card type added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Validate form inputs before proceeding
            if (!dxValidationProvider1.Validate())
            {
                return;
            }

            int cardTypeId = int.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id").ToString());

            // Retrieve the CardType entity from the database using the ID
            using (var context = new AppDbContext()) // Replace with your actual DbContext
            {
                CardType cardType = context.CardTypes.FirstOrDefault(c => c.Id == cardTypeId);

                if (cardType != null)
                {
                    // Update the cardType properties with the new values
                    cardType.Name = txtName.Text;
                    cardType.CurrencyPerPoint = decimal.Parse(txtCurrencyPerPoint.Text);
                    cardType.PointsPerCurrency = decimal.Parse(txtPointsPerCurrency.Text);

                    // Save changes to the database
                    context.SaveChanges();

                    // Optionally, display a success message
                    MessageBox.Show("Card type updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Card type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Reload or refresh the data
            LoadData();
        }

        private void BtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int cardTypeId = int.Parse(gridView.GetRowCellValue(gridView.FocusedRowHandle, "Id").ToString());

            // Confirm deletion with the user
            DialogResult result = MessageBox.Show("Are you sure you want to delete this Card Type?",
                                                  "Delete Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var context = new AppDbContext()) // Replace with your actual DbContext
                {
                    // Retrieve the CardType entity by Id
                    CardType cardType = context.CardTypes.FirstOrDefault(c => c.Id == cardTypeId);

                    if (cardType != null)
                    {
                        // Remove the cardType entity from the context
                        context.CardTypes.Remove(cardType);

                        // Save changes to the database
                        context.SaveChanges();

                        // Optionally, show success message
                        MessageBox.Show("Card type deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Card type not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Reload or refresh the data
                LoadData();
            }
        }

    }
}