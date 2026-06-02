using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using Pos.Report;
using Pos.Report.Customer;
using Pos.Report.Employee;
using Pos.Report.MasterDetailReport;
using Pos.Report.Purchase;
using Pos.Report.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Forms.Report
{
    public partial class FormView : DevExpress.XtraEditors.XtraForm
    {
        public int id;

        private Salesx80mm rep80mm;

        public FormView()
        {
            InitializeComponent();

            this.toRtl();
        }

        public FormView(Salesx80mm rep)
        {

            InitializeComponent();
            rep80mm = rep;
            documentViewer.PrintingSystem = rep80mm.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1.6f;
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

        public void A4InvoiceM1View(A4InvoiceM1 a4InvoiceM1)
        {
            documentViewer.PrintingSystem = a4InvoiceM1.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        public void MasterDetailReportView(MasterDetailReport masterDetailReport)
        {
            documentViewer.PrintingSystem = masterDetailReport.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        public void CustomerList(CustomerList customerList)
        {
            documentViewer.PrintingSystem = customerList.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        public void CustomerBadge(CustomerCard customerCard)
        {
            documentViewer.PrintingSystem = customerCard.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        
        public void EmployeeBadge(EmployeeCard employeeCard)
        {
            documentViewer.PrintingSystem = employeeCard.PrintingSystem;
            documentViewer.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToPageWidth, new object[] { });
            documentViewer.Zoom = 1f;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        private void FormView_Load(object sender, EventArgs e)
        {
            //
        }
    }
}