using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Accessibility;
using Pos.Forms.Role;
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
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;

namespace Pos.Forms.User
{
    public partial class AddEditUserPermissions : DevExpress.XtraEditors.XtraForm
    {
        public Users users = null;
        public DataTable dt = new DataTable();
        public int currentItemId = 0;

        public AddEditUserPermissions()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewPermissions.RowStyle += gridViewPermissions_RowStyle;
            gridViewPermissions.FocusedRowChanged += gridViewPermissions_FocusedRowChanged;
            gridViewPermissions.CustomDrawCell += gridViewPermissions_CustomDrawCell;
            gridViewPermissions.RowHeight = Function.Helper.RowHeight;
            gridViewPermissions.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPermissions_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPermissions.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPermissions_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPermissions.FocusedRowHandle && e.Column == gridViewPermissions.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPermissions_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
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

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont10;
            layoutControlGroup7.AppearanceGroup.Font = customFont10;
            layoutControlGroup2.AppearanceGroup.Font = customFont10;
            layoutControlGroup3.AppearanceGroup.Font = customFont10;
            layoutControlGroup4.AppearanceGroup.Font = customFont10;
            layoutControlGroup5.AppearanceGroup.Font = customFont10;
            layoutControlGroup6.AppearanceGroup.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem3.AppearanceItemCaption.Font = customFont9;
            layoutControlItem1.AppearanceItemCaption.Font = customFont9;
            layoutControlItem4.AppearanceItemCaption.Font = customFont9;
            layoutControlItem5.AppearanceItemCaption.Font = customFont9;

            // Apply custom font to gridControlUsers columns
            foreach (GridColumn column in gridViewPermissions.Columns)
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

        public void setUsersObject(Users users)
        {
            this.users = users;
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

            if (this.users != null || currentItemId != 0)
            {
                using (var context = new AppDbContext())
                {
                    this.currentItemId = this.users != null ? this.users.user_id : currentItemId;

                    Models.User user = context.Users.Find(this.currentItemId);

                    if (user != null)
                    {
                        txtUserFullName.Text = user.FirstName + " " + user.LastName;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        public void loadPermissions()
        {
            using (var context = new AppDbContext())
            {
                this.currentItemId = this.users != null ? this.users.user_id : currentItemId;

                gridControlPermissions.DataSource = context.UserHasPermissions
                    .Where(p => p.UserId == this.currentItemId)
                    .Include(r => r.User)
                    //.Include(n => n.Permission)
                    .OrderByDescending(p => p.Id)
                    .ToList();
            }
        }

        public void lookUpPermissions()
        {
            using (var context = new AppDbContext())
            {
                txtPermissions.Properties.DataSource = context.Permissions.OrderByDescending(p => p.Id).ToList();
                txtPermissions.Properties.DisplayMember = "Name"; // Set display member
                txtPermissions.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void AddEditUserPermissions_Load(object sender, EventArgs e)
        {
            txtUserFullName.ReadOnly = true;
            this.lookUpPermissions();
            this.loadPermissions();
            this.edit();
        }

        private void txtPermissions_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string allreadyExists;

            if (lang == "en")
            {
                allreadyExists = "The Permission Allready Exists !";
            }
            else if (lang == "fr")
            {
                allreadyExists = "L'autorisation existe déjà !";
            }
            else
            {
                allreadyExists = "الإذن موجود بالفعل!";
            }

            if (txtPermissions.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    int permissionId = int.Parse(txtPermissions.EditValue.ToString());
                    this.currentItemId = this.users != null ? this.users.user_id : currentItemId;

                    if (context.UserHasPermissions.Where(p => p.UserId == this.currentItemId).Where(p => p.PermissionId == permissionId).Count() == 0)
                    {
                        Models.UserHasPermission roleHasPermission = new Models.UserHasPermission();

                        roleHasPermission.UserId = this.currentItemId;
                        roleHasPermission.PermissionId = permissionId;
                        roleHasPermission.CreatedAt = DateTime.Now;
                        roleHasPermission.UpdatedAt = DateTime.Now;

                        context.UserHasPermissions.Add(roleHasPermission);
                        context.SaveChanges();

                        this.loadPermissions();

                        Function.Sound.Added();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        MessageBox.Show(allreadyExists);
                    }
                }
            }
        }

        private void btnAssignAllPermissions_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to assign all permissions ?";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir attribuer toutes les autorisations ?";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد أنك تريد تعيين كافة الأذونات؟";
            }

            if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    int userId = Properties.Settings.Default.userId;

                    List<UserHasPermission> userHasPermissionlists = new List<UserHasPermission>();

                    foreach (var item in context.Permissions.ToList())
                    {
                        if (context.UserHasPermissions.Where(p => p.UserId == userId).Where(p => p.PermissionId == item.Id).Count() == 0)
                        {
                            UserHasPermission userHasPermission = new UserHasPermission();
                            userHasPermission.UserId = userId;
                            userHasPermission.PermissionId = item.Id;
                            userHasPermission.CreatedAt = DateTime.Now;
                            userHasPermission.UpdatedAt = DateTime.Now;
                            userHasPermissionlists.Add(userHasPermission);
                        }
                    }

                    context.UserHasPermissions.AddRange(userHasPermissionlists);
                    context.SaveChanges();
                    this.loadPermissions();

                    Sound.Added();
                }
            }
            else
            {
                Sound.Wrong();
            }
        }

        private void btnDeleteAllPermissions_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to delete All Permissions ?";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Voulez-vous vraiment supprimer toutes les autorisations ?";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في حذف كافة الأذونات؟";
            }

            int userId = Properties.Settings.Default.userId;

            if (XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    var userHasPermissions = context.UserHasPermissions.Where(p => p.UserId == userId);
                    context.UserHasPermissions.RemoveRange(userHasPermissions);
                    context.SaveChanges();
                    this.loadPermissions();
                    Function.Sound.Deleted();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private Models.User GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Users.Find(currentItemId);
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
                int minId = context.Users.Min(b => b.Id);
                int maxId = context.Users.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                Models.User currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtUserFullName.Text = currentItem.FirstName + " " + currentItem.LastName;
                    this.loadPermissions();
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
                    int? maxId = context.Users.Max(b => (int?)b.Id);
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

                var nextItem = context.Users.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Users.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
    }
}