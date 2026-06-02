namespace Pos.Forms.Register
{
    partial class OpenRegister
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenRegister));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            btnOpenRegister = new DevExpress.XtraEditors.SimpleButton();
            txtBusinessLocationId = new DevExpress.XtraEditors.LookUpEdit();
            btnQuickBussLocation = new DevExpress.XtraEditors.SimpleButton();
            txtCashInHand = new DevExpress.XtraEditors.SpinEdit();
            txtRegisterId = new DevExpress.XtraEditors.LookUpEdit();
            btnQuickRegister = new DevExpress.XtraEditors.SimpleButton();
            txtComment = new DevExpress.XtraEditors.MemoEdit();
            btnClose = new DevExpress.XtraEditors.SimpleButton();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItemRegister = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItemBusinessLocation = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            dxValidationProviderRegister = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtBusinessLocationId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCashInHand.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtRegisterId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtComment.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemRegister).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemBusinessLocation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderRegister).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(btnOpenRegister);
            layoutControl1.Controls.Add(txtBusinessLocationId);
            layoutControl1.Controls.Add(btnQuickBussLocation);
            layoutControl1.Controls.Add(txtCashInHand);
            layoutControl1.Controls.Add(txtRegisterId);
            layoutControl1.Controls.Add(btnQuickRegister);
            layoutControl1.Controls.Add(txtComment);
            layoutControl1.Controls.Add(btnClose);
            layoutControl1.Controls.Add(btnCloseFrm);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            layoutControl1.Root = Root;
            // 
            // btnOpenRegister
            // 
            btnOpenRegister.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnOpenRegister.Appearance.BackColor");
            btnOpenRegister.Appearance.BorderColor = (System.Drawing.Color)resources.GetObject("btnOpenRegister.Appearance.BorderColor");
            btnOpenRegister.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnOpenRegister.Appearance.Font");
            btnOpenRegister.Appearance.Options.UseBackColor = true;
            btnOpenRegister.Appearance.Options.UseBorderColor = true;
            btnOpenRegister.Appearance.Options.UseFont = true;
            btnOpenRegister.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnOpenRegister.ImageOptions.SvgImage = Properties.Resources.openWhiteSign;
            btnOpenRegister.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnOpenRegister, "btnOpenRegister");
            btnOpenRegister.Name = "btnOpenRegister";
            btnOpenRegister.StyleController = layoutControl1;
            btnOpenRegister.Click += btnOpenRegister_Click;
            // 
            // txtBusinessLocationId
            // 
            resources.ApplyResources(txtBusinessLocationId, "txtBusinessLocationId");
            txtBusinessLocationId.Name = "txtBusinessLocationId";
            txtBusinessLocationId.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtBusinessLocationId.Properties.Appearance.Font");
            txtBusinessLocationId.Properties.Appearance.Options.UseFont = true;
            txtBusinessLocationId.Properties.AutoHeight = (bool)resources.GetObject("txtBusinessLocationId.Properties.AutoHeight");
            txtBusinessLocationId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("txtBusinessLocationId.Properties.Buttons")) });
            txtBusinessLocationId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("txtBusinessLocationId.Properties.Columns"), resources.GetString("txtBusinessLocationId.Properties.Columns1")) });
            txtBusinessLocationId.Properties.NullText = resources.GetString("txtBusinessLocationId.Properties.NullText");
            txtBusinessLocationId.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            txtBusinessLocationId.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            txtBusinessLocationId.StyleController = layoutControl1;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            dxValidationProviderRegister.SetValidationRule(txtBusinessLocationId, conditionValidationRule1);
            // 
            // btnQuickBussLocation
            // 
            btnQuickBussLocation.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnQuickBussLocation.ImageOptions.SvgImage = Properties.Resources.add_plus;
            btnQuickBussLocation.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnQuickBussLocation, "btnQuickBussLocation");
            btnQuickBussLocation.Name = "btnQuickBussLocation";
            btnQuickBussLocation.StyleController = layoutControl1;
            btnQuickBussLocation.Click += btnQuickBussLocation_Click;
            // 
            // txtCashInHand
            // 
            resources.ApplyResources(txtCashInHand, "txtCashInHand");
            txtCashInHand.Name = "txtCashInHand";
            txtCashInHand.Properties.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("txtCashInHand.Properties.Appearance.BackColor");
            txtCashInHand.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtCashInHand.Properties.Appearance.Font");
            txtCashInHand.Properties.Appearance.Options.UseBackColor = true;
            txtCashInHand.Properties.Appearance.Options.UseFont = true;
            txtCashInHand.Properties.AutoHeight = (bool)resources.GetObject("txtCashInHand.Properties.AutoHeight");
            txtCashInHand.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("txtCashInHand.Properties.Buttons")) });
            txtCashInHand.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            txtCashInHand.StyleController = layoutControl1;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            dxValidationProviderRegister.SetValidationRule(txtCashInHand, conditionValidationRule2);
            // 
            // txtRegisterId
            // 
            resources.ApplyResources(txtRegisterId, "txtRegisterId");
            txtRegisterId.Name = "txtRegisterId";
            txtRegisterId.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtRegisterId.Properties.Appearance.Font");
            txtRegisterId.Properties.Appearance.Options.UseFont = true;
            txtRegisterId.Properties.AutoHeight = (bool)resources.GetObject("txtRegisterId.Properties.AutoHeight");
            txtRegisterId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton((DevExpress.XtraEditors.Controls.ButtonPredefines)resources.GetObject("txtRegisterId.Properties.Buttons")) });
            txtRegisterId.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] { new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("txtRegisterId.Properties.Columns"), resources.GetString("txtRegisterId.Properties.Columns1")) });
            txtRegisterId.Properties.NullText = resources.GetString("txtRegisterId.Properties.NullText");
            txtRegisterId.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            txtRegisterId.Properties.PopupSizeable = false;
            txtRegisterId.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            txtRegisterId.StyleController = layoutControl1;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            dxValidationProviderRegister.SetValidationRule(txtRegisterId, conditionValidationRule3);
            // 
            // btnQuickRegister
            // 
            btnQuickRegister.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnQuickRegister.ImageOptions.SvgImage = Properties.Resources.add_plus;
            btnQuickRegister.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnQuickRegister, "btnQuickRegister");
            btnQuickRegister.Name = "btnQuickRegister";
            btnQuickRegister.StyleController = layoutControl1;
            btnQuickRegister.Click += btnQuickRegister_Click;
            // 
            // txtComment
            // 
            resources.ApplyResources(txtComment, "txtComment");
            txtComment.Name = "txtComment";
            txtComment.Properties.Appearance.Font = (System.Drawing.Font)resources.GetObject("txtComment.Properties.Appearance.Font");
            txtComment.Properties.Appearance.Options.UseFont = true;
            txtComment.StyleController = layoutControl1;
            // 
            // btnClose
            // 
            btnClose.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnClose.ImageOptions.SvgImage = Properties.Resources.close_sign;
            btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            resources.ApplyResources(btnClose, "btnClose");
            btnClose.Name = "btnClose";
            btnClose.StyleController = layoutControl1;
            btnClose.Click += btnClose_Click;
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
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlGroup2, layoutControlItem1, layoutControlItem2 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(455, 431);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.AppearanceGroup.BackColor = (System.Drawing.Color)resources.GetObject("layoutControlGroup1.AppearanceGroup.BackColor");
            layoutControlGroup1.AppearanceGroup.BorderColor = (System.Drawing.Color)resources.GetObject("layoutControlGroup1.AppearanceGroup.BorderColor");
            layoutControlGroup1.AppearanceGroup.Font = (System.Drawing.Font)resources.GetObject("layoutControlGroup1.AppearanceGroup.Font");
            layoutControlGroup1.AppearanceGroup.Options.UseBackColor = true;
            layoutControlGroup1.AppearanceGroup.Options.UseBorderColor = true;
            layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItemRegister, layoutControlItemBusinessLocation, layoutControlItem7, layoutControlItem5, layoutControlItem3, layoutControlItem4 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 54);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(445, 306);
            resources.ApplyResources(layoutControlGroup1, "layoutControlGroup1");
            // 
            // layoutControlItemRegister
            // 
            layoutControlItemRegister.Control = btnQuickRegister;
            layoutControlItemRegister.Location = new System.Drawing.Point(385, 39);
            layoutControlItemRegister.MinSize = new System.Drawing.Size(40, 38);
            layoutControlItemRegister.Name = "layoutControlItemRegister";
            layoutControlItemRegister.Size = new System.Drawing.Size(44, 39);
            layoutControlItemRegister.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItemRegister.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItemRegister.TextVisible = false;
            // 
            // layoutControlItemBusinessLocation
            // 
            layoutControlItemBusinessLocation.Control = btnQuickBussLocation;
            layoutControlItemBusinessLocation.Location = new System.Drawing.Point(385, 78);
            layoutControlItemBusinessLocation.MinSize = new System.Drawing.Size(40, 38);
            layoutControlItemBusinessLocation.Name = "layoutControlItemBusinessLocation";
            layoutControlItemBusinessLocation.Size = new System.Drawing.Size(44, 39);
            layoutControlItemBusinessLocation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItemBusinessLocation.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItemBusinessLocation.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem7.AppearanceItemCaption.Font");
            layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem7.Control = txtComment;
            layoutControlItem7.Location = new System.Drawing.Point(0, 117);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(429, 147);
            resources.ApplyResources(layoutControlItem7, "layoutControlItem7");
            layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem7.TextSize = new System.Drawing.Size(144, 19);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem5.AppearanceItemCaption.Font");
            layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem5.Control = txtBusinessLocationId;
            layoutControlItem5.Location = new System.Drawing.Point(0, 78);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(385, 39);
            resources.ApplyResources(layoutControlItem5, "layoutControlItem5");
            layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem5.TextSize = new System.Drawing.Size(144, 19);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font");
            layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem3.Control = txtRegisterId;
            layoutControlItem3.Location = new System.Drawing.Point(0, 39);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(385, 39);
            resources.ApplyResources(layoutControlItem3, "layoutControlItem3");
            layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem3.TextSize = new System.Drawing.Size(144, 19);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.AppearanceItemCaption.BackColor = (System.Drawing.Color)resources.GetObject("layoutControlItem4.AppearanceItemCaption.BackColor");
            layoutControlItem4.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font");
            layoutControlItem4.AppearanceItemCaption.Options.UseBackColor = true;
            layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            layoutControlItem4.Control = txtCashInHand;
            layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(429, 39);
            resources.ApplyResources(layoutControlItem4, "layoutControlItem4");
            layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem4.TextSize = new System.Drawing.Size(144, 19);
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem9, emptySpaceItem3, simpleLabelItem1 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(445, 54);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = btnCloseFrm;
            layoutControlItem9.Location = new System.Drawing.Point(380, 0);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(49, 38);
            layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.AllowHotTrack = false;
            emptySpaceItem3.Location = new System.Drawing.Point(159, 0);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new System.Drawing.Size(221, 38);
            emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
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
            simpleLabelItem1.TextSize = new System.Drawing.Size(144, 19);
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = btnClose;
            layoutControlItem1.Location = new System.Drawing.Point(0, 360);
            layoutControlItem1.MinSize = new System.Drawing.Size(40, 38);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(222, 61);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = btnOpenRegister;
            layoutControlItem2.Location = new System.Drawing.Point(222, 360);
            layoutControlItem2.MinSize = new System.Drawing.Size(40, 38);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem2.Size = new System.Drawing.Size(223, 61);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 
            // OpenRegister
            // 
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("OpenRegister.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.SvgImage = Properties.Resources.cash_counter_colored;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "OpenRegister";
            FormClosed += OpenRegister_FormClosed;
            Load += OpenRegister_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtBusinessLocationId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCashInHand.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtRegisterId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtComment.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemRegister).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItemBusinessLocation).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderRegister).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnOpenRegister;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProviderRegister;
        private DevExpress.XtraEditors.LookUpEdit txtBusinessLocationId;
        private DevExpress.XtraEditors.SimpleButton btnQuickBussLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBusinessLocation;
        private DevExpress.XtraEditors.SpinEdit txtCashInHand;
        private DevExpress.XtraEditors.LookUpEdit txtRegisterId;
        private DevExpress.XtraEditors.SimpleButton btnQuickRegister;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemRegister;
        private DevExpress.XtraEditors.MemoEdit txtComment;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}