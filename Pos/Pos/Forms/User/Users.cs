using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Function;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Columns;
using Pos.Models;
using Microsoft.EntityFrameworkCore;

namespace Pos.Forms.User
{
    public partial class Users : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;
        public int user_id = 0;
        private OverlayForm overlay;

        private void ShowOverlay()
        {
            if (overlay == null)
            {
                overlay = new OverlayForm(this);
                overlay.Show();
            }
        }

        private void HideOverlay()
        {
            if (overlay != null)
            {
                overlay.Close();
                overlay.Dispose();
                overlay = null;
            }
        }

        public Users()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewUsers.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewUsers.RowStyle += gridViewUsers_RowStyle;
            gridViewUsers.FocusedRowChanged += gridViewUsers_FocusedRowChanged;
            gridViewUsers.CustomDrawCell += gridViewUsers_CustomDrawCell;
            gridViewUsers.RowHeight = Function.Helper.RowHeight;
            gridViewUsers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewUsers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewUsers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewUsers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewUsers.FocusedRowHandle && e.Column == gridViewUsers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewUsers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
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
                if (gridViewUsers.RowCount > 0 && gridViewUsers.FocusedRowHandle >= 0)
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
            userEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add User"))
            {
                AddEditUser user = new AddEditUser();
                user.setUsersObject(this);
                user.setTypeOperation("Add");
                user.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            userDelete();
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
                ApplyCustomFont();
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

            btnRefreshItems.Font = customFont;
            btnDeleteItem.Font = customFont;
            btnPrintItems.Font = customFont;
            btnEditItem.Font = customFont;
            btnAddItem.Font = customFont;
            simpleButton1.Font = customFont;
            btnUsersLastMonth.Font = customFont;
            btnTodayUsers.Font = customFont;
            btnUsersThisYear.Font = customFont;
            btnUsersOfTheWeek.Font = customFont;
            btnUsersOfTheMonth.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;
            layoutControlGroup8.AppearanceGroup.Font = customFont10;
            layoutControlGroup9.AppearanceGroup.Font = customFont10;
            layoutControlGroup10.AppearanceGroup.Font = customFont10;
            layoutControlGroup12.AppearanceGroup.Font = customFont10;
            layoutControlGroup13.AppearanceGroup.Font = customFont10;
            layoutControlGroup14.AppearanceGroup.Font = customFont10;
            layoutControlGroup15.AppearanceGroup.Font = customFont10;
            layoutControlGroup16.AppearanceGroup.Font = customFont10;
            layoutControlGroup17.AppearanceGroup.Font = customFont10;
            layoutControlGroup18.AppearanceGroup.Font = customFont10;
            layoutControlGroup19.AppearanceGroup.Font = customFont10;
            currentPageLabel.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem21.AppearanceItemCaption.Font = customFont9;
            layoutControlItem20.AppearanceItemCaption.Font = customFont9;
            layoutControlItem19.AppearanceItemCaption.Font = customFont9;
            layoutControlItem18.AppearanceItemCaption.Font = customFont9;
            layoutControlItem17.AppearanceItemCaption.Font = customFont9;
            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;
            layoutControlItem26.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewUsers.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
        }

        private void ApplyFontToAllControls(Control parentControl, Font customFont)
        {
            foreach (Control control in parentControl.Controls)
            {
                control.Font = customFont;

                // Recursively apply font to child controls
                if (control.Controls.Count > 0)
                {
                    ApplyFontToAllControls(control, customFont);
                }
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

        public void ShowRibbon(bool show = false)
        {
            ribbonUsers.Visible = show;
        }

        private void Users_Load(object sender, EventArgs e)
        {
            btnUserEdit.Enabled = false;
            btnUserDelete.Enabled = false;
            this.switchBtns();
            this.loadUsers();
        }

        public void loadUsers()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Users
                .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.FullName.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm))
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
                var currentPageData = context.Users.Include(l => l.BusinessLocation)
                    .Where(p => p.FirstName.Contains(searchTerm) ||
                        p.LastName.Contains(searchTerm) ||
                        p.FullName.Contains(searchTerm) ||
                        p.Email.Contains(searchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlUsers.DataSource = currentPageData;
                UpdatePageLabel();
                UpdateNavigationButtons();
            }
        }

