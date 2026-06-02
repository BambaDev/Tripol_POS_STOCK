using DevExpress.XtraEditors;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class Shared
    {
        public static AppDbContext db  = new AppDbContext();

        public static DataTable DateRanges()
        {
            DataTable dt = new();

            dt.Columns.Add("Id");
            dt.Columns.Add("Name");

            dt.Rows.Add(1, "Today");
            dt.Rows.Add(2, "Yesterday");
            dt.Rows.Add(3, "This Week");
            dt.Rows.Add(4, "Last Week");
            dt.Rows.Add(5, "This Month");
            dt.Rows.Add(6, "Last Month");
            dt.Rows.Add(7, "This Year");
            dt.Rows.Add(8, "Last Year");
            dt.Rows.Add(9, "Custom");

            return dt;
        }

        public static void SetRanges(DateEdit dtStart, DateEdit dtEnd, LookUpEdit cbxDate)
        {
            dtStart.Enabled = false;
            dtEnd.Enabled = false;

            int selectedValue = int.Parse(cbxDate.EditValue.ToString());
            switch (selectedValue)
            {
                case 1:
                    dtStart.DateTime = DateTime.Today;
                    dtEnd.DateTime = dtStart.DateTime.AddDays(1);
                    break;
                case 2:
                    dtStart.DateTime = DateTime.Today.AddDays(-1);
                    dtEnd.DateTime = dtStart.DateTime.AddDays(1);
                    break;
                case 3:
                    dtStart.DateTime = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 1);
                    dtEnd.DateTime = dtStart.DateTime.AddDays(7);
                    break;
                case 4:
                    dtStart.DateTime = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 8);
                    dtEnd.DateTime = dtStart.DateTime.AddDays(7);
                    break;
                case 5:
                    dtStart.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    dtEnd.DateTime = dtStart.DateTime.AddMonths(1);
                    break;
                case 6:
                    dtStart.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    dtEnd.DateTime = dtStart.DateTime.AddMonths(1);
                    break;
                case 7:
                    dtStart.DateTime = new DateTime(DateTime.Today.Year, 1, 1);
                    dtEnd.DateTime = dtStart.DateTime.AddYears(1);
                    break;
                case 8:
                    dtStart.DateTime = new DateTime(DateTime.Today.Year - 1, 1, 1);
                    dtEnd.DateTime = dtStart.DateTime.AddYears(1);
                    break;
                case 9:
                    dtStart.Enabled = true;
                    dtEnd.Enabled = true;
                    break;
                default:
                    cbxDate.EditValue = 9;
                    break;
            }
        }
    }
}
