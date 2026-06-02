using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Native;
using Pos.Forms.Alert;
using Pos.Forms.Overlay;
using Pos.Function;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Filtering.ExcelFilterOptions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using Microsoft.EntityFrameworkCore;

namespace Pos.Forms.Printer
{
    public partial class Printers : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemEdit;
        private ToolStripMenuItem menuItemDelete;
        public int printer_id = 0;
        private OverlayForm overlay;

        private void ShowOverlay()
        {
            if (overlay == null)
            {
                overlay = new OverlayForm(this);
                overlay.Show();
            }
        }

        private void HideOverlay()
        {
            if (overlay != null)
            {
                overlay.Close();
                overlay.Dispose();
                overlay = null;
            }
        }

        public Printers()
        {
            InitializeComponent();

            InitializeContextMenu();

            gridViewPrinters.MouseUp += gridView_MouseUp;

            this.toRtl();
            this.BorderStyle();

            // style the grid view
            gridViewPrinters.RowStyle += gridViewPrinters_RowStyle;
            gridViewPrinters.FocusedRowChanged += gridViewPrinters_FocusedRowChanged;
            gridViewPrinters.CustomDrawCell += gridViewPrinters_CustomDrawCell;
            gridViewPrinters.RowHeight = Function.Helper.RowHeight;
            gridViewPrinters.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewPrinters_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewPrinters.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewPrinters_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewPrinters.FocusedRowHandle && e.Column == gridViewPrinters.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewPrinters_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        private void InitializeContextMenu()
        {
            string lang = Properties.Settings.Default.Lang;

            string msgMenuItemEdit;
            string msgMenuItemAdd;
            string msgMenuItemDelete;

            if (lang == "en")
            {
                msgMenuItemEdit = "Edit";
                msgMenuItemAdd = "Add";
                msgMenuItemDelete = "Delete";
            }
            else if (lang == "fr")
            {
                msgMenuItemEdit = "Modifier";
                msgMenuItemAdd = "Ajouter";
                msgMenuItemDelete = "Supprimer";
            }
            else
            {
                msgMenuItemEdit = "تحرير";
                msgMenuItemAdd = "أضف";
                msgMenuItemDelete = "حذف";
            }

            contextMenu = new ContextMenuStrip();
            menuItemEdit = new ToolStripMenuItem(msgMenuItemEdit);
            menuItemAdd = new ToolStripMenuItem(msgMenuItemAdd);
            menuItemDelete = new ToolStripMenuItem(msgMenuItemDelete);

            menuItemEdit.Image = Properties.Resources.rightclick_suitcase;
            menuItemAdd.Image = Properties.Resources.rightclick_case_study;
            menuItemDelete.Image = Properties.Resources.rightclick_remove;

            contextMenu.Items.AddRange(new ToolStripItem[] { menuItemEdit, menuItemAdd, menuItemDelete });

            menuItemEdit.Click += MenuItemEdit_Click;
            menuItemAdd.Click += MenuItemAdd_Click;
            menuItemDelete.Click += MenuItemDelete_Click;
        }

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (gridViewPrinters.RowCount > 0 && gridViewPrinters.FocusedRowHandle >= 0)
                {
                    GridView view = sender as GridView;
                    GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

                    if (hitInfo.InRow)
                    {
                        view.FocusedRowHandle = hitInfo.RowHandle;
                        contextMenu.Show(view.GridControl, e.Location);
                    }
                }
                    
            }
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            printerEdit();
        }

        private void MenuItemAdd_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Printer"))
            {
                AddEditPrinter printer = new AddEditPrinter();
                printer.setPrintersObject(this);
                printer.setTypeOperation("Add");
                printer.ShowDialog();
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            printerDelete();
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

        public void ShowRibbon(bool show = false)
        {
            ribbonPrinters.Visible = show;
        }

        private void Printers_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            this.loadPrinters();
        }

        public void loadPrinters()
        {
            using (var context = new AppDbContext())
            {
                gridControlPrinters.DataSource = context.Printers.OrderByDescending(p => p.Id).Include(b=>b.BusinessLocation)
                    .ToList();
            }
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddEditPrinter printer = new AddEditPrinter();
            printer.setPrintersObject(this);
            printer.setTypeOperation("Add");
            printer.ShowDialog();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.printerEdit();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.printerDelete();
        }

        private void gridViewRoles_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewPrinters.RowCount > 0 && gridViewPrinters.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.printer_id = int.Parse(gridViewPrinters.GetRowCellValue(gridViewPrinters.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        private void gridViewPrinters_DoubleClick(object sender, EventArgs e)
        {
            if (gridViewPrinters.RowCount > 0 && gridViewPrinters.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.printer_id = int.Parse(gridViewPrinters.GetRowCellValue(gridViewPrinters.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.printerEdit();
            }
        }

        private void repEditPrinter_Click(object sender, EventArgs e)
        {
            if (gridViewPrinters.RowCount > 0 && gridViewPrinters.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.printer_id = int.Parse(gridViewPrinters.GetRowCellValue(gridViewPrinters.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
                this.printerEdit();
            }
        }

        private void repDeletePrinter_Click(object sender, EventArgs e)
        {
            this.printerDelete();
        }

        private void gridViewPrinters_RowClick(object sender, EventArgs e)
        {
            if (gridViewPrinters.RowCount > 0 && gridViewPrinters.FocusedRowHandle >= 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;

                this.printer_id = int.Parse(gridViewPrinters.GetRowCellValue(gridViewPrinters.FocusedRowHandle, "Id").ToString());
                Function.Sound.Selected();
            }
        }

        public void printerEdit()
        {
            if (Function.Permission.HasPermission("Edit Printer"))
            {
                string lang = Properties.Settings.Default.Lang;

                string pleaseSelectItem;

                if (lang == "en")
                {
                    pleaseSelectItem = "Please select item !";
                }
                else if (lang == "fr")
                {
                    pleaseSelectItem = "Veuillez sélectionner l'article !";
                }
                else
                {
                    pleaseSelectItem = "الرجاء تحديد العنصر!";
                }

                if (this.printer_id != 0)
                {
                    AddEditPrinter printer = new AddEditPrinter();
                    printer.setPrintersObject(this);
                    printer.setTypeOperation("Edit");
                    printer.ShowDialog();
                } else
                {
                    Sound.Selected();
                    AlertMessageBox alertMessage = new AlertMessageBox(pleaseSelectItem);
                    alertMessage.ShowDialog();
                }
            }
        }

        public void printerDelete()
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;

            if (lang == "en")
            {
                confirmation = "Confirmation";
                areYouSure = "Are you sure want to delete Item ?";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmation";
                areYouSure = "Etes-vous sûr de vouloir supprimer l'élément ?";
            }
            else
            {
                confirmation = "التأكيد";
                areYouSure = "هل أنت متأكد من رغبتك في حذف العنصر؟";
            }

            if (Function.Permission.HasPermission("Delete Printer"))
            {
                using (var context = new AppDbContext())
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    this.printer_id = int.Parse(gridViewPrinters.GetRowCellValue(gridViewPrinters.FocusedRowHandle, "Id").ToString());

                    Models.Printer printer = context.Printers.Find(this.printer_id);

                    if (printer != null & XtraMessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        context.Printers.Remove(printer);
                        context.SaveChanges();
                        this.loadPrinters();
                        Function.Sound.Deleted();

                        ShowOverlay();
                        ShowCustomAlert showCustomAlert = new ShowCustomAlert("Operation accomplished successfully.", 2500);
                        showCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                        showCustomAlert.trnsMsgSuccess();
                        showCustomAlert.ShowDialog();
                        HideOverlay();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                    }
                }
            }
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPrinters();
        }

        private void btnPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Sound.Added();
            gridControlPrinters.ShowPrintPreview();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Printer"))
            {
                AddEditPrinter printer = new AddEditPrinter();
                printer.setPrintersObject(this);
                printer.setTypeOperation("Add");
                printer.ShowDialog();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            this.printerEdit();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string pleaseSelectItem;

            if (lang == "en")
            {
                pleaseSelectItem = "Please select Printer !";
            }
            else if (lang == "fr")
            {
                pleaseSelectItem = "Veuillez sélectionner une Imprimante !";
            }
            else
            {
                pleaseSelectItem = "الرجاء تحديد هذا الخيار";
            }
            if (this.printer_id!=0)
            {
                this.printerDelete();
                this.printer_id = 0;
            }
            else
            {
                XtraMessageBox.Show(pleaseSelectItem);
            }

        }

        private void btnPrintItems_Click(object sender, EventArgs e)
        {
            CustomPrintGridControl customPrint = new CustomPrintGridControl(gridControlPrinters);
            customPrint.PrintGridControl(gridViewPrinters);
        }

        private void btnRefreshItems_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            Function.Sound.Selected();
            this.loadPrinters();
        }

        private void btnReloadPrinters_Click(object sender, EventArgs e)
        {
            string lang = Properties.Settings.Default.Lang;

            string confirmation;
            string areYouSure;
            string success;
            string error;
            string canceled;
            string printersReloadedSuccess;
            string anErrorOccurred;
            string operationCanceled;

            if (lang == "en")
            {
                confirmation = "Confirm Reload";
                areYouSure = "Are you sure you want to reload printers? This will remove all existing printer entries.";
                success = "Success";
                error = "Error";
                canceled = "Canceled";
                printersReloadedSuccess = "Printers reloaded successfully";
                anErrorOccurred = "An error occurred while reloading printers: ";
                operationCanceled = "Operation canceled";
            }
            else if (lang == "fr")
            {
                confirmation = "Confirmer le rechargement";
                areYouSure = "Êtes-vous sûr de vouloir recharger les imprimantes ? Cela supprimera toutes les entrées d'imprimante existantes.";
                success = "Succès";
                error = "Erreur";
                canceled = "Annulé";
                printersReloadedSuccess = "Imprimantes rechargées avec succès";
                anErrorOccurred = "Une erreur s'est produite lors du rechargement des imprimantes : ";
                operationCanceled = "Opération annulée";
            }
            else
            {
                confirmation = "تأكيد التحديث";
                areYouSure = "هل أنت متأكد من رغبتك في إعادة تحميل الطابعات؟ سيؤدي هذا إلى إزالة كافة إدخالات الطابعة الموجودة.";
                success = "النجاح";
                error = "خطأ";
                canceled = "تم الإلغاء";
                printersReloadedSuccess = "تم إعادة تحميل الطابعات بنجاح";
                anErrorOccurred = "حدث خطأ أثناء إعادة تحميل الطابعات:";
                operationCanceled = "تم إلغاء العملية";
            }

            // Show confirmation dialog
            var result = MessageBox.Show(areYouSure, confirmation, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var context = new AppDbContext())
                    {
                        // Clear existing printers
                        context.Printers.RemoveRange(context.Printers);

                        // Add each installed printer
                        foreach (string printerName in PrinterSettings.InstalledPrinters)
                        {
                            Models.Printer printer = new Models.Printer
                            {
                                Title = printerName,
                                PrinterIpAddress = "127.0.0.1",
                                PrinterPort = 60,
                                CharactersPerLine = "60",
                                Type = "Type",
                                BusinessLocationId = Properties.Settings.Default.BusinessLocationId,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            };

                            context.Printers.Add(printer);
                        }

                        // Save changes to the database
                        context.SaveChanges();

                        Sound.Added();

                        this.loadPrinters();

                        MessageBox.Show(printersReloadedSuccess, success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(anErrorOccurred + ex.Message, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(operationCanceled, canceled, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}