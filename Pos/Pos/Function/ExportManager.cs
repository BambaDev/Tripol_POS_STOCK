using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Pos.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace Pos.Function
{
    /// <summary>
    /// Gestionnaire centralisé pour exports sécurisés (Excel, CSV, PDF)
    /// Prévient injection formules, contrôle accès, logs audit
    /// </summary>
    public static class ExportManager
    {
        // Limites exports
        public const int MAX_EXPORT_ROWS = 50000;
        public const int WARNING_EXPORT_ROWS = 10000;

        /// <summary>
        /// Export sécurisé vers Excel
        /// </summary>
        public static void SecureExportToExcel(
            GridView gridView,
            string entityType,
            int userId,
            string requiredPermission,
            AppDbContext context = null)
        {
            // VALIDATION 1: Permission
            if (!Permission.HasPermission(requiredPermission))
            {
                return; // AccessDenied déjà affiché
            }

            // VALIDATION 2: Nombre de lignes
            if (gridView.RowCount == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "No data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            if (gridView.RowCount > MAX_EXPORT_ROWS)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Cannot export more than {MAX_EXPORT_ROWS:N0} rows.\n\n" +
                    $"Current rows: {gridView.RowCount:N0}\n\n" +
                    $"Please filter the data first.",
                    "Export Limit Exceeded",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Avertissement si export volumineux
            if (gridView.RowCount > WARNING_EXPORT_ROWS)
            {
                var result = DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Warning: You are about to export {gridView.RowCount:N0} rows.\n\n" +
                    $"This may take several minutes.\n\n" +
                    $"Continue?",
                    "Large Export Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result != DialogResult.Yes)
                    return;
            }

            // VALIDATION 3: SaveFileDialog sécurisé
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                dialog.FileName = $"{entityType}_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                dialog.DefaultExt = "xlsx";
                dialog.Title = "Export to Excel";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;

                    // Validation finale extension
                    if (!filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Only .xlsx files are allowed.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    try
                    {
                        // Options export sécurisées
                        var exportOptions = new XlsxExportOptions
                        {
                            ExportMode = XlsxExportMode.SingleFile,
                            SheetName = SanitizeSheetName(entityType),
                            TextExportMode = TextExportMode.Value, // Prévient injection formules
                            ShowGridLines = true
                        };

                        // Export avec sanitization
                        gridView.ExportToXlsx(filePath, exportOptions);

                        // Log audit
                        if (context != null)
                        {
                            AuditLogger.LogAction(
                                context,
                                "Excel Export",
                                entityType,
                                null,
                                $"Exported {gridView.RowCount:N0} rows to {Path.GetFileName(filePath)}",
                                userId
                            );
                            context.SaveChanges();
                        }

                        // Notification succès
                        var openFile = DevExpress.XtraEditors.XtraMessageBox.Show(
                            $"Export successful!\n\n" +
                            $"File: {Path.GetFileName(filePath)}\n" +
                            $"Rows: {gridView.RowCount:N0}\n" +
                            $"Location: {Path.GetDirectoryName(filePath)}\n\n" +
                            $"Open file now?",
                            "Export Successful",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        if (openFile == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = filePath,
                                UseShellExecute = true
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Export error: {ex.Message}");
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Export failed. Please ensure:\n\n" +
                            "- The file is not already open\n" +
                            "- You have write permissions\n" +
                            "- Sufficient disk space is available",
                            "Export Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        /// <summary>
        /// Export sécurisé vers CSV
        /// </summary>
        public static void SecureExportToCSV(
            GridView gridView,
            string entityType,
            int userId,
            string requiredPermission,
            AppDbContext context = null)
        {
            // VALIDATION 1: Permission
            if (!Permission.HasPermission(requiredPermission))
            {
                return;
            }

            // VALIDATION 2: Données
            if (gridView.RowCount == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("No data to export.", "Export");
                return;
            }

            if (gridView.RowCount > MAX_EXPORT_ROWS)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    $"Cannot export more than {MAX_EXPORT_ROWS:N0} rows.",
                    "Export Limit"
                );
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.FileName = $"{entityType}_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                dialog.DefaultExt = "csv";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;

                    if (!filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Only .csv files are allowed.", "Error");
                        return;
                    }

                    try
                    {
                        // Options CSV sécurisées
                        var exportOptions = new CsvExportOptions
                        {
                            TextExportMode = TextExportMode.Value, // Prévient injection formules
                            Separator = ",",
                            QuoteStringsWithSeparators = true
                        };

                        gridView.ExportToCsv(filePath, exportOptions);

                        // SÉCURITÉ SUPPLÉMENTAIRE: Sanitizer le CSV après création
                        SanitizeCSVFile(filePath);

                        // Log
                        if (context != null)
                        {
                            AuditLogger.LogAction(
                                context,
                                "CSV Export",
                                entityType,
                                null,
                                $"Exported {gridView.RowCount:N0} rows to {Path.GetFileName(filePath)}",
                                userId
                            );
                            context.SaveChanges();
                        }

                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            $"Export successful!\n\nFile: {Path.GetFileName(filePath)}\nRows: {gridView.RowCount:N0}",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"CSV export error: {ex.Message}");
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "Export failed.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        /// <summary>
        /// Export sécurisé vers PDF
        /// </summary>
        public static void SecureExportToPDF(
            GridView gridView,
            string entityType,
            int userId,
            string requiredPermission,
            AppDbContext context = null)
        {
            if (!Permission.HasPermission(requiredPermission))
            {
                return;
            }

            if (gridView.RowCount == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("No data to export.", "Export");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF Files (*.pdf)|*.pdf";
                dialog.FileName = $"{entityType}_Export_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                dialog.DefaultExt = "pdf";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = dialog.FileName;

                    if (!filePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Only .pdf files are allowed.", "Error");
                        return;
                    }

                    try
                    {
                        var exportOptions = new PdfExportOptions
                        {
                            ShowPrintDialogOnOpen = false
                        };

                        gridView.ExportToPdf(filePath, exportOptions);

                        if (context != null)
                        {
                            AuditLogger.LogAction(
                                context,
                                "PDF Export",
                                entityType,
                                null,
                                $"Exported {gridView.RowCount:N0} rows to {Path.GetFileName(filePath)}",
                                userId
                            );
                            context.SaveChanges();
                        }

                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            $"Export successful!\n\nFile: {Path.GetFileName(filePath)}\nRows: {gridView.RowCount:N0}",
                            "Success"
                        );
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"PDF export error: {ex.Message}");
                        DevExpress.XtraEditors.XtraMessageBox.Show("Export failed.", "Error");
                    }
                }
            }
        }

        /// <summary>
        /// Sanitize nom de feuille Excel (enlève caractères invalides)
        /// </summary>
        private static string SanitizeSheetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Export";

            // Excel interdit ces caractères dans noms de feuilles
            char[] invalidChars = { ':', '\\', '/', '?', '*', '[', ']' };
            string sanitized = name;

            foreach (char c in invalidChars)
            {
                sanitized = sanitized.Replace(c, '_');
            }

            // Limite longueur (Excel max 31 caractères)
            if (sanitized.Length > 31)
            {
                sanitized = sanitized.Substring(0, 31);
            }

            return sanitized;
        }

        /// <summary>
        /// Sanitize fichier CSV pour prévenir CSV Injection
        /// </summary>
        private static void SanitizeCSVFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                bool modified = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string original = lines[i];
                    string sanitized = SanitizeCSVLine(original);

                    if (sanitized != original)
                    {
                        lines[i] = sanitized;
                        modified = true;
                    }
                }

                if (modified)
                {
                    File.WriteAllLines(filePath, lines);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CSV sanitization error: {ex.Message}");
                // Ne pas bloquer l'export si sanitization échoue
            }
        }

        /// <summary>
        /// Sanitize une ligne CSV (prévient injection formules)
        /// </summary>
        private static string SanitizeCSVLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                return line;

            // Caractères dangereux en début de cellule CSV
            char[] dangerousStarts = { '=', '+', '-', '@', '\t', '\r' };

            // Split par virgule (simpliste, assume pas de virgules dans guillemets)
            string[] cells = line.Split(',');
            bool modified = false;

            for (int i = 0; i < cells.Length; i++)
            {
                string cell = cells[i].Trim().Trim('"');

                if (!string.IsNullOrEmpty(cell))
                {
                    foreach (char dangerous in dangerousStarts)
                    {
                        if (cell[0] == dangerous)
                        {
                            // Préfixer avec apostrophe pour forcer texte
                            cells[i] = "\"'" + cell + "\"";
                            modified = true;
                            break;
                        }
                    }
                }
            }

            return modified ? string.Join(",", cells) : line;
        }

        /// <summary>
        /// Sanitize valeur pour Excel/CSV (prévient injection formules)
        /// Utiliser avant d'insérer dans cellule
        /// </summary>
        public static string SanitizeForExcelCSV(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // Si commence par caractère dangereux
            char[] dangerousStarts = { '=', '+', '-', '@', '\t', '\r' };

            if (Array.IndexOf(dangerousStarts, value[0]) >= 0)
            {
                // Préfixer avec apostrophe pour forcer texte
                return "'" + value;
            }

            return value;
        }
    }
}
