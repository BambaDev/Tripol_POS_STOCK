namespace Pos.Forms.Auth
{
    partial class LockScreen
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
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition3 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition4 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            LockScreenImg = new DevExpress.XtraEditors.PictureEdit();
            txtPin1 = new DevExpress.XtraEditors.TextEdit();
            txtPin2 = new DevExpress.XtraEditors.TextEdit();
            txtPin3 = new DevExpress.XtraEditors.TextEdit();
            txtPin4 = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroupTechnicalWorker = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            toastNotificationsManager = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(components);
            dxValidationProviderLogin = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(components);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LockScreenImg.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPin1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPin2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPin3.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPin4.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupTechnicalWorker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderLogin).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(LockScreenImg);
            layoutControl1.Controls.Add(txtPin1);
            layoutControl1.Controls.Add(txtPin2);
            layoutControl1.Controls.Add(txtPin3);
            layoutControl1.Controls.Add(txtPin4);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(441, 377);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // LockScreenImg
            // 
            LockScreenImg.EditValue = Properties.Resources.password_monochromatic;
            LockScreenImg.Location = new System.Drawing.Point(15, 15);
            LockScreenImg.Name = "LockScreenImg";
            LockScreenImg.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            LockScreenImg.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            LockScreenImg.Size = new System.Drawing.Size(405, 291);
            LockScreenImg.StyleController = layoutControl1;
            LockScreenImg.TabIndex = 1;
            // 
            // txtPin1
            // 
            txtPin1.EditValue = "";
            txtPin1.Location = new System.Drawing.Point(18, 313);
            txtPin1.Name = "txtPin1";
            txtPin1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            txtPin1.Properties.Appearance.Options.UseFont = true;
            txtPin1.Size = new System.Drawing.Size(98, 44);
            txtPin1.StyleController = layoutControl1;
            txtPin1.TabIndex = 2;
            txtPin1.EditValueChanged += txtPin1_EditValueChanged;
            txtPin1.Enter += txtPin1_Enter;
            // 
            // txtPin2
            // 
            txtPin2.EditValue = "";
            txtPin2.Location = new System.Drawing.Point(120, 313);
            txtPin2.Name = "txtPin2";
            txtPin2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold);
            txtPin2.Properties.Appearance.Options.UseFont = true;
            txtPin2.Size = new System.Drawing.Size(98, 44);
            txtPin2.StyleController = layoutControl1;
            txtPin2.TabIndex = 3;
            txtPin2.EditValueChanged += txtPin2_EditValueChanged;
            txtPin2.Enter += txtPin2_Enter;
            // 
            // txtPin3
            // 
            txtPin3.EditValue = "";
            txtPin3.Location = new System.Drawing.Point(222, 313);
            txtPin3.Name = "txtPin3";
            txtPin3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold);
            txtPin3.Properties.Appearance.Options.UseFont = true;
            txtPin3.Size = new System.Drawing.Size(98, 44);
            txtPin3.StyleController = layoutControl1;
            txtPin3.TabIndex = 4;
            txtPin3.EditValueChanged += txtPin3_EditValueChanged;
            txtPin3.Enter += txtPin3_Enter;
            // 
            // txtPin4
            // 
            txtPin4.EditValue = "";
            txtPin4.Location = new System.Drawing.Point(324, 313);
            txtPin4.Name = "txtPin4";
            txtPin4.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold);
            txtPin4.Properties.Appearance.Options.UseFont = true;
            txtPin4.Size = new System.Drawing.Size(99, 44);
            txtPin4.StyleController = layoutControl1;
            txtPin4.TabIndex = 5;
            txtPin4.EditValueChanged += txtPin4_EditValueChanged;
            txtPin4.Enter += txtPin4_Enter;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroupTechnicalWorker });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(441, 377);
            Root.TextVisible = false;
            // 
            // layoutControlGroupTechnicalWorker
            // 
            layoutControlGroupTechnicalWorker.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroupTechnicalWorker.AppearanceGroup.Options.UseFont = true;
            layoutControlGroupTechnicalWorker.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroupTechnicalWorker.AppearanceItemCaption.Options.UseFont = true;
            layoutControlGroupTechnicalWorker.AppearanceTabPage.Header.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            layoutControlGroupTechnicalWorker.AppearanceTabPage.Header.Options.UseFont = true;
            layoutControlGroupTechnicalWorker.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup4, layoutControlItem9 });
            layoutControlGroupTechnicalWorker.Location = new System.Drawing.Point(0, 0);
            layoutControlGroupTechnicalWorker.Name = "layoutControlGroupTechnicalWorker";
            layoutControlGroupTechnicalWorker.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroupTechnicalWorker.Size = new System.Drawing.Size(431, 367);
            layoutControlGroupTechnicalWorker.Text = "Lock Screen";
            layoutControlGroupTechnicalWorker.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem4, layoutControlItem8, layoutControlItem6, layoutControlItem10 });
            layoutControlGroup4.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            layoutControlGroup4.Location = new System.Drawing.Point(0, 295);
            layoutControlGroup4.Name = "layoutControlGroup4";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition1.Width = 25D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 25D;
            columnDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition3.Width = 25D;
            columnDefinition4.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition4.Width = 25D;
            layoutControlGroup4.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDefinition1, columnDefinition2, columnDefinition3, columnDefinition4 });
            rowDefinition1.Height = 100D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            layoutControlGroup4.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDefinition1 });
            layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            layoutControlGroup4.Size = new System.Drawing.Size(415, 56);
            layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = txtPin3;
            layoutControlItem4.Location = new System.Drawing.Point(204, 0);
            layoutControlItem4.MinSize = new System.Drawing.Size(56, 28);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.OptionsTableLayoutItem.ColumnIndex = 2;
            layoutControlItem4.Size = new System.Drawing.Size(102, 50);
            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = txtPin4;
            layoutControlItem8.Location = new System.Drawing.Point(306, 0);
            layoutControlItem8.MinSize = new System.Drawing.Size(72, 48);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.OptionsTableLayoutItem.ColumnIndex = 3;
            layoutControlItem8.Size = new System.Drawing.Size(103, 50);
            layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
            layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            layoutControlItem6.Control = txtPin1;
            layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            layoutControlItem6.MinSize = new System.Drawing.Size(56, 28);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(102, 50);
            layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = txtPin2;
            layoutControlItem10.Location = new System.Drawing.Point(102, 0);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem10.Size = new System.Drawing.Size(102, 50);
            layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = LockScreenImg;
            layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            layoutControlItem9.MaxSize = new System.Drawing.Size(409, 295);
            layoutControlItem9.MinSize = new System.Drawing.Size(409, 295);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new System.Drawing.Size(415, 295);
            layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem9.TextVisible = false;
            // 
            // toastNotificationsManager
            // 
            toastNotificationsManager.ApplicationId = "fbb9a007-873c-413d-8d75-1b1cb96d2e8d";
            toastNotificationsManager.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] { new DevExpress.XtraBars.ToastNotifications.ToastNotification("b046d266-5b5a-4af9-93b6-3c3aca9ceee8", null, "Pellentesque lacinia tellus eget volutpat", "User added Successfully", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.Text01) });
            // 
            // LockScreen
            // 
            Appearance.BackColor = System.Drawing.Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(441, 377);
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            IconOptions.SvgImage = Properties.Resources.login_lock;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LockScreen";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Login";
            FormClosing += LockScreen_FormClosing;
            Load += LockScreen_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LockScreenImg.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPin1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPin2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPin3.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPin4.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroupTechnicalWorker).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxValidationProviderLogin).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProviderLogin;
		private DevExpress.XtraEditors.PictureEdit LockScreenImg;
		private DevExpress.XtraEditors.TextEdit txtPin1;
		private DevExpress.XtraEditors.TextEdit txtPin2;
		private DevExpress.XtraEditors.TextEdit txtPin3;
		private DevExpress.XtraEditors.TextEdit txtPin4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupTechnicalWorker;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}