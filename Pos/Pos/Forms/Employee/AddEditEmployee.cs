using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Employee.Department;
using Pos.Forms.Employee.Position;
using Pos.Forms.Employee.Skill;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
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
using Twilio.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pos.Forms.Employee
{
    public partial class AddEditEmployee : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Employees employees = null;
        public string type = "Add";
        public int currentItemId = 0;
        bool PicChanged = false;
        public AddEditEmployee()
        {
            InitializeComponent();

            SetupSkillsTokenEdit();
            SetupLanguagesTokenEdit();

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

        public void ApplyCustomFont(float fontSize = 10.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            btnAddDepartment.Font = customFont;
            btnAddPosition.Font = customFont;
            btnAddSkill.Font = customFont;

            txtAssured.Font = customFont;
            txtBlackList.Font = customFont;

            layoutControlGroup1.AppearanceGroup.Font = customFont;
            layoutControlGroup2.AppearanceGroup.Font = customFont;
            layoutControlGroup3.AppearanceGroup.Font = customFont;
            layoutControlGroup4.AppearanceGroup.Font = customFont;
            layoutControlGroup5.AppearanceGroup.Font = customFont;
            layoutControlGroup6.AppearanceGroup.Font = customFont;
            layoutControlGroup7.AppearanceGroup.Font = customFont;
            layoutControlGroup8.AppearanceGroup.Font = customFont;
            layoutControlGroup9.AppearanceGroup.Font = customFont;
            layoutControlGroup10.AppearanceGroup.Font = customFont;
            layoutControlGroup11.AppearanceGroup.Font = customFont;
            layoutControlGroup12.AppearanceGroup.Font = customFont;
            layoutControlGroup13.AppearanceGroup.Font = customFont;
            layoutControlGroup14.AppearanceGroup.Font = customFont;
            layoutControlGroup15.AppearanceGroup.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(8.0F, isBold);
            Font customFont8 = Function.CustomArabicFont.customFont;

            layoutControlItem1.AppearanceItemCaption.Font = customFont8;
            layoutControlItem2.AppearanceItemCaption.Font = customFont8;
            layoutControlItem3.AppearanceItemCaption.Font = customFont8;
            layoutControlItem4.AppearanceItemCaption.Font = customFont8;
            layoutControlItem5.AppearanceItemCaption.Font = customFont8;
            layoutControlItem6.AppearanceItemCaption.Font = customFont8;
            layoutControlItem7.AppearanceItemCaption.Font = customFont8;
            layoutControlItem8.AppearanceItemCaption.Font = customFont8;
            layoutControlItem9.AppearanceItemCaption.Font = customFont8;
            layoutControlItem10.AppearanceItemCaption.Font = customFont8;
            layoutControlItem11.AppearanceItemCaption.Font = customFont8;
            layoutControlItem22.AppearanceItemCaption.Font = customFont8;
            layoutControlItem13.AppearanceItemCaption.Font = customFont8;
            layoutControlItem14.AppearanceItemCaption.Font = customFont8;
            layoutControlItem15.AppearanceItemCaption.Font = customFont8;
            layoutControlItem16.AppearanceItemCaption.Font = customFont8;
            layoutControlItem17.AppearanceItemCaption.Font = customFont8;
            layoutControlItem18.AppearanceItemCaption.Font = customFont8;
            layoutControlItem19.AppearanceItemCaption.Font = customFont8;
            layoutControlItem20.AppearanceItemCaption.Font = customFont8;
            layoutControlItem21.AppearanceItemCaption.Font = customFont8;
            layoutControlItem22.AppearanceItemCaption.Font = customFont8;
            layoutControlItem23.AppearanceItemCaption.Font = customFont8;
            layoutControlItem24.AppearanceItemCaption.Font = customFont8;
            layoutControlItem25.AppearanceItemCaption.Font = customFont8;
            layoutControlItem26.AppearanceItemCaption.Font = customFont8;
            layoutControlItem27.AppearanceItemCaption.Font = customFont8;
            layoutControlItem28.AppearanceItemCaption.Font = customFont8;
            layoutControlItem29.AppearanceItemCaption.Font = customFont8;
            layoutControlItem30.AppearanceItemCaption.Font = customFont8;
            layoutControlItem31.AppearanceItemCaption.Font = customFont8;
            layoutControlItem32.AppearanceItemCaption.Font = customFont8;
            layoutControlItem33.AppearanceItemCaption.Font = customFont8;
            layoutControlItem34.AppearanceItemCaption.Font = customFont8;
            layoutControlItem35.AppearanceItemCaption.Font = customFont8;
            layoutControlItem36.AppearanceItemCaption.Font = customFont8;
            layoutControlItem37.AppearanceItemCaption.Font = customFont8;
            layoutControlItem38.AppearanceItemCaption.Font = customFont8;
            layoutControlItem39.AppearanceItemCaption.Font = customFont8;
            layoutControlItem40.AppearanceItemCaption.Font = customFont8;
            layoutControlItem41.AppearanceItemCaption.Font = customFont8;
            layoutControlItem42.AppearanceItemCaption.Font = customFont8;
            layoutControlItem43.AppearanceItemCaption.Font = customFont8;
            layoutControlItem44.AppearanceItemCaption.Font = customFont8;
            layoutControlItem45.AppearanceItemCaption.Font = customFont8;
            layoutControlItem46.AppearanceItemCaption.Font = customFont8;
            layoutControlItem47.AppearanceItemCaption.Font = customFont8;
            layoutControlItem48.AppearanceItemCaption.Font = customFont8;
            layoutControlItem49.AppearanceItemCaption.Font = customFont8;
            layoutControlItem50.AppearanceItemCaption.Font = customFont8;
            layoutControlItem51.AppearanceItemCaption.Font = customFont8;
            layoutControlItem52.AppearanceItemCaption.Font = customFont8;
            layoutControlItem53.AppearanceItemCaption.Font = customFont8;
            layoutControlItem54.AppearanceItemCaption.Font = customFont8;
            layoutControlItem55.AppearanceItemCaption.Font = customFont8;
            layoutControlItem56.AppearanceItemCaption.Font = customFont8;
            layoutControlItem57.AppearanceItemCaption.Font = customFont8;
            layoutControlItem58.AppearanceItemCaption.Font = customFont8;
            layoutControlItem59.AppearanceItemCaption.Font = customFont8;
            layoutControlItem60.AppearanceItemCaption.Font = customFont8;
            layoutControlItem61.AppearanceItemCaption.Font = customFont8;
            layoutControlItem62.AppearanceItemCaption.Font = customFont8;
            layoutControlItem63.AppearanceItemCaption.Font = customFont8;
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

        private void SetupSkillsTokenEdit()
        {
            using (var context = new AppDbContext())
            {
                // Fetch skills data
                var skills = context.Skills.ToList();

                // Populate the TokenEdit with the list of skills
                foreach (Models.Skill skill in skills)
                {
                    txtSkills.Properties.Tokens.Add(new TokenEditToken(skill.Name, skill.Id));
                }
            }
        }

        private void SetupLanguagesTokenEdit()
        {
            // Define the list of languages
            List<string> languages = new List<string>
            {
                "Afrikaans", "Albanian", "Amharic", "Arabic", "Armenian", "Azerbaijani", "Bambara", "Basque", "Belarusian",
                "Bengali", "Bhojpuri", "Bosnian", "Bulgarian", "Burmese", "Catalan", "Cebuano", "Chechen", "Chinese (Cantonese)",
                "Chinese (Mandarin)", "Corsican", "Croatian", "Czech", "Danish", "Dari", "Dutch", "Dzongkha", "English",
                "Esperanto", "Estonian", "Faroese", "Fijian", "Finnish", "French", "Galician", "Georgian", "German",
                "Greek", "Gujarati", "Haitian Creole", "Hausa", "Hawaiian", "Hebrew", "Hindi", "Hmong", "Hungarian",
                "Icelandic", "Igbo", "Ilocano", "Indonesian", "Irish", "Italian", "Japanese", "Javanese", "Kannada",
                "Kazakh", "Khmer", "Kinyarwanda", "Kirundi", "Korean", "Kurdish", "Kyrgyz", "Lao", "Latin", "Latvian",
                "Lithuanian", "Luxembourgish", "Macedonian", "Malagasy", "Malay", "Malayalam", "Maltese", "Maori",
                "Marathi", "Mongolian", "Nepali", "Norwegian", "Odia", "Pashto", "Persian (Farsi)", "Polish", "Portuguese",
                "Punjabi", "Quechua", "Romanian", "Russian", "Samoan", "Sanskrit", "Serbian", "Shona", "Sindhi", "Sinhala",
                "Slovak", "Slovenian", "Somali", "Spanish", "Swahili", "Swedish", "Tajik", "Tamil", "Tatar", "Telugu",
                "Thai", "Tibetan", "Tigrinya", "Tongan", "Turkish", "Turkmen", "Ukrainian", "Urdu", "Uyghur", "Uzbek",
                "Vietnamese", "Welsh", "Wolof", "Xhosa", "Yiddish", "Yoruba", "Zulu"
            };

            // Populate the TokenEdit with the list of languages
            foreach (string language in languages)
            {
                txtLanguagesSpoken.Properties.Tokens.Add(new TokenEditToken(language, language));
            }
        }

        public void setEmployeesObject(Employees employees)
        {
            this.employees = employees;
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

            if (this.employees != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.employees.employee_id != 0)
                        this.currentItemId = this.employees.employee_id;

                    this.employees.employee_id = 0;

                    Models.Employee employee = context.Employees.Find(this.currentItemId);

                    if (employee != null)
                    {
                        txtFirstName.Text = employee.FirstName;
                        txtLastName.Text = employee.LastName;
                        txtEmail.Text = employee.Email;
                        txtImage.EditValue = employee.Image;
                        txtGender.EditValue = employee.Gender;
                        txtHireDate.EditValue = employee.HireDate;
                        txtNameOfFather.Text = employee.NameOfFather;
                        txtNameOfMother.Text = employee.NameOfMother;
                        txtAddress.Text = employee.Address;
                        txtHeight.EditValue = employee.Height;
                        txtWeight.EditValue = employee.Weight;
                        txtDateOfBirth.EditValue = employee.DateOfBirth;
                        txtFamilySituation.Text = employee.FamilySituation;
                        txtBloodGroup.Text = employee.BloodGroup;
                        txtCivility.Text = employee.Civility;
                        txtSpecialMarque.Text = employee.SpecialMarque;
                        txtPhoneNumber.Text = employee.PhoneNumber;
                        txtAssured.EditValue = employee.Assured;
                        txtNoCard.Text = employee.NoCard;
                        txtCardDeliveryAt.EditValue = employee.CardDeliveryAt;
                        txtNoPass.Text = employee.NoPass;
                        txtPassDeliveryAt.EditValue = employee.PassDeliveryAt;
                        txtBlackList.EditValue = employee.BlackList;
                        txtSpouseName.Text = employee.SpouseName;
                        txtShortBiography.Text = employee.ShortBiography;
                        txtChildrenCount.EditValue = employee.ChildrenCount;
                        txtEmergencyContactPhone.Text = employee.EmergencyContactPhone;
                        txtEmergencyContactRelation.Text = employee.EmergencyContactRelation;
                        txtEmergencyContactName.Text = employee.EmergencyContactName;
                        txtEducationLevel.Text = employee.EducationLevel;
                        txtExperienceYears.Text = employee.ExperienceYears;
                        txtPreviousEmployer.Text = employee.PreviousEmployer;
                        txtCertifications.Text = employee.Certifications;
                        txtLanguagesSpoken.EditValue = employee.LanguagesSpoken;
                        txtSkills.EditValue = employee.Skills;
                        txtWorkHoursPerWeek.EditValue = employee.WorkHoursPerWeek;
                        txtVacationDays.EditValue = employee.VacationDays;
                        txtSickDays.EditValue = employee.SickDays;
                        txtLastPromotionDate.EditValue = employee.LastPromotionDate;
                        txtLinkedInProfile.Text = employee.LinkedInProfile;
                        txtEmploymentType.Text = employee.EmploymentType;
                        txtNote.Text = employee.Note;
                        txtCodePostal.Text = employee.CodePostal;
                        txtStatus.Text = employee.Status;
                        txtPosition.EditValue = employee.PositionId;
                        txtDepartment.EditValue = employee.DepartmentId;
                        txtBusinessLocation.EditValue = employee.BusinessLocationId;
                        txtCountry.EditValue = employee.CountryId;
                        txtState.EditValue = employee.StateId;
                        txtCity.EditValue = employee.CityId;

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


        private void btnSave_Click(object sender, EventArgs e)
        {
            // ===== PHASE 3E: INPUT VALIDATION =====
            if (!Function.FormValidationHelper.ValidateEmployeeForm(
                txtFirstName,
                txtLastName,
                txtEmail,
                txtPhoneNumber))
            {
                return; // Validation échouée
            }
            // ===== FIN VALIDATION =====

            //SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);
            DateTime date;
            if (dxValidationProvider.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Image image = txtImage.Image;

                    byte[] imageBytes;

                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    image.Save(memoryStream, ImageFormat.Png);
                    //    imageBytes = memoryStream.ToArray();
                    //}

                    Models.Employee employee;

                    if (this.type == "Add")
                    {
                        employee = new Models.Employee();
                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.Email = txtEmail.Text;
                        if (PicChanged)
                        {
                            // Get the image from the PictureEdit control
                            if (txtImage.Image != null)
                            {
                                image = txtImage.Image;

                                // Convert the image to a byte array using a MemoryStream

                                using (var memoryStream = new MemoryStream())
                                {
                                    image.Save(memoryStream, ImageFormat.Png); // Choose the appropriate format based on your needs
                                    imageBytes = memoryStream.ToArray();
                                }

                                employee.Image = imageBytes;
                            }
                        }
                        PicChanged = false;

                        employee.Status = txtStatus.Text;
                        employee.Gender = txtGender.Text;
                        employee.PositionId = int.Parse(txtPosition.EditValue.ToString());
                        employee.DepartmentId = int.Parse(txtDepartment.EditValue.ToString());

                        if (txtHireDate.EditValue is DateTime HireDateValue)
                        {
                            employee.HireDate = DateOnly.FromDateTime(HireDateValue);
                        }

                        employee.NameOfFather = txtNameOfFather.Text;
                        employee.NameOfMother = txtNameOfMother.Text;
                        employee.Address = txtAddress.Text;
                        employee.Height = decimal.Parse(txtHeight.EditValue.ToString());
                        employee.Weight = decimal.Parse(txtWeight.EditValue.ToString());
                        employee.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
                        employee.FamilySituation = txtFamilySituation.Text;
                        employee.BloodGroup = txtBloodGroup.Text;
                        employee.Civility = txtCivility.Text;
                        employee.SpecialMarque = txtSpecialMarque.Text;
                        employee.PhoneNumber = txtPhoneNumber.Text;
                        employee.Assured = txtAssured.IsOn ? true : false;
                        employee.NoCard = txtNoCard.Text;

                        if (DateTime.TryParse(txtCardDeliveryAt.Text, out date))
                        {
                            employee.CardDeliveryAt = date;
                        }

                        employee.NoPass = txtNoPass.Text;

                        if (DateTime.TryParse(txtPassDeliveryAt.Text, out date))
                        {
                            employee.PassDeliveryAt = date;
                        }

                        employee.BlackList = txtBlackList.IsOn ? true : false;
                        employee.SpouseName = txtSpouseName.Text;
                        employee.ShortBiography = txtShortBiography.Text;
                        employee.ChildrenCount = int.Parse(txtChildrenCount.Text);
                        employee.EmergencyContactPhone = txtEmergencyContactPhone.Text;
                        employee.EmergencyContactRelation = txtEmergencyContactRelation.Text;
                        employee.EmergencyContactName = txtEmergencyContactName.Text;
                        employee.EducationLevel = txtEducationLevel.Text;
                        employee.ExperienceYears = txtExperienceYears.Text;
                        employee.PreviousEmployer = txtPreviousEmployer.Text;
                        employee.Certifications = txtCertifications.Text;
                        employee.LanguagesSpoken = txtLanguagesSpoken.EditValue != null ? txtLanguagesSpoken.EditValue.ToString() : null;
                        employee.Skills = txtSkills.EditValue != null ? txtSkills.EditValue.ToString() : null;
                        employee.WorkHoursPerWeek = int.Parse(txtWorkHoursPerWeek.Text);
                        employee.VacationDays = int.Parse(txtVacationDays.Text);
                        employee.SickDays = int.Parse(txtSickDays.Text);
                        if (DateTime.TryParse(txtLastPromotionDate.Text, out date))
                        {
                            employee.LastPromotionDate = date;
                        }

                        employee.LinkedInProfile = txtLinkedInProfile.Text;
                        employee.EmploymentType = txtEmploymentType.Text;
                        employee.Note = txtNote.Text;
                        employee.CodePostal = txtCodePostal.Text;
                        employee.Status = txtStatus.Text;
                        employee.BusinessLocationId = int.Parse(txtBusinessLocation.EditValue.ToString());
                        employee.CountryId = int.Parse(txtCountry.EditValue.ToString());
                        employee.StateId = int.Parse(txtState.EditValue.ToString());
                        employee.CityId = int.Parse(txtCity.EditValue.ToString());
                        employee.UserId = Properties.Settings.Default.userId;

                        employee.CreatedAt = DateTime.Now;
                        employee.UpdatedAt = DateTime.Now;

                        context.Employees.Add(employee);
                        context.SaveChanges();

                        employee.Code = Function.Helper.generateCode(employee.Id);

                        context.Employees.Update(employee);
                        context.SaveChanges();

                        ClearData();
                    }
                    else
                    {
                        if (this.employees != null)
                        {
                            if (this.employees.employee_id != 0)
                            {
                                this.currentItemId = this.employees.employee_id;
                            }
                        }


                        employee = context.Employees.Find(this.currentItemId);

                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.Email = txtEmail.Text;
                        if (PicChanged)
                        {
                            // Get the image from the PictureEdit control
                            if (txtImage.Image != null)
                            {
                                image = txtImage.Image;

                                // Convert the image to a byte array using a MemoryStream

                                using (var memoryStream = new MemoryStream())
                                {
                                    image.Save(memoryStream, ImageFormat.Png); // Choose the appropriate format based on your needs
                                    imageBytes = memoryStream.ToArray();
                                }

                                employee.Image = imageBytes;
                            }

                        }
                        PicChanged = false;

                        employee.Status = txtStatus.Text;
                        employee.Gender = txtGender.Text;

                        employee.PositionId = int.Parse(txtPosition.EditValue.ToString());
                        employee.DepartmentId = int.Parse(txtDepartment.EditValue.ToString());

                        if (txtHireDate.EditValue is DateTime HireDateValue)
                        {
                            employee.HireDate = DateOnly.FromDateTime(HireDateValue);
                        }

                        employee.NameOfFather = txtNameOfFather.Text;
                        employee.NameOfMother = txtNameOfMother.Text;
                        employee.Address = txtAddress.Text;
                        employee.Height = decimal.Parse(txtHeight.EditValue.ToString());
                        employee.Weight = decimal.Parse(txtWeight.EditValue.ToString());
                        employee.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
                        employee.FamilySituation = txtFamilySituation.Text;
                        employee.BloodGroup = txtBloodGroup.Text;
                        employee.Civility = txtCivility.Text;
                        employee.SpecialMarque = txtSpecialMarque.Text;
                        employee.PhoneNumber = txtPhoneNumber.Text;
                        employee.Assured = txtAssured.IsOn ? true : false;
                        employee.NoCard = txtNoCard.Text;
                        if (DateTime.TryParse(txtCardDeliveryAt.Text, out date))
                        {
                            employee.CardDeliveryAt = date;
                        }

                        employee.NoPass = txtNoPass.Text;

                        employee.BlackList = txtBlackList.IsOn ? true : false;
                        employee.SpouseName = txtSpouseName.Text;
                        employee.ShortBiography = txtShortBiography.Text;
                        employee.ChildrenCount = int.Parse(txtChildrenCount.Text);
                        employee.EmergencyContactPhone = txtEmergencyContactPhone.Text;
                        employee.EmergencyContactRelation = txtEmergencyContactRelation.Text;
                        employee.EmergencyContactName = txtEmergencyContactName.Text;
                        employee.EducationLevel = txtEducationLevel.Text;
                        employee.ExperienceYears = txtExperienceYears.Text;
                        employee.PreviousEmployer = txtPreviousEmployer.Text;
                        employee.Certifications = txtCertifications.Text;
                        employee.LanguagesSpoken = txtLanguagesSpoken.EditValue != null ? txtLanguagesSpoken.EditValue.ToString() : null;
                        employee.Skills = txtSkills.EditValue != null ? txtSkills.EditValue.ToString() : null;
                        employee.WorkHoursPerWeek = int.Parse(txtWorkHoursPerWeek.Text);
                        employee.VacationDays = int.Parse(txtVacationDays.Text);
                        employee.SickDays = int.Parse(txtSickDays.Text);
                        if (DateTime.TryParse(txtLastPromotionDate.Text, out date))
                        {
                            employee.LastPromotionDate = date;
                        }
                        if (DateTime.TryParse(txtPassDeliveryAt.Text, out date))
                        {
                            employee.PassDeliveryAt = date;
                        }

                        employee.LinkedInProfile = txtLinkedInProfile.Text;
                        employee.EmploymentType = txtEmploymentType.Text;
                        employee.Note = txtNote.Text;
                        employee.CodePostal = txtCodePostal.Text;
                        employee.Status = txtStatus.Text;
                        employee.BusinessLocationId = int.Parse(txtBusinessLocation.EditValue.ToString());
                        employee.CountryId = int.Parse(txtCountry.EditValue.ToString());
                        employee.StateId = int.Parse(txtState.EditValue.ToString());
                        employee.CityId = int.Parse(txtCity.EditValue.ToString());

                        employee.UpdatedAt = DateTime.Now;

                        context.Employees.Update(employee);
                        context.SaveChanges();
                    }

                    context.SaveChanges();

                    Function.Sound.Added();

                    if (this.employees != null)
                        this.employees.loadEmployees();

                }
            }
            else
            {
                Function.Sound.Wrong();
            }

            //SplashScreenManager.CloseForm();
        }

        public void getBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBusinessLocation.Properties.DataSource = context.BusinessLocations.ToList();
                txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
                txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getDepartments()
        {
            using (var context = new AppDbContext())
            {
                txtDepartment.Properties.DataSource = context.Departments.ToList();
                txtDepartment.Properties.DisplayMember = "DepartmentName"; // Set display member
                txtDepartment.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public void getPositions()
        {
            using (var context = new AppDbContext())
            {
                txtPosition.Properties.DataSource = context.Positions.ToList();
                txtPosition.Properties.DisplayMember = "Title"; // Set display member
                txtPosition.Properties.ValueMember = "Id"; // Set value member
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

        public void getCities(int stateId)
        {
            using (var context = new AppDbContext())
            {
                txtCity.Properties.DataSource = context.Cities.Where(c => c.StateId == stateId).ToList();
                txtCity.Properties.DisplayMember = "Name"; // Set display member
                txtCity.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void AddEditEmployee_Load(object sender, EventArgs e)
        {
            txtStatus.EditValue = "Active";
            txtGender.EditValue = "Man";

            this.getBusinessLocations();

            this.getCountries();

            this.getDepartments();

            this.getPositions();

            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        public void ClearData()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtCountry.Clear();
            txtState.Clear();
            txtGender.Clear();
            txtCity.Clear();
            txtEducationLevel.Clear();
            txtDateOfBirth.Clear();
            txtCodePostal.Clear();
            txtAddress.Clear();
            txtBloodGroup.Clear();
            txtBusinessLocation.Clear();
            txtCardDeliveryAt.Clear();
            txtCertifications.Clear();
            txtChildrenCount.Clear();
            txtCivility.Clear();
            txtCodePostal.Clear();
            txtDepartment.Clear();
            txtEmail.Clear();
            txtEmergencyContactName.Clear();
            txtEmergencyContactPhone.Clear();
            txtEmergencyContactRelation.Clear();
            txtEmploymentType.Clear();
            txtExperienceYears.Clear();
            txtFamilySituation.Clear();
            txtHeight.Clear();
            txtHireDate.Clear();
            txtLastPromotionDate.Clear();
            txtLinkedInProfile.Clear();
            txtNameOfFather.Clear();
            txtNameOfMother.Clear();
            txtNoCard.Clear();
            txtNoPass.Clear();

            txtNote.Clear();
            txtPassDeliveryAt.Clear();
            txtPhoneNumber.Clear();
            txtPosition.Clear();
            txtPreviousEmployer.Clear();
            txtShortBiography.Clear();
            txtSickDays.Clear();
            txtSpecialMarque.Clear();
            txtSpouseName.Clear();

            txtVacationDays.Clear();
            txtWeight.Clear();
            txtWorkHoursPerWeek.Clear();
            txtStatus.EditValue = "Active";
            txtSkills.EditValue = null;
            txtLanguagesSpoken.EditValue = null;


        }
        private void btnSupplierReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtImage.EditValue = null;
            txtStatus.EditValue = "Active";
            txtGender.EditValue = "Man";

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;

            Function.Sound.Added();
        }

        private Models.Employee GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Employees.Find(currentItemId);
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
                int minId = context.Employees.Min(b => b.Id);
                int maxId = context.Employees.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Employee currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtFirstName.Text = currentItem.FirstName;
                    txtLastName.Text = currentItem.LastName;
                    txtEmail.Text = currentItem.Email;
                    txtImage.EditValue = currentItem.Image;
                    txtGender.EditValue = currentItem.Gender;
                    txtHireDate.EditValue = currentItem.HireDate;
                    txtNameOfFather.Text = currentItem.NameOfFather;
                    txtNameOfMother.Text = currentItem.NameOfMother;
                    txtAddress.Text = currentItem.Address;
                    txtHeight.EditValue = currentItem.Height;
                    txtWeight.EditValue = currentItem.Weight;
                    txtDateOfBirth.EditValue = currentItem.DateOfBirth;
                    txtFamilySituation.Text = currentItem.FamilySituation;
                    txtBloodGroup.Text = currentItem.BloodGroup;
                    txtCivility.Text = currentItem.Civility;
                    txtSpecialMarque.Text = currentItem.SpecialMarque;
                    txtPhoneNumber.Text = currentItem.PhoneNumber;
                    txtAssured.EditValue = currentItem.Assured;
                    txtNoCard.Text = currentItem.NoCard;
                    txtCardDeliveryAt.EditValue = currentItem.CardDeliveryAt;
                    txtNoPass.Text = currentItem.NoPass;
                    txtPassDeliveryAt.EditValue = currentItem.PassDeliveryAt;
                    txtBlackList.EditValue = currentItem.BlackList;
                    txtSpouseName.Text = currentItem.SpouseName;
                    txtShortBiography.Text = currentItem.ShortBiography;
                    txtChildrenCount.EditValue = currentItem.ChildrenCount;
                    txtEmergencyContactPhone.Text = currentItem.EmergencyContactPhone;
                    txtEmergencyContactRelation.Text = currentItem.EmergencyContactRelation;
                    txtEmergencyContactName.Text = currentItem.EmergencyContactName;
                    txtEducationLevel.Text = currentItem.EducationLevel;
                    txtExperienceYears.Text = currentItem.ExperienceYears;
                    txtPreviousEmployer.Text = currentItem.PreviousEmployer;
                    txtCertifications.Text = currentItem.Certifications;
                    txtLanguagesSpoken.EditValue = currentItem.LanguagesSpoken;
                    txtSkills.EditValue = currentItem.Skills;
                    txtWorkHoursPerWeek.EditValue = currentItem.WorkHoursPerWeek;
                    txtVacationDays.EditValue = currentItem.VacationDays;
                    txtSickDays.EditValue = currentItem.SickDays;
                    txtLastPromotionDate.EditValue = currentItem.LastPromotionDate;
                    txtLinkedInProfile.Text = currentItem.LinkedInProfile;
                    txtEmploymentType.Text = currentItem.EmploymentType;
                    txtNote.Text = currentItem.Note;
                    txtCodePostal.Text = currentItem.CodePostal;
                    txtStatus.Text = currentItem.Status;
                    txtPosition.EditValue = currentItem.PositionId;
                    txtDepartment.EditValue = currentItem.DepartmentId;
                    txtBusinessLocation.EditValue = currentItem.BusinessLocationId;
                    txtCountry.EditValue = currentItem.CountryId;
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
                    int? minId = context.Employees.Min(b => (int?)b.Id);
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
                    int? maxId = context.Employees.Max(b => (int?)b.Id);
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

                var nextItem = context.Employees.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    currentItemId = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            if (currentItemId != 0)
            {
                using (var context = new AppDbContext())
                {
                    var prevItem = context.Employees.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
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
                searchLookUpEdit.Properties.DataSource = context.Employees.ToList();
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

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void txtCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCountry.EditValue != null)
            {
                this.getStates(int.Parse(txtCountry.EditValue.ToString()));
            }
        }

        private void txtState_EditValueChanged(object sender, EventArgs e)
        {
            if (txtState.EditValue != null)
            {
                this.getCities(int.Parse(txtState.EditValue.ToString()));
            }
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            AddEditDepartment addEditDepartment = new AddEditDepartment();
            addEditDepartment.ShowDialog();
        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            AddEditPosition addEditPosition = new AddEditPosition();
            addEditPosition.ShowDialog();
        }

        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            AddEditSkill addEditSkill = new AddEditSkill();
            addEditSkill.ShowDialog();
        }

        private void txtImage_EditValueChanged(object sender, EventArgs e)
        {
            PicChanged = true;
        }

        private void txtDepartment_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddDepartement")
            {
                // Show the message

                AddEditDepartment addEditDepartment = new AddEditDepartment(this);
                addEditDepartment.ShowDialog();
            }
        }
        public void setDepartment(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtDepartment.Properties.DataSource = AppDb.Departments.ToList();
                    txtDepartment.EditValue = id;
                }
            }
            catch { }

        }

        private void txtPosition_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddPosition")
            {
                // Show the message

                AddEditPosition addEditPosition = new AddEditPosition(this);
                addEditPosition.ShowDialog();
            }
        }
        public void setPosition(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtPosition.Properties.DataSource = AppDb.Positions.ToList();
                    txtPosition.EditValue = id;
                }
            }
            catch { }

        }



        private void txtBusinessLocation_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Tag != null && e.Button.Tag.ToString() == "AddBusinessLocation")
            {
                // Show the message

                AddEditBusinessLocation addEditBusinessLocation = new AddEditBusinessLocation();
                addEditBusinessLocation.ShowDialog();
            }
        }
        public void setBusinessLocation(int id)
        {
            try
            {
                using (AppDbContext AppDb = new AppDbContext())
                {
                    txtBusinessLocation.Properties.DataSource = AppDb.BusinessLocations.ToList();
                    txtBusinessLocation.EditValue = id;
                }
            }
            catch { }

        }
    }
}