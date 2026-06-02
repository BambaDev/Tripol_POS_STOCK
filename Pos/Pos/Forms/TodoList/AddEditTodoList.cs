using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Expense;
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
using DevExpress.XtraLayout;

namespace Pos.Forms.TodoList
{
    public partial class AddEditTodoList : DevExpress.XtraEditors.XtraForm
    {
        public TodoLists todoLists = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditTodoList()
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

            simpleLabelItem1.AppearanceItemCaption.Font = customFont;
            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
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

        public void setTodoListsObject(TodoLists todoLists)
        {
            this.todoLists = todoLists;
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

            if (this.todoLists != null)
            {
                using (var context = new AppDbContext())
                {
                    int task_id = 0;

                    if (this.todoLists != null)
                    {
                        task_id = this.todoLists.todo_list_id;
                    }

                    if (currentItemId == 0)
                    {
                        currentItemId = task_id;
                    }

                    Models.TodoList todoList = context.TodoLists.Find(currentItemId);

                    if (todoList != null)
                    {
                        txtName.Text = todoList.Name;
                        txtDescription.Text = todoList.Description;
                        txtStartDate.EditValue = todoList.StartDate;
                        txtEndDate.EditValue = todoList.EndDate;
                        txtStatus.EditValue = todoList.Status;
                        txtUser.EditValue = todoList.AssignedTo;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderTasks.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.TodoList todoList;

                    if (this.type == "Add")
                    {
                        todoList = new Models.TodoList();
                        todoList.Name = txtName.Text;
                        todoList.Description = txtDescription.Text;
                        todoList.StartDate = DateTime.Parse(txtStartDate.EditValue.ToString());
                        todoList.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                        todoList.Status = txtStatus.Text;
                        todoList.AssignedTo = int.Parse(txtUser.EditValue.ToString());
                        todoList.UserId = Properties.Settings.Default.userId;
                        todoList.CreatedAt = DateTime.Now;
                        todoList.UpdatedAt = DateTime.Now;

                        context.TodoLists.Add(todoList);

                        txtName.Text = "";
                    }
                    else
                    {
                        int task_id = 0;

                        if (this.todoLists != null)
                        {
                            task_id = this.todoLists.todo_list_id;
                        }

                        if (currentItemId == 0)
                        {
                            currentItemId = task_id;
                        }

                        todoList = context.TodoLists.Find(currentItemId);

                        if (todoList != null)
                        {
                            todoList.Name = txtName.Text;
                            todoList.Description = txtDescription.Text;
                            todoList.StartDate = DateTime.Parse(txtStartDate.EditValue.ToString());
                            todoList.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                            todoList.Status = txtStatus.Text;
                            todoList.AssignedTo = int.Parse(txtUser.EditValue.ToString());
                            todoList.UpdatedAt = DateTime.Now;

                            context.Entry(todoList).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                        }
                    }

                    context.SaveChanges();

                    Function.Sound.Added();

                    if (this.todoLists != null)
                        this.todoLists.loadTodoLists();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditTodoList_Load(object sender, EventArgs e)
        {
            txtStartDate.EditValue = DateTime.Now;
            txtEndDate.EditValue = DateTime.Now;
            txtStatus.EditValue = "Pending";

            this.getUsers();

            if (this.type != "Add")
            {
                this.edit();
            }
        }

        public void getUsers()
        {
            using (var context = new AppDbContext())
            {
                txtUser.Properties.DataSource = context.Users.ToList();
                txtUser.Properties.DisplayMember = "FullName"; // Set display member
                txtUser.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private Models.TodoList GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.TodoLists.Find(currentItemId);
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
                int minId = context.TodoLists.Min(b => b.Id);
                int maxId = context.TodoLists.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.TodoList currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtName.Text = currentItem.Name;
                    txtDescription.Text = currentItem.Description;
                    txtStartDate.EditValue = currentItem.StartDate;
                    txtEndDate.EditValue = currentItem.EndDate;
                    txtStatus.EditValue = currentItem.Status;
                    txtUser.EditValue = currentItem.AssignedTo;
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
                    int? minId = context.TodoLists.Min(b => (int?)b.Id);
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
                    int? maxId = context.TodoLists.Max(b => (int?)b.Id);
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

                var nextItem = context.TodoLists.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.TodoLists.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtStartDate.EditValue = DateTime.Now;
            txtEndDate.EditValue = DateTime.Now;
            txtStatus.EditValue = "Pending";

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