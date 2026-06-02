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

namespace Pos.Forms.CardFidelity.Transaction
{
    public partial class Transactions : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int transaction_id = 0;

        public Transactions()
        {
            InitializeComponent();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadTransactions();
        }

        public void loadTransactions()
        {
            gridControlTransactions.DataSource = Shared.db.Transactions.OrderByDescending(p => p.Id).ToList();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            // do somthing
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            // do somthing
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.transactionDelete();
        }

        private void repDeleteTransaction_Click(object sender, EventArgs e)
        {
            this.transactionDelete();
        }

        private void gridViewTransactions_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.transaction_id = int.Parse(gridViewTransactions.GetRowCellValue(gridViewTransactions.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void transactionDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.transaction_id = int.Parse(gridViewTransactions.GetRowCellValue(gridViewTransactions.FocusedRowHandle, "Id").ToString());

            Models.Transaction transaction = Shared.db.Transactions.Find(this.transaction_id);

            if (transaction != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Transactions.Remove(transaction);
                Shared.db.SaveChanges();
                this.loadTransactions();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadTransactions();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlTransactions.ShowPrintPreview();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlTransactions.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadTransactions();
        }
    }
}