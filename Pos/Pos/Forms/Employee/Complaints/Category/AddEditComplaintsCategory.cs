using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
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
using Microsoft.Office.Interop.Excel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.Employee.Complaints.Category
{
    public partial class AddEditComplaintsCategory : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();

        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;
        Models.ComplaintCategory localcomplaintCategory;
        AddEditComplaints  localAddEditComplaints;
        bool isAddEditComplaintsCategory = false;
        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditComplaintsCategory()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
            InitializeSearchLookUpEdit();
            LoadData();
            this.type = "Add";
        }
        public AddEditComplaintsCategory(AddEditComplaints AddEditComplaints)
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
            InitializeSearchLookUpEdit();
            LoadData();
            localAddEditComplaints = AddEditComplaints;
            isAddEditComplaintsCategory = true;
            this.type = "Add";
        }
        public AddEditComplaintsCategory(int idComplaintCategories)
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
            InitializeSearchLookUpEdit();
            LoadData(idComplaintCategories);
            this.type = "Edit";
        }
        public void LoadData()
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = AppDb.ComplaintCategories.ToList();
            }
        }

        public void LoadData(int idComplaintCategories)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = AppDb.ComplaintCategories.ToList();
                currentItemId = idComplaintCategories;
                localcomplaintCategory = AppDb.ComplaintCategories.SingleOrDefault(x => x.Id == idComplaintCategories);
                txtDescription.Text = localcomplaintCategory.Description;
                txtName.Text = localcomplaintCategory.Name;
                this.type = "Edit";
            }
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
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            System.Drawing.Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;

            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            System.Drawing.Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
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


        private void ClearFormFields()
        {
            txtName.Text = "";
            txtDescription.Text = "";

        }



        private Models.ExpenseCategory GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.ExpenseCategories.Find(currentItemId);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                if (currentItemId == 0)
                {
                    // If no current selection, disable all navigation buttons.
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    return;
                }

                // Retrieve the minimum and maximum ID values from the Brands dataset.
                int minId = context.ComplaintCategories.Min(b => b.Id);
                int maxId = context.ComplaintCategories.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                LoadData(currentItemId);

            }
        }


        private void MoveToFirst()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the first item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du premier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأول:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? minId = context.ComplaintCategories.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        currentItemId = minId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToLast()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the last item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du dernier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأخير:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? maxId = context.ComplaintCategories.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        currentItemId = maxId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToNext()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.ComplaintCategories.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    currentItemId = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    var prevItem = context.ComplaintCategories.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        currentItemId = prevItem.Id;
                        DisplayCurrentItem();
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeSearchLookUpEdit()
        {
            string lang = Properties.Settings.Default.Lang;

            string name;
            string writeSomething;

            if (lang == "en")
            {
                name = "Name";
                writeSomething = "Write something...";
            }
            else if (lang == "fr")
            {
                name = "nom";
                writeSomething = "Écris quelque chose...";
            }
            else
            {
                name = "اسم";
                writeSomething = "أكتب شيئا...";
            }

            using (var context = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = context.ComplaintCategories.ToList();
                searchLookUpEdit.Properties.DisplayMember = "Name";
                searchLookUpEdit.Properties.ValueMember = "Id";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("Name", name);

                searchLookUpEdit.Properties.NullText = writeSomething;

                searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if ((searchLookUpEdit.EditValue != null) && !(string.IsNullOrEmpty(searchLookUpEdit.EditValue.ToString())))
            {
                LoadData(int.Parse(searchLookUpEdit.EditValue.ToString()));
            }
        }

        private void btnSaveExpenseCategory_Click(object sender, EventArgs e)
        {
            if (dxValidationProviderCategory.Validate())
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    if (this.type=="Add")
                    {
                        Models.ComplaintCategory complaintCategory = new Models.ComplaintCategory();
                        complaintCategory.Name = txtName.Text;
                        complaintCategory.Description = txtDescription.Text;
                        complaintCategory.CreateAt = DateTime.Now;
                        complaintCategory.UpdateAt = DateTime.Now;
                        AppDb.ComplaintCategories.Add(complaintCategory);
                        if (isAddEditComplaintsCategory==true)
                        {
                            localAddEditComplaints.setComplaintsCategory(complaintCategory.Id);
                            this.Close();
                        }
                    }
                    else
                    {
                        if (this.type == "Edit")
                        {
                            localcomplaintCategory.Name=txtName.Text;
                            localcomplaintCategory.Description=txtDescription.Text;
                            localcomplaintCategory.UpdateAt= DateTime.Now;
                            AppDb.ComplaintCategories.Update(localcomplaintCategory);
                            if (isAddEditComplaintsCategory == true)
                            {
                                localAddEditComplaints.setComplaintsCategory(localcomplaintCategory.Id);
                                this.Close();
                            }
                        }
                    }

                    AppDb.SaveChanges();
                    Function.Sound.Added();
                }
            }
        }
    }
}