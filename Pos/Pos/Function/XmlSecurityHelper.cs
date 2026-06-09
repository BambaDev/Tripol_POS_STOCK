using System;
using System.Xml;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 3G: Protection contre XXE (XML External Entity) attacks
    /// Configure XmlReader avec settings sécurisés
    /// </summary>
    public static class XmlSecurityHelper
    {
        /// <summary>
        /// Crée un XmlReaderSettings sécurisé qui prévient XXE attacks
        /// </summary>
        public static XmlReaderSettings GetSecureXmlReaderSettings()
        {
            return new XmlReaderSettings
            {
                // CRITIQUE: Désactiver DTD (Document Type Definition)
                // Les DTD permettent les external entities = XXE vulnerability
                DtdProcessing = DtdProcessing.Prohibit,

                // Désactiver XmlResolver pour empêcher résolution d'URIs externes
                XmlResolver = null,

                // Limites pour DoS prevention
                MaxCharactersFromEntities = 1024,
                MaxCharactersInDocument = 10_000_000, // 10 MB

                // Validation désactivée (pas besoin de schéma externe)
                ValidationType = ValidationType.None,

                // Ignorer whitespace pour performance
                IgnoreWhitespace = true,
                IgnoreComments = true,
                IgnoreProcessingInstructions = true
            };
        }

        /// <summary>
        /// Crée un XmlReader sécurisé depuis une string
        /// </summary>
        public static XmlReader CreateSecureXmlReader(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException("XML content cannot be empty", nameof(xml));

            var settings = GetSecureXmlReaderSettings();
            var stringReader = new System.IO.StringReader(xml);

            return XmlReader.Create(stringReader, settings);
        }

        /// <summary>
        /// Crée un XmlReader sécurisé depuis un stream
        /// </summary>
        public static XmlReader CreateSecureXmlReader(System.IO.Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var settings = GetSecureXmlReaderSettings();
            return XmlReader.Create(stream, settings);
        }

        /// <summary>
        /// Crée un XmlReader sécurisé depuis un fichier
        /// </summary>
        public static XmlReader CreateSecureXmlReaderFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty", nameof(filePath));

            if (!System.IO.File.Exists(filePath))
                throw new System.IO.FileNotFoundException("XML file not found", filePath);

            var settings = GetSecureXmlReaderSettings();
            return XmlReader.Create(filePath, settings);
        }

        /// <summary>
        /// Parse XML de manière sécurisée et retourne XmlDocument
        /// </summary>
        public static XmlDocument ParseSecureXml(string xml)
        {
            var doc = new XmlDocument();

            // Désactiver XmlResolver sur le document aussi
            doc.XmlResolver = null;

            using (var reader = CreateSecureXmlReader(xml))
            {
                doc.Load(reader);
            }

            return doc;
        }

        /// <summary>
        /// Valide qu'une string XML ne contient pas de patterns XXE suspects
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateXmlForXXE(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return (false, "XML cannot be empty");

            // Patterns suspects XXE
            string[] suspiciousPatterns = {
                "<!DOCTYPE",
                "<!ENTITY",
                "SYSTEM",
                "PUBLIC",
                "file://",
                "http://",
                "https://",
                "ftp://"
            };

            string xmlLower = xml.ToLower();

            foreach (var pattern in suspiciousPatterns)
            {
                if (xmlLower.Contains(pattern.ToLower()))
                {
                    return (false, $"XML contains suspicious pattern: {pattern}");
                }
            }

            return (true, string.Empty);
        }
    }
}
