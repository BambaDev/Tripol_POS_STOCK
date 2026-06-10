using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Pos.Function
{
    /// <summary>
    /// PHASE 4B RGPD: EF Core ValueConverter pour encryption automatique des strings
    /// Encrypte lors de l'écriture en DB, décrypte lors de la lecture
    /// </summary>
    public class EncryptedStringConverter : ValueConverter<string, string>
    {
        public EncryptedStringConverter()
            : base(
                // Conversion vers DB: encrypting
                plaintext => FieldEncryption.Encrypt(plaintext),
                // Conversion depuis DB: decrypting
                ciphertext => FieldEncryption.Decrypt(ciphertext),
                // Mapping hints (optionnel)
                new ConverterMappingHints())
        {
        }
    }

    /// <summary>
    /// PHASE 4B RGPD: EF Core ValueConverter pour encryption automatique des byte arrays
    /// Utilisé pour images, documents, données binaires
    /// </summary>
    public class EncryptedBytesConverter : ValueConverter<byte[], byte[]>
    {
        public EncryptedBytesConverter()
            : base(
                // Conversion vers DB: encrypting
                plainBytes => FieldEncryption.EncryptBytes(plainBytes),
                // Conversion depuis DB: decrypting
                encryptedBytes => FieldEncryption.DecryptBytes(encryptedBytes),
                // Mapping hints
                new ConverterMappingHints())
        {
        }
    }

    /// <summary>
    /// PHASE 4B RGPD: ValueConverter pour DateTime (optionnel - si besoin d'encryption dates sensibles)
    /// Note: Généralement les dates ne sont pas encryptées car perdent la capacité de tri/filtrage
    /// </summary>
    public class EncryptedDateTimeConverter : ValueConverter<DateTime?, string>
    {
        public EncryptedDateTimeConverter()
            : base(
                // Conversion vers DB: serialize puis encrypt
                dateTime => dateTime.HasValue
                    ? FieldEncryption.Encrypt(dateTime.Value.ToString("O")) // ISO 8601 format
                    : null,
                // Conversion depuis DB: decrypt puis parse
                ciphertext => !string.IsNullOrEmpty(ciphertext)
                    ? DateTime.Parse(FieldEncryption.Decrypt(ciphertext))
                    : (DateTime?)null,
                new ConverterMappingHints())
        {
        }
    }

    /// <summary>
    /// PHASE 4B RGPD: ValueConverter pour decimal (montants financiers sensibles)
    /// Note: Comme DateTime, l'encryption empêche les calculs SQL - à utiliser avec précaution
    /// </summary>
    public class EncryptedDecimalConverter : ValueConverter<decimal?, string>
    {
        public EncryptedDecimalConverter()
            : base(
                // Conversion vers DB: serialize puis encrypt
                amount => amount.HasValue
                    ? FieldEncryption.Encrypt(amount.Value.ToString("F2")) // 2 decimal places
                    : null,
                // Conversion depuis DB: decrypt puis parse
                ciphertext => !string.IsNullOrEmpty(ciphertext)
                    ? decimal.Parse(FieldEncryption.Decrypt(ciphertext))
                    : (decimal?)null,
                new ConverterMappingHints())
        {
        }
    }

    /// <summary>
    /// PHASE 4B RGPD: ValueConverter pour int (IDs sensibles comme numéros de carte)
    /// </summary>
    public class EncryptedIntConverter : ValueConverter<int?, string>
    {
        public EncryptedIntConverter()
            : base(
                // Conversion vers DB: serialize puis encrypt
                number => number.HasValue
                    ? FieldEncryption.Encrypt(number.Value.ToString())
                    : null,
                // Conversion depuis DB: decrypt puis parse
                ciphertext => !string.IsNullOrEmpty(ciphertext)
                    ? int.Parse(FieldEncryption.Decrypt(ciphertext))
                    : (int?)null,
                new ConverterMappingHints())
        {
        }
    }
}
