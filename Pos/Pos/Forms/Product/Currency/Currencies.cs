using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Overlay;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Product.Currency
{
    public partial class Currencies : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int currency_id = 0;

        public Currencies()
        {
            InitializeComponent();
        }

        private void Currencies_Load(object sender, EventArgs e)
        {
            btnCurrencyEdit.Enabled = false;
            btnCurrencyDelete.Enabled = false;
            this.loadCurrencies();
        }

        public void loadCurrencies()
        {
            gridControlCurrencies.DataSource = Shared.db.Currencies.OrderByDescending(p => p.Id).ToList();
        }

        private void btnAddCurrency_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditCurrency currency = new AddEditCurrency();
            currency.setCurrenciesObject(this);
            currency.setTypeOperation("Add");
            currency.ShowDialog();
        }

        private void btnCurrencyEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.currencyEdit();
        }

        private void btnCurrencyDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.currencyDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnCurrencyEdit.Enabled = true;
            btnCurrencyDelete.Enabled = true;

            this.currency_id = int.Parse(gridViewCurrencies.GetRowCellValue(gridViewCurrencies.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewCurrencies_DoubleClick(object sender, EventArgs e)
        {
            btnCurrencyEdit.Enabled = true;
            btnCurrencyDelete.Enabled = true;

            this.currency_id = int.Parse(gridViewCurrencies.GetRowCellValue(gridViewCurrencies.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.currencyEdit();
        }

        private void repEditCurrency_Click(object sender, EventArgs e)
        {
            btnCurrencyEdit.Enabled = true;
            btnCurrencyDelete.Enabled = true;

            this.currency_id = int.Parse(gridViewCurrencies.GetRowCellValue(gridViewCurrencies.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.currencyEdit();
        }

        private void repDeleteCurrency_Click(object sender, EventArgs e)
        {
            this.currencyDelete();
        }

        private void gridViewCurrencies_RowClick(object sender, EventArgs e)
        {
            btnCurrencyEdit.Enabled = true;
            btnCurrencyDelete.Enabled = true;

            this.currency_id = int.Parse(gridViewCurrencies.GetRowCellValue(gridViewCurrencies.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void currencyEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditCurrency currency = new AddEditCurrency();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            currency.FormClosed += (s, args) => overlay.Close();

            currency.setCurrenciesObject(this);
            currency.setTypeOperation("Edit");
            currency.Show();
            currency.TopMost = true;
        }

        public void currencyDelete()
        {
            btnCurrencyEdit.Enabled = true;
            btnCurrencyDelete.Enabled = true;

            this.currency_id = int.Parse(gridViewCurrencies.GetRowCellValue(gridViewCurrencies.FocusedRowHandle, "Id").ToString());

            Models.Currency currency = Shared.db.Currencies.Find(this.currency_id);

            if (currency != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Currencies.Remove(currency);
                Shared.db.SaveChanges();
                this.loadCurrencies();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnCurrencyRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnCurrencyEdit.Enabled = false;
            btnCurrencyDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadCurrencies();
        }

        private void btnPrintCurrencies_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlCurrencies.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditCurrency currency = new AddEditCurrency();
            //currency.setCurrenciesObject(this);
            currency.setTypeOperation("Add");
            currency.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.currencyEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.currencyDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlCurrencies.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnCurrencyEdit.Enabled = false;
            btnCurrencyDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadCurrencies();
        }
    }
}