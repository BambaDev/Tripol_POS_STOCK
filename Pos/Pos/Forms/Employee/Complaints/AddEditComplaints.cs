using DevExpress.Drawing;
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
using System.Xml.Linq;
using DevExpress.XtraLayout;
using Pos.Forms.Employee.Complaints.Category;

namespace Pos.Forms.Employee.Complaints
{
    public partial class AddEditComplaints : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();

        public string type = "Add";
        public int currentItemId = 0;
        Models.Complaint localcomplaint;

        public AddEditComplaints()
        {
            InitializeComponent();
            this.type = "Add";
            
            this.toRtl();
            this.BorderStyle();
            LoadData();
        }
        public void LoadData()
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                cbxComplaintsCategory.Properties.DataSource = AppDb.ComplaintCategories.ToList();
                cbxEmployee.Properties.DataSource=AppDb.Employees.ToList();

            }
        }
        public void LoadData(int idComplaints)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                cbxComplaintsCategory.Properties.DataSource = AppDb.ComplaintCategories.ToList();
                cbxEmployee.Properties.DataSource = AppDb.Employees.ToList();
                currentItemId = idComplaints;

                // Fetching the complaint
                localcomplaint = AppDb.Complaints.SingleOrDefault(x => x.Id == idComplaints);

                if (localcomplaint != null)
                {
                    txtDescription.Text = localcomplaint.Description;
                    cbxEmployee.EditValue = localcomplaint.EmployeeId;
                    cbxComplaintsCategory.EditValue = localcomplaint.ComplaintCateId;
                    txttitle.Text = localcomplaint.Title;
                }
                else
                {
                    // Handle the case where the complaint is not found
                    MessageBox.Show("Complaint not found!");
                }
            }

        }
        public void SetEmployees(int idEmployees)
        {
            cbxEmployee.EditValue=idEmployees;
        }
        public AddEditComplaints(int idComplaits)
        {
            InitializeComponent();
            LoadData(idComplaits);
            this.type = "Edit";
            this.toRtl();
            this.BorderStyle();
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
            Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlItem14.AppearanceItemCaption.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            txtQuickCategory.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem14.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
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



        public void setTypeOperation(string type)
        {
            this.type = type;
        }



        private void btnSaveExp_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
         

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
               
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'article!";
               
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
               
            }

            if (dxValidationProviderComplaitns.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Complaint Complaint;
                   

                    if (this.type == "Add")
                    {


                        // Add new expense
                        Complaint = new Models.Complaint
                        {
                            Title = txttitle.Text,
                            Description = txtDescription.Text,
                            ComplaintCateId = int.Parse(cbxComplaintsCategory.EditValue.ToString()),
                            UserId = Properties.Settings.Default.userId,
                            EmployeeId= int.Parse(cbxEmployee.EditValue.ToString()),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Complaints.Add(Complaint);
                        ClearFormFields();
                    }
                    else
                    {



                        Complaint = context.Complaints.Find(this.currentItemId);

                        if (Complaint != null)
                        {


                            Complaint.Title = txttitle.Text;
                            Complaint.EmployeeId = int.Parse(cbxEmployee.EditValue.ToString());
                            Complaint.Description = txtDescription.Text;
                            Complaint.ComplaintCateId = int.Parse(cbxComplaintsCategory.EditValue.ToString());
                            Complaint.UpdatedAt = DateTime.Now;

                            context.Entry(Complaint).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            return;
                        }
                    }

                    // Save changes to the database
                    context.SaveChanges();

                    Function.Sound.Added();

                    // Reload expenses

                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearFormFields()
        {
            txtDescription.Clear();
            cbxComplaintsCategory.Clear();
            cbxEmployee.Clear();
            txttitle.Clear();
         
        }

        private void AddEditExpense_Load(object sender, EventArgs e)
        {


        }

        private Models.Complaint GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Complaints.Find(currentItemId);
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
                int minId = context.Complaints.Min(b => b.Id);
                int maxId = context.Complaints.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Complaint currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txttitle.Text = currentItem.Title;
                    cbxComplaintsCategory.EditValue = currentItem.ComplaintCateId;
                    cbxEmployee.EditValue=currentItem.EmployeeId;
                    txtDescription.Text = currentItem.Description;
                
                }
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
                    int? minId = context.Complaints.Min(b => (int?)b.Id);
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
                    int? maxId = context.Complaints.Max(b => (int?)b.Id);
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

                var nextItem = context.Complaints.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Complaints.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            cbxComplaintsCategory.Clear();
            cbxEmployee.Clear();
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

        private void txtQuickCategory_Click(object sender, EventArgs e)
        {
            AddEditComplaintsCategory cardTypeFrm = new AddEditComplaintsCategory(this);
            cardTypeFrm.ShowDialog();
        }
        public void setComplaintsCategory(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    cbxComplaintsCategory.Properties.DataSource = AppDb.ComplaintCategories.ToList();
                    cbxComplaintsCategory.EditValue = id;
                }
            }
            catch { }

        }
        public void setComplaintsCategory()
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    cbxComplaintsCategory.Properties.DataSource = AppDb.ComplaintCategories.ToList();

                }
            }
            catch { }

        }
    }
}