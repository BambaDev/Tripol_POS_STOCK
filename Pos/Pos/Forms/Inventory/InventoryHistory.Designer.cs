namespace Pos.Forms.Inventory
{
    partial class InventoryHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryHistory));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlInventoryHistories = new DevExpress.XtraGrid.GridControl();
            gridViewInventoryHistories = new DevExpress.XtraGrid.Views.Grid.GridView();
            colYear = new DevExpress.XtraGrid.Columns.GridColumn();
            colMonth = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotalAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            btnOK = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlInventoryHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewInventoryHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlInventoryHistories);
            layoutControl1.Controls.Add(btnCloseFrm);
            layoutControl1.Controls.Add(btnOK);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlInventoryHistories;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlInventoryHistories;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlInventoryHistories
            // 
            resources.ApplyResources(gridControlInventoryHistories, "gridControlInventoryHistories");
            gridControlInventoryHistories.MainView = gridViewInventoryHistories;
            gridControlInventoryHistories.Name = "gridControlInventoryHistories";
            gridControlInventoryHistories.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewInventoryHistories });
            // 
            // gridViewInventoryHistories
            // 
            gridViewInventoryHistories.Appearance.FocusedRow.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.FocusedRow.Font");
            gridViewInventoryHistories.Appearance.FocusedRow.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.GroupFooter.Font");
            gridViewInventoryHistories.Appearance.GroupFooter.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.GroupRow.Font");
            gridViewInventoryHistories.Appearance.GroupRow.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.HeaderPanel.BorderColor = (System.Drawing.Color)resources.GetObject("gridViewInventoryHistories.Appearance.HeaderPanel.BorderColor");
            gridViewInventoryHistories.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.HeaderPanel.Font");
            gridViewInventoryHistories.Appearance.HeaderPanel.Options.UseBorderColor = true;
            gridViewInventoryHistories.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.Preview.Font");
            gridViewInventoryHistories.Appearance.Preview.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.Row.Font");
            gridViewInventoryHistories.Appearance.Row.Options.UseFont = true;
            gridViewInventoryHistories.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewInventoryHistories.Appearance.TopNewRow.Font");
            gridViewInventoryHistories.Appearance.TopNewRow.Options.UseFont = true;
            gridViewInventoryHistories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colYear, colMonth, colTotalAmount });
            gridViewInventoryHistories.GridControl = gridControlInventoryHistories;
            gridViewInventoryHistories.Name = "gridViewInventoryHistories";
            gridViewInventoryHistories.OptionsView.ShowGroupPanel = false;
            gridViewInventoryHistories.OptionsView.ShowIndicator = false;
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
            btnCloseFrm.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnCloseFrm, "btnCloseFrm");
            btnCloseFrm.Name = "btnCloseFrm";
            btnCloseFrm.StyleController = layoutControl1;
            btnCloseFrm.Click += btnCloseFrm_Click;
            // 
            // btnOK
            // 
            btnOK.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnOK.Appearance.BackColor");
            btnOK.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnOK.Appearance.Font");
            btnOK.Appearance.Options.UseBackColor = true;
            btnOK.Appearance.Options.UseFont = true;
            resources.ApplyResources(btnOK, "btnOK");
            btnOK.Name = "btnOK";
            btnOK.StyleController = layoutControl1;
            btnOK.Click += btnOK_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup2, layoutControlGroup3 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(527, 434);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AppearanceGroup.BorderColor = (System.Drawing.Color)resources.GetObject("layoutControlGroup1.AppearanceGroup.BorderColor");
            layoutControlGroup1.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceGroup.Font");
            layoutControlGroup1.AppearanceGroup.Options.UseBorderColor = true;
            layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceItemCaption.Font");
            layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlGroup5 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 58);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(517, 308);
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(501, 46);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font");
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = searchControl1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(485, 30);
            resources.ApplyResources(layoutControlItem2, "layoutControlItem2");
            layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem2.TextSize = new System.Drawing.Size(52, 18);
            layoutControlItem2.TextToControlDistance = 5;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 46);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup5.Size = new System.Drawing.Size(501, 226);
            layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlInventoryHistories;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(485, 210);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, simpleLabelItem1, emptySpaceItem1 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(517, 58);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(457, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(44, 42);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(44, 42);
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
            simpleLabelItem1.Size = new System.Drawing.Size(174, 42);
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(170, 19);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(174, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(283, 42);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, emptySpaceItem2 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 366);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(517, 58);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnOK;
            layoutControlItem4.Location = new System.Drawing.Point(410, 0);
            layoutControlItem4.MinSize = new System.Drawing.Size(38, 34);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(91, 42);
            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(410, 42);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // InventoryHistory
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "InventoryHistory";
            Load += InventoryHistory_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlInventoryHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewInventoryHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlInventoryHistories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewInventoryHistories;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colYear;
        private DevExpress.XtraGrid.Columns.GridColumn colMonth;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalAmount;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}