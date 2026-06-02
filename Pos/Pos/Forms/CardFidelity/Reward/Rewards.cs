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

namespace Pos.Forms.CardFidelity.Reward
{
    public partial class Rewards : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int reward_id = 0;

        public Rewards()
        {
            InitializeComponent();
        }

        private void Rewards_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadRewards();
        }

        public void loadRewards()
        {
            gridControlRewards.DataSource = Shared.db.Rewards.OrderByDescending(p => p.Id).ToList();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditReward reward = new AddEditReward();
            reward.setRewardsObject(this);
            reward.setTypeOperation("Add");
            reward.ShowDialog();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.rewardEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.rewardDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_id = int.Parse(gridViewRewards.GetRowCellValue(gridViewRewards.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        private void gridViewRewards_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_id = int.Parse(gridViewRewards.GetRowCellValue(gridViewRewards.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.rewardEdit();
        }

        private void repEditReward_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_id = int.Parse(gridViewRewards.GetRowCellValue(gridViewRewards.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
            this.rewardEdit();
        }

        private void repDeleteReward_Click(object sender, EventArgs e)
        {
            this.rewardDelete();
        }

        private void gridViewRewards_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_id = int.Parse(gridViewRewards.GetRowCellValue(gridViewRewards.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void rewardEdit()
        {
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();

            AddEditReward addEditReward = new AddEditReward();

            // This ensures the overlay form is displayed behind the modal form but above the parent form
            addEditReward.FormClosed += (s, args) => overlay.Close();

            addEditReward.setRewardsObject(this);
            addEditReward.setTypeOperation("Edit");
            addEditReward.Show();
            addEditReward.TopMost = true;
        }

        public void rewardDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_id = int.Parse(gridViewRewards.GetRowCellValue(gridViewRewards.FocusedRowHandle, "Id").ToString());

            Models.Reward reward = Shared.db.Rewards.Find(this.reward_id);

            if (reward != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.Rewards.Remove(reward);
                Shared.db.SaveChanges();
                this.loadRewards();
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
            this.loadRewards();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlRewards.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddEditReward reward = new AddEditReward();
            //reward.setRewardsObject(this);
            reward.setTypeOperation("Add");
            reward.ShowDialog();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.rewardEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.rewardDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlRewards.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadRewards();
        }
    }
}