namespace Pos.Forms.Setting
{
    partial class Backups
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Backups));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            gridControlBackups = new DevExpress.XtraGrid.GridControl();
            gridViewBackups = new DevExpress.XtraGrid.Views.Grid.GridView();
            colName = new DevExpress.XtraGrid.Columns.GridColumn();
            colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            repoBackupDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            colRestore = new DevExpress.XtraGrid.Columns.GridColumn();
            repoRestoreBackup = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            btnBackupDatabase = new DevExpress.XtraEditors.SimpleButton();
            btnRestoreDatabase = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroupBackupsList = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup20 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem64 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup21 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupRestore = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem62 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroupBackup = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
            dxValidationProviderWhatsApp = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            dxValidationProviderMail = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            dxValidationProviderGoogleDrive = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControlBackups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBackups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repoBackupDelete).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repoRestoreBackup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupBackupsList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup20).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRestore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupBackup).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderWhatsApp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderMail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderGoogleDrive).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(searchControl1);
            layoutControl1.Controls.Add(gridControlBackups);
            layoutControl1.Controls.Add(btnBackupDatabase);
            layoutControl1.Controls.Add(btnRestoreDatabase);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridControlBackups;
            resources.ApplyResources(searchControl1, "searchControl1");
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl1.Properties.Appearance.Font");
            searchControl1.Properties.Appearance.Options.UseFont = true;
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridControlBackups;
            searchControl1.Properties.FindDelay = 200;
            searchControl1.StyleController = layoutControl1;
            // 
            // gridControlBackups
            // 
            resources.ApplyResources(gridControlBackups, "gridControlBackups");
            gridControlBackups.MainView = gridViewBackups;
            gridControlBackups.Name = "gridControlBackups";
            gridControlBackups.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repoBackupDelete, repoRestoreBackup });
            gridControlBackups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewBackups });
            // 
            // gridViewBackups
            // 
            gridViewBackups.Appearance.FocusedRow.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.FocusedRow.Font");
            gridViewBackups.Appearance.FocusedRow.Options.UseFont = true;
            gridViewBackups.Appearance.GroupFooter.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.GroupFooter.Font");
            gridViewBackups.Appearance.GroupFooter.Options.UseFont = true;
            gridViewBackups.Appearance.GroupRow.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.GroupRow.Font");
            gridViewBackups.Appearance.GroupRow.Options.UseFont = true;
            gridViewBackups.Appearance.HeaderPanel.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.HeaderPanel.Font");
            gridViewBackups.Appearance.HeaderPanel.Options.UseFont = true;
            gridViewBackups.Appearance.Preview.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.Preview.Font");
            gridViewBackups.Appearance.Preview.Options.UseFont = true;
            gridViewBackups.Appearance.Row.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.Row.Font");
            gridViewBackups.Appearance.Row.Options.UseFont = true;
            gridViewBackups.Appearance.TopNewRow.Font = (System.Drawing.Font)resources.GetObject("gridViewBackups.Appearance.TopNewRow.Font");
            gridViewBackups.Appearance.TopNewRow.Options.UseFont = true;
            gridViewBackups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colName, colPath, colDate, colDelete, colRestore });
            gridViewBackups.GridControl = gridControlBackups;
            gridViewBackups.Name = "gridViewBackups";
            gridViewBackups.OptionsView.ShowGroupPanel = false;
            gridViewBackups.OptionsView.ShowIndicator = false;
            // 
            // colName
            // 
            resources.ApplyResources(colName, "colName");
            colName.FieldName = "Name";
            colName.Name = "colName";
            colName.OptionsColumn.AllowEdit = false;
            colName.OptionsColumn.AllowFocus = false;
            // 
            // colPath
            // 
            resources.ApplyResources(colPath, "colPath");
            colPath.FieldName = "Path";
            colPath.Name = "colPath";
            colPath.OptionsColumn.AllowEdit = false;
            colPath.OptionsColumn.AllowFocus = false;
            // 
            // colDate
            // 
            resources.ApplyResources(colDate, "colDate");
            colDate.FieldName = "Date";
            colDate.Name = "colDate";
            colDate.OptionsColumn.AllowEdit = false;
            colDate.OptionsColumn.AllowFocus = false;
            // 
            // colDelete
            // 
            resources.ApplyResources(colDelete, "colDelete");
            colDelete.ColumnEdit = repoBackupDelete;
            colDelete.FieldName = "Delete";
            colDelete.Name = "colDelete";
            // 
            // repoBackupDelete
            // 
            resources.ApplyResources(repoBackupDelete, "repoBackupDelete");
            repoBackupDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repoBackupDelete.Buttons")) });
            repoBackupDelete.Name = "repoBackupDelete";
            repoBackupDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoBackupDelete.Click += repoBackupDelete_Click;
            // 
            // colRestore
            // 
            resources.ApplyResources(colRestore, "colRestore");
            colRestore.ColumnEdit = repoRestoreBackup;
            colRestore.FieldName = "Restore";
            colRestore.Name = "colRestore";
            // 
            // repoRestoreBackup
            // 
            resources.ApplyResources(repoRestoreBackup, "repoRestoreBackup");
            repoRestoreBackup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("repoRestoreBackup.Buttons")) });
            repoRestoreBackup.Name = "repoRestoreBackup";
            repoRestoreBackup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            repoRestoreBackup.Click += repoRestoreBackup_Click;
            // 
            // btnBackupDatabase
            // 
            btnBackupDatabase.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnBackupDatabase.Appearance.BackColor");
            btnBackupDatabase.Appearance.BorderColor = (System.Drawing.Color)resources.GetObject("btnBackupDatabase.Appearance.BorderColor");
            btnBackupDatabase.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnBackupDatabase.Appearance.Font");
            btnBackupDatabase.Appearance.Options.UseBackColor = true;
            btnBackupDatabase.Appearance.Options.UseBorderColor = true;
            btnBackupDatabase.Appearance.Options.UseFont = true;
            resources.ApplyResources(btnBackupDatabase, "btnBackupDatabase");
            btnBackupDatabase.Name = "btnBackupDatabase";
            btnBackupDatabase.StyleController = layoutControl1;
            btnBackupDatabase.Click += btnBackupDatabase_Click;
            // 
            // btnRestoreDatabase
            // 
            btnRestoreDatabase.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnRestoreDatabase.Appearance.BackColor");
            btnRestoreDatabase.Appearance.BorderColor = (System.Drawing.Color)resources.GetObject("btnRestoreDatabase.Appearance.BorderColor");
            btnRestoreDatabase.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnRestoreDatabase.Appearance.Font");
            btnRestoreDatabase.Appearance.Options.UseBackColor = true;
            btnRestoreDatabase.Appearance.Options.UseBorderColor = true;
            btnRestoreDatabase.Appearance.Options.UseFont = true;
            resources.ApplyResources(btnRestoreDatabase, "btnRestoreDatabase");
            btnRestoreDatabase.Name = "btnRestoreDatabase";
            btnRestoreDatabase.StyleController = layoutControl1;
            btnRestoreDatabase.Click += btnRestoreDatabase_Click;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupBackupsList, layoutControlGroupRestore, layoutControlGroupBackup });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(1201, 719);
            Root.TextVisible = false;
            // 
            // layoutControlGroupBackupsList
            // 
            layoutControlGroupBackupsList.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroupBackupsList.AppearanceGroup.Font");
            layoutControlGroupBackupsList.AppearanceGroup.Options.UseFont = true;
            layoutControlGroupBackupsList.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroupBackupsList.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup20, layoutControlGroup21 });
            layoutControlGroupBackupsList.Location = new System.Drawing.Point(0, 0);
            layoutControlGroupBackupsList.Name = "layoutControlGroupBackupsList";
            layoutControlGroupBackupsList.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroupBackupsList.Size = new System.Drawing.Size(1191, 609);
            resources.ApplyResources(layoutControlGroupBackupsList, "layoutControlGroupBackupsList");
            // 
            // layoutControlGroup20
            // 
            layoutControlGroup20.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup20.AppearanceGroup.Font");
            layoutControlGroup20.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup20.CaptionImageOptions.SvgImage = Properties.Resources.search;
            layoutControlGroup20.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroup20.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem64 });
            layoutControlGroup20.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup20.Name = "layoutControlGroup20";
            layoutControlGroup20.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup20.Size = new System.Drawing.Size(1175, 46);
            resources.ApplyResources(layoutControlGroup20, "layoutControlGroup20");
            layoutControlGroup20.TextVisible = false;
            // 
            // layoutControlItem64
            // 
            layoutControlItem64.Control = searchControl1;
            layoutControlItem64.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            layoutControlItem64.ImageOptions.SvgImage = Properties.Resources.search;
            layoutControlItem64.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlItem64.Location = new System.Drawing.Point(0, 0);
            layoutControlItem64.Name = "layoutControlItem64";
            layoutControlItem64.Size = new System.Drawing.Size(1159, 30);
            resources.ApplyResources(layoutControlItem64, "layoutControlItem64");
            layoutControlItem64.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem64.TextSize = new System.Drawing.Size(33, 25);
            layoutControlItem64.TextToControlDistance = 5;
            // 
            // layoutControlGroup21
            // 
            layoutControlGroup21.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem63 });
            layoutControlGroup21.Location = new System.Drawing.Point(0, 46);
            layoutControlGroup21.Name = "layoutControlGroup21";
            layoutControlGroup21.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup21.Size = new System.Drawing.Size(1175, 526);
            layoutControlGroup21.TextVisible = false;
            // 
            // layoutControlItem63
            // 
            layoutControlItem63.Control = gridControlBackups;
            layoutControlItem63.Location = new System.Drawing.Point(0, 0);
            layoutControlItem63.Name = "layoutControlItem63";
            layoutControlItem63.Size = new System.Drawing.Size(1159, 510);
            layoutControlItem63.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem63.TextVisible = false;
            // 
            // layoutControlGroupRestore
            // 
            layoutControlGroupRestore.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroupRestore.AppearanceGroup.Font");
            layoutControlGroupRestore.AppearanceGroup.Options.UseFont = true;
            layoutControlGroupRestore.CaptionImageOptions.SvgImage = Properties.Resources.Sync;
            layoutControlGroupRestore.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroupRestore.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem62 });
            layoutControlGroupRestore.Location = new System.Drawing.Point(584, 609);
            layoutControlGroupRestore.Name = "layoutControlGroupRestore";
            layoutControlGroupRestore.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroupRestore.Size = new System.Drawing.Size(607, 100);
            resources.ApplyResources(layoutControlGroupRestore, "layoutControlGroupRestore");
            // 
            // layoutControlItem62
            // 
            layoutControlItem62.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem62.AppearanceItemCaption.Font");
            layoutControlItem62.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem62.Control = btnRestoreDatabase;
            layoutControlItem62.Location = new System.Drawing.Point(0, 0);
            layoutControlItem62.MinSize = new System.Drawing.Size(98, 26);
            layoutControlItem62.Name = "layoutControlItem62";
            layoutControlItem62.Size = new System.Drawing.Size(591, 57);
            layoutControlItem62.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem62.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem62.TextVisible = false;
            // 
            // layoutControlGroupBackup
            // 
            layoutControlGroupBackup.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroupBackup.AppearanceGroup.Font");
            layoutControlGroupBackup.AppearanceGroup.Options.UseFont = true;
            layoutControlGroupBackup.CaptionImageOptions.SvgImage = Properties.Resources.data_backup;
            layoutControlGroupBackup.CaptionImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            layoutControlGroupBackup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem61 });
            layoutControlGroupBackup.Location = new System.Drawing.Point(0, 609);
            layoutControlGroupBackup.Name = "layoutControlGroupBackup";
            layoutControlGroupBackup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroupBackup.Size = new System.Drawing.Size(584, 100);
            resources.ApplyResources(layoutControlGroupBackup, "layoutControlGroupBackup");
            // 
            // layoutControlItem61
            // 
            layoutControlItem61.Control = btnBackupDatabase;
            layoutControlItem61.Location = new System.Drawing.Point(0, 0);
            layoutControlItem61.MinSize = new System.Drawing.Size(94, 26);
            layoutControlItem61.Name = "layoutControlItem61";
            layoutControlItem61.Size = new System.Drawing.Size(568, 57);
            layoutControlItem61.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem61.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem61.TextVisible = false;
            // 
            // Backups
            // 
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("Backups.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            IconOptions.SvgImage = Properties.Resources.data_backup;
            Name = "Backups";
            Load += Backups_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControlBackups).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridViewBackups).EndInit();
            ((System.ComponentModel.ISupportInitialize)repoBackupDelete).EndInit();
            ((System.ComponentModel.ISupportInitialize)repoRestoreBackup).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupBackupsList).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup20).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem64).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup21).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem63).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupRestore).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem62).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupBackup).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem61).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderWhatsApp).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderMail).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderGoogleDrive).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProviderWhatsApp;
		private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProviderMail;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProviderGoogleDrive;
        private DevExpress.XtraEditors.SimpleButton btnBackupDatabase;
        private DevExpress.XtraEditors.SimpleButton btnRestoreDatabase;
        private DevExpress.XtraGrid.GridControl gridControlBackups;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBackups;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBackupDelete;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colRestore;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoRestoreBackup;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupBackupsList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem64;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem63;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRestore;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem62;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupBackup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem61;
    }
}