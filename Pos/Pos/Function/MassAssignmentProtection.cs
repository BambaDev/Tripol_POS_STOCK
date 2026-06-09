using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 3G: Protection contre Mass Assignment attacks
    /// Empêche modification de propriétés sensibles via binding automatique
    /// </summary>

    /// <summary>
    /// Attribut pour marquer les propriétés qui ne peuvent PAS être modifiées via mass assignment
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ProtectedFromMassAssignmentAttribute : Attribute
    {
        public string Reason { get; set; }

        public ProtectedFromMassAssignmentAttribute(string reason = "Security sensitive property")
        {
            Reason = reason;
        }
    }

    /// <summary>
    /// Attribut pour marquer les propriétés AUTORISÉES en mass assignment (whitelist)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowMassAssignmentAttribute : Attribute
    {
    }

    /// <summary>
    /// Helper pour valider mass assignment
    /// </summary>
    public static class MassAssignmentProtection
    {
        /// <summary>
        /// Valide qu'une propriété peut être modifiée via mass assignment
        /// </summary>
        public static bool CanAssignProperty(PropertyInfo property)
        {
            // Si marquée comme protégée, refuser
            if (property.GetCustomAttribute<ProtectedFromMassAssignmentAttribute>() != null)
            {
                return false;
            }

            // Si marquée comme autorisée, accepter
            if (property.GetCustomAttribute<AllowMassAssignmentAttribute>() != null)
            {
                return true;
            }

            // Par défaut, autoriser les propriétés simples
            // Bloquer les propriétés sensibles par nom
            string[] sensitivePropertyNames = {
                "Id",
                "CreatedAt",
                "UpdatedAt",
                "IsAdmin",
                "RoleId",
                "Password",
                "PasswordHash",
                "Salt",
                "IsDeleted",
                "IsArchived",
                "IsActive" // Peut être modifié explicitement mais pas via binding
            };

            return !sensitivePropertyNames.Contains(property.Name, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Obtient la liste des propriétés autorisées pour mass assignment
        /// </summary>
        public static IEnumerable<PropertyInfo> GetAllowedProperties(Type entityType)
        {
            return entityType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && CanAssignProperty(p));
        }

        /// <summary>
        /// Copie uniquement les propriétés autorisées d'un objet source vers destination
        /// Utilise la whitelist pour sécurité
        /// </summary>
        public static void SafeCopy<T>(T source, T destination, params string[] allowedProperties) where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                // Vérifier whitelist si fournie
                if (allowedProperties != null && allowedProperties.Length > 0)
                {
                    if (!allowedProperties.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                    {
                        continue; // Skip si pas dans whitelist
                    }
                }

                // Vérifier protection mass assignment
                if (!CanAssignProperty(property))
                {
                    continue; // Skip si protégé
                }

                // Copier valeur
                var value = property.GetValue(source);
                property.SetValue(destination, value);
            }
        }

        /// <summary>
        /// Valide qu'un dictionnaire de valeurs ne contient que des propriétés autorisées
        /// Utilisé pour valider form data / JSON payloads
        /// </summary>
        public static (bool isValid, string errorMessage, List<string> blockedProperties)
            ValidateAssignment(Type entityType, Dictionary<string, object> values)
        {
            var blockedProperties = new List<string>();
            var allowedProperties = GetAllowedProperties(entityType)
                .Select(p => p.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var key in values.Keys)
            {
                if (!allowedProperties.Contains(key))
                {
                    blockedProperties.Add(key);
                }
            }

            if (blockedProperties.Any())
            {
                return (
                    false,
                    $"Properties {string.Join(", ", blockedProperties)} cannot be modified via mass assignment",
                    blockedProperties
                );
            }

            return (true, string.Empty, new List<string>());
        }

        /// <summary>
        /// Obtient les propriétés protégées d'un type
        /// Utile pour logging/debugging
        /// </summary>
        public static IEnumerable<string> GetProtectedProperties(Type entityType)
        {
            return entityType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => !CanAssignProperty(p))
                .Select(p => p.Name);
        }
    }

    /// <summary>
    /// Extension methods pour faciliter l'usage
    /// </summary>
    public static class MassAssignmentExtensions
    {
        /// <summary>
        /// Vérifie si une propriété est protégée contre mass assignment
        /// </summary>
        public static bool IsProtectedFromMassAssignment(this PropertyInfo property)
        {
            return !MassAssignmentProtection.CanAssignProperty(property);
        }

        /// <summary>
        /// Obtient les propriétés sûres pour mass assignment
        /// </summary>
        public static IEnumerable<PropertyInfo> GetSafeProperties(this Type type)
        {
            return MassAssignmentProtection.GetAllowedProperties(type);
        }
    }
}
