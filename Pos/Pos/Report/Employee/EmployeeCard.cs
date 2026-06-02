using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.Employee
{
    public partial class EmployeeCard : DevExpress.XtraReports.UI.XtraReport
    {
        public EmployeeCard(int EmployeeId)
        {
            InitializeComponent();
            param.Value = EmployeeId;
        }
    }
}
