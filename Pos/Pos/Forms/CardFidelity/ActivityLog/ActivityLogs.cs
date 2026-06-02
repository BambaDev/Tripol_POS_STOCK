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

namespace Pos.Forms.CardFidelity.ActivityLog
{
    public partial class ActivityLogs : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int activity_log_id = 0;

        public ActivityLogs()
        {
            InitializeComponent();
        }

        private void ActivityLogs_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadActivityLogs();
        }

        public void loadActivityLogs()
        {
            gridControlActivityLogs.DataSource = Shared.db.ActivityLogs.OrderByDescending(p => p.Id).ToList();
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

        private void repDeleteTier_Click(object sender, EventArgs e)
        {
            this.activityLogDelete();
        }

        private void gridViewActivityLogs_RowClick(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.activity_log_id = int.Parse(gridViewActivityLogs.GetRowCellValue(gridViewActivityLogs.FocusedRowHandle, "Id").ToString());
            Function.Sound.Selected();
        }

        public void activityLogDelete()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;

            this.activity_log_id = int.Parse(gridViewActivityLogs.GetRowCellValue(gridViewActivityLogs.FocusedRowHandle, "Id").ToString());

            Models.ActivityLog activityLog = Shared.db.ActivityLogs.Find(this.activity_log_id);

            if (activityLog != null & XtraMessageBox.Show("Are you sure want to delete Item ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Shared.db.ActivityLogs.Remove(activityLog);
                Shared.db.SaveChanges();
                this.loadActivityLogs();
                Function.Sound.Deleted();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlActivityLogs.ShowPrintPreview();
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadActivityLogs();
        }
    }
}