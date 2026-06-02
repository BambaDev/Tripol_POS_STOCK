using DevExpress.XtraEditors;
using Pos.Function;
using Pos.Models;
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

namespace Pos.Forms.Alert
{
    public partial class TodaySummary : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt = new DataTable();

        public TodaySummary()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 5, 5));
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

        private void TodaySummary_Load(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add("Period", typeof(string));
                    dt.Columns.Add("GrossTotal", typeof(string));
                    dt.Columns.Add("NetTotal", typeof(string));
                }

                // Clear existing rows to avoid duplication
                dt.Rows.Clear();

                // Calculate date ranges
                var today = DateTime.Today;
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var startOfYear = new DateTime(today.Year, 1, 1);

                var endOfLastWeek = startOfWeek.AddDays(-1);
                var startOfLastWeek = endOfLastWeek.AddDays(-6);

                var startOfLastMonth = startOfMonth.AddMonths(-1);
                var endOfLastMonth = startOfMonth.AddDays(-1);

                var startOfLastYear = startOfYear.AddYears(-1);
                var endOfLastYear = startOfYear.AddDays(-1);

                // Fetch and calculate gross and net totals
                var grossTotalToday = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date == today)
                    .Sum(c => (decimal?)c.Amount) ?? 0;

                var netTotalToday = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date == today)
                    .Sum(c => (decimal?)c.Paid) ?? 0;

                var grossTotalLastWeek = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastWeek && c.CreatedAt.Value.Date <= endOfLastWeek)
                    .Sum(c => (decimal?)c.Amount) ?? 0;

                var netTotalLastWeek = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastWeek && c.CreatedAt.Value.Date <= endOfLastWeek)
                    .Sum(c => (decimal?)c.Paid) ?? 0;

                var grossTotalLastMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastMonth && c.CreatedAt.Value.Date <= endOfLastMonth)
                    .Sum(c => (decimal?)c.Amount) ?? 0;

                var netTotalLastMonth = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastMonth && c.CreatedAt.Value.Date <= endOfLastMonth)
                    .Sum(c => (decimal?)c.Paid) ?? 0;

                var grossTotalLastYear = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastYear && c.CreatedAt.Value.Date <= endOfLastYear)
                    .Sum(c => (decimal?)c.Amount) ?? 0;

                var netTotalLastYear = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date >= startOfLastYear && c.CreatedAt.Value.Date <= endOfLastYear)
                    .Sum(c => (decimal?)c.Paid) ?? 0;

                string lang = Properties.Settings.Default.Lang;

                string nameTotalAmountToday = "Total Today";
                string nameTotalAmountLastWeek = "Total Last Week";
                string nameTotalAmountLastMonth = "Total Last Month";
                string nameTotalAmountLastYear = "Total Last Year";

                if (lang == "fr")
                {
                    nameTotalAmountToday = "Total aujourd'hui";
                    nameTotalAmountLastWeek = "Total de la semaine dernière";
                    nameTotalAmountLastMonth = "Total du mois dernier";
                    nameTotalAmountLastYear = "Total de l'année dernière";
                }
                else if (lang == "ar")
                {
                    nameTotalAmountToday = "إجمالي اليوم";
                    nameTotalAmountLastWeek = "إجمالي في الأسبوع الماضي";
                    nameTotalAmountLastMonth = "إجمالي في الشهر الماضي";
                    nameTotalAmountLastYear = "إجمالي في العام الماضي";
                }

                // Define summary items with gross and net totals
                var summaryItems = new[]
                {
                    new { Period = nameTotalAmountToday, GrossTotal = grossTotalToday, NetTotal = netTotalToday },
                    new { Period = nameTotalAmountLastWeek, GrossTotal = grossTotalLastWeek, NetTotal = netTotalLastWeek },
                    new { Period = nameTotalAmountLastMonth, GrossTotal = grossTotalLastMonth, NetTotal = netTotalLastMonth },
                    new { Period = nameTotalAmountLastYear, GrossTotal = grossTotalLastYear, NetTotal = netTotalLastYear }
                };

                // Add each summary item as a new row in the DataTable
                foreach (var item in summaryItems)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Period"] = item.Period;
                    newRow["GrossTotal"] = Function.Helper.FormatAmount(item.GrossTotal.ToString());
                    newRow["NetTotal"] = Function.Helper.FormatAmount(item.NetTotal.ToString());
                    dt.Rows.Add(newRow);
                }

                // Bind the DataTable to the GridControl
                gridControlTodaySummary.DataSource = dt;
            }

            //if (dt.Columns.Count == 0)
            //{
            //    dt.Columns.Add("Section", typeof(string));
            //    dt.Columns.Add("Value", typeof(string));
            //}

            //gridControlTodaySummary.DataSource = Function.Helper.getTodaySummaryTable(
            //    dt,
            //    Properties.Settings.Default.BusinessLocation
            //);

            using (AppDbContext AppDb = new AppDbContext())
            {
                var lastRegisterRecord = AppDb.RegisterRecords
                 .Where(r => r.UserId == Properties.Settings.Default.userId)
                 .OrderByDescending(r => r.Id) // Vous pouvez aussi utiliser un autre champ, comme CreatedAt
                 .FirstOrDefault();
                if (lastRegisterRecord != null)
                {
                    txtCashInHand.Text = lastRegisterRecord.CashInHand.ToString();
                }
                else
                {
                    txtCashInHand.Text = "0";
                }
            }
        }

        private void btnPrintTodaySummary_Click(object sender, EventArgs e)
        {
            Sound.Added();
            gridControlTodaySummary.ShowPrintPreview();
        }

        private void btnTodaySummaryWhatsUpSender_Click(object sender, EventArgs e)
        {
            if (Function.Helper.canSendMessageViaWhatsUp())
            {
                Models.Setting setting = Function.Helper.getSetting();

                string message = Function.Helper.CreateTodaySummaryMessage(Properties.Settings.Default.BusinessLocation);

                WhatsAppMessageSender whatsAppMessageSender = new Function.WhatsAppMessageSender(
                    setting.AccountSid,
                    setting.AuthToken
                );

                whatsAppMessageSender.SendMessageAsync(
                    setting.FromPhoneNumber,
                    "782394356",
                    message
                );

                Sound.Added();
            }
            else
            {
                Alert.FeatureDisabled featureDisabled = new Alert.FeatureDisabled();
                featureDisabled.ShowDialog();
            }
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}