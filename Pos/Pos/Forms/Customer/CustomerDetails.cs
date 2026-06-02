using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using Pos.Function;
using Pos.Models;
using Pos.Properties;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Stripe;
using System.Management;
using DevExpress.Utils.Extensions;

namespace Pos.Forms.Customer
{
    public partial class CustomerDetails : XtraForm
    {
        private int OrderId;
        private int PaymentId;

        public CustomerDetails(int id)
        {
            InitializeComponent();
            LoadClients();
            cbxClient.EditValue = id;
        }

        private void CbxClient_EditValueChanged(object sender, EventArgs e)
        {
            if (cbxClient.EditValue == null)
            {
                return;
            }
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));
                Text = "Customer Details - " + customer.FullName;
            }
            LoadData();
        }

        private static bool IsFormAlreadyOpen(Form form)
        {
            foreach (Form mdiChild in ActiveForm.MdiChildren)
            {
                if (mdiChild.GetType() == form.GetType())
                {
                    mdiChild.Close();
                }
            }
            return false;
        }

        private static void OpenMdiChildForm(Form form)
        {
            if (!IsFormAlreadyOpen(form))
            {
                form.MdiParent = ActiveForm;
                form.Show();
            }
        }

        private void LoadClients()
        {
            using (var context = new AppDbContext())
            {
                cbxClient.Properties.DataSource = context.Customers.ToList();
                cbxClient.Properties.DisplayMember = "FullName"; // Set display member
                cbxClient.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));

                LoadClientDetails(customer);
                LoadOrders(customer);
                LoadPayments(customer);
                LoadPointsTransactions(customer);
            }
        }

        private void LoadClientDetails(Models.Customer customer)
        {
            using (var context = new AppDbContext())
            {
                string City = "Not set";
                string State = "Not set";
                string Country = "Not set";

                var _City = context.Cities.Find(customer.CityId);

                if (_City != null)
                {
                    City = _City.Name;
                }

                var _State = context.States.Find(customer.StateId);

                if (_State != null)
                {
                    State = _State.Name;
                }

                var _Country = context.Countries.Find(customer.CountryId);

                if (_Country != null)
                {
                    Country = _Country.Name;
                }

                lblId.Text = customer.Id.ToString();
                lblName.Text = customer.FullName;
                lblEmail.Text = customer.Email;
                lblPhone.Text = customer.Phone;
                lblBirthDate.Text = "Not Set"; // customer.BirthDate.ToString();
                lblGender.Text = customer.Gender;
                lblAddress.Text = customer.Address;
                lblCity.Text = City;
                lblState.Text = State;
                lblCountry.Text = Country;
                lblCreatedAt.Text = customer.CreatedAt.ToString();
                lblUpdatedAt.Text = customer.UpdatedAt.ToString();

                LoadLoyaltyDetails(customer.Id);
            }
        }

        private void LoadLoyaltyDetails(int id)
        {
            using (var context = new AppDbContext())
            {
                // Eagerly load the related LoyaltyCards and their associated CardType
                Models.Customer customer = context.Customers
                    .Include(c => c.LoyaltyCards)  // Include LoyaltyCards
                    .ThenInclude(l => l.CardType)  // Include the related CardType for each LoyaltyCard
                    .FirstOrDefault(i => i.Id == id);

                LoyaltyCard card = customer?.LoyaltyCards?.FirstOrDefault();

                if (card == null)
                {
                    lcLoyaltyCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lcCreateLoyaltyCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    lcLoyaltyCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lcCreateLoyaltyCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    lblCardSKU.Text = card.Sku;
                    lblCardStatus.Text = card.IsActive == true ? "Active" : "Inactive";
                    lblCardType.Text = card.CardType?.Name;  // Ensure card.CardType is not null

                    decimal? points = card.Points;
                    decimal? pointsAmount = points * card.CardType?.PointsPerCurrency;
                    lblCardPoints.Text = points.ToString();
                    lblCardPointsAmount.Text = pointsAmount?.ToString();

                    if (card.IsActive == true)
                    {
                        btnDisableLoyaltyCard.Enabled = true;
                        btnEnableLoyaltyCard.Enabled = false;
                    }
                    else
                    {
                        btnDisableLoyaltyCard.Enabled = false;
                        btnEnableLoyaltyCard.Enabled = true;
                    }
                }
            }
        }

        private string GenerateSku()
        {
            return "SKU-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        private void BtnCreateLoyaltyCard_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));

                // Create a new LoyaltyCard entity
                LoyaltyCard loyaltyCard = new LoyaltyCard
                {
                    CustomerId = customer.Id,
                    Sku = GenerateSku(),
                    Points = 0,
                    IsActive = true,
                    CardTypeId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                context.LoyaltyCards.Add(loyaltyCard); // Adds the entity to the DbSet
                context.SaveChanges();

                LoadLoyaltyDetails(int.Parse(cbxClient.EditValue.ToString()));
            }
        }

        private void BtnEnableLoyaltyCard_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));

                LoyaltyCard card = customer.LoyaltyCards.FirstOrDefault();

                card.IsActive = true;
                context.LoyaltyCards.Update(card);
                context.SaveChanges();
                LoadLoyaltyDetails(customer.Id);
            }
        }

        private void BtnDisableLoyaltyCard_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));

                LoyaltyCard card = customer.LoyaltyCards.FirstOrDefault();
                using AppDbContext db = new();
                card.IsActive = false;
                db.LoyaltyCards.Update(card);
                db.SaveChanges();
                LoadLoyaltyDetails(customer.Id);
            }
        }

        private void LoadPointsTransactions(Models.Customer customer)
        {
            using (var context = new AppDbContext())
            {
                // Ensure CardType is included with LoyaltyCard
                var pointsTransactions = context.Sales
                    .Where(s => s.CustomerId == customer.Id)
                    .Include(s => s.PointsTransactions)  // Include PointsTransactions
                        .ThenInclude(pt => pt.LoyaltyCard)  // Include LoyaltyCard
                        .ThenInclude(lc => lc.CardType)  // Include CardType in LoyaltyCard
                    .SelectMany(s => s.PointsTransactions.Select(pt => new
                    {
                        Sku = pt.LoyaltyCard.Sku,  // LoyaltyCard SKU
                        Points = pt.LoyaltyCard.Points,  // Points from LoyaltyCard
                        Amount = pt.Amount,  // PointsTransaction amount
                        CardType = pt.LoyaltyCard.CardType.Name ?? "N/A",  // CardType name (handle null with "N/A")
                        ReferenceNo = s.ReferenceNo,  // Assuming ReferenceNo exists in Sales
                        SaleType = s.SaleType,  // Assuming SaleType exists in Sales
                        Date = pt.Date  // Date of PointsTransaction
                    }))
                    .ToList();

                // Bind the result to the grid control
                grcPoints.DataSource = pointsTransactions;
            }
        }

        private void LoadOrders(Models.Customer customer)
        {
            using (var context = new AppDbContext())
            {
                var sales = context.Sales.Include(c => c.Customer).Where(i => i.CustomerId == customer.Id).ToList();
                grcOrders.DataSource = sales;
            }
        }

        private void LoadPayments(Models.Customer customer)
        {
            using (var context = new AppDbContext())
            {
                // Query to return only the required fields from SalePayments and Sale
                var salesPayments = context.SalePayments
                    .Where(sp => sp.CustomerId == customer.Id)
                    .Include(sp => sp.Sale)  // Include the related Sale entity to access ReferenceNo and SaleType
                    .Select(sp => new
                    {
                        Amount = sp.Amount,  // SalePayment Amount
                        CreatedAt = sp.CreatedAt,  // SalePayment CreatedAt
                        ReferenceNo = sp.Sale.ReferenceNo,  // Sale ReferenceNo
                        SaleType = sp.Sale.SaleType,  // SaleType from Sale entity
                        Due = sp.Due,  // SalePayment Due
                        DueDate = sp.DueDate  // SalePayment DueDate
                    })
                    .ToList();

                // Bind the result to the grid control
                grcPayments.DataSource = salesPayments;
            }
        }

        private void GrvOrders_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grvOrders.GetRowCellValue(grvOrders.FocusedRowHandle, "Id") == null)
            {
                return;
            }
            OrderId = Convert.ToInt32(grvOrders.GetRowCellValue(grvOrders.FocusedRowHandle, "Id"));
        }

        private void GrvPayments_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (grvPayments.GetRowCellValue(grvPayments.FocusedRowHandle, "Id") == null)
            {
                return;
            }
            PaymentId = Convert.ToInt32(grvPayments.GetRowCellValue(grvPayments.FocusedRowHandle, "Id"));
        }

        private void GrvPayment_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {

        }

        private void GrvPayment_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {

        }

        private void GrvPayment_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {
            if (e.RelationIndex == 0)
            {
                e.RelationName = "Payments";
            }
        }

        private void GrvPayment_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void ClientDetailsForm_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.Find(int.Parse(cbxClient.EditValue.ToString()));

                if (e.Page == lcgOrders)
                {
                    LoadOrders(customer);
                }
                else if (e.Page == lcgPayments)
                {
                    LoadPayments(customer);
                }
                else if (e.Page == lcgPoints)
                {
                    LoadPointsTransactions(customer);
                }
            }
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {
            LoadLoyaltyDetails(int.Parse(cbxClient.EditValue.ToString()));
        }
    }
}