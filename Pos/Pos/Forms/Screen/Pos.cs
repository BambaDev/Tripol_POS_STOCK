using DevExpress.CodeParser;
using DevExpress.CodeParser.Diagnostics;
using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Import.Html;
using Pos.Forms.Alert;
using Pos.Forms.Customer;
using Pos.Forms.Overlay;
using Pos.Forms.Printer;
using Pos.Forms.Product.PriceGroup;
using Pos.Forms.Product.Warehouse;
using Pos.Forms.Purchase;
using Pos.Forms.Report;
using Pos.Forms.Supplier;
using Pos.Function;
using Pos.Models;
using Pos.Report;
using Pos.Report.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static DevExpress.Utils.Filtering.ExcelFilterOptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Pay = Pos.Forms.Pay;

namespace Pos.Forms.Screen
{
    public partial class Pos : DevExpress.XtraEditors.XtraForm
    {
        private System.Windows.Forms.Timer searchTimer = new System.Windows.Forms.Timer();
        private OverlayForm overlay;
        public Sale.Sales sales = null;
        DataTable dt = new DataTable();
        public string type = "Add";
        public string saleType = "Carry in";
        public int user_id = 0;
        public int sale_id = 0;
        public int productId = 0;
        public int warehouse_id = 0;
        public int business_location_id = 0;
        public int row_idex = 0;
        public int isScanProductId = 0;
        public int currentPriceGroupId = 0;

        // SÉCURITÉ: Flag anti-double-clic pour prévenir double sauvegarde
        private bool _isSaving = false;

        private int currentPage = 1;
        private int totalPages = 1;
        private int itemsPerPage = 20;
        private string searchTerm = string.Empty;

        private int hdCurrentPage = 1;
        private int hdTotalPages = 1;
        private int hdItemsPerPage = 20;
        private string hdSearchTerm = string.Empty;

        private int uoCurrentPage = 1;
        private int uoTotalPages = 1;
        private int uoItemsPerPage = 20;
        private string uoSearchTerm = string.Empty;

        private int loCurrentPage = 1;
        private int loTotalPages = 1;
        private int loItemsPerPage = 20;
        private string loSearchTerm = string.Empty;

        private int lcCurrentPage = 1;
        private int lcTotalPages = 1;
        private int lcItemsPerPage = 20;
        private string lcSearchTerm = string.Empty;

        private int srCurrentPage = 1;
        private int srTotalPages = 1;
        private int srItemsPerPage = 20;
        private string srSearchTerm = string.Empty;

        private int cateCurrentPage = 1;
        private int cateItemsPerPage = 10;
        private string cateSearchTerm = string.Empty;
        public Models.Sale localSale = null;
        public DataRow currentExistingRow;
        public decimal oldpaidAmount = 0;
        public Pos()
        {
            InitializeComponent();

            // Setup the timer
            searchTimer.Interval = 300; // Delay for 300 milliseconds
            searchTimer.Tick += SearchTimer_Tick;

            this.toRtl();

            // style the grid view
            gridViewCustomerSales.RowStyle += gridViewCustomerSales_RowStyle;
            gridViewCustomerSales.FocusedRowChanged += gridViewCustomerSales_FocusedRowChanged;
            gridViewCustomerSales.CustomDrawCell += gridViewCustomerSales_CustomDrawCell;
            gridViewCustomerSales.RowHeight = Function.Helper.RowHeight;
            gridViewCustomerSales.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewCustomerSaleDetails.RowStyle += gridViewCustomerSaleDetails_RowStyle;
            gridViewCustomerSaleDetails.FocusedRowChanged += gridViewCustomerSaleDetails_FocusedRowChanged;
            gridViewCustomerSaleDetails.CustomDrawCell += gridViewCustomerSaleDetails_CustomDrawCell;
            gridViewCustomerSaleDetails.RowHeight = Function.Helper.RowHeight;
            gridViewCustomerSaleDetails.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewLatestCustomers.RowStyle += gridViewLatestCustomers_RowStyle;
            gridViewLatestCustomers.FocusedRowChanged += gridViewLatestCustomers_FocusedRowChanged;
            gridViewLatestCustomers.CustomDrawCell += gridViewLatestCustomers_CustomDrawCell;
            gridViewLatestCustomers.RowHeight = Function.Helper.RowHeight;
            gridViewLatestCustomers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewSupplierPurchases.RowStyle += gridViewSupplierPurchases_RowStyle;
            gridViewSupplierPurchases.FocusedRowChanged += gridViewSupplierPurchases_FocusedRowChanged;
            gridViewSupplierPurchases.CustomDrawCell += gridViewSupplierPurchases_CustomDrawCell;
            gridViewSupplierPurchases.RowHeight = Function.Helper.RowHeight;
            gridViewSupplierPurchases.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewSupplierPurchaseDetails.RowStyle += gridViewSupplierPurchaseDetails_RowStyle;
            gridViewSupplierPurchaseDetails.FocusedRowChanged += gridViewSupplierPurchaseDetails_FocusedRowChanged;
            gridViewSupplierPurchaseDetails.CustomDrawCell += gridViewSupplierPurchaseDetails_CustomDrawCell;
            gridViewSupplierPurchaseDetails.RowHeight = Function.Helper.RowHeight;
            gridViewSupplierPurchaseDetails.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewLatestSuppliers.RowStyle += gridViewLatestSuppliers_RowStyle;
            gridViewLatestSuppliers.FocusedRowChanged += gridViewLatestSuppliers_FocusedRowChanged;
            gridViewLatestSuppliers.CustomDrawCell += gridViewLatestSuppliers_CustomDrawCell;
            gridViewLatestSuppliers.RowHeight = Function.Helper.RowHeight;
            gridViewLatestSuppliers.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewLatestSaleReturns.RowStyle += gridViewLatestSaleReturns_RowStyle;
            gridViewLatestSaleReturns.FocusedRowChanged += gridViewLatestSaleReturns_FocusedRowChanged;
            gridViewLatestSaleReturns.CustomDrawCell += gridViewLatestSaleReturns_CustomDrawCell;
            gridViewLatestSaleReturns.RowHeight = Function.Helper.RowHeight;
            gridViewLatestSaleReturns.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewHoldOrders.RowStyle += gridViewHoldOrders_RowStyle;
            gridViewHoldOrders.FocusedRowChanged += gridViewHoldOrders_FocusedRowChanged;
            gridViewHoldOrders.CustomDrawCell += gridViewHoldOrders_CustomDrawCell;
            gridViewHoldOrders.RowHeight = Function.Helper.RowHeight;
            gridViewHoldOrders.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewUnpaidOrders.RowStyle += gridViewUnpaidOrders_RowStyle;
            gridViewUnpaidOrders.FocusedRowChanged += gridViewUnpaidOrders_FocusedRowChanged;
            gridViewUnpaidOrders.CustomDrawCell += gridViewUnpaidOrders_CustomDrawCell;
            gridViewUnpaidOrders.RowHeight = Function.Helper.RowHeight;
            gridViewUnpaidOrders.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;

            // style the grid view
            gridViewLatestOrders.RowStyle += gridViewLatestOrders_RowStyle;
            gridViewLatestOrders.FocusedRowChanged += gridViewLatestOrders_FocusedRowChanged;
            gridViewLatestOrders.CustomDrawCell += gridViewLatestOrders_CustomDrawCell;
            gridViewLatestOrders.RowHeight = Function.Helper.RowHeight;
            gridViewLatestOrders.ColumnPanelRowHeight = Function.Helper.ColumnPanelRowHeight;
        }

        // style the grid view
        private void gridViewCustomerSales_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCustomerSales.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCustomerSales_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCustomerSales.FocusedRowHandle && e.Column == gridViewCustomerSales.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCustomerSales_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewCustomerSaleDetails_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewCustomerSaleDetails.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewCustomerSaleDetails_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewCustomerSaleDetails.FocusedRowHandle && e.Column == gridViewCustomerSaleDetails.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewCustomerSaleDetails_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }


