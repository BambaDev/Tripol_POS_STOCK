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

namespace Pos.Forms.Expense
{
    public partial class AddEditExpense : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Expenses expenses = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditExpense()
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

        public void setExpensesObject(Expenses expenses)
        {
            this.expenses = expenses;
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

            if (this.expenses != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.expenses.expense_id != 0)
                        this.currentItemId = this.expenses.expense_id;

                    this.expenses.expense_id = 0;
                    Models.Expense expense = context.Expenses.Find(this.currentItemId);

                    if (expense != null)
                    {
                        txtReferenceNo.Text = expense.ReferenceNo;
                        txtAmount.EditValue = expense.Amount;
                        txtNote.Text = expense.Note;
                        txtExpenseCategoryId.EditValue = expense.ExpenseCategoryId;
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

        private void btnSaveExp_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string referenceNoAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                referenceNoAlreadyExists = "An Expense with this reference no already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'article!";
                referenceNoAlreadyExists = "Une dépense avec cette référence existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                referenceNoAlreadyExists = "يوجد بالفعل حساب بهذا المرجع.";
            }

            if (dxValidationProviderExpense.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Expense expense;
                    bool isReferenceNoExists = false;

                    if (this.type == "Add")
                    {
                        // Check if an expense with the same reference no already exists
                        isReferenceNoExists = context.Expenses.Any(e => e.ReferenceNo == txtReferenceNo.Text);

                        if (isReferenceNoExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(referenceNoAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new expense
                        expense = new Models.Expense
                        {
                            ReferenceNo = txtReferenceNo.Text,
                            Amount = decimal.Parse(txtAmount.Text),
                            Note = txtNote.Text,
                            ExpenseCategoryId = int.Parse(txtExpenseCategoryId.EditValue.ToString()),
                            UserId = 1,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Expenses.Add(expense);
                        ClearFormFields();
                    }
                    else
                    {
                        if (this.expenses != null)
                        {
                            if (this.expenses.expense_id!=0)
                            {
                                this.currentItemId = this.expenses.expense_id;
                            }
                        }
                           

                        expense = context.Expenses.Find(this.currentItemId);

                        if (expense != null)
                        {
                            // Check if the new reference no is unique excluding the current record
                            isReferenceNoExists = context.Expenses.Any(e => e.ReferenceNo == txtReferenceNo.Text && e.Id != expense.Id);

                            if (isReferenceNoExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(referenceNoAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            expense.ReferenceNo = txtReferenceNo.Text;
                            expense.Amount = decimal.Parse(txtAmount.Text);
                            expense.Note = txtNote.Text;
                            expense.ExpenseCategoryId = int.Parse(txtExpenseCategoryId.EditValue.ToString());
                            expense.UpdatedAt = DateTime.Now;

                            context.Entry(expense).State = EntityState.Modified;
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
                    if (this.expenses != null)
                    {
                        this.expenses.switchBtns();
                        this.expenses.loadExpenses();
                    }
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearFormFields()
        {
            txtReferenceNo.Text = "";
            txtAmount.Text = "";
            txtNote.Text = "";
            txtExpenseCategoryId.EditValue = null;
        }

        private void AddEditExpense_Load(object sender, EventArgs e)
        {
            this.getCategories();

            if (this.type != "Add")
            {
                this.edit();
            }
        }

        private void txtQuickCategory_Click(object sender, EventArgs e)
        {
            Category.AddEditExpenseCategory addEditExpenseCategory = new Category.AddEditExpenseCategory();
            addEditExpenseCategory.setObject(this);
            addEditExpenseCategory.ShowDialog();
        }

        public void getCategories()
        {
            using (var context = new AppDbContext())
            {
                txtExpenseCategoryId.Properties.DataSource = context.ExpenseCategories.ToList();
                txtExpenseCategoryId.Properties.DisplayMember = "Name"; // Set display member
                txtExpenseCategoryId.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void selectCategory(int id)
        {
            this.getCategories();
            txtExpenseCategoryId.EditValue = id;
        }

        private Models.Expense GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Expenses.Find(currentItemId);
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
                int minId = context.Expenses.Min(b => b.Id);
                int maxId = context.Expenses.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Expense currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtReferenceNo.Text = currentItem.ReferenceNo;
                    txtAmount.EditValue = currentItem.Amount;
                    txtNote.Text = currentItem.Note;
                    txtExpenseCategoryId.EditValue = currentItem.ExpenseCategoryId;
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
                    int? minId = context.Expenses.Min(b => (int?)b.Id);
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
                    int? maxId = context.Expenses.Max(b => (int?)b.Id);
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

                var nextItem = context.Expenses.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Expenses.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtReferenceNo.Text = string.Empty;
            txtAmount.EditValue = 0;
            txtNote.Text = string.Empty;
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}