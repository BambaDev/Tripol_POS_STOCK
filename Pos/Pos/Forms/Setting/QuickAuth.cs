using DevExpress.XtraEditors;
using Pos.Forms.Overlay;
using Pos.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Setting
{
    public partial class QuickAuth : DevExpress.XtraEditors.XtraForm
    {
        private OverlayForm overlay;

        public QuickAuth()
        {
            InitializeComponent();
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

        private void QuickAuth_Load(object sender, EventArgs e)
        {

        }

        private void btnQuickAuth_Click(object sender, EventArgs e)
        {
            btnQuickAuth.Enabled = false;
            string lang = Properties.Settings.Default.Lang;

            string usernameOrPasswordIsIncorrect = "";
            string error = "";
            string anErrorOccurred = "";
            string pleaseEnterUsernamePassword = "";

            if (lang == "en")
            {
                usernameOrPasswordIsIncorrect = "Password Is Incorrect";
                pleaseEnterUsernamePassword = "Please Enter Your Password";
                anErrorOccurred = "An error occurred";
                error = "error";
            }
            else if (lang == "fr")
            {
                usernameOrPasswordIsIncorrect = "le mot de passe est incorrect";
                pleaseEnterUsernamePassword = "Veuillez saisir votre mot de passe";
                anErrorOccurred = "Une erreur s'est produite";
                error = "erreur";
            }
            else
            {
                usernameOrPasswordIsIncorrect = "كلمة المرور غير صحيحة";
                pleaseEnterUsernamePassword = "الرجاء إدخال كلمة المرور";
                anErrorOccurred = "حدث خطأ";
                error = "خطأ";
            }

            try { 
                if(dxValidationProvider.Validate())
                {
                    if(txtPassword.Text == Properties.Settings.Default.SuperAdminPassword)
                    {
                        Sound.Selected();
                        Forms.Setting.Backups backups = new Forms.Setting.Backups();
                        backups.hideBackupButton();
                        backups.hideDeleteColumnButton();
                        backups.ShowDialog();
                        this.Close();
                    } else
                    {
                        btnQuickAuth.Enabled = true;
                        Sound.Wrong();
                        XtraMessageBox.Show(usernameOrPasswordIsIncorrect, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    btnQuickAuth.Enabled = true;
                    Sound.Wrong();
                    XtraMessageBox.Show(pleaseEnterUsernamePassword, error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                btnQuickAuth.Enabled = true;
                Sound.Wrong();
                XtraMessageBox.Show($"{anErrorOccurred} : {ex.Message}", error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}