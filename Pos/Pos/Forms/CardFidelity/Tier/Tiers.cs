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

namespace Pos.Forms.CardFidelity.Tier
{
    public partial class Tiers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int tier_id = 0;

        public Tiers()
        {
            InitializeComponent();
        }

        private void Tiers_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadTiers();
        }

        public void loadTiers()
        {
            gridControlTiers.DataSource = Shared.db.CustomerTiers.OrderByDescending(p => p.Id).ToList();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditTier tier = new AddEditTier();
            tier.setTiersObject(this);
            tier.setTypeOperation("Add");
            tier.ShowDialog();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.tierEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.tierDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.tier_id = int.Parse(gridViewTiers.GetRowCellValue(gridViewTiers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewTiers_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.tier_id = int.Parse(gridViewTiers.GetRowCellValue(gridViewTiers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.tierEdit();
        }

        private void repEditTier_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.tier_id = int.Parse(gridViewTiers.GetRowCellValue(gridViewTiers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.tierEdit();
        }

        private void repDeleteTier_Click(object sender, EventArgs e)
        {
            this.tierDelete();
        }

        private void gridViewTiers_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.tier_id = int.Parse(gridViewTiers.GetRowCellValue(gridViewTiers.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void tierEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditTier addEditTier = new AddEditTier();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            addEditTier.FormClosed += (s, args) => overlay.Close();

            addEditTier.setTiersObject(this);
            addEditTier.setTypeOperation("Edit");
            addEditTier.Show();
            addEditTier.TopMost = true;
        }

        public void tierDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.tier_id = int.Parse(gridViewTiers.GetRowCellValue(gridViewTiers.FocusedRowHandle, "Id").ToString());

            Models.CustomerTier customerTier = Shared.db.CustomerTiers.Find(this.tier_id);

            if (customerTier != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.CustomerTiers.Remove(customerTier);
                Shared.db.SaveChanges();
                this.loadTiers();
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
            this.loadTiers();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlTiers.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditTier tier = new AddEditTier();
            //tier.setTiersObject(this);
            tier.setTypeOperation("Add");
            tier.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.tierEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.tierDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlTiers.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadTiers();
        }
    }
}