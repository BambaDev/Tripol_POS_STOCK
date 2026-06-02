using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.MasterDetailReport
{
    public partial class MasterDetailReport : DevExpress.XtraReports.UI.XtraReport
    {
        public MasterDetailReport(int blId,string date)
        {
            InitializeComponent();
            businessLocationId.Value = blId;
            caseDate.Value = date;
        }
    }
}
