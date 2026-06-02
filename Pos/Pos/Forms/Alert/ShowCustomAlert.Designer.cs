namespace Pos.Forms.Alert
{
    partial class ShowCustomAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowCustomAlert));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            alertIcon = new DevExpress.XtraEditors.PictureEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            simpleLabelItemMessage = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)alertIcon.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItemMessage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Controls.Add(alertIcon);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(680, 256, 650, 400);
            layoutControl1.Root = Root;
            // 
            // alertIcon
            // 
            resources.ApplyResources(alertIcon, "alertIcon");
            alertIcon.EditValue = Properties.Resources.spinner200px200px;
            alertIcon.Name = "alertIcon";
            alertIcon.Properties.Appearance.ForeColor = (System.Drawing.Color)resources.GetObject("alertIcon.Properties.Appearance.ForeColor");
            alertIcon.Properties.Appearance.Options.UseForeColor = true;
            alertIcon.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            alertIcon.Properties.PictureAlignment = System.Drawing.ContentAlignment.BottomCenter;
            alertIcon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            alertIcon.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            alertIcon.StyleController = layoutControl1;
            // 
            // Root
            // 
            resources.ApplyResources(Root, "Root");
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { simpleLabelItemMessage, layoutControlItem1 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            Root.Size = new System.Drawing.Size(436, 208);
            Root.TextVisible = false;
            // 
            // simpleLabelItemMessage
            // 
            resources.ApplyResources(simpleLabelItemMessage, "simpleLabelItemMessage");
            simpleLabelItemMessage.AllowHotTrack = false;
            simpleLabelItemMessage.AppearanceItemCaption.BackColor = (System.Drawing.Color)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaption.BackColor");
            simpleLabelItemMessage.AppearanceItemCaption.BorderColor = (System.Drawing.Color)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaption.BorderColor");
            simpleLabelItemMessage.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaption.Font");
            simpleLabelItemMessage.AppearanceItemCaption.ForeColor = (System.Drawing.Color)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaption.ForeColor");
            simpleLabelItemMessage.AppearanceItemCaption.Options.UseBackColor = true;
            simpleLabelItemMessage.AppearanceItemCaption.Options.UseBorderColor = true;
            simpleLabelItemMessage.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItemMessage.AppearanceItemCaption.Options.UseForeColor = true;
            simpleLabelItemMessage.AppearanceItemCaption.Options.UseTextOptions = true;
            simpleLabelItemMessage.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            simpleLabelItemMessage.AppearanceItemCaptionDisabled.BackColor = (System.Drawing.Color)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaptionDisabled.BackColor");
            simpleLabelItemMessage.AppearanceItemCaptionDisabled.BorderColor = (System.Drawing.Color)resources.GetObject("simpleLabelItemMessage.AppearanceItemCaptionDisabled.BorderColor");
            simpleLabelItemMessage.AppearanceItemCaptionDisabled.Options.UseBackColor = true;
            simpleLabelItemMessage.AppearanceItemCaptionDisabled.Options.UseBorderColor = true;
            simpleLabelItemMessage.Location = new System.Drawing.Point(0, 126);
            simpleLabelItemMessage.MinSize = new System.Drawing.Size(111, 17);
            simpleLabelItemMessage.Name = "simpleLabelItemMessage";
            simpleLabelItemMessage.Size = new System.Drawing.Size(436, 82);
            simpleLabelItemMessage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            simpleLabelItemMessage.TextSize = new System.Drawing.Size(222, 23);
            // 
            // layoutControlItem1
            // 
            resources.ApplyResources(layoutControlItem1, "layoutControlItem1");
            layoutControlItem1.Control = alertIcon;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(436, 126);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // ShowCustomAlert
            // 
            resources.ApplyResources(this, "$this");
            Appearance.BackColor = (System.Drawing.Color)resources.GetObject("ShowCustomAlert.Appearance.BackColor");
            Appearance.Options.UseBackColor = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "ShowCustomAlert";
            Load += ShowCustomAlert_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)alertIcon.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItemMessage).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PictureEdit alertIcon;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItemMessage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}