namespace Pos.Forms.Alert
{
    partial class TodaySummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TodaySummary));
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            gridControlTodaySummary = new DevExpress.XtraGrid.GridControl();
            gridViewTodaySummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            colPeriod = new DevExpress.XtraGrid.Columns.GridColumn();
            colGrossTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            colNetTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            btnPrintTodaySummary = new DevExpress.XtraEditors.SimpleButton();
            btnTodaySummaryWhatsUpSender = new DevExpress.XtraEditors.SimpleButton();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            txtCashInHand = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridControlTodaySummary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTodaySummary).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCashInHand.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(gridControlTodaySummary);
            layoutControl1.Controls.Add(btnPrintTodaySummary);
            layoutControl1.Controls.Add(btnTodaySummaryWhatsUpSender);
            layoutControl1.Controls.Add(btnCloseFrm);
            layoutControl1.Controls.Add(txtCashInHand);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // gridControlTodaySummary
            // 
            resources.ApplyResources(gridControlTodaySummary, "gridControlTodaySummary");
            gridControlTodaySummary.MainView = gridViewTodaySummary;
            gridControlTodaySummary.Name = "gridControlTodaySummary";
            gridControlTodaySummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewTodaySummary });
            // 
            // gridViewTodaySummary
            // 
            gridViewTodaySummary.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewTodaySummary.Appearance.GroupFooter.Font");
            gridViewTodaySummary.Appearance.GroupFooter.Options.UseFont = true;
            gridViewTodaySummary.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewTodaySummary.Appearance.GroupRow.Font");
            gridViewTodaySummary.Appearance.GroupRow.Options.UseFont = true;
            gridViewTodaySummary.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewTodaySummary.Appearance.HeaderPanel.Font");
            gridViewTodaySummary.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewTodaySummary.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewTodaySummary.Appearance.Preview.Font");
            gridViewTodaySummary.Appearance.Preview.Options.UseFont = true;
            gridViewTodaySummary.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewTodaySummary.Appearance.Row.Font");
            gridViewTodaySummary.Appearance.Row.Options.UseFont = true;
            gridViewTodaySummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colPeriod, colGrossTotal, colNetTotal });
            gridViewTodaySummary.GridControl = gridControlTodaySummary;
            gridViewTodaySummary.Name = "gridViewTodaySummary";
            gridViewTodaySummary.OptionsView.ShowGroupPanel = false;
            gridViewTodaySummary.OptionsView.ShowIndicator = false;
            // 
            // colPeriod
            // 
            resources.ApplyResources(colPeriod, "colPeriod");
            colPeriod.FieldName = "Period";
            colPeriod.Name = "colPeriod";
            colPeriod.OptionsColumn.AllowEdit = false;
            colPeriod.OptionsColumn.AllowFocus = false;
            colPeriod.OptionsFilter.AllowAutoFilter = false;
            colPeriod.OptionsFilter.AllowFilter = false;
            // 
            // colGrossTotal
            // 
            resources.ApplyResources(colGrossTotal, "colGrossTotal");
            colGrossTotal.FieldName = "GrossTotal";
            colGrossTotal.Name = "colGrossTotal";
            colGrossTotal.OptionsColumn.AllowEdit = false;
            colGrossTotal.OptionsColumn.AllowFocus = false;
            colGrossTotal.OptionsFilter.AllowAutoFilter = false;
            colGrossTotal.OptionsFilter.AllowFilter = false;
            // 
            // colNetTotal
            // 
            resources.ApplyResources(colNetTotal, "colNetTotal");
            colNetTotal.FieldName = "NetTotal";
            colNetTotal.Name = "colNetTotal";
            // 
            // btnPrintTodaySummary
            // 
            btnPrintTodaySummary.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnPrintTodaySummary.Appearance.BackColor");
            btnPrintTodaySummary.Appearance.BorderColor = (System.Drawing.Color)resources.GetObject("btnPrintTodaySummary.Appearance.BorderColor");
            btnPrintTodaySummary.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnPrintTodaySummary.Appearance.Font");
            btnPrintTodaySummary.Appearance.Options.UseBackColor = true;
            btnPrintTodaySummary.Appearance.Options.UseBorderColor = true;
            btnPrintTodaySummary.Appearance.Options.UseFont = true;
            btnPrintTodaySummary.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnPrintTodaySummary.ImageOptions.SvgImage = Properties.Resources.print_white;
            btnPrintTodaySummary.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnPrintTodaySummary, "btnPrintTodaySummary");
            btnPrintTodaySummary.Name = "btnPrintTodaySummary";
            btnPrintTodaySummary.StyleController = layoutControl1;
            btnPrintTodaySummary.Click += btnPrintTodaySummary_Click;
            // 
            // btnTodaySummaryWhatsUpSender
            // 
            btnTodaySummaryWhatsUpSender.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnTodaySummaryWhatsUpSender.ImageOptions.SvgImage = Properties.Resources.whatsapp;
            btnTodaySummaryWhatsUpSender.ImageOptions.SvgImageSize = new System.Drawing.Size(40, 40);
            resources.ApplyResources(btnTodaySummaryWhatsUpSender, "btnTodaySummaryWhatsUpSender");
            btnTodaySummaryWhatsUpSender.Name = "btnTodaySummaryWhatsUpSender";
            btnTodaySummaryWhatsUpSender.StyleController = layoutControl1;
            btnTodaySummaryWhatsUpSender.Click += btnTodaySummaryWhatsUpSender_Click;
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
            // txtCashInHand
            // 
            resources.ApplyResources(txtCashInHand, "txtCashInHand");
            txtCashInHand.Name = "txtCashInHand";
            txtCashInHand.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtCashInHand.Properties.Appearance.Font");
            txtCashInHand.Properties.Appearance.Options.UseFont = true;
            txtCashInHand.Properties.ReadOnly = true;
            txtCashInHand.StyleController = layoutControl1;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, simpleLabelItem1, layoutControlGroup2 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(582, 528);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlGroup3, layoutControlGroup4 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 101);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup1.Size = new System.Drawing.Size(572, 417);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlTodaySummary;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(566, 295);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem5 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 295);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(566, 52);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem5.AppearanceItemCaption.Font");
            layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem5.Control = txtCashInHand;
            layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(550, 36);
            resources.ApplyResources(layoutControlItem5, "layoutControlItem5");
            layoutControlItem5.TextSize = new System.Drawing.Size(169, 19);
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem3 });
            layoutControlGroup4.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            layoutControlGroup4.Location = new System.Drawing.Point(0, 347);
            layoutControlGroup4.Name = "layoutControlGroup4";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition1.Width = 100D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 100D;
            layoutControlGroup4.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDefinition1, columnDefinition2 });
            rowDefinition1.Height = 100D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            layoutControlGroup4.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDefinition1 });
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(566, 64);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = btnPrintTodaySummary;
            layoutControlItem2.Location = new System.Drawing.Point(275, 0);
            layoutControlItem2.MinSize = new System.Drawing.Size(55, 34);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem2.Size = new System.Drawing.Size(275, 48);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnTodaySummaryWhatsUpSender;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(50, 48);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(275, 48);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AllowHotTrack = false;
            simpleLabelItem1.AppearanceItemCaption.BackColor = (System.Drawing.Color)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.BackColor");
            simpleLabelItem1.AppearanceItemCaption.BorderColor = (System.Drawing.Color)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.BorderColor");
            simpleLabelItem1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font");
            simpleLabelItem1.AppearanceItemCaption.ForeColor = (System.Drawing.Color)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.ForeColor");
            simpleLabelItem1.AppearanceItemCaption.Options.UseBackColor = true;
            simpleLabelItem1.AppearanceItemCaption.Options.UseBorderColor = true;
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = true;
            simpleLabelItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            simpleLabelItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            simpleLabelItem1.Location = new System.Drawing.Point(0, 54);
            simpleLabelItem1.MinSize = new System.Drawing.Size(147, 23);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(572, 47);
            simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(169, 23);
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, simpleLabelItem2, emptySpaceItem2 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(572, 54);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnCloseFrm;
            layoutControlItem4.Location = new System.Drawing.Point(491, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(65, 38);
            layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // simpleLabelItem2
            // 
            simpleLabelItem2.AllowHotTrack = false;
            simpleLabelItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font");
            simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem2.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem2.Name = "simpleLabelItem2";
            simpleLabelItem2.Size = new System.Drawing.Size(211, 38);
            resources.ApplyResources(simpleLabelItem2, "simpleLabelItem2");
            simpleLabelItem2.TextSize = new System.Drawing.Size(169, 18);
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(211, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(280, 38);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // TodaySummary
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.SvgImage = Properties.Resources.overview;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TodaySummary";
            Load += TodaySummary_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridControlTodaySummary).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewTodaySummary).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCashInHand.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.GridControl gridControlTodaySummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTodaySummary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnPrintTodaySummary;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colPeriod;
        private DevExpress.XtraGrid.Columns.GridColumn colGrossTotal;
        private DevExpress.XtraEditors.SimpleButton btnTodaySummaryWhatsUpSender;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit txtCashInHand;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraGrid.Columns.GridColumn colNetTotal;
    }
}