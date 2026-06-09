using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Pos.Function
{
    /// <summary>
    /// Helper pour validation centralisée dans les formulaires
    /// Utilisé par Phase 3E pour intégrer InputSanitizer
    /// </summary>
    public static class FormValidationHelper
    {
        /// <summary>
        /// Valide et sanitize un TextEdit pour un nom (personne, produit, etc.)
        /// </summary>
        /// <param name="textEdit">Le contrôle TextEdit à valider</param>
        /// <param name="fieldName">Nom du champ pour message erreur</param>
        /// <param name="maxLength">Longueur maximale</param>
        /// <returns>True si valide, False sinon (avec message affiché)</returns>
        public static bool ValidateAndSanitizeName(TextEdit textEdit, string fieldName, int maxLength = 100)
        {
            var validation = InputSanitizer.ValidateName(textEdit.Text, maxLength);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                return false;
            }

            textEdit.Text = validation.sanitized;
            return true;
        }

        /// <summary>
        /// Valide et sanitize un TextEdit pour email
        /// </summary>
        public static bool ValidateAndSanitizeEmail(TextEdit textEdit, string fieldName = "Email")
        {
            // Si vide, c'est OK (champ optionnel dans la plupart des forms)
            if (string.IsNullOrWhiteSpace(textEdit.Text))
                return true;

            var validation = InputSanitizer.ValidateEmail(textEdit.Text);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                return false;
            }

            textEdit.Text = validation.sanitized;
            return true;
        }

        /// <summary>
        /// Valide et sanitize un TextEdit pour téléphone
        /// </summary>
        public static bool ValidateAndSanitizePhone(TextEdit textEdit, string fieldName = "Phone")
        {
            // Si vide, c'est OK (champ optionnel)
            if (string.IsNullOrWhiteSpace(textEdit.Text))
                return true;

            var validation = InputSanitizer.ValidatePhone(textEdit.Text);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                return false;
            }

            textEdit.Text = validation.sanitized;
            return true;
        }

        /// <summary>
        /// Valide et sanitize un MemoEdit pour description/adresse
        /// </summary>
        public static bool ValidateAndSanitizeDescription(Control control, string fieldName, int maxLength = 500)
        {
            // Si vide, c'est OK (champ optionnel)
            if (string.IsNullOrWhiteSpace(control.Text))
                return true;

            var validation = InputSanitizer.ValidateDescription(control.Text, maxLength);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                control.Focus();
                return false;
            }

            control.Text = validation.sanitized;
            return true;
        }

        /// <summary>
        /// Valide et sanitize un TextEdit pour code/SKU
        /// </summary>
        public static bool ValidateAndSanitizeCode(TextEdit textEdit, string fieldName, int maxLength = 50)
        {
            var validation = InputSanitizer.ValidateCode(textEdit.Text, maxLength);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                return false;
            }

            textEdit.Text = validation.sanitized;
            return true;
        }

        /// <summary>
        /// Valide un montant dans un TextEdit
        /// </summary>
        public static bool ValidateAmount(TextEdit textEdit, string fieldName, out decimal amount, bool allowNegative = false)
        {
            var validation = InputSanitizer.ValidateAmount(textEdit.Text, allowNegative);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                amount = 0;
                return false;
            }

            amount = validation.value;
            return true;
        }

        /// <summary>
        /// Valide une quantité dans un TextEdit
        /// </summary>
        public static bool ValidateQuantity(TextEdit textEdit, string fieldName, out int quantity)
        {
            var validation = InputSanitizer.ValidateQuantity(textEdit.Text);

            if (!validation.isValid)
            {
                XtraMessageBox.Show(
                    $"{fieldName} Error: {validation.errorMessage}",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textEdit.Focus();
                quantity = 0;
                return false;
            }

            quantity = validation.value;
            return true;
        }

        /// <summary>
        /// Valide tous les champs Customer requis
        /// Retourne true si tous valides
        /// </summary>
        public static bool ValidateCustomerForm(
            TextEdit txtFirstName,
            TextEdit txtLastName,
            TextEdit txtEmail,
            TextEdit txtPhone,
            Control txtAddress)
        {
            // Valider FirstName (requis)
            if (!ValidateAndSanitizeName(txtFirstName, "First Name", 50))
                return false;

            // Valider LastName (requis)
            if (!ValidateAndSanitizeName(txtLastName, "Last Name", 50))
                return false;

            // Valider Email (optionnel mais si présent doit être valide)
            if (!ValidateAndSanitizeEmail(txtEmail))
                return false;

            // Valider Phone (optionnel mais si présent doit être valide)
            if (!ValidateAndSanitizePhone(txtPhone))
                return false;

            // Valider Address (optionnel)
            if (!ValidateAndSanitizeDescription(txtAddress, "Address", 200))
                return false;

            return true;
        }

        /// <summary>
        /// Valide tous les champs Product requis
        /// </summary>
        public static bool ValidateProductForm(
            TextEdit txtProductName,
            TextEdit txtSKU,
            Control txtDescription)
        {
            // Valider ProductName (requis)
            if (!ValidateAndSanitizeName(txtProductName, "Product Name", 100))
                return false;

            // Valider SKU (requis)
            if (!string.IsNullOrWhiteSpace(txtSKU.Text))
            {
                if (!ValidateAndSanitizeCode(txtSKU, "SKU", 50))
                    return false;
            }

            // Valider Description (optionnel)
            if (!ValidateAndSanitizeDescription(txtDescription, "Description", 500))
                return false;

            return true;
        }

        /// <summary>
        /// Valide tous les champs Employee requis
        /// </summary>
        public static bool ValidateEmployeeForm(
            TextEdit txtFirstName,
            TextEdit txtLastName,
            TextEdit txtEmail,
            TextEdit txtPhone)
        {
            // Valider FirstName (requis)
            if (!ValidateAndSanitizeName(txtFirstName, "First Name", 50))
                return false;

            // Valider LastName (requis)
            if (!ValidateAndSanitizeName(txtLastName, "Last Name", 50))
                return false;

            // Valider Email (optionnel mais si présent doit être valide)
            if (!ValidateAndSanitizeEmail(txtEmail))
                return false;

            // Valider Phone (optionnel mais si présent doit être valide)
            if (!ValidateAndSanitizePhone(txtPhone))
                return false;

            return true;
        }
    }
}
