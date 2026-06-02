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

namespace Pos.Forms.Expense.Category
{
    public partial class AddEditExpenseCategory : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public ExpenseCategories categories = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditExpenseCategory()
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
            System.Drawing.Font customFont = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
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

        public void setCategoriesObject(ExpenseCategories categories)
        {
            this.categories = categories;
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

            if (this.categories != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.categories.category_id != 0)
                        this.currentItemId = this.categories.category_id;

                    this.categories.category_id = 0;

                    Models.ExpenseCategory category = context.ExpenseCategories.Find(this.currentItemId);

                    if (category != null)
                    {
                        txtName.Text = category.Name;
                        txtCode.Text = category.Code;
                        txtStatus.EditValue = category.Status;
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

        private void btnSaveExpenseCategory_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string nameAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                nameAlreadyExists = "An Expense Category with this name already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
                nameAlreadyExists = "Une catégorie de dépenses portant ce nom existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                nameAlreadyExists = "توجد بالفعل فئة نفقات بهذا الاسم.";
            }

            if (dxValidationProviderCategory.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.ExpenseCategory category;

                    if (this.type == "Add")
                    {
                        // Check if an expense category with the same name already exists
                        if (context.ExpenseCategories.Any(c => c.Name == txtName.Text))
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new expense category
                        category = new Models.ExpenseCategory
                        {
                            Name = txtName.Text,
                            Code = txtCode.Text,
                            Status = txtStatus.Text,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.ExpenseCategories.Add(category);
                        ClearFormFields();
                    }
                    else
                    {
                        if (this.categories != null)
                        {
                            if (this.categories.category_id!=0)
                            {
                                this.currentItemId = this.categories.category_id;
                            }
                        }
                            

                        category = context.ExpenseCategories.Find(this.currentItemId);

                        if (category != null)
                        {
                            // Check if the new name is unique excluding the current record
                            if (context.ExpenseCategories.Any(c => c.Name == txtName.Text && c.Id != category.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            category.Name = txtName.Text;
                            category.Code = txtCode.Text;
                            category.Status = txtStatus.Text;
                            category.UpdatedAt = DateTime.Now;

                            context.Entry(category).State = EntityState.Modified;
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

                    if (this.obj != null)
                    {
                        if (this.obj is AddEditExpense)
                        {
                            AddEditExpense addEditExpense = (AddEditExpense)this.obj;
                            addEditExpense.selectCategory(category.Id);
                        }
                    }

                    // Reload categories
                    if (this.categories != null)
                        this.categories.loadCategories();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearFormFields()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtStatus.Text = "";
        }

        private void AddEditExpenseCategory_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
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
                int minId = context.ExpenseCategories.Min(b => b.Id);
                int maxId = context.ExpenseCategories.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.ExpenseCategory currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtName.Text = currentItem.Name;
                    txtCode.Text = currentItem.Code;
                    txtStatus.EditValue = currentItem.Status;
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
                    int? minId = context.ExpenseCategories.Min(b => (int?)b.Id);
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
                    int? maxId = context.ExpenseCategories.Max(b => (int?)b.Id);
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

                var nextItem = context.ExpenseCategories.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.ExpenseCategories.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtCode.Text = string.Empty;
            txtStatus.EditValue = "Active";
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
                searchLookUpEdit.Properties.DataSource = context.ExpenseCategories.ToList();
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
            if (this.obj != null)
            {
                if (this.obj is AddEditExpense)
                {
                    AddEditExpense addEditExpense = (AddEditExpense)this.obj;
                    addEditExpense.selectCategory(this.currentItemId);
                    Sound.Selected();
                    this.Close();
                }
            }
        }
    }
}