        // style the grid view
        private void gridViewLatestCustomers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewLatestCustomers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewLatestCustomers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewLatestCustomers.FocusedRowHandle && e.Column == gridViewLatestCustomers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewLatestCustomers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }


        // style the grid view
        private void gridViewSupplierPurchases_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewSupplierPurchases.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewSupplierPurchases_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewSupplierPurchases.FocusedRowHandle && e.Column == gridViewSupplierPurchases.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewSupplierPurchases_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewSupplierPurchaseDetails_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewLatestCustomers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewSupplierPurchaseDetails_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewLatestCustomers.FocusedRowHandle && e.Column == gridViewLatestCustomers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewSupplierPurchaseDetails_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewLatestSuppliers_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewLatestSuppliers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewLatestSuppliers_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewLatestSuppliers.FocusedRowHandle && e.Column == gridViewLatestSuppliers.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewLatestSuppliers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewLatestSaleReturns_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewLatestSaleReturns.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewLatestSaleReturns_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewLatestSaleReturns.FocusedRowHandle && e.Column == gridViewLatestSaleReturns.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewLatestSaleReturns_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewHoldOrders_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewHoldOrders.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewHoldOrders_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewHoldOrders.FocusedRowHandle && e.Column == gridViewHoldOrders.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewHoldOrders_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewUnpaidOrders_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewUnpaidOrders.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewUnpaidOrders_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewUnpaidOrders.FocusedRowHandle && e.Column == gridViewUnpaidOrders.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewUnpaidOrders_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        // style the grid view
        private void gridViewLatestOrders_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Check if the row is even or odd
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.RowColor);
                    e.Appearance.ForeColor = Color.White; // Optional: change text color for better visibility
                }

                if (e.RowHandle == gridViewLatestSuppliers.FocusedRowHandle)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.FocusColor); // Focus color
                    e.Appearance.ForeColor = Color.White; // Optional
                }
            }
        }

        // CustomDrawCell event for clicked cell appearance
        private void gridViewLatestOrders_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle == gridViewLatestOrders.FocusedRowHandle && e.Column == gridViewLatestOrders.FocusedColumn)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedCellColor); // Clicked cell color
                e.Appearance.ForeColor = Color.White; // Optional
            }
        }

        // FocusedRowChanged event for clicked row background color
        private void gridViewLatestOrders_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.Appearance.FocusedRow.BackColor = ColorTranslator.FromHtml(Function.Helper.ClickedRowBgColor); // Clicked row background color
            view.Appearance.FocusedRow.ForeColor = Color.White; // Optional: set text color for focused row
        }

        public Pos(Models.Sale sale)
        {
            InitializeComponent();
            type = "Edit";
            LoadData(sale);

            // Setup the timer
            searchTimer.Interval = 300; // Delay for 300 milliseconds
            searchTimer.Tick += SearchTimer_Tick;

            this.toRtl();
        }

        public void toRtl()
        {
            string lang = Properties.Settings.Default.Lang;

            if (lang == "ar")
            {
                //this.ApplyCustomFont();

                // Set the form to use RTL
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;

                // Set individual controls to use RTL if necessary
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    control.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                }
            }
        }

        public void ApplyCustomFont(float fontSize = 12.0F, bool isBold = false)
        {
            // Load the custom font with specified size and style
            Function.CustomArabicFont.LoadCustomFont(fontSize, isBold);
            Font customFont = Function.CustomArabicFont.customFont;

            // Apply the custom font to the form
            this.Font = customFont;

            // Apply the custom font to buttons named Btn1 to Btn20
            for (int i = 1; i <= 20; i++)
            {
                System.Windows.Forms.Control btn = this.Controls.Find("Btn" + i, true).FirstOrDefault();
                System.Windows.Forms.Control simpleButton = this.Controls.Find("simpleButton" + i, true).FirstOrDefault();

                if (btn != null)
                {
                    btn.Font = customFont;
                }

                if (simpleButton != null)
                {
                    simpleButton.Font = customFont;
                }
            }
        }

        public void LoadData(Models.Sale sale)
        {
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("ProductId", typeof(int));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("SaleQuantity", typeof(int));
                dt.Columns.Add("UnitCostBd", typeof(decimal));
                dt.Columns.Add("DiscountPercent", typeof(decimal));
                dt.Columns.Add("UnitCostBt", typeof(decimal));
                dt.Columns.Add("LineTotal", typeof(decimal));
                dt.Columns.Add("ProfitMargin", typeof(decimal));
                dt.Columns.Add("UnitSellingPrice", typeof(decimal));
            }
            localSale = sale;
            this.sale_id = sale.Id;

            if (sale.SaleType == "Carry in")
            {
                txtSaleType.SelectedIndex = 0;

            }
            else if (sale.SaleType == "Pick up")
            {
                txtSaleType.SelectedIndex = 1; // ou la valeur associée à "Pick up"
            }
            else
            {
                txtSaleType.SelectedIndex = 2; // ou la valeur associée à l'autre option
            }

            txtSaleDate.EditValue = DateTime.Parse(sale.SaleDate.ToString());
            txtReferenceNo.Text = sale.ReferenceNo;

            txtSaleStatus.EditValue = sale.SaleStatus;
            txtPaymentStatus.EditValue = sale.PaymentSatus;
            txtPaidAmount.EditValue = decimal.Parse(sale.PaidAmount.ToString());
            txtTotalTax.EditValue = decimal.Parse(sale.TotalTax.ToString());
            txtTotalDiscount.EditValue = decimal.Parse(sale.TotalDiscount.ToString());
            txtDue.EditValue = decimal.Parse(sale.Due.ToString());
            txtReturnAmount.EditValue = decimal.Parse(sale.ReturnAmount.ToString());
            txtNetTotalAmount.Text = sale.NetTotalAmount.ToString();

            txtCustomer.EditValue = sale.CustomerId;
            txtWarehouse.EditValue = sale.WarehouseId;

            var saleDetails = Shared.db.SaleDetails.Where(m => m.SaleId == sale_id).ToList();

            // Remplir le DataTable avec les éléments de SaleDetails
            foreach (var saleDetail in saleDetails)
            {
                DataRow newRow = dt.NewRow();
                newRow["Id"] = saleDetail.Id;
                newRow["ProductId"] = saleDetail.ProductId;
                newRow["UnitId"] = saleDetail.UnitId;
                newRow["ProductName"] = saleDetail.ProductName;
                newRow["SaleQuantity"] = saleDetail.SaleQuantity;
                newRow["UnitCostBd"] = saleDetail.UnitCostBd;
                newRow["DiscountPercent"] = saleDetail.DiscountPercent;
                newRow["UnitCostBt"] = saleDetail.UnitCostBt;
                newRow["LineTotal"] = saleDetail.LineTotal;
                newRow["ProfitMargin"] = saleDetail.ProfitMargin;
                newRow["UnitSellingPrice"] = saleDetail.UnitSellingPrice;
                dt.Rows.Add(newRow);
            }

            // Assigner le DataTable au gridControl
            gridControlProducts.DataSource = dt;
            txtNetTotalAmount.Text = this.calculeTotal().ToString();
            txtBussLocation.EditValue = sale.BusinessLocationId;
        }

        public decimal CalculateTotalBenefit()
        {
            // Variable to store total benefit
            decimal totalBenefit = 0;

            // Loop through each row to calculate the benefit
            foreach (DataRow row in dt.Rows)
            {
                // Get UnitCostBt (purchase price) and UnitSellingPrice (sale price) values
                decimal unitCost = row["UnitCostBt"] != DBNull.Value ? Convert.ToDecimal(row["UnitCostBt"]) : 0;
                decimal unitSellingPrice = row["UnitSellingPrice"] != DBNull.Value ? Convert.ToDecimal(row["UnitSellingPrice"]) : 0;

                // Calculate Benefit
                decimal benefit = unitSellingPrice - unitCost;

                // Add the benefit to the total
                totalBenefit += benefit;
            }

            txtBenefit.Text = totalBenefit.ToString();

            return totalBenefit;
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

        public void setSalesObject(Sale.Sales sales)
        {
            this.sales = sales;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.sales != null)
            {
                Models.Sale sale = Shared.db.Sales.Find(this.sales.sale_id);

                if (sale != null)
                {
                    this.sale_id = sale.Id;

                    if (sale.SaleType == "Carry in")
                    {
                        txtSaleType.SelectedIndex = 0;

                    }
                    else if (sale.SaleType == "Pick up")
                    {
                        txtSaleType.SelectedIndex = 1; // ou la valeur associée à "Pick up"
                    }
                    else
                    {
                        txtSaleType.SelectedIndex = 2; // ou la valeur associée à l'autre option
                    }


                    txtSaleDate.EditValue = DateTime.Parse(sale.SaleDate.ToString());
                    txtReferenceNo.Text = sale.ReferenceNo;
                    //txtDiscountType.EditValue = sale.DiscountType;

                    txtSaleStatus.EditValue = sale.SaleStatus;
                    txtPaymentStatus.EditValue = sale.PaymentSatus;
                    //txtDiscountAmount.EditValue = decimal.Parse(sale.DiscountAmount.ToString());
                    //txtAdditionalNotes.Text = sale.AdditionalNotes;
                    //txtShippingDetails.Text = sale.ShippingDetails;
                    //txtAdditionalShippingCharges.EditValue = decimal.Parse(sale.AdditionalShippingCharges.ToString());
                    txtPaidAmount.EditValue = decimal.Parse(sale.PaidAmount.ToString());
                    txtTotalTax.EditValue = decimal.Parse(sale.TotalTax.ToString());
                    txtTotalDiscount.EditValue = decimal.Parse(sale.TotalDiscount.ToString());
                    txtDue.EditValue = decimal.Parse(sale.Due.ToString());
                    txtReturnAmount.EditValue = decimal.Parse(sale.ReturnAmount.ToString());
                    txtNetTotalAmount.Text = sale.NetTotalAmount.ToString();
                    txtBussLocation.EditValue = Int32.Parse(sale.BusinessLocationId.ToString());
                    txtCustomer.EditValue = Int32.Parse(sale.CustomerId.ToString());
                    txtWarehouse.EditValue = Int32.Parse(sale.WarehouseId.ToString());

                    this.getSaleItems(this.sale_id);

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();
                }
                else
                {
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getSaleItems(int id)
        {
            gridControlProducts.DataSource = Shared.db.SaleDetails.Where(m => m.SaleId == id).ToList();
        }

        public void pay()
        {
            // SÉCURITÉ: Prévenir double-clic / double sauvegarde
            if (_isSaving)
            {
                XtraMessageBox.Show("Sauvegarde en cours, veuillez patienter...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isSaving = true;

            try
            {
                PayWithTransaction();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur sauvegarde vente: {ex.Message}\n{ex.StackTrace}");
                XtraMessageBox.Show("Erreur lors de la sauvegarde. Aucune modification appliquée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
            }
        }

        // SÉCURITÉ: Sauvegarde avec transaction atomique (tout ou rien)
        private void PayWithTransaction()
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                // TRANSACTION: Garantir que TOUTES les opérations réussissent ensemble
                using (var transaction = AppDb.Database.BeginTransaction())
                {
                    try
                    {
                        Models.Sale sale;

                        if (this.type == "Add")
                        {
                            sale = new Models.Sale();

                    decimal due = 0;
                    decimal returnAmount = 0;

                    if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                    {
                        returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                    }
                    else
                    {
                        due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                    }

                    sale.SaleDate = DateTime.Parse(txtSaleDate.EditValue.ToString());
                    sale.ReferenceNo = txtReferenceNo.Text;
                    sale.SaleType = saleType;
                    //sale.DiscountType = txtDiscountType.Text;
                    //sale.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                    sale.SaleStatus = txtSaleStatus.Text;
                    sale.PaymentSatus = txtPaymentStatus.Text;
                    //sale.AdditionalNotes = txtAdditionalNotes.Text;
                    //sale.ShippingDetails = txtShippingDetails.Text;
                    //sale.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                    sale.NetTotalAmount = this.calculeTotal();
                    sale.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                    sale.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                    sale.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                    sale.Due = due;
                    sale.ReturnAmount = returnAmount;
                    sale.NumberItems = dt.Rows.Count;
                    sale.BusinessLocationId = int.Parse(txtBussLocation.EditValue.ToString());
                    sale.CustomerId = Int32.Parse(txtCustomer.EditValue.ToString());
                    sale.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                    sale.UserId = Properties.Settings.Default.userId;
                    sale.SaleMonth = DateTime.Now.Month;
                    sale.SaleYear = DateTime.Now.Year;
                    sale.CreatedAt = DateTime.Now;
                    sale.UpdatedAt = DateTime.Now;

                    // SÉCURITÉ: Arrondir tous les montants à 2 décimales
                    Function.SaleValidator.RoundAllAmounts(sale);

                    // SÉCURITÉ: Valider la vente avant sauvegarde
                    var saleValidation = Function.SaleValidator.ValidateSale(sale);
                    if (!saleValidation.isValid)
                    {
                        throw new InvalidOperationException($"Vente invalide: {saleValidation.errorMessage}");
                    }

                    AppDb.Sales.Add(sale);

                    AppDb.SaveChanges();

                    List<SaleDetail> saleDetailList = new List<SaleDetail>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        /*
                        // Fetch the data from DataTable row
                        var productId = dt.Rows[i]["ProductId"] != DBNull.Value ? dt.Rows[i]["ProductId"].ToString() : "NULL";
                        var unitId = dt.Rows[i]["UnitId"] != DBNull.Value ? dt.Rows[i]["UnitId"].ToString() : "NULL";
                        var productName = dt.Rows[i]["ProductName"] != DBNull.Value ? dt.Rows[i]["ProductName"].ToString() : "NULL";
                        var saleQuantity = dt.Rows[i]["SaleQuantity"] != DBNull.Value ? dt.Rows[i]["SaleQuantity"].ToString() : "NULL";
                        var unitCostBd = dt.Rows[i]["UnitCostBd"] != DBNull.Value ? dt.Rows[i]["UnitCostBd"].ToString() : "NULL";
                        var discountPercent = dt.Rows[i]["DiscountPercent"] != DBNull.Value ? dt.Rows[i]["DiscountPercent"].ToString() : "NULL";
                        var unitCostBt = dt.Rows[i]["UnitCostBt"] != DBNull.Value ? dt.Rows[i]["UnitCostBt"].ToString() : "NULL";
                        var lineTotal = dt.Rows[i]["LineTotal"] != DBNull.Value ? dt.Rows[i]["LineTotal"].ToString() : "NULL";
                        var profitMargin = dt.Rows[i]["ProfitMargin"] != DBNull.Value ? dt.Rows[i]["ProfitMargin"].ToString() : "NULL";
                        var unitSellingPrice = dt.Rows[i]["UnitSellingPrice"] != DBNull.Value ? dt.Rows[i]["UnitSellingPrice"].ToString() : "NULL";

                        // Create a message string with all fields before creating the SaleDetail object
                        string message = $"ProductId: {productId}\n" +
                                         $"UnitId: {unitId}\n" +
                                         $"ProductName: {productName}\n" +
                                         $"SaleQuantity: {saleQuantity}\n" +
                                         $"UnitCostBd: {unitCostBd}\n" +
                                         $"DiscountPercent: {discountPercent}\n" +
                                         $"UnitCostBt: {unitCostBt}\n" +
                                         $"LineTotal: {lineTotal}\n" +
                                         $"ProfitMargin: {profitMargin}\n" +
                                         $"UnitSellingPrice: {unitSellingPrice}";

                        // Display the message box with all fields at the start of the loop
                        MessageBox.Show(message, "Sale Detail Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        */

                        SaleDetail saleDetail = new SaleDetail();
                        saleDetail.ProductId = Convert.ToInt32(this.dt.Rows[i]["ProductId"]);
                        saleDetail.UnitId = Convert.ToInt32(this.dt.Rows[i]["UnitId"]);
                        saleDetail.SaleId = sale.Id;
                        saleDetail.ProductName = this.dt.Rows[i]["ProductName"].ToString();
                        saleDetail.SaleQuantity = Convert.ToDecimal(this.dt.Rows[i]["SaleQuantity"]);
                        saleDetail.UnitCostBd = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBd"]);
                        saleDetail.DiscountPercent = Convert.ToDecimal(this.dt.Rows[i]["DiscountPercent"]);
                        saleDetail.UnitCostBt = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBt"]);
                        saleDetail.LineTotal = Convert.ToDecimal(this.dt.Rows[i]["LineTotal"]);
                        saleDetail.ProfitMargin = Convert.ToDecimal(this.dt.Rows[i]["ProfitMargin"]);
                        saleDetail.UnitSellingPrice = Convert.ToDecimal(this.dt.Rows[i]["UnitSellingPrice"]);
                        saleDetail.CreatedAt = DateTime.Now;
                        saleDetail.UpdatedAt = DateTime.Now;
                        saleDetailList.Add(saleDetail);

                        //Models.Product product = AppDb.Products.SingleOrDefault(x => x.Id == saleDetail.ProductId);
                        Models.ProductWarehouse productWarehouse = AppDb.ProductWarehouses
                         .FirstOrDefault(pw => pw.ProductId == saleDetail.ProductId && pw.WarehouseId == sale.WarehouseId);

                        if (productWarehouse != null)
                        {
                            productWarehouse.Qty -= saleDetail.SaleQuantity;
                            AppDb.ProductWarehouses.Update(productWarehouse);
                            AppDb.SaveChanges();
                        }
                    }

                    AppDb.SaleDetails.AddRange(saleDetailList);
                    AppDb.SaveChanges();

                    var payment = new Models.SalePayment
                    {
                        SaleId = sale.Id,
                        CustomerId = sale.CustomerId,
                        UserId = Properties.Settings.Default.userId,
                        BusinessLocationId = Properties.Settings.Default.BusinessLocation == 0 ? (int?)null : Properties.Settings.Default.BusinessLocation,
                        Amount = decimal.Parse(txtPaidAmount.EditValue.ToString()),
                        Due = due,
                        DueDate = DateTime.Now,
                        IsPaid = due == 0 ? true : false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    AppDb.SalePayments.Add(payment);

                    AppDb.SaveChanges();

                    if (due > 0)
                    {
                        Models.Customer customer = AppDb.Customers.Find(sale.CustomerId);

                        decimal currentDue = customer.CurrentDue.Value + due;

                        customer.CurrentDue = currentDue;
                        AppDb.Entry(customer).State = EntityState.Modified;
                        AppDb.SaveChanges();
                    }

                    btnMtDataReset_Click(null, EventArgs.Empty);

                    this.deleteCurrentHold(sale.ReferenceNo);

                    this.ReferenceNo();
                }
                else
                {
                    if (localSale == null)
                    {
                        sale = AppDb.Sales.Find(this.sales.sale_id);
                    }
                    else
                    {
                        sale = localSale;
                    }


                    decimal due = 0;
                    decimal returnAmount = 0;

                    if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                    {
                        returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                    }
                    else
                    {
                        due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                    }


                    if (sale != null)
                    {
                        sale.SaleDate = DateTime.Parse(txtSaleDate.EditValue.ToString());
                        sale.SaleType = saleType;
                        sale.UpdatedAt = DateTime.Now;

                        sale.NumberItems = dt.Rows.Count;
                        sale.PaymentSatus = txtPaymentStatus.Text;
                        sale.NetTotalAmount = this.calculeTotal();
                        sale.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                        sale.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                        sale.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                        sale.ReturnAmount = returnAmount;
                        sale.Due = due;

                        AppDb.Entry(sale).State = EntityState.Modified;
                        AppDb.SaveChanges();

                        // Liste existante des `SaleDetail`
                        var existingSaleDetails = sale.SaleDetails.ToList();
                        List<SaleDetail> saleDetailList = new List<SaleDetail>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int productId = Convert.ToInt32(dt.Rows[i]["ProductId"]);
                            var existingSaleDetail = existingSaleDetails.FirstOrDefault(sd => sd.ProductId == productId && sd.SaleId == sale.Id);

                            if (existingSaleDetail == null)
                            {
                                // Si le produit n'existe pas dans les détails, l'ajouter
                                SaleDetail saleDetail = new SaleDetail
                                {
                                    ProductId = productId,
                                    UnitId = Convert.ToInt32(this.dt.Rows[i]["UnitId"]),
                                    SaleId = sale.Id,
                                    ProductName = dt.Rows[i]["ProductName"].ToString(),
                                    SaleQuantity = Convert.ToDecimal(dt.Rows[i]["SaleQuantity"]),
                                    UnitCostBd = Convert.ToDecimal(dt.Rows[i]["UnitCostBd"]),
                                    DiscountPercent = Convert.ToDecimal(dt.Rows[i]["DiscountPercent"]),
                                    UnitCostBt = Convert.ToDecimal(dt.Rows[i]["UnitCostBt"]),
                                    LineTotal = Convert.ToDecimal(dt.Rows[i]["LineTotal"]),
                                    ProfitMargin = Convert.ToDecimal(dt.Rows[i]["ProfitMargin"]),
                                    UnitSellingPrice = Convert.ToDecimal(dt.Rows[i]["UnitSellingPrice"]),
                                    CreatedAt = DateTime.Now,
                                    UpdatedAt = DateTime.Now
                                };

                                saleDetailList.Add(saleDetail);

                                // Mettre à jour le stock

                                var productWarehouse = AppDb.ProductWarehouses.FirstOrDefault(pw => pw.ProductId == saleDetail.ProductId
                                && pw.WarehouseId == sale.WarehouseId);
                                if (productWarehouse != null)
                                {
                                    productWarehouse.Qty -= saleDetail.SaleQuantity;
                                    AppDb.ProductWarehouses.Update(productWarehouse);
                                    AppDb.SaveChanges();
                                }
                            }
                            else
                            {
                                // Si le produit existe, le modifier
                                decimal oldSaleQuantity = Convert.ToDecimal(existingSaleDetail.SaleQuantity);
                                existingSaleDetail.SaleQuantity = Convert.ToDecimal(dt.Rows[i]["SaleQuantity"]);
                                existingSaleDetail.UnitCostBd = Convert.ToDecimal(dt.Rows[i]["UnitCostBd"]);
                                existingSaleDetail.DiscountPercent = Convert.ToDecimal(dt.Rows[i]["DiscountPercent"]);
                                existingSaleDetail.UnitCostBt = Convert.ToDecimal(dt.Rows[i]["UnitCostBt"]);
                                existingSaleDetail.LineTotal = Convert.ToDecimal(dt.Rows[i]["LineTotal"]);
                                existingSaleDetail.ProfitMargin = Convert.ToDecimal(dt.Rows[i]["ProfitMargin"]);
                                existingSaleDetail.UnitSellingPrice = Convert.ToDecimal(dt.Rows[i]["UnitSellingPrice"]);
                                existingSaleDetail.UpdatedAt = DateTime.Now;

                                AppDb.SaleDetails.Update(existingSaleDetail);

                                var productWarehouse = AppDb.ProductWarehouses.FirstOrDefault(pw => pw.ProductId == existingSaleDetail.ProductId
                                && pw.WarehouseId == sale.WarehouseId);
                                if (productWarehouse != null)
                                {
                                    productWarehouse.Qty += oldSaleQuantity;
                                    productWarehouse.Qty -= existingSaleDetail.SaleQuantity;
                                    AppDb.ProductWarehouses.Update(productWarehouse);
                                    AppDb.SaveChanges();
                                }

                                existingSaleDetails.Remove(existingSaleDetail); // Retirer cet élément de la liste des éléments restants à supprimer
                            }
                        }
                        // Supprimer les anciens détails qui ne sont pas présents dans les nouveaux
                        foreach (var detailToDelete in existingSaleDetails)
                        {
                            Shared.db.SaleDetails.Remove(detailToDelete);

                            // Recréditer le stock

                            var productWarehouse = AppDb.ProductWarehouses.FirstOrDefault(pw => pw.ProductId == detailToDelete.ProductId
                            && pw.WarehouseId == sale.WarehouseId);
                            if (productWarehouse != null)
                            {
                                productWarehouse.Qty += detailToDelete.SaleQuantity;
                                AppDb.ProductWarehouses.Update(productWarehouse);
                                AppDb.SaveChanges();
                            }

                        }
                        // Ajouter les nouveaux détails
                        AppDb.SaleDetails.AddRange(saleDetailList);

                        // Sauvegarder les modifications dans la base de données
                        AppDb.SaveChanges();
                        decimal amoutadd = decimal.Parse(txtPaidAmount.EditValue.ToString()) - oldpaidAmount;
                        if (amoutadd > 0)
                        {
                            var payment = new Models.SalePayment
                            {
                                SaleId = sale.Id,
                                CustomerId = sale.CustomerId,
                                UserId = Properties.Settings.Default.userId,
                                BusinessLocationId = Properties.Settings.Default.BusinessLocation == 0 ? (int?)null : Properties.Settings.Default.BusinessLocation,
                                Amount = amoutadd,
                                Due = due,
                                DueDate = DateTime.Now,
                                IsPaid = due == 0 ? true : false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            };

                            AppDb.SalePayments.Add(payment);

                            AppDb.SaveChanges();

                        }

                        if (due > 0)
                        {
                            Models.Customer customer = AppDb.Customers.Find(sale.CustomerId);

                            decimal currentDue = customer.CurrentDue.Value + due;

                            customer.CurrentDue = currentDue;
                            AppDb.Entry(customer).State = EntityState.Modified;
                            AppDb.SaveChanges();
                        }

                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                        // TRANSACTION: Traiter les points de fidélité dans la même transaction
                        Function.Helper.ProcessLoyaltyPoints(sale.Id);

                        // COMMIT: Toutes les opérations ont réussi → Valider la transaction
                        transaction.Commit();

                        // Succès → Actions post-transaction (impression, sons, rafraîchissement UI)
                        this.printSalesX80mm(sale.Id);

                        Function.Sound.Added();

                        this.getLatestSale();
                        this.getUnpaidOrders();
                        this.GetCustomerSales();
                        this.todayTotalProfit();
                        this.todayTotalNetProfitAndGross();

                        this.todayTotalPurchases();
                        this.GetSupplierPurchases();
                        this.todayTotalPurchasesAndDue();

                        txtProductsCardSearch.Focus();
                    }
                    catch (Exception ex)
                    {
                        // ROLLBACK: En cas d'erreur, annuler TOUTES les modifications
                        transaction.Rollback();

                        // Logger l'erreur pour analyse
                        System.Diagnostics.Debug.WriteLine($"Transaction rollback: {ex.Message}\n{ex.StackTrace}");

                        throw; // Remonter l'exception pour affichage à l'utilisateur
                    }
                }
            }
        }

        public void printSalesX80mm(int saleId)
        {
            if (saleId != 0)
            {
                // Retrieve the printer name from settings
                string printerName = Properties.Settings.Default.PrinterReciept;

                // Initialize the TechnicalRepiarA4 report with the sale_id
                Salesx80mm report80mm = new Salesx80mm(saleId);
                report80mm.CreateDocument();

                // Check if the printer name is valid
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    XtraMessageBox.Show($"Printer \"{printerName}\" is not valid.", "Printer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a PrintTool to handle printing the document
                ReportPrintTool printTool = new ReportPrintTool(report80mm);

                // Set the printer name in the PrintTool
                printTool.PrinterSettings.PrinterName = printerName;

                try
                {
                    // Print the document using the specified printer
                    printTool.Print(printerName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"An error occurred while printing: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                XtraMessageBox.Show("You can't print without validated data.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dxValidationProviderSale.Validate())
            {
                if (dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }
                oldpaidAmount = decimal.Parse((txtPaidAmount.Text));
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Pay.Pay.Pay pay = new Pay.Pay.Pay(this);

                pay.FormClosed += (s, args) => overlay.Close();

                pay.Show();
                pay.TopMost = true;
            }
            else
            {
                Function.Sound.Wrong();
            }
        }

        public void changeNumberItems(int sale_id)
        {
            Models.Sale sale = Shared.db.Sales.Find(sale_id);

            if (sale != null)
            {
                if (type == "Add")
                {
                    sale.NumberItems = dt.Rows.Count;
                }
                else
                {
                    sale.NumberItems = gridViewProducts.RowCount;
                    sale.NetTotalAmount = this.calculeTotal();
                }

                sale.UpdatedAt = DateTime.Now;

                Shared.db.Entry(sale).State = EntityState.Modified;

                Shared.db.SaveChanges();
            }
        }

        public decimal calculeTotal()
        {
            // SÉCURISÉ: Calcul avec arrondi cohérent (prévient accumulation erreurs décimales)
            decimal total = 0;

            if (type == "Add")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decimal lineTotal = decimal.Parse(dt.Rows[i]["LineTotal"].ToString());
                    // Arrondir chaque ligne individuellement
                    total += Function.MoneyHelper.Round(lineTotal);
                }
            }
            else
            {
                for (int i = 0; i < gridViewProducts.RowCount; i++)
                {
                    decimal lineTotal = decimal.Parse(gridViewProducts.GetRowCellValue(i, "LineTotal").ToString());
                    // Arrondir chaque ligne individuellement
                    total += Function.MoneyHelper.Round(lineTotal);
                }
            }

            // Arrondir le total final
            total = Function.MoneyHelper.Round(total);

            // VALIDATION: Vérifier que le total n'est pas absurde
            if (total < 0)
            {
                System.Diagnostics.Debug.WriteLine("ALERTE: Total négatif détecté!");
                total = 0;
            }

            if (total > Function.BusinessLimits.MAX_SALE_AMOUNT)
            {
                XtraMessageBox.Show(
                    $"Le montant total ({Function.MoneyHelper.Format(total)}) dépasse la limite autorisée ({Function.MoneyHelper.Format(Function.BusinessLimits.MAX_SALE_AMOUNT)}).",
                    "Montant trop élevé",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            txtTotal.Text = total.ToString("F2");

            return total;
        }

        public void ReferenceNo()
        {
            int lastId = Shared.db.Sales.Count() + 1;
            txtReferenceNo.Text = Function.Helper.generateRefNo("REF", lastId);
        }

        private void Pos_Load(object sender, EventArgs e)
        {
            //PosTopBanner
            //PosBottomBanner
            //PosCategoriesWithoutImgs
            //PosProductsWithoutImgs

            //PoslayoutControlGroupLatestOrders
            //PoslayoutControlGroupLatestCustomers
            //PoslayoutControlGroupLatestSuppliers
            //PoslayoutControlGroupSalesReturns
            //PoslayoutControlGroupHold
            //PoslayoutControlGroupUnpaidOrders

            if (!Properties.Settings.Default.PoslayoutControlGroupLatestOrders)
                layoutControlGroupLatestOrders.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (!Properties.Settings.Default.PoslayoutControlGroupLatestCustomers)
                layoutControlGroupLatestCustomers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (!Properties.Settings.Default.PoslayoutControlGroupLatestSuppliers)
                layoutControlGroupLatestSuppliers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (!Properties.Settings.Default.PoslayoutControlGroupSalesReturns)
                layoutControlGroupSalesReturns.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (!Properties.Settings.Default.PoslayoutControlGroupHold)
                layoutControlGroupHold.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (!Properties.Settings.Default.PoslayoutControlGroupUnpaidOrders)
                layoutControlGroupUnpaidOrders.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (
                !Properties.Settings.Default.PoslayoutControlGroupLatestOrders &&
                !Properties.Settings.Default.PoslayoutControlGroupLatestCustomers &&
                !Properties.Settings.Default.PoslayoutControlGroupLatestSuppliers &&
                !Properties.Settings.Default.PoslayoutControlGroupSalesReturns &&
                !Properties.Settings.Default.PoslayoutControlGroupHold &&
                !Properties.Settings.Default.PoslayoutControlGroupUnpaidOrders
                )
            {
                //layoutControlGroupQuickSale.TextVisible = false;
                //layoutControlGroupQuickSale.CaptionImageOptions.SvgImage = null; // Hide the icon
                //layoutControlGroupQuickSale.ShowInCustomizationForm = false;  // Hide the bar or caption altogether
                //layoutControlGroupQuickSale.GroupBordersVisible = false;  // Hide the entire border and caption
            }

            if (Properties.Settings.Default.PosTopBanner)
            {
                PosTopBanner.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                QuickActions.TextVisible = true;
                quickExit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItemQuickCustomer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                btn14.ImageOptions.Location = ImageLocation.MiddleCenter;
                btn15.ImageOptions.Location = ImageLocation.MiddleCenter;
                btn16.ImageOptions.Location = ImageLocation.MiddleCenter;
                btn17.ImageOptions.Location = ImageLocation.MiddleCenter;
                quickActionCloseForm.ImageOptions.Location = ImageLocation.MiddleCenter;
            }

            if (Properties.Settings.Default.PosBottomBanner)
            {
                PosBottomBanner.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            if (Properties.Settings.Default.PosCategoriesWithoutImgs)
            {
                //tileViewColImage.ImageOptions.Image = Properties.Resources.rightclick_money_bag;
            }

            if (!Properties.Settings.Default.PosCategories)
            {
                PosCategories.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                PosCategoriesNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            if (Properties.Settings.Default.PosProductsWithoutImgs)
            {
                //colImage.ImageOptions.SvgImage = Function.Helper.GetOptimizedImageBytes(Properties.Resources.box_package);
            }

            this.ReferenceNo();

            txtProductsCardSearch.Focus();

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("ProductId", typeof(int));
                dt.Columns.Add("UnitId", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("SaleQuantity", typeof(decimal));
                dt.Columns.Add("UnitCostBd", typeof(decimal));
                dt.Columns.Add("DiscountPercent", typeof(decimal));
                dt.Columns.Add("UnitCostBt", typeof(decimal));
                dt.Columns.Add("LineTotal", typeof(decimal));
                dt.Columns.Add("ProfitMargin", typeof(decimal));
                dt.Columns.Add("UnitSellingPrice", typeof(decimal));
            }

            if (this.type != "Add")
            {
                this.edit();
            }
            else
            {
                txtPaymentStatus.EditValue = "Pending";
                //txtDiscountType.EditValue = "None";
                txtSaleStatus.EditValue = "Received";
                txtSaleDate.EditValue = DateTime.Now;
            }

            this.BusinessLocations();
            this.Warehouses();
            this.Customers();
            this.PriceGroups();

            txtBussLocation.EditValue = Properties.Settings.Default.BusinessLocationId;

            this.warehouse();
            this.customer();

            this.currentPriceGroupId = 1008;

            this.getCategories(1);
            this.getProducts();

            this.GetCustomerSales();
            //this.getLatestSale();
            //this.getLatestCustomers();
            //this.getSaleReturns();
            //this.getLatestSaleHolds();
            //this.getUnpaidOrders();
        }

        public void warehouse()
        {
            using (var context = new AppDbContext())
            {
                Models.Warehouse warehouse = context.Warehouses.FirstOrDefault();

                if (warehouse != null)
                {
                    txtWarehouse.EditValue = warehouse.Id;
                    this.warehouse_id = warehouse.Id;
                }
            }
        }

        public void customer()
        {
            using (var context = new AppDbContext())
            {
                Models.Customer customer = context.Customers.FirstOrDefault();

                if (customer != null)
                    txtCustomer.EditValue = customer.Id;
            }
        }

        public async void BusinessLocations()
        {
            using (var context = new AppDbContext())
            {
                txtBussLocation.Properties.DataSource = await context.BusinessLocations.ToListAsync();
                txtBussLocation.Properties.DisplayMember = "Name"; // Set display member
                txtBussLocation.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public async void Warehouses()
        {
            using (var context = new AppDbContext())
            {
                txtWarehouse.Properties.DataSource = await context.Warehouses.ToListAsync();
                txtWarehouse.Properties.DisplayMember = "Name"; // Set display member
                txtWarehouse.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public async void Customers()
        {
            using (var context = new AppDbContext())
            {
                txtCustomer.Properties.DataSource = await context.Customers.ToListAsync();
                txtCustomer.Properties.DisplayMember = "FirstName"; // Set display member
                txtCustomer.Properties.ValueMember = "Id"; // Set value member
            }
        }

        public async void PriceGroups()
        {
            using (var context = new AppDbContext())
            {
                var priceGroups = await context.PriceGroups.ToListAsync();

                // Create a new list for the DataSource and add the manual entry
                var updatedPriceGroups = new List<PriceGroup>
                {
                    new PriceGroup { Id = 0, Name = "Normal" } // Add manual entry at the start
                };

                // Add the existing price groups from the database
                updatedPriceGroups.AddRange(priceGroups);

                // Set the modified list as the DataSource
                txtPriceGroup.Properties.DataSource = updatedPriceGroups;
                txtPriceGroup.Properties.DisplayMember = "Name"; // Set display member
                txtPriceGroup.Properties.ValueMember = "Id"; // Set value member

                txtPriceGroup.EditValue = 0;
            }
        }

        private void tabbedControlGroup_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            // Determine which tab was selected
            var selectedTab = e.Page;

            switch (selectedTab.Name)
            {
                case "layoutControlGroupLatestOrders":
                    this.getLatestSale();
                    break;

                case "layoutControlGroupLatestCustomers":
                    this.getLatestCustomers();
                    this.todayTotalProfit();
                    this.todayTotalNetProfitAndGross();
                    break;

                case "layoutControlGroupLatestSuppliers":
                    this.getLatestSuppliers();
                    this.todayTotalPurchases();
                    this.GetSupplierPurchases();
                    this.todayTotalPurchasesAndDue();
                    break;

                case "layoutControlGroupSalesReturns":
                    this.getSaleReturns();
                    break;

                case "layoutControlGroupHold":
                    this.getLatestSaleHolds();
                    break;

                case "layoutControlGroupUnpaidOrders":
                    this.getUnpaidOrders();
                    break;

                default:
                    // Handle cases where no known tab is selected, if necessary
                    break;
            }
        }

        public void getUnpaidOrders()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                uoPerPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Sales
                .Where(order => order.Due > 0)
                .Where(p => p.ReferenceNo.Contains(uoSearchTerm) ||
                        p.Customer.FullName.Contains(uoSearchTerm))
                .Count();
                uoTotalPages = (int)Math.Ceiling((double)totalItems / uoItemsPerPage);

                // Load the first page of data
                BindDataToGridUo(uoCurrentPage);
            }
        }

        private void BindDataToGridUo(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * uoItemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.Sales.Include(w => w.Warehouse).Include(c => c.Customer)
                    .Where(order => order.Due > 0)
                    .Where(p => p.ReferenceNo.Contains(uoSearchTerm) ||
                        p.Customer.FullName.Contains(uoSearchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(uoItemsPerPage)
                    .ToList();

                gridControlUnpaidOrders.DataSource = currentPageData;
                UpdatePageLabelUo();
                UpdateNavigationButtonsUo();
            }
        }

        private void UpdatePageLabelUo()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            uoCurrentPageLabel.Text = $"{page} {uoCurrentPage} {of} {uoTotalPages}";
        }

        private void UpdateNavigationButtonsUo()
        {
            uoNavPrevPage.Enabled = uoCurrentPage > 1;
            uoNavFirstPage.Enabled = uoCurrentPage > 1;
            uoNavNextPage.Enabled = uoCurrentPage < uoTotalPages;
            uoNavLastPage.Enabled = uoCurrentPage < uoTotalPages;
        }

        public void getLatestSale()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                loPerPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Sales
                .Where(p => p.ReferenceNo.Contains(loSearchTerm) ||
                        p.Customer.FullName.Contains(loSearchTerm))
                .Count();
                loTotalPages = (int)Math.Ceiling((double)totalItems / loItemsPerPage);

                // Load the first page of data
                BindDataToGridLo(loCurrentPage);
            }
        }

        private void BindDataToGridLo(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * loItemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.Sales
                    .Where(p => p.ReferenceNo.Contains(loSearchTerm) ||
                        p.Customer.FullName.Contains(loSearchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(loItemsPerPage)
                    .ToList();

                gridControlLatestOrders.DataSource = currentPageData;
                UpdatePageLabelLo();
                UpdateNavigationButtonsLo();
            }
        }

        private void UpdatePageLabelLo()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            loCurrentPageLabel.Text = $"{page} {loCurrentPage} {of} {loTotalPages}";
        }

        private void UpdateNavigationButtonsLo()
        {
            loNavPrevPage.Enabled = loCurrentPage > 1;
            loNavFirstPage.Enabled = loCurrentPage > 1;
            loNavNextPage.Enabled = loCurrentPage < loTotalPages;
            loNavLastPage.Enabled = loCurrentPage < loTotalPages;
        }

        public void getLatestSuppliers()
        {
            using (var context = new AppDbContext())
            {
                var today = DateTime.Today;

                // Fetch data for suppliers who made purchases today
                var data = context.Suppliers
                    .OrderByDescending(p => p.Id)
                    .GroupJoin(
                        context.Purchases
                            .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0), // Filter purchases by today's date
                        supplier => supplier.Id,
                        purchase => purchase.SupplierId,
                        (supplier, purchases) => new
                        {
                            Id = supplier.Id,
                            FullName = supplier.FirstName + " " + supplier.LastName,
                            Total = purchases.Sum(p => (decimal?)p.PaidAmount) ?? 0,  // Total paid by the supplier today
                            CurrentDue = purchases.Sum(p => (decimal?)p.Due) ?? 0     // Current due by the supplier today
                        })
                    .ToList();

                // Bind the data to the grid
                gridControlLatestSuppliers.DataSource = data;
            }
        }

        public void getLatestCustomers()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                lcPerPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Customers
                .Where(p => p.FirstName.Contains(lcSearchTerm) ||
                            p.LastName.Contains(lcSearchTerm) ||
                            p.FullName.Contains(lcSearchTerm) ||
                            p.Email.Contains(lcSearchTerm))
                .Count();

                lcTotalPages = (int)Math.Ceiling((double)totalItems / lcItemsPerPage);

                // Load the first page of data
                BindDataToGridLc(lcCurrentPage);
            }
        }

        private void BindDataToGridLc(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * lcItemsPerPage;
                var today = DateTime.Today;

                // Fetch data for customers who have placed orders today
                var currentPageData = context.Customers
                    .Where(p => p.FirstName.Contains(lcSearchTerm) ||
                                p.LastName.Contains(lcSearchTerm) ||
                                p.FullName.Contains(lcSearchTerm) ||
                                p.Email.Contains(lcSearchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(lcItemsPerPage)
                    .GroupJoin(
                        context.Sales
                            .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0), // Filter sales by today's date
                        customer => customer.Id,
                        sale => sale.CustomerId,
                        (customer, sales) => new
                        {
                            CustomerId = customer.Id,
                            FullName = customer.FullName,
                            Total = sales.Sum(s => s.PaidAmount) ?? 0,  // Total paid by the customer today
                            CurrentDue = customer.CurrentDue           // Total due by the customer
                        })
                    .Where(c => c.Total > 0) // Only include customers with orders today
                    .ToList();

                // Bind the data to the grid
                gridControlLatestCustomers.DataSource = currentPageData;

                // Update pagination
                UpdatePageLabelLc();
                UpdateNavigationButtonsLc();
            }
        }

        private void UpdatePageLabelLc()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            lcCurrentPageLabel.Text = $"{page} {lcCurrentPage} {of} {lcTotalPages}";
        }

        private void UpdateNavigationButtonsLc()
        {
            lcNavPrevPage.Enabled = lcCurrentPage > 1;
            lcNavFirstPage.Enabled = lcCurrentPage > 1;
            lcNavNextPage.Enabled = lcCurrentPage < lcTotalPages;
            lcNavLastPage.Enabled = lcCurrentPage < lcTotalPages;
        }

        public void getLatestSaleHolds()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                hdPerPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.SaleHolds
                .Where(p => p.ReferenceNo.Contains(hdSearchTerm) ||
                        p.Customer.FullName.Contains(hdSearchTerm))
                .Count();
                hdTotalPages = (int)Math.Ceiling((double)totalItems / hdItemsPerPage);

                // Load the first page of data
                BindDataToGridHd(hdCurrentPage);
            }
        }

        private void BindDataToGridHd(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * hdItemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.SaleHolds
                    .Where(p => p.ReferenceNo.Contains(hdSearchTerm) ||
                        p.Customer.FullName.Contains(hdSearchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(hdItemsPerPage)
                    .ToList();

                gridControlHoldOrders.DataSource = currentPageData;
                UpdatePageLabelHd();
                UpdateNavigationButtonsHd();
            }
        }

        private void UpdatePageLabelHd()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            hdCurrentPageLabel.Text = $"{page} {hdCurrentPage} {of} {hdTotalPages}";
        }

        private void UpdateNavigationButtonsHd()
        {
            hdNavPrevPage.Enabled = hdCurrentPage > 1;
            hdNavFirstPage.Enabled = hdCurrentPage > 1;
            hdNavNextPage.Enabled = hdCurrentPage < hdTotalPages;
            hdNavLastPage.Enabled = hdCurrentPage < hdTotalPages;
        }

        public void getSaleReturns()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                srPerPage.SelectedIndex = 2;

                // Calculate the total number of pages
                int totalItems = context.Returns
                .Where(p => p.ReferenceNo.Contains(srSearchTerm) ||
                        p.Customer.FullName.Contains(srSearchTerm))
                .Count();
                srTotalPages = (int)Math.Ceiling((double)totalItems / srItemsPerPage);

                // Load the first page of data
                BindDataToGridSr(srCurrentPage);
            }
        }

        private void BindDataToGridSr(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * srItemsPerPage;

                // Fetch the data for the current page from the database
                var currentPageData = context.Returns
                    .Where(p => p.ReferenceNo.Contains(srSearchTerm) ||
                        p.Customer.FullName.Contains(srSearchTerm))
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(srItemsPerPage)
                    .ToList();

                gridControlLatestSaleReturns.DataSource = currentPageData;
                UpdatePageLabelSr();
                UpdateNavigationButtonsSr();
            }
        }

        private void UpdatePageLabelSr()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            srCurrentPageLabel.Text = $"{page} {srCurrentPage} {of} {srTotalPages}";
        }

        private void UpdateNavigationButtonsSr()
        {
            srNavPrevPage.Enabled = srCurrentPage > 1;
            srNavFirstPage.Enabled = srCurrentPage > 1;
            srNavNextPage.Enabled = srCurrentPage < srTotalPages;
            srNavLastPage.Enabled = srCurrentPage < srTotalPages;
        }

        public async void getProducts()
        {
            using (var context = new AppDbContext())
            {
                // Set default items per page
                perPage.SelectedIndex = 2;
                //MessageBox.Show(currentPriceGroupId.ToString());

                // Common query to fetch product information with business location and warehouse filtering
                var query = from p in context.Products
                            join pw in context.ProductWarehouses on p.Id equals pw.ProductId into pwJoined
                            from pwj in pwJoined.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.ProductName,
                                p.Thumbnail,
                                Qty = (int?)pwj.Qty ?? 0,
                                WarehouseId = (int?)pwj.WarehouseId,
                                Price = currentPriceGroupId != 0
                                    ? (decimal?)context.ProductPrices
                                           .Where(pp => pp.ProductId == p.Id && pp.PriceGroupId == this.currentPriceGroupId)
                                           .Select(pp => pp.Price)
                                           .FirstOrDefault()
                                    : (decimal?)p.SellingPrice ?? 0
                            };

                // Apply warehouse_id filter
                if (this.warehouse_id != 0)
                {
                    query = query.Where(p => p.WarehouseId == this.warehouse_id);
                }

                // Apply search filter for product name
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => p.ProductName.Contains(searchTerm));
                }

                // Fetch the product list
                var productList = await query
                    .OrderByDescending(p => p.Id)
                    .ToListAsync();

                // Calculate the total number of pages
                int totalItems = productList.Count();
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // Load the first page of data
                BindDataToGrid(currentPage);
            }
        }

        private void BindDataToGrid(int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                Sound.Selected();

                int startRecord = (pageNumber - 1) * itemsPerPage;

                var query = from p in context.Products
                            join pw in context.ProductWarehouses on p.Id equals pw.ProductId into pwJoined
                            from pwj in pwJoined.DefaultIfEmpty()
                            group new { p, pwj } by new
                            {
                                p.Id,
                                // Use only supported data types in grouping, avoid `text`, `ntext`, or `image` types
                                // Use CAST or CONVERT to work with data type issues if necessary
                                ProductName = p.ProductName, // Ensure this is not a `ntext` or `text`
                                Thumbnail = p.Thumbnail // Ensure this is not an `image` type, otherwise exclude or convert it
                            } into g
                            select new
                            {
                                g.Key.Id,
                                g.Key.ProductName,
                                // You can consider converting or removing unsupported types like image, if needed
                                Thumbnail = g.Key.Thumbnail,
                                Qty = g.Sum(x => (int?)x.pwj.Qty ?? 0),
                                WarehouseId = (int?)null,
                                Price = currentPriceGroupId != 0
                                    ? (decimal?)context.ProductPrices
                                        .Where(pp => pp.ProductId == g.Key.Id && pp.PriceGroupId == this.currentPriceGroupId)
                                        .Select(pp => pp.Price)
                                        .FirstOrDefault()
                                    : (decimal?)g.Max(x => x.p.SellingPrice) ?? 0
                            };

                // Apply warehouse_id filter after grouping
                if (this.warehouse_id != 0)
                {
                    query = query.Where(p => context.ProductWarehouses.Any(pw => pw.ProductId == p.Id && pw.WarehouseId == this.warehouse_id));
                }

                // Apply search filter for product name
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(p => p.ProductName.Contains(searchTerm));
                }

                var currentPageData = query
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToList();

                gridControlProductsCards.DataSource = currentPageData;
                UpdatePageLabel();
                UpdateNavigationButtons();
            }
        }

        private void UpdatePageLabel()
        {
            string lang = Properties.Settings.Default.Lang;

            string page;
            string of;

            if (lang == "en")
            {
                page = "Page";
                of = "of";
            }
            else if (lang == "fr")
            {
                page = "Page";
                of = "de";
            }
            else
            {
                page = "صفحة";
                of = "من";
            }

            currentPageLabel.Text = $"{page} {currentPage} {of} {totalPages}";
        }

        private void UpdateNavigationButtons()
        {
            NavPrevPage.Enabled = currentPage > 1;
            NavFirstPage.Enabled = currentPage > 1;
            NavNextPage.Enabled = currentPage < totalPages;
            NavLastPage.Enabled = currentPage < totalPages;
        }

        private void txtSaleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtSaleType.SelectedIndex == 0)
            {
                saleType = "Carry in";
            }
            else if (txtSaleType.SelectedIndex == 1)
            {
                saleType = "Pick up";
            }
            else
            {
                saleType = "On site";
            }

            Function.Sound.Selected();
        }


        private void repDeleteItem_Click(object sender, EventArgs e)
        {
            int focusedRowHandle = gridViewProducts.FocusedRowHandle;
            object productId = gridViewProducts.GetRowCellValue(focusedRowHandle, "ProductId");
            object detailsId = gridViewProducts.GetRowCellValue(focusedRowHandle, "Id");

            if (XtraMessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.type == "Add")
                {
                    DeleteItemFromDataTable(focusedRowHandle);
                }
                else
                {
                    DeleteItemFromDatabase(detailsId, focusedRowHandle);
                }
                RefreshTotalAmount();
                Function.Sound.Deleted();
            }

            this.CalculateTotalBenefit();
        }

        private void DeleteItemFromDataTable(int rowIndex)
        {
            if (dt.Rows.Count > 0)
            {
                dt.Rows.RemoveAt(rowIndex);
                dt.AcceptChanges();
                gridViewProducts.RefreshData();
            }
        }

        private void DeleteItemFromDatabase(object detailsId, int rowIndex)
        {
            try
            {
                Models.SaleDetail saleDetail = Shared.db.SaleDetails.Find(Convert.ToInt32(detailsId));
                if (saleDetail != null)
                {
                    Shared.db.SaleDetails.Remove(saleDetail);
                    Shared.db.SaveChanges();
                    gridViewProducts.DeleteRow(rowIndex);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error while deleting item: " + ex.Message);
            }
        }

        private void RefreshTotalAmount()
        {
            txtNetTotalAmount.Text = calculeTotal().ToString();
        }

        public int getIndex(string value)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == value)
                    return i;
            }
            return -1;
        }

        private void btnMtDataReset_Click(object sender, EventArgs e)
        {
            Sound.Wrong();
            txtNetTotalAmount.Text = "00000000.00 DA";
            txtTotal.EditValue = 0;
            txtPaidAmount.EditValue = 0;
            txtTotalTax.EditValue = 0;
            txtTotalDiscount.EditValue = 0;
            txtDue.EditValue = 0;
            txtReturnAmount.EditValue = 0;

            dt = new DataTable();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("ProductId", typeof(int));
            dt.Columns.Add("UnitId", typeof(int));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("SaleQuantity", typeof(decimal));
            dt.Columns.Add("UnitCostBd", typeof(decimal));
            dt.Columns.Add("DiscountPercent", typeof(decimal));
            dt.Columns.Add("UnitCostBt", typeof(decimal));
            dt.Columns.Add("LineTotal", typeof(decimal));
            dt.Columns.Add("ProfitMargin", typeof(decimal));
            dt.Columns.Add("UnitSellingPrice", typeof(decimal));

            gridControlProducts.DataSource = dt;
        }

        private void gridViewProducts_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                // Calculate the index from the view to the data source
                int rowIndex = gridViewProducts.GetDataSourceRowIndex(e.RowHandle);
                int productId = int.Parse(gridViewProducts.GetRowCellValue(rowIndex, "ProductId").ToString());
                int warehouseId = this.warehouse_id;  // Assuming warehouse_id is globally or previously set
                decimal newQuantity = decimal.Parse(dt.Rows[rowIndex]["SaleQuantity"].ToString());

                // Perform stock availability check asynchronously and update UI accordingly
                if (!CheckProductQuantityAsync(productId, warehouseId, newQuantity))
                {
                    // If not enough stock, update on UI thread
                    Sound.Wrong();
                    dt.Rows[rowIndex]["SaleQuantity"] = GetAvailableQuantityAsync(productId, warehouseId);
                    gridViewProducts.RefreshRow(rowIndex);

                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.OutOfStock outOfStock = new Alert.OutOfStock();
                    outOfStock.FormClosed += (s, args) => overlay.Close();
                    outOfStock.Show();
                    outOfStock.TopMost = true;
                }
                else
                {
                    // Update sale details directly if enough stock is available
                    decimal saleQuantity = Convert.ToDecimal(dt.Rows[rowIndex]["SaleQuantity"]);
                    decimal discountPercent = Convert.ToDecimal(dt.Rows[rowIndex]["DiscountPercent"]);

                    // Update based on the column that triggered the event
                    if (e.Column.FieldName == "Discount Percent")
                        discountPercent = Convert.ToDecimal(e.Value);

                    if (e.Column.FieldName == "Quantity")
                        saleQuantity = Convert.ToDecimal(e.Value);

                    decimal unitPrice = Convert.ToDecimal(dt.Rows[rowIndex]["UnitSellingPrice"]);
                    decimal lineTotal = saleQuantity * unitPrice * (1 - discountPercent / 100);
                    dt.Rows[rowIndex].SetField("LineTotal", lineTotal);



                    Invoke(new Action(() =>
                    {
                        txtNetTotalAmount.Text = this.calculeTotal().ToString();
                        Sound.Selected();
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void gridViewProducts_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            // Attempt to retrieve the value safely
            var idValue = gridViewProducts.GetRowCellValue(gridViewProducts.FocusedRowHandle, "ProductId");

            // Check if the value is not null and can be parsed into an integer
            if (idValue != null && int.TryParse(idValue.ToString(), out int ProductId))
            {

                bool IsQuantityPopUp = false;
                using (AppDbContext AppDb = new AppDbContext())
                {
                    Models.Setting setting = AppDb.Settings.FirstOrDefault();
                    IsQuantityPopUp = setting.IsQuantityPopUp ?? false;
                }

                if (ProductId != 0)
                {
                    this.productId = ProductId;

                    if (!CheckProductQuantityAsync(this.productId, this.warehouse_id, 1))
                    {
                        Sound.Wrong();

                        OverlayForm overlay = new OverlayForm(this);
                        overlay.Show();

                        Alert.OutOfStock outOfStock = new Alert.OutOfStock();
                        outOfStock.FormClosed += (s, args) => overlay.Close();
                        outOfStock.Show();
                        outOfStock.TopMost = true;
                    }
                    else
                    {
                        DataRow existingRow = dt.Rows
                                   .Cast<DataRow>()
                                   .FirstOrDefault(r => r["ProductId"].Equals(productId));
                        //if (type == "Add")
                        //{
                        if (ProductId != 0)
                        {
                            // Check if the product already exists in the DataTable
                            //if (existingRow != null)
                            if (IsQuantityPopUp == true)
                            {
                                this.currentExistingRow = existingRow;

                                OverlayForm overlay = new OverlayForm(this);
                                overlay.Show();
                                UpdateQty updateQty = new UpdateQty(productId, int.Parse(txtCustomer.EditValue.ToString()));
                                updateQty.FormClosed += (s, args) => overlay.Close();
                                updateQty.setObject(this);
                                updateQty.setQty(decimal.Parse(existingRow["SaleQuantity"].ToString()));
                                updateQty.Show();
                                updateQty.TopMost = true;

                                //// Product already exists, just update the quantity
                                //int currentQty = Convert.ToInt32(existingRow["SaleQuantity"]);
                                //existingRow["SaleQuantity"] = currentQty + 1;

                                //// Recalculate the line total based on the new quantity
                                //decimal unitPrice = Convert.ToDecimal(existingRow["UnitSellingPrice"]);
                                //existingRow["LineTotal"] = unitPrice * (currentQty + 1);
                            }
                            else
                            {
                                if (existingRow != null)
                                {
                                    existingRow["SaleQuantity"] = Convert.ToInt32(existingRow["SaleQuantity"]) + 1;// Exemple pour mettre à jour la quantité
                                    existingRow["LineTotal"] = Convert.ToInt32(existingRow["SaleQuantity"]) * Convert.ToInt32(existingRow["UnitCostBt"]); // Recalculer le total de la ligne
                                }
                                else
                                {
                                    // Product does not exist, add a new row
                                    Models.Product product = Shared.db.Products.Find(productId);
                                    DataRow newRow = dt.NewRow();
                                    newRow["ProductId"] = productId;
                                    newRow["UnitId"] = product.UnitId;
                                    newRow["ProductName"] = product.ProductName;
                                    newRow["SaleQuantity"] = 1;
                                    newRow["UnitCostBd"] = product.PurchasePriceExcTax;
                                    newRow["DiscountPercent"] = 0;  // Assuming no discount initially
                                    newRow["UnitCostBt"] = product.PurchasePriceExcTax;
                                    newRow["LineTotal"] = Convert.ToDecimal(newRow["SaleQuantity"]) * product.SellingPrice;
                                    newRow["ProfitMargin"] = product.Xmargin;
                                    newRow["UnitSellingPrice"] = product.SellingPrice;
                                    dt.Rows.Add(newRow);
                                }

                            }

                            // Refresh the grid to show the updated data
                            gridControlProducts.DataSource = dt;
                            //}

                            //if (id != null)
                            //{
                            //    int index = dt.Rows.Count == 0 ? -1 : this.getIndex(productId.ToString());
                            //    Models.Product product = Shared.db.Products.Find(productId);

                            //    if (index == -1)
                            //    {
                            //        DataRow NewRow = dt.NewRow();
                            //        NewRow["ProductId"] = productId;
                            //        NewRow["ProductName"] = product.ProductName;
                            //        NewRow["SaleQuantity"] = 1;
                            //        NewRow["UnitCostBd"] = product.PurchasePriceExcTax;
                            //        NewRow["DiscountPercent"] = 0;
                            //        NewRow["UnitCostBt"] = product.PurchasePriceExcTax;
                            //        NewRow["LineTotal"] = Convert.ToDecimal(NewRow["SaleQuantity"]) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                            //        NewRow["ProfitMargin"] = product.Xmargin;
                            //        NewRow["UnitSellingPrice"] = product.SellingPrice;

                            //        dt.Rows.Add(NewRow);
                            //    }
                            //    else
                            //    {
                            //        int qty = int.Parse(dt.Rows[index]["SaleQuantity"].ToString());

                            //        dt.Rows[index].SetField("SaleQuantity", qty + 1);
                            //        dt.AcceptChanges();
                            //    }

                            //    gridControlProducts.DataSource = null;
                            //    gridControlProducts.DataSource = dt;
                            //}
                        }
                        //else
                        //{
                        //    if (IsQuantityPopUp == true)
                        //    {
                        //        this.currentExistingRow = existingRow;

                        //        OverlayForm overlay = new OverlayForm(this);
                        //        overlay.Show();
                        //        UpdateQty updateQty = new UpdateQty(productId, int.Parse(txtCustomer.EditValue.ToString()));
                        //        updateQty.FormClosed += (s, args) => overlay.Close();
                        //        updateQty.setObject(this);

                        //        updateQty.Show();
                        //        updateQty.TopMost = true;
                        //    }
                        //    else
                        //    {

                        //    }
                        //        //object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

                        //        //Models.Product product = Shared.db.Products.Find(productId);

                        //        //Models.SaleDetail saleDetail = Shared.db.SaleDetails.Find(int.Parse(DetailsId.ToString()));

                        //        //if (saleDetail != null)
                        //        //{
                        //        //    saleDetail.SaleQuantity = saleDetail.SaleQuantity + 1;
                        //        //    saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - saleDetail.DiscountPercent / 100);

                        //        //    Shared.db.Entry(saleDetail).State = EntityState.Modified;

                        //        //    Shared.db.SaveChanges();
                        //        //}
                        //        //else
                        //        //{
                        //        //    saleDetail.ProductId = productId;
                        //        //    saleDetail.SaleId = this.sale_id;
                        //        //    saleDetail.ProductName = txtProducts.Text;
                        //        //    saleDetail.SaleQuantity = 1;
                        //        //    saleDetail.UnitCostBd = product.PurchasePriceExcTax;
                        //        //    saleDetail.DiscountPercent = 0;
                        //        //    saleDetail.UnitCostBt = product.PurchasePriceExcTax;
                        //        //    saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                        //        //    saleDetail.ProfitMargin = product.Xmargin;
                        //        //    saleDetail.UnitSellingPrice = product.SellingPrice;

                        //        //    Shared.db.SaleDetails.Add(saleDetail);

                        //        //    Shared.db.SaveChanges();
                        //        //}

                        //        this.changeNumberItems(this.sale_id);

                        //    this.getSaleItems(this.sale_id);
                        //}

                        txtNetTotalAmount.Text = this.calculeTotal().ToString();

                        //this.getProducts();

                        Function.Sound.Added();

                    }

                }
            }
            else
            {
                // Handle the case where the value is null or cannot be parsed (optional)
                MessageBox.Show("Unable to retrieve a valid row Id.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public decimal getPaidAmount()
        {
            return decimal.Parse(txtPaidAmount.EditValue.ToString());
        }

        public void updatePaidAmoutByPay(decimal amount)
        {
            txtPaidAmount.EditValue = amount;
        }

        private void txtPaidAmount_EditValueChanged(object sender, EventArgs e)
        {
            // Ensure all necessary fields are properly formatted as decimals, handle incorrect inputs
            if (!decimal.TryParse(txtPaidAmount.EditValue?.ToString(), out decimal paidAmount))
            {
                XtraMessageBox.Show("Please enter a valid numeric value for the paid amount.");
                txtPaidAmount.EditValue = 0;  // Reset the value or handle as necessary
                return;  // Exit the function if input is not valid
            }

            decimal total = 0, discount = 0, tax = 0;

            // Safe parsing for Total, Discount, and Tax fields
            decimal.TryParse(txtTotal.EditValue?.ToString(), out total);
            decimal.TryParse(txtTotalDiscount.EditValue?.ToString(), out discount);
            decimal.TryParse(txtTotalTax.EditValue?.ToString(), out tax);

            // Calculate the net amount after discount and tax but before any payments
            decimal netAmount = total - discount + tax;

            // Calculate the due amount and return amount
            try
            {
                decimal dueAmount = netAmount - paidAmount;
                decimal returnAmount = 0;

                // Ensure the due amount is never negative; calculate return amount if payment exceeds the net amount
                if (dueAmount < 0)
                {
                    returnAmount = -dueAmount;  // Positive value of dueAmount, which is the excess payment
                    dueAmount = 0;  // Due amount is zero since the paid amount covers the net amount
                }

                // Update the interface fields
                txtDue.Text = dueAmount.ToString("F2");  // Format for two decimal places
                txtReturnAmount.EditValue = returnAmount.ToString("F2");  // Update the return amount field with formatted value

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error calculating financial details: " + ex.Message);
                // Optionally log the error or handle further
            }
        }

        public void updateQtyByModal(DataRow existingRow, decimal newQty, int ProductId, decimal SellingPrice)
        {
            int givenProductId = ProductId; // Le productId que vous voulez vérifier
            bool productExists = false;

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt32(row["ProductId"]) == givenProductId)
                {
                    productExists = true;
                    break;
                }
            }

            if (productExists == false)
            {
                Models.Product product = Shared.db.Products.Find(givenProductId);
                DataRow newRow = dt.NewRow();
                newRow["ProductId"] = givenProductId;
                newRow["UnitId"] = product.UnitId;
                newRow["ProductName"] = product.ProductName;
                newRow["SaleQuantity"] = newQty;
                newRow["UnitCostBd"] = product.PurchasePriceExcTax;
                newRow["DiscountPercent"] = 0;  // Assuming no discount initially
                newRow["UnitCostBt"] = product.PurchasePriceExcTax;
                //newRow["LineTotal"] = Convert.ToDecimal(newRow["SaleQuantity"]) * product.SellingPrice;
                newRow["LineTotal"] = Convert.ToDecimal(newRow["SaleQuantity"]) * SellingPrice;
                newRow["ProfitMargin"] = product.Xmargin;
                //newRow["UnitSellingPrice"] = product.SellingPrice;
                newRow["UnitSellingPrice"] = SellingPrice;
                dt.Rows.Add(newRow);
            }
            else
            {
                decimal currentQty = Convert.ToDecimal(existingRow["SaleQuantity"]);
                existingRow["SaleQuantity"] = newQty;

                // Recalculate the line total based on the new quantity
                decimal unitPrice = Convert.ToDecimal(existingRow["UnitSellingPrice"]);
                existingRow["LineTotal"] = unitPrice * (newQty);
            }
            // Product already exists, just update the quantity
            //int currentQty = Convert.ToInt32(existingRow["SaleQuantity"]);
            //existingRow["SaleQuantity"] = currentQty + newQty;

            //// Recalculate the line total based on the new quantity
            //decimal unitPrice = Convert.ToDecimal(existingRow["UnitSellingPrice"]);
            //existingRow["LineTotal"] = unitPrice * (currentQty + newQty);

            this.CalculateTotalBenefit();
            txtDue.Text = this.calculeTotal().ToString();
        }

        private void tileViewProductsCards_Click(object sender, EventArgs e)
        {
            bool IsQuantityPopUp = false;
            using (AppDbContext AppDb = new AppDbContext())
            {
                Models.Setting setting = AppDb.Settings.FirstOrDefault();
                IsQuantityPopUp = setting.IsQuantityPopUp ?? false;
            }
            object id = this.tileViewProductsCards.GetRowCellValue(this.tileViewProductsCards.FocusedRowHandle, "Id");

            if (id != null)
            {
                this.productId = int.Parse(id.ToString());

                if (!CheckProductQuantityAsync(this.productId, this.warehouse_id, 1))
                {
                    Sound.Wrong();

                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.OutOfStock outOfStock = new Alert.OutOfStock();
                    outOfStock.FormClosed += (s, args) => overlay.Close();
                    outOfStock.Show();
                    outOfStock.TopMost = true;
                }
                else
                {
                    DataRow existingRow = dt.Rows
                               .Cast<DataRow>()
                               .FirstOrDefault(r => r["ProductId"].Equals(productId));
                    //if (type == "Add")
                    //{
                    if (id != null)
                    {
                        // Check if the product already exists in the DataTable


                        //if (existingRow != null)
                        if (IsQuantityPopUp == true)
                        {
                            this.currentExistingRow = existingRow;

                            OverlayForm overlay = new OverlayForm(this);
                            overlay.Show();
                            UpdateQty updateQty = new UpdateQty(productId, int.Parse(txtCustomer.EditValue.ToString()));
                            updateQty.FormClosed += (s, args) => overlay.Close();
                            updateQty.setObject(this);

                            updateQty.Show();
                            updateQty.TopMost = true;
                            
                            txtDue.Text = calculeTotal().ToString();

                            //// Product already exists, just update the quantity
                            //int currentQty = Convert.ToInt32(existingRow["SaleQuantity"]);
                            //existingRow["SaleQuantity"] = currentQty + 1;

                            //// Recalculate the line total based on the new quantity
                            //decimal unitPrice = Convert.ToDecimal(existingRow["UnitSellingPrice"]);
                            //existingRow["LineTotal"] = unitPrice * (currentQty + 1);
                        }
                        else
                        {

                            if (existingRow != null)
                            {
                                existingRow["SaleQuantity"] = Convert.ToInt32(existingRow["SaleQuantity"]) + 1;// Exemple pour mettre à jour la quantité
                                existingRow["LineTotal"] = Convert.ToInt32(existingRow["SaleQuantity"]) * Convert.ToInt32(existingRow["UnitCostBt"]); // Recalculer le total de la ligne
                            }
                            else
                            {
                                // Product does not exist, add a new row
                                Models.Product product = Shared.db.Products.Find(productId);
                                DataRow newRow = dt.NewRow();
                                newRow["ProductId"] = productId;
                                newRow["UnitId"] = product.UnitId;
                                newRow["ProductName"] = product.ProductName;
                                newRow["SaleQuantity"] = 1;
                                newRow["UnitCostBd"] = product.PurchasePriceExcTax;
                                newRow["DiscountPercent"] = 0;  // Assuming no discount initially
                                newRow["UnitCostBt"] = product.PurchasePriceExcTax;
                                newRow["LineTotal"] = Convert.ToDecimal(newRow["SaleQuantity"]) * product.SellingPrice;
                                newRow["ProfitMargin"] = product.Xmargin;
                                newRow["UnitSellingPrice"] = product.SellingPrice;
                                dt.Rows.Add(newRow);
                            }

                        }

                        // Refresh the grid to show the updated data
                        gridControlProducts.DataSource = dt;
                        //}

                        //if (id != null)
                        //{
                        //    int index = dt.Rows.Count == 0 ? -1 : this.getIndex(productId.ToString());
                        //    Models.Product product = Shared.db.Products.Find(productId);

                        //    if (index == -1)
                        //    {
                        //        DataRow NewRow = dt.NewRow();
                        //        NewRow["ProductId"] = productId;
                        //        NewRow["ProductName"] = product.ProductName;
                        //        NewRow["SaleQuantity"] = 1;
                        //        NewRow["UnitCostBd"] = product.PurchasePriceExcTax;
                        //        NewRow["DiscountPercent"] = 0;
                        //        NewRow["UnitCostBt"] = product.PurchasePriceExcTax;
                        //        NewRow["LineTotal"] = Convert.ToDecimal(NewRow["SaleQuantity"]) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                        //        NewRow["ProfitMargin"] = product.Xmargin;
                        //        NewRow["UnitSellingPrice"] = product.SellingPrice;

                        //        dt.Rows.Add(NewRow);
                        //    }
                        //    else
                        //    {
                        //        int qty = int.Parse(dt.Rows[index]["SaleQuantity"].ToString());

                        //        dt.Rows[index].SetField("SaleQuantity", qty + 1);
                        //        dt.AcceptChanges();
                        //    }

                        //    gridControlProducts.DataSource = null;
                        //    gridControlProducts.DataSource = dt;
                        //}
                    }
                    //else
                    //{
                    //    if (IsQuantityPopUp == true)
                    //    {
                    //        this.currentExistingRow = existingRow;

                    //        OverlayForm overlay = new OverlayForm(this);
                    //        overlay.Show();
                    //        UpdateQty updateQty = new UpdateQty(productId, int.Parse(txtCustomer.EditValue.ToString()));
                    //        updateQty.FormClosed += (s, args) => overlay.Close();
                    //        updateQty.setObject(this);

                    //        updateQty.Show();
                    //        updateQty.TopMost = true;
                    //    }
                    //    else
                    //    {

                    //    }
                    //        //object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

                    //        //Models.Product product = Shared.db.Products.Find(productId);

                    //        //Models.SaleDetail saleDetail = Shared.db.SaleDetails.Find(int.Parse(DetailsId.ToString()));

                    //        //if (saleDetail != null)
                    //        //{
                    //        //    saleDetail.SaleQuantity = saleDetail.SaleQuantity + 1;
                    //        //    saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - saleDetail.DiscountPercent / 100);

                    //        //    Shared.db.Entry(saleDetail).State = EntityState.Modified;

                    //        //    Shared.db.SaveChanges();
                    //        //}
                    //        //else
                    //        //{
                    //        //    saleDetail.ProductId = productId;
                    //        //    saleDetail.SaleId = this.sale_id;
                    //        //    saleDetail.ProductName = txtProducts.Text;
                    //        //    saleDetail.SaleQuantity = 1;
                    //        //    saleDetail.UnitCostBd = product.PurchasePriceExcTax;
                    //        //    saleDetail.DiscountPercent = 0;
                    //        //    saleDetail.UnitCostBt = product.PurchasePriceExcTax;
                    //        //    saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                    //        //    saleDetail.ProfitMargin = product.Xmargin;
                    //        //    saleDetail.UnitSellingPrice = product.SellingPrice;

                    //        //    Shared.db.SaleDetails.Add(saleDetail);

                    //        //    Shared.db.SaveChanges();
                    //        //}

                    //        this.changeNumberItems(this.sale_id);

                    //    this.getSaleItems(this.sale_id);
                    //}

                    txtNetTotalAmount.Text = this.calculeTotal().ToString();

                    //this.getProducts();

                    Function.Sound.Added();

                }

            }

            txtDue.Text = this.calculeTotal().ToString();
        }

        public void SetTotalDueAmount()
        {
            txtDue.Text = this.calculeTotal().ToString();
        }

        public void SetNetTotalAmount()
        {
            txtNetTotalAmount.Text = this.calculeTotal().ToString();
        }
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            //gridControlProductsCards.DataSource = Shared.db.Products.OrderByDescending(p => p.Id).ToList();
            getProducts();
        }

        private void txtTotalDiscount_EditValueChanged(object sender, EventArgs e)
        {
            // SÉCURISÉ: Validation stricte des remises (prévient montants négatifs)

            // VALIDATION 1: Valeur numérique valide
            if (!decimal.TryParse(txtTotalDiscount.EditValue?.ToString(), out decimal totalDiscount))
            {
                XtraMessageBox.Show("Please enter a valid numeric value for the total discount.");
                txtTotalDiscount.EditValue = 0;
                return;
            }

            // VALIDATION 2: Pas de valeur négative
            if (totalDiscount < 0)
            {
                XtraMessageBox.Show("Discount cannot be negative.");
                txtTotalDiscount.EditValue = 0;
                return;
            }

            // Récupérer les autres montants
            decimal.TryParse(txtTotal.EditValue?.ToString(), out decimal total);
            decimal.TryParse(txtTotalTax.EditValue?.ToString(), out decimal tax);
            decimal.TryParse(txtPaidAmount.EditValue?.ToString(), out decimal paidAmount);

            // VALIDATION 3: La remise ne peut PAS dépasser le total (CRITIQUE)
            if (totalDiscount > total)
            {
                string message = $"La remise ({Function.MoneyHelper.Format(totalDiscount)}) ne peut pas dépasser le total ({Function.MoneyHelper.Format(total)}).";
                XtraMessageBox.Show(message, "Remise invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Plafonner la remise au total maximum
                txtTotalDiscount.EditValue = total;
                totalDiscount = total;
            }

            // Calcul avec arrondi cohérent
            decimal netAmount = Function.MoneyHelper.Round((total - totalDiscount) + tax);

            // VALIDATION 4: Le montant net ne peut JAMAIS être négatif
            if (netAmount < 0)
            {
                XtraMessageBox.Show("Le montant net ne peut pas être négatif. La remise a été ajustée.", "Erreur de calcul");
                txtTotalDiscount.EditValue = Function.MoneyHelper.Round(total - tax);
                netAmount = 0;
            }

            // Calculer montant dû et rendu
            decimal dueAmount = Function.MoneyHelper.Round(netAmount - paidAmount);
            decimal returnAmount = 0;

            if (dueAmount < 0)
            {
                returnAmount = -dueAmount;
                dueAmount = 0;
            }

            // Mise à jour UI
            txtNetTotalAmount.Text = netAmount.ToString("F2");
            txtDue.Text = dueAmount.ToString("F2");
            txtReturnAmount.EditValue = returnAmount.ToString("F2");

            RecalculateAndRefreshFields();
        }

        private void txtTotalTax_EditValueChanged(object sender, EventArgs e)
        {
            // SÉCURISÉ: Validation stricte des taxes

            // VALIDATION 1: Valeur numérique valide
            if (!decimal.TryParse(txtTotalTax.EditValue?.ToString(), out decimal totalTax))
            {
                XtraMessageBox.Show("Please enter a valid numeric value for the tax.");
                txtTotalTax.EditValue = 0;
                return;
            }

            // VALIDATION 2: Pas de valeur négative
            if (totalTax < 0)
            {
                XtraMessageBox.Show("Tax cannot be negative.");
                txtTotalTax.EditValue = 0;
                return;
            }

            // VALIDATION 3: Vérifier que la taxe n'est pas absurde (> 50% du total)
            decimal.TryParse(txtTotal.EditValue?.ToString(), out decimal total);
            decimal maxReasonableTax = Function.MoneyHelper.Multiply(total, Function.BusinessLimits.MAX_TAX_RATE / 100m);

            if (totalTax > maxReasonableTax)
            {
                string message = $"La taxe ({Function.MoneyHelper.Format(totalTax)}) semble excessive (> {Function.BusinessLimits.MAX_TAX_RATE}% du total). Veuillez vérifier.";
                var result = XtraMessageBox.Show(message, "Taxe élevée", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Cancel)
                {
                    txtTotalTax.EditValue = 0;
                    return;
                }
            }

            // Récupérer les autres montants
            decimal.TryParse(txtTotalDiscount.EditValue?.ToString(), out decimal totalDiscount);
            decimal.TryParse(txtPaidAmount.EditValue?.ToString(), out decimal paidAmount);

            // Calcul avec arrondi cohérent
            decimal netAmount = Function.MoneyHelper.Round((total - totalDiscount) + totalTax);

            // VALIDATION 4: Le montant net ne peut pas être négatif
            if (netAmount < 0)
            {
                XtraMessageBox.Show("Le montant net ne peut pas être négatif.", "Erreur de calcul");
                txtTotalTax.EditValue = 0;
                netAmount = Function.MoneyHelper.Round(total - totalDiscount);
            }

            // Calculer montant dû et rendu
            decimal dueAmount = Function.MoneyHelper.Round(netAmount - paidAmount);
            decimal returnAmount = 0;

            if (dueAmount < 0)
            {
                returnAmount = -dueAmount;
                dueAmount = 0;
            }

            // Mise à jour UI
            txtNetTotalAmount.Text = netAmount.ToString("F2");
            txtDue.Text = dueAmount.ToString("F2");
            txtReturnAmount.EditValue = returnAmount.ToString("F2");

            RecalculateAndRefreshFields();
        }

        private void RecalculateAndRefreshFields()
        {
            // Example additional recalculations might be needed here
            decimal.TryParse(txtTotalTax.EditValue?.ToString(), out decimal tax);
            decimal.TryParse(txtPaidAmount.EditValue?.ToString(), out decimal paidAmount);

            // Example re-calculation of total for consistency
            decimal total = decimal.Parse(txtNetTotalAmount.Text) - tax + decimal.Parse(txtTotalDiscount.EditValue?.ToString());

            // Set the recalculated total back to its field if necessary
            txtTotal.EditValue = total.ToString("F2");

            // Further recalculations can be added here if dependent on the discount or other fields
        }

        private void btnSalePrint_Click(object sender, EventArgs e)
        {
            //ReceiptReport receiptReport = new ReceiptReport();

            //Models.Maintenance maintenance = Shared.db.Maintenances.Find(this.maintenances.maintenance_id);

            //if (maintenance != null)
            //{
            //    Models.Customer customer = Shared.db.Customers.Find(maintenance.CustomerId);
            //    receiptReport.setCustomerInfo(customer);

            //    Models.BusinessLocation businessLocation = Shared.db.BusinessLocations.Find(maintenance.BusinessLocationId);
            //    receiptReport.setBusinessLocationInfo(businessLocation);

            //    Models.Brand brand = Shared.db.Brands.Find(maintenance.BrandId);
            //    receiptReport.setBrandInfo(brand);

            //    Models.Device device = Shared.db.Devices.Find(maintenance.DeviceId);
            //    receiptReport.setDeviceInfo(device);

            //    Models.Modele modele = Shared.db.Modeles.Find(maintenance.ModeleId);
            //    receiptReport.setModeleInfo();

            //    Models.Technical technical = Shared.db.Technicals.Find(maintenance.TechnicalId);
            //    receiptReport.setTechnicalInfo(technical);

            //    List<MaintenanceRepairChecklist> maintenanceRepairChecklist = Shared.db.MaintenanceRepairChecklists
            //    .Where(m => m.MaintenanceId == maintenance.Id)
            //    .Include(r => r.RepairChecklist)
            //    .ToList();

            //    receiptReport.setRepairChecklistInfo(maintenanceRepairChecklist);
            //}

            //receiptReport.CreateDocument();
            //documentViewer.PrintingSystem = receiptReport.PrintingSystem;
            //documentViewer.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth, new object[] { });
        }

        // Function to check if there is enough quantity of a product in the warehouse
        public bool CheckProductQuantityAsync(int productId, int warehouseId, decimal requestedQuantity)
        {
            var productInWarehouse = Shared.db.ProductWarehouses.FirstOrDefault(p => p.ProductId == productId && p.WarehouseId == warehouseId);
            // Check if the product exists and has enough quantity
            return productInWarehouse != null && productInWarehouse.Qty >= requestedQuantity;
        }

        private void txtWarehouse_EditValueChanged(object sender, EventArgs e)
        {
            if (txtWarehouse.EditValue != null)
            {
                this.warehouse_id = Convert.ToInt32(txtWarehouse.EditValue.ToString());
                this.getProducts();
            }
        }

        public int GetAvailableQuantityAsync(int productId, int warehouseId)
        {
            var productInWarehouse = Shared.db.ProductWarehouses
                .Where(p => p.ProductId == productId && p.WarehouseId == warehouseId)
                .FirstOrDefault();

            if (productInWarehouse != null)
                return (int)productInWarehouse.Qty;

            return 0;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            if (dxValidationProviderSale.Validate())
            {
                Models.SaleHold sale;

                if (dt.Rows.Count == 0)
                {
                    Sound.Wrong();
                    XtraMessageBox.Show("Please add at least one items.");
                    return;
                }

                if (this.type == "Add")
                {
                    sale = new Models.SaleHold();

                    decimal due = 0;
                    decimal returnAmount = 0;

                    if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                    {
                        returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                    }
                    else
                    {
                        due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                    }

                    sale.SaleDate = DateTime.Parse(txtSaleDate.EditValue.ToString());
                    sale.ReferenceNo = txtReferenceNo.Text;
                    //sale.DiscountType = txtDiscountType.Text;
                    //sale.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                    sale.SaleStatus = txtSaleStatus.Text;
                    sale.PaymentSatus = txtPaymentStatus.Text;
                    //sale.AdditionalNotes = txtAdditionalNotes.Text;
                    //sale.ShippingDetails = txtShippingDetails.Text;
                    //sale.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                    sale.NetTotalAmount = this.calculeTotal();
                    sale.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                    sale.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                    sale.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                    sale.Due = due;
                    sale.ReturnAmount = returnAmount;
                    sale.NumberItems = dt.Rows.Count;
                    sale.BusinessLocationId = int.Parse(txtBussLocation.EditValue.ToString());
                    sale.CustomerId = Int32.Parse(txtCustomer.EditValue.ToString());
                    sale.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                    sale.UserId = 1;
                    sale.SaleMonth = DateTime.Now.Month;
                    sale.SaleYear = DateTime.Now.Year;
                    sale.CreatedAt = DateTime.Now;
                    sale.UpdatedAt = DateTime.Now;

                    Shared.db.SaleHolds.Add(sale);

                    Shared.db.SaveChanges();

                    List<SaleDetailHold> saleDetailList = new List<SaleDetailHold>();

                    for (int i = 0; dt.Rows.Count > i; i++)
                    {
                        SaleDetailHold saleDetail = new SaleDetailHold();
                        saleDetail.ProductId = Convert.ToInt32(this.dt.Rows[i]["ProductId"]);
                        saleDetail.SaleId = sale.Id;
                        saleDetail.ProductName = this.dt.Rows[i]["ProductName"].ToString();
                        saleDetail.SaleQuantity = Convert.ToDecimal(this.dt.Rows[i]["SaleQuantity"]);
                        saleDetail.UnitCostBd = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBd"]);
                        saleDetail.DiscountPercent = Convert.ToDecimal(this.dt.Rows[i]["DiscountPercent"]);
                        saleDetail.UnitCostBt = Convert.ToDecimal(this.dt.Rows[i]["UnitCostBt"]);
                        saleDetail.LineTotal = Convert.ToDecimal(this.dt.Rows[i]["LineTotal"]);
                        saleDetail.ProfitMargin = Convert.ToDecimal(this.dt.Rows[i]["ProfitMargin"]);
                        saleDetail.UnitSellingPrice = Convert.ToDecimal(this.dt.Rows[i]["UnitSellingPrice"]);
                        saleDetail.CreatedAt = DateTime.Now;
                        saleDetail.UpdatedAt = DateTime.Now;
                        saleDetailList.Add(saleDetail);
                    }

                    Shared.db.SaleDetailHolds.AddRange(saleDetailList);
                    Shared.db.SaveChanges();

                    btnMtDataReset_Click(null, EventArgs.Empty);

                    this.ReferenceNo();
                }
                else
                {
                    sale = Shared.db.SaleHolds.Find(this.sales.sale_id);

                    decimal due = 0;
                    decimal returnAmount = 0;

                    if (decimal.Parse(txtPaidAmount.EditValue.ToString()) >= this.calculeTotal())
                    {
                        returnAmount = decimal.Parse(txtPaidAmount.EditValue.ToString()) - this.calculeTotal();
                    }
                    else
                    {
                        due = this.calculeTotal() - decimal.Parse(txtPaidAmount.EditValue.ToString());
                    }

                    if (sale != null)
                    {
                        sale.SaleDate = DateTime.Parse(txtSaleDate.EditValue.ToString());
                        sale.ReferenceNo = txtReferenceNo.Text;
                        //sale.DiscountType = txtDiscountType.Text;
                        sale.SaleStatus = txtSaleStatus.Text;
                        //sale.DiscountAmount = decimal.Parse(txtDiscountAmount.EditValue.ToString());
                        sale.PaymentSatus = txtPaymentStatus.Text;
                        //sale.AdditionalNotes = txtAdditionalNotes.Text;
                        //sale.ShippingDetails = txtShippingDetails.Text;
                        //sale.AdditionalShippingCharges = decimal.Parse(txtAdditionalShippingCharges.EditValue.ToString());
                        sale.NetTotalAmount = this.calculeTotal();
                        sale.PaidAmount = decimal.Parse(txtPaidAmount.EditValue.ToString());
                        sale.TotalTax = decimal.Parse(txtTotalTax.EditValue.ToString());
                        sale.TotalDiscount = decimal.Parse(txtTotalDiscount.EditValue.ToString());
                        sale.ReturnAmount = returnAmount;
                        sale.Due = due;
                        sale.NumberItems = gridViewProducts.RowCount;
                        sale.BusinessLocationId = Int32.Parse(txtBussLocation.EditValue.ToString());
                        sale.CustomerId = Int32.Parse(txtCustomer.EditValue.ToString());
                        sale.WarehouseId = Int32.Parse(txtWarehouse.EditValue.ToString());
                        sale.UserId = 1;
                        sale.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(sale).State = EntityState.Modified;

                        Shared.db.SaveChanges();
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Function.Sound.Added();
            }
            else
            {
                Function.Sound.Wrong();
            }

            this.getLatestSaleHolds();
        }

        private void deleteCurrentHold(string referenceNo = "")
        {
            using (var transaction = Shared.db.Database.BeginTransaction())
            {
                try
                {
                    // Delete Sale Holds
                    var sale = Shared.db.SaleHolds.FirstOrDefault(s => s.ReferenceNo == referenceNo);
                    if (sale != null)
                    {
                        // Delete Sale Detail List Holds
                        var saleDetailList = Shared.db.SaleDetailHolds.Where(s => s.SaleId == sale.Id).ToList();
                        foreach (var detail in saleDetailList)
                        {
                            Shared.db.SaleDetailHolds.Remove(detail);
                        }

                        Shared.db.SaleHolds.Remove(sale);
                        Shared.db.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle the error
                    transaction.Rollback();
                    // Depending on your error handling strategy, you might want to log this error or show a message to the user.
                    Console.WriteLine("Error deleting sale hold: " + ex.Message);
                }
            }
        }


        private void repoReturnToList_Click(object sender, EventArgs e)
        {
            object orderId = this.gridViewHoldOrders.GetRowCellValue(this.gridViewHoldOrders.FocusedRowHandle, "Id");

            dt.Rows.Clear();

            Models.SaleHold sale = Shared.db.SaleHolds.Find(orderId);

            txtSaleDate.EditValue = sale.SaleDate;
            txtReferenceNo.Text = sale.ReferenceNo;
            txtSaleType.Text = sale.SaleType;
            txtSaleStatus.Text = sale.SaleStatus;
            txtPaymentStatus.Text = sale.PaymentSatus;
            txtNetTotalAmount.Text = sale.NetTotalAmount.ToString() + " DA";
            txtPaidAmount.EditValue = sale.PaidAmount;
            txtTotalTax.EditValue = sale.TotalTax;
            txtTotalDiscount.EditValue = sale.TotalDiscount;
            txtDue.EditValue = sale.Due;
            txtReturnAmount.EditValue = sale.ReturnAmount;
            txtBussLocation.EditValue = sale.BusinessLocationId;
            txtCustomer.EditValue = sale.CustomerId;
            txtWarehouse.EditValue = sale.WarehouseId;

            var saleDetailList = Shared.db.SaleDetailHolds.Where(s => s.SaleId == sale.Id).ToList();

            foreach (var item in saleDetailList)
            {
                DataRow NewRow = dt.NewRow();
                NewRow["ProductId"] = item.ProductId;
                NewRow["ProductName"] = item.ProductName;
                NewRow["SaleQuantity"] = item.SaleQuantity;
                NewRow["UnitCostBd"] = item.UnitCostBd;
                NewRow["DiscountPercent"] = item.DiscountPercent;
                NewRow["UnitCostBt"] = item.UnitCostBt;
                NewRow["LineTotal"] = item.LineTotal;
                NewRow["ProfitMargin"] = item.ProfitMargin;
                NewRow["UnitSellingPrice"] = item.UnitSellingPrice;

                dt.Rows.Add(NewRow);
            }

            gridControlProducts.DataSource = dt;

            Sound.Selected();
        }

        private async void tileViewCategories_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            try
            {
                object id = this.tileViewCategories.GetRowCellValue(this.tileViewCategories.FocusedRowHandle, "Id");
                int cateId = int.Parse(id.ToString());

                await LoadProductsByCategoryAsync(cateId, currentPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsByCategoryAsync(int cateId, int pageNumber)
        {
            using (var context = new AppDbContext())
            {
                var query = from p in context.Products
                            where p.CategoryId == cateId
                            join pw in context.ProductWarehouses on p.Id equals pw.ProductId into joined
                            from j in joined.DefaultIfEmpty()
                            select new
                            {
                                p.Id,
                                p.ProductName,
                                p.Image,
                                Qty = (int?)j.Qty ?? 0,
                                WarehouseId = (int?)j.WarehouseId
                            };

                int totalItems = await query.CountAsync();
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                int startRecord = (pageNumber - 1) * itemsPerPage;
                var pagedData = await query
                    .OrderByDescending(p => p.Id)
                    .Skip(startRecord)
                    .Take(itemsPerPage)
                    .ToListAsync();

                gridControlProductsCards.DataSource = pagedData;
                UpdatePageLabel();
                UpdateNavigationButtons();
            }
        }

        public void SetCustomer(int idcustomer)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                txtCustomer.Properties.DataSource = AppDb.Customers.Where(x => x.Status == "Active").ToList();
                if (idcustomer != 0)
                {
                    txtCustomer.EditValue = idcustomer;
                }
                else
                {
                    Models.Customer customer = AppDb.Customers.FirstOrDefault();

                    if (customer != null)
                    {
                        txtCustomer.EditValue = customer.Id;
                    }
                }

            }
        }
        private void btnQuickCustomer_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Customer.AddEditCustomer addEditCustomer = new Customer.AddEditCustomer(this);
            addEditCustomer.FormClosed += (s, args) => overlay.Close();
            addEditCustomer.Show();
            addEditCustomer.TopMost = true;
        }

        public void SetWarehouse(int idWarehouses)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                txtWarehouse.Properties.DataSource = AppDb.Warehouses.ToList();
                if (idWarehouses != 0)
                {
                    txtWarehouse.EditValue = idWarehouses;
                    this.warehouse_id = idWarehouses;
                }
                else
                {
                    Models.Warehouse warehouse = AppDb.Warehouses.FirstOrDefault();

                    if (warehouse != null)
                    {
                        txtWarehouse.EditValue = warehouse.Id;
                        this.warehouse_id = warehouse.Id;
                    }
                }

            }
        }
        private void btnQuickWarehouse_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Product.Warehouse.AddEditWarehouse addEditWarehouse = new Product.Warehouse.AddEditWarehouse(this);
            addEditWarehouse.FormClosed += (s, args) => overlay.Close();
            addEditWarehouse.Show();
            addEditWarehouse.TopMost = true;
        }
        public void SetBusinessLocation(int idtxtBussLocation)
        {
            using (AppDbContext AppDb = new AppDbContext())
            {
                txtBussLocation.Properties.DataSource = AppDb.BusinessLocations.ToList();
                if (idtxtBussLocation != 0)
                {
                    txtBussLocation.EditValue = idtxtBussLocation;
                }
                else
                {
                    Models.BusinessLocation BusinessLocation = AppDb.BusinessLocations.FirstOrDefault();

                    if (BusinessLocation != null)
                    {
                        txtBussLocation.EditValue = BusinessLocation.Id;
                    }
                }

            }
        }
        private void btnQuickBl_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            BusinessLocation.AddEditBusinessLocation addEditBusinessLocation = new BusinessLocation.AddEditBusinessLocation(this);
            addEditBusinessLocation.FormClosed += (s, args) => overlay.Close();
            addEditBusinessLocation.Show();
            addEditBusinessLocation.TopMost = true;
        }

        private void btnRewards_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            CardFidelity.Reward.PreviewRewards previewRewards = new CardFidelity.Reward.PreviewRewards();
            previewRewards.FormClosed += (s, args) => overlay.Close();
            previewRewards.Show();
            previewRewards.TopMost = true;
        }

        private void btnPromotions_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Product.Promotion.PreviewPromotions previewPromotions = new Product.Promotion.PreviewPromotions();
            previewPromotions.FormClosed += (s, args) => overlay.Close();
            previewPromotions.Show();
            previewPromotions.TopMost = true;
        }

        private void btnLockScreen_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsLockScreen)
            {
                Sound.Selected();
                this.ShowOverlay();
                Auth.LockScreen lockScreen = new Auth.LockScreen();
                lockScreen.ShowDialog();
                this.HideOverlay();
            }
            else
            {
                AccessDenied accessDenied = new AccessDenied();
                accessDenied.ShowDialog();
            }
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            XtraMessageBox.Show("Refresh All");
        }

        private void btnAlertQuantity_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Alert Quantity"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Alert.AlertQuantity alertQuantity = new Alert.AlertQuantity();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                alertQuantity.FormClosed += (s, args) => overlay.Close();

                alertQuantity.Show();
                alertQuantity.TopMost = true;
            }
        }

        private void btnTodaySummary_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Today Summary"))
            {
                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Alert.TodaySummary todaySummary = new Alert.TodaySummary();

                // This ensures the overlay form is displayed behind the modal form but above the parent form
                todaySummary.FormClosed += (s, args) => overlay.Close();

                todaySummary.Show();
                todaySummary.TopMost = true;
            }
        }

        private void btnCloseRegister_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Close Register"))
            {
                int currentBusinessLocationId = Properties.Settings.Default.BusinessLocation;

                if (Function.Helper.IsRegisterOpen(currentBusinessLocationId))
                {
                    OverlayForm overlay = new OverlayForm(this);
                    overlay.Show();

                    Alert.CloseRegister closeRegister = new Alert.CloseRegister();

                    // This ensures the overlay form is displayed behind the modal form but above the parent form
                    closeRegister.FormClosed += (s, args) => overlay.Close();

                    closeRegister.Show();
                    closeRegister.TopMost = true;
                }
                else
                {
                    XtraMessageBox.Show("No open register record found for the specified business location.");
                }
            }
        }

        //public Models.Product GetProductByBarcode(string barcode)
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        var productBarcode = context.ProductBarCodes
        //                                    .Include(p => p.Product)  // Ensure the Product is loaded along with the ProductBarcode
        //                                    .FirstOrDefault(pb => pb.Value == barcode);

        //        return productBarcode?.Product; // Returns the Product if the barcode is found, otherwise null
        //    }
        //}

        private void txtProductsCardSearch_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCanScanBarCode.IsOn)
            {
                searchTimer.Stop();
                searchTimer.Start();

                txtProductsCardSearch.Client = null;
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            if (txtCanScanBarCode.IsOn)
            {
                searchTimer.Stop();
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            string barcode = txtProductsCardSearch.Text.Trim();

            if (!string.IsNullOrEmpty(barcode))
            {
                try
                {
                    var product = GetProductByBarcode(barcode);

                    if (product != null)
                    {
                        this.scanProduct(product.Id);
                    }
                    else
                    {
                        txtProductsCardSearch.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                txtProductsCardSearch.Clear();
            }
        }

        private Models.Product GetProductByBarcode(string barcode)
        {
            using (var context = new AppDbContext())
            {
                var productBarcode = context.ProductBarCodes
                                            .Include(p => p.Product)  // Ensure the Product is loaded along with the ProductBarcode
                                            .FirstOrDefault(pb => pb.Value == barcode);

                return productBarcode?.Product; // Returns the Product if the barcode is found, otherwise null
            }
        }

        public void scanProduct(int productId)
        {
            this.productId = productId;

            if (!CheckProductQuantityAsync(this.productId, this.warehouse_id, 1))
            {
                Sound.Wrong();

                OverlayForm overlay = new OverlayForm(this);
                overlay.Show();

                Alert.OutOfStock outOfStock = new Alert.OutOfStock();
                outOfStock.FormClosed += (s, args) => overlay.Close();
                outOfStock.Show();
                outOfStock.TopMost = true;
            }
            else
            {
                if (type == "Add")
                {
                    if (this.productId != 0)
                    {
                        // Check if the product already exists in the DataTable
                        DataRow existingRow = dt.Rows
                            .Cast<DataRow>()
                            .FirstOrDefault(r => r["ProductId"].Equals(productId));

                        if (existingRow != null)
                        {
                            this.currentExistingRow = existingRow;

                            OverlayForm overlay = new OverlayForm(this);
                            overlay.Show();
                            UpdateQty updateQty = new UpdateQty(productId, int.Parse(txtCustomer.EditValue.ToString()));
                            updateQty.FormClosed += (s, args) =>
                            {
                                overlay.Close();
                                overlay.Dispose();
                            };
                            updateQty.setObject(this);
                            updateQty.Show();
                            updateQty.TopMost = true;

                            //// Product already exists, just update the quantity
                            //int currentQty = Convert.ToInt32(existingRow["SaleQuantity"]);
                            //existingRow["SaleQuantity"] = currentQty + 1;

                            //// Recalculate the line total based on the new quantity
                            //decimal unitPrice = Convert.ToDecimal(existingRow["UnitSellingPrice"]);
                            //existingRow["LineTotal"] = unitPrice * (currentQty + 1);
                        }
                        else
                        {
                            // Product does not exist, add a new row
                            Models.Product product = Shared.db.Products.Find(productId);
                            DataRow newRow = dt.NewRow();
                            newRow["ProductId"] = productId;
                            newRow["UnitId"] = product.UnitId;
                            newRow["ProductName"] = product.ProductName;
                            newRow["SaleQuantity"] = 1;
                            newRow["UnitCostBd"] = product.PurchasePriceExcTax;
                            newRow["DiscountPercent"] = 0;  // Assuming no discount initially
                            newRow["UnitCostBt"] = product.PurchasePriceExcTax;
                            newRow["LineTotal"] = Convert.ToDecimal(newRow["SaleQuantity"]) * product.SellingPrice;
                            newRow["ProfitMargin"] = product.Xmargin;
                            newRow["UnitSellingPrice"] = product.SellingPrice;
                            dt.Rows.Add(newRow);
                        }

                        // Refresh the grid to show the updated data
                        gridControlProducts.DataSource = dt;
                    }

                    //if (id != null)
                    //{
                    //    int index = dt.Rows.Count == 0 ? -1 : this.getIndex(productId.ToString());
                    //    Models.Product product = Shared.db.Products.Find(productId);

                    //    if (index == -1)
                    //    {
                    //        DataRow NewRow = dt.NewRow();
                    //        NewRow["ProductId"] = productId;
                    //        NewRow["ProductName"] = product.ProductName;
                    //        NewRow["SaleQuantity"] = 1;
                    //        NewRow["UnitCostBd"] = product.PurchasePriceExcTax;
                    //        NewRow["DiscountPercent"] = 0;
                    //        NewRow["UnitCostBt"] = product.PurchasePriceExcTax;
                    //        NewRow["LineTotal"] = Convert.ToDecimal(NewRow["SaleQuantity"]) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                    //        NewRow["ProfitMargin"] = product.Xmargin;
                    //        NewRow["UnitSellingPrice"] = product.SellingPrice;

                    //        dt.Rows.Add(NewRow);
                    //    }
                    //    else
                    //    {
                    //        int qty = int.Parse(dt.Rows[index]["SaleQuantity"].ToString());

                    //        dt.Rows[index].SetField("SaleQuantity", qty + 1);
                    //        dt.AcceptChanges();
                    //    }

                    //    gridControlProducts.DataSource = null;
                    //    gridControlProducts.DataSource = dt;
                    //}
                }
                else
                {
                    object DetailsId = this.gridViewProducts.GetRowCellValue(this.gridViewProducts.FocusedRowHandle, "Id");

                    Models.Product product = Shared.db.Products.Find(productId);

                    Models.SaleDetail saleDetail = Shared.db.SaleDetails.Find(int.Parse(DetailsId.ToString()));

                    if (saleDetail != null)
                    {
                        saleDetail.SaleQuantity = saleDetail.SaleQuantity + 1;
                        saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - saleDetail.DiscountPercent / 100);

                        Shared.db.Entry(saleDetail).State = EntityState.Modified;

                        Shared.db.SaveChanges();
                    }
                    else
                    {
                        saleDetail.ProductId = productId;
                        saleDetail.SaleId = this.sale_id;

                        saleDetail.SaleQuantity = 1;
                        saleDetail.UnitCostBd = product.PurchasePriceExcTax;
                        saleDetail.DiscountPercent = 0;
                        saleDetail.UnitCostBt = product.PurchasePriceExcTax;
                        saleDetail.LineTotal = Convert.ToDecimal(saleDetail.SaleQuantity) * Convert.ToDecimal(product.SellingPrice) * (1 - 0 / 100);
                        saleDetail.ProfitMargin = product.Xmargin;
                        saleDetail.UnitSellingPrice = product.SellingPrice;

                        Shared.db.SaleDetails.Add(saleDetail);

                        Shared.db.SaveChanges();
                    }

                    this.changeNumberItems(this.sale_id);

                    this.getSaleItems(this.sale_id);
                }

                txtNetTotalAmount.Text = this.calculeTotal().ToString();

                this.getProducts();

                Function.Sound.Added();

            }

            txtProductsCardSearch.Clear();
        }

        private void txtPriceGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPriceGroup.EditValue != null)
            {
                this.currentPriceGroupId = int.Parse(txtPriceGroup.EditValue.ToString());
                this.getProducts();
            }
        }

        private void txtCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCustomer.EditValue != null)
            {
                var customer = Shared.db.Customers.Find(int.Parse(txtCustomer.EditValue.ToString()));

                if (customer != null)
                {
                    this.user_id = customer.Id;
                    this.currentPriceGroupId = customer.PriceGroup ?? 0;
                    txtPriceGroup.EditValue = this.currentPriceGroupId;
                }
            }
        }

        private void txtCanScanBarCode_EditValueChanged(object sender, EventArgs e)
        {
            if (txtCanScanBarCode.IsOn)
            {
                txtProductsCardSearch.Select();
            }
        }

        private void gridViewUnpaidOrders_Click(object sender, EventArgs e)
        {
            Sound.Selected();
        }

        private void btnPayDue_Click(object sender, EventArgs e)
        {
            this.orderDuePayment();
        }

        private void gridViewUnpaidOrders_DoubleClick(object sender, EventArgs e)
        {
            this.orderDuePayment();
        }

        public void orderDuePayment()
        {
            try
            {
                // Retrieve the order ID from the selected row in the grid view
                object id = this.gridViewUnpaidOrders.GetRowCellValue(this.gridViewUnpaidOrders.FocusedRowHandle, "Id");

                if (id != null)
                {
                    // Convert the retrieved ID to an integer
                    int orderId = int.Parse(id.ToString());

                    //// Create a new instance of the OrderPayment form and pass the order ID
                    //OrderPayment orderPayment = new OrderPayment(orderId);

                    //// Show the OrderPayment form as a dialog
                    //orderPayment.setObject(this);
                    //orderPayment.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a valid order to pay.");
                }
            }
            catch (Exception ex)
            {
                // Handle any potential exceptions
                MessageBox.Show("An error occurred while processing the payment: " + ex.Message);
            }
        }

        private void btnRefreshHold_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getLatestSaleHolds();
        }

        private void btnRefreshUnpaidOrders_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getUnpaidOrders();
        }

        private void btnRefreshLatestOrders_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getLatestSale();
        }

        private void btnRefreshLatestCustomers_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getLatestCustomers();
            this.GetCustomerSales();
            this.todayTotalProfit();
            this.todayTotalNetProfitAndGross();
        }

        private void btnRefreshSaleReturn_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            this.getSaleReturns();
        }

        private void btnPayCustomerDue_Click(object sender, EventArgs e)
        {
            ShowOverlay();
            Payment payment = new Payment(this.user_id);
            payment.ShowDialog();
            HideOverlay();
        }

        private void txtDue_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void NavPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                BindDataToGrid(currentPage);
            }
        }

        private void NavFirstPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                BindDataToGrid(currentPage);
            }
        }

        private void NavLastPage_Click(object sender, EventArgs e)
        {
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                BindDataToGrid(currentPage);
            }
        }

        private void NavNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                BindDataToGrid(currentPage);
            }
        }

        private void perPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(perPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    itemsPerPage = newItemsPerPage;
                    currentPage = 1; // Reset to the first page

                    var query = from p in context.Products
                                join pw in context.ProductWarehouses on p.Id equals pw.ProductId into pwJoined
                                from pwj in pwJoined.DefaultIfEmpty()
                                join pp in context.ProductPrices on p.Id equals pp.ProductId
                                where pp.PriceGroupId == this.currentPriceGroupId
                                select new
                                {
                                    p.Id,
                                    p.ProductName,
                                    p.Image,
                                    Qty = (int?)pwj.Qty ?? 0,
                                    WarehouseId = (int?)pwj.WarehouseId,
                                    Price = (decimal?)pp.Price
                                };

                    var productList = query
                        .OrderByDescending(p => p.Id);

                    int totalItems = productList.Count();
                    totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
                    BindDataToGrid(currentPage);
                }
            }
        }

        private void txtProductsCardSearch_TextChanged(object sender, EventArgs e)
        {
            searchTerm = txtProductsCardSearch.Text.Trim();
            currentPage = 1; // Reset to the first page
            this.getProducts();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Add Purchase"))
            {
                ShowOverlay();
                Purchase.AddEditPurchase addEditPurchase = new Purchase.AddEditPurchase();
                addEditPurchase.ShowDialog();
                HideOverlay();
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("Sales"))
            {
                ShowOverlay();
                Sale.Sales sales = new Sale.Sales();
                sales.ShowDialog();
                HideOverlay();
            }
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Statistics"))
            {
                ShowOverlay();
                Statistics.Statistics statistics = new Statistics.Statistics();
                statistics.ShowDialog();
                HideOverlay();
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Reports"))
            {
                ShowOverlay();
                OrderReport.Report report = new OrderReport.Report();
                report.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwProducts_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Products"))
            {
                ShowOverlay();
                Product.Products products = new Product.Products();
                products.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwCategories_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Categories"))
            {
                ShowOverlay();
                Product.Category.Categories categories = new Product.Category.Categories();
                categories.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwBrands_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Brands"))
            {
                ShowOverlay();
                Brand.Brands brands = new Brand.Brands();
                brands.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwWarranties_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Warranties"))
            {
                ShowOverlay();
                Product.Warranty.Warranties warranties = new Product.Warranty.Warranties();
                warranties.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwUnits_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Units"))
            {
                ShowOverlay();
                Product.Unit.Units units = new Product.Unit.Units();
                units.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwFields_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Fields"))
            {
                ShowOverlay();
                Product.Field.Fields fields = new Product.Field.Fields();
                fields.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwPriceGroup_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Price Groups"))
            {
                ShowOverlay();
                Product.PriceGroup.PriceGroups priceGroups = new Product.PriceGroup.PriceGroups();
                priceGroups.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwPromotions_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Promotions"))
            {
                ShowOverlay();
                Product.Promotion.Promotions promotions = new Product.Promotion.Promotions();
                promotions.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwStocks_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Stocks"))
            {
                ShowOverlay();
                Stock.Stocks stocks = new Stock.Stocks();
                stocks.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwAdjustments_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Adjustments"))
            {
                ShowOverlay();
                Stock.Adjustment.Adjustments adjustments = new Stock.Adjustment.Adjustments();
                adjustments.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwTransfers_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Transfers"))
            {
                ShowOverlay();
                Stock.Transfer.Transfers transfers = new Stock.Transfer.Transfers();
                transfers.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwInventry_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Inventories"))
            {
                ShowOverlay();
                Inventory.Inventories inventories = new Inventory.Inventories();
                inventories.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwStatistics_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Statistics"))
            {
                ShowOverlay();
                Statistics.Statistics statistics = new Statistics.Statistics();
                statistics.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwReport_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Reports"))
            {
                ShowOverlay();
                OrderReport.Report report = new OrderReport.Report();
                report.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwExpenses_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Expenses"))
            {
                ShowOverlay();
                Expense.Expenses expenses = new Expense.Expenses();
                expenses.ShowDialog();
                HideOverlay();
            }
        }

        private void btnQwWastes_Click(object sender, EventArgs e)
        {
            if (Function.Permission.HasPermission("List Wastes"))
            {
                ShowOverlay();
                Waste.Wastes wastes = new Waste.Wastes();
                wastes.ShowDialog();
                HideOverlay();
            }
        }

        private void hdNavPrevPage_Click(object sender, EventArgs e)
        {
            if (hdCurrentPage > 1)
            {
                hdCurrentPage--;
                BindDataToGrid(hdCurrentPage);
            }
        }

        private void hdNavFirstPage_Click(object sender, EventArgs e)
        {
            if (hdCurrentPage != 1)
            {
                hdCurrentPage = 1;
                BindDataToGridHd(hdCurrentPage);
            }
        }

        private void hdNavLastPage_Click(object sender, EventArgs e)
        {
            if (hdCurrentPage != hdTotalPages)
            {
                hdCurrentPage = hdTotalPages;
                BindDataToGridHd(hdCurrentPage);
            }
        }

        private void hdNavNextPage_Click(object sender, EventArgs e)
        {
            if (hdCurrentPage < hdTotalPages)
            {
                hdCurrentPage++;
                BindDataToGridHd(hdCurrentPage);
            }
        }

        private void hdPerPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(hdPerPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    hdItemsPerPage = newItemsPerPage;
                    hdCurrentPage = 1; // Reset to the first page
                    int totalItems = context.SaleHolds.Count();
                    hdTotalPages = (int)Math.Ceiling((double)totalItems / hdItemsPerPage);
                    BindDataToGridHd(hdCurrentPage);
                }
            }
        }

        private void searchControlHd_TextChanged(object sender, EventArgs e)
        {
            hdSearchTerm = searchControlHd.Text.Trim();
            hdCurrentPage = 1; // Reset to the first page
            this.getLatestSaleHolds();
        }

        private void uoNavPrevPage_Click(object sender, EventArgs e)
        {
            if (uoCurrentPage > 1)
            {
                uoCurrentPage--;
                BindDataToGridUo(uoCurrentPage);
            }
        }

        private void uoNavFirstPage_Click(object sender, EventArgs e)
        {
            if (uoCurrentPage != 1)
            {
                uoCurrentPage = 1;
                BindDataToGridUo(uoCurrentPage);
            }
        }

        private void uoNavLastPage_Click(object sender, EventArgs e)
        {
            if (uoCurrentPage != uoTotalPages)
            {
                uoCurrentPage = uoTotalPages;
                BindDataToGridUo(uoCurrentPage);
            }
        }

        private void uoNavNextPage_Click(object sender, EventArgs e)
        {
            if (uoCurrentPage < uoTotalPages)
            {
                uoCurrentPage++;
                BindDataToGridUo(uoCurrentPage);
            }
        }

        private void uoPerPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(uoPerPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    uoItemsPerPage = newItemsPerPage;
                    uoCurrentPage = 1; // Reset to the first page
                    int totalItems = context.Sales.Where(order => order.Due > 0).Count();
                    uoTotalPages = (int)Math.Ceiling((double)totalItems / uoItemsPerPage);
                    BindDataToGridUo(uoCurrentPage);
                }
            }
        }

        private void searchControlUo_TextChanged(object sender, EventArgs e)
        {
            uoSearchTerm = searchControlUo.Text.Trim();
            uoCurrentPage = 1; // Reset to the first page
            this.getUnpaidOrders();
        }

        private void loNavPrevPage_Click(object sender, EventArgs e)
        {
            if (loCurrentPage > 1)
            {
                loCurrentPage--;
                BindDataToGridLo(loCurrentPage);
            }
        }

        private void loNavFirstPage_Click(object sender, EventArgs e)
        {
            if (loCurrentPage != 1)
            {
                loCurrentPage = 1;
                BindDataToGridLo(loCurrentPage);
            }
        }

        private void loNavLastPage_Click(object sender, EventArgs e)
        {
            if (loCurrentPage != loTotalPages)
            {
                loCurrentPage = loTotalPages;
                BindDataToGridLo(loCurrentPage);
            }
        }

        private void loNavNextPage_Click(object sender, EventArgs e)
        {
            if (loCurrentPage < loTotalPages)
            {
                loCurrentPage++;
                BindDataToGridLo(loCurrentPage);
            }
        }

        private void loPerPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(loPerPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    loItemsPerPage = newItemsPerPage;
                    loCurrentPage = 1; // Reset to the first page
                    int totalItems = context.Sales.Count();
                    loTotalPages = (int)Math.Ceiling((double)totalItems / loItemsPerPage);
                    BindDataToGridLo(loCurrentPage);
                }
            }
        }

        private void searchControlLo_TextChanged(object sender, EventArgs e)
        {
            loSearchTerm = searchControlLo.Text.Trim();
            loCurrentPage = 1; // Reset to the first page
            this.getLatestSale();
        }

        private void lcNavPrevPage_Click(object sender, EventArgs e)
        {
            if (lcCurrentPage > 1)
            {
                lcCurrentPage--;
                BindDataToGridLc(lcCurrentPage);
            }
        }

        private void lcNavFirstPage_Click(object sender, EventArgs e)
        {
            if (lcCurrentPage != 1)
            {
                lcCurrentPage = 1;
                BindDataToGridLc(lcCurrentPage);
            }
        }

        private void lcNavLastPage_Click(object sender, EventArgs e)
        {
            if (lcCurrentPage != lcTotalPages)
            {
                lcCurrentPage = lcTotalPages;
                BindDataToGridLc(lcCurrentPage);
            }
        }

        private void lcNavNextPage_Click(object sender, EventArgs e)
        {
            if (lcCurrentPage < lcTotalPages)
            {
                lcCurrentPage++;
                BindDataToGridLc(lcCurrentPage);
            }
        }

        private void lcPerPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(lcPerPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    lcItemsPerPage = newItemsPerPage;
                    lcCurrentPage = 1; // Reset to the first page
                    int totalItems = context.Customers.Count();
                    lcTotalPages = (int)Math.Ceiling((double)totalItems / lcItemsPerPage);
                    BindDataToGridLc(lcCurrentPage);
                }
            }
        }

        private void searchControlLc_TextChanged(object sender, EventArgs e)
        {
            lcSearchTerm = searchControlLc.Text.Trim();
            lcCurrentPage = 1; // Reset to the first page
            this.getLatestCustomers();
        }

        private void srNavPrevPage_Click(object sender, EventArgs e)
        {
            if (srCurrentPage > 1)
            {
                srCurrentPage--;
                BindDataToGridSr(srCurrentPage);
            }
        }

        private void srNavFirstPage_Click(object sender, EventArgs e)
        {
            if (srCurrentPage != 1)
            {
                srCurrentPage = 1;
                BindDataToGridSr(srCurrentPage);
            }
        }

        private void srNavLastPage_Click(object sender, EventArgs e)
        {
            if (srCurrentPage != srTotalPages)
            {
                srCurrentPage = srTotalPages;
                BindDataToGridSr(srCurrentPage);
            }
        }

        private void srNavNextPage_Click(object sender, EventArgs e)
        {
            if (srCurrentPage < srTotalPages)
            {
                srCurrentPage++;
                BindDataToGridSr(srCurrentPage);
            }
        }

        private void srPerPage_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(srPerPage.SelectedItem.ToString(), out int newItemsPerPage))
            {
                using (var context = new AppDbContext())
                {
                    srItemsPerPage = newItemsPerPage;
                    srCurrentPage = 1; // Reset to the first page
                    int totalItems = context.Returns.Count();
                    srTotalPages = (int)Math.Ceiling((double)totalItems / srItemsPerPage);
                    BindDataToGridSr(srCurrentPage);
                }
            }
        }

        private void searchControlSr_TextChanged(object sender, EventArgs e)
        {
            srSearchTerm = searchControlSr.Text.Trim();
            srCurrentPage = 1; // Reset to the first page
            this.getSaleReturns();
        }

        public async void getCategories(int page)
        {
            using (var context = new AppDbContext())
            {
                var services = await context.Categories
                    .Where(p => p.Name.Contains(cateSearchTerm))
                    .OrderByDescending(i => i.Id)
                    .Skip((page - 1) * cateItemsPerPage)
                    .Take(cateItemsPerPage)
                    .ToListAsync();

                gridControlCategories.DataSource = services;
            }
        }

        private void btnPrevCategory_Click(object sender, EventArgs e)
        {
            if (cateCurrentPage > 1)
            {
                cateCurrentPage--;
                getCategories(cateCurrentPage);
                Sound.Selected();
            }
        }

        private void btnNextCategory_Click(object sender, EventArgs e)
        {
            using (var context = new AppDbContext())
            {
                int totalItems = context.Categories.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / cateItemsPerPage);

                if (cateCurrentPage < totalPages)
                {
                    cateCurrentPage++;
                    getCategories(cateCurrentPage);
                    Sound.Selected();
                }
            }
        }

        private void repositoryItemButtonHoldDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int holdid = 0;
            using (AppDbContext AppDb = new AppDbContext())
            {
                // Charger l'objet SaleHold avec ses SaleDetailHolds liés
                var saleHold = AppDb.SaleHolds
                    .Include(sh => sh.SaleDetailHolds)
                    .SingleOrDefault(sh => sh.Id == holdid);

                // Vérifier si l'objet existe
                if (saleHold != null && XtraMessageBox.Show("Are you sure you want to delete this SaleHold?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Supprimer les éléments liés dans SaleDetailHold
                    AppDb.SaleDetailHolds.RemoveRange(saleHold.SaleDetailHolds);

                    // Supprimer l'objet SaleHold lui-même
                    AppDb.SaleHolds.Remove(saleHold);
                    // Sauvegarder les modifications dans la base de données
                    AppDb.SaveChanges();
                    gridControlHoldOrders.DataSource = AppDb.SaleHolds.ToList();
                }
            }
        }

        private void txtBussLocation_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBussLocation.EditValue != null)
            {
                this.business_location_id = Convert.ToInt32(txtBussLocation.EditValue.ToString());
            }
        }

        private void BtnPrinters_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Printers printers = new Printers();
            printers.FormClosed += (s, args) => overlay.Close();
            printers.Show();
            printers.TopMost = true;
        }

        private void BtnCurrency_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Currency.Currencies currencies = new Currency.Currencies();
            currencies.FormClosed += (s, args) => overlay.Close();
            currencies.Show();
            currencies.TopMost = true;
        }

        public void GetCustomerSales(int customerId = 0)
        {
            using (var context = new AppDbContext())
            {
                // Fetch sales from today with their corresponding sale details
                var today = DateTime.Today;

                // Query for sales
                var salesQuery = context.Sales
                    .Include(s => s.SaleDetails)
                    .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0); // Filter for today's sales

                // Filter by CustomerId if provided
                if (customerId > 0)
                {
                    salesQuery = salesQuery.Where(s => s.CustomerId == customerId);
                }

                // Execute query and project into SaleViewModel
                var sales = salesQuery
                    .Select(s => new SaleViewModel
                    {
                        CustomerId = s.CustomerId,
                        ReferenceNo = s.ReferenceNo,
                        PaidAmount = s.PaidAmount,
                        Due = s.Due,
                        saleDetails = s.SaleDetails.Select(sd => new SaleDetailViewModel
                        {
                            ProductName = sd.ProductName,
                            SaleQuantity = sd.SaleQuantity,
                            LineTotal = sd.LineTotal
                        }).ToList()
                    })
                    .ToList();

                // Bind the filtered data to the grid
                gridControlCustomerSales.DataSource = sales;
            }
        }

        private void gridViewLatestCustomers_RowClick(object sender, RowClickEventArgs e)
        {
            object id = this.gridViewLatestCustomers.GetRowCellValue(this.gridViewLatestCustomers.FocusedRowHandle, "CustomerId");
            int customerIdValue = int.Parse(id.ToString());

            Function.Sound.Selected();

            if (customerIdValue != 0 && int.TryParse(customerIdValue.ToString(), out int customerId))
            {
                // Fetch sales for the selected customer if the CustomerId is valid
                if (customerId > 0)
                {
                    this.GetCustomerSales(customerId);
                    this.todayTotalNetProfitAndGross(customerId);
                }
            }
        }

        public void todayTotalProfit()
        {
            using (var context = new AppDbContext())
            {
                var today = DateTime.Today;

                string total = context.SalePayments
                    .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0)
                    .Where(p => p.IsPaid == true)
                    .Sum(s => s.Amount)
                    .ToString();

                totalProfit.Text = Function.Helper.FormatAmount(total);
            }
        }

        public void todayTotalNetProfitAndGross(int customerId = 0)
        {
            using (var context = new AppDbContext())
            {
                // Calculate date ranges
                var today = DateTime.Today;

                // Base query for sale payments on today's date
                var salePaymentsQuery = context.SalePayments
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date == today);

                // If a customer ID is provided, filter by CustomerId
                if (customerId > 0)
                {
                    salePaymentsQuery = salePaymentsQuery.Where(c => c.CustomerId == customerId);
                }

                // Calculate Gross Total (Assuming Amount represents gross amount)
                var grossTotalToday = salePaymentsQuery
                    .Sum(c => (decimal?)c.Amount) ?? 0;

                // Calculate Net Total (Assuming Paid represents the net amount)
                var netTotalToday = salePaymentsQuery
                    .Where(p => p.IsPaid == true)
                    .Sum(c => (decimal?)c.Paid) ?? 0;

                // Display totals in your UI elements
                currentGrossTotal.Text = Function.Helper.FormatAmount(grossTotalToday.ToString());
                currentNetTotal.Text = Function.Helper.FormatAmount(netTotalToday.ToString());
            }
        }

        public void GetSupplierPurchases(int supplierId = 0)
        {
            using (var context = new AppDbContext())
            {
                // Fetch sales from today with their corresponding sale details
                var today = DateTime.Today;

                // Query for sales
                var purchasesQuery = context.Purchases
                    .Include(s => s.PurchaseDetails)
                    .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0); // Filter for today's sales

                // Filter by CustomerId if provided
                if (supplierId > 0)
                {
                    purchasesQuery = purchasesQuery.Where(s => s.SupplierId == supplierId);
                }

                // Execute query and project into SaleViewModel
                var purchases = purchasesQuery
                    .Select(s => new PurchaseViewModel
                    {
                        ReferenceNo = s.ReferenceNo,
                        PaidAmount = s.PaidAmount,
                        Due = s.Due,
                        purchaseDetails = s.PurchaseDetails.Select(sd => new PurchaseDetailViewModel
                        {
                            ProductName = sd.ProductName,
                            PurchaseQuantity = sd.PurchaseQuantity,
                            Total = sd.LineTotal
                        }).ToList()
                    })
                    .ToList();

                // Bind the filtered data to the grid
                gridControlSupplierPurchases.DataSource = purchases;
            }
        }

        public void todayTotalPurchases()
        {
            using (var context = new AppDbContext())
            {
                var today = DateTime.Today;

                string total = context.Purchases
                    .Where(s => EF.Functions.DateDiffDay(s.CreatedAt, today) == 0)
                    .Sum(s => s.PaidAmount)
                    .ToString();

                totalPurchases.Text = Function.Helper.FormatAmount(total);
            }
        }

        public void todayTotalPurchasesAndDue(int supplierId = 0)
        {
            using (var context = new AppDbContext())
            {
                // Calculate date ranges
                var today = DateTime.Today;

                // Base query for sale payments on today's date
                var purchasesQuery = context.Purchases
                    .Where(c => c.CreatedAt.HasValue && c.CreatedAt.Value.Date == today);

                // If a customer ID is provided, filter by CustomerId
                if (supplierId > 0)
                {
                    purchasesQuery = purchasesQuery.Where(c => c.SupplierId == supplierId);
                }

                // Calculate Gross Total (Assuming Amount represents gross amount)
                var totalPaidAmount = purchasesQuery
                    .Sum(c => (decimal?)c.PaidAmount) ?? 0;

                // Calculate Net Total (Assuming Paid represents the net amount)
                var totalDue = purchasesQuery
                    .Sum(c => (decimal?)c.Due) ?? 0;

                // Display totals in your UI elements
                currentTotalPaidAmount.Text = Function.Helper.FormatAmount(totalPaidAmount.ToString());
                currentTotalDue.Text = Function.Helper.FormatAmount(totalDue.ToString());
            }
        }

        private void gridViewLatestSuppliers_RowClick(object sender, RowClickEventArgs e)
        {
            object id = this.gridViewLatestSuppliers.GetRowCellValue(this.gridViewLatestSuppliers.FocusedRowHandle, "Id");
            int supplierIdValue = int.Parse(id.ToString());

            Function.Sound.Selected();

            if (supplierIdValue != 0 && int.TryParse(supplierIdValue.ToString(), out int supplierId))
            {
                // Fetch sales for the selected customer if the CustomerId is valid
                if (supplierId > 0)
                {
                    this.GetSupplierPurchases(supplierId);
                    this.todayTotalPurchasesAndDue(supplierId);
                }
            }
        }

        private void latestSuppliersRefresh_Click(object sender, EventArgs e)
        {
            Sound.Selected();

            this.todayTotalPurchases();
            this.getLatestSuppliers();
            this.GetSupplierPurchases();
            this.todayTotalPurchasesAndDue();
        }

        private void newProduct_Click(object sender, EventArgs e)
        {
            ShowOverlay();
            Product.AddEditProduct addEditProduct = new Product.AddEditProduct();
            addEditProduct.setObject(this);
            addEditProduct.ShowDialog();
            HideOverlay();
        }

        private void quickActionCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tileViewProductsCards_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            if (Properties.Settings.Default.PosProductsWithoutImgs)
            {
                // Get stock quantity from the data source
                int stockQuantity = (int)tileViewProductsCards.GetRowCellValue(e.RowHandle, "Qty");

                // Define custom hex colors
                Color successColor = ColorTranslator.FromHtml("#34495e");
                Color textColor = ColorTranslator.FromHtml("#fff");
                Color dangerColor = ColorTranslator.FromHtml("#f39c12");

                if (stockQuantity > 0)
                {
                    // Set the background color for available stock
                    e.Item.AppearanceItem.Normal.BackColor = successColor;
                    e.Item.AppearanceItem.Normal.ForeColor = textColor;
                }
                else
                {
                    // Set the background color for out-of-stock items
                    e.Item.AppearanceItem.Normal.BackColor = dangerColor;
                }
            }
        }

        private void tileViewProductsCards_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            // Check if it's the unbound image column
            //if (e.Column == colImage && e.IsGetData)
            //{
            //    // Get stock availability from the data source
            //    bool isStockAvailable = (bool)tileViewProductsCards.GetListSourceRowCellValue(e.ListSourceRowIndex, "Qty");

            //    if (isStockAvailable)
            //    {
            //        e.Value = ;  // success color
            //    }
            //    else
            //    {
            //        e.Value = ;  // daner color
            //    }
            //}
        }

        private void quickCustomer_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            OverlayForm overlay = new OverlayForm(this);
            overlay.Show();
            Customer.AddEditCustomer addEditCustomer = new Customer.AddEditCustomer(this);
            addEditCustomer.FormClosed += (s, args) => overlay.Close();
            addEditCustomer.Show();
            addEditCustomer.TopMost = true;
        }
    }

    public class SaleViewModel
    {
        public string ReferenceNo { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? Due { get; set; }
        public int? CustomerId { get; set; }

        public List<SaleDetailViewModel> saleDetails { get; set; } = new List<SaleDetailViewModel>();
    }

    public class SaleDetailViewModel
    {
        public string ProductName { get; set; }
        public decimal? SaleQuantity { get; set; }
        public decimal? LineTotal { get; set; }
    }

    public class PurchaseViewModel
    {
        public string ReferenceNo { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? Due { get; set; }
        public int? SupplierId { get; set; }

        public List<PurchaseDetailViewModel> purchaseDetails { get; set; } = new List<PurchaseDetailViewModel>();
    }

    public class PurchaseDetailViewModel
    {
        public string ProductName { get; set; }
        public decimal? PurchaseQuantity { get; set; }
        public decimal? Total { get; set; }
    }
}