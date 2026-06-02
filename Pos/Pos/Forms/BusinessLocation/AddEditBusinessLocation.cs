using DevExpress.ChartRangeControlClient.Core;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
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
using Stripe.Terminal;
using Pos.Forms.Employee;
using Pos.Forms.Purchase;
using Pos.Forms.User;
using PosScreen = Pos.Forms.Screen.Pos;

namespace Pos.Forms.BusinessLocation
{
    public partial class AddEditBusinessLocation : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public BusinessLocations business_locations = null;
        public string type = "Add";
        public Object obj = null;
        public int currentItemId = 0;
        PosScreen localpos;
        bool isPos = false;
        AddEditEmployee addEditEmployee;
        bool isEditEmployee = false;
        AddEditPurchase addEditPurchase;
        bool isaddEditPurchase = false;
        AddEditUser localaddEditUser;
        bool isaddEditUser = false;
        public void setObject(Object obj)
        {
            this.obj = obj;
        }
        public AddEditBusinessLocation(AddEditUser addEditUser)
        {
            InitializeComponent();
            isaddEditUser = true;
            localaddEditUser = addEditUser;
            this.toRtl();
            this.BorderStyle();
        }
        public AddEditBusinessLocation(AddEditPurchase addEditPurchas)
        {
            InitializeComponent();
            isaddEditPurchase = true;
            addEditPurchase = addEditPurchas;
            this.toRtl();
            this.BorderStyle();
        }
        public AddEditBusinessLocation(AddEditEmployee addEditEmploye)
        {
            InitializeComponent();
            isEditEmployee = true;
            addEditEmployee = addEditEmploye;
            this.toRtl();
            this.BorderStyle();
        }
        public AddEditBusinessLocation(PosScreen pos)
        {
            InitializeComponent();
            isPos = true;
            localpos = pos;
            this.toRtl();
            this.BorderStyle();
        }
        public AddEditBusinessLocation()
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
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont10;
            layoutControlItem2.AppearanceItemCaption.Font = customFont10;
            layoutControlItem3.AppearanceItemCaption.Font = customFont10;
            layoutControlItem4.AppearanceItemCaption.Font = customFont10;
            layoutControlItem5.AppearanceItemCaption.Font = customFont10;
            layoutControlItem6.AppearanceItemCaption.Font = customFont10;
            layoutControlItem7.AppearanceItemCaption.Font = customFont10;
            layoutControlItem8.AppearanceItemCaption.Font = customFont10;
            layoutControlItem9.AppearanceItemCaption.Font = customFont10;
            layoutControlItem10.AppearanceItemCaption.Font = customFont10;
            layoutControlItem11.AppearanceItemCaption.Font = customFont10;
            layoutControlItem12.AppearanceItemCaption.Font = customFont10;
            layoutControlItem13.AppearanceItemCaption.Font = customFont10;
            layoutControlItem34.AppearanceItemCaption.Font = customFont10;
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

        public void setBusinessLocationsObject(BusinessLocations business_locations)
        {
            this.business_locations = business_locations;
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
                pleaseSelectItem = "الرجاء تحديد البند!";
            }

            if (this.business_locations != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.business_locations.business_location_id != 0)
                        this.currentItemId = this.business_locations.business_location_id;

                    this.business_locations.business_location_id = 0;

                    Models.BusinessLocation business_location = context.BusinessLocations.Find(this.currentItemId);

