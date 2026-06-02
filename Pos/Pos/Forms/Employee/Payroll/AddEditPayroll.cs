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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DevExpress.XtraLayout;

namespace Pos.Forms.Employee.Payroll
{
    public partial class AddEditPayroll : DevExpress.XtraEditors.XtraForm
    {
        public Payrolls payrolls = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditPayroll()
        {
            InitializeComponent();

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

            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlItem14.AppearanceItemCaption.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            layoutControlItem17.AppearanceItemCaption.Font = customFont10;
            layoutControlItem14.AppearanceItemCaption.Font = customFont10;
            layoutControlItem16.AppearanceItemCaption.Font = customFont10;
            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
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

        public void setPayrollsObject(Payrolls payrolls)
        {
            this.payrolls = payrolls;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item !";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'article !";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
            }

            if (this.payrolls != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.payrolls.payroll_id != 0)
                        this.currentItemId = this.payrolls.payroll_id;

                    this.payrolls.payroll_id = 0;

                    Models.Payroll payroll = context.Payrolls.Find(this.currentItemId);

                    if (payroll != null)
                    {
                        txtPayPeriodStart.EditValue = payroll.PayPeriodStart;
                        txtPayPeriodEnd.EditValue = payroll.PayPeriodEnd;
                        txtGrossPay.EditValue = payroll.GrossPay;
                        txtNetPay.EditValue = payroll.NetPay;
                        txtDeductions.EditValue = payroll.Deductions;
                        txtOvertime.EditValue = payroll.Overtime;
                        txtEmployee.EditValue = payroll.EmployeeId;

                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Payroll payroll;

                    if (this.type == "Add")
                    {
                        payroll = new Models.Payroll();

                        if (txtPayPeriodStart.EditValue is DateTime PayPeriodStartValue)
                        {
                            payroll.PayPeriodStart = DateOnly.FromDateTime(PayPeriodStartValue);
                        }

                        if (txtPayPeriodEnd.EditValue is DateTime PayPeriodEndValue)
                        {
                            payroll.PayPeriodEnd = DateOnly.FromDateTime(PayPeriodEndValue);
                        }

                        payroll.GrossPay = decimal.Parse(txtGrossPay.EditValue.ToString());
                        payroll.NetPay = decimal.Parse(txtNetPay.EditValue.ToString());
                        payroll.Deductions = decimal.Parse(txtDeductions.EditValue.ToString());
                        payroll.Overtime = decimal.Parse(txtOvertime.EditValue.ToString());

                        payroll.EmployeeId = int.Parse(txtEmployee.EditValue.ToString());

                        payroll.UserId = Properties.Settings.Default.userId;

                        payroll.CreatedAt = DateTime.Now;
                        payroll.UpdatedAt = DateTime.Now;

                        context.Payrolls.Add(payroll);
                    }
                    else
                    {
                        if (this.payrolls != null)
                            this.currentItemId = this.payrolls.payroll_id;

                        payroll = context.Payrolls.Find(this.currentItemId);

                        if (txtPayPeriodStart.EditValue is DateTime PayPeriodStartValue)
                        {
                            payroll.PayPeriodStart = DateOnly.FromDateTime(PayPeriodStartValue);
                        }

                        if (txtPayPeriodEnd.EditValue is DateTime PayPeriodEndValue)
                        {
                            payroll.PayPeriodEnd = DateOnly.FromDateTime(PayPeriodEndValue);
                        }

                        payroll.GrossPay = decimal.Parse(txtGrossPay.EditValue.ToString());
                        payroll.NetPay = decimal.Parse(txtNetPay.EditValue.ToString());
                        payroll.Deductions = decimal.Parse(txtDeductions.EditValue.ToString());
                        payroll.Overtime = decimal.Parse(txtOvertime.EditValue.ToString());

                        payroll.EmployeeId = int.Parse(txtEmployee.EditValue.ToString());

                        payroll.UpdatedAt = DateTime.Now;

                        context.Entry(payroll).State = EntityState.Modified;
                    }

                    context.SaveChanges();

                    Function.Sound.Added();

                    if (this.payrolls != null)
                        this.payrolls.loadPayrolls();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public void getEmployees()
        {
            using (var context = new AppDbContext())
            {
                txtEmployee.Properties.DataSource = context.Employees.ToList();
                txtEmployee.Properties.DisplayMember = "FirstName"; // Set display member
                txtEmployee.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void AddEditPayroll_Load(object sender, EventArgs e)
        {
            this.getEmployees();

            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private void btnSupplierReset_Click(object sender, EventArgs e)
        {
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;

            Function.Sound.Added();
        }

        private Models.Payroll GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Payrolls.Find(currentItemId);
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
                int minId = context.Payrolls.Min(b => b.Id);
                int maxId = context.Payrolls.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Payroll currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtPayPeriodStart.EditValue = currentItem.PayPeriodStart;
                    txtPayPeriodEnd.EditValue = currentItem.PayPeriodEnd;
                    txtGrossPay.EditValue = currentItem.GrossPay;
                    txtNetPay.EditValue = currentItem.NetPay;
                    txtDeductions.EditValue = currentItem.Deductions;
                    txtOvertime.EditValue = currentItem.Overtime;
                    txtEmployee.EditValue = currentItem.EmployeeId;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
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
                    int? minId = context.Payrolls.Min(b => (int?)b.Id);
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
                    int? maxId = context.Payrolls.Max(b => (int?)b.Id);
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

                var nextItem = context.Payrolls.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Payrolls.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeSearchLookUpEdit()
        {
            string lang = Properties.Settings.Default.Lang;

            string firstName;
            string lastName;
            string writeSomething;

            if (lang == "en")
            {
                firstName = "First Name";
                lastName = "Last Name";
                writeSomething = "Write something...";
            }
            else if (lang == "fr")
            {
                firstName = "Prénom";
                lastName = "Nom de famille";
                writeSomething = "Écrire quelque chose...";
            }
            else
            {
                firstName = "الاسم الأول";
                lastName = "الاسم الأخير";
                writeSomething = "اكتب شيئًا...";
            }

            using (var context = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = context.Payrolls.ToList();
                searchLookUpEdit.Properties.DisplayMember = "Employee.FirstName";
                searchLookUpEdit.Properties.ValueMember = "EmployeeId";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("Employee.FirstName", firstName);
                searchLookUpEdit.Properties.View.Columns.AddVisible("Employee.LastName", lastName);

                searchLookUpEdit.Properties.NullText = writeSomething;

                searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string selected;
            string noItemSelected;

            if (lang == "en")
            {
                selected = "Selected value is not a valid integer";
                noItemSelected = "No item selected.";
            }
            else if (lang == "fr")
            {
                selected = "La valeur sélectionnée n'est pas un entier valide";
                noItemSelected = "Aucun élément sélectionné.";
            }
            else
            {
                selected = "القيمة المحددة ليست عددًا صحيحًا صالحًا";
                noItemSelected = "لم يتم تحديد أي عنصر.";
            }

            if (searchLookUpEdit.EditValue != null)
            {
                if (int.TryParse(searchLookUpEdit.EditValue.ToString(), out int itemId))
                {
                    this.currentItemId = itemId;
                    this.edit();
                }
                else
                {
                    Console.WriteLine(selected);
                }
            }
            else
            {
                Console.WriteLine(noItemSelected);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}