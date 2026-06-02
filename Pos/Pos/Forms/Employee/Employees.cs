using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Pos.Forms.Overlay;
using Pos.Forms.Report;
using Pos.Function;
using Pos.Models;
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
using Stripe;
using Twilio.Types;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using Pos.Forms.Alert;
using Microsoft.EntityFrameworkCore;
using Pos.Report.Customer;
using Pos.Report.Employee;

namespace Pos.Forms.Employee
{
    public partial class Employees : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int employee_id = 0;

        public Employees()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewEmployees.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewEmployees.RowStyle += gridViewEmployees_RowStyle;
            gridViewEmployees.FocusedRowChanged += gridViewEmployees_FocusedRowChanged;
            gridViewEmployees.CustomDrawCell += gridViewEmployees_CustomDrawCell;
            gridViewEmployees.RowHeight = Function.Helper.RowHeight;
            gridViewEmployees.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewEmployees_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewEmployees.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewEmployees_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewEmployees.FocusedRowHandle && e.Column == gridViewEmployees.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewEmployees_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        private void InitializeContextMenu()
        {
            string lang = Properties.Settings.Default.Lang;

            string msgMenuItemEdit;
            string msgMenuItemAdd;
            string msgMenuItemDelete;

            if (lang == "en")
            {
                msgMenuItemEdit = "Edit";
                msgMenuItemAdd = "Add";
                msgMenuItemDelete = "Delete";
            }
            else if (lang == "fr")
            {
                msgMenuItemEdit = "Modifier";
                msgMenuItemAdd = "Ajouter";
                msgMenuItemDelete = "Supprimer";
            }
            else
            {
                msgMenuItemEdit = "تحرير";
                msgMenuItemAdd = "أضف";
                msgMenuItemDelete = "حذف";
            }

            contextMenu = new ContextMenuStrip();
            menuItemEdit = new ToolStripMenuItem(msgMenuItemEdit);
            menuItemAdd = new ToolStripMenuItem(msgMenuItemAdd);
            menuItemDelete = new ToolStripMenuItem(msgMenuItemDelete);

            menuItemEdit.Image = Properties.Resources.rightclick_suitcase;
            menuItemAdd.Image = Properties.Resources.rightclick_case_study;
            menuItemDelete.Image = Properties.Resources.rightclick_remove;

            contextMenu.Items.AddRange(new ToolStripItem[] { menuItemEdit, menuItemAdd, menuItemDelete });

            menuItemEdit.Click += MenuItemEdit_Click;
            menuItemAdd.Click += MenuItemAdd_Click;
            menuItemDelete.Click += MenuItemDelete_Click;
        }

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

