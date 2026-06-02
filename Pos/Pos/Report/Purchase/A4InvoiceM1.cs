using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Pos.Report.Purchase
{
    public partial class A4InvoiceM1 : DevExpress.XtraReports.UI.XtraReport
    {
        public int purchaseId;

        public A4InvoiceM1()
        {
            InitializeComponent();
        }

        public void setId(int id)
        {
            this.purchaseId = id;
        }
    }
}
