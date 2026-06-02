using DevExpress.Mvvm.Native;
using DevExpress.XtraBars;
using DevExpress.XtraPrinting;
using Microsoft.EntityFrameworkCore;
//using Pos.Forms.Reporting;
//using Pos.Forms.SettingForms;
using Pos.Function;
using Pos.Models;
using Pos.Properties;
//using Pos.Services;
//using Pos.Services.Invoices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Pos.Forms.Sale
{
    public partial class OrderPayment : DevExpress.XtraEditors.XtraForm
    {
        private readonly int? OrderId = null;
        private readonly int? TempId = null;

        public OrderPayment(int? orderId, int? orderTempId)
        {
            OrderId = orderId;
            TempId = orderTempId;
            InitializeComponent();
        }

        private void BtnClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }

        private void Pay()
        {
            //if (!dxValidationProvider1.Validate())
            //{
            //    return;
            //}
            //using AppDbContext db = new();
            //if (TempId == null)
            //{
            //    Order order = db.Orders
            //        .Include(o => o.OrderGlasses)
            //        .Include(o => o.OrderItems)
            //        .SingleOrDefault(o => o.Id == OrderId);

            //    if (order == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        return;
            //    }

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    if (order.Rest < amount)
            //    {
            //        amount -= decimal.Parse(txtChange.EditValue.ToString());
            //    }

            //    db.Payments.Add(new Payment
            //    {
            //        ClientId = order.ClientId,
            //        OrderId = order.Id,
            //        PaymentId = null,
            //        Amount = amount,
            //        OldDue = order.Rest - amount,
            //        UserId = Shared.User.Id,
            //        MethodId = (int)cbxMethod.EditValue,
            //        Date = DateTime.Now
            //    });


            //    order.IsConfiremed = true;
            //    order.UserId = Shared.User.Id;
            //    db.Orders.Update(order);
            //    db.SaveChanges();

            //    if (IsRedeemed)
            //    {
            //        LoyaltyCardService.RedeemPoints(order.Id);
            //    }
            //    else
            //    {
            //        LoyaltyCardService.RemoveRedeemPoints(order.Id);
            //    }
            //    LoyaltyCardService.EarningPoints(order.Id);

            //    PrintInvoice(order.Id);

            //    PrintCorrections(order.Id);

            //    Shared.CreatedSound();
            //    DialogResult = DialogResult.Yes;
            //    Close();
            //}
            //else
            //{
            //    OrderTemp orderTemp = db.OrderTemps
            //        .Include(o => o.OrderGlassTemps)
            //        .Include(o => o.OrderItemTemps)
            //        .SingleOrDefault(o => o.Id == TempId);
            //    if (orderTemp == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        return;
            //    }

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    if (orderTemp.Rest < amount)
            //    {
            //        amount -= decimal.Parse(txtChange.EditValue.ToString());
            //    }
            //    if (amount < orderTemp.Rest && orderTemp.ClientId == Settings.Default.POSDefaultUser)
            //    {
            //        FlyoutDialogHelper.Information(this, "Le client par défaut ne peut pas s'endetter.");
            //        return;
            //    }

            //    Order order = new();
            //    if (OrderId == null)
            //    {
            //        order.ClientId = orderTemp.ClientId;
            //        order.Discount = orderTemp.Discount;
            //        order.WithTva = orderTemp.WithTva;
            //        order.PrescriptionId = orderTemp.PrescriptionId;
            //        order.UserId = Shared.User.Id;
            //        order.IsConfiremed = true;
            //        order.CreatedAt = DateTime.Now;
            //        order.UpdatedAt = DateTime.Now;

            //        db.Orders.Add(order);
            //        db.SaveChanges();

            //        orderTemp.OrderGlassTemps.ForEach(item =>
            //        {
            //            Glass glass = db.Glasses.Find(item.GlassId);

            //            glass.Stock -= item.Qte;

            //            OrderGlass OrderGlass = new()
            //            {
            //                OrderId = order.Id,
            //                GlassId = item.GlassId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva
            //            };

            //            db.OrderGlasses.Add(OrderGlass);
            //        });

            //        orderTemp.OrderItemTemps.ForEach(item =>
            //        {
            //            Item glass = db.Items.Find(item.ItemId);

            //            if (item.Qte <= glass.Stock)
            //            {
            //                glass.Stock -= item.Qte;
            //            }
            //            else
            //            {
            //                FlyoutDialogHelper.Information(this, $"{item.Name} has Only {glass.Stock} in stock!");
            //                item.Qte = glass.Stock;
            //                glass.Stock = 0;
            //            }

            //            db.OrderItems.Add(new OrderItem
            //            {
            //                OrderId = order.Id,
            //                ItemId = item.ItemId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });

            //        db.SaveChanges();

            //        DialogResult = DialogResult.Yes;
            //        Close();
            //    }
            //    else
            //    {
            //        order = db.Orders
            //            .Include(x => x.OrderGlasses).ThenInclude(x => x.Glass)
            //            .Include(x => x.OrderItems).ThenInclude(x => x.Item)
            //            .Include(x => x.Payments)
            //            .Include(o => o.Invoices)
            //            .SingleOrDefault(x => x.Id == OrderId);

            //        if (order.ClientId != orderTemp.ClientId)
            //        {
            //            foreach (Payment payment in order.Payments)
            //            {
            //                payment.ClientId = orderTemp.ClientId;
            //                db.Payments.Update(payment);
            //            }
            //        }

            //        order.ClientId = orderTemp.ClientId;
            //        order.Discount = orderTemp.Discount;
            //        order.WithTva = orderTemp.WithTva;
            //        order.PrescriptionId = orderTemp.PrescriptionId;
            //        order.IsConfiremed = true;
            //        order.UserId = Shared.User.Id;
            //        order.UpdatedAt = DateTime.Now;

            //        order.OrderGlasses.ForEach(og =>
            //        {
            //            og.Glass.Stock += og.Qte;
            //            db.OrderGlasses.Remove(og);
            //        });

            //        order.OrderItems.ForEach(og =>
            //        {
            //            og.Item.Stock += og.Qte;
            //            db.OrderItems.Remove(og);
            //        });

            //        orderTemp.OrderGlassTemps.ForEach(item =>
            //        {
            //            Glass glass = db.Glasses.Find(item.GlassId);

            //            glass.Stock -= item.Qte;

            //            order.OrderGlasses.Add(new OrderGlass
            //            {
            //                GlassId = item.GlassId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });
            //        orderTemp.OrderItemTemps.ForEach(item =>
            //        {
            //            Item glass = db.Items.Find(item.ItemId);

            //            if (item.Qte <= glass.Stock)
            //            {
            //                glass.Stock -= item.Qte;
            //            }
            //            else
            //            {
            //                FlyoutDialogHelper.Information(this, $"{item.Name} has Only {glass.Stock} in stock!");
            //                item.Qte = glass.Stock;
            //                glass.Stock = 0;
            //            }

            //            order.OrderItems.Add(new OrderItem
            //            {
            //                ItemId = item.ItemId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });

            //        db.SaveChanges();
            //    }
            //    if (IsRedeemed)
            //    {
            //        LoyaltyCardService.RedeemPoints(order.Id);
            //    }
            //    else
            //    {
            //        LoyaltyCardService.RemoveRedeemPoints(order.Id);
            //    }

            //    LoyaltyCardService.EarningPoints(order.Id);

            //    db.Payments.Add(new Payment
            //    {
            //        ClientId = order.ClientId,
            //        OrderId = order.Id,
            //        PaymentId = null,
            //        Amount = amount,
            //        OldDue = order.Rest - amount,
            //        UserId = Shared.User.Id,
            //        MethodId = (int)cbxMethod.EditValue,
            //        Date = DateTime.Now
            //    });
            //    db.SaveChanges();
            //    PrintInvoice(order.Id);
            //    PrintCorrections(order.Id);



            //    Shared.CreatedSound();
            //    DialogResult = DialogResult.Yes;
            //    Close();
            //}
        }

        private void BtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (!dxValidationProvider1.Validate())
            //{
            //    return;
            //}
            //using AppDbContext db = new();
            //if (TempId == null)
            //{
            //    Order order = db.Orders
            //        .Include(o => o.OrderGlasses)
            //        .Include(o => o.OrderItems)
            //        .SingleOrDefault(o => o.Id == OrderId);

            //    if (order == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        return;
            //    }

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    if (order.Rest < amount)
            //    {
            //        amount -= decimal.Parse(txtChange.EditValue.ToString());
            //    }

            //    db.Payments.Add(new Payment
            //    {
            //        ClientId = order.ClientId,
            //        OrderId = order.Id,
            //        PaymentId = null,
            //        Amount = amount,
            //        OldDue = order.Rest - amount,
            //        UserId = Shared.User.Id,
            //        MethodId = (int)cbxMethod.EditValue,
            //        Date = DateTime.Now
            //    });


            //    order.IsConfiremed = true;
            //    order.UserId = Shared.User.Id;
            //    db.Orders.Update(order);
            //    db.SaveChanges();

            //    if (IsRedeemed)
            //    {
            //        LoyaltyCardService.RedeemPoints(order.Id);
            //    }
            //    else
            //    {
            //        LoyaltyCardService.RemoveRedeemPoints(order.Id);
            //    }
            //    LoyaltyCardService.EarningPoints(order.Id);

            //    PrintInvoice(order.Id);

            //    PrintCorrections(order.Id);

            //    Shared.CreatedSound();
            //    DialogResult = DialogResult.Yes;
            //    Close();
            //}
            //else
            //{
            //    OrderTemp orderTemp = db.OrderTemps
            //        .Include(o => o.OrderGlassTemps)
            //        .Include(o => o.OrderItemTemps)
            //        .SingleOrDefault(o => o.Id == TempId);
            //    if (orderTemp == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        return;
            //    }

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    if (orderTemp.Rest < amount)
            //    {
            //        amount -= decimal.Parse(txtChange.EditValue.ToString());
            //    }
            //    if (amount < orderTemp.Rest && orderTemp.ClientId == Settings.Default.POSDefaultUser)
            //    {
            //        FlyoutDialogHelper.Information(this, "Le client par défaut ne peut pas s'endetter.");
            //        return;
            //    }

            //    Order order = new();
            //    if (OrderId == null)
            //    {
            //        order.ClientId = orderTemp.ClientId;
            //        order.Discount = orderTemp.Discount;
            //        order.WithTva = orderTemp.WithTva;
            //        order.UserId = Shared.User.Id;
            //        order.IsConfiremed = true;
            //        order.CreatedAt = DateTime.Now;
            //        order.UpdatedAt = DateTime.Now;

            //        db.Orders.Add(order);
            //        db.SaveChanges();

            //        orderTemp.OrderGlassTemps.ForEach(item =>
            //        {
            //            Glass glass = db.Glasses.Find(item.GlassId);

            //            glass.Stock -= item.Qte;

            //            OrderGlass OrderGlass = new()
            //            {
            //                OrderId = order.Id,
            //                GlassId = item.GlassId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva
            //            };

            //            db.OrderGlasses.Add(OrderGlass);
            //        });

            //        orderTemp.OrderItemTemps.ForEach(item =>
            //        {
            //            Item glass = db.Items.Find(item.ItemId);

            //            if (item.Qte <= glass.Stock)
            //            {
            //                glass.Stock -= item.Qte;
            //            }
            //            else
            //            {
            //                FlyoutDialogHelper.Information(this, $"{item.Name} has Only {glass.Stock} in stock!");
            //                item.Qte = glass.Stock;
            //                glass.Stock = 0;
            //            }

            //            db.OrderItems.Add(new OrderItem
            //            {
            //                OrderId = order.Id,
            //                ItemId = item.ItemId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });

            //        db.SaveChanges();

            //        DialogResult = DialogResult.Yes;
            //        Close();
            //    }
            //    else
            //    {
            //        order = db.Orders
            //            .Include(x => x.OrderGlasses).ThenInclude(x => x.Glass)
            //            .Include(x => x.OrderItems).ThenInclude(x => x.Item)
            //            .Include(x => x.Payments)
            //            .Include(o => o.Invoices)
            //            .SingleOrDefault(x => x.Id == OrderId);

            //        if (order.ClientId != orderTemp.ClientId)
            //        {
            //            foreach (Payment payment in order.Payments)
            //            {
            //                payment.ClientId = orderTemp.ClientId;
            //                db.Payments.Update(payment);
            //            }
            //        }

            //        order.ClientId = orderTemp.ClientId;
            //        order.Discount = orderTemp.Discount;
            //        order.WithTva = orderTemp.WithTva;
            //        order.IsConfiremed = true;
            //        order.UserId = Shared.User.Id;
            //        order.UpdatedAt = DateTime.Now;

            //        order.OrderGlasses.ForEach(og =>
            //        {
            //            og.Glass.Stock += og.Qte;
            //            db.OrderGlasses.Remove(og);
            //        });

            //        order.OrderItems.ForEach(og =>
            //        {
            //            og.Item.Stock += og.Qte;
            //            db.OrderItems.Remove(og);
            //        });

            //        orderTemp.OrderGlassTemps.ForEach(item =>
            //        {
            //            Glass glass = db.Glasses.Find(item.GlassId);

            //            glass.Stock -= item.Qte;

            //            order.OrderGlasses.Add(new OrderGlass
            //            {
            //                GlassId = item.GlassId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });
            //        orderTemp.OrderItemTemps.ForEach(item =>
            //        {
            //            Item glass = db.Items.Find(item.ItemId);

            //            if (item.Qte <= glass.Stock)
            //            {
            //                glass.Stock -= item.Qte;
            //            }
            //            else
            //            {
            //                FlyoutDialogHelper.Information(this, $"{item.Name} has Only {glass.Stock} in stock!");
            //                item.Qte = glass.Stock;
            //                glass.Stock = 0;
            //            }

            //            order.OrderItems.Add(new OrderItem
            //            {
            //                ItemId = item.ItemId,
            //                Name = item.Name,
            //                Price = item.Price,
            //                PurchasePrice = item.PurchasePrice,
            //                Qte = item.Qte,
            //                Tva = item.Tva,
            //            });
            //        });

            //        db.SaveChanges();
            //    }
            //    if (IsRedeemed)
            //    {
            //        LoyaltyCardService.RedeemPoints(order.Id);
            //    }
            //    else
            //    {
            //        LoyaltyCardService.RemoveRedeemPoints(order.Id);
            //    }

            //    LoyaltyCardService.EarningPoints(order.Id);

            //    db.Payments.Add(new Payment
            //    {
            //        ClientId = order.ClientId,
            //        OrderId = order.Id,
            //        PaymentId = null,
            //        Amount = amount,
            //        OldDue = order.Rest - amount,
            //        UserId = Shared.User.Id,
            //        MethodId = (int)cbxMethod.EditValue,
            //        Date = DateTime.Now
            //    });
            //    db.SaveChanges();
            //    PrintInvoice(order.Id);
            //    PrintCorrections(order.Id);



            //    Shared.CreatedSound();
            //    DialogResult = DialogResult.Yes;
            //    Close();
            //}

        }

        private static void PrintCorrections(int orderId)
        {
            //using AppDbContext db = new();
            //Order order = db.Orders
            //    .Include(x => x.OrderGlasses)
            //    .SingleOrDefault(x => x.Id == orderId);
            //if (order.OrderGlasses.Count == 0)
            //{
            //    return;
            //}

            //CorrectionA5Report report = new();

            //report.setInvoiceId((int)order.Id);
            //report.PrinterName = Settings.Default.DefaultPrinter80mm;
            //report.CreateDocument();

            //if (Settings.Default.PrintA5Preview == true)
            //{
            //    CorrectionDocumentViewer documentViewer = new();
            //    documentViewer.documentViewer1.PrintingSystem = report.PrintingSystem;
            //    documentViewer.documentViewer1.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToPageWidth, []);
            //    documentViewer.ShowDialog();
            //}
            //else
            //{
            //    report.Print();
            //}
        }

        private void PrintInvoice(int orderId)
        {
            //using AppDbContext db = new();
            //Order order = db.Orders
            //    .Include(x => x.OrderGlasses)
            //    .Include(x => x.OrderItems)
            //    .Include(x => x.Client)
            //    .Include(x => x.Invoices)
            //    .SingleOrDefault(x => x.Id == orderId);

            //Invoice invoice;

            //if (order.Invoices.Count > 0)
            //{
            //    invoice = InvoiceService.UpdateInvoice(order.Invoices.First().Id);

            //    InvoiceService.DeleteAllItems(invoice.Id);
            //}
            //else
            //{
            //    invoice = InvoiceService.CreateInvoice(order.Id);
            //}

            //foreach (OrderItem item in order.OrderItems)
            //{
            //    InvoiceItemService.CreateInvoiceItem(invoice.Id, item.Name, (int)item.Qte, (decimal)(item.Price + item.Tva));
            //}

            //foreach (OrderGlass item in order.OrderGlasses)
            //{
            //    InvoiceItemService.CreateInvoiceItem(invoice.Id, item.Name, (int)item.Qte, (decimal)(item.Price + item.Tva));
            //}

            //while (string.IsNullOrEmpty(Settings.Default.DefaultPrinter80mm))
            //{
            //    if (FlyoutDialogHelper.Information(this, "Aucune imprimante par défaut définie. Voulez-vous en configurer?") != DialogResult.Yes) return;

            //    DefaultPrintersSettingsForm form = new();
            //    form.ShowDialog();
            //}

            //Invoice80mmReport report80mm = new();
            //report80mm.SetInvoiceId(invoice.Id);
            //report80mm.PrinterName = Settings.Default.DefaultPrinter80mm;
            //report80mm.CreateDocument();
            //report80mm.Print();
        }

        void LoadMethods()
        {
            //using AppDbContext db = new();
            //DataTable dt = new();

            //dt.Columns.Add("Id");
            //dt.Columns.Add("Name");

            //foreach (Method item in db.Methods.ToList())
            //{
            //    dt.Rows.Add(item.Id, Shared.GetSeedResourceText(item.Name));
            //}
            //cbxMethod.Properties.DataSource = dt;
            //cbxMethod.Properties.DisplayMember = "Name";
            //cbxMethod.Properties.ValueMember = "Id";

            //bool check = false;
            //POSForm form = new();

            //while (!check || form.DialogResult == DialogResult.Cancel)
            //{
            //    int defaultId = Settings.Default.PurchaseDefaultMethod;
            //    Method method = db.Methods.Find(defaultId);
            //    if (method != null)
            //    {
            //        cbxMethod.EditValue = method.Id;
            //        check = true;
            //    }
            //    else
            //    {
            //        FlyoutDialogHelper.Information(this, "Methode par defaut est indefini!, veuillez le definer!");
            //        form.ShowDialog();
            //    }
            //}
        }

        void LoadData()
        {
            //LoadMethods();

            //using AppDbContext db = new();

            //if (TempId == null)
            //{
            //    Order order = db.Orders
            //        .Include(o => o.Client).ThenInclude(c => c.LoyaltyCards).ThenInclude(l => l.CardType)
            //        .Include(x => x.PointsTransactions)
            //        .SingleOrDefault(o => o.Id == OrderId);

            //    if (order == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        Close();
            //        return;
            //    }

            //    if (order.PointsTransactions.Any(x => x.Type == 0))
            //    {
            //        IsRedeemed = true;
            //    }

            //    txtOrderId.Text = order.Id.ToString();
            //    txtClient.Text = order.Client.Name;

            //    txtAmount.Text = order.Rest.ToString();

            //    lblRest.Text = order.Rest.ToString();
            //    lblTotal.Text = order.Total.ToString();
            //    lblPaid.Text = order.TotalPaid.ToString();

            //    if (!order.Client.LoyaltyCards.Any(x => x.IsActive == true))
            //    {
            //        //layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //lblEarnedPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        return;
            //    }
            //    LoyaltyCard card = order.Client.LoyaltyCards.First();

            //    btnRedeem.Enabled = true;

            //    txtPoints.Text = LoyaltyCardService.GetCardPoints(card.Id).ToString();
            //    txtRedeemPoints.Text = LoyaltyCardService.GetPointsToCurrency(card.Id).ToString();

            //    if (IsRedeemed)
            //    {
            //        btnRedeem.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            //        btnRedeem.Text = "Remove Redeem";
            //    }
            //    else
            //    {
            //        btnRedeem.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            //        btnRedeem.Text = "Redeem";
            //    }

            //    lblEarnedPoints.Text = ((decimal)(order.Total / card.CardType.PointsPerCurrency)).ToString("0.##");
            //}
            //else
            //{
            //    OrderTemp order = db.OrderTemps
            //        .Include(o => o.Client).ThenInclude(c => c.LoyaltyCards).ThenInclude(l => l.CardType)
            //        .SingleOrDefault(o => o.Id == TempId);

            //    if (order == null)
            //    {
            //        FlyoutDialogHelper.Information(this, "Commande introuvable");
            //        Close();
            //        return;
            //    }

            //    txtOrderId.Text = order.Id.ToString();
            //    txtClient.Text = order.Client.Name;

            //    txtAmount.Text = order.Rest.ToString();

            //    lblRest.Text = order.Rest.ToString();
            //    lblTotal.Text = order.Total.ToString();
            //    lblPaid.Text = order.TotalPaid.ToString();

            //    if (!order.Client.LoyaltyCards.Any(x => x.IsActive == true))
            //    {
            //        //layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //simpleLabelItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        //lblEarnedPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //        return;
            //    }
            //    LoyaltyCard card = order.Client.LoyaltyCards.First();

            //    btnRedeem.Enabled = true;

            //    txtPoints.Text = LoyaltyCardService.GetCardPoints(card.Id).ToString();
            //    txtRedeemPoints.Text = LoyaltyCardService.GetPointsToCurrency(card.Id).ToString();

            //    if (order.RedeemAmount > 0)
            //    {
            //        btnRedeem.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            //        btnRedeem.Text = "Remove Redeem";
            //    }
            //    else
            //    {
            //        btnRedeem.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            //        btnRedeem.Text = "Redeem";
            //    }

            //    lblEarnedPoints.Text = ((decimal)(order.Total / card.CardType.PointsPerCurrency)).ToString("0.##");
            //}
        }

        private void OrderPayment_Load(object sender, EventArgs e)
        {
            txtAmount.Focus();
            txtAmount.Select();
            LoadData();
            //ShortcutsNames();
        }
        #region SHORTCUTS
        private void ShortcutsNames()
        {
            //KeyDown += Form_KeyDown;
            //KeyPreview = true;
            ////btnAdd.Hint = SHORTCUTS.ENTER.DisplayName;
            //btnRedeem.Text += "\n" + SHORTCUTS.UPDATE.DisplayName;
            ////btnClose.Hint = SHORTCUTS.CANCEL.DisplayName;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == SHORTCUTS.ENTER.Key)
            //{
            //    btnAdd.PerformClick();
            //}
            //if (e.KeyCode == SHORTCUTS.UPDATE.Key)
            //{
            //    btnRedeem.PerformClick();
            //}
            //if (e.KeyCode == SHORTCUTS.CANCEL.Key)
            //{
            //    Close();
            //}
        }
        #endregion

        private void BtnRedeem_Click(object sender, EventArgs e)
        {
            //if (FlyoutDialogHelper.Confirmation(this, description: "Voulez-vous vraiment appliquer le redeem?") != DialogResult.Yes) return;
            //using AppDbContext db = new();
            //Order order = db.Orders.Find(OrderId);
            //if (order != null)
            //{
            //    FlyoutDialogHelper.Information(this, description: "Voulez-vous vraiment appliquer le redeem?");
            //    return;
            //}

            //IsRedeemed = !IsRedeemed;
            //if (IsRedeemed)
            //{
            //    lblTotal.Text = (decimal.Parse(lblTotal.Text) - decimal.Parse(txtRedeemPoints.Text)).ToString("0.##");
            //    lblRest.Text = (decimal.Parse(lblTotal.Text) - decimal.Parse(lblPaid.Text)).ToString("0.##");
            //}
            //else
            //{
            //    lblTotal.Text = (decimal.Parse(lblTotal.Text) + decimal.Parse(txtRedeemPoints.Text)).ToString("0.##");
            //    lblRest.Text = (decimal.Parse(lblTotal.Text) - decimal.Parse(lblPaid.Text)).ToString("0.##");
            //}

            //txtAmount.EditValue = lblRest.Text;
            //txtChange.EditValue = (decimal.Parse(txtAmount.EditValue.ToString()) - decimal.Parse(lblRest.Text)).ToString("0.##");
        }

        private void TxtAmount_EditValueChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtAmount.Text)) return;

            //using AppDbContext db = new();
            //if (TempId == null)
            //{
            //    Order order = db.Orders.Find(OrderId);

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    txtChange.EditValue = amount - order.Rest;
            //}
            //else
            //{
            //    OrderTemp order = db.OrderTemps.Find(TempId);

            //    decimal amount = decimal.Parse(txtAmount.EditValue.ToString());

            //    txtChange.EditValue = amount - order.Rest;
            //}
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Pay();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}