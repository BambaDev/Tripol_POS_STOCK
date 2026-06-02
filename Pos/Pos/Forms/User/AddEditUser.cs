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
using Stripe;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using Pos.Forms.BusinessLocation;
namespace Pos.Forms.User
{
    public partial class AddEditUser : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Users users = null;
        public string type = "Add";
        public string register = "";
        public int BusinessLocationID = 0;
        public int currentItemId = 0;

        public AddEditUser(string register = "")
        {
            InitializeComponent();

            this.toRtl();
            this.register = register;
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

            simpleLabelItem1.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            System.Drawing.Font customFont10 = Function.CustomArabicFont.customFont;

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

            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            System.Drawing.Font customFont9 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont9;
            layoutControlItem2.AppearanceItemCaption.Font = customFont9;
            layoutControlItem3.AppearanceItemCaption.Font = customFont9;
            layoutControlItem4.AppearanceItemCaption.Font = customFont9;
            layoutControlItem5.AppearanceItemCaption.Font = customFont9;
            layoutControlItem6.AppearanceItemCaption.Font = customFont9;
            layoutControlItem7.AppearanceItemCaption.Font = customFont9;
            layoutControlItem8.AppearanceItemCaption.Font = customFont9;
            layoutControlItem9.AppearanceItemCaption.Font = customFont9;
            layoutControlItem10.AppearanceItemCaption.Font = customFont9;
            layoutControlItem11.AppearanceItemCaption.Font = customFont9;
            layoutControlItem12.AppearanceItemCaption.Font = customFont9;
            layoutControlItem13.AppearanceItemCaption.Font = customFont9;
            layoutControlItem14.AppearanceItemCaption.Font = customFont9;
            layoutControlItem15.AppearanceItemCaption.Font = customFont9;
            layoutControlItem16.AppearanceItemCaption.Font = customFont9;
            layoutControlItem17.AppearanceItemCaption.Font = customFont9;
            layoutControlItem18.AppearanceItemCaption.Font = customFont9;
            layoutControlItem19.AppearanceItemCaption.Font = customFont9;
            layoutControlItem20.AppearanceItemCaption.Font = customFont9;
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

            if (this.users != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.users.user_id != 0)
                        this.currentItemId = this.users.user_id;

                    this.users.user_id = 0;

                    Models.User user = context.Users.Find(this.currentItemId);

                    if (user != null)
                    {
                        txtFirstName.Text = user.FirstName;
                        txtLastName.Text = user.LastName;
                        txtUserLogin.Text = user.UserLogin;
                        txtEmail.Text = user.Email;
                        txtPhone.Text = user.Phone;
                        txtPassword.Text = "";
                        txtImage.EditValue = user.Image;
                        txtIsAdmin.EditValue = user.RoleId;
                        txtStatus.EditValue = user.Status;
                        txtGender.EditValue = user.Gender;
                        txtCountry.EditValue = user.CityId;
                        txtState.EditValue = user.StateId;
                        txtCity.EditValue = user.CityId;
                        txtBusinessLocations.EditValue = user.BusinessLocationId;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string userLoginAlreadyExists;
            string emailAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select a user.";
                userLoginAlreadyExists = "A User with this UserLogin already exists.";
                emailAlreadyExists = "A User with this email already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner un utilisateur.";
                userLoginAlreadyExists = "Un utilisateur avec ce UserLogin existe déjà.";
                emailAlreadyExists = "Un utilisateur avec cette adresse email existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد مستخدم.";
                userLoginAlreadyExists = "يوجد بالفعل مستخدم لديه UserLogin هذا.";
                emailAlreadyExists = "مستخدم بهذا البريد الإلكتروني موجود بالفعل.";
            }

            if (dxValidationProviderUser.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.User user;
                    bool isUserLoginExists = false;
                    bool isEmailExists = false;

                    if (this.type == "Add")
                    {
                        // Check if a user with the same UserLogin or email already exists
                        isUserLoginExists = context.Users.Any(u => u.UserLogin == txtUserLogin.Text);
                        isEmailExists = context.Users.Any(u => u.Email == txtEmail.Text);

                        if (isUserLoginExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(userLoginAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        if (isEmailExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(emailAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new user
                        user = new Models.User();
                        user.FirstName = txtFirstName.Text;
                        user.LastName = txtLastName.Text;
                        user.Email = txtEmail.Text;
                        user.FullName = txtFirstName.Text + " " + txtLastName.Text;
                        user.UserLogin = txtUserLogin.Text;
                        user.Password = Function.PasswordHelper.HashPassword(txtPassword.Text);
                        user.Image = GetImageBytes(txtImage.Image);
                        user.IsAdmin = txtIsAdmin.Text;
                        user.Status = txtStatus.Text;
                        user.Gender = txtGender.Text;
                        user.Phone = txtPhone.Text;
                        user.PinOne = txtPin1.Text;
                        user.PinTwo = txtPin2.Text;
                        user.PinThree = txtPin3.Text;
                        user.PinFour = txtPin4.Text;
                        user.RoleId = int.Parse(txtIsAdmin.EditValue.ToString());
                        if ((txtCountry.EditValue != null) && !(string.IsNullOrEmpty(txtCountry.EditValue.ToString())))
                        {
                            user.CountryId = int.Parse(txtCountry.EditValue.ToString());
                        }
                        if ((txtState.EditValue != null) && !(string.IsNullOrEmpty(txtState.EditValue.ToString())))
                        {
                            user.StateId = int.Parse(txtState.EditValue.ToString());
                        }
                        if ((txtCity.EditValue != null) && !(string.IsNullOrEmpty(txtCity.EditValue.ToString())))
                        {
                            user.CityId = int.Parse(txtCity.EditValue.ToString());
                        }
                        if ((txtBusinessLocations.EditValue != null) && !(string.IsNullOrEmpty(txtBusinessLocations.EditValue.ToString())))
                        {
                            user.BusinessLocationId = int.Parse(txtBusinessLocations.EditValue.ToString());
                        }

                        user.CreatedAt = DateTime.Now;
                        user.UpdatedAt = DateTime.Now;
                        //user = new Models.User
                        //{
                        //    FirstName = txtFirstName.Text,
                        //    LastName = txtLastName.Text,
                        //    UserLogin = txtUserLogin.Text,
                        //    Email = txtEmail.Text,
                        //    Password = Function.PasswordHelper.HashPassword(txtPassword.Text),
                        //    Image = GetImageBytes(txtImage.Image),
                        //    IsAdmin = txtIsAdmin.Text,
                        //    Status = txtStatus.Text,
                        //    Gender = txtGender.Text,
                        //    Phone = txtPhone.Text,
                        //    PinOne = txtPin1.Text,
                        //    PinTwo = txtPin2.Text,
                        //    PinThree = txtPin3.Text,
                        //    PinFour = txtPin4.Text,
                        //    CountryId = int.Parse(txtCountry.EditValue.ToString()),
                        //    StateId = int.Parse(txtState.EditValue.ToString()),
                        //    CityId = int.Parse(txtCity.EditValue.ToString()),
                        //    BusinessLocationId = int.Parse(txtBusinessLocations.EditValue.ToString()),
                        //    CreatedAt = DateTime.Now,
                        //    UpdatedAt = DateTime.Now
                        //};

                        context.Users.Add(user);
                        ClearFormFields();
                    }
                    else
                    {
                        if (this.users != null)
                        {
                            if (this.users.user_id != 0)
                            {
                                this.currentItemId = this.users.user_id;
                            }
                        }


                        user = context.Users.Find(this.currentItemId);

                        if (user != null)
                        {
                            // Check if the new UserLogin or email is unique excluding the current record
                            isUserLoginExists = context.Users.Any(u => u.UserLogin == txtUserLogin.Text && u.Id != user.Id);
                            isEmailExists = context.Users.Any(u => u.Email == txtEmail.Text && u.Id != user.Id);

                            if (isUserLoginExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(userLoginAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            if (isEmailExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(emailAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            user.FirstName = txtFirstName.Text;
                            user.LastName = txtLastName.Text;
                            user.UserLogin = txtUserLogin.Text;
                            user.Email = txtEmail.Text;
                            user.FullName = txtFirstName.Text + " " + txtLastName.Text;
                            user.Password = Function.PasswordHelper.HashPassword(txtPassword.Text);
                            user.Image = GetImageBytes(txtImage.Image);
                            user.IsAdmin = txtIsAdmin.Text;
                            user.RoleId = int.Parse(txtIsAdmin.EditValue.ToString());
                            user.Status = txtStatus.Text;
                            user.Gender = txtGender.Text;
                            user.Phone = txtPhone.Text;
                            if ((txtCountry.EditValue != null) && !(string.IsNullOrEmpty(txtCountry.EditValue.ToString())))
                            {
                                user.CountryId = int.Parse(txtCountry.EditValue.ToString());
                            }
                            if ((txtState.EditValue != null) && !(string.IsNullOrEmpty(txtState.EditValue.ToString())))
                            {
                                user.StateId = int.Parse(txtState.EditValue.ToString());
                            }

                            if ((txtCity.EditValue != null) && !(string.IsNullOrEmpty(txtCity.EditValue.ToString())))
                            {
                                user.CityId = int.Parse(txtCity.EditValue.ToString());
                            }

                            user.BusinessLocationId = int.Parse(txtBusinessLocations.EditValue.ToString());
                            user.UpdatedAt = DateTime.Now;

                            if (txtPin1.Text != "" && txtPin2.Text != "" && txtPin3.Text != "" && txtPin4.Text != "")
                            {
                                user.PinOne = txtPin1.Text;
                                user.PinTwo = txtPin2.Text;
                                user.PinThree = txtPin3.Text;
                                user.PinFour = txtPin4.Text;
                            }

                            context.Entry(user).State = EntityState.Modified;
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

                    txtPassword.Text = "";

                    // Reload users
                    if (this.users != null)
                        this.users.loadUsers();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private byte[] GetImageBytes(Image image)
        {
            if (image == null) return null;

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        private void ClearFormFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUserLogin.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtIsAdmin.Clear();
            txtStatus.Text = "";
            txtGender.Text = "";
            txtPhone.Text = "";
            txtCountry.EditValue = null;
            txtState.EditValue = null;
            txtCity.EditValue = null;
            txtBusinessLocations.EditValue = null;
        }

        private void AddEditUser_Load(object sender, EventArgs e)
        {
            //if (this.register == "Register")
            //{
            //    Models.Setting setting = context.Settings.FirstOrDefault();

            //    PurchaseCode purchaseCodeAlert = new PurchaseCode();

            //    if (setting != null)
            //    {
            //        if (Function.Helper.isPurchaseCodeActivated())
            //        {
            //            if (setting.PurchaseCode == null)
            //            {
            //                purchaseCodeAlert.ShowDialog();
            //            }
            //            else
            //            {
            //                var purchaseCode = new EnvatoPurchaseCode(Properties.Settings.Default.EvantoToken);
            //                var isValid = await purchaseCode.VerifyPurchase(setting.PurchaseCode);

            //                if (!isValid)
            //                {
            //                    purchaseCodeAlert.ShowDialog();
            //                }
            //            }
            //        }
            //    }
            //}

            if (this.type != "Add")
            {
                this.edit();
            }

            this.loadBusinessLocations();
            this.loadCountries();
            this.loadStates();
            this.loadCities();
            this.InitializeSearchLookUpEdit();
            LoadRole();
        }

        public void LoadRole()
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                txtIsAdmin.Properties.DataSource = AppDb.Roles.ToList();
            }
        }
        public void loadBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBusinessLocations.Properties.DataSource = context.BusinessLocations.ToList();
                txtBusinessLocations.Properties.DisplayMember = "Name";
                txtBusinessLocations.Properties.ValueMember = "Id";
            }
        }

        public void loadCountries()
        {
            using (var context = new AppDbContext())
            {
                txtCountry.Properties.DataSource = context.Countries.ToList();
                txtCountry.Properties.DisplayMember = "Name";
                txtCountry.Properties.ValueMember = "Id";
            }
        }

        public void loadStates()
        {
            using (var context = new AppDbContext())
            {
                txtState.Properties.DataSource = context.States.ToList();
                txtState.Properties.DisplayMember = "Name";
                txtState.Properties.ValueMember = "Id";
            }
        }

        public void loadCities()
        {
            using (var context = new AppDbContext())
            {
                txtCity.Properties.DataSource = context.Cities.ToList();
                txtCity.Properties.DisplayMember = "Name";
                txtCity.Properties.ValueMember = "Id";
            }
        }

        private void btnUserReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUserLogin.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            txtImage.EditValue = null;
            txtStatus.EditValue = "Active";
            txtIsAdmin.Clear();
            txtGender.EditValue = "Man";

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;

            Function.Sound.Added();
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

                this.type = "Edit";

                Models.User currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtFirstName.Text = currentItem.FirstName;
                    txtLastName.Text = currentItem.LastName;
                    txtUserLogin.Text = currentItem.UserLogin;
                    txtEmail.Text = currentItem.Email;
                    txtPhone.Text = currentItem.Phone;
                    txtImage.EditValue = currentItem.Image;
                    txtIsAdmin.EditValue = currentItem.RoleId;
                    txtStatus.EditValue = currentItem.Status;
                    txtGender.EditValue = currentItem.Gender;
                    txtCountry.EditValue = currentItem.CityId;
                    txtState.EditValue = currentItem.StateId;
                    txtCity.EditValue = currentItem.CityId;
                    txtBusinessLocations.EditValue = currentItem.BusinessLocationId;
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
                searchLookUpEdit.Properties.DataSource = context.Users.ToList();
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

        private void txtCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCountry.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    txtState.Properties.DataSource = context.States.Where(c => c.CountryId == int.Parse(txtCountry.EditValue.ToString())).ToList();
                    txtState.Properties.DisplayMember = "Name";
                    txtState.Properties.ValueMember = "Id";
                }
            }
        }

        private void txtState_EditValueChanged(object sender, EventArgs e)
        {
            if (txtState.EditValue != null)
            {
                using (var context = new AppDbContext())
                {
                    txtCity.Properties.DataSource = context.Cities.Where(c => c.StateId == int.Parse(txtState.EditValue.ToString())).ToList();
                    txtCity.Properties.DisplayMember = "Name";
                    txtCity.Properties.ValueMember = "Id";
                }
            }
        }

        private void txtBusinessLocations_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddBusinessLocation")
            {
                // Show the message

                AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation(this);
                addEditBusinessLocation.ShowDialog();
            }
        }
        public void setBusinessLocations(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtBusinessLocations.Properties.DataSource = AppDb.BusinessLocations.ToList();
                    txtBusinessLocations.EditValue = id;
                }
            }
            catch { }

        }
    }
}