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
using DevExpress.XtraLayout;
using Twilio.TwiML.Voice;
using DevExpress.XtraGrid.Columns;

namespace Pos.Forms.Location
{
    public partial class AddEditLocation : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Locations locations = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditLocation()
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

            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlItem13.AppearanceItemCaption.Font = customFont;
            layoutControlGroupLocation.AppearanceGroup.Font = customFont;
            layoutControlItem.AppearanceItemCaption.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(10.0F, isBold);
            Font customFont10 = Function.CustomArabicFont.customFont;

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

        public void setLocationsObject(Locations locations)
        {
            this.locations = locations;
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
                pleaseSelectItem = "Veuillez sélectionner l'élément !";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
            }

            if (this.locations != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.locations.currentTab == "layoutControlGroupCountries")
                    {
                        if (this.locations.country_id != 0)
                            this.currentItemId = this.locations.country_id;

                        Models.Country country = context.Countries.Find(this.currentItemId);

                        if (country != null)
                        {
                            txtName.Text = country.Name;
                            btnSelect.Enabled = true;
                        }
                        else
                        {
                            btnSelect.Enabled = false;
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                        }
                    }
                    else if (this.locations.currentTab == "layoutControlGroupStates")
                    {
                        if (this.locations.state_id != 0)
                            this.currentItemId = this.locations.state_id;

                        Models.State state = context.States.Find(this.currentItemId);

                        if (state != null)
                        {
                            txtName.Text = state.Name;
                            txtItem.EditValue = state.CountryId;
                            btnSelect.Enabled = true;
                        }
                        else
                        {
                            btnSelect.Enabled = false;
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                        }
                    }
                    else if (this.locations.currentTab == "layoutControlGroupCities")
                    {
                        if (this.locations.city_id != 0)
                            this.currentItemId = this.locations.city_id;

                        Models.City city = context.Cities.Find(this.currentItemId);

                        if (city != null)
                        {
                            txtName.Text = city.Name;
                            txtItem.EditValue = city.StateId;
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
        }

        public void getItems()
        {
            using (var context = new AppDbContext())
            {
                if (this.locations.currentTab == "layoutControlGroupStates")
                {
                    txtItem.Properties.DataSource = context.Countries.ToList();
                }
                else if (this.locations.currentTab == "layoutControlGroupCities")
                {
                    txtItem.Properties.DataSource = context.States.ToList();
                }

                txtItem.Properties.DisplayMember = "Name"; // Set display member
                txtItem.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if(this.locations != null)
            {
                if (this.locations.currentTab == "layoutControlGroupCountries")
                {
                    this.saveCountry();
                }
                else if (this.locations.currentTab == "layoutControlGroupStates")
                {
                    this.saveState();
                }
                else if (this.locations.currentTab == "layoutControlGroupCities")
                {
                    this.saveCity();
                }

                if (this.locations != null)
                    this.locations.loadItems(this.locations.currentTab);
            }

            SplashScreenManager.CloseForm();
        }

        public void saveCountry()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string nameAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                nameAlreadyExists = "A Country with this Name already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
                nameAlreadyExists = "Un pays portant ce nom existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                nameAlreadyExists = "هناك دولة بهذا الاسم بالفعل.";
            }

            if (dxValidationProviderCountry.Validate())
            {
                Models.Country country;
                using (var context = new AppDbContext())
                {
                    if (this.type == "Add")
                    {
                        // Check if a country with the same name already exists
                        if (context.Countries.Any(c => c.Name == txtName.Text))
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new country
                        country = new Models.Country
                        {
                            Name = txtName.Text,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Countries.Add(country);
                        ClearCountryFormFields();
                    }
                    else
                    {
                        if (this.locations != null)
                            this.currentItemId = this.locations.country_id;

                        country = context.Countries.Find(this.currentItemId);

                        if (country != null)
                        {
                            // Check if the new name is unique excluding the current record
                            if (context.Countries.Any(c => c.Name == txtName.Text && c.Id != country.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            country.Name = txtName.Text;
                            country.UpdatedAt = DateTime.Now;

                            context.Entry(country).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            return;
                        }
                    }

                    context.SaveChanges();
                }

                Function.Sound.Added();
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearCountryFormFields()
        {
            txtName.Text = "";
        }

        public void saveState()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string nameAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                nameAlreadyExists = "A State with this Name already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
                nameAlreadyExists = "Un État portant ce nom existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                nameAlreadyExists = "هناك ولاية بهذا الاسم بالفعل.";
            }

            if (dxValidationProviderState.Validate())
            {
                Models.State state;
                using (var context = new AppDbContext())
                {
                    if (this.type == "Add")
                    {
                        // Check if a state with the same name already exists
                        if (context.States.Any(s => s.Name == txtName.Text))
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new state
                        state = new Models.State
                        {
                            Name = txtName.Text,
                            CountryId = int.Parse(txtItem.EditValue.ToString()),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.States.Add(state);
                        ClearStateFormFields();
                    }
                    else
                    {
                        if (this.locations != null)
                            this.currentItemId = this.locations.state_id;

                        state = context.States.Find(this.currentItemId);

                        if (state != null)
                        {
                            // Check if the new name is unique excluding the current record
                            if (context.States.Any(s => s.Name == txtName.Text && s.Id != state.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            state.Name = txtName.Text;
                            state.CountryId = int.Parse(txtItem.EditValue.ToString());
                            state.UpdatedAt = DateTime.Now;

                            context.Entry(state).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            return;
                        }
                    }

                    context.SaveChanges();
                }

                Function.Sound.Added();

                if (this.locations != null)
                    this.locations.loadItems("");
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearStateFormFields()
        {
            txtName.Text = "";
            txtItem.EditValue = null;
        }

        public void saveCity()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string nameAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                nameAlreadyExists = "A City with this Name already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
                nameAlreadyExists = "Une ville portant ce nom existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                nameAlreadyExists = "هناك مدينة بهذا الاسم بالفعل.";
            }

            if (dxValidationProviderCity.Validate())
            {
                Models.City city;
                using (var context = new AppDbContext())
                {
                    if (this.type == "Add")
                    {
                        // Check if a city with the same name already exists
                        if (context.Cities.Any(c => c.Name == txtName.Text))
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new city
                        city = new Models.City
                        {
                            Name = txtName.Text,
                            StateId = int.Parse(txtItem.EditValue.ToString()),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Cities.Add(city);
                        ClearCityFormFields();
                    }
                    else
                    {
                        if (this.locations != null)
                            this.currentItemId = this.locations.city_id;

                        city = context.Cities.Find(this.currentItemId);

                        if (city != null)
                        {
                            // Check if the new name is unique excluding the current record
                            if (context.Cities.Any(c => c.Name == txtName.Text && c.Id != city.Id))
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            city.Name = txtName.Text;
                            city.StateId = int.Parse(txtItem.EditValue.ToString());
                            city.UpdatedAt = DateTime.Now;

                            context.Entry(city).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            return;
                        }
                    }

                    context.SaveChanges();
                }

                Function.Sound.Added();

                if (this.locations != null)
                    this.locations.loadItems("");
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearCityFormFields()
        {
            txtName.Text = "";
            txtItem.EditValue = null;
        }

        private void AddEditCaseType_Load(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string Countries;
            string States;
            string Cities;

            if (lang == "en")
            {
                Countries = "Countries";
                States = "States";
                Cities = "Cities";
            }
            else if (lang == "fr")
            {
                Countries = "Pays";
                States = "États";
                Cities = "Villes";
            }
            else
            {
                Countries = "البلدان";
                States = "الدول";
                Cities = "المدن";
            }

            txtItem.EditValue = "Active";

            if (this.locations.currentTab == "layoutControlGroupCountries")
            {
                layoutControlGroupLocation.Text = Countries; 
                layoutControlItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (this.locations.currentTab == "layoutControlGroupStates")
            {
                layoutControlGroupLocation.Text = States;
                layoutControlItem.Text = Countries;
            }
            else if (this.locations.currentTab == "layoutControlGroupCities")
            {
                layoutControlGroupLocation.Text = Cities;
                layoutControlItem.Text = States;
            }

            this.getItems();
            
            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Country GetCurrentCountry()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Countries.Find(currentItemId);
                else
                    return null;
            }
        }

        private Models.State GetCurrentState()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.States.Find(currentItemId);
                else
                    return null;
            }
        }

        private Models.City GetCurrentCity()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Cities.Find(currentItemId);
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

                if (this.locations.currentTab == "layoutControlGroupCountries")
                {
                    // Retrieve the minimum and maximum ID values from the Brands dataset.
                    int minId = context.Countries.Min(b => b.Id);
                    int maxId = context.Countries.Max(b => b.Id);

                    // Enable or disable navigation buttons based on the current record's ID.
                    btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                    btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                    btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                    btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                    this.type = "Edit";

                    Models.Country currentItem = GetCurrentCountry();

                    if (currentItem != null)
                    {
                        this.currentItemId = currentItem.Id;
                        txtName.Text = currentItem.Name;
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                    }
                }
                else if (this.locations.currentTab == "layoutControlGroupStates")
                {
                    // Retrieve the minimum and maximum ID values from the Brands dataset.
                    int minId = context.States.Min(b => b.Id);
                    int maxId = context.States.Max(b => b.Id);

                    // Enable or disable navigation buttons based on the current record's ID.
                    btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                    btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                    btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                    btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                    this.type = "Edit";

                    Models.State currentItem = GetCurrentState();

                    if (currentItem != null)
                    {
                        this.currentItemId = currentItem.Id;
                        txtName.Text = currentItem.Name;
                        txtItem.EditValue = currentItem.CountryId;
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                    }
                }
                else if (this.locations.currentTab == "layoutControlGroupCities")
                {
                    // Retrieve the minimum and maximum ID values from the Brands dataset.
                    int minId = context.Cities.Min(b => b.Id);
                    int maxId = context.Cities.Max(b => b.Id);

                    // Enable or disable navigation buttons based on the current record's ID.
                    btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                    btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                    btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                    btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                    this.type = "Edit";

                    Models.City currentItem = GetCurrentCity();

                    if (currentItem != null)
                    {
                        this.currentItemId = currentItem.Id;
                        txtName.Text = currentItem.Name;
                        txtItem.EditValue = currentItem.StateId;
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                    }
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
                    if (this.locations.currentTab == "layoutControlGroupCountries")
                    {
                        int? minId = context.Countries.Min(b => (int?)b.Id);
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
                    else if (this.locations.currentTab == "layoutControlGroupStates")
                    {
                        int? minId = context.States.Min(b => (int?)b.Id);
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
                    else if (this.locations.currentTab == "layoutControlGroupCities")
                    {
                        int? minId = context.Cities.Min(b => (int?)b.Id);
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
                    if (this.locations.currentTab == "layoutControlGroupCountries")
                    {
                        int? maxId = context.Countries.Max(b => (int?)b.Id);
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
                    else if (this.locations.currentTab == "layoutControlGroupStates")
                    {
                        int? maxId = context.States.Max(b => (int?)b.Id);
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
                    else if (this.locations.currentTab == "layoutControlGroupCities")
                    {
                        int? maxId = context.Cities.Max(b => (int?)b.Id);
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

                if (this.locations.currentTab == "layoutControlGroupCountries")
                {
                    var nextItem = context.Countries.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                    if (nextItem != null)
                    {
                        currentItemId = nextItem.Id;
                        DisplayCurrentItem();
                    }
                }
                else if (this.locations.currentTab == "layoutControlGroupStates")
                {
                    var nextItem = context.States.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                    if (nextItem != null)
                    {
                        currentItemId = nextItem.Id;
                        DisplayCurrentItem();
                    }
                }
                else if (this.locations.currentTab == "layoutControlGroupCities")
                {
                    var nextItem = context.Cities.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                    if (nextItem != null)
                    {
                        currentItemId = nextItem.Id;
                        DisplayCurrentItem();
                    }
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    if (this.locations.currentTab == "layoutControlGroupCountries")
                    {
                        var prevItem = context.Countries.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                        if (prevItem != null)
                        {
                            currentItemId = prevItem.Id;
                            DisplayCurrentItem();
                        }
                    }
                    else if (this.locations.currentTab == "layoutControlGroupStates")
                    {
                        var prevItem = context.States.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                        if (prevItem != null)
                        {
                            currentItemId = prevItem.Id;
                            DisplayCurrentItem();
                        }
                    }
                    else if (this.locations.currentTab == "layoutControlGroupCities")
                    {
                        var prevItem = context.Cities.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                        if (prevItem != null)
                        {
                            currentItemId = prevItem.Id;
                            DisplayCurrentItem();
                        }
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
                if (this.locations.currentTab == "layoutControlGroupCountries")
                {
                    searchLookUpEdit.Properties.DataSource = context.Countries.ToList();
                }
                else if (this.locations.currentTab == "layoutControlGroupStates")
                {
                    searchLookUpEdit.Properties.DataSource = context.States.ToList();
                }
                else if (this.locations.currentTab == "layoutControlGroupCities")
                {
                    searchLookUpEdit.Properties.DataSource = context.Cities.ToList();
                }

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

        }
    }
}