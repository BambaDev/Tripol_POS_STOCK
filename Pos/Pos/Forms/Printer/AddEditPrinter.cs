using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
//using Pos.Forms.BusinessLocation;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Printer
{
    public partial class AddEditPrinter : DevExpress.XtraEditors.XtraForm
    {
        private readonly UniqueChecker _uniqueChecker = new UniqueChecker();
        public Printers printers = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditPrinter()
        {
            InitializeComponent();

            this.toRtl();
            this.BorderStyle();
        }

        public void BorderStyle()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang != "ar")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
            }
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                // Set the form to use RTL
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (Control control in this.Controls)
                {
                    control.RightToLeft = RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            //bbiAllCustomersss.ItemAppearance.Normal.Font = customFont;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public void setPrintersObject(Printers printers)
        {
            this.printers = printers;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem = lang switch
            {
                "en" => "Please select item!",
                "fr" => "Veuillez sélectionner l'article !",
                _ => "الرجاء تحديد العنصر!"
            };

            if (this.printers != null)
            {
                using (var context = new AppDbContext())
                {
                    if (this.printers.printer_id != 0)
                        this.currentItemId = this.printers.printer_id;

                    this.printers.printer_id = 0;

                    Models.Printer printer = context.Printers.Find(this.currentItemId);

                    if (printer != null)
                    {
                        txtTitle.Text = printer.Title;
                        txtPrinterIpAddress.Text = printer.PrinterIpAddress;
                        txtPrinterPort.EditValue = printer.PrinterPort;
                        txtCharactersPerLine.EditValue = printer.CharactersPerLine;
                        txtType.EditValue = printer.Type;
                        txtBusinessLocation.EditValue = printer.BusinessLocationId;
                        btnSelect.Enabled = true;
                    }
                    else
                    {
                        btnSelect.Enabled = false;
                        Function.Sound.Wrong();
                        XtraMessageBox.Show(pleaseSelectItem);
                    }
                }
            }
            else
            {
                btnSelect.Enabled = false;
                Function.Sound.Wrong();
                XtraMessageBox.Show(pleaseSelectItem);
            }
        }

        public void getBusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBusinessLocation.Properties.DataSource = context.BusinessLocations.ToList();
                txtBusinessLocation.Properties.DisplayMember = "Name"; // Set display member
                txtBusinessLocation.Properties.ValueMember = "Id"; // Set value member
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;
            string nameAlreadyExists;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select item!";
                nameAlreadyExists = "A Printer with this Title already exists.";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner l'élément!";
                nameAlreadyExists = "Une imprimante portant ce titre existe déjà.";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد العنصر!";
                nameAlreadyExists = "توجد طابعة بهذا العنوان بالفعل.";
            }

            if (dxValidationProvider.Validate())
            {
                using (var context = new AppDbContext())
                {
                    Models.Printer printer;
                    bool isTitleExists = false;

                    if (this.type == "Add")
                    {
                        // Check if a printer with the same title already exists
                        isTitleExists = context.Printers.Any(p => p.Title == txtTitle.Text);

                        if (isTitleExists)
                        {
                            AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                            alertMessage.ShowDialog();
                            return;
                        }

                        // Add new printer
                        printer = new Models.Printer
                        {
                            Title = txtTitle.Text,
                            PrinterIpAddress = txtPrinterIpAddress.Text,
                            PrinterPort = int.Parse(txtPrinterPort.EditValue.ToString()),
                            CharactersPerLine = txtCharactersPerLine.EditValue.ToString(),
                            Type = txtType.Text,
                            BusinessLocationId = int.Parse(txtBusinessLocation.EditValue.ToString()),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        context.Printers.Add(printer);
                        ClearFormFields();
                    }
                    else
                    {
                        if (this.printers != null)
                            this.currentItemId = this.printers.printer_id;

                        printer = context.Printers.Find(this.currentItemId);

                        if (printer != null)
                        {
                            // Check if the new title is unique excluding the current record
                            isTitleExists = context.Printers.Any(p => p.Title == txtTitle.Text && p.Id != printer.Id);

                            if (isTitleExists)
                            {
                                AlertMessageBox alertMessage = new AlertMessageBox(nameAlreadyExists);
                                alertMessage.ShowDialog();
                                return;
                            }

                            printer.Title = txtTitle.Text;
                            printer.PrinterIpAddress = txtPrinterIpAddress.Text;
                            printer.PrinterPort = int.Parse(txtPrinterPort.EditValue.ToString());
                            printer.CharactersPerLine = txtCharactersPerLine.EditValue.ToString();
                            printer.Type = txtType.Text;
                            printer.BusinessLocationId = int.Parse(txtBusinessLocation.EditValue.ToString());
                            printer.UpdatedAt = DateTime.Now;

                            context.Entry(printer).State = EntityState.Modified;
                        }
                        else
                        {
                            Function.Sound.Wrong();
                            XtraMessageBox.Show(pleaseSelectItem);
                            return;
                        }
                    }

                    // Save changes to the database
                    context.SaveChanges();

                    Function.Sound.Added();

                    // Reload printers
                    if (this.printers != null)
                        this.printers.loadPrinters();
                }
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        private void ClearFormFields()
        {
            txtTitle.Text = "";
            txtPrinterIpAddress.Text = "";
            txtPrinterPort.EditValue = null;
            txtCharactersPerLine.EditValue = null;
            txtType.Text = "";
            txtBusinessLocation.EditValue = null;
        }

        private void AddEditPrinter_Load(object sender, EventArgs e)
        {
            txtType.EditValue = "Network";

            //txtBusinessLocation.EditValue = Properties.Settings.Default.BusinessLocation;

            this.getBusinessLocations();

            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Printer GetCurrentData()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                    return context.Printers.Find(currentItemId);
                else
                    return null;
            }
        }

        private void DisplayCurrentItem()
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                if (currentItemId == 0)
                {
                    // If no current selection, disable all navigation buttons.
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                    btnEnd.Enabled = false;
                    return;
                }

                // Retrieve the minimum and maximum ID values from the Brands dataset.
                int minId = context.Printers.Min(b => b.Id);
                int maxId = context.Printers.Max(b => b.Id);

                // Enable or disable navigation buttons based on the current record's ID.
                btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
                btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
                btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
                btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

                this.type = "Edit";

                Models.Printer currentItem = GetCurrentData();

                if (currentItem != null)
                {
                    this.currentItemId = currentItem.Id;
                    txtTitle.Text = currentItem.Title;
                    txtPrinterIpAddress.Text = currentItem.PrinterIpAddress;
                    txtPrinterPort.EditValue = currentItem.PrinterPort;
                    txtCharactersPerLine.EditValue = currentItem.CharactersPerLine;
                    txtType.EditValue = currentItem.Type;
                    txtBusinessLocation.EditValue = currentItem.BusinessLocationId;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
                }
            }
        }


        private void MoveToFirst()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the first item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du premier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأول:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? minId = context.Printers.Min(b => (int?)b.Id);
                    if (minId.HasValue)
                    {
                        currentItemId = minId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToLast()
        {
            string lang = Properties.Settings.Default.Lang;

            string noEntries;
            string failedToRetrieve;

            if (lang == "en")
            {
                noEntries = "No entries found in DB.";
                failedToRetrieve = "Failed to retrieve the last item:";
            }
            else if (lang == "fr")
            {
                noEntries = "Aucune entrée trouvée dans la base de données.";
                failedToRetrieve = "Échec de la récupération du dernier élément :";
            }
            else
            {
                noEntries = "لم يتم العثور على إدخالات في قاعدة البيانات.";
                failedToRetrieve = "فشل استرداد العنصر الأخير:";
            }

            try
            {
                using (var context = new AppDbContext())
                {
                    int? maxId = context.Printers.Max(b => (int?)b.Id);
                    if (maxId.HasValue)
                    {
                        currentItemId = maxId.Value;
                        DisplayCurrentItem();
                    }
                    else
                    {
                        MessageBox.Show(noEntries);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(failedToRetrieve + ex.Message);
            }
        }

        private void MoveToNext()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId == 0)
                {
                    this.MoveToFirst();
                }

                var nextItem = context.Printers.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
                if (nextItem != null)
                {
                    currentItemId = nextItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void MoveToPrevious()
        {
            using (var context = new AppDbContext())
            {
                if (currentItemId != 0)
                {
                    var prevItem = context.Printers.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                    if (prevItem != null)
                    {
                        currentItemId = prevItem.Id;
                        DisplayCurrentItem();
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";

            txtTitle.Text = string.Empty;
            txtPrinterPort.EditValue = 0;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeSearchLookUpEdit()
        {
            string lang = Properties.Settings.Default.Lang;

            string name;
            string writeSomething;

            if (lang == "en")
            {
                name = "Name";
                writeSomething = "Write something...";
            }
            else if (lang == "fr")
            {
                name = "nom";
                writeSomething = "Écris quelque chose...";
            }
            else
            {
                name = "اسم";
                writeSomething = "أكتب شيئا...";
            }

            using (var context = new AppDbContext())
            {
                searchLookUpEdit.Properties.DataSource = context.Printers.ToList();
                searchLookUpEdit.Properties.DisplayMember = "Title";
                searchLookUpEdit.Properties.ValueMember = "Id";

                searchLookUpEdit.Properties.View.Columns.Clear();
                searchLookUpEdit.Properties.View.Columns.AddVisible("Title", name);

                searchLookUpEdit.Properties.NullText = writeSomething;

                searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            }
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string noItemSelected;

            if (lang == "en")
            {
                noItemSelected = "No item selected.";
            }
            else if (lang == "fr")
            {
                noItemSelected = "Aucun élément sélectionné.";
            }
            else
            {
                noItemSelected = "لم يتم تحديد أي عنصر.";
            }

            if (searchLookUpEdit.EditValue != null)
            {
                this.currentItemId = int.Parse(searchLookUpEdit.EditValue.ToString());
                this.edit();
            }
            else
            {
                Console.WriteLine(noItemSelected);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}