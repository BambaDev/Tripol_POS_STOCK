using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.Sales
{
    public partial class Salesx80mm : DevExpress.XtraReports.UI.XtraReport
    {
        
        public Salesx80mm(int SaleId)
        {
            
            InitializeComponent();
            ParamId.Value = SaleId;
        }
    }
}