        private void UpdatePageLabel()
        {
            currentPageLabel.Text = $"Page {currentPage} of {totalPages}";
        }

        private void UpdateNavigationButtons()
        {
            NavPrevPage.Enabled = currentPage > 1;
            NavFirstPage.Enabled = currentPage > 1;
            NavNextPage.Enabled = currentPage < totalPages;
            NavLastPage.Enabled = currentPage < totalPages;
        }

        public void userEdit()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select a user.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner un utilisateur.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد مستخدم.";
            }

            if (Function.Permission.HasPermission("Edit User"))
            {
                if (this.user_id != 0)
                {
                    AddEditUser user = new AddEditUser();
                    user.setUsersObject(this);
                    user.setTypeOperation("Edit");
                    user.ShowDialog();
                }
                else
                {
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void userDelete()
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string cantDeleteThisUser;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to delete user ?";
                cantDeleteThisUser = "You can't delete this user !";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir supprimer l'utilisateur ?";
                cantDeleteThisUser = "Vous ne pouvez pas supprimer cet utilisateur !";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في حذف المستخدم؟";
                cantDeleteThisUser = "لا يمكنك حذف هذا المستخدم!";
            }

            if (Function.Permission.HasPermission("Delete User"))
            {
                using (var context = new AppDbContext())
                {
                    btnUserEdit.Enabled = true;
                    btnUserDelete.Enabled = true;

                    this.user_id = int.Parse(gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, "Id").ToString());

                    int currentUserId = Properties.Settings.Default.userId;

                    if (currentUserId != this.user_id)
                    {
                        Models.User user = context.Users.Find(this.user_id);

                        if (user != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            context.Users.Remove(user);
                            context.SaveChanges();
                            this.loadUsers();
                            Function.Sound.Deleted();

                            ShowOverlay();
                            ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                            showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                            showCustomAlert.trnsMsgSuccess();
                            showCustomAlert.ShowDialog();
                            HideOverlay();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                        }
                    }
                    else
                    {
                        AlertMessageBox alertMessage = new AlertMessageBox(cantDeleteThisUser);
                        alertMessage.ShowDialog();
                    }
                }
            }
        }

        private void btnAddUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditUser user = new AddEditUser();
            user.setUsersObject(this);
            user.setTypeOperation("Add");
            user.ShowDialog();
        }

        private void btnUserEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.userEdit();
        }

        private void btnUserDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.userDelete();
        }

        private void btnUserRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnUserEdit.Enabled = false;
            btnUserDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadUsers();
        }

        private void gridViewUsers_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewUsers.RowCount > 0 && gridViewUsers.FocusedRowHandle >= 0)
            {
                btnUserEdit.Enabled = true;
                btnUserDelete.Enabled = true;

                this.user_id = int.Parse(gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.userEdit();
            }
        }

        private void gridViewUsers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewUsers.RowCount > 0 && gridViewUsers.FocusedRowHandle >= 0)
            {
                Function.Sound.Selected();
                btnUserEdit.Enabled = true;
                btnUserDelete.Enabled = true;

                this.user_id = int.Parse(gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, "Id").ToString());
                this.loadUser();
            }

        }

        private void repEditUser_Click(object sender, EventArgs e)
        {
            if (gridViewUsers.RowCount > 0 && gridViewUsers.FocusedRowHandle >= 0)
            {
                btnUserEdit.Enabled = true;
                btnUserDelete.Enabled = true;

                this.user_id = int.Parse(gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.userEdit();
            }

        }

        private void repDeleteUser_Click(object sender, EventArgs e)
        {
            this.userDelete();
        }

        private void repUserPermissions_Click(object sender, EventArgs e)
        {
            if (gridViewUsers.RowCount > 0 && gridViewUsers.FocusedRowHandle >= 0)
            {
                this.user_id = int.Parse(gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, "Id").ToString());
                AddEditUserPermissions addEditUserPermissions = new AddEditUserPermissions();
                addEditUserPermissions.setUsersObject(this);
                addEditUserPermissions.ShowDialog();
            }
        }

        private void btnPrintUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlUsers);
            customPrint.PrintGridControl(gridViewUsers);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add User"))
            {
                AddEditUser user = new AddEditUser();
                user.setUsersObject(this);
                user.setTypeOperation("Add");
                user.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.userEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            this.userDelete();
        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlUsers);
            customPrint.PrintGridControl(gridViewUsers);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnUserEdit.Enabled = false;
            btnUserDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadUsers();
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void switchBtns()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
            var endOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);  // Corrected to include the last day of the month
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);  // Week ends on Sunday
            var startOfYear = new DateTime(today.Year, 1, 1);
            var endOfYear = new DateTime(today.Year, 12, 31);

            int countbtnUsersLastMonth = 0;
            int countbtnUsersOfTheMonth = 0;
            int countbtnUsersOfTheWeek = 0;
            int countbtnTodayUsers = 0;
            int countbtnUsersThisYear = 0;

            string lang = Properties.Settings.Default.Lang;

            string msgCountbtnUsersLastMonth = "Users Last Month";
            string msgCountbtnUsersOfTheMonth = "Users Of The Month";
            string msgCountbtnUsersOfTheWeek = "Users Of The Week";
            string msgCountbtnTodayUsers = "Today's Users";
            string msgCountbtnUsersThisYear = "Users This Year";

            if (lang == "fr")
            {
                msgCountbtnUsersLastMonth = "Utilisateurs le mois dernier";
                msgCountbtnUsersOfTheMonth = "Utilisateurs du mois";
                msgCountbtnUsersOfTheWeek = "Utilisateurs de la semaine";
                msgCountbtnTodayUsers = "Utilisateurs d'aujourd'hui";
                msgCountbtnUsersThisYear = "Utilisateurs de cette année";
            }
            else if (lang == "ar")
            {
                msgCountbtnUsersLastMonth = "المستخدمون في الشهر الماضي";
                msgCountbtnUsersOfTheMonth = "مستخدمو الشهر";
                msgCountbtnUsersOfTheWeek = "مستخدمو الأسبوع";
                msgCountbtnTodayUsers = "مستخدمو اليوم";
                msgCountbtnUsersThisYear = "مستخدمو هذا العام";
            }

            using (var context = new AppDbContext())
            {
                countbtnUsersLastMonth = context.Users
                    .Where(c => c.CreatedAt >= startOfLastMonth && c.CreatedAt <= endOfLastMonth)
                    .OrderByDescending(p => p.Id)
                    .Count();

                btnUsersLastMonth.Text = "( " + countbtnUsersLastMonth + " ) " + msgCountbtnUsersLastMonth;

                countbtnUsersOfTheMonth = context.Users
                    .Where(c => c.CreatedAt >= startOfMonth && c.CreatedAt <= endOfMonth)
                    .OrderByDescending(p => p.Id)
                    .Count();

                btnUsersOfTheMonth.Text = "( " + countbtnUsersOfTheMonth + " ) " + msgCountbtnUsersOfTheMonth;

                countbtnUsersOfTheWeek = context.Users
                    .Where(c => c.CreatedAt >= startOfWeek && c.CreatedAt <= endOfWeek)
                    .OrderByDescending(p => p.Id)
                    .Count();

                btnUsersOfTheWeek.Text = "( " + countbtnUsersOfTheWeek + " ) " + msgCountbtnUsersOfTheWeek;

                countbtnTodayUsers = context.Users
                    .Where(c => c.CreatedAt == today)
                    .OrderByDescending(p => p.Id)
                    .Count();

                btnTodayUsers.Text = "( " + countbtnTodayUsers + " ) " + msgCountbtnTodayUsers;

                countbtnUsersThisYear = context.Users
                    .Where(c => c.CreatedAt >= startOfYear && c.CreatedAt <= endOfYear)
                    .OrderByDescending(p => p.Id)
                    .Count();

                btnUsersThisYear.Text = "( " + countbtnUsersThisYear + " ) " + msgCountbtnUsersThisYear;
            }
        }

        private void btnUsersOfTheWeek_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7);

                gridControlUsers.DataSource = context.Users.Include(l => l.BusinessLocation)
                    .Where(c => c.CreatedAt >= startOfWeek && c.CreatedAt < endOfWeek)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnUsersOfTheMonth_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1);

                gridControlUsers.DataSource = context.Users.Include(l => l.BusinessLocation)
                    .Where(c => c.CreatedAt >= startOfMonth && c.CreatedAt < endOfMonth)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnUsersThisYear_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                // Play the selected sound
                Sound.Selected();

                // Get today's date
                var today = DateTime.Today;
                var startOfDay = today;
                var endOfDay = today.AddDays(1).AddTicks(-1); // Get end of today's date

                // Fetch users created today
                gridControlUsers.DataSource = context.Users
                    .Include(l => l.BusinessLocation)
                    .Where(c => c.CreatedAt >= startOfDay && c.CreatedAt <= endOfDay) // Query for today's data
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnTodayUsers_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;

                gridControlUsers.DataSource = context.Users.Include(l => l.BusinessLocation)
                    .Where(c => c.CreatedAt == today)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnUsersLastMonth_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                var today = DateTime.Today;
                var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                var endOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1); // Last day of last month

                gridControlUsers.DataSource = context.Users.Include(l => l.BusinessLocation)
                    .Where(c => c.CreatedAt >= startOfLastMonth && c.CreatedAt <= endOfLastMonth)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        private void btnUserDetails_Click(object sender, EventArgs e)
        {
            this.userEdit();
        }

        public void loadUser()
        {
            if (this.user_id != 0)
            {
                using (var context = new AppDbContext())
                {
                    Models.User user = context.Users.Find(this.user_id);

                    if (user != null)
                    {
                        txtFullName.Text = user.FirstName;
                        txtGender.Text = user.Gender;
                        txtEmail.Text = user.Email;
                        userImage.EditValue = user.Image;
                        txtRole.Text = user.IsAdmin;
                        txtStatus.Text = user.Status;
                        txtGender.Text = user.Gender;
                        txtCreatedAt.Text = user.CreatedAt.ToString();
                    }
                }
            }
        }

        private Models.User GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (this.user_id != 0)
                    return context.Users.Find(this.user_id);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                if (this.user_id == 0)
                {
                    // If no current selection, disable all navigation buttons.
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    return;
                }

                // Retrieve the minimum and maximum ID values from the Brands dataset.
                int minId = context.Users.Min(b => b.Id);
                int maxId = context.Users.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = this.user_id > minId; // Disable if on the first item
                btnNext.Enabled = this.user_id < maxId; // Disable if on the last item
                btnEnd.Enabled = this.user_id > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = this.user_id < maxId; // Disable if on the last item (end of the list)

                Models.User currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.user_id = currentItem.Id;
                    this.loadUser();
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
                    int? minId = context.Users.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        this.user_id = minId.Value;
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
                    int? maxId = context.Users.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        this.user_id = maxId.Value;
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
                if (this.user_id == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.Users.Where(b => b.Id > this.user_id).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    this.user_id = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            if (this.user_id != 0)
            {
                using (var context = new AppDbContext())
                {
                    var prevItem = context.Users.Where(b => b.Id < this.user_id).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        this.user_id = prevItem.Id;
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
                    int totalItems = context.Users.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }

        }

        private void searchControl_TextChanged(object sender, EventArgs e)
        {
            searchTerm = searchControl.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.loadUsers();
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlUsers, "csv");
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlUsers, "pdf");
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            GridExporter.Export(gridControlUsers, "xlsx");
        }
    }
}