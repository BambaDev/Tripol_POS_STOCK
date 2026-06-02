using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Employee.Department;
using Pos.Function;
using Pos.Models;
using Pos.Report.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using EmployeeReview = Pos.Report.Employee.EmployeePerformanceReview;

namespace Pos.Forms.Employee.EmployeePerformanceReview
{
    public partial class AddEditEmployeeReview : DevExpress.XtraEditors.XtraForm
    {
        public EmployeeReviews employeeReviews = null;
        public string type = "Add";
        public Object obj = null;
        public DataTable dt = new DataTable();
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditEmployeeReview()
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
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
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

        public void setEmployeeReviewsObject(EmployeeReviews employeeReviews)
        {
            this.employeeReviews = employeeReviews;
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

            if (this.employeeReviews != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.employeeReviews.review_id != 0)
                        this.currentItemId = this.employeeReviews.review_id;

                    this.employeeReviews.review_id = 0;

                    Models.EmployeeReview employeePerformanceReview = context.EmployeeReviews.Find(this.currentItemId);

                    if (employeePerformanceReview != null)
                    {
                        searchLookUpEdit.EditValue = employeePerformanceReview.EmployeeId;
                        txtInterviewer.EditValue = employeePerformanceReview.InterviewerId;
                        txtReviewPeriod.EditValue = employeePerformanceReview.ReviewPeriod;
                        txtComment.Text = employeePerformanceReview.Comment;
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

                Models.EmployeeReview employeeReview;

                using (var context = new AppDbContext())
                {
                    if (this.type == "Add")
                    {
                        employeeReview = new Models.EmployeeReview();
                        employeeReview.EmployeeId = currentItemId;
                        employeeReview.InterviewerId = int.Parse(txtInterviewer.EditValue.ToString());
                        employeeReview.UserId = Properties.Settings.Default.userId;
                        employeeReview.ReviewPeriod = DateTime.Parse(txtReviewPeriod.Text);
                        employeeReview.Comment = txtComment.Text;
                        employeeReview.CreatedAt = DateTime.Now;
                        employeeReview.UpdatedAt = DateTime.Now;

                        context.EmployeeReviews.Add(employeeReview);
                        context.SaveChanges();

                        List<Models.EmployeePerformanceReview> employeePerformanceReviewslists = new List<Models.EmployeePerformanceReview>();

                        for (int i = 0; dt.Rows.Count > i; i++)
                        {
                            Models.EmployeePerformanceReview employeePerformanceReviews = new Models.EmployeePerformanceReview();
                            employeePerformanceReviews.EmployeeReviewId = employeeReview.Id;
                            employeePerformanceReviews.EvaluationId = Convert.ToInt32(this.dt.Rows[i]["EvaluationId"]);
                            employeePerformanceReviews.EvaluationName = this.dt.Rows[i]["Name"].ToString();
                            employeePerformanceReviews.Box1 = bool.Parse(this.dt.Rows[i]["Box1"].ToString());
                            employeePerformanceReviews.Box2 = bool.Parse(this.dt.Rows[i]["Box2"].ToString());
                            employeePerformanceReviews.Box3 = bool.Parse(this.dt.Rows[i]["Box3"].ToString());
                            employeePerformanceReviews.Box4 = bool.Parse(this.dt.Rows[i]["Box4"].ToString());
                            employeePerformanceReviews.Box5 = bool.Parse(this.dt.Rows[i]["Box5"].ToString());
                            employeePerformanceReviews.CreatedAt = DateTime.Now;
                            employeePerformanceReviews.UpdatedAt = DateTime.Now;
                            employeePerformanceReviewslists.Add(employeePerformanceReviews);
                        }

                        context.EmployeePerformanceReviews.AddRange(employeePerformanceReviewslists);
                        context.SaveChanges();

                        txtComment.Text = "";
                    }
                    else
                    {
                        if (this.employeeReviews != null)
                            this.currentItemId = this.employeeReviews.review_id;

                        employeeReview = context.EmployeeReviews.Find(this.currentItemId);

                        if (employeeReview != null)
                        {
                            employeeReview.EmployeeId = currentItemId;
                            employeeReview.InterviewerId = int.Parse(txtInterviewer.EditValue.ToString());
                            employeeReview.UserId = Properties.Settings.Default.userId;
                            employeeReview.ReviewPeriod = DateTime.Parse(txtReviewPeriod.Text);
                            employeeReview.Comment = txtComment.Text;
                            employeeReview.UpdatedAt = DateTime.Now;

                            context.Entry(employeeReview).State = EntityState.Modified;

                            context.SaveChanges();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                        }
                    }

                }

                this.printEmployeePerformanceReviewA4(employeeReview.Id);

                Function.Sound.Added();

                if (this.employeeReviews != null)
                    this.employeeReviews.loadReviews();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public void printEmployeePerformanceReviewA4(int employeeReviewId)
        {
            string lang = Properties.Settings.Default.Lang;

            string printerError;
            string isNotValid;
            string printer;
            string anErrorOccurred;
            string validationError;
            string validatedData;

            if (lang == "en")
            {
                printerError = "Printer Error";
                isNotValid = "is Not Valid";
                printer = "Printer";
                anErrorOccurred = "An error occurred while printing:";
                validationError = "Validation Error";
                validatedData = "You can't print without validated data.";
            }
            else if (lang == "fr")
            {
                printerError = "Erreur d'imprimante";
                isNotValid = "n'est pas valide";
                printer = "Imprimante";
                anErrorOccurred = "Une erreur s'est produite lors de l'impression :";
                validationError = "Erreur de validation";
                validatedData = "Vous ne pouvez pas imprimer sans données validées.";
            }
            else
            {
                printerError = "خطأ في الطابعة";
                isNotValid = "غير صحيح";
                printer = "طابعة";
                anErrorOccurred = "حدث خطأ أثناء الطباعة:";
                validationError = "خطأ في التحقق";
                validatedData = "لا يمكنك الطباعة بدون بيانات تم التحقق من صحتها.";
            }

            if (employeeReviewId != 0)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterDocument;

                EmployeeReview employeePerformanceReview = new EmployeeReview(employeeReviewId);
                employeePerformanceReview.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"{printer} \"{printerName}\" {isNotValid}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(employeePerformanceReview);

                // Set the printer name in the PrintTool
                printTool.PrinterSettings.PrinterName = printerName;

                try
                {
                    // Print the document using the specified printer
                    printTool.Print(printerName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{anErrorOccurred} {ex.Message}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Display the report using a form with a DocumentViewer
                //FormView reportViewerFrm = new FormView();
                //reportViewerFrm.EmployeePerformanceReview(employeePerformanceReview);
                //reportViewerFrm.Show();
            }
            else
            {
                XtraMessageBox.Show(validatedData, validationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void getInterviewers()
        {
            using (var context = new AppDbContext())
            {
                txtInterviewer.Properties.DataSource = context.Users.ToList();
                txtInterviewer.Properties.DisplayMember = "FirstName"; // Set display member
                txtInterviewer.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void AddEditEmployeeReview_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("EvaluationId", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Box1", typeof(bool));
                dt.Columns.Add("Box2", typeof(bool));
                dt.Columns.Add("Box3", typeof(bool));
                dt.Columns.Add("Box4", typeof(bool));
                dt.Columns.Add("Box5", typeof(bool));
            }

            this.getInterviewers();

            this.initEvaluations();

            this.InitializeSearchLookUpEdit();
        }

        private void initEvaluations()
        {
            using (var context = new AppDbContext())
            {
                var evaluations = context.Evaluations.ToList();

                foreach (var item in evaluations)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["EvaluationId"] = item.Id;
                    newRow["Name"] = item.Name;
                    newRow["Box1"] = false;
                    newRow["Box2"] = false;
                    newRow["Box3"] = false;
                    newRow["Box4"] = false;
                    newRow["Box5"] = false;

                    dt.Rows.Add(newRow);
                }

                gridControlEmployeePerformanceReview.DataSource = dt;
            }
        }

        private Models.EmployeeReview GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.EmployeeReviews.Find(currentItemId);
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
                int minId = context.EmployeeReviews.Min(b => b.Id);
                int maxId = context.EmployeeReviews.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.EmployeeReview currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    searchLookUpEdit.EditValue = currentItem.EmployeeId;
                    txtInterviewer.EditValue = currentItem.InterviewerId;
                    txtReviewPeriod.EditValue = currentItem.ReviewPeriod;
                    txtComment.Text = currentItem.Comment;

                    this.getEmployee(currentItem.EmployeeId.Value);

                    EmployeeReview employeePerformanceReview = new EmployeeReview(currentItem.Id);
                    employeePerformanceReview.CreateDocument();

                    documentViewer.PrintingSystem = employeePerformanceReview.PrintingSystem;
                    documentViewer.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth, new object[] { });

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
                    int? minId = context.EmployeeReviews.Min(b => (int?)b.Id);
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
                    int? maxId = context.EmployeeReviews.Max(b => (int?)b.Id);
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

                var nextItem = context.EmployeeReviews.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.EmployeeReviews.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtComment.Text = string.Empty;
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
                // Assuming context.Brands returns a DbSet<Brand>
                searchLookUpEdit.Properties.DataSource = context.Employees.ToList();
                searchLookUpEdit.Properties.DisplayMember = "FirstName";  // Display member is the Brand Name
                searchLookUpEdit.Properties.ValueMember = "Id";  // Value member is the Brand Id

                // This sets up the columns you want to display in the dropdown
                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("FirstName", firstName);
                searchLookUpEdit.Properties.View.Columns.AddVisible("LastName", lastName);

                searchLookUpEdit.Properties.NullText = writeSomething;

                // Enable auto-search functionality
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
                    this.getEmployee(this.currentItemId);
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

        public void getEmployee(int employeeId)
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

            using (var context = new AppDbContext())
            {
                Models.Employee employee = context.Employees.Find(employeeId);

                if (employee != null)
                {
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtEmail.Text = employee.Email;
                    txtImage.EditValue = employee.Image;
                    txtAddress.Text = employee.Address;
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

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}