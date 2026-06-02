using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Numerics;

namespace Pos.Function
{
    internal class GridExporter
    {
        public static void ExportGridToCsv(GridControl gridControl, string filePath)
        {
            gridControl.ExportToCsv(filePath);
        }

        public static void ExportGridToPdf(GridControl gridControl, string filePath)
        {
            gridControl.ExportToPdf(filePath);
        }

        public static void ExportGridToXlsx(GridControl gridControl, string filePath)
        {
            gridControl.ExportToXlsx(filePath);
        }

        public static void Export(GridControl gridControl, string format)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string complete;
            string msgSuccess;
            string saveAs;
            string unsupportedExportFormat;

            if (lang == "en")
            {
                confirmation = "Confirm Export";
                areYouSure = "Are you sure you want to export the data to";
                complete = "Export Complete";
                msgSuccess = "Data successfully exported to";
                saveAs = "Save as";
                unsupportedExportFormat = "Unsupported export format";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmer l'exportation";
                areYouSure = "Êtes-vous sûr de vouloir exporter les données vers";
                complete = "Exportation terminée";
                msgSuccess = "Données exportées avec succès vers";
                saveAs = "Enregistrer sous"; 
                unsupportedExportFormat = "Format d'exportation non pris en charge";
            }
            else
            {
                confirmation = "تأكيد التصدير";
                areYouSure = "هل أنت متأكد من رغبتك في تصدير البيانات إلى";
                complete = "اكتمل التصدير";
                msgSuccess = "تم تصدير البيانات إلى بنجاح";
                saveAs = "حفظ باسم"; 
                unsupportedExportFormat = "تنسيق التصدير غير مدعوم";
            }

            if (MessageBox.Show($"{areYouSure} {format.ToUpper()}?", confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    switch (format.ToLower())
                    {
                        case "csv":
                            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                            break;
                        case "pdf":
                            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                            break;
                        case "xlsx":
                            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                            break;
                        default:
                            throw new ArgumentException(unsupportedExportFormat);
                    }

                    saveFileDialog.Title = $"{saveAs} {format.ToUpper()}";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        switch (format.ToLower())
                        {
                            case "csv":
                                ExportGridToCsv(gridControl, saveFileDialog.FileName);
                                break;
                            case "pdf":
                                ExportGridToPdf(gridControl, saveFileDialog.FileName);
                                break;
                            case "xlsx":
                                ExportGridToXlsx(gridControl, saveFileDialog.FileName);
                                break;
                        }
                        
                        Sound.Added();

                        MessageBox.Show($"{msgSuccess} {saveFileDialog.FileName}", complete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
