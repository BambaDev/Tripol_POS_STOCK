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

namespace Pos.Forms.CardFidelity.RewardHistory
{
    public partial class RewardHistories : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int reward_history_id = 0;

        public RewardHistories()
        {
            InitializeComponent();
        }

        private void RewardHistories_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadRewardHistories();
        }

        public void loadRewardHistories()
        {
            gridControlRewardHistories.DataSource = Shared.db.RewardHistories.OrderByDescending(p => p.Id).ToList();
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
            this.activityLogDelete();
        }

        private void repDeleteRewardHistory_Click(object sender, EventArgs e)
        {
            this.activityLogDelete();
        }

        private void gridViewRewardHistories_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_history_id = int.Parse(gridViewRewardHistories.GetRowCellValue(gridViewRewardHistories.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void activityLogDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.reward_history_id = int.Parse(gridViewRewardHistories.GetRowCellValue(gridViewRewardHistories.FocusedRowHandle, "Id").ToString());

            Models.RewardHistory rewardHistory = Shared.db.RewardHistories.Find(this.reward_history_id);

            if (rewardHistory != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.RewardHistories.Remove(rewardHistory);
                Shared.db.SaveChanges();
                this.loadRewardHistories();
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
            this.loadRewardHistories();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlRewardHistories.ShowPrintPreview();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlRewardHistories.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadRewardHistories();
        }
    }
}