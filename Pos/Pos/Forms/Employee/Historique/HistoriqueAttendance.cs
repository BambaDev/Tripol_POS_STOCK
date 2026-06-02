using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Function;
using Pos.Models;
using Pos.Report.Employee;
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
using Pos.Forms.Employee.Complaints.Category;
using Pos.Forms.Expense;
using Pos.Forms.Employee.Attendance;
using DevExpress.Data.ODataLinq.Helpers;

namespace Pos.Forms.Employee
{
    public partial class HistoriqueAttendance : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
        public Attendances attendances = null;
        public string type = "Add";
        public int currentItemId = 0;
        public int employeeId = 0;
        int currentattendacneId = 0;
        private string searchTerm = string.Empty;
        int complaintsId;
        public HistoriqueAttendance()
        {
            InitializeComponent();

            // Setup the timer
            searchTimer.Interval = 300; // Delay for 300 milliseconds
            searchTimer.Tick += SearchTimer_Tick;
            btnAddComplaints.Enabled = false;
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
            layoutControlGroup3.AppearanceGroup.Font = customFont;

            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup10.AppearanceGroup.Font = customFont;
            layoutControlGroup12.AppearanceGroup.Font = customFont;
            layoutControlGroup13.AppearanceGroup.Font = customFont;
            layoutControlGroup14.AppearanceGroup.Font = customFont;
            layoutControlGroup15.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;


            layoutControlItem18.AppearanceItemCaption.Font = customFont10;

            layoutControlItem54.AppearanceItemCaption.Font = customFont10;
            layoutControlItem55.AppearanceItemCaption.Font = customFont10;
            layoutControlItem56.AppearanceItemCaption.Font = customFont10;
            layoutControlItem57.AppearanceItemCaption.Font = customFont10;
            layoutControlItem58.AppearanceItemCaption.Font = customFont10;
            layoutControlItem59.AppearanceItemCaption.Font = customFont10;
            layoutControlItem60.AppearanceItemCaption.Font = customFont10;
            layoutControlItem61.AppearanceItemCaption.Font = customFont10;
            layoutControlItem62.AppearanceItemCaption.Font = customFont10;
            layoutControlItem63.AppearanceItemCaption.Font = customFont10;
            layoutControlItem64.AppearanceItemCaption.Font = customFont10;
            layoutControlItem65.AppearanceItemCaption.Font = customFont10;
            layoutControlItem66.AppearanceItemCaption.Font = customFont10;
            layoutControlItem67.AppearanceItemCaption.Font = customFont10;
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

        public void setAttendancesObject(Attendances attendances)
        {
            this.attendances = attendances;
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

            if (this.attendances != null)
            {
                btnAddComplaints.Enabled = true;
                using (var context = new AppDbContext())
                {
                    if (this.attendances.attendance_id != 0)
                        this.currentattendacneId = this.attendances.attendance_id;

                    this.attendances.attendance_id = 0;

                    Models.Attendance attendance = context.Attendances.Find(this.currentattendacneId);
                    employeeId = (int)attendance.EmployeeId;
                    if (attendance != null)
                    {

                        txtEmployee.EditValue = attendance.EmployeeId;
                        btnSelect.Enabled = true;
                        if (attendance.EmployeeId != null)
                        {
                            LoadcomplaintsEmployee((int)attendance.EmployeeId);
                        }

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



        public void getEmployees()
        {
            using (var context = new AppDbContext())
            {
                txtEmployee.Properties.DataSource = context.Employees.ToList();
                txtEmployee.Properties.DisplayMember = "FirstName"; // Set display member
                txtEmployee.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getEmployeesAttendancesOfToday()
        {
            using (var context = new AppDbContext())
            {
                // Get today's date
                var today = DateOnly.FromDateTime(DateTime.Today);

                // Query the database for attendances with today's date
                var todaysAttendances = context.Attendances

                    .Include(a => a.Employee)
                    .Include(a => a.User)
                    .ToList();

                // Set the data source of the grid control to today's attendances
                gridControlAttendances.DataSource = todaysAttendances;
            }
        }
        public void getEmployeesAttendancesOfTodayForEmployee(int idEmployee)
        {
            using (var context = new AppDbContext())
            {
                // Get today's date
                var today = DateOnly.FromDateTime(DateTime.Today);

                // Query the database for attendances with today's date
                var todaysAttendances = context.Attendances
                    .Where(a => a.EmployeeId == idEmployee)
                    .Include(a => a.Employee)
                    .Include(a => a.User)
                    .ToList();

                // Set the data source of the grid control to today's attendances
                gridControlAttendances.DataSource = todaysAttendances;
            }
        }

        private void AddEditAttendance_Load(object sender, EventArgs e)
        {
            txtScanEmployeeCard.Select();

            this.getEmployees();

            this.initDocumentViewer();

            if (this.type != "Add")
            {
                this.edit();
            }
            using (AppDbContext AppDb = new AppDbContext())
            {

                txtEmployee.Properties.DataSource = AppDb.Employees.ToList();

            }



            this.getEmployeesAttendancesOfToday();

            this.InitializeSearchLookUpEdit();
        }

        public void initDocumentViewer()
        {
            EmployeeCard employeeCard = new EmployeeCard(0);
            employeeCard.CreateDocument();

            documentViewer.PrintingSystem = employeeCard.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        public void ClearData()
        {
            initDocumentViewer();
            txtEmployee.Clear();
            txtCivility.Clear();
            txtBloodGroup.Clear();
            txtDateOfBirth.Clear();
            txtEmail.Clear();
            txtFamilySituation.Clear();
            txtFirstName.Clear();
            txtGender.Clear();
            txtImage.Image = null;
            txtLastName.Clear();
            txtLastPromotionDate.Clear();
            txtNameOfFather.Clear();
            txtNameOfMother.Clear();
            txtScanEmployeeCard.Clear();
            txtSickDays.Clear();
            txtVacationDays.Clear();
            txtYear.Clear();
            
        }
        private void btnSupplierReset_Click(object sender, EventArgs e)
        {


            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
            btnAddComplaints.Enabled = false;
            ClearData();
            using (AppDbContext AppDb = new AppDbContext())
            {
                gridControlAttendances.DataSource = AppDb.Attendances.Include(x => x.Employee).ToList();
            }
            Function.Sound.Added();
        }

        private Models.Attendance GetCurrentData(int idEmployee)
        {
            using (var context = new AppDbContext())
            {
                if (idEmployee != 0)
                {
                    Models.Attendance current = context.Attendances.SingleOrDefault(x => x.EmployeeId == idEmployee && x.CreatedAt.Value == DateTime.Today);
                    employeeId = (int)current.EmployeeId;
                    btnAddComplaints.Enabled = true;
                    return current;
                }

                else
                    return null;
            }
        }
        public void LoadcomplaintsEmployee(int idEmployee)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                grcComplaints.DataSource = AppDb.Complaints.Where(x => x.EmployeeId == idEmployee)
                    .Include(c => c.ComplaintCate)
                    .ToList();
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
                int minId = context.Employees.Min(b => b.Id);
                int maxId = context.Employees.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                //Models.Attendance currentItem = GetCurrentData(currentItemId);
                employeeId = currentItemId;
                txtEmployee.EditValue = employeeId;

                this.currentItemId = employeeId;
                this.PreviewEmployeeCard(employeeId);
                this.getEmployeeDetails(employeeId);



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

                    int? minId = context.Employees.Min(b => (int?)b.Id);
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
                    int? maxId = context.Employees.Max(b => (int?)b.Id);
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
                else
                {
                    var nextItem = context.Employees.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                    if (nextItem != null)
                    {
                        currentItemId = nextItem.Id;
                        DisplayCurrentItem();
                    }
                }


            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    var prevItem = context.Employees.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
                searchLookUpEdit.Properties.DataSource = context.Employees.ToList();
                searchLookUpEdit.Properties.DisplayMember = "FirstName";
                searchLookUpEdit.Properties.ValueMember = "Id";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("FirstName", firstName);
                searchLookUpEdit.Properties.View.Columns.AddVisible("LastName", lastName);

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
                    this.PreviewEmployeeCard(this.currentItemId);
                    this.getEmployeeDetails(this.currentItemId);
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

        private void txtScanEmployeeCard_EditValueChanged(object sender, EventArgs e)
        {
            // Restart the timer every time the user types something
            searchTimer.Stop();
            searchTimer.Start();

            txtScanEmployeeCard.Text = string.Empty;
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        public void getEmployeeDetails(int? employeeId)
        {
            using (var context = new AppDbContext())
            {
                Models.Employee employee = context.Employees.Find(employeeId);

                if (employee != null)
                {
                    txtImage.EditValue = employee.Image;
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtNameOfFather.Text = employee.NameOfFather;
                    txtNameOfMother.Text = employee.NameOfMother;
                    txtEmail.Text = employee.Email;
                    txtGender.EditValue = employee.Gender;
                    txtVacationDays.EditValue = employee.VacationDays;
                    txtSickDays.EditValue = employee.SickDays;
                    txtLastPromotionDate.EditValue = employee.LastPromotionDate;
                    txtFamilySituation.EditValue = employee.FamilySituation;
                    txtCivility.EditValue = employee.Civility;
                    txtBloodGroup.EditValue = employee.Civility;
                    txtDateOfBirth.EditValue = employee.DateOfBirth;

                    this.PreviewEmployeeCard(employee.Id);
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void PreviewEmployeeCard(int? employeeId)
        {
            EmployeeCard employeeCard = new EmployeeCard((int)employeeId); // Adjust parameter as required
            employeeCard.CreateDocument(); // Ensure this method is implemented or adjust as necessary

            documentViewer.PrintingSystem = employeeCard.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        private void PerformSearch()
        {
            string code = txtScanEmployeeCard.Text.Trim();

            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    var employee = GetEmployeeByCode(code);

                    if (employee != null)
                    {
                        btnAddComplaints.Enabled = true;
                        this.HandleEmployeeScan(employee.Id);
                        this.PreviewEmployeeCard(employee.Id);
                 
                        LoadcomplaintsEmployee((int)employee.Id);
                        this.getEmployeesAttendancesOfToday();
                    }
                    else
                    {
                        MessageBox.Show("Employee not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtScanEmployeeCard.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while searching for the employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtScanEmployeeCard.Clear();
            }

            txtScanEmployeeCard.Select();
        }

        private Models.Employee GetEmployeeByCode(string code)
        {
            using (var context = new AppDbContext())
            {
                return context.Employees.FirstOrDefault(e => e.Code == code);
            }
        }

        private void HandleEmployeeScan(int employeeId)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            using (var context = new AppDbContext())
            {
                var existingAttendance = context.Attendances
                    .FirstOrDefault(a => a.EmployeeId == employeeId && a.Date == currentDate);

                if (existingAttendance == null)
                {
                    // If no existing attendance record, perform Scan In
                    scanEmployeeIn(employeeId);
                }
                else if (existingAttendance.TimeOut == null)
                {
                    // If there is an attendance record but no TimeOut, perform Scan Out
                    scanEmployeeOut(employeeId);
                }
                else
                {
                    // If there is an attendance record with TimeOut, the employee has already scanned out
                    MessageBox.Show("Employee has already scanned out today.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtScanEmployeeCard.Clear();
                }
            }
        }

        private void scanEmployeeIn(int employeeId)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);


            txtEmployee.EditValue = employeeId;
            txtScanEmployeeCard.Clear();
            Function.Sound.Added();

            using (var context = new AppDbContext())
            {
                var newAttendance = new Models.Attendance
                {
                    EmployeeId = employeeId,
                    UserId = Properties.Settings.Default.userId,
                    Date = currentDate,
                    TimeIn = currentTime,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                context.Attendances.Add(newAttendance);
                context.SaveChanges();
            }
        }

        private void scanEmployeeOut(int employeeId)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

            // Update the UI controls

            txtEmployee.EditValue = employeeId;
            txtScanEmployeeCard.Clear();
            Function.Sound.Added();

            using (var context = new AppDbContext())
            {
                var existingAttendance = context.Attendances
                    .FirstOrDefault(a => a.EmployeeId == employeeId && a.Date == currentDate);

                if (existingAttendance != null)
                {
                    existingAttendance.TimeOut = currentTime;
                    existingAttendance.UpdatedAt = DateTime.Now;

                    if (existingAttendance.TimeIn.HasValue && existingAttendance.TimeOut.HasValue)
                    {
                        TimeSpan timeIn = existingAttendance.TimeIn.Value.ToTimeSpan();
                        TimeSpan timeOut = existingAttendance.TimeOut.Value.ToTimeSpan();
                        existingAttendance.HoursWorked = CalculateHoursWorked(timeIn, timeOut);
                    }

                    context.SaveChanges();
                }
            }
        }

        private decimal CalculateHoursWorked(TimeSpan timeIn, TimeSpan timeOut)
        {
            // Calculate the duration between TimeIn and TimeOut
            TimeSpan duration = timeOut - timeIn;

            // Convert the duration to total hours
            decimal hoursWorked = (decimal)duration.TotalHours;

            return Math.Round(hoursWorked, 2); // Round to 2 decimal places
        }

        private void btnRefreshAttendances_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getEmployeesAttendancesOfToday();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

   
        private void grvComplaints_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                complaintsId = int.Parse(grvComplaints.GetRowCellValue(grvComplaints.FocusedRowHandle, "Id").ToString());
            }
        }
     
        private void btnAddComplaints_Click(object sender, EventArgs e)
        {
            Forms.Employee.Complaints.AddEditComplaints addEditComplaints = new Complaints.AddEditComplaints();
            addEditComplaints.SetEmployees(employeeId);
            addEditComplaints.ShowDialog();
            LoadcomplaintsEmployee(employeeId);
        }

        private void txtEmployee_EditValueChanged(object sender, EventArgs e)
        {
            if ((txtEmployee.EditValue != null) && !(string.IsNullOrEmpty(txtEmployee.EditValue.ToString())))
            {
                BindDataToGrid(1, null, null);
                btnAddComplaints.Enabled = true;
                LoadcomplaintsEmployee(int.Parse(txtEmployee.EditValue.ToString()));
            }
                
            //var today = DateOnly.FromDateTime(DateTime.Today);
            //IQueryable<Models.Attendance> filteredAttendance = Attendanceliste;

            //using (AppDbContext AppDb = new AppDbContext())
            //{
            //    if ((txtEmployee.EditValue != null) && !(string.IsNullOrEmpty(txtEmployee.EditValue.ToString())))
            //    {
            //        filteredAttendance = filteredAttendance.Where(x => x.EmployeeId == int.Parse(txtEmployee.EditValue.ToString()));
            //    }
            //    if ((txtYear.EditValue != null) && !(string.IsNullOrEmpty(txtYear.EditValue.ToString())))
            //    {
            //        filteredAttendance = filteredAttendance.Where(x => x.Date.Value.Year == int.Parse(txtYear.EditValue.ToString()));
            //    }
            //    gridControlAttendances.DataSource = filteredAttendance.ToList();  // Ensure to convert IQueryable to a list or binding list
            //    gridControlAttendances.RefreshDataSource();
            //}

        }

        private void btnJanuary_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 1, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnFebruary_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 2, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnMarch_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 3, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnApril_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 4, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnMay_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 5, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnJune_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 6, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnJuly_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 7, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnAugust_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 8, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnSeptember_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 9, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnOctober_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 10, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnNovember_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 11, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);
        }

        private void btnDecember_Click(object sender, EventArgs e)
        {
            DateTime year;
            DateTime date;
            if (DateTime.TryParse(txtYear.Text, out date))
            {
                year = date;
            }
            else
            {
                year = DateTime.Now;
            }
            DateTime startDate = new DateTime(year.Year, 12, 1);
            DateTime endDate = startDate.AddMonths(1);
            BindDataToGrid(1, startDate, endDate);

        }
        private void BindDataToGrid(int pageNumber, DateTime? startDate = null, DateTime? endDate = null)
        {

            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var query = context.Attendances.AsQueryable();

                // Apply search term filter
                //if (!string.IsNullOrEmpty(searchTerm))
                //{
                //    query = query.Where(p =>
                //                           p.Employee.LastName.Contains(searchTerm) ||
                //                           p.Employee.FirstName.Contains(searchTerm));
                //}
                if ((txtEmployee.EditValue != null) && !(string.IsNullOrEmpty(txtEmployee.EditValue.ToString())))
                {
                    query = query.Where(p => p.EmployeeId == int.Parse(txtEmployee.EditValue.ToString()));
                }
                // Apply date filter if provided
                if (startDate.HasValue && endDate.HasValue)
                {
                    // Convert DateTime to DateOnly for comparison
                    var startDateOnly = DateOnly.FromDateTime(startDate.Value);
                    var endDateOnly = DateOnly.FromDateTime(endDate.Value);

                    query = query.Where(p => p.Date >= startDateOnly && p.Date < endDateOnly);
                }
                if (txtYear.EditValue != null && DateTime.TryParse(txtYear.EditValue.ToString(), out DateTime selectedYear))
                {
                    // Filtrer les enregistrements en fonction de l'année sélectionnée
                    query = query.Where(x => x.Date.HasValue && x.Date.Value.Year == selectedYear.Year);

                   
                }
                query = query.Include(u => u.User);

                var currentPageData = query
                    .OrderByDescending(p => p.Id).Include(x=>x.Employee)
                    //.Skip(startRecord)
                    //.Take(itemsPerPage)
                    .ToList();

                gridControlAttendances.DataSource = currentPageData;
            }
        }


        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl1.Text.Trim();
        }

        private void txtYear_EditValueChanged(object sender, EventArgs e)
        {
            BindDataToGrid(1,null,null);
        }
    }
}