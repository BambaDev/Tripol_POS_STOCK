using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Repository;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pos.Function
{
    internal static class LookUpEditPaginator
    {
        private static PanelControl paginationPanel;
        private static TextEdit txtCurrentPage;

        public static void AddPagination(LookUpEdit lookUpEdit, PagedDataSource<object> pagedDataSource)
        {
            lookUpEdit.Properties.Popup += (sender, e) => CustomizePopup(lookUpEdit, pagedDataSource);
        }

        private static void CustomizePopup(LookUpEdit lookUpEdit, PagedDataSource<object> pagedDataSource)
        {
            string lang = Properties.Settings.Default.Lang;
            Font font = new Font("Arial", 8, FontStyle.Bold);

            string prev;
            string next;

            if (lang == "en")
            {
                prev = "Previous";
                next = "Next";
            }
            else if (lang == "fr")
            {
                prev = "Précédent";
                next = "Suivant";
            }
            else
            {
                Function.CustomArabicFont.LoadCustomFont(8.0F, true);
                font = Function.CustomArabicFont.customFont;

                prev = "السابق";
                next = "التالي";
            }

            if (lookUpEdit == null) return;

            var popup = lookUpEdit.GetPopupEditForm() as PopupLookUpEditForm;
            if (popup == null) return;

            var form = popup as Form;
            if (form == null) return;

            // Remove existing pagination panel if it exists
            if (paginationPanel != null)
            {
                form.Controls.Remove(paginationPanel);
            }

            // Create a new pagination panel
            paginationPanel = new PanelControl { Dock = DockStyle.Bottom, Height = 40, Padding = new Padding(5) };

            txtCurrentPage = new TextEdit
            {
                Text = pagedDataSource.GetCurrentPage().ToString(),
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = font,
                Height = 40,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Padding = new Padding(10),
                Margin = new Padding(10)
            };

            var btnNext = new SimpleButton
            {
                Text = next,
                Dock = DockStyle.Right,
                Appearance = { Font = font },
                Margin = new Padding(5),
                Padding = new Padding(5)
            };

            var btnPrevious = new SimpleButton
            {
                Text = prev,
                Dock = DockStyle.Left,
                Appearance = { Font = font },
                Margin = new Padding(5),
                Padding = new Padding(5)
            };

            btnNext.Appearance.BackColor = Color.LightBlue;
            btnNext.Appearance.Options.UseBackColor = true;

            btnPrevious.Appearance.BackColor = Color.LightBlue;
            btnPrevious.Appearance.Options.UseBackColor = true;

            btnNext.Click += (sender, e) =>
            {
                if (pagedDataSource.HasMorePages())
                {
                    pagedDataSource.NextPage();
                    lookUpEdit.Properties.DataSource = pagedDataSource.GetDataPage();
                    txtCurrentPage.Text = pagedDataSource.GetCurrentPage().ToString();
                    lookUpEdit.ShowPopup();
                }
            };

            btnPrevious.Click += (sender, e) =>
            {
                pagedDataSource.PreviousPage();
                lookUpEdit.Properties.DataSource = pagedDataSource.GetDataPage();
                txtCurrentPage.Text = pagedDataSource.GetCurrentPage().ToString();
                lookUpEdit.ShowPopup();
            };

            if (lang == "ar")
            {
                paginationPanel.Controls.Add(btnPrevious);
                paginationPanel.Controls.Add(txtCurrentPage);
                paginationPanel.Controls.Add(btnNext);
            }
            else
            {
                paginationPanel.Controls.Add(btnNext);
                paginationPanel.Controls.Add(txtCurrentPage);
                paginationPanel.Controls.Add(btnPrevious);
            }

            // Add the pagination panel to the popup form
            form.Controls.Add(paginationPanel);
        }
    }
}
