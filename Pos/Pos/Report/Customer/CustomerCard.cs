using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.Customer
{
    public partial class CustomerCard : DevExpress.XtraReports.UI.XtraReport
    {
        public CustomerCard(int CustomerId)
        {

            InitializeComponent();
            param.Value = CustomerId;
        }
    }
}
