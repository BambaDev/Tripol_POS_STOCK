using DevExpress.XtraEditors;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.Customer
{
    public partial class Details : DevExpress.XtraEditors.XtraForm
    {
        public int customerId = 0;
        public decimal currentDue = 0;
        public Customer.Customers customers = null;

        public Details(int customerId)
        {
            InitializeComponent();

            this.toRtl();

            this.customerId = customerId;
            this.BorderStyle();

            // style the grid view
            gridViewCases.RowStyle += gridViewCases_RowStyle;
            gridViewCases.FocusedRowChanged += gridViewCases_FocusedRowChanged;
            gridViewCases.CustomDrawCell += gridViewCases_CustomDrawCell;
            gridViewCases.RowHeight = Function.Helper.RowHeight;
            gridViewCases.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewPayments.RowStyle += gridViewPayments_RowStyle;
            gridViewPayments.FocusedRowChanged += gridViewPayments_FocusedRowChanged;
            gridViewPayments.CustomDrawCell += gridViewPayments_CustomDrawCell;
            gridViewPayments.RowHeight = Function.Helper.RowHeight;
            gridViewPayments.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCases_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCases.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCases_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCases.FocusedRowHandle && e.Column == gridViewCases.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCases_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }


        // style the grid view
        private void gridViewPayments_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPayments.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPayments_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPayments.FocusedRowHandle && e.Column == gridViewPayments.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPayments_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        public void BorderStyle()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang != "ar")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            }
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                this.ApplyCustomFont();

                // Set the form to use RTL
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;
            simpleLabelItem2.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            layoutControlItem11.AppearanceItemCaption.Font = customFont10;
            layoutControlItem12.AppearanceItemCaption.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            foreach (GridColumn column in gridViewCases.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }

            foreach (GridColumn column in gridViewPayments.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
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

        public void setObject(Customers customers)
        {
            this.customers = customers;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                var customer = context.Customers.Find(this.customerId);

                if (customer != null)
                {
                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    txtEmail.Text = customer.Email;
                    txtGender.Text = customer.Gender;
                    txtAddress.Text = customer.Address;
                    txtCustomerImage.EditValue = customer.Image;

                    this.currentDue = decimal.Parse(customer.CurrentDue.ToString());
                }

                this.loadMaintenances();
            }
        }

        public void loadMaintenances()
        {
            using (var context = new AppDbContext())
            {
                //
            }
        }

        public void loadPayments(int maintenanceId)
        {
            using (var context = new AppDbContext())
            {
               //
            }
        }

        private void gridViewCases_Click(object sender, EventArgs e)
        {
            if (gridViewCases.RowCount > 0 && gridViewCases.FocusedRowHandle >= 0)
               
            {
                // Retrieve the values from the focused row and handle nulls by defaulting to 0
                var idValue = gridViewCases.GetRowCellValue(gridViewCases.FocusedRowHandle, "Id") ?? 0;
                var feesValue = gridViewCases.GetRowCellValue(gridViewCases.FocusedRowHandle, "Fees") ?? 0m;
                var dueValue = gridViewCases.GetRowCellValue(gridViewCases.FocusedRowHandle, "Due") ?? 0m;

                int caseId = int.TryParse(idValue.ToString(), out int parsedCaseId) ? parsedCaseId : 0;
                decimal totalAmount = decimal.TryParse(feesValue.ToString(), out decimal parsedTotalAmount) ? parsedTotalAmount : 0m;
                decimal due = decimal.TryParse(dueValue.ToString(), out decimal parsedDue) ? parsedDue : 0m;

                // Update the text boxes with the retrieved values
                txtTotalAmount.Text = totalAmount.ToString("N2") + " DA"; // Format the amount
                txtTheAmountPaid.Text = this.calculeTotal() + " DA"; // Calculate the total amount paid
                txtAmountOwed.Text = due.ToString("N2") + " DA"; // Format the amount owed

                // Load payments related to the selected case
                this.loadPayments(caseId);

                // Play a sound to indicate the selection
                Function.Sound.Selected();
            }
        }

        public decimal calculeTotal()
        {
            decimal total = 0;

            for (int i = 0; i < gridViewPayments.RowCount; i++)
            {
                total += decimal.Parse(gridViewPayments.GetRowCellValue(i, "Amount").ToString());
            }

            return total;
        }
    }
}