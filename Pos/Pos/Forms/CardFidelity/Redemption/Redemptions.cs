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

namespace Pos.Forms.CardFidelity.Redemption
{
    public partial class Redemptions : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int redemption_id = 0;

        public Redemptions()
        {
            InitializeComponent();
        }

        private void Redemptions_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadRedemptions();
        }

        public void loadRedemptions()
        {
            gridControlRedemptions.DataSource = Shared.db.Redemptions.OrderByDescending(p => p.Id).ToList();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditRedemption redemption = new AddEditRedemption();
            redemption.setRedemptionsObject(this);
            redemption.setTypeOperation("Add");
            redemption.ShowDialog();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.redemptionEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.redemptionDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.redemption_id = int.Parse(gridViewRedemptions.GetRowCellValue(gridViewRedemptions.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewRedemptions_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.redemption_id = int.Parse(gridViewRedemptions.GetRowCellValue(gridViewRedemptions.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.redemptionEdit();
        }

        private void repEditRedemption_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.redemption_id = int.Parse(gridViewRedemptions.GetRowCellValue(gridViewRedemptions.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.redemptionEdit();
        }

        private void repDeleteRedemption_Click(object sender, EventArgs e)
        {
            this.redemptionDelete();
        }

        private void gridViewRedemptions_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.redemption_id = int.Parse(gridViewRedemptions.GetRowCellValue(gridViewRedemptions.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void redemptionEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditRedemption addEditRedemption = new AddEditRedemption();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            addEditRedemption.FormClosed += (s, args) => overlay.Close();

            addEditRedemption.setRedemptionsObject(this);
            addEditRedemption.setTypeOperation("Edit");
            addEditRedemption.Show();
            addEditRedemption.TopMost = true;
        }

        public void redemptionDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.redemption_id = int.Parse(gridViewRedemptions.GetRowCellValue(gridViewRedemptions.FocusedRowHandle, "Id").ToString());

            Models.Redemption redemption = Shared.db.Redemptions.Find(this.redemption_id);

            if (redemption != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Redemptions.Remove(redemption);
                Shared.db.SaveChanges();
                this.loadRedemptions();
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
            this.loadRedemptions();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlRedemptions.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditRedemption redemption = new AddEditRedemption();
            //redemption.setRedemptionsObject(this);
            redemption.setTypeOperation("Add");
            redemption.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.redemptionEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.redemptionDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlRedemptions.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadRedemptions();
        }
    }
}