using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Function;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.DataProcessing.InMemoryDataProcessor.AddSurrogateOperationAlgorithm;
using Pos.Models;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.Devices;
using System.Windows.Documents;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DevExpress.XtraWaitForm;
using Pos.Forms.Overlay;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using Pos.Forms.Auth;
using System.Data.SqlClient;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Pos.Forms.Setting
{
    public partial class Backups : DevExpress.XtraEditors.XtraForm
    {
        private OverlayForm overlay;
        public string resourceDirectory = "";
        public string lang = "en";

        public Backups()
        {
            InitializeComponent();

            this.toRtl();

            // Set the resource directory path relative to the executable
            resourceDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Settings");

            // Ensure the resource directory exists
            if (!Directory.Exists(resourceDirectory))
            {
                Directory.CreateDirectory(resourceDirectory);
            }

            // style the grid view
            gridViewBackups.RowStyle += gridViewBackups_RowStyle;
            gridViewBackups.FocusedRowChanged += gridViewBackups_FocusedRowChanged;
            gridViewBackups.CustomDrawCell += gridViewBackups_CustomDrawCell;
            gridViewBackups.RowHeight = Function.Helper.RowHeight;
            gridViewBackups.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewBackups_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewBackups.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewBackups_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewBackups.FocusedRowHandle && e.Column == gridViewBackups.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewBackups_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                this.ApplyCustomFont();

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
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            btnBackupDatabase.Font = customFont;
            btnRestoreDatabase.Font = customFont;

            layoutControlGroupBackup.AppearanceGroup.Font = customFont;
            layoutControlGroupBackupsList.AppearanceGroup.Font = customFont;
            layoutControlGroupRestore.AppearanceGroup.Font = customFont;

            Function.CustomArabicFont.LoadCustomFont(9.0F, isBold);
            Font customFont9 = Function.CustomArabicFont.customFont;

            foreach (GridColumn column in gridViewBackups.Columns)
            {
                column.AppearanceHeader.Font = customFont9;

                if (Function.CustomArabicFont.isArabicData())
                    column.AppearanceCell.Font = customFont9;
            }
        }

        public void hideBackupButton()
        {
            layoutControlGroupBackup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public void hideDeleteColumnButton()
        {
            colDelete.Visible = false;
        }
        
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

        private void Backups_Load(object sender, EventArgs e)
        {
            this.LoadBackupsToGrid();
        }

        private async void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to backup the database?", "Confirm Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    // Change this path to a more accessible directory, such as C:\Backups
                    string backupFolderPath = @"C:\Backups";
                    Directory.CreateDirectory(backupFolderPath);

                    string backupFileName = $"Backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
                    string backupFilePath = Path.Combine(backupFolderPath, backupFileName);

                    ShowOverlay();
                    ShowCustomAlert showCustomAlert = new ShowCustomAlert("Please be patient until the process is finished.", 0, false);
                    showCustomAlert.SetIcon(ShowCustomAlert.AlertIcon.Loading);
                    showCustomAlert.TopMost = true;
                    showCustomAlert.Show();

                    try
                    {
                        await Task.Run(() => DatabaseUtility.BackupDatabaseAsync(context, backupFilePath));

                        string logFilePath = Path.Combine(backupFolderPath, "BackupLog.txt");
                        using (StreamWriter sw = new StreamWriter(logFilePath, true))
                        {
                            sw.WriteLine($"{backupFileName}\t{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{backupFilePath}");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"An error occurred while backing up the database: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        showCustomAlert.Close();

                        ShowCustomAlert successCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                        successCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                        successCustomAlert.trnsMsgSuccess();
                        successCustomAlert.ShowDialog();
                        HideOverlay();

                        this.LoadBackupsToGrid(); // Refresh the grid after backup

                        Sound.Selected();

                        MessageBox.Show("Database backup completed successfully.", "Backup Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LoadBackupsToGrid()
        {
            string backupFolderPath = @"C:\Backups";
            string logFilePath = Path.Combine(backupFolderPath, "BackupLog.txt");

            if (!File.Exists(logFilePath))
            {
                MessageBox.Show("Log file does not exist.");
                return;
            }

            DataTable backupsTable = new DataTable();
            backupsTable.Columns.Add("Name", typeof(string));
            backupsTable.Columns.Add("Date", typeof(DateTime));
            backupsTable.Columns.Add("Path", typeof(string));

            using (StreamReader sr = new StreamReader(logFilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split('\t');
                    if (parts.Length == 3)
                    {
                        backupsTable.Rows.Add(parts[0], DateTime.Parse(parts[1]), parts[2]);
                    }
                }
            }

            if (backupsTable.Rows.Count == 0)
            {
                MessageBox.Show("No backups found.");
            }

            gridControlBackups.DataSource = backupsTable;
            gridControlBackups.RefreshDataSource();
        }

        private async void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restoring the database will overwrite the existing data. Are you sure you want to proceed?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var context = new AppDbContext())
                {
                    using (var openFileDialog = new OpenFileDialog
                    {
                        Filter = "Backup files (*.bak)|*.bak",
                        Title = "Select Database Backup"
                    })
                    {
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ShowOverlay();
                            ShowCustomAlert showCustomAlert = new ShowCustomAlert("Please be patient until the process is finished.", 0, false);
                            showCustomAlert.SetIcon(ShowCustomAlert.AlertIcon.Loading);
                            showCustomAlert.TopMost = true;
                            showCustomAlert.Show();

                            try
                            {
                                string backupFilePath = openFileDialog.FileName;
                                await Task.Run(() => DatabaseUtility.RestoreDatabaseAsync(context, backupFilePath));
                            }
                            catch (SqlException ex)
                            {
                                MessageBox.Show($"An error occurred while backing up the database: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                showCustomAlert.Close();
                                HideOverlay();

                                Sound.Selected();

                                MessageBox.Show("Restore complete. The application will now reload.", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Simulate reload by restarting the application
                                Application.Restart();
                            }
                        }
                    }
                }
            }
        }

        private void repoBackupDelete_Click(object sender, EventArgs e)
        {
            var selectedRow = gridViewBackups.GetFocusedRow() as BackupInfo;
            if (selectedRow != null && MessageBox.Show("Are you sure you want to delete this backup?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(selectedRow.Path);
                RemoveBackupFromLog(selectedRow.Path);
                LoadBackupsToGrid(); // Refresh the grid after deletion
                MessageBox.Show("Backup deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RemoveBackupFromLog(string backupFilePath)
        {
            string backupFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            string logFilePath = Path.Combine(backupFolderPath, "BackupLog.txt");

            var logLines = File.ReadAllLines(logFilePath).Where(line => !line.Contains(backupFilePath)).ToList();
            File.WriteAllLines(logFilePath, logLines);
        }

        private class BackupInfo
        {
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string Path { get; set; }
        }

        private async void repoRestoreBackup_Click(object sender, EventArgs e)
        {
            if (gridViewBackups.FocusedRowHandle > 0)
            {
                string path = gridViewBackups.GetRowCellValue(gridViewBackups.FocusedRowHandle, "Path").ToString();

                if (MessageBox.Show("Restoring the database will overwrite the existing data. Are you sure you want to proceed?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        ShowOverlay();
                        ShowCustomAlert showCustomAlert = new ShowCustomAlert("Please be patient until the process is finished.", 0, false);
                        showCustomAlert.SetIcon(ShowCustomAlert.AlertIcon.Loading);
                        showCustomAlert.TopMost = true;
                        showCustomAlert.Show();

                        try
                        {
                            await Task.Run(() => DatabaseUtility.RestoreDatabaseAsync(context, path));
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"An error occurred while backing up the database: {ex.Message}", "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            showCustomAlert.Close();

                            ShowCustomAlert successCustomAlert = new ShowCustomAlert("Operation accomplished successfully.");
                            successCustomAlert.SetPosition(ShowCustomAlert.AlertPosition.TopRight);
                            successCustomAlert.trnsMsgSuccess();
                            successCustomAlert.ShowDialog();
                            HideOverlay();

                            Sound.Selected();

                            MessageBox.Show("Restore complete. The application will now reload.", "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Simulate reload by restarting the application
                            Application.Restart();
                        }
                    }
                }
                else
                {
                    Sound.Wrong();
                }
            }
        }
    }
}