namespace Pos.Forms.LoyaltyForms
{
    partial class LoyaltyCardTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoyaltyCardTypeForm));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            btnAdd = new DevExpress.XtraBars.BarButtonItem();
            btnEdit = new DevExpress.XtraBars.BarButtonItem();
            btnRefreshCountry = new DevExpress.XtraBars.BarButtonItem();
            btnDelete = new DevExpress.XtraBars.BarButtonItem();
            btnClose = new DevExpress.XtraBars.BarButtonItem();
            barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            Coun = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            searchControl3 = new DevExpress.XtraEditors.SearchControl();
            gridControl = new DevExpress.XtraGrid.GridControl();
            gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            txtName = new DevExpress.XtraEditors.TextEdit();
            txtPointsPerCurrency = new DevExpress.XtraEditors.TextEdit();
            txtCurrencyPerPoint = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            CountryName = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPointsPerCurrency.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCurrencyPerPoint.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CountryName).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProvider1).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            resources.ApplyResources(ribbon, "ribbon");
            ribbon.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            ribbon.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.ExpandCollapseItem.ImageOptions.ImageIndex = (int)resources.GetObject("ribbon.ExpandCollapseItem.ImageOptions.ImageIndex");
            ribbon.ExpandCollapseItem.ImageOptions.LargeImageIndex = (int)resources.GetObject("ribbon.ExpandCollapseItem.ImageOptions.LargeImageIndex");
            ribbon.ExpandCollapseItem.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ribbon.ExpandCollapseItem.ImageOptions.SvgImage");
            ribbon.ExpandCollapseItem.SearchTags = resources.GetString("ribbon.ExpandCollapseItem.SearchTags");
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, btnAdd, btnEdit, btnRefreshCountry, btnDelete, btnClose, barButtonItem1, barButtonGroup1, btnRefresh });
            ribbon.MaxItemId = 24;
            ribbon.Name = "ribbon";
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
            ribbon.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            ribbon.ShowQatLocationSelector = false;
            ribbon.ShowToolbarCustomizeItem = false;
            ribbon.StatusBar = ribbonStatusBar;
            ribbon.Toolbar.ShowCustomizeItem = false;
            ribbon.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // btnAdd
            // 
            resources.ApplyResources(btnAdd, "btnAdd");
            btnAdd.Id = 1;
            btnAdd.ImageOptions.ImageIndex = (int)resources.GetObject("btnAdd.ImageOptions.ImageIndex");
            btnAdd.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnAdd.ImageOptions.LargeImageIndex");
            btnAdd.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnAdd.ImageOptions.SvgImage");
            btnAdd.Name = "btnAdd";
            btnAdd.ItemClick += BtnAdd_ItemClick;
            // 
            // btnEdit
            // 
            resources.ApplyResources(btnEdit, "btnEdit");
            btnEdit.Id = 2;
            btnEdit.ImageOptions.ImageIndex = (int)resources.GetObject("btnEdit.ImageOptions.ImageIndex");
            btnEdit.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnEdit.ImageOptions.LargeImageIndex");
            btnEdit.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnEdit.ImageOptions.SvgImage");
            btnEdit.Name = "btnEdit";
            btnEdit.ItemClick += BtnEdit_ItemClick;
            // 
            // btnRefreshCountry
            // 
            resources.ApplyResources(btnRefreshCountry, "btnRefreshCountry");
            btnRefreshCountry.Id = 3;
            btnRefreshCountry.ImageOptions.ImageIndex = (int)resources.GetObject("btnRefreshCountry.ImageOptions.ImageIndex");
            btnRefreshCountry.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnRefreshCountry.ImageOptions.LargeImageIndex");
            btnRefreshCountry.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnRefreshCountry.ImageOptions.SvgImage");
            btnRefreshCountry.Name = "btnRefreshCountry";
            // 
            // btnDelete
            // 
            resources.ApplyResources(btnDelete, "btnDelete");
            btnDelete.Id = 4;
            btnDelete.ImageOptions.ImageIndex = (int)resources.GetObject("btnDelete.ImageOptions.ImageIndex");
            btnDelete.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnDelete.ImageOptions.LargeImageIndex");
            btnDelete.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnDelete.ImageOptions.SvgImage");
            btnDelete.Name = "btnDelete";
            btnDelete.ItemClick += BtnDelete_ItemClick;
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            btnClose.Id = 5;
            btnClose.ImageOptions.ImageIndex = (int)resources.GetObject("btnClose.ImageOptions.ImageIndex");
            btnClose.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnClose.ImageOptions.LargeImageIndex");
            btnClose.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnClose.ImageOptions.SvgImage");
            btnClose.Name = "btnClose";
            btnClose.ItemClick += BtnClose_ItemClick;
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(barButtonItem1, "barButtonItem1");
            barButtonItem1.Id = 6;
            barButtonItem1.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonItem1.ImageOptions.ImageIndex");
            barButtonItem1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonItem1.ImageOptions.LargeImageIndex");
            barButtonItem1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonItem1.ImageOptions.SvgImage");
            barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonGroup1
            // 
            resources.ApplyResources(barButtonGroup1, "barButtonGroup1");
            barButtonGroup1.Id = 7;
            barButtonGroup1.ImageOptions.ImageIndex = (int)resources.GetObject("barButtonGroup1.ImageOptions.ImageIndex");
            barButtonGroup1.ImageOptions.LargeImageIndex = (int)resources.GetObject("barButtonGroup1.ImageOptions.LargeImageIndex");
            barButtonGroup1.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("barButtonGroup1.ImageOptions.SvgImage");
            barButtonGroup1.Name = "barButtonGroup1";
            // 
            // btnRefresh
            // 
            resources.ApplyResources(btnRefresh, "btnRefresh");
            btnRefresh.Id = 11;
            btnRefresh.ImageOptions.ImageIndex = (int)resources.GetObject("btnRefresh.ImageOptions.ImageIndex");
            btnRefresh.ImageOptions.LargeImageIndex = (int)resources.GetObject("btnRefresh.ImageOptions.LargeImageIndex");
            btnRefresh.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnRefresh.ImageOptions.SvgImage");
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ItemClick += BtnRefresh_ItemClick;
            // 
            // ribbonPage1
            // 
            resources.ApplyResources(ribbonPage1, "ribbonPage1");
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { Coun, ribbonPageGroup2 });
            ribbonPage1.Name = "ribbonPage1";
            // 
            // Coun
            // 
            resources.ApplyResources(Coun, "Coun");
            Coun.ItemLinks.Add(btnAdd);
            Coun.ItemLinks.Add(btnEdit);
            Coun.ItemLinks.Add(btnDelete);
            Coun.ItemLinks.Add(btnRefresh);
            Coun.Name = "Coun";
            // 
            // ribbonPageGroup2
            // 
            resources.ApplyResources(ribbonPageGroup2, "ribbonPageGroup2");
            ribbonPageGroup2.ItemLinks.Add(btnClose);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // ribbonStatusBar
            // 
            resources.ApplyResources(ribbonStatusBar, "ribbonStatusBar");
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            // 
            // layoutControl1
            // 
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Controls.Add(searchControl3);
            layoutControl1.Controls.Add(gridControl);
            layoutControl1.Controls.Add(txtName);
            layoutControl1.Controls.Add(txtPointsPerCurrency);
            layoutControl1.Controls.Add(txtCurrencyPerPoint);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // searchControl3
            // 
            resources.ApplyResources(searchControl3, "searchControl3");
            searchControl3.Client = gridControl;
            searchControl3.MenuManager = ribbon;
            searchControl3.Name = "searchControl3";
            searchControl3.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("searchControl3.Properties.Appearance.Font");
            searchControl3.Properties.Appearance.Options.UseFont = true;
            searchControl3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl3.Properties.Client = gridControl;
            searchControl3.Properties.FindDelay = 300;
            searchControl3.StyleController = layoutControl1;
            // 
            // gridControl
            // 
            resources.ApplyResources(gridControl, "gridControl");
            gridControl.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl.EmbeddedNavigator.AccessibleDescription");
            gridControl.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl.EmbeddedNavigator.AccessibleName");
            gridControl.EmbeddedNavigator.AllowHtmlTextInToolTip = (DevExpress.Utils.DefaultBoolean)resources.GetObject("gridControl.EmbeddedNavigator.AllowHtmlTextInToolTip");
            gridControl.EmbeddedNavigator.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("gridControl.EmbeddedNavigator.Anchor");
            gridControl.EmbeddedNavigator.AutoSize = (bool)resources.GetObject("gridControl.EmbeddedNavigator.AutoSize");
            gridControl.EmbeddedNavigator.BackgroundImage = (System.Drawing.Image)resources.GetObject("gridControl.EmbeddedNavigator.BackgroundImage");
            gridControl.EmbeddedNavigator.BackgroundImageLayout = (System.Windows.Forms.ImageLayout)resources.GetObject("gridControl.EmbeddedNavigator.BackgroundImageLayout");
            gridControl.EmbeddedNavigator.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("gridControl.EmbeddedNavigator.ImeMode");
            gridControl.EmbeddedNavigator.Margin = (System.Windows.Forms.Padding)resources.GetObject("gridControl.EmbeddedNavigator.Margin");
            gridControl.EmbeddedNavigator.MaximumSize = (System.Drawing.Size)resources.GetObject("gridControl.EmbeddedNavigator.MaximumSize");
            gridControl.EmbeddedNavigator.TextLocation = (DevExpress.XtraEditors.NavigatorButtonsTextLocation)resources.GetObject("gridControl.EmbeddedNavigator.TextLocation");
            gridControl.EmbeddedNavigator.ToolTip = resources.GetString("gridControl.EmbeddedNavigator.ToolTip");
            gridControl.EmbeddedNavigator.ToolTipIconType = (DevExpress.Utils.ToolTipIconType)resources.GetObject("gridControl.EmbeddedNavigator.ToolTipIconType");
            gridControl.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl.EmbeddedNavigator.ToolTipTitle");
            gridControl.MainView = gridView;
            gridControl.MenuManager = ribbon;
            gridControl.Name = "gridControl";
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView });
            // 
            // gridView
            // 
            resources.ApplyResources(gridView, "gridView");
            gridView.Appearance.EvenRow.BackColor = (System.Drawing.Color)resources.GetObject("gridView.Appearance.EvenRow.BackColor");
            gridView.Appearance.EvenRow.Options.UseBackColor = true;
            gridView.ColumnPanelRowHeight = 28;
            gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridColumn1, gridColumn2 });
            gridView.GridControl = gridControl;
            gridView.Name = "gridView";
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsView.EnableAppearanceEvenRow = true;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.OptionsView.ShowIndicator = false;
            gridView.RowHeight = 30;
            gridView.RowClick += GrvCategory_RowClick;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(gridColumn1, "gridColumn1");
            gridColumn1.FieldName = "Id";
            gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(gridColumn2, "gridColumn2");
            gridColumn2.FieldName = "Name";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem() });
            // 
            // txtName
            // 
            resources.ApplyResources(txtName, "txtName");
            txtName.MenuManager = ribbon;
            txtName.Name = "txtName";
            txtName.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtName.Properties.Appearance.Font");
            txtName.Properties.Appearance.Options.UseFont = true;
            txtName.Properties.AutoHeight = (bool)resources.GetObject("txtName.Properties.AutoHeight");
            txtName.StyleController = layoutControl1;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "Name is required.";
            dxValidationProvider1.SetValidationRule(txtName, conditionValidationRule4);
            // 
            // txtPointsPerCurrency
            // 
            resources.ApplyResources(txtPointsPerCurrency, "txtPointsPerCurrency");
            txtPointsPerCurrency.MenuManager = ribbon;
            txtPointsPerCurrency.Name = "txtPointsPerCurrency";
            txtPointsPerCurrency.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtPointsPerCurrency.Properties.Appearance.Font");
            txtPointsPerCurrency.Properties.Appearance.Options.UseFont = true;
            txtPointsPerCurrency.Properties.AutoHeight = (bool)resources.GetObject("txtPointsPerCurrency.Properties.AutoHeight");
            txtPointsPerCurrency.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtPointsPerCurrency.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            txtPointsPerCurrency.Properties.MaskSettings.Set("mask", "d");
            txtPointsPerCurrency.StyleController = layoutControl1;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            dxValidationProvider1.SetValidationRule(txtPointsPerCurrency, conditionValidationRule1);
            // 
            // txtCurrencyPerPoint
            // 
            resources.ApplyResources(txtCurrencyPerPoint, "txtCurrencyPerPoint");
            txtCurrencyPerPoint.MenuManager = ribbon;
            txtCurrencyPerPoint.Name = "txtCurrencyPerPoint";
            txtCurrencyPerPoint.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtCurrencyPerPoint.Properties.Appearance.Font");
            txtCurrencyPerPoint.Properties.Appearance.Options.UseFont = true;
            txtCurrencyPerPoint.Properties.AutoHeight = (bool)resources.GetObject("txtCurrencyPerPoint.Properties.AutoHeight");
            txtCurrencyPerPoint.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtCurrencyPerPoint.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            txtCurrencyPerPoint.Properties.MaskSettings.Set("mask", "d");
            txtCurrencyPerPoint.StyleController = layoutControl1;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            dxValidationProvider1.SetValidationRule(txtCurrencyPerPoint, conditionValidationRule2);
            // 
            // Root
            // 
            resources.ApplyResources(Root, "Root");
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlGroup1, layoutControlItem4 });
            Root.Name = "Root";
            Root.Size = new System.Drawing.Size(378, 378);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            resources.ApplyResources(layoutControlItem1, "layoutControlItem1");
            layoutControlItem1.Control = gridControl;
            layoutControlItem1.Location = new System.Drawing.Point(0, 173);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(358, 185);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            layoutControlGroup1.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceGroup.Font");
            layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup1.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceItemCaption.Font");
            layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroup1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { CountryName, layoutControlItem2, layoutControlItem3 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Size = new System.Drawing.Size(358, 145);
            // 
            // CountryName
            // 
            resources.ApplyResources(CountryName, "CountryName");
            CountryName.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("CountryName.AppearanceItemCaption.Font");
            CountryName.AppearanceItemCaption.Options.UseFont = true;
            CountryName.Control = txtName;
            CountryName.Location = new System.Drawing.Point(0, 0);
            CountryName.Name = "CountryName";
            CountryName.Size = new System.Drawing.Size(334, 34);
            CountryName.TextSize = new System.Drawing.Size(92, 16);
            // 
            // layoutControlItem2
            // 
            resources.ApplyResources(layoutControlItem2, "layoutControlItem2");
            layoutControlItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem2.AppearanceItemCaption.Font");
            layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem2.Control = txtPointsPerCurrency;
            layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(334, 34);
            layoutControlItem2.TextSize = new System.Drawing.Size(92, 16);
            // 
            // layoutControlItem3
            // 
            resources.ApplyResources(layoutControlItem3, "layoutControlItem3");
            layoutControlItem3.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font");
            layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem3.Control = txtCurrencyPerPoint;
            layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(334, 34);
            layoutControlItem3.TextSize = new System.Drawing.Size(92, 16);
            // 
            // layoutControlItem4
            // 
            resources.ApplyResources(layoutControlItem4, "layoutControlItem4");
            layoutControlItem4.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font");
            layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem4.Control = searchControl3;
            layoutControlItem4.Location = new System.Drawing.Point(0, 145);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(358, 28);
            layoutControlItem4.TextSize = new System.Drawing.Size(92, 16);
            // 
            // LoyaltyCardTypeForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            Controls.Add(ribbonStatusBar);
            Controls.Add(ribbon);
            IconOptions.SvgImage = Properties.Resources.money_bag;
            Name = "LoyaltyCardTypeForm";
            Ribbon = ribbon;
            StatusBar = ribbonStatusBar;
            Load += GlassTypeForm_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPointsPerCurrency.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCurrencyPerPoint.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)CountryName).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup Coun;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnRefreshCountry;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlItem CountryName;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private DevExpress.XtraEditors.SearchControl searchControl3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtPointsPerCurrency;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtCurrencyPerPoint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}