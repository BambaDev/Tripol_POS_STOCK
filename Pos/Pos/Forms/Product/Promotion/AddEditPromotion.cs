using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using DevExpress.XtraSplashScreen;
using Pos.Forms.Alert;
using Pos.Forms.BusinessLocation;
using Pos.Function;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pos.Forms.Product.Promotion
{
    public partial class AddEditPromotion : DevExpress.XtraEditors.XtraForm
    {
        public Promotions promotions = null;
        public string type = "Add";
        public int currentItemId = 0;

        public AddEditPromotion()
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

        public void setPromotionsObject(Promotions promotions)
        {
            this.promotions = promotions;
        }

        public void setTypeOperation(string type)
        {
            this.type = type;
        }

        public void edit()
        {
            if (this.promotions != null || currentItemId != 0)
            {
                this.currentItemId = this.promotions != null ? this.promotions.promotion_id : currentItemId;

                Models.Promotion printer = Shared.db.Promotions.Find(this.currentItemId);

                if (printer != null)
                {
                    txtTitle.Text = printer.Title;
                    txtStartDate.EditValue = printer.StartDate;
                    txtEndDate.EditValue = printer.EndDate;
                    txtType.EditValue = printer.Type;
                    txtQty.EditValue = printer.Qty;
                    txtDiscount.EditValue = printer.Discount;
                    txtStatus.EditValue = printer.Status;
                    txtPromotionCode.Text = printer.PromotionCode;
                    txtProduct.EditValue = printer.ProductId;
                    btnSelect.Enabled = true;
                }
                else
                {
                    btnSelect.Enabled = false;
                    Function.Sound.Wrong();
                    XtraMessageBox.Show("Please select item !");
                }
            }
        }

        public void getProducts()
        {
            txtProduct.Properties.DataSource = Shared.db.Products.ToList();
            txtProduct.Properties.DisplayMember = "ProductName"; // Set display member
            txtProduct.Properties.ValueMember = "Id"; // Set value member
        }

        public bool IsUniquePromoCode(string code)
        {
            return !Shared.db.Promotions.Any(p => p.PromotionCode == code);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Wait), true, true, false);

            if (dxValidationProvider.Validate())
            {
                Models.Promotion promotion;

                if (this.type == "Add")
                {
                    var promoCode = Function.Helper.GeneratePromoCode(IsUniquePromoCode,6);

                    promotion = new Models.Promotion();
                    promotion.Title = txtTitle.Text;
                    promotion.StartDate = DateTime.Parse(txtStartDate.EditValue.ToString());
                    promotion.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                    promotion.Type = txtType.EditValue.ToString();
                    promotion.Qty = int.Parse(txtQty.EditValue.ToString());
                    promotion.Discount = decimal.Parse(txtDiscount.EditValue.ToString());
                    promotion.Status = txtStatus.EditValue.ToString();
                    promotion.PromotionCode = txtPromotionCode.Text;
                    promotion.ProductId = int.Parse(txtProduct.EditValue.ToString());
                    promotion.CreatedAt = DateTime.Now;
                    promotion.UpdatedAt = DateTime.Now;

                    Shared.db.Promotions.Add(promotion);

                    txtTitle.Text = "";
                }
                else
                {
                    if (this.promotions != null)
                        this.currentItemId = this.promotions.promotion_id;

                    promotion = Shared.db.Promotions.Find(this.currentItemId);

                    if (promotion != null)
                    {
                        promotion.Title = txtTitle.Text;
                        promotion.StartDate = DateTime.Parse(txtStartDate.EditValue.ToString());
                        promotion.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                        promotion.Type = txtType.EditValue.ToString();
                        promotion.Qty = int.Parse(txtQty.EditValue.ToString());
                        promotion.Discount = decimal.Parse(txtDiscount.EditValue.ToString());
                        promotion.Status = txtStatus.EditValue.ToString();
                        promotion.PromotionCode = txtPromotionCode.Text;
                        promotion.ProductId = int.Parse(txtProduct.EditValue.ToString());
                        promotion.UpdatedAt = DateTime.Now;

                        Shared.db.Entry(promotion).State = EntityState.Modified;
                    }
                    else
                    {
                        Function.Sound.Wrong();
                        XtraMessageBox.Show("Please select item !");
                    }
                }

                Shared.db.SaveChanges();

                Function.Sound.Added();

                if (this.promotions != null)
                    this.promotions.loadPromotions();
            }
            else
            {
                Function.Sound.Wrong();
            }

            SplashScreenManager.CloseForm();
        }

        private void AddEditPromotion_Load(object sender, EventArgs e)
        {
            txtStatus.EditValue = "Active";
            txtType.EditValue = "Sales Promotion";

            txtPromotionCode.Text = Function.Helper.GeneratePromoCode(IsUniquePromoCode,6);

            this.getProducts();

            if (this.type != "Add")
            {
                this.edit();
            }

            this.InitializeSearchLookUpEdit();
        }

        private Models.Promotion GetCurrentData()
        {
            if (currentItemId != 0)
                return Shared.db.Promotions.Find(currentItemId);
            else
                return null;
        }

        private void DisplayCurrentItem()
        {
            Sound.Selected();

            if (currentItemId == 0)
            {
                // If no current selection, disable all navigation buttons.
                btnPrev.Enabled = false;
                btnNext.Enabled = false;
                btnStart.Enabled = false;
                btnEnd.Enabled = false;
                return;
            }

            // Retrieve the minimum and maximum ID values from the Brands dataset.
            int minId = Shared.db.Promotions.Min(b => b.Id);
            int maxId = Shared.db.Promotions.Max(b => b.Id);

            // Enable or disable navigation buttons based on the current record's ID.
            btnPrev.Enabled = currentItemId > minId; // Disable if on the first item
            btnNext.Enabled = currentItemId < maxId; // Disable if on the last item
            btnEnd.Enabled = currentItemId > minId; // Disable if on the first item (start of the list)
            btnStart.Enabled = currentItemId < maxId; // Disable if on the last item (end of the list)

            this.type = "Edit";

            Models.Promotion currentItem = GetCurrentData();

            if (currentItem != null)
            {
                this.currentItemId = currentItem.Id;
                txtTitle.Text = currentItem.Title;
                txtStartDate.EditValue = currentItem.StartDate;
                txtEndDate.EditValue = currentItem.EndDate;
                txtType.EditValue = currentItem.Type;
                txtQty.EditValue = currentItem.Qty;
                txtDiscount.EditValue = currentItem.Discount;
                txtStatus.EditValue = currentItem.Status;
                txtPromotionCode.Text = currentItem.PromotionCode;
                txtProduct.EditValue = currentItem.ProductId;
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        private void MoveToFirst()
        {
            try
            {
                int? minId = Shared.db.Promotions.Min(b => (int?)b.Id);
                if (minId.HasValue)
                {
                    currentItemId = minId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the first item: " + ex.Message);
            }
        }

        private void MoveToLast()
        {
            try
            {
                int? maxId = Shared.db.Promotions.Max(b => (int?)b.Id);
                if (maxId.HasValue)
                {
                    currentItemId = maxId.Value;
                    DisplayCurrentItem();
                }
                else
                {
                    MessageBox.Show("No entries found in DB.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve the last item: " + ex.Message);
            }
        }

        private void MoveToNext()
        {
            if (currentItemId == 0)
            {
                this.MoveToFirst();
            }

            var nextItem = Shared.db.Promotions.Where(b => b.Id > currentItemId).OrderBy(b => b.Id).FirstOrDefault();
            if (nextItem != null)
            {
                currentItemId = nextItem.Id;
                DisplayCurrentItem();
            }
        }

        private void MoveToPrevious()
        {
            if (currentItemId != 0)
            {
                var prevItem = Shared.db.Promotions.Where(b => b.Id < currentItemId).OrderByDescending(b => b.Id).FirstOrDefault();
                if (prevItem != null)
                {
                    currentItemId = prevItem.Id;
                    DisplayCurrentItem();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MoveToLast();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            MoveToFirst();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            MoveToNext();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            MoveToPrevious();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Sound.Added();
            this.type = "Add";

            txtTitle.Text = string.Empty;
            txtQty.EditValue = 0;

            btnPrev.Enabled = true;
            btnNext.Enabled = true;
            btnStart.Enabled = true;
            btnEnd.Enabled = true;
        }

        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeSearchLookUpEdit()
        {
            searchLookUpEdit.Properties.DataSource = Shared.db.Currencies.ToList();
            searchLookUpEdit.Properties.DisplayMember = "Name";
            searchLookUpEdit.Properties.ValueMember = "Id";

            searchLookUpEdit.Properties.View.Columns.Clear();
            searchLookUpEdit.Properties.View.Columns.AddVisible("Name", "Name");

            searchLookUpEdit.Properties.NullText = "Write something...";

            searchLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        private void searchLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (searchLookUpEdit.EditValue != null)
            {
                if (int.TryParse(searchLookUpEdit.EditValue.ToString(), out int itemId))
                {
                    this.currentItemId = itemId;
                    this.edit();
                }
                else
                {
                    Console.WriteLine("Selected value is not a valid integer");
                }
            }
            else
            {
                Console.WriteLine("No item selected.");
            }
        }

        private void btnGeneratePromoCode_Click(object sender, EventArgs e)
        {
            Sound.Selected();
            txtPromotionCode.Text = Function.Helper.GeneratePromoCode(IsUniquePromoCode,6);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}