                    if (business_location != null)
                    {
                        txtName.Text = business_location.Name;
                        txtLocationID.Text = business_location.LocationId;
                        txtLandmark.Text = business_location.Landmark;
                        txtCity.Text = business_location.City;
                        txtZipCode.Text = business_location.ZipCode;
                        txtState.Text = business_location.State;
                        txtCountry.Text = business_location.Country;
                        txtMobile.Text = business_location.Mobile;
                        txtAlternateContactNumber.Text = business_location.AlternateContactNumber;
                        txtEmail.Text = business_location.Email;
                        txtWebsite.Text = business_location.Website;
                        txtIsDefault.EditValue = business_location.IsDefault;
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

        private void btnSaveBusinessLocation_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string alreadyExists;
            string pleaseSelectItem;

            if (lang == "en")
            {
                alreadyExists = "A business location with this name already exists.";
                pleaseSelectItem = "Please select item!";
            }
            else if (lang == "fr")
            {
                alreadyExists = "Un établissement portant ce nom existe déjà.";
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
            }
            else
            {
                alreadyExists = "يوجد بالفعل موقع عمل بهذا الاسم.";
                pleaseSelectItem = "الرجاء تحديد العنصر!";
            }

            if (dxValidationProviderBusinessLocation.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.BusinessLocation businessLocation;

                    if (this.type == "Add")
                    {
                        // Check if a business location with the same name already exists
                        if (context.BusinessLocations.Any(b => b.Name == txtName.Text))
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(alreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new business location
                        businessLocation = new Models.BusinessLocation
                        {
                            Name = txtName.Text,
                            LocationId = txtLocationID.Text,
                            Landmark = txtLandmark.Text,
                            City = txtCity.Text,
                            ZipCode = txtZipCode.Text,
                            State = txtState.Text,
                            Country = txtCountry.Text,
                            Mobile = txtMobile.Text,
                            AlternateContactNumber = txtAlternateContactNumber.Text,
                            Email = txtEmail.Text,
                            Website = txtWebsite.Text,
                            IsDefault = txtIsDefault.Text,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.BusinessLocations.Add(businessLocation);
                     
                        ClearFormFields();
                    }
                    else
                    {
                        if (this.business_locations != null)
                        {
                            if (this.business_locations.business_location_id!=0)
                            {
                                this.currentItemId = this.business_locations.business_location_id;
                            }
                        }
                            

                        businessLocation = context.BusinessLocations.Find(this.currentItemId);

                        if (businessLocation != null)
                        {
                            // Check if the new name is unique excluding the current record
                            if (context.BusinessLocations.Any(b => b.Name == txtName.Text && b.Id != businessLocation.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(alreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            businessLocation.Name = txtName.Text;
                            businessLocation.LocationId = txtLocationID.Text;
                            businessLocation.Landmark = txtLandmark.Text;
                            businessLocation.City = txtCity.Text;
                            businessLocation.ZipCode = txtZipCode.Text;
                            businessLocation.State = txtState.Text;
                            businessLocation.Country = txtCountry.Text;
                            businessLocation.Mobile = txtMobile.Text;
                            businessLocation.AlternateContactNumber = txtAlternateContactNumber.Text;
                            businessLocation.Email = txtEmail.Text;
                            businessLocation.Website = txtWebsite.Text;
                            businessLocation.IsDefault = txtIsDefault.Text;
                            businessLocation.UpdatedAt = DateTime.Now;

                            context.Entry(businessLocation).State = EntityState.Modified;
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
                    if (isPos)
                    {
                        localpos.SetBusinessLocation(businessLocation.Id);
                        this.Close();
                    }
                    if (isEditEmployee)
                    {
                        //addEditEmployee.SetBusinessLocation(businessLocation.Id);
                        this.Close();
                    }

                    
                    Function.Sound.Added();

                    if (isaddEditPurchase)
                    {
                        addEditPurchase.setBussLocation(businessLocation.Id);
                        this.Close();
                        
                    }
                    if (isaddEditUser)
                    {
                        localaddEditUser.setBusinessLocations(businessLocation.Id);
                        this.Close();
                    }
                    // Ensure only one default business location
                    if (txtIsDefault.Text == "Yes")
                    {
                        this.changeIsDefaultToNo(businessLocation.Id);
                    }

                    // Reload business locations
                    if (this.business_locations != null)
                        this.business_locations.loadBusinessLocations();
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
            txtLocationID.Text = "";
            txtLandmark.Text = "";
            txtCity.Text = "";
            txtZipCode.Text = "";
            txtState.Text = "";
            txtCountry.Text = "";
            txtMobile.Text = "";
            txtAlternateContactNumber.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            txtIsDefault.EditValue = "No";
        }

        public void changeIsDefaultToNo(int id)
        {
            using (var context = new AppDbContext())
            {
                var businessLocations = context.BusinessLocations.Where(b => b.Id != id).ToList();

                foreach (var bl in businessLocations)
                {
                    bl.IsDefault = "No";
                }

                context.SaveChanges();
            }
        }

        private void AddEditBusinessLocation_Load(object sender, EventArgs e)
        {

            this.getCountries();

            if (this.type != "Add")
            {
                this.edit();
            }
            else
            {
                txtIsDefault.EditValue = "No";
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.BusinessLocation GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.BusinessLocations.Find(currentItemId);
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
                int minId = context.BusinessLocations.Min(b => b.Id);
                int maxId = context.BusinessLocations.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.BusinessLocation currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;

                    txtName.Text = currentItem.Name;
                    txtLocationID.Text = currentItem.LocationId;
                    txtLandmark.Text = currentItem.Landmark;
                    txtCity.Text = currentItem.City;
                    txtZipCode.Text = currentItem.ZipCode;
                    txtState.Text = currentItem.State;
                    txtCountry.Text = currentItem.Country;
                    txtMobile.Text = currentItem.Mobile;
                    txtAlternateContactNumber.Text = currentItem.AlternateContactNumber;
                    txtEmail.Text = currentItem.Email;
                    txtWebsite.Text = currentItem.Website;
                    txtIsDefault.EditValue = currentItem.IsDefault;
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
            using (var context = new AppDbContext())
            {
                string lang = Properties.Settings.Default.Lang;

                string noEntries;
                string failedToRetrieve;

                if (lang == "en")
                {
                    noEntries = "No entries found in DB.";
                    failedToRetrieve = "Failed to retrieve the first item: ";
                }
                else if (lang == "fr")
                {
                    noEntries = "Aucune entrée trouvée dans la base de données.";
                    failedToRetrieve = "Échec de la récupération du premier élément : ";
                }
                else
                {
                    noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                    failedToRetrieve = "فشل استرداد العنصر الأول: ";
                }

                try
                {
                    int? minId = context.BusinessLocations.Min(b => (int?)b.Id);
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
                catch (Exception ex)
                {
                    MessageBox.Show(failedToRetrieve + ex.Message);
                }
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
                failedToRetrieve = "Failed to retrieve the last item: ";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du dernier élément : ";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأخير: ";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? maxId = context.BusinessLocations.Max(b => (int?)b.Id);
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

                var nextItem = context.BusinessLocations.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
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
                    var prevItem = context.BusinessLocations.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
            txtLocationID.Text = string.Empty;
            txtLandmark.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtState.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAlternateContactNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtIsDefault.EditValue = "No";

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

            string writeSomething;
            string transName;

            if (lang == "en")
            {
                writeSomething = "Write something...";
                transName = "Name";
            }
            else if (lang == "fr")
            {
                writeSomething = "Écrire quelque chose...";
                transName = "Nom";
            }
            else
            {
                writeSomething = "اكتب شيئًا...";
                transName = "الاسم";
            }

            using (var context = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = context.BusinessLocations.ToList();
                searchLookUpEdit.Properties.DisplayMember = "Name";
                searchLookUpEdit.Properties.ValueMember = "Id";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("Name", transName);

                searchLookUpEdit.Properties.NullText = writeSomething;

                searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string selectedValue;
            string noItemSelected;

            if (lang == "en")
            {
                selectedValue = "Selected value is not a valid integer";
                noItemSelected = "No item selected.";
            }
            else if (lang == "fr")
            {
                selectedValue = "La valeur sélectionnée n'est pas un entier valide";
                noItemSelected = "Aucun élément sélectionné.";
            }
            else
            {
                selectedValue = "القيمة المحددة ليست عددًا صحيحًا صالحًا";
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
                    Console.WriteLine(selectedValue);
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
                Sound.Selected();
                this.Close();
            }
        }

        public void getCity(int stateId)
        {
            using (var context = new AppDbContext())
            {
                txtCity.Properties.DataSource = context.Cities.Where(x => x.StateId == stateId).ToList();
                txtCity.Properties.DisplayMember = "Title"; // Set display member
                txtCity.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getCountries()
        {
            using (var context = new AppDbContext())
            {
                txtCountry.Properties.DataSource = context.Countries.ToList();
                txtCountry.Properties.DisplayMember = "Name"; // Set display member
                txtCountry.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getStates(int countryId)
        {
            using (var context = new AppDbContext())
            {
                txtState.Properties.DataSource = context.States.Where(c => c.CountryId == countryId).ToList();
                txtState.Properties.DisplayMember = "Name"; // Set display member
                txtState.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void txtCountry_EditValueChanged(object sender, EventArgs e)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                if ((txtCountry.EditValue != null) && !(string.IsNullOrEmpty(txtCountry.EditValue.ToString())))
                {
                    txtCountry.Clear();
                    txtCity.Clear();
                    txtState.Properties.DataSource = AppDb.States.Where(c => c.CountryId == int.Parse(txtCountry.EditValue.ToString())).ToList();
                }

            }
        }

        private void txtState_EditValueChanged(object sender, EventArgs e)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                if ((txtState.EditValue != null) && !(string.IsNullOrEmpty(txtState.EditValue.ToString())))
                {
                    txtCity.Clear();
                    txtCity.Properties.DataSource = AppDb.Cities.Where(c => c.StateId == int.Parse(txtState.EditValue.ToString())).ToList();
                }

            }
        }
    }
}