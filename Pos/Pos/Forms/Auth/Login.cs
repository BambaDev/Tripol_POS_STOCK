using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Pos.Models;
using Pos.Forms.Overlay;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Native;
using DevExpress.XtraLayout;
using DevExpress.XtraPdfViewer.Native;

namespace Pos.Forms.Auth
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        private OverlayForm overlay;
        public int BusinessLocationID = 0;

        public Login()
        {
            InitializeComponent();

            this.toRtl();
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

        public void ApplyCustomFont(float fontSize = 16.0F, bool isBold = false)
        {
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            btnLogin.Font = customFont;
            btnReset.Font = customFont;
            btnSendItViaEmail.Font = customFont;

            layoutControl1.Font = customFont;
            layoutControlGroup11.AppearanceItemCaption.Font = customFont;
            layoutControlGroup2.AppearanceTabPage.Header.Font = customFont;
            layoutControlItem5.AppearanceItemCaption.Font = customFont;
            layoutControlItem6.AppearanceItemCaption.Font = customFont;
            simpleLabelItem1.AppearanceItemCaption.Font = customFont;
            simpleLabelItem3.AppearanceItemCaption.Font = customFont;
            simpleLabelItem2.AppearanceItemCaption.Font = customFont;

            simpleButton1.Font = customFont;
            simpleButton2.Font = customFont;
            simpleButton3.Font = customFont;
            simpleButton4.Font = customFont;
            simpleButton5.Font = customFont;
            simpleButton6.Font = customFont;
            simpleButton7.Font = customFont;
            simpleButton8.Font = customFont;
            simpleButton9.Font = customFont;
            simpleButton10.Font = customFont;
            simpleButton11.Font = customFont;
            simpleButton12.Font = customFont;
            simpleButton13.Font = customFont;
            simpleButton14.Font = customFont;
            simpleButton15.Font = customFont;
            simpleButton16.Font = customFont;
            simpleButton17.Font = customFont;
            simpleButton18.Font = customFont;
            simpleButton19.Font = customFont;
            simpleButton20.Font = customFont;
            simpleLabelItem4.AppearanceItemCaption.Font = customFont;
            simpleLabelItem5.AppearanceItemCaption.Font = customFont;

            txtPassword.Properties.Appearance.Font = customFont;
            txtUsername.Properties.Appearance.Font = customFont;
        }

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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            string lang = Properties.Settings.Default.Lang;

            string usernameOrPasswordIsIncorrect = "";
            string error = "";
            string anErrorOccurred = "";
            string pleaseEnterUsernamePassword = "";

            if (lang == "en")
            {
                usernameOrPasswordIsIncorrect = "Username Or Password Is Incorrect";
                pleaseEnterUsernamePassword = "Please Enter Username & Password";
                anErrorOccurred = "An error occurred";
                error = "error";
            }
            else if (lang == "fr")
            {
                usernameOrPasswordIsIncorrect = "L'identifiant ou le mot de passe est incorrect";
                pleaseEnterUsernamePassword = "Veuillez saisir votre nom d'utilisateur et votre mot de passe";
                anErrorOccurred = "Une erreur s'est produite";
                error = "erreur";
            }
            else
            {
                usernameOrPasswordIsIncorrect = "اسم المستخدم أو كلمة المرور غير صحيحة";
                pleaseEnterUsernamePassword = "الرجاء إدخال اسم المستخدم وكلمة المرور";
                anErrorOccurred = "حدث خطأ";
                error = "خطأ";
            }

            try
            {
                if (dxValidationProviderLogin.Validate())
                {
                    // ===== PHASE 3F: RATE LIMITING PROGRESSIF =====
                    // Appliquer délai progressif AVANT vérification credentials
                    // Ralentit les attaques brute force (0s, 2s, 5s, 10s, 20s, 30s)
                    BruteForceProtection.ApplyProgressiveDelay(txtUsername.Text);
                    // ===== FIN RATE LIMITING =====

                    // PROTECTION BRUTE FORCE: Vérifier si le compte est verrouillé
                    if (BruteForceProtection.IsAccountLocked(txtUsername.Text, out int remainingMinutes))
                    {
                        btnLogin.Enabled = true;
                        Sound.Wrong();

                        string lockoutMessage = lang == "en"
                            ? $"Account locked due to too many failed attempts. Try again in {remainingMinutes} minutes."
                            : lang == "fr"
                            ? $"Compte verrouillé en raison de trop de tentatives échouées. Réessayez dans {remainingMinutes} minutes."
                            : $"تم قفل الحساب بسبب عدد كبير من المحاولات الفاشلة. حاول مرة أخرى بعد {remainingMinutes} دقيقة.";

                        XtraMessageBox.Show(lockoutMessage, error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Appliquer un délai progressif basé sur les échecs précédents
                    BruteForceProtection.ApplyProgressiveDelay(txtUsername.Text);

                    using (var context = new AppDbContext())
                    {
                        // Perform asynchronous database query
                        Models.User user = await Task.Run(() =>
                            context.Users.Include(u => u.Role).SingleOrDefault(
                                u => u.UserLogin == txtUsername.Text
                            )
                        );

                        // PROTECTION TIMING ATTACK: Toujours vérifier le mot de passe
                        // même si l'utilisateur n'existe pas (temps constant ~100ms)
                        const string DUMMY_HASH = "$2a$11$DummyHashForTimingAttackProtectionXYZ123456789012345678901234";
                        bool passwordValid = user != null
                            ? PasswordHelper.VerifyPassword(txtPassword.Text, user.Password)
                            : PasswordHelper.VerifyPassword(txtPassword.Text, DUMMY_HASH);

                        if (user == null || !passwordValid)
                        {
                            // Enregistrer la tentative échouée
                            BruteForceProtection.RecordLoginAttempt(
                                txtUsername.Text,
                                false,
                                user == null ? "User not found" : "Invalid password"
                            );

                            btnLogin.Enabled = true;
                            Sound.Wrong();
                            XtraMessageBox.Show(usernameOrPasswordIsIncorrect, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Check if business location ID is not null
                            if (user.BusinessLocationId == null)
                            {
                                XtraMessageBox.Show("User's business location is not set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            Properties.Settings.Default.BusinessLocationId = (int)user.BusinessLocationId;

                            // Store user settings
                            Properties.Settings.Default.userId = user.Id;
                            Properties.Settings.Default.isAdmin = user.Role.Name;
                            Properties.Settings.Default.UserRoleID = user.Role.Id;
                            Properties.Settings.Default.hasLogin = true;

                            var setting = Function.Helper.getSetting();

                            Properties.Settings.Default.PrinterDocument = setting.PrinterDocument;
                            Properties.Settings.Default.PrinterReciept = setting.PrinterReciept;
                            Properties.Settings.Default.Lang = setting.Lang;
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

                            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);

                            // Check if register records exist
                            var register = context.Registers.FirstOrDefault();
                            if (register == null)
                            {
                                XtraMessageBox.Show("No register found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Properties.Settings.Default.CashRegisterId = register.Id;

                            // Ensure default currency exists
                            Models.Currency currency = await Task.Run(() => Function.Helper.getDefualtCurrency());
                            if (currency == null)
                            {
                                XtraMessageBox.Show("Default currency not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            Properties.Settings.Default.DefaultCurrency = currency.Code;
                            Properties.Settings.Default.DefaultCurrencyDirection = currency.Direction ?? false;

                            Properties.Settings.Default.CurrentUserFullName = $"{user.FirstName} {user.LastName}";

                            Properties.Settings.Default.Save();

                            // Enregistrer la tentative réussie et réinitialiser les échecs
                            BruteForceProtection.RecordLoginAttempt(txtUsername.Text, true);
                            BruteForceProtection.ResetFailedAttempts(txtUsername.Text);

                            // Créer une session sécurisée
                            SessionManager.CreateSession(user.Id);

                            Sound.Added();

                            ShowOverlay();
                            ShowCustomAlert showCustomAlert = new ShowCustomAlert(null, 2500);
                            showCustomAlert.HideMessage();
                            showCustomAlert.HideMessage();
                            showCustomAlert.ShowDialog();
                            HideOverlay();

                            MainFrm mainFrm = new MainFrm();
                            mainFrm.ShowDialog();

                            this.Close();
                        }
                    }
                }
                else
                {
                    btnLogin.Enabled = true;
                    Sound.Wrong();
                    XtraMessageBox.Show(pleaseEnterUsernamePassword, error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                btnLogin.Enabled = true;
                // Handle any unexpected errors
                Sound.Wrong();

                // SÉCURISÉ: Ne jamais exposer les détails techniques à l'utilisateur
                // Log l'erreur pour les développeurs, message générique pour l'utilisateur
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}\n{ex.StackTrace}");

                XtraMessageBox.Show(anErrorOccurred, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Login_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnSendItViaEmail_Click(object sender, EventArgs e)
        {
            //this.SendPasswordResetToken(txtResetEmailPassword.Text);

            //PasswordResetForm passwordResetForm = new PasswordResetForm();
            //passwordResetForm.ShowDialog();
        }

        private void btnPoweredBySkyStudio_Click(object sender, EventArgs e)
        {
            string url = "https://skystudio-agency.com";

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Required to open the URL in the default web browser
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open the link. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnManageDatabases_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            ShowOverlay();
            Forms.Setting.QuickAuth quickAuth = new Setting.QuickAuth();
            quickAuth.ShowDialog();
            HideOverlay();
        }
    }
}