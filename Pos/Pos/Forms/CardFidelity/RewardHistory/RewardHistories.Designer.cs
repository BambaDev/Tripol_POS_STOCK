namespace Pos.Forms.CardFidelity.RewardHistory
{
    partial class RewardHistories
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnAdd = new DevExpress.XtraBars.BarButtonItem();
            btnEdit = new DevExpress.XtraBars.BarButtonItem();
            btnDelete = new DevExpress.XtraBars.BarButtonItem();
            btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            btnPrint = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            btnRefreshItems = new DevExpress.XtraEditors.SimpleButton();
            btnPrintItems = new DevExpress.XtraEditors.SimpleButton();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlRewardHistories = new DevExpress.XtraGrid.GridControl();
            gridViewRewardHistories = new DevExpress.XtraGrid.Views.Grid.GridView();
            colId = new DevExpress.XtraGrid.Columns.GridColumn();
            colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            tbBtnRewardHistoryDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            repDeleteRewardHistory = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            colDateClaimed = new DevExpress.XtraGrid.Columns.GridColumn();
            colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            colRewardId = new DevExpress.XtraGrid.Columns.GridColumn();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRewardHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRewardHistories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repDeleteRewardHistory).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            ribbon.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, btnAdd, btnEdit, btnDelete, btnRefresh, btnPrint });
            ribbon.Location = new System.Drawing.Point(0, 0);
            ribbon.MaxItemId = 6;
            ribbon.Name = "ribbon";
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbon.ShowQatLocationSelector = false;
            ribbon.ShowToolbarCustomizeItem = false;
            ribbon.Size = new System.Drawing.Size(932, 153);
            ribbon.StatusBar = ribbonStatusBar;
            ribbon.Toolbar.ShowCustomizeItem = false;
            ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            ribbon.Visible = false;
            // 
            // btnAdd
            // 
            btnAdd.Caption = "Add";
            btnAdd.Id = 1;
            btnAdd.ImageOptions.SvgImage = Properties.Resources.add_plus;
            btnAdd.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnAdd.ItemAppearance.Normal.Options.UseFont = true;
            btnAdd.Name = "btnAdd";
            btnAdd.ItemClick += btnAdd_ItemClick;
            // 
            // btnEdit
            // 
            btnEdit.Caption = "Edit";
            btnEdit.Id = 2;
            btnEdit.ImageOptions.SvgImage = Properties.Resources.edit_property;
            btnEdit.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnEdit.ItemAppearance.Normal.Options.UseFont = true;
            btnEdit.Name = "btnEdit";
            btnEdit.ItemClick += btnEdit_ItemClick;
            // 
            // btnDelete
            // 
            btnDelete.Caption = "Delete";
            btnDelete.Id = 3;
            btnDelete.ImageOptions.SvgImage = Properties.Resources.delete_trash;
            btnDelete.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnDelete.ItemAppearance.Normal.Options.UseFont = true;
            btnDelete.Name = "btnDelete";
            btnDelete.ItemClick += btnDelete_ItemClick;
            // 
            // btnRefresh
            // 
            btnRefresh.Caption = "Refresh";
            btnRefresh.Id = 4;
            btnRefresh.ImageOptions.SvgImage = Properties.Resources.refresh_folder;
            btnRefresh.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnRefresh.ItemAppearance.Normal.Options.UseFont = true;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ItemClick += btnRefresh_ItemClick;
            // 
            // btnPrint
            // 
            btnPrint.Caption = "Print";
            btnPrint.Id = 5;
            btnPrint.ImageOptions.SvgImage = Properties.Resources.print;
            btnPrint.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnPrint.ItemAppearance.Normal.Options.UseFont = true;
            btnPrint.Name = "btnPrint";
            btnPrint.ItemClick += btnPrint_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1, ribbonPageGroup2, ribbonPageGroup3 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(btnAdd);
            ribbonPageGroup1.ItemLinks.Add(btnEdit);
            ribbonPageGroup1.ItemLinks.Add(btnDelete);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.Alignment = DevExpress.XtraBars.Ribbon.RibbonPageGroupAlignment.Far;
            ribbonPageGroup2.ItemLinks.Add(btnRefresh);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.ItemLinks.Add(btnPrint);
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "ribbonPageGroup3";
            // 
            // ribbonStatusBar
            // 
            ribbonStatusBar.Location = new System.Drawing.Point(0, 519);
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            ribbonStatusBar.Size = new System.Drawing.Size(932, 33);
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
            layoutControl1.Controls.Add(btnRefreshItems);
            layoutControl1.Controls.Add(btnPrintItems);
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlRewardHistories);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 153);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new System.Drawing.Size(932, 366);
            layoutControl1.TabIndex = 2;
            layoutControl1.Text = "layoutControl1";
            // 
            // btnRefreshItems
            // 
            btnRefreshItems.Appearance.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            btnRefreshItems.Appearance.BorderColor = System.Drawing.Color.FromArgb(44, 62, 80);
            btnRefreshItems.Appearance.Options.UseBackColor = true;
            btnRefreshItems.Appearance.Options.UseBorderColor = true;
            btnRefreshItems.ImageOptions.SvgImage = Properties.Resources.refresh_white;
            btnRefreshItems.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            btnRefreshItems.Location = new System.Drawing.Point(817, 78);
            btnRefreshItems.Name = "btnRefreshItems";
            btnRefreshItems.Size = new System.Drawing.Size(95, 34);
            btnRefreshItems.StyleController = layoutControl1;
            btnRefreshItems.TabIndex = 11;
            btnRefreshItems.Text = "Refresh";
            btnRefreshItems.Click += btnRefreshItems_Click;
            // 
            // btnPrintItems
            // 
            btnPrintItems.Appearance.BackColor = System.Drawing.Color.FromArgb(142, 68, 173);
            btnPrintItems.Appearance.BorderColor = System.Drawing.Color.FromArgb(142, 68, 173);
            btnPrintItems.Appearance.Options.UseBackColor = true;
            btnPrintItems.Appearance.Options.UseBorderColor = true;
            btnPrintItems.ImageOptions.SvgImage = Properties.Resources.print_white;
            btnPrintItems.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            btnPrintItems.Location = new System.Drawing.Point(817, 40);
            btnPrintItems.Name = "btnPrintItems";
            btnPrintItems.Size = new System.Drawing.Size(95, 34);
            btnPrintItems.StyleController = layoutControl1;
            btnPrintItems.TabIndex = 12;
            btnPrintItems.Text = "Print";
            btnPrintItems.Click += btnPrintItems_Click;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlRewardHistories;
            searchControl1.Location = new System.Drawing.Point(95, 48);
            searchControl1.MenuManager = ribbon;
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlRewardHistories;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.Size = new System.Drawing.Size(694, 24);
            searchControl1.StyleController = layoutControl1;
            searchControl1.TabIndex = 0;
            // 
            // gridControlRewardHistories
            // 
            gridControlRewardHistories.Location = new System.Drawing.Point(28, 92);
            gridControlRewardHistories.MainView = gridViewRewardHistories;
            gridControlRewardHistories.MenuManager = ribbon;
            gridControlRewardHistories.Name = "gridControlRewardHistories";
            gridControlRewardHistories.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repDeleteRewardHistory });
            gridControlRewardHistories.Size = new System.Drawing.Size(761, 246);
            gridControlRewardHistories.TabIndex = 2;
            gridControlRewardHistories.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewRewardHistories });
            // 
            // gridViewRewardHistories
            // 
            gridViewRewardHistories.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            gridViewRewardHistories.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewRewardHistories.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F);
            gridViewRewardHistories.Appearance.Row.Options.UseFont = true;
            gridViewRewardHistories.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colId, colCreatedAt, tbBtnRewardHistoryDelete, colStatus, colDateClaimed, colImage, colCustomer, colRewardId });
            gridViewRewardHistories.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            gridViewRewardHistories.GridControl = gridControlRewardHistories;
            gridViewRewardHistories.Name = "gridViewRewardHistories";
            gridViewRewardHistories.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewRewardHistories.OptionsView.EnableAppearanceEvenRow = true;
            gridViewRewardHistories.OptionsView.ShowAutoFilterRow = true;
            gridViewRewardHistories.OptionsView.ShowGroupPanel = false;
            gridViewRewardHistories.RowClick += gridViewRewardHistories_RowClick;
            // 
            // colId
            // 
            colId.Caption = "Id";
            colId.FieldName = "Id";
            colId.Name = "colId";
            colId.OptionsColumn.AllowEdit = false;
            colId.Visible = true;
            colId.VisibleIndex = 0;
            colId.Width = 81;
            // 
            // colCreatedAt
            // 
            colCreatedAt.Caption = "Created At";
            colCreatedAt.FieldName = "CreatedAt";
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.OptionsColumn.AllowEdit = false;
            colCreatedAt.Visible = true;
            colCreatedAt.VisibleIndex = 6;
            colCreatedAt.Width = 102;
            // 
            // tbBtnRewardHistoryDelete
            // 
            tbBtnRewardHistoryDelete.Caption = "Delete";
            tbBtnRewardHistoryDelete.ColumnEdit = repDeleteRewardHistory;
            tbBtnRewardHistoryDelete.Name = "tbBtnRewardHistoryDelete";
            tbBtnRewardHistoryDelete.Visible = true;
            tbBtnRewardHistoryDelete.VisibleIndex = 7;
            tbBtnRewardHistoryDelete.Width = 58;
            // 
            // repDeleteRewardHistory
            // 
            repDeleteRewardHistory.AutoHeight = false;
            editorButtonImageOptions1.SvgImage = Properties.Resources.delete_trash;
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(25, 25);
            repDeleteRewardHistory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default) });
            repDeleteRewardHistory.ContextImageOptions.SvgImage = Properties.Resources.delete_trash;
            repDeleteRewardHistory.ContextImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            repDeleteRewardHistory.Name = "repDeleteRewardHistory";
            repDeleteRewardHistory.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repDeleteRewardHistory.Click += repDeleteRewardHistory_Click;
            // 
            // colStatus
            // 
            colStatus.Caption = "Status";
            colStatus.FieldName = "Status";
            colStatus.Name = "colStatus";
            colStatus.Visible = true;
            colStatus.VisibleIndex = 5;
            colStatus.Width = 357;
            // 
            // colDateClaimed
            // 
            colDateClaimed.Caption = "Date Claimed";
            colDateClaimed.FieldName = "DateClaimed";
            colDateClaimed.Name = "colDateClaimed";
            colDateClaimed.Visible = true;
            colDateClaimed.VisibleIndex = 4;
            colDateClaimed.Width = 240;
            // 
            // colImage
            // 
            colImage.Caption = "Image";
            colImage.FieldName = "Customer.Image";
            colImage.Name = "colImage";
            colImage.Visible = true;
            colImage.VisibleIndex = 1;
            colImage.Width = 112;
            // 
            // colCustomer
            // 
            colCustomer.Caption = "Customer";
            colCustomer.FieldName = "Customer.FirstName";
            colCustomer.Name = "colCustomer";
            colCustomer.Visible = true;
            colCustomer.VisibleIndex = 2;
            colCustomer.Width = 173;
            // 
            // colRewardId
            // 
            colRewardId.Caption = "Reward";
            colRewardId.FieldName = "Reward.Name";
            colRewardId.Name = "colRewardId";
            colRewardId.Visible = true;
            colRewardId.VisibleIndex = 3;
            colRewardId.Width = 183;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup2, layoutControlGroup3 });
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(932, 366);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroup2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlGroup5 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(797, 346);
            layoutControlGroup2.Text = "Reward Histories";
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridControlRewardHistories;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(765, 250);
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
            layoutControlItem2.Size = new System.Drawing.Size(765, 28);
            layoutControlItem2.Text = "Search";
            layoutControlItem2.TextSize = new System.Drawing.Size(55, 19);
            // 
            // layoutControlGroup3
            // 
            layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem6, layoutControlItem7, emptySpaceItem1 });
            layoutControlGroup3.Location = new System.Drawing.Point(797, 0);
            layoutControlGroup3.Name = "layoutControlGroup3";
            layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup3.Size = new System.Drawing.Size(115, 346);
            layoutControlGroup3.Text = "Actions";
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = btnPrintItems;
            layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(99, 38);
            layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = btnRefreshItems;
            layoutControlItem7.Location = new System.Drawing.Point(0, 38);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(99, 38);
            layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(0, 76);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(99, 234);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2 });
            layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup4.Name = "layoutControlGroup4";
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup4.Size = new System.Drawing.Size(781, 44);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup5.Location = new System.Drawing.Point(0, 44);
            layoutControlGroup5.Name = "layoutControlGroup5";
            layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup5.Size = new System.Drawing.Size(781, 266);
            layoutControlGroup5.TextVisible = false;
            // 
            // RewardHistories
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(932, 552);
            Controls.Add(layoutControl1);
            Controls.Add(ribbonStatusBar);
            Controls.Add(ribbon);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "RewardHistories";
            Ribbon = ribbon;
            StatusBar = ribbonStatusBar;
            Text = "Reward Histories";
            Load += RewardHistories_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlRewardHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewRewardHistories).EndInit();
            ((System.ComponentModel.ISupportInitialize)repDeleteRewardHistory).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControlRewardHistories;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRewardHistories;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repDeleteRewardHistory;
        private DevExpress.XtraGrid.Columns.GridColumn tbBtnRewardHistoryDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraBars.BarButtonItem btnPrint;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colDateClaimed;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colRewardId;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnRefreshItems;
        private DevExpress.XtraEditors.SimpleButton btnPrintItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
    }
}