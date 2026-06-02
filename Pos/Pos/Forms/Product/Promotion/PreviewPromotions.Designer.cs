namespace Pos.Forms.Product.Promotion
{
    partial class PreviewPromotions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewPromotions));
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlPromotions = new DevExpress.XtraGrid.GridControl();
            gridViewPromotions = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colType = new DevExpress.XtraGrid.Columns.GridColumn();
            colQty = new DevExpress.XtraGrid.Columns.GridColumn();
            colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            colPromotionCode = new DevExpress.XtraGrid.Columns.GridColumn();
            colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            btnExportXlsx = new DevExpress.XtraEditors.SimpleButton();
            btnExportPdf = new DevExpress.XtraEditors.SimpleButton();
            btnExportCsv = new DevExpress.XtraEditors.SimpleButton();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlPromotions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPromotions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            SuspendLayout();
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Location = new System.Drawing.Point(0, 0);
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(932, 120);
            Root.TextVisible = false;
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlPromotions);
            layoutControl1.Controls.Add(btnCloseFrm);
            layoutControl1.Controls.Add(btnExportXlsx);
            layoutControl1.Controls.Add(btnExportPdf);
            layoutControl1.Controls.Add(btnExportCsv);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlPromotions;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlPromotions;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlPromotions
            // 
            resources.ApplyResources(gridControlPromotions, "gridControlPromotions");
            gridControlPromotions.MainView = gridViewPromotions;
            gridControlPromotions.Name = "gridControlPromotions";
            gridControlPromotions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewPromotions });
            // 
            // gridViewPromotions
            // 
            gridViewPromotions.Appearance.FocusedRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.FocusedRow.Font");
            gridViewPromotions.Appearance.FocusedRow.Options.UseFont = true;
            gridViewPromotions.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.GroupFooter.Font");
            gridViewPromotions.Appearance.GroupFooter.Options.UseFont = true;
            gridViewPromotions.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.GroupRow.Font");
            gridViewPromotions.Appearance.GroupRow.Options.UseFont = true;
            gridViewPromotions.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.HeaderPanel.Font");
            gridViewPromotions.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewPromotions.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.Preview.Font");
            gridViewPromotions.Appearance.Preview.Options.UseFont = true;
            gridViewPromotions.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.Row.Font");
            gridViewPromotions.Appearance.Row.Options.UseFont = true;
            gridViewPromotions.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewPromotions.Appearance.TopNewRow.Font");
            gridViewPromotions.Appearance.TopNewRow.Options.UseFont = true;
            gridViewPromotions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colCreatedAt, colStartDate, colEndDate, colType, colQty, colDiscount, colStatus, colPromotionCode, colProduct, colImage });
            gridViewPromotions.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            gridViewPromotions.GridControl = gridControlPromotions;
            gridViewPromotions.Name = "gridViewPromotions";
            gridViewPromotions.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewPromotions.OptionsView.EnableAppearanceEvenRow = true;
            gridViewPromotions.OptionsView.ShowAutoFilterRow = true;
            gridViewPromotions.OptionsView.ShowGroupPanel = false;
            gridViewPromotions.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            resources.ApplyResources(colId, "colId");
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.OptionsColumn.AllowEdit = false;
            // 
            // colCreatedAt
            // 
            resources.ApplyResources(colCreatedAt, "colCreatedAt");
            colCreatedAt.FieldName = "CreatedAt";
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.OptionsColumn.AllowEdit = false;
            // 
            // colStartDate
            // 
            resources.ApplyResources(colStartDate, "colStartDate");
            colStartDate.FieldName = "StartDate";
            colStartDate.Name = "colStartDate";
            // 
            // colEndDate
            // 
            resources.ApplyResources(colEndDate, "colEndDate");
            colEndDate.FieldName = "EndDate";
            colEndDate.Name = "colEndDate";
            // 
            // colType
            // 
            resources.ApplyResources(colType, "colType");
            colType.FieldName = "Type";
            colType.Name = "colType";
            // 
            // colQty
            // 
            resources.ApplyResources(colQty, "colQty");
            colQty.FieldName = "Qty";
            colQty.Name = "colQty";
            // 
            // colDiscount
            // 
            resources.ApplyResources(colDiscount, "colDiscount");
            colDiscount.FieldName = "Discount";
            colDiscount.Name = "colDiscount";
            // 
            // colStatus
            // 
            resources.ApplyResources(colStatus, "colStatus");
            colStatus.FieldName = "Status";
            colStatus.Name = "colStatus";
            // 
            // colPromotionCode
            // 
            resources.ApplyResources(colPromotionCode, "colPromotionCode");
            colPromotionCode.FieldName = "PromotionCode";
            colPromotionCode.Name = "colPromotionCode";
            // 
            // colProduct
            // 
            resources.ApplyResources(colProduct, "colProduct");
            colProduct.FieldName = "Product.ProductName";
            colProduct.Name = "colProduct";
            // 
            // colImage
            // 
            resources.ApplyResources(colImage, "colImage");
            colImage.FieldName = "Product.Image";
            colImage.Name = "colImage";
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
            // btnExportXlsx
            // 
            btnExportXlsx.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnExportXlsx.ImageOptions.SvgImage = Properties.Resources.export_excel1;
            btnExportXlsx.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            resources.ApplyResources(btnExportXlsx, "btnExportXlsx");
            btnExportXlsx.Name = "btnExportXlsx";
            btnExportXlsx.StyleController = layoutControl1;
            // 
            // btnExportPdf
            // 
            btnExportPdf.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnExportPdf.ImageOptions.SvgImage = Properties.Resources.export_pdf1;
            btnExportPdf.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            resources.ApplyResources(btnExportPdf, "btnExportPdf");
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.StyleController = layoutControl1;
            // 
            // btnExportCsv
            // 
            btnExportCsv.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnExportCsv.ImageOptions.SvgImage = Properties.Resources.export_csv1;
            btnExportCsv.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            resources.ApplyResources(btnExportCsv, "btnExportCsv");
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.StyleController = layoutControl1;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2, layoutControlGroup3 });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(932, 552);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup2.AppearanceGroup.Font");
            layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup2.AppearanceItemCaption.Font");
            layoutControlGroup2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlGroup5, layoutControlGroup6 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 63);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(922, 479);
            resources.ApplyResources(layoutControlGroup2, "layoutControlGroup2");
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(656, 49);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font");
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = searchControl1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(640, 33);
            resources.ApplyResources(layoutControlItem2, "layoutControlItem2");
            layoutControlItem2.TextSize = new System.Drawing.Size(85, 18);
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 49);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup5.Size = new System.Drawing.Size(906, 414);
            layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlPromotions;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(890, 398);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, layoutControlItem5, layoutControlItem6 });
            layoutControlGroup6.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            layoutControlGroup6.Location = new System.Drawing.Point(656, 0);
            layoutControlGroup6.Name = "layoutControlGroup6";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition1.Width = 33D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 33D;
            columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition3.Width = 33D;
            layoutControlGroup6.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDefinition1, columnDefinition2, columnDefinition3 });
            rowDefinition1.Height = 100D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            layoutControlGroup6.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDefinition1 });
            layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup6.Size = new System.Drawing.Size(250, 49);
            layoutControlGroup6.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnExportXlsx;
            layoutControlItem4.Location = new System.Drawing.Point(156, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.OptionsTableLayoutItem.ColumnIndex = 2;
            layoutControlItem4.Size = new System.Drawing.Size(78, 33);
            layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = btnExportPdf;
            layoutControlItem5.Location = new System.Drawing.Point(78, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem5.Size = new System.Drawing.Size(78, 33);
            layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = btnExportCsv;
            layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(78, 33);
            layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, emptySpaceItem1, simpleLabelItem1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(922, 63);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(856, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(49, 47);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(50, 47);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(157, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(699, 47);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AllowHotTrack = false;
            simpleLabelItem1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font");
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(157, 47);
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(85, 18);
            // 
            // PreviewPromotions
            // 
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("PreviewPromotions.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.SvgImage = Properties.Resources.shopping_cart_promotion;
            MaximizeBox = false;
            Name = "PreviewPromotions";
            Load += Promotions_Load;
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlPromotions).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewPromotions).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlPromotions;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPromotions;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colQty;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colPromotionCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraEditors.SimpleButton btnExportXlsx;
        private DevExpress.XtraEditors.SimpleButton btnExportPdf;
        private DevExpress.XtraEditors.SimpleButton btnExportCsv;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}