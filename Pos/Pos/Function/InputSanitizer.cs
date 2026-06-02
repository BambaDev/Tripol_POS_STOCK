using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pos.Function
{
    /// <summary>
    /// Sanitize et valide les entrées utilisateur
    /// Prévient XSS, SQL injection, injection commandes
    /// </summary>
    public static class InputSanitizer
    {
        /// <summary>
        /// Valide un nom (personne, produit, etc.)
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidateName(string name, int maxLength = 100)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return (false, "Name cannot be empty", null);
            }

            // Trim espaces
            string sanitized = name.Trim();

            // Longueur
            if (sanitized.Length > maxLength)
            {
                return (false, $"Name too long (max {maxLength} characters)", null);
            }

            // Caractères dangereux
            char[] dangerousChars = { '<', '>', '{', '}', '|', '\\', '^', '~', '[', ']', '`', '\0' };
            if (sanitized.IndexOfAny(dangerousChars) >= 0)
            {
                return (false, "Name contains invalid characters", null);
            }

            // Patterns SQL injection basiques
            string[] sqlPatterns = { "--", "/*", "*/", "xp_", "sp_", "exec ", "execute ", "drop ", "truncate " };
            string lowerName = sanitized.ToLower();

            foreach (string pattern in sqlPatterns)
            {
                if (lowerName.Contains(pattern))
                {
                    return (false, "Name contains prohibited patterns", null);
                }
            }

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Valide une adresse email
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return (false, "Email cannot be empty", null);
            }

            string sanitized = email.Trim().ToLower();

            // Regex email simple mais robuste
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(sanitized, emailPattern))
            {
                return (false, "Invalid email format", null);
            }

            // Longueur max
            if (sanitized.Length > 254)
            {
                return (false, "Email too long", null);
            }

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Valide un numéro de téléphone
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return (false, "Phone cannot be empty", null);
            }

            // Enlever caractères autorisés (espaces, tirets, parenthèses)
            string digitsOnly = Regex.Replace(phone, @"[\s\-\(\)\+]", "");

            // Vérifier que ne contient que chiffres
            if (!Regex.IsMatch(digitsOnly, @"^\d+$"))
            {
                return (false, "Phone must contain only digits", null);
            }

            // Longueur raisonnable (6-15 chiffres)
            if (digitsOnly.Length < 6 || digitsOnly.Length > 15)
            {
                return (false, "Phone must be 6-15 digits", null);
            }

            // Format standardisé
            string sanitized = phone.Trim();

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Valide une description/notes (texte libre mais sécurisé)
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidateDescription(string description, int maxLength = 500)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return (true, string.Empty, string.Empty); // Description optionnelle
            }

            string sanitized = description.Trim();

            // Longueur
            if (sanitized.Length > maxLength)
            {
                return (false, $"Description too long (max {maxLength} characters)", null);
            }

            // Enlever caractères de contrôle dangereux
            sanitized = Regex.Replace(sanitized, @"[\x00-\x08\x0B\x0C\x0E-\x1F]", "");

            // Patterns script injection
            string[] scriptPatterns = { "<script", "javascript:", "onerror=", "onload=", "<iframe", "<object", "<embed" };
            string lowerDesc = sanitized.ToLower();

            foreach (string pattern in scriptPatterns)
            {
                if (lowerDesc.Contains(pattern))
                {
                    return (false, "Description contains prohibited content", null);
                }
            }

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Valide un code/référence (alphanumérique + tirets/underscores)
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidateCode(string code, int maxLength = 50)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return (false, "Code cannot be empty", null);
            }

            string sanitized = code.Trim().ToUpper();

            // Longueur
            if (sanitized.Length > maxLength)
            {
                return (false, $"Code too long (max {maxLength} characters)", null);
            }

            // Seulement alphanumérique + tiret + underscore
            if (!Regex.IsMatch(sanitized, @"^[A-Z0-9\-_]+$"))
            {
                return (false, "Code can only contain letters, numbers, hyphens and underscores", null);
            }

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Valide une URL
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitized) ValidateURL(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return (true, string.Empty, string.Empty); // URL optionnelle
            }

            string sanitized = url.Trim();

            // Vérifier format URI
            if (!Uri.TryCreate(sanitized, UriKind.Absolute, out Uri uriResult))
            {
                return (false, "Invalid URL format", null);
            }

            // Protocoles autorisés uniquement
            string[] allowedSchemes = { "http", "https" };
            if (!allowedSchemes.Contains(uriResult.Scheme.ToLower()))
            {
                return (false, "Only HTTP and HTTPS URLs are allowed", null);
            }

            return (true, string.Empty, sanitized);
        }

        /// <summary>
        /// Enlève caractères HTML/Script (basic XSS prevention)
        /// </summary>
        public static string StripHTML(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Enlever balises HTML
            string stripped = Regex.Replace(input, @"<[^>]+>", string.Empty);

            // Décoder entités HTML basiques
            stripped = stripped.Replace("&lt;", "<")
                              .Replace("&gt;", ">")
                              .Replace("&amp;", "&")
                              .Replace("&quot;", "\"")
                              .Replace("&#39;", "'");

            return stripped;
        }

        /// <summary>
        /// Valide un montant monétaire
        /// </summary>
        public static (bool isValid, string errorMessage, decimal value) ValidateAmount(string amountText, bool allowNegative = false)
        {
            if (string.IsNullOrWhiteSpace(amountText))
            {
                return (false, "Amount cannot be empty", 0);
            }

            // Enlever espaces et symboles monétaires
            string cleaned = amountText.Trim()
                                      .Replace("$", "")
                                      .Replace("€", "")
                                      .Replace("DA", "")
                                      .Replace(",", "")
                                      .Trim();

            // Parser en decimal
            if (!decimal.TryParse(cleaned, out decimal amount))
            {
                return (false, "Invalid amount format", 0);
            }

            // Négatif autorisé ?
            if (!allowNegative && amount < 0)
            {
                return (false, "Amount cannot be negative", 0);
            }

            // Limite raisonnable
            if (Math.Abs(amount) > BusinessLimits.MAX_SALE_AMOUNT)
            {
                return (false, $"Amount too large (max {MoneyHelper.Format(BusinessLimits.MAX_SALE_AMOUNT)})", 0);
            }

            return (true, string.Empty, amount);
        }

        /// <summary>
        /// Valide une quantité (entier positif)
        /// </summary>
        public static (bool isValid, string errorMessage, int value) ValidateQuantity(string quantityText)
        {
            if (string.IsNullOrWhiteSpace(quantityText))
            {
                return (false, "Quantity cannot be empty", 0);
            }

            if (!int.TryParse(quantityText.Trim(), out int quantity))
            {
                return (false, "Invalid quantity format", 0);
            }

            if (quantity < 0)
            {
                return (false, "Quantity cannot be negative", 0);
            }

            const int MAX_QUANTITY = 1000000;
            if (quantity > MAX_QUANTITY)
            {
                return (false, $"Quantity too large (max {MAX_QUANTITY:N0})", 0);
            }

            return (true, string.Empty, quantity);
        }

        /// <summary>
        /// Sanitize requête de recherche (prévient injection SQL via LIKE)
        /// </summary>
        public static string SanitizeSearchQuery(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return string.Empty;

            string sanitized = searchQuery.Trim();

            // Échapper caractères spéciaux SQL LIKE
            sanitized = sanitized.Replace("[", "[[]")
                                .Replace("%", "[%]")
                                .Replace("_", "[_]");

            // Limite longueur
            const int MAX_SEARCH_LENGTH = 100;
            if (sanitized.Length > MAX_SEARCH_LENGTH)
            {
                sanitized = sanitized.Substring(0, MAX_SEARCH_LENGTH);
            }

            return sanitized;
        }
    }
}
