using System;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4 RGPD: Niveaux de sensibilité des données selon RGPD/GDPR
    /// </summary>
    public enum SensitivityLevel
    {
        /// <summary>
        /// Données publiques - Aucune restriction
        /// Exemples: Nom de produit, catégories, informations publiques
        /// </summary>
        Public = 0,

        /// <summary>
        /// Données internes - Usage interne uniquement
        /// Exemples: Notes internes, statuts, références
        /// </summary>
        Internal = 1,

        /// <summary>
        /// Données confidentielles - PII standard (Article 6 GDPR)
        /// Exemples: Nom, email, téléphone, adresse
        /// Requiert consentement et protection appropriée
        /// </summary>
        Confidential = 2,

        /// <summary>
        /// Données restreintes - Catégories spéciales (Article 9 GDPR)
        /// Exemples: Santé, données biométriques, origine ethnique
        /// Requiert consentement explicite et mesures de sécurité renforcées
        /// </summary>
        Restricted = 3,

        /// <summary>
        /// Données hautement sensibles - Protection maximale
        /// Exemples: Numéros passeport, données médicales détaillées, informations bancaires
        /// Requiert encryption at-rest et in-transit, audit logging systématique
        /// </summary>
        Classified = 4
    }

    /// <summary>
    /// PHASE 4 RGPD: Catégories de données sensibles pour classification
    /// </summary>
    public static class DataCategory
    {
        public const string PII = "PII";                    // Personal Identifiable Information
        public const string Health = "Health";              // Article 9 GDPR - Health data
        public const string Financial = "Financial";        // Banking, payroll, transactions
        public const string Biometric = "Biometric";        // Article 9 GDPR - Fingerprints, photos, blood type
        public const string Identity = "Identity";          // Passport, ID card, driving license
        public const string Contact = "Contact";            // Email, phone, address
        public const string Behavioral = "Behavioral";      // Purchase history, preferences
        public const string Legal = "Legal";                // Contracts, legal documents
        public const string Security = "Security";          // Passwords, PINs, tokens
    }

    /// <summary>
    /// PHASE 4 RGPD: Attribut pour marquer les propriétés contenant des données sensibles
    /// Utilisé pour classification automatique, encryption, masking, et audit
    /// </summary>
    /// <example>
    /// <code>
    /// [SensitiveData(Level = SensitivityLevel.Confidential, Category = DataCategory.PII, RequiresEncryption = true)]
    /// public string Email { get; set; }
    ///
    /// [SensitiveData(Level = SensitivityLevel.Restricted, Category = DataCategory.Health, RequiresEncryption = true, LogAccessAttempts = true)]
    /// public string HealthCondition { get; set; }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SensitiveDataAttribute : Attribute
    {
        /// <summary>
        /// Niveau de sensibilité de la donnée selon classification GDPR
        /// </summary>
        public SensitivityLevel Level { get; set; } = SensitivityLevel.Confidential;

        /// <summary>
        /// Catégorie de la donnée (PII, Health, Financial, etc.)
        /// Utilisé pour appliquer les règles d'anonymisation appropriées
        /// </summary>
        public string Category { get; set; } = DataCategory.PII;

        /// <summary>
        /// Si true, la donnée doit être encryptée at-rest dans la base de données
        /// Utilise AES-256-GCM via FieldEncryption helper
        /// </summary>
        public bool RequiresEncryption { get; set; } = false;

        /// <summary>
        /// Si true, tous les accès à cette propriété sont loggés dans AuditTrail
        /// Utilisé pour données Article 9 GDPR (santé, biométrie)
        /// </summary>
        public bool LogAccessAttempts { get; set; } = false;

        /// <summary>
        /// Si true, la valeur est masquée dans les exports et audit trails
        /// Exemples: "john@test.com" → "j***@test.com", "0555123456" → "***3456"
        /// </summary>
        public bool MaskInAuditTrail { get; set; } = true;

        /// <summary>
        /// Si true, cette donnée doit être anonymisée lors d'une demande d'effacement GDPR
        /// Si false, la donnée peut être conservée pour obligations légales (comptabilité, fiscal)
        /// </summary>
        public bool SubjectToErasure { get; set; } = true;

        /// <summary>
        /// Durée de rétention en jours après dernière utilisation
        /// -1 = retention indéfinie (cas légaux comme transactions fiscales)
        /// 0 = aucune rétention spécifique
        /// </summary>
        public int RetentionDays { get; set; } = -1;

        /// <summary>
        /// Raison de la collecte (base légale GDPR: Consent, Contract, Legal, Legitimate Interest)
        /// </summary>
        public string LegalBasis { get; set; } = "Consent";

        /// <summary>
        /// Description lisible par l'utilisateur pour export GDPR
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public SensitiveDataAttribute()
        {
        }

        /// <summary>
        /// Constructeur avec niveau et catégorie
        /// </summary>
        public SensitiveDataAttribute(SensitivityLevel level, string category)
        {
            Level = level;
            Category = category;
        }
    }

    /// <summary>
    /// PHASE 4 RGPD: Attribut pour marquer les propriétés exclues de l'encryption automatique
    /// Utilisé pour propriétés techniques (ID, CreatedAt) ou déjà hashées (Password)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NoEncryptionAttribute : Attribute
    {
        public string Reason { get; set; }

        public NoEncryptionAttribute(string reason = "Technical field or already protected")
        {
            Reason = reason;
        }
    }

    /// <summary>
    /// PHASE 4 RGPD: Attribut pour marquer les entités complètes comme contenant des données sensibles
    /// Applique des règles de protection par défaut à toutes les propriétés
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SensitiveEntityAttribute : Attribute
    {
        public SensitivityLevel DefaultLevel { get; set; } = SensitivityLevel.Confidential;
        public bool RequiresAuditLogging { get; set; } = true;
        public string Description { get; set; }

        public SensitiveEntityAttribute()
        {
        }

        public SensitiveEntityAttribute(SensitivityLevel defaultLevel, string description)
        {
            DefaultLevel = defaultLevel;
            Description = description;
        }
    }
}
