using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.Employee
{
    public partial class EmployeePerformanceReview : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeePerformanceReview(int EmployeeId)
        {
            InitializeComponent();
            param.Value = EmployeeId;
        }
    }
}
