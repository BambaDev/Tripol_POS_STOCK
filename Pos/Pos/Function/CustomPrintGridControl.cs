using DevExpress.XtraPrinting;
using Pos.Forms.Alert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Pos.Function;
using Pos.Models;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid;
using System.Drawing.Printing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.CodeParser;

namespace Pos.Function
{
    internal class CustomPrintGridControl
    {
        private GridControl gridControlCases;

        public CustomPrintGridControl(GridControl gridControl)
        {
            this.gridControlCases = gridControl;
        }

        public void PrintGridControl(GridView gridViewItems)
        {
            string lang = Properties.Settings.Default.Lang;

            string printConfirmation;
            string areYouDure;
            string thereAreNoCases;
            string msgErreurPrinter, msgErreurPrinterInvalide, msgErreurPrinterInvalideEnd,msgErreurTitle;

            if (lang == "en")
            {
                printConfirmation = "Print Confirmation";
                areYouDure = "Are you sure you want to print the list?";
                thereAreNoCases = "There are no cases to print";
                msgErreurPrinter = "Select a printer";
                msgErreurPrinterInvalide = "The specified printer";
                msgErreurPrinterInvalideEnd = "is not valid. Please check your printer settings.";
                msgErreurTitle = "Printer Error";
            }
            else if (lang == "fr")
            {
                printConfirmation = "Confirmation d'impression";
                areYouDure = "Etes-vous sûr de vouloir imprimer la liste ?";
                thereAreNoCases = "Il n'y a aucun cas à imprimer";
                msgErreurPrinter = "Selectionner une  Imprimante";
                msgErreurPrinterInvalide = "L'imprimante spécifiée";
                msgErreurPrinterInvalideEnd = "n'est pas valide. Veuillez vérifier les paramètres de votre imprimante.";
                msgErreurTitle = "Erreur d'imprimante";
            }
            else
            {
                printConfirmation = "تأكيد الطباعة";
                areYouDure = "هل أنت متأكد من رغبتك في طباعة القائمة؟"; 
                thereAreNoCases = "لا توجد حالات للطباعة";
                msgErreurPrinter = "حدد طابعة";
                msgErreurPrinterInvalide = "الطابعة المحددة";
                msgErreurPrinterInvalideEnd = "غير صالح. يرجى التحقق من إعدادات الطابعة الخاصة بك";
                msgErreurTitle = "خطأ في الطابعة";
            }

            if (gridViewItems.RowCount >= 0)
            {
                var result = MessageBox.Show(areYouDure, printConfirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Sound.Selected();

                    // Create the printing system and link
                    PrintingSystem printingSystem = new PrintingSystem();
                    PrintableComponentLink printableComponentLink = new PrintableComponentLink(printingSystem)
                    {
                        Component = gridControlCases
                    };

                    // Set the paper kind and other print settings
                    printableComponentLink.Landscape = true;

                    // Handle the events to customize the header and footer
                    printableComponentLink.CreateReportHeaderArea += PrintableComponentLink_CreateReportHeaderArea;
                    printableComponentLink.CreateReportFooterArea += PrintableComponentLink_CreateReportFooterArea;

                    // Create the document
                    printableComponentLink.CreateDocument();

                    printableComponentLink.ShowPreview();

                    // Configure and print the document
                    PrintToolBase printTool = new PrintToolBase(printableComponentLink.PrintingSystem);
                    if (Properties.Settings.Default.PrinterDocument==null)
                    {
                        XtraMessageBox.Show(msgErreurPrinter);
                    }
                    else
                    {
                        printTool.PrinterSettings.PrinterName = Properties.Settings.Default.PrinterDocument;
                    }

                    try
                    {
                        printTool.Print();
                    }
                    catch (System.Drawing.Printing.InvalidPrinterException ex)
                    {
                        // Handle the exception, e.g., show a message to the user
                        XtraMessageBox.Show($"{msgErreurPrinterInvalide} '{Properties.Settings.Default.PrinterDocument}' {msgErreurPrinterInvalideEnd} {ex}", $"{msgErreurTitle}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Optionally log the exception or take other actions
                    }
                    catch (Exception ex) {
                        XtraMessageBox.Show($"{msgErreurPrinterInvalide} '{Properties.Settings.Default.PrinterDocument}' {msgErreurPrinterInvalideEnd} {ex}", $"{msgErreurTitle}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Print the document automatically
                  
                }
                else
                {
                    Sound.Wrong();
                }
            }
            else
            {
                AlertMessageBox alertMessage = new AlertMessageBox(thereAreNoCases);
                alertMessage.ShowDialog();
            }
        }

        // Event handler for creating the header
        private void PrintableComponentLink_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Models.Setting setting = Function.Helper.getSetting();

            // Convert byte array to Image
            byte[] logoBytes = setting.Logo; // Assuming this returns a byte array
            Image logo = null;
            using (MemoryStream ms = new MemoryStream(logoBytes))
            {
                logo = Image.FromStream(ms);
            }

            // Add logo
            if (logo != null)
            {
                RectangleF logoRect = new RectangleF(0, 0, 100, 100);
                e.Graph.DrawImage(logo, logoRect, BorderSide.None, Color.Transparent);
            }

            // Add title
            string title = setting.Company;
            e.Graph.Font = new Font("Arial", 16, FontStyle.Bold);
            RectangleF titleRect = new RectangleF(110, 0, e.Graph.ClientPageSize.Width - 110, 30);
            e.Graph.DrawString(title, Color.Black, titleRect, BorderSide.None);

            // Add description
            string description = setting.Description;
            e.Graph.Font = new Font("Arial", 12);
            RectangleF descRect = new RectangleF(110, 35, e.Graph.ClientPageSize.Width - 110, 60);
            e.Graph.DrawString(description, Color.Black, descRect, BorderSide.None);
        }

        // Event handler for creating the footer
        private void PrintableComponentLink_CreateReportFooterArea(object sender, CreateAreaEventArgs e)
        {
            Models.Setting setting = Function.Helper.getSetting();

            // Add additional information in the footer
            string footerTextLeft = setting.SubTitle;
            e.Graph.Font = new Font("Arial", 10);
            RectangleF footerRectLeft = new RectangleF(0, 0, e.Graph.ClientPageSize.Width / 2, 30);
            e.Graph.DrawString(footerTextLeft, Color.Black, footerRectLeft, BorderSide.None);

            // Add page number
            string footerTextRight = setting.Website;
            RectangleF footerRectRight = new RectangleF(e.Graph.ClientPageSize.Width / 2, 0, e.Graph.ClientPageSize.Width / 2, 30);
            e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, footerTextRight, Color.Black, footerRectRight, BorderSide.None);
        }
    }
}
