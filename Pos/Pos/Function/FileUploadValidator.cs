using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace Pos.Function
{
    /// <summary>
    /// Valide les uploads de fichiers (images, documents)
    /// Prévient upload malware, polyglot files, image bombs
    /// </summary>
    public static class FileUploadValidator
    {
        // Tailles maximales
        public const long MAX_IMAGE_SIZE_BYTES = 10 * 1024 * 1024; // 10 MB
        public const long MAX_DOCUMENT_SIZE_BYTES = 50 * 1024 * 1024; // 50 MB

        // Extensions autorisées
        private static readonly string[] ALLOWED_IMAGE_EXTENSIONS = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        private static readonly string[] ALLOWED_DOCUMENT_EXTENSIONS = { ".pdf", ".xlsx", ".csv", ".txt" };

        // MIME types valides pour images
        private static readonly string[] VALID_IMAGE_MIME_TYPES = {
            "image/jpeg",
            "image/jpg",
            "image/png",
            "image/bmp",
            "image/gif"
        };

        /// <summary>
        /// Valide une image uploadée
        /// </summary>
        public static (bool isValid, string errorMessage, string sanitizedFileName, long fileSizeKB) ValidateImage(string filePath)
        {
            // VALIDATION 1: Fichier existe
            if (!File.Exists(filePath))
            {
                return (false, "File does not exist", null, 0);
            }

            FileInfo fileInfo = new FileInfo(filePath);

            // VALIDATION 2: Taille fichier
            if (fileInfo.Length == 0)
            {
                return (false, "File is empty", null, 0);
            }

            if (fileInfo.Length > MAX_IMAGE_SIZE_BYTES)
            {
                long maxSizeMB = MAX_IMAGE_SIZE_BYTES / (1024 * 1024);
                long actualSizeMB = fileInfo.Length / (1024 * 1024);
                return (false, $"File too large ({actualSizeMB} MB). Maximum: {maxSizeMB} MB", null, 0);
            }

            // VALIDATION 3: Extension fichier (double extension attack)
            string extension = fileInfo.Extension.ToLowerInvariant();

            if (!ALLOWED_IMAGE_EXTENSIONS.Contains(extension))
            {
                return (false, $"Invalid file extension: {extension}. Allowed: {string.Join(", ", ALLOWED_IMAGE_EXTENSIONS)}", null, 0);
            }

            // VALIDATION 4: Vérifier que c'est vraiment une image (pas juste l'extension)
            try
            {
                using (var image = Image.FromFile(filePath))
                {
                    // Vérifier format image valide
                    if (!IsValidImageFormat(image.RawFormat))
                    {
                        return (false, "Invalid image format", null, 0);
                    }

                    // VALIDATION 5: Dimensions raisonnables (détection image bomb)
                    const int MAX_WIDTH = 10000;
                    const int MAX_HEIGHT = 10000;

                    if (image.Width > MAX_WIDTH || image.Height > MAX_HEIGHT)
                    {
                        return (false, $"Image dimensions too large ({image.Width}x{image.Height}). Maximum: {MAX_WIDTH}x{MAX_HEIGHT}", null, 0);
                    }

                    // VALIDATION 6: Ratio raisonnable (détection anomalie)
                    double ratio = (double)image.Width / image.Height;
                    if (ratio > 100 || ratio < 0.01)
                    {
                        return (false, "Unusual image aspect ratio detected", null, 0);
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                return (false, "Image format not supported or file corrupted", null, 0);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Image validation error: {ex.Message}");
                return (false, "Not a valid image file", null, 0);
            }

            // VALIDATION 7: Nom fichier sécurisé
            string sanitizedFileName = SanitizeFileName(fileInfo.Name);

            long fileSizeKB = fileInfo.Length / 1024;

            return (true, string.Empty, sanitizedFileName, fileSizeKB);
        }

        /// <summary>
        /// Vérifie que le format d'image est valide
        /// </summary>
        private static bool IsValidImageFormat(ImageFormat format)
        {
            return format.Equals(ImageFormat.Jpeg) ||
                   format.Equals(ImageFormat.Png) ||
                   format.Equals(ImageFormat.Bmp) ||
                   format.Equals(ImageFormat.Gif);
        }

        /// <summary>
        /// Sanitize nom de fichier (enlève caractères dangereux)
        /// </summary>
        public static string SanitizeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "unnamed_file";

            // Enlever caractères invalides
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            // Enlever caractères dangereux supplémentaires
            sanitized = sanitized.Replace("..", "").Replace("~", "").Replace("$", "");

            // Limiter longueur
            const int MAX_FILENAME_LENGTH = 200;
            if (sanitized.Length > MAX_FILENAME_LENGTH)
            {
                string extension = Path.GetExtension(sanitized);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(sanitized);
                sanitized = nameWithoutExt.Substring(0, MAX_FILENAME_LENGTH - extension.Length) + extension;
            }

            return sanitized;
        }

        /// <summary>
        /// Valide un document uploadé (PDF, Excel, etc.)
        /// </summary>
        public static (bool isValid, string errorMessage) ValidateDocument(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return (false, "File does not exist");
            }

            FileInfo fileInfo = new FileInfo(filePath);

            // Taille
            if (fileInfo.Length > MAX_DOCUMENT_SIZE_BYTES)
            {
                long maxSizeMB = MAX_DOCUMENT_SIZE_BYTES / (1024 * 1024);
                return (false, $"File too large. Maximum: {maxSizeMB} MB");
            }

            // Extension
            string extension = fileInfo.Extension.ToLowerInvariant();
            if (!ALLOWED_DOCUMENT_EXTENSIONS.Contains(extension))
            {
                return (false, $"Invalid file type: {extension}. Allowed: {string.Join(", ", ALLOWED_DOCUMENT_EXTENSIONS)}");
            }

            // Vérifier magic bytes (signature fichier)
            if (!ValidateFileSignature(filePath, extension))
            {
                return (false, "File content does not match extension (possible file type spoofing)");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// Vérifie la signature du fichier (magic bytes)
        /// Détecte si extension ne correspond pas au contenu réel
        /// </summary>
        private static bool ValidateFileSignature(string filePath, string extension)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length < 4)
                        return false;

                    byte[] headerBytes = new byte[8];
                    fs.Read(headerBytes, 0, 8);

                    // Vérifier magic bytes selon extension
                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            // JPEG: FF D8 FF
                            return headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF;

                        case ".png":
                            // PNG: 89 50 4E 47
                            return headerBytes[0] == 0x89 && headerBytes[1] == 0x50 &&
                                   headerBytes[2] == 0x4E && headerBytes[3] == 0x47;

                        case ".pdf":
                            // PDF: 25 50 44 46 (%PDF)
                            return headerBytes[0] == 0x25 && headerBytes[1] == 0x50 &&
                                   headerBytes[2] == 0x44 && headerBytes[3] == 0x46;

                        case ".xlsx":
                            // XLSX: PK (ZIP signature: 50 4B)
                            return headerBytes[0] == 0x50 && headerBytes[1] == 0x4B;

                        case ".bmp":
                            // BMP: 42 4D (BM)
                            return headerBytes[0] == 0x42 && headerBytes[1] == 0x4D;

                        default:
                            return true; // Pas de vérification pour autres types
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Génère un nom de fichier sécurisé et unique
        /// </summary>
        public static string GenerateSecureFileName(string originalFileName)
        {
            string extension = Path.GetExtension(originalFileName);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 8);

            return $"{timestamp}_{randomPart}{extension}";
        }

        /// <summary>
        /// Redimensionne une image si trop grande (économie espace)
        /// </summary>
        public static byte[] OptimizeImage(string filePath, int maxWidth = 1200, int maxHeight = 1200)
        {
            using (var originalImage = Image.FromFile(filePath))
            {
                // Si déjà petite, retourner telle quelle
                if (originalImage.Width <= maxWidth && originalImage.Height <= maxHeight)
                {
                    return File.ReadAllBytes(filePath);
                }

                // Calculer nouvelles dimensions (garder ratio)
                int newWidth, newHeight;
                double ratioX = (double)maxWidth / originalImage.Width;
                double ratioY = (double)maxHeight / originalImage.Height;
                double ratio = Math.Min(ratioX, ratioY);

                newWidth = (int)(originalImage.Width * ratio);
                newHeight = (int)(originalImage.Height * ratio);

                // Redimensionner
                using (var resizedImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }

                    // Convertir en byte[]
                    using (MemoryStream ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }
            }
        }
    }
}
