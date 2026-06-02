using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Overlay
{
	public partial class OverlayForm : DevExpress.XtraEditors.XtraForm
	{
		public OverlayForm(Form parentForm = null)
		{
			InitializeComponent();

			if (parentForm != null)
			{
				this.StartPosition = FormStartPosition.Manual;
				this.FormBorderStyle = FormBorderStyle.None;
				this.Opacity = 0.5; // Adjust for desired level of "blur"
				this.BackColor = Color.DarkCyan;
				this.ShowInTaskbar = false;
				this.Location = parentForm.Location;
				this.Size = parentForm.Size;
				parentForm.LocationChanged += OverlayForm_LocationChanged;
				parentForm.SizeChanged += OverlayForm_SizeChanged;
			}
		}

		private void OverlayForm_SizeChanged(object sender, EventArgs e)
		{
			var parentForm = (Form)sender;
			this.Size = parentForm.Size;
		}

		private void OverlayForm_LocationChanged(object sender, EventArgs e)
		{
			var parentForm = (Form)sender;
			this.Location = parentForm.Location;
		}
	}
}