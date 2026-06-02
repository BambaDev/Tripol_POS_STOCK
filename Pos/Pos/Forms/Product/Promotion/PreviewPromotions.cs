using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Overlay;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Product.Promotion
{
    public partial class PreviewPromotions : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int promotion_id = 0;

        public PreviewPromotions()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void Promotions_Load(object sender, EventArgs e)
        {
            this.loadPromotions();
        }

        public void loadPromotions()
        {
            try
            {
                // Ensuring the database context is properly initialized
                if (Shared.db != null)
                {
                    DateTime currentDate = DateTime.Now; // Capture the current date
                    var promotions = Shared.db.Promotions
                        .Where(p => (p.StartDate == null || p.StartDate <= currentDate) && (p.EndDate == null || p.EndDate >= currentDate)) // Filter for active promotions
                        .OrderByDescending(p => p.Id)
                        .ToList();

                    if (promotions != null && promotions.Count > 0)
                    {
                        gridControlPromotions.DataSource = promotions;
                    }
                    else
                    {
                        MessageBox.Show("No active promotions found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gridControlPromotions.DataSource = null; // Clear previous data if no active promotions are found
                    }
                }
                else
                {
                    MessageBox.Show("Database context is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                MessageBox.Show($"Failed to load promotions due to an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}