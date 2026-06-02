namespace Pos.Forms.Purchase
{
    partial class PurchaseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseHistory));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlPurchaseHistories = new DevExpress.XtraGrid.GridControl();
            gridViewPurchaseHistories = new DevExpress.XtraGrid.Views.Grid.GridView();
            colYear = new DevExpress.XtraGrid.Columns.GridColumn();
            colMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlPurchaseHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPurchaseHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlPurchaseHistories);
            layoutControl1.Controls.Add(btnCloseFrm);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlPurchaseHistories;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlPurchaseHistories;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlPurchaseHistories
            // 
            resources.ApplyResources(gridControlPurchaseHistories, "gridControlPurchaseHistories");
            gridControlPurchaseHistories.MainView = gridViewPurchaseHistories;
            gridControlPurchaseHistories.Name = "gridControlPurchaseHistories";
            gridControlPurchaseHistories.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPurchaseHistories });
            // 
            // gridViewPurchaseHistories
            // 
            gridViewPurchaseHistories.Appearance.FocusedRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.FocusedRow.Font");
            gridViewPurchaseHistories.Appearance.FocusedRow.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.GroupFooter.Font");
            gridViewPurchaseHistories.Appearance.GroupFooter.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.GroupRow.Font");
            gridViewPurchaseHistories.Appearance.GroupRow.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.HeaderPanel.BorderColor = (System.Drawing.Color)resources.GetObject("gridViewPurchaseHistories.Appearance.HeaderPanel.BorderColor");
            gridViewPurchaseHistories.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.HeaderPanel.Font");
            gridViewPurchaseHistories.Appearance.HeaderPanel.Options.UseBorderColor = true;
            gridViewPurchaseHistories.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.Preview.Font");
            gridViewPurchaseHistories.Appearance.Preview.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.Row.Font");
            gridViewPurchaseHistories.Appearance.Row.Options.UseFont = true;
            gridViewPurchaseHistories.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPurchaseHistories.Appearance.TopNewRow.Font");
            gridViewPurchaseHistories.Appearance.TopNewRow.Options.UseFont = true;
            gridViewPurchaseHistories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colYear, colMonth, colTotalAmount });
            gridViewPurchaseHistories.GridControl = gridControlPurchaseHistories;
            gridViewPurchaseHistories.Name = "gridViewPurchaseHistories";
            gridViewPurchaseHistories.OptionsView.ShowGroupPanel = false;
            gridViewPurchaseHistories.OptionsView.ShowIndicator = false;
            // 
            // colYear
            // 
            resources.ApplyResources(colYear, "colYear");
            colYear.FieldName = "Year";
            colYear.ImageOptions.SvgImage = Properties.Resources.weekend;
            colYear.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            colYear.Name = "colYear";
            colYear.OptionsColumn.AllowEdit = false;
            // 
            // colMonth
            // 
            resources.ApplyResources(colMonth, "colMonth");
            colMonth.FieldName = "Month";
            colMonth.ImageOptions.SvgImage = Properties.Resources.new_year_calendar;
            colMonth.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            colMonth.Name = "colMonth";
            colMonth.OptionsColumn.AllowEdit = false;
            // 
            // colTotalAmount
            // 
            resources.ApplyResources(colTotalAmount, "colTotalAmount");
            colTotalAmount.FieldName = "TotalAmount";
            colTotalAmount.ImageOptions.SvgImage = Properties.Resources.bill;
            colTotalAmount.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.OptionsColumn.AllowEdit = false;
            // 
            // btnCloseFrm
            // 
            btnCloseFrm.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnCloseFrm.ImageOptions.SvgImage = Properties.Resources.cancel2;
            btnCloseFrm.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            resources.ApplyResources(btnCloseFrm, "btnCloseFrm");
            btnCloseFrm.Name = "btnCloseFrm";
            btnCloseFrm.StyleController = layoutControl1;
            btnCloseFrm.Click += btnCloseFrm_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, emptySpaceItem1, layoutControlItem3, simpleLabelItem1 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(624, 451);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceGroup.Font");
            layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceItemCaption.Font");
            layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup1.CaptionImageOptions.SvgImage = Properties.Resources.add_to_shopping_basket;
            layoutControlGroup1.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroup1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem1 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 33);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(614, 408);
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font");
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = searchControl1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(598, 32);
            resources.ApplyResources(layoutControlItem2, "layoutControlItem2");
            layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem2.TextSize = new System.Drawing.Size(55, 19);
            layoutControlItem2.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(159, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(406, 33);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(565, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(35, 33);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(49, 33);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AllowHotTrack = false;
            simpleLabelItem1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font");
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(159, 33);
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(151, 19);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlPurchaseHistories;
            layoutControlItem1.Location = new System.Drawing.Point(0, 32);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(598, 333);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // PurchaseHistory
            // 
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("PurchaseHistory.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            IconOptions.SvgImage = Properties.Resources.return_purchase;
            Name = "PurchaseHistory";
            Load += PurchaseHistory_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlPurchaseHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPurchaseHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlPurchaseHistories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPurchaseHistories;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colYear;
        private DevExpress.XtraGrid.Columns.GridColumn colMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}