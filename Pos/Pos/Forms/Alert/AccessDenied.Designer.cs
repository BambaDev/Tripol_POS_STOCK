namespace Pos.Forms.Alert
{
    partial class AccessDenied
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessDenied));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            btnAccessDenied = new DevExpress.XtraEditors.SimpleButton();
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            btnCloseFrm = new DevExpress.XtraEditors.SimpleButton();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(btnAccessDenied);
            layoutControl1.Controls.Add(pictureEdit1);
            layoutControl1.Controls.Add(btnCloseFrm);
            resources.ApplyResources(layoutControl1, "layoutControl1");
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            // 
            // btnAccessDenied
            // 
            btnAccessDenied.Appearance.BackColor = (System.Drawing.Color)resources.GetObject("btnAccessDenied.Appearance.BackColor");
            btnAccessDenied.Appearance.BorderColor = (System.Drawing.Color)resources.GetObject("btnAccessDenied.Appearance.BorderColor");
            btnAccessDenied.Appearance.Font = (System.Drawing.Font)resources.GetObject("btnAccessDenied.Appearance.Font");
            btnAccessDenied.Appearance.Options.UseBackColor = true;
            btnAccessDenied.Appearance.Options.UseBorderColor = true;
            btnAccessDenied.Appearance.Options.UseFont = true;
            resources.ApplyResources(btnAccessDenied, "btnAccessDenied");
            btnAccessDenied.Name = "btnAccessDenied";
            btnAccessDenied.StyleController = layoutControl1;
            btnAccessDenied.Click += btnAccessDenied_Click;
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = Properties.Resources.online_protection_flatline;
            resources.ApplyResources(pictureEdit1, "pictureEdit1");
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            pictureEdit1.StyleController = layoutControl1;
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
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlGroup1, layoutControlItem2, simpleLabelItem1, layoutControlGroup2 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            Root.Size = new System.Drawing.Size(585, 523);
            Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1 });
            layoutControlGroup1.Location = new System.Drawing.Point(0, 434);
            layoutControlGroup1.Name = "layoutControlGroup1";
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup1.Size = new System.Drawing.Size(575, 79);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = btnAccessDenied;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.MinSize = new System.Drawing.Size(44, 36);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(559, 63);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = pictureEdit1;
            layoutControlItem2.Location = new System.Drawing.Point(0, 119);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(575, 315);
            layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
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
            simpleLabelItem1.Location = new System.Drawing.Point(0, 62);
            simpleLabelItem1.MinSize = new System.Drawing.Size(383, 22);
            simpleLabelItem1.Name = "simpleLabelItem1";
            simpleLabelItem1.Size = new System.Drawing.Size(575, 57);
            simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(simpleLabelItem1, "simpleLabelItem1");
            simpleLabelItem1.TextSize = new System.Drawing.Size(376, 23);
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem3, simpleLabelItem2, emptySpaceItem1 });
            layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "layoutControlGroup2";
            layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlGroup2.Size = new System.Drawing.Size(575, 62);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnCloseFrm;
            layoutControlItem3.Location = new System.Drawing.Point(513, 0);
            layoutControlItem3.MinSize = new System.Drawing.Size(44, 42);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(46, 46);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem2
            // 
            simpleLabelItem2.AllowHotTrack = false;
            simpleLabelItem2.AppearanceItemCaption.Font = (System.Drawing.Font)resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font");
            simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            simpleLabelItem2.Location = new System.Drawing.Point(0, 0);
            simpleLabelItem2.MinSize = new System.Drawing.Size(383, 24);
            simpleLabelItem2.Name = "simpleLabelItem2";
            simpleLabelItem2.Size = new System.Drawing.Size(396, 46);
            simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            resources.ApplyResources(simpleLabelItem2, "simpleLabelItem2");
            simpleLabelItem2.TextSize = new System.Drawing.Size(376, 18);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(396, 0);
            emptySpaceItem1.MinSize = new System.Drawing.Size(106, 26);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(117, 46);
            emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // AccessDenied
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Shadow;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AccessDenied";
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)simpleLabelItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnAccessDenied;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.SimpleButton btnCloseFrm;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}