namespace Pos.Forms.Alert
{
    partial class AlertQuantity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertQuantity));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlProducts = new DevExpress.XtraGrid.GridControl();
            gridViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            colAlertQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            colSellingPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            btnOK = new DevExpress.XtraEditors.SimpleButton();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlProducts);
            layoutControl1.Controls.Add(btnOK);
            layoutControl1.Controls.Add(btnCloseFrm);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlProducts;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlProducts;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlProducts
            // 
            resources.ApplyResources(gridControlProducts, "gridControlProducts");
            gridControlProducts.MainView = gridViewProducts;
            gridControlProducts.Name = "gridControlProducts";
            gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewProducts });
            // 
            // gridViewProducts
            // 
            gridViewProducts.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.GroupFooter.Font");
            gridViewProducts.Appearance.GroupFooter.Options.UseFont = true;
            gridViewProducts.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.GroupRow.Font");
            gridViewProducts.Appearance.GroupRow.Options.UseFont = true;
            gridViewProducts.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.HeaderPanel.Font");
            gridViewProducts.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewProducts.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.Preview.Font");
            gridViewProducts.Appearance.Preview.Options.UseFont = true;
            gridViewProducts.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.Row.Font");
            gridViewProducts.Appearance.Row.Options.UseFont = true;
            gridViewProducts.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewProducts.Appearance.TopNewRow.Font");
            gridViewProducts.Appearance.TopNewRow.Options.UseFont = true;
            gridViewProducts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colImage, colProductName, colAlertQuantity, colQuantity, colSellingPrice, colTotal });
            gridViewProducts.GridControl = gridControlProducts;
            gridViewProducts.Name = "gridViewProducts";
            gridViewProducts.OptionsView.ShowGroupPanel = false;
            gridViewProducts.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            resources.ApplyResources(colId, "colId");
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.OptionsColumn.AllowEdit = false;
            colId.OptionsColumn.AllowFocus = false;
            colId.OptionsFilter.AllowAutoFilter = false;
            colId.OptionsFilter.AllowFilter = false;
            // 
            // colImage
            // 
            resources.ApplyResources(colImage, "colImage");
            colImage.FieldName = "Image";
            colImage.Name = "colImage";
            colImage.OptionsColumn.AllowEdit = false;
            colImage.OptionsFilter.AllowAutoFilter = false;
            colImage.OptionsFilter.AllowFilter = false;
            // 
            // colProductName
            // 
            resources.ApplyResources(colProductName, "colProductName");
            colProductName.FieldName = "ProductName";
            colProductName.Name = "colProductName";
            colProductName.OptionsColumn.AllowEdit = false;
            colProductName.OptionsFilter.AllowAutoFilter = false;
            colProductName.OptionsFilter.AllowFilter = false;
            // 
            // colAlertQuantity
            // 
            resources.ApplyResources(colAlertQuantity, "colAlertQuantity");
            colAlertQuantity.FieldName = "AlertQuantity";
            colAlertQuantity.Name = "colAlertQuantity";
            colAlertQuantity.OptionsColumn.AllowEdit = false;
            colAlertQuantity.OptionsFilter.AllowAutoFilter = false;
            colAlertQuantity.OptionsFilter.AllowFilter = false;
            // 
            // colQuantity
            // 
            resources.ApplyResources(colQuantity, "colQuantity");
            colQuantity.FieldName = "Quantity";
            colQuantity.Name = "colQuantity";
            colQuantity.OptionsColumn.AllowEdit = false;
            colQuantity.OptionsFilter.AllowAutoFilter = false;
            colQuantity.OptionsFilter.AllowFilter = false;
            // 
            // colSellingPrice
            // 
            resources.ApplyResources(colSellingPrice, "colSellingPrice");
            colSellingPrice.FieldName = "SellingPrice";
            colSellingPrice.Name = "colSellingPrice";
            colSellingPrice.OptionsColumn.AllowEdit = false;
            colSellingPrice.OptionsFilter.AllowAutoFilter = false;
            colSellingPrice.OptionsFilter.AllowFilter = false;
            // 
            // colTotal
            // 
            resources.ApplyResources(colTotal, "colTotal");
            colTotal.FieldName = "Total";
            colTotal.Name = "colTotal";
            colTotal.OptionsColumn.AllowEdit = false;
            colTotal.OptionsFilter.AllowAutoFilter = false;
            colTotal.OptionsFilter.AllowFilter = false;
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
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup2, layoutControlGroup3 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(761, 553);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceGroup.Font");
            layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup1.CaptionImageOptions.SvgImage = Properties.Resources.products;
            layoutControlGroup1.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlGroup5 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 54);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(751, 438);
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(735, 46);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font");
            layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem4.Control = searchControl1;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(719, 30);
            resources.ApplyResources(layoutControlItem4, "layoutControlItem4");
            layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem4.TextSize = new System.Drawing.Size(52, 18);
            layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 46);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup5.Size = new System.Drawing.Size(735, 347);
            layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = gridControlProducts;
            layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(719, 331);
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, simpleLabelItem1, emptySpaceItem2 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(751, 54);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = btnCloseFrm;
            layoutControlItem2.Location = new System.Drawing.Point(692, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(43, 38);
            layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AllowHotTrack = false;
            simpleLabelItem1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font");
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(159, 38);
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(106, 18);
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(159, 0);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(533, 38);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 492);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(751, 51);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = btnOK;
            layoutControlItem1.Location = new System.Drawing.Point(643, 0);
            layoutControlItem1.MinSize = new System.Drawing.Size(39, 34);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(92, 35);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(643, 35);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // AlertQuantity
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            Name = "AlertQuantity";
            Load += AlertQuantity_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProducts;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colAlertQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colSellingPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
    }
}