                    if (hitInfo.InRow)
                    {
                        view.FocusedRowHandle = hitInfo.RowHandle;
                        contextMenu.Show(view.GridControl, e.Location);
                    }
                }

            }
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            employeeEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee"))
            {
                AddEditEmployee employee = new AddEditEmployee();
                employee.setEmployeesObject(this);
                employee.setTypeOperation("Add");
                employee.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            employeeDelete();
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

            btnCloseFrm.Font = customFont;
            btnRefreshItems.Font = customFont;
            btnDeleteItem.Font = customFont;
            btnPrintItems.Font = customFont;
            btnEditItem.Font = customFont;
            btnAddItem.Font = customFont;
            simpleButton1.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewEmployees.Columns)
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

        private void Employees_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadEmployees();
        }
        public void printbadge(int employee_id)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string printerError;
            string isNotValid;
            string printer;
            string anErrorOccurred;
            string validationError;
            string validatedData;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to print the badge ?";
                printerError = "Printer Error";
                isNotValid = "is Not Valid";
                printer = "Printer";
                anErrorOccurred = "An error occurred while printing:";
                validationError = "Validation Error";
                validatedData = "You can't print without validated data.";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir imprimer le badge ?";
                printerError = "Erreur d'imprimante";
                isNotValid = "n'est pas valide";
                printer = "Imprimante";
                anErrorOccurred = "Une erreur s'est produite lors de l'impression :";
                validationError = "Erreur de validation";
                validatedData = "Vous ne pouvez pas imprimer sans données validées.";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في طباعة الشارة؟";
                printerError = "خطأ في الطابعة";
                isNotValid = "غير صحيح";
                printer = "طابعة";
                anErrorOccurred = "حدث خطأ أثناء الطباعة:";
                validationError = "خطأ في التحقق";
                validatedData = "لا يمكنك الطباعة بدون بيانات تم التحقق من صحتها.";
            }

            if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterReciept;

                // Initialize the TechnicalRepiarA4 report with the maintenance_id
                EmployeeCard employeeCard = new EmployeeCard(employee_id);
                employeeCard.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"{printer} \"{printerName}\" {isNotValid}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(employeeCard);

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
                FormView documentViewerForm = new FormView();
                documentViewerForm.EmployeeBadge(employeeCard);
                documentViewerForm.Show();
            }
            else
            {
                XtraMessageBox.Show(validatedData, validationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void loadEmployees()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Employees.Include(x => x.Department).Include(y => y.Position)
                .Where(p => p.FirstName.Contains(searchTerm) ||
                p.LastName.Contains(searchTerm) ||
                p.NameOfFather.Contains(searchTerm) ||
                p.NameOfMother.Contains(searchTerm) ||
                p.Address.Contains(searchTerm) ||
                p.FamilySituation.Contains(searchTerm) ||
                p.BloodGroup.Contains(searchTerm) ||
                p.Civility.Contains(searchTerm) ||
                p.SpecialMarque.Contains(searchTerm) ||
                p.Email.Contains(searchTerm) ||
                p.PhoneNumber.Contains(searchTerm) ||
                p.NoCard.Contains(searchTerm) ||
                p.NoPass.Contains(searchTerm) ||
                p.SpouseName.Contains(searchTerm) ||
                p.ShortBiography.Contains(searchTerm) ||
                p.EmergencyContactPhone.Contains(searchTerm) ||
                p.EmergencyContactRelation.Contains(searchTerm) ||
                p.EmergencyContactName.Contains(searchTerm) ||
                p.EducationLevel.Contains(searchTerm) ||
                p.ExperienceYears.Contains(searchTerm) ||
                p.PreviousEmployer.Contains(searchTerm) ||
                p.Certifications.Contains(searchTerm) ||
                p.LanguagesSpoken.Contains(searchTerm) ||
                p.Skills.Contains(searchTerm) ||
                p.LinkedInProfile.Contains(searchTerm) ||
                p.EmploymentType.Contains(searchTerm) ||
                p.Note.Contains(searchTerm) ||
                p.Gender.Contains(searchTerm) ||
                p.CodePostal.Contains(searchTerm) ||
                p.Code.Contains(searchTerm))
                .Count();
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // Load the first page of data
                BindDataToGrid(currentPage);
            }
        }

        private void BindDataToGrid(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * itemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.Employees
                .Where(p => p.FirstName.Contains(searchTerm) ||
                p.LastName.Contains(searchTerm) ||
                p.NameOfFather.Contains(searchTerm) ||
                p.NameOfMother.Contains(searchTerm) ||
                p.Address.Contains(searchTerm) ||
                p.FamilySituation.Contains(searchTerm) ||
                p.BloodGroup.Contains(searchTerm) ||
                p.Civility.Contains(searchTerm) ||
                p.SpecialMarque.Contains(searchTerm) ||
                p.Email.Contains(searchTerm) ||
                p.PhoneNumber.Contains(searchTerm) ||
                p.NoCard.Contains(searchTerm) ||
                p.NoPass.Contains(searchTerm) ||
                p.SpouseName.Contains(searchTerm) ||
                p.ShortBiography.Contains(searchTerm) ||
                p.EmergencyContactPhone.Contains(searchTerm) ||
                p.EmergencyContactRelation.Contains(searchTerm) ||
                p.EmergencyContactName.Contains(searchTerm) ||
                p.EducationLevel.Contains(searchTerm) ||
                p.ExperienceYears.Contains(searchTerm) ||
                p.PreviousEmployer.Contains(searchTerm) ||
                p.Certifications.Contains(searchTerm) ||
                p.LanguagesSpoken.Contains(searchTerm) ||
                p.Skills.Contains(searchTerm) ||
                p.LinkedInProfile.Contains(searchTerm) ||
                p.EmploymentType.Contains(searchTerm) ||
                p.Note.Contains(searchTerm) ||
                p.Gender.Contains(searchTerm) ||
                p.CodePostal.Contains(searchTerm) ||
                p.Code.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                     .Select(p => new // Projection des champs nécessaires
                     {
                         p.Id,
                         p.FirstName,
                         p.LastName,
                         p.Email,
                         p.Gender,
                         p.HireDate,
                         p.Status,
                         p.PhoneNumber,
                         PositionTitle = p.Position.Title, // Sélection du champ `Title` de la table `Position`
                         DepartementName = p.Department.DepartmentName
                     })
                    .ToList();

                gridControlEmployees.DataSource = currentPageData;
                UpdatePageLabel();
                UpdateNavigationButtons();
            }
        }

        private void UpdatePageLabel()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            currentPageLabel.Text = $"{page} {currentPage} {of} {totalPages}";
        }

        private void UpdateNavigationButtons()
        {
            NavPrevPage.Enabled = currentPage > 1;
            NavFirstPage.Enabled = currentPage > 1;
            NavNextPage.Enabled = currentPage < totalPages;
            NavLastPage.Enabled = currentPage < totalPages;
        }

        public void employeeEdit()
        {
            if (Function.Permission.HasPermission("Edit Employee"))
            {
                if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    AddEditEmployee employee = new AddEditEmployee();

                    // This ensures the overlay form is displayed behind the modal form but above the parent form
                    employee.FormClosed += (s, args) => overlay.Close();

                    employee.setEmployeesObject(this);
                    employee.setTypeOperation("Edit");
                    employee.Show();
                    employee.TopMost = true;
                }

            }
        }

        public void employeeDelete()
        {
            if (Function.Permission.HasPermission("Delete Employee"))
            {
                if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    string lang = Properties.Settings.Default.Lang;

                    string confirmation;
                    string areYouSure;

                    if (lang == "en")
                    {
                        confirmation = "Confirmation";
                        areYouSure = "Are you sure want to delete Item ?";
                    }
                    else if (lang == "fr")
                    {
                        confirmation = "Confirmation";
                        areYouSure = "Etes-vous sûr de vouloir supprimer l'élément ?";
                    }
                    else
                    {
                        confirmation = "التأكيد";
                        areYouSure = "هل أنت متأكد من رغبتك في حذف العنصر؟";
                    }

                    using (var context = new AppDbContext())
                    {
                        this.employee_id = int.Parse(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "Id").ToString());

                        Models.Employee employee = context.Employees.Find(this.employee_id);

                        if (employee != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.Employees.Remove(employee);
                            context.SaveChanges();
                            this.loadEmployees();
                            Function.Sound.Deleted();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                        }
                    }
                }
            }
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee"))
            {
                AddEditEmployee employee = new AddEditEmployee();
                employee.setEmployeesObject(this);
                employee.setTypeOperation("Add");
                employee.ShowDialog();
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.employeeEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.employeeDelete();
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadEmployees();
        }

        private void gridViewEmployees_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.employee_id = int.Parse(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.employeeEdit();
            }
        }

        private void gridViewEmployees_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.employee_id = int.Parse(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void repEditEmployee_Click(object sender, EventArgs e)
        {
            if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.employee_id = int.Parse(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.employeeEdit();
            }
        }

        private void repDeleteEmployee_Click(object sender, EventArgs e)
        {
            this.employeeDelete();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlEmployees);
            customPrint.PrintGridControl(gridViewEmployees);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Employee"))
            {
                AddEditEmployee employee = new AddEditEmployee();
                employee.setTypeOperation("Add");
                employee.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a employee to add or update.";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un employee à ajouter ou à mettre à jour.";
            }
            else
            {
                pleaseSelect = "الرجاء اختيار موظف لإضافته أو تحديثه";
            }

            if (this.employee_id != 0)
            {
                this.employeeEdit();
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelect;

            if (lang == "en")
            {
                pleaseSelect = "Please select a employee to delete";
            }
            else if (lang == "fr")
            {
                pleaseSelect = "Veuillez sélectionner un employé à supprimer.";
            }
            else
            {
                pleaseSelect = "الرجاء تحديد موظف لحذفه";
            }

            if (this.employee_id != 0)
            {
                this.employeeDelete();
                this.employee_id = 0;
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelect);
                alertMessage.ShowDialog();
            }

        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.printEmployeesA4();
        }

        public void printEmployeesA4()
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

            if (gridViewEmployees.RowCount != 0)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterDocument;

                // Initialize the TechnicalRepiarA4 report with the maintenance_id
                EmployeeList employeeList = new EmployeeList();
                employeeList.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"{printer} \"{printerName}\" {isNotValid}", printerError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(employeeList);

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
                //reportViewerFrm.EmployeeList(employeeList);
                //reportViewerFrm.Show();
            }
            else
            {
                XtraMessageBox.Show(validatedData, validationError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadEmployees();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NavPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                BindDataToGrid(currentPage);
            }
        }

        private void NavFirstPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                BindDataToGrid(currentPage);
            }
        }

        private void NavLastPage_Click(object sender, EventArgs e)
        {
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                BindDataToGrid(currentPage);
            }
        }

        private void NavNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                BindDataToGrid(currentPage);
            }
        }

        private void perPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page
                    int totalItems = context.Employees.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadEmployees();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployees, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployees, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlEmployees, "xlsx");
        }

        private void repoItemBtnPrintbadge_Click(object sender, EventArgs e)
        {
            if (gridViewEmployees.RowCount > 0 && gridViewEmployees.FocusedRowHandle >= 0)
            {
                this.employee_id = int.Parse(gridViewEmployees.GetRowCellValue(gridViewEmployees.FocusedRowHandle, "Id").ToString());
                this.printbadge(this.employee_id);
            }
        }
    }
}