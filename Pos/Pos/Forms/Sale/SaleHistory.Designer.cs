namespace Pos.Forms.Sale
{
    partial class SaleHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleHistory));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlSaleHistories = new DevExpress.XtraGrid.GridControl();
            gridViewSaleHistories = new DevExpress.XtraGrid.Views.Grid.GridView();
            colYear = new DevExpress.XtraGrid.Columns.GridColumn();
            colMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSaleHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSaleHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlSaleHistories);
            layoutControl1.Controls.Add(btnCloseFrm);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlSaleHistories;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlSaleHistories;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlSaleHistories
            // 
            resources.ApplyResources(gridControlSaleHistories, "gridControlSaleHistories");
            gridControlSaleHistories.MainView = gridViewSaleHistories;
            gridControlSaleHistories.Name = "gridControlSaleHistories";
            gridControlSaleHistories.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewSaleHistories });
            // 
            // gridViewSaleHistories
            // 
            gridViewSaleHistories.Appearance.FocusedRow.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.FocusedRow.Font");
            gridViewSaleHistories.Appearance.FocusedRow.Options.UseFont = true;
            gridViewSaleHistories.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.GroupFooter.Font");
            gridViewSaleHistories.Appearance.GroupFooter.Options.UseFont = true;
            gridViewSaleHistories.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.GroupRow.Font");
            gridViewSaleHistories.Appearance.GroupRow.Options.UseFont = true;
            gridViewSaleHistories.Appearance.HeaderPanel.BorderColor = (System.Drawing.Color)resources.GetObject("gridViewSaleHistories.Appearance.HeaderPanel.BorderColor");
            gridViewSaleHistories.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.HeaderPanel.Font");
            gridViewSaleHistories.Appearance.HeaderPanel.Options.UseBorderColor = true;
            gridViewSaleHistories.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewSaleHistories.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.Preview.Font");
            gridViewSaleHistories.Appearance.Preview.Options.UseFont = true;
            gridViewSaleHistories.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.Row.Font");
            gridViewSaleHistories.Appearance.Row.Options.UseFont = true;
            gridViewSaleHistories.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewSaleHistories.Appearance.TopNewRow.Font");
            gridViewSaleHistories.Appearance.TopNewRow.Options.UseFont = true;
            gridViewSaleHistories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colYear, colMonth, colTotalAmount });
            gridViewSaleHistories.GridControl = gridControlSaleHistories;
            gridViewSaleHistories.Name = "gridViewSaleHistories";
            gridViewSaleHistories.OptionsView.ShowGroupPanel = false;
            gridViewSaleHistories.OptionsView.ShowIndicator = false;
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
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup4 });
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
            layoutControlGroup1.CaptionImageOptions.SvgImage = Properties.Resources.order_histories;
            layoutControlGroup1.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2, layoutControlGroup3 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 49);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(614, 392);
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(598, 46);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = searchControl1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(582, 30);
            resources.ApplyResources(layoutControlItem2, "layoutControlItem2");
            layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem2.TextSize = new System.Drawing.Size(55, 19);
            layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 46);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(598, 301);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlSaleHistories;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(582, 285);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { emptySpaceItem2, layoutControlItem3, simpleLabelItem1 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(614, 49);
            layoutControlGroup4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(124, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(425, 33);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(549, 0);
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
            simpleLabelItem1.Size = new System.Drawing.Size(124, 33);
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(120, 19);
            // 
            // SaleHistory
            // 
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("SaleHistory.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            IconOptions.SvgImage = Properties.Resources.tab_return_sales;
            Name = "SaleHistory";
            Load += PurchaseHistory_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlSaleHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewSaleHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlSaleHistories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSaleHistories;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colYear;
        private DevExpress.XtraGrid.Columns.GridColumn colMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
    }
}