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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using Pos.Forms.Sale;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.Customer
{
    public partial class AddEditCustomer : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Customers customers = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;
        bool PicChanged = false;
        PosScreen localpos;
        bool isPos = false;

        public void setObject(Object obj)
        {
            this.obj = obj;
        }

        public AddEditCustomer(PosScreen pos)
        {
            InitializeComponent();
            isPos = true;
            localpos = pos;
            this.toRtl();
            this.BorderStyle();
        }
        public AddEditCustomer()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
        }
        public AddEditCustomer(int customerEditId)
        {
            InitializeComponent();
            this.currentItemId = customerEditId;
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

            //layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlItem4.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem16.AppearanceItemCaption.Font = customFont10;
            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem17.AppearanceItemCaption.Font = customFont10;
            layoutControlItem18.AppearanceItemCaption.Font = customFont10;
            layoutControlItem19.AppearanceItemCaption.Font = customFont10;
            layoutControlItem20.AppearanceItemCaption.Font = customFont10;
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

        public void setCustomersObject(Customers customers)
        {
            this.customers = customers;
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

            if (this.customers != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.customers.customer_id != 0)
                        this.currentItemId = this.customers.customer_id;

                    this.customers.customer_id = 0;

                    Models.Customer user = context.Customers.Find(this.currentItemId);

                    if (user != null)
                    {
                        txtFirstName.Text = user.FirstName;
                        txtLastName.Text = user.LastName;
                        txtEmail.Text = user.Email;
                        txtPhone.Text = user.Phone;
                        txtImage.EditValue = user.Image;
                        txtDue.EditValue = user.CurrentDue;
                        cbxPriceGroups.EditValue = user.PriceGroup;
                        txtStatus.EditValue = user.Status;
                        txtGender.EditValue = user.Gender;
                        txtCountry.EditValue = user.CityId;
                        txtState.EditValue = user.StateId;
                        txtCity.EditValue = user.CityId;
                        txtAddress.Text = user.Address;
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
            FetchData();

        }

        public void FetchData()
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
                Models.Customer user = context.Customers.Find(this.currentItemId);

                if (user != null)
                {
                    txtFirstName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.Phone;
                    txtImage.EditValue = user.Image;
                    cbxPriceGroups.EditValue = user.PriceGroup;
                    txtStatus.EditValue = user.Status;
                    txtGender.EditValue = user.Gender;
                    txtCountry.EditValue = user.CityId;
                    txtState.EditValue = user.StateId;
                    txtCity.EditValue = user.CityId;
                    txtAddress.Text = user.Address;
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
        public void ClearData()
        {
            txtAddress.Clear();
            txtCity.Clear();
            txtCountry.Clear();
            txtEmail.Clear();
            txtFirstName.Clear();
            cbxPriceGroups.Clear();
            txtGender.Clear();
            txtImage.Image = null;
            PicChanged = false;
            txtLastName.Clear();
            txtPhone.Clear();
            txtState.Clear();
            txtStatus.Clear();
        }
        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            // ===== PHASE 3E: INPUT VALIDATION =====
            // Valider et sanitizer TOUS les inputs avant sauvegarde
            if (!Function.FormValidationHelper.ValidateCustomerForm(
                txtFirstName,
                txtLastName,
                txtEmail,
                txtPhone,
                txtAddress))
            {
                return; // Validation échouée, message déjà affiché
            }
            // ===== FIN VALIDATION =====

            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string emailAlreadyExists;
            string phoneAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item !";
                emailAlreadyExists = "A customer with this email already exists.";
                phoneAlreadyExists = "A customer with this phone already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'article !";
                emailAlreadyExists = "Un client avec cette adresse email existe déjà.";
                phoneAlreadyExists = "Un client avec ce téléphone existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                emailAlreadyExists = "يوجد بالفعل عميل لديه هذا البريد الإلكتروني.";
                phoneAlreadyExists = "يوجد عميل بهذا الهاتف بالفعل.";
            }

            if (dxValidationProviderCustomer.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Customer customer;
                    bool isEmailExists = false;
                    bool isPhoneExists = false;

                    if (this.type == "Add")
                    {
                        // Check if a customer with the same email or phone already exists
                        isEmailExists = context.Customers.Any(c => c.Email == txtEmail.Text);
                        isPhoneExists = context.Customers.Any(c => c.Phone == txtPhone.Text);

                        if (isEmailExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(emailAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        if (isPhoneExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(phoneAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new customer
                        customer = new Models.Customer();

                        customer.FirstName= txtFirstName.Text;
                        customer.LastName= txtLastName.Text;
                        customer.FullName = txtFirstName.Text +" "+ txtLastName.Text;
                        
                        if(txtEmail.Text != "")
                            customer.Email= txtEmail.Text;

                        if (txtStatus.Text != "")
                            customer.Status= txtStatus.Text;

                        if (cbxPriceGroups.Text != "")
                            customer.PriceGroup = int.Parse(cbxPriceGroups.EditValue.ToString());

                        if (txtGender.Text != "")
                            customer.Gender= txtGender.Text;

                        if (txtPhone.Text != "")
                            customer.Phone= txtPhone.Text;

                        if (txtCountry.EditValue != null)
                            customer.CountryId = int.Parse(txtCountry.EditValue.ToString());

                        if (txtState.EditValue != null)
                            customer.StateId = int.Parse(txtState.EditValue.ToString());

                        if (txtCity.EditValue != null)
                            customer.CityId = int.Parse(txtCity.EditValue.ToString());

                        customer.CurrentDue = 0;

                        if (txtAddress.Text != "")
                            customer.Address = txtAddress.Text;

                        customer.CreatedAt = DateTime.Now;
                        customer.UpdatedAt = DateTime.Now;

                        if (PicChanged)
                        {
                            // Get the image from the PictureEdit control
                            if (txtImage.Image!=null)
                            {
                                Image image = txtImage.Image;

                                // Convert the image to a byte array using a MemoryStream
                                byte[] imageBytes;
                                using (var memoryStream = new MemoryStream())
                                {
                                    image.Save(memoryStream, ImageFormat.Png); // Choose the appropriate format based on your needs
                                    imageBytes = memoryStream.ToArray();
                                }

                                customer.Image = imageBytes;
                            }
                           
                        }
                        PicChanged = false;

                        context.Customers.Add(customer);
                        context.SaveChanges();

                        if (isPos)
                        {
                            if (customer.Status== "Active")
                            {
                                localpos.SetCustomer(customer.Id);

                            }
                            else
                            {
                                localpos.SetCustomer(0);
                            }
                            this.Close();
                        }

                        ClearData();
                        // Generate barcode
                        customer.Code = Function.Helper.generateBarCode(customer.Id);
                        context.Entry(customer).State = EntityState.Modified;
                    }
                    else
                    {
                        
                        if (this.customers != null)
                        {
                            if (this.customers.customer_id!=0)
                            {
                                this.currentItemId = this.customers.customer_id;
                            }
                           
                        }

                        //if (customerEditId!=0)
                        //{
                        //    this.currentItemId = customerEditId;
                        //}
                        customer = context.Customers.Find(this.currentItemId);

                        if (customer != null)
                        {
                            // Check if the new email or phone is unique excluding the current record
                            isEmailExists = context.Customers.Any(c => c.Email == txtEmail.Text && c.Id != customer.Id);
                            isPhoneExists = context.Customers.Any(c => c.Phone == txtPhone.Text && c.Id != customer.Id);

                            if (isEmailExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(emailAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            if (isPhoneExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(phoneAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            customer.FirstName = txtFirstName.Text;
                            customer.LastName = txtLastName.Text;
                            customer.FullName = txtFirstName.Text + " " + txtLastName.Text;

                            if (PicChanged)
                            {
                                // Get the image from the PictureEdit control
                                if (txtImage.Image != null)
                                {
                                    Image image = txtImage.Image;

                                    // Convert the image to a byte array using a MemoryStream
                                    byte[] imageBytes;
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        image.Save(memoryStream, ImageFormat.Png); // Choose the appropriate format based on your needs
                                        imageBytes = memoryStream.ToArray();
                                    }

                                    customer.Image = imageBytes;
                                }

                            }
                            PicChanged = false;

                            if (txtEmail.Text != "")
                                customer.Email = txtEmail.Text;

                            if (txtStatus.Text != "")
                                customer.Status = txtStatus.Text;

                            if (cbxPriceGroups.Text != "")
                                customer.PriceGroup = int.Parse(cbxPriceGroups.EditValue.ToString());

                            if (txtGender.Text != "")
                                customer.Gender = txtGender.Text;

                            if (txtPhone.Text != "")
                                customer.Phone = txtPhone.Text;

                            if (txtCountry.EditValue != null)
                                customer.CountryId = int.Parse(txtCountry.EditValue.ToString());

                            if (txtState.EditValue != null)
                                customer.StateId = int.Parse(txtState.EditValue.ToString());

                            if (txtCity.EditValue != null)
                                customer.CityId = int.Parse(txtCity.EditValue.ToString());

                            if (txtAddress.Text != "")
                                customer.Address = txtAddress.Text;

                            customer.UpdatedAt = DateTime.Now;

                            context.Customers.Update(customer);
                           
                            context.SaveChanges();
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

                    // Reload customers
                    if (this.customers != null)
                        this.customers.loadCustomers();
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

        private void AddEditCustomer_Load(object sender, EventArgs e)
        {
            if (this.type != "Add")
            {
                txtFirstName.Focus();
                txtFirstName.Select();
                this.edit();
            }
            else
            {
                txtStatus.EditValue = "Active";
                txtGender.EditValue = "Man";
                txtFirstName.Focus();
                txtFirstName.Select();
            }

            this.loadCountries();
            this.loadStates();
            this.loadCities();
            this.InitializeSearchLookUpEdit();
            LoadPriceGroups();
            txtFirstName.Focus();
            txtFirstName.Select();
        }

        public void LoadPriceGroups()
        {
            using AppDbContext appDbContext = new AppDbContext();
            cbxPriceGroups.Properties.DataSource = appDbContext.PriceGroups.ToList();
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

        private void btnCustomerReset_Click(object sender, EventArgs e)
        {
            this.type = "Add";
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtImage.EditValue = null;
            txtStatus.EditValue = "Active";
            txtGender.EditValue = "Man";
            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;

            Function.Sound.Added();
        }

        private Models.Customer GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Customers.Find(currentItemId);
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
                int minId = context.Customers.Min(b => b.Id);
                int maxId = context.Customers.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Customer currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtFirstName.Text = currentItem.FirstName;
                    txtLastName.Text = currentItem.LastName;
                    txtEmail.Text = currentItem.Email;
                    txtImage.EditValue = currentItem.Image;
                    txtStatus.EditValue = currentItem.Status;
                    txtGender.EditValue = currentItem.Gender;
                    txtPhone.Text = currentItem.Phone;
                    txtAddress.Text = currentItem.Address;
                    txtCountry.EditValue = currentItem.CityId;
                    txtState.EditValue = currentItem.StateId;
                    txtCity.EditValue = currentItem.CityId;
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
                    int? minId = context.Customers.Min(b => (int?)b.Id);
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
                    int? maxId = context.Customers.Max(b => (int?)b.Id);
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

                var nextItem = context.Customers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.Customers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
                searchLookUpEdit.Properties.DataSource = context.Customers.ToList();
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

            if (searchLookUpEdit.EditValue != null && string.IsNullOrEmpty(searchLookUpEdit.EditValue.ToString()))
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
            // do somthing
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

        private void txtImage_EditValueChanged(object sender, EventArgs e)
        {
            PicChanged = true;
        }
    }
}