using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Accessibility;
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
using Stripe.Radar;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid.Columns;

namespace Pos.Forms.Role
{
    public partial class AddEditRole : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Roles roles = null;
      
        public string type = "Add";
        public DataTable dt = new DataTable();
        public int currentItemId = 0;

        public AddEditRole()
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
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;
            toggleSwitchAllPermissions.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

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

        public void setRolesObject(Roles roles)
        {
            this.roles = roles;
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

            if (this.roles != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.roles.role_id != 0)
                        this.currentItemId = this.roles.role_id;

                    this.roles.role_id = 0;

                    Models.Role role = context.Roles.Find(this.currentItemId);

                    if (role != null)
                    {
                        txtRole.Text = role.Name;
                        this.loadPermissions();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        private void btnSaveRole_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string nameAlreadyExists;

            if (lang == "en")
            {
                nameAlreadyExists = "A Role with this Name already exists.";
            }
            else if (lang == "fr")
            {
                nameAlreadyExists = "Un rôle portant ce nom existe déjà.";
            }
            else
            {
                nameAlreadyExists = "دور بهذا الاسم موجود بالفعل.";
            }

            if (dxValidationProviderRole.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Role role;
                    bool isNameExists = false;

                    if (this.type == "Add")
                    {
                        // Check if a role with the same name already exists
                        isNameExists = context.Roles.Any(r => r.Name == txtRole.Text);

                        if (isNameExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new role
                        role = new Models.Role
                        {
                            Name = txtRole.Text,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Roles.Add(role);
                        context.SaveChanges();

                        AssignPermissions(role.Id);

                        txtRole.Text = "";
                    }
                    else
                    {
                        if (this.roles != null)
                        {
                            if (this.roles.role_id!=0)
                            {
                                this.currentItemId = this.roles.role_id;
                            }
                        }
                           
                      
                        role = context.Roles.Find(this.currentItemId);
                        
                        if (role != null)
                        {
                            // Check if the new name is unique excluding the current record
                            isNameExists = context.Roles.Any(r => r.Name == txtRole.Text && r.Id != role.Id);

                            if (isNameExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            role.Name = txtRole.Text;
                            role.UpdatedAt = DateTime.Now;

                            context.Entry(role).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            return;
                        }
                    }

                    Function.Sound.Added();

                    // Reload roles
                    if (this.roles != null)
                        this.roles.loadRoles();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void AssignPermissions(int roleId)
        {
            using (var context = new AppDbContext())
            {
                if (toggleSwitchAllPermissions.IsOn)
                {
                    this.addAllPermissions(roleId);
                }
                else if (dt.Rows.Count > 0)
                {
                    List<RoleHasPermission> roleHasPermissionlists = new List<RoleHasPermission>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        RoleHasPermission roleHasPermission = new RoleHasPermission
                        {
                            RoleId = roleId,
                            PermissionId = int.Parse(dt.Rows[i]["Id"].ToString()),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        roleHasPermissionlists.Add(roleHasPermission);
                    }

                    context.RoleHasPermissions.AddRange(roleHasPermissionlists);
                    context.SaveChanges();

                    dt.Rows.Clear();
                    gridControlPermissions.DataSource = dt;
                }
            }
        }

        public int getIndex(string value)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == value)
                    return i;
            }

            return -1;
        }

        public void loadPermissions()
        {
            dt = new DataTable();

            gridControlPermissions.DataSource = null;

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Permission.Name", typeof(string));
                dt.Columns.Add("CreatedAt", typeof(string));
            }

            //this.currentItemId = this.roles != null ? this.roles.role_id : currentItemId;

            //if (this.roles != null)
            //{
            //    this.currentItemId = this.roles.role_id;
            //}
          

            using (var context = new AppDbContext())
            {
                gridControlPermissions.DataSource = context.RoleHasPermissions
                .Where(p => p.RoleId == this.currentItemId)
                .Include(r => r.Role)
                .Include(n => n.Permission)
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

        private void AddEditRole_Load(object sender, EventArgs e)
        {
            this.lookUpPermissions();

            if (this.type != "Add")
            {
                this.edit();
                this.loadPermissions();
            }
            else
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Id", typeof(string));
                    dt.Columns.Add("Permission.Name", typeof(string));
                    dt.Columns.Add("CreatedAt", typeof(string));
                }
            }
        }

        private void txtPermissions_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;


            string nameAlreadyExists;

            if (lang == "en")
            {
                nameAlreadyExists = "The Permission Already Exists !";
            }
            else if (lang == "fr")
            {
                nameAlreadyExists = "L'autorisation existe déjà !";
            }
            else
            {
                nameAlreadyExists = "الإذن موجود بالفعل!";
            }

            if (txtPermissions.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    int permissionId = int.Parse(txtPermissions.EditValue.ToString());

                    if (this.type != "Add")
                    {
                        if (context.RoleHasPermissions.Where(p => p.RoleId == this.currentItemId).Where(p => p.PermissionId == permissionId).Count() == 0)
                        {
                            Models.RoleHasPermission roleHasPermission = new Models.RoleHasPermission();
                           
                           
                            roleHasPermission.RoleId = this.currentItemId;
                            roleHasPermission.PermissionId = permissionId;
                            roleHasPermission.CreatedAt = DateTime.Now;
                            roleHasPermission.UpdatedAt = DateTime.Now;

                            context.RoleHasPermissions.Add(roleHasPermission);
                            context.SaveChanges();

                            this.loadPermissions();

                            Function.Sound.Added();
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            MessageBox.Show(nameAlreadyExists);
                        }
                    }
                    else
                    {
                        int index = this.getIndex(permissionId.ToString());

                        if (index == -1)
                        {
                            DataRow NewRow = dt.NewRow();

                            NewRow["Id"] = permissionId;
                            NewRow["Permission.Name"] = txtPermissions.Text;
                            NewRow["CreatedAt"] = DateTime.Now;
                            dt.Rows.Add(NewRow);

                            gridControlPermissions.DataSource = dt;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            MessageBox.Show(nameAlreadyExists);
                        }
                    }
                }
            }
        }

        private Models.Role GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Roles.Find(currentItemId);
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
                int minId = context.Roles.Min(b => b.Id);
                int maxId = context.Roles.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Role currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtRole.Text = currentItem.Name;
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
                    int? minId = context.Roles.Min(b => (int?)b.Id);
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
                    int? maxId = context.Roles.Max(b => (int?)b.Id);
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

                var nextItem = context.Roles.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Roles.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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

            txtRole.Text = string.Empty;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addAllPermissions(int role_id)
        {
            using(var context = new AppDbContext())
            {
                var permoissions = context.Permissions.ToList();
                  
                if (permoissions.Count > 0)
                {
                    List<RoleHasPermission> roleHasPermissionlists = new List<RoleHasPermission>();

                    foreach (var permoission in permoissions)
                    {
                        RoleHasPermission roleHasPermission = new RoleHasPermission();
                        roleHasPermission.RoleId = role_id;
                        roleHasPermission.PermissionId = permoission.Id;
                        roleHasPermission.CreatedAt = DateTime.Now;
                        roleHasPermission.UpdatedAt = DateTime.Now;
                        roleHasPermissionlists.Add(roleHasPermission);
                    }

                    context.RoleHasPermissions.AddRange(roleHasPermissionlists);
                    context.SaveChanges();
                    
                    this.currentItemId = role_id;

                    this.loadPermissions();
                }
            }
        }
    }
}