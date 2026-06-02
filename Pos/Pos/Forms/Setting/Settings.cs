using DevExpress.XtraEditors;
using DevExpress.XtraPdfViewer.Native;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.DataProcessing.InMemoryDataProcessor.AddSurrogateOperationAlgorithm;

namespace Pos.Forms.Setting
{
    public partial class Settings : DevExpress.XtraEditors.XtraForm
    {
        public string lang = "en";

        public Settings()
        {
            InitializeComponent();
        }

        private void btnSettingWhatsApp_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderWhatsApp.Validate())
            {
                Models.Setting setting = Shared.db.Settings.FirstOrDefault();

                if (setting != null)
                {
                    setting.WhatsAppStatus = txtWhatsAppStatus.Text;
                    setting.AccountSid = txtAccountSid.Text;
                    setting.AuthToken = txtAuthToken.Text;
                    setting.FromPhoneNumber = txtFromPhoneNumber.Text;
                    setting.WhatsAppMaintStatus = txtWhatsAppMaintStatus.Text;
                    setting.WhatsAppMaintInvoiceStatus = txtWhatsAppMaintInvoiceStatus.Text;
                    setting.WhatsAppPhoneCode = txtWhatsAppPhoneCode.EditValue.ToString();
                    setting.UpdatedAt = DateTime.Now;

                    Shared.db.Entry(setting).State = EntityState.Modified;
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();
            }
            else
            {
                Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        public void preparePhoneCode()
        {
            txtWhatsAppPhoneCode.Properties.DataSource = PhoneCode.GetCountryPhoneCodes();
            txtWhatsAppPhoneCode.Properties.DisplayMember = "Name";
            txtWhatsAppPhoneCode.Properties.ValueMember = "Code";
            txtWhatsAppPhoneCode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        public void getData()
        {
            Models.Setting setting = Shared.db.Settings.FirstOrDefault();

            if (setting != null)
            {
                Logo.EditValue = setting.Logo;
                txtCompany.Text = setting.Company;
                txtEmail.Text = setting.Email;
                txtDescription.Text = setting.Description;
                txtWebsite.Text = setting.Website;
                txtTitle.Text = setting.Title;
                txtSubTitle.Text = setting.SubTitle;
                txtAddress.Text = setting.Address;
                txtTel.Text = setting.Tel;
                txtFax.Text = setting.Fax;
                txtCompte.Text = setting.Compte;
                txtRib.Text = setting.Rib;
                txtNis.Text = setting.Nis;
                txtRc.Text = setting.Rc;
                txtAi.Text = setting.Ai;
                txtIdFiscal.Text = setting.IdFiscal;

                LockScreenImg.EditValue = setting.LockScreenImg;
                IsLockScreen.EditValue = setting.IsLockScreen;

                isSoundAdded.EditValue = setting.IsSoundAdded;
                isSoundDeleted.EditValue = setting.IsSoundDeleted;
                isSoundSelected.EditValue = setting.IsSoundSelected;
                isSoundDenied.EditValue = setting.IsSoundDenied;
                isSoundWrong.EditValue = setting.IsSoundWrong;
                ckIsQuantityPopUp.EditValue = (bool)setting.IsQuantityPopUp;

                PosTopBanner.EditValue = setting.PosTopBanner;
                PosBottomBanner.EditValue = setting.PosBottomBanner;
                PosCategories.EditValue = setting.PosCategories;
                PosCategoriesWithoutImgs.EditValue = setting.PosCategoriesWithoutImgs;
                PosProductsWithoutImgs.EditValue = setting.PosProductsWithoutImgs;
                
                PoslayoutControlGroupLatestOrders.EditValue = setting.PoslayoutControlGroupLatestOrders;
                PoslayoutControlGroupLatestCustomers.EditValue = setting.PoslayoutControlGroupLatestCustomers;
                PoslayoutControlGroupLatestSuppliers.EditValue = setting.PoslayoutControlGroupLatestSuppliers;
                PoslayoutControlGroupSalesReturns.EditValue = setting.PoslayoutControlGroupSalesReturns;
                PoslayoutControlGroupHold.EditValue = setting.PoslayoutControlGroupHold;
                PoslayoutControlGroupUnpaidOrders.EditValue = setting.PoslayoutControlGroupUnpaidOrders;

                txtWhatsAppStatus.EditValue = setting.WhatsAppStatus;
                txtAccountSid.Text = setting.AccountSid;
                txtAuthToken.Text = setting.AuthToken;
                txtFromPhoneNumber.Text = setting.FromPhoneNumber;
                txtWhatsAppMaintStatus.EditValue = setting.WhatsAppMaintStatus;
                txtWhatsAppMaintInvoiceStatus.EditValue = setting.WhatsAppMaintInvoiceStatus;
                txtWhatsAppPhoneCode.EditValue = setting.WhatsAppPhoneCode;

                txtMailMailer.Text = setting.MailMailer;
                txtMailStatus.EditValue = setting.MailStatus;
                txtAllowHtml.EditValue = setting.MailAllowHtml;
                txtMailHost.Text = setting.MailHost;
                txtMailPort.EditValue = setting.MailPort;
                txtMailUsername.Text = setting.MailUsername;
                txtMailPassword.Text = setting.MailPassword;

                txtPrintDocument.EditValue = setting.PrinterDocumentId;
                txtPrintReciept.EditValue = setting.PrinterRecieptId;

                if (setting.Lang == "en")
                {
                    txtLang.SelectedIndex = 0;
                }
                else if (setting.Lang == "fr")
                {
                    txtLang.SelectedIndex = 1;
                }
                else
                {
                    txtLang.SelectedIndex = 2;
                }

                Properties.Settings.Default.PrinterDocument = setting.PrinterDocument;
                Properties.Settings.Default.PrinterReciept = setting.PrinterReciept;
            }
            else
            {
                Function.Sound.Wrong();
                XtraMessageBox.Show("Please Add Setting !");
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.getData();
            this.preparePhoneCode();
            this.getPrintersForLookUpReciept();
            this.getPrintersForLookUpDocument();
        }

        public void getPrintersForLookUpReciept()
        {
            txtPrintReciept.Properties.DataSource = Shared.db.Printers.ToList();
            txtPrintReciept.Properties.DisplayMember = "Title";
            txtPrintReciept.Properties.ValueMember = "Id";
        }

        public void getPrintersForLookUpDocument()
        {
            txtPrintDocument.Properties.DataSource = Shared.db.Printers.ToList();
            txtPrintDocument.Properties.DisplayMember = "Title";
            txtPrintDocument.Properties.ValueMember = "Id";
        }

        private void btnSaveMail_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProviderMail.Validate())
            {
                Models.Setting setting = Shared.db.Settings.FirstOrDefault();

                if (setting != null)
                {
                    setting.MailMailer = txtMailMailer.Text;
                    setting.MailStatus = txtMailStatus.Text;
                    setting.MailAllowHtml = txtAllowHtml.Text;
                    setting.MailHost = txtMailHost.Text;
                    setting.MailPort = int.Parse(txtMailPort.Text);
                    setting.MailUsername = txtMailUsername.Text;
                    setting.MailPassword = txtMailPassword.Text;
                    setting.UpdatedAt = DateTime.Now;

                    Shared.db.Entry(setting).State = EntityState.Modified;
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();
            }
            else
            {
                Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string settingsSavedSuccessfully;
            string anErrorOccurred;
            string success;
            string error;

            if (lang == "en")
            {
                settingsSavedSuccessfully = "Settings saved successfully";
                anErrorOccurred = "An error occurred while saving the settings:";
                success = "Success";
                error = "Error";
            }
            else if (lang == "fr")
            {
                settingsSavedSuccessfully = "Paramètres enregistrés avec succès";
                anErrorOccurred = "Une erreur s'est produite lors de l'enregistrement des paramètres :";
                success = "Succès";
                error = "Erreur";
            }
            else
            {
                settingsSavedSuccessfully = "تم حفظ الإعدادات بنجاح";
                anErrorOccurred = "حدث خطأ أثناء حفظ الإعدادات:";
                success = "النجاح";
                error = "خطأ";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    Models.Setting setting = Shared.db.Settings.FirstOrDefault();

                    if (setting != null)
                    {
                        byte[] imageBytes = null, lockSceenBytes = null;

                        // Save Logo image
                        if (Logo.Image != null)
                        {
                            System.Drawing.Image image = Logo.Image;
                            using (var memoryStream = new MemoryStream())
                            {
                                image.Save(memoryStream, ImageFormat.Png);
                                imageBytes = memoryStream.ToArray();
                            }
                        }

                        // Save LockScreen image
                        if (LockScreenImg.Image != null)
                        {
                            System.Drawing.Image lockSceen = LockScreenImg.Image;
                            using (var memoryStream = new MemoryStream())
                            {
                                lockSceen.Save(memoryStream, ImageFormat.Png);
                                lockSceenBytes = memoryStream.ToArray();
                            }
                        }

                        // Assign the byte arrays to the settings only if they are not null
                        if (imageBytes != null)
                        {
                            setting.Logo = imageBytes;
                        }

                        if (lockSceenBytes != null)
                        {
                            setting.LockScreenImg = lockSceenBytes;
                        }

                        setting.IsLockScreen = IsLockScreen.IsOn;

                        Properties.Settings.Default.IsLockScreen = (bool)setting.IsLockScreen;

                        setting.IsSoundAdded = isSoundAdded.IsOn;
                        setting.IsSoundDeleted = isSoundDeleted.IsOn;
                        setting.IsSoundSelected = isSoundSelected.IsOn;
                        setting.IsSoundDenied = isSoundDenied.IsOn;
                        setting.IsSoundWrong = isSoundWrong.IsOn;
                        setting.IsQuantityPopUp = ckIsQuantityPopUp.Checked;

                        setting.PosTopBanner = PosTopBanner.IsOn;
                        setting.PosBottomBanner = PosBottomBanner.IsOn;
                        setting.PosCategories = PosCategories.IsOn;
                        setting.PosCategoriesWithoutImgs = PosCategoriesWithoutImgs.IsOn;
                        setting.PosProductsWithoutImgs = PosProductsWithoutImgs.IsOn;
                        
                        setting.PoslayoutControlGroupLatestOrders = PoslayoutControlGroupLatestOrders.IsOn;
                        setting.PoslayoutControlGroupLatestCustomers = PoslayoutControlGroupLatestCustomers.IsOn;
                        setting.PoslayoutControlGroupLatestSuppliers = PoslayoutControlGroupLatestSuppliers.IsOn;
                        setting.PoslayoutControlGroupSalesReturns = PoslayoutControlGroupSalesReturns.IsOn;
                        setting.PoslayoutControlGroupHold = PoslayoutControlGroupHold.IsOn;
                        setting.PoslayoutControlGroupUnpaidOrders = PoslayoutControlGroupUnpaidOrders.IsOn;

                        setting.Company = txtCompany.Text;
                        setting.Email = txtEmail.Text;
                        setting.Description = txtDescription.Text;
                        setting.Website = txtWebsite.Text;
                        setting.Title = txtTitle.Text;
                        setting.SubTitle = txtSubTitle.Text;
                        setting.Address = txtAddress.Text;
                        setting.Tel = txtTel.Text;
                        setting.Fax = txtFax.Text;
                        setting.Compte = txtCompte.Text;
                        setting.Rib = txtRib.Text;
                        setting.Nis = txtNis.Text;
                        setting.Rc = txtRc.Text;
                        setting.Ai = txtAi.Text;
                        setting.IdFiscal = txtIdFiscal.Text;

                        if (txtPrintDocument.EditValue != null)
                        {
                            setting.PrinterDocument = txtPrintDocument.Text;
                            setting.PrinterDocumentId = int.Parse(txtPrintDocument.EditValue.ToString());
                        }

                        if (txtPrintReciept.EditValue != null)
                        {
                            setting.PrinterReciept = txtPrintReciept.Text;
                            setting.PrinterRecieptId = int.Parse(txtPrintReciept.EditValue.ToString());
                        }

                        setting.Lang = this.lang;

                        setting.UpdatedAt = DateTime.Now;

                        context.Entry(setting).State = EntityState.Modified;
                        context.SaveChanges();

                        this.getData();

                        Function.Sound.Added();

                        if (Properties.Settings.Default.Lang != setting.Lang)
                        {
                            //this.NotifyLanguageChange();
                            Properties.Settings.Default.Save();

                            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

                            LanguageChangedNotifier.OnLanguageChanged();
                        }

                        Properties.Settings.Default.Lang = setting.Lang;

                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);
                        
                        Properties.Settings.Default.PosTopBanner = (bool)setting.PosTopBanner;
                        Properties.Settings.Default.PosBottomBanner = (bool)setting.PosBottomBanner;
                        Properties.Settings.Default.PosCategories = (bool)setting.PosCategories;
                        Properties.Settings.Default.PosCategoriesWithoutImgs = (bool)setting.PosCategoriesWithoutImgs;
                        Properties.Settings.Default.PosProductsWithoutImgs = (bool)setting.PosProductsWithoutImgs;
                        Properties.Settings.Default.PoslayoutControlGroupLatestOrders = (bool)setting.PoslayoutControlGroupLatestOrders;
                        Properties.Settings.Default.PoslayoutControlGroupLatestCustomers = (bool)setting.PoslayoutControlGroupLatestCustomers;
                        Properties.Settings.Default.PoslayoutControlGroupLatestSuppliers = (bool)setting.PoslayoutControlGroupLatestSuppliers;
                        Properties.Settings.Default.PoslayoutControlGroupSalesReturns = (bool)setting.PoslayoutControlGroupSalesReturns;
                        Properties.Settings.Default.PoslayoutControlGroupHold = (bool)setting.PoslayoutControlGroupHold;
                        Properties.Settings.Default.PoslayoutControlGroupUnpaidOrders = (bool)setting.PoslayoutControlGroupUnpaidOrders;
                        Properties.Settings.Default.Save();

                        MessageBox.Show(settingsSavedSuccessfully, success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.getData();

                    Function.Sound.Added();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{anErrorOccurred} {ex.Message}", error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtLang.SelectedIndex == 0)
            {
                this.lang = "en";
            }
            else if (txtLang.SelectedIndex == 1)
            {
                this.lang = "fr";
            }
            else if (txtLang.SelectedIndex == 2)
            {
                this.lang = "ar";
            }
        }
    }
}