namespace Pos.Forms.CardFidelity.Reward
{
    partial class PreviewRewards
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
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlRewards = new DevExpress.XtraGrid.GridControl();
            gridViewRewards = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colName = new DevExpress.XtraGrid.Columns.GridColumn();
            colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            colPointsRequired = new DevExpress.XtraGrid.Columns.GridColumn();
            colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRewards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRewards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
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
            layoutControl1.Controls.Add(gridControlRewards);
            layoutControl1.Controls.Add(btnCloseFrm);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new System.Drawing.Size(932, 552);
            layoutControl1.TabIndex = 2;
            layoutControl1.Text = "layoutControl1";
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlRewards;
            searchControl1.Location = new System.Drawing.Point(106, 81);
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlRewards;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.Size = new System.Drawing.Size(803, 24);
            searchControl1.StyleController = layoutControl1;
            searchControl1.TabIndex = 2;
            // 
            // gridControlRewards
            // 
            gridControlRewards.Location = new System.Drawing.Point(23, 125);
            gridControlRewards.MainView = gridViewRewards;
            gridControlRewards.Name = "gridControlRewards";
            gridControlRewards.Size = new System.Drawing.Size(886, 404);
            gridControlRewards.TabIndex = 3;
            gridControlRewards.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRewards });
            // 
            // gridViewRewards
            // 
            gridViewRewards.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            gridViewRewards.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewRewards.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            gridViewRewards.Appearance.Row.Options.UseFont = true;
            gridViewRewards.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colName, colCreatedAt, colPointsRequired, colImage });
            gridViewRewards.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            gridViewRewards.GridControl = gridControlRewards;
            gridViewRewards.Name = "gridViewRewards";
            gridViewRewards.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewRewards.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRewards.OptionsView.ShowAutoFilterRow = true;
            gridViewRewards.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            colId.Caption = "Id";
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.OptionsColumn.AllowEdit = false;
            colId.Visible = true;
            colId.VisibleIndex = 0;
            colId.Width = 94;
            // 
            // colName
            // 
            colName.Caption = "Name";
            colName.FieldName = "Name";
            colName.Name = "colName";
            colName.OptionsColumn.AllowEdit = false;
            colName.Visible = true;
            colName.VisibleIndex = 2;
            colName.Width = 509;
            // 
            // colCreatedAt
            // 
            colCreatedAt.Caption = "Created At";
            colCreatedAt.FieldName = "CreatedAt";
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.OptionsColumn.AllowEdit = false;
            colCreatedAt.Visible = true;
            colCreatedAt.VisibleIndex = 4;
            colCreatedAt.Width = 256;
            // 
            // colPointsRequired
            // 
            colPointsRequired.Caption = "Points Required";
            colPointsRequired.FieldName = "PointsRequired";
            colPointsRequired.Name = "colPointsRequired";
            colPointsRequired.Visible = true;
            colPointsRequired.VisibleIndex = 3;
            colPointsRequired.Width = 297;
            // 
            // colImage
            // 
            colImage.Caption = "Image";
            colImage.FieldName = "Image";
            colImage.Name = "colImage";
            colImage.Visible = true;
            colImage.VisibleIndex = 1;
            colImage.Width = 146;
            // 
            // btnCloseFrm
            // 
            btnCloseFrm.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnCloseFrm.ImageOptions.SvgImage = Properties.Resources.cancel2;
            btnCloseFrm.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            btnCloseFrm.Location = new System.Drawing.Point(876, 15);
            btnCloseFrm.Name = "btnCloseFrm";
            btnCloseFrm.Size = new System.Drawing.Size(41, 38);
            btnCloseFrm.StyleController = layoutControl1;
            btnCloseFrm.TabIndex = 0;
            btnCloseFrm.Text = "simpleButton1";
            btnCloseFrm.Click += btnCloseFrm_Click;
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
            layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroup2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlGroup5 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 58);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(922, 484);
            layoutControlGroup2.Text = "Rewards";
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlRewards;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(890, 408);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = searchControl1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(890, 28);
            layoutControlItem2.Text = "Search";
            layoutControlItem2.TextSize = new System.Drawing.Size(71, 19);
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, emptySpaceItem1, simpleLabelItem1 });
            layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(922, 58);
            layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(861, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(44, 42);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(45, 42);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(120, 0);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(741, 42);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            simpleLabelItem1.AllowHotTrack = false;
            simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(120, 42);
            simpleLabelItem1.Text = "Rewards";
            simpleLabelItem1.TextSize = new System.Drawing.Size(71, 19);
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(906, 44);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 44);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup5.Size = new System.Drawing.Size(906, 424);
            layoutControlGroup5.TextVisible = false;
            // 
            // PreviewRewards
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(932, 552);
            Controls.Add(layoutControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            Name = "PreviewRewards";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Rewards";
            Load += Rewards_Load;
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRewards).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRewards).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlRewards;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRewards;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colPointsRequired;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
    }
}