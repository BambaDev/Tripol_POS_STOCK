namespace Pos.Forms.Report
{
    partial class FormView
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
            documentViewer = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            SuspendLayout();
            // 
            // documentViewer
            // 
            documentViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            documentViewer.IsMetric = true;
            documentViewer.Location = new System.Drawing.Point(0, 0);
            documentViewer.Name = "documentViewer";
            documentViewer.Size = new System.Drawing.Size(702, 540);
            documentViewer.TabIndex = 0;
            // 
            // FormView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(702, 540);
            Controls.Add(documentViewer);
            Name = "FormView";
            Text = "Document Viewer";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormView_Load;
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer;
    }
}