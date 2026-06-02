using DevExpress.DataAccess.Native.Excel;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Pos.Forms.Auth;
using Pos.Forms.BusinessLocation;
using Pos.Forms.Product.Warehouse;
using Pos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Controls;
using System.IO;

namespace Pos.Function
{
    internal class Helper
    {
        // style the grid view
        public static string RowColor = "#1abc9c";
        public static string FocusColor = "#3498db";
        public static string ClickedCellColor = "";
        public static string ClickedRowBgColor = "#16a085";
        public static int RowHeight = 30;
        public static int ColumnPanelRowHeight = 30;

        public PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        public void LoadCustomFont()
        {
            byte[] fontBytes = Properties.Resources.DIGITAL_7__ITALIC_;
            IntPtr fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontData);
        }

        public static BaseLayoutItem GetCurrentActiveTab(TabbedControlGroup tabbedControlGroup)
		{
			foreach (LayoutGroup tab in tabbedControlGroup.TabPages)
			{
				if (tab.Visible)
				{
					return tab;
				}
			}
			return null; // No active tab found
        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, int maxWidth, int maxHeight)
        {
            int width = image.Width;
            int height = image.Height;

            if (width > maxWidth || height > maxHeight)
            {
                // Calculate the scaling factor to maintain aspect ratio
                float scalingFactor = Math.Min((float)maxWidth / width, (float)maxHeight / height);

                width = (int)(width * scalingFactor);
                height = (int)(height * scalingFactor);
            }

            // Create a new bitmap with the desired size
            var resizedImage = new Bitmap(width, height);

            // Draw the original image onto the resized bitmap
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        public static void ProcessLoyaltyPoints(int saleId)
        {
            using (var context = new AppDbContext())
            {
                // Retrieve the sale information
                var sale = context.Sales.Include(s => s.Customer).FirstOrDefault(s => s.Id == saleId);
                if (sale == null || sale.Customer == null)
                {
                    // Handle missing sale or customer
                    return;
                }

                // Retrieve the customer's loyalty card
                var loyaltyCard = context.LoyaltyCards.Include(l => l.CardType).FirstOrDefault(l => l.CustomerId == sale.CustomerId);
                if (loyaltyCard == null || loyaltyCard.CardType == null)
                {
                    // Handle missing loyalty card or card type
                    return;
                }

                // Retrieve card type information
                var cardType = loyaltyCard.CardType;

                // Calculate points based on the sale amount
                decimal paidAmount = sale.PaidAmount.GetValueOrDefault(0m);
                decimal pointsPerCurrency = cardType.PointsPerCurrency.GetValueOrDefault(0m);
                decimal currencyPerPoint = cardType.CurrencyPerPoint.GetValueOrDefault(0m);

                // Initialize pointsToAward
                decimal pointsToAward = 0m;

                // Calculate points to award based on PointsPerCurrency
                if (pointsPerCurrency > 0m)
                {
                    pointsToAward = decimal.Round(paidAmount * pointsPerCurrency, 2); // Multiply and round to 2 decimal places
                }
                // Calculate points to award based on CurrencyPerPoint
                else if (currencyPerPoint > 0m)
                {
                    pointsToAward = decimal.Round(paidAmount / currencyPerPoint, 2); // Divide and round to 2 decimal places
                }

                // Update loyalty card points
                if (loyaltyCard.Points.HasValue)
                {
                    loyaltyCard.Points += pointsToAward;
                }
                else
                {
                    loyaltyCard.Points = pointsToAward;
                }

                // Create a new points transaction
                var pointsTransaction = new PointsTransaction
                {
                    LoyaltyCardId = loyaltyCard.Id,
                    TypeId = cardType.Id,
                    UserId = sale.UserId,  // Assuming there's a user associated with the sale
                    Amount = pointsToAward,
                    SaleId = sale.Id,
                    Date = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                context.PointsTransactions.Add(pointsTransaction);

                // Save changes to the database
                context.SaveChanges();
            }
        }

        public static byte[] ConvertToJpegWithCompression(System.Drawing.Image image, long quality)
        {
            using (var memoryStream = new MemoryStream())
            {
                var jpegEncoder = ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality); // Quality range: 0-100

                image.Save(memoryStream, jpegEncoder, encoderParameters);
                return memoryStream.ToArray();
            }
        }

        public static System.Drawing.Image ConvertTo8Bit(System.Drawing.Image image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(image, 0, 0);
            }

            return bmp;
        }

        public static byte[] GetOptimizedImageBytes(System.Drawing.Image image)
        {
            // Step 1: Resize the image if it's too large
            System.Drawing.Image resizedImage = ResizeImage(image, 800, 600); // Resize to a max of 800x600

            // Step 2: Convert to JPEG with compression
            byte[] optimizedBytes = ConvertToJpegWithCompression(resizedImage, 75L); // Adjust quality (0-100)

            // Step 3: Cleanup
            resizedImage.Dispose();

            return optimizedBytes;
        }

        public static string GeneratePromoCode(Func<string, bool> isUnique, int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string result;
            do
            {
                result = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (!isUnique(result));

            return result;
        }

        public static string generateSku()
        {
            string prefix = "SKU-";
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 3);
            string checksum = prefix + randomPart;

            return prefix + randomPart + checksum;
        }

        public static string generateRefNo(string code = "REF", int id = 1)
        {
            return $"{code}-{id.ToString().PadLeft(8, '0')}";
        }

        public static string generateCode(int id = 1)
        {
            return $"{id.ToString().PadLeft(8, '0')}";
        }

        public static string MonthNameByNumber(int monthNumber)
        {
            string[] monthNames = new string[]{"January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"};

            string monthName = (monthNumber >= 1 && monthNumber <= 12) ? monthNames[monthNumber - 1] : "Invalid month";
            
            return monthName;
        }

        public static string FormatAmount(string amount)
        {
            string currencySymbol = Properties.Settings.Default.DefaultCurrency;
            bool isRight = Properties.Settings.Default.DefaultCurrencyDirection;

            if (isRight)
            {
                return $"{amount} {currencySymbol}";
            }
            else
            {
                return $"{currencySymbol} {amount}";
            }
        }

        public static bool hasUser()
        {
            return Shared.db.Users.Count() == 0 ? false : true;
        }

        public static bool hasAlertQty()
        {
            var prducts = from p in Shared.db.Products
                          join pw in Shared.db.ProductWarehouses on p.Id equals pw.ProductId
                          where pw.Qty <= p.AlertQuantity
                          select new
                          {
                              p.Id,
                              p.Image,
                              p.ProductName,
                              p.AlertQuantity,
                              Quantity = pw.Qty,
                              p.SellingPrice
                          };
            return prducts.Count() > 0;
        }

        public static Models.Currency getDefualtCurrency()
        {
            return Shared.db.Currencies.FirstOrDefault(b => b.IsActive == true);
        }

        public static Models.BusinessLocation getDefualtBusinessLocation()
        {
            return Shared.db.BusinessLocations.FirstOrDefault(b => b.IsDefault == "Yes");
        }

        public static Models.BusinessLocation getBusinessLocationById(int id)
        {
            return Shared.db.BusinessLocations.Find(id);
        }

        public static bool IsRegisterOpen(int BusinessLocationId)
        {
            return Shared.db.RegisterRecords
           .Any(b => b.BusinessLocationId == BusinessLocationId && b.ClosedAt == null);
        }

		public static bool canSendMessageViaWhatsUp()
		{
			return Shared.db.Settings.First().WhatsAppStatus == "Active";
		}

		public static bool isPurchaseCodeActivated()
		{
			return Shared.db.Settings.First().PurchaseCodeCondition == "Active";
		}

		public static bool canSendEmail()
		{
			return Shared.db.Settings.First().MailStatus == "Active";
		}

		public static Setting getSetting()
        {
            return Shared.db.Settings.FirstOrDefault();
        }

        public static string ConvertToBase64String(byte[] imageBytes)
        {
            return Convert.ToBase64String(imageBytes);
        }

        public static string generateBarCode(int id = 0)
        {
            return $"{id.ToString().PadLeft(13, '0')}";
        }

        public static void CloseRegister(int userId, int BusinessLocationId)
        {
            // Attempt to retrieve the first open register record for the specified business location
            var registerRecord = Shared.db.RegisterRecords
                   .Where(b => b.BusinessLocationId == BusinessLocationId && b.ClosedAt == null)
                   .FirstOrDefault();

            // Check if a register record was found
            if (registerRecord != null)
            {
                // Set the user who is closing the register and the closing time
                registerRecord.ClosedById = userId;
                registerRecord.ClosedAt = DateTime.Now;

                // Save changes to the database
                Shared.db.SaveChanges();
            }
            else
            {
                // Optionally, handle the case where no open register record was found
                // You could log this situation, throw a custom exception, return a status, etc.
                XtraMessageBox.Show("No open register record found for the specified business location.");
            }
        }

        public static DataTable getTodaySummaryTable(DataTable dt, int BusinessLocationId)
        {
            try
            {
                // Attempt to retrieve the active register record for the specified business location
                RegisterRecord registerRecord = Shared.db.RegisterRecords
                       .Where(b => b.BusinessLocationId == BusinessLocationId && b.ClosedAt == null)
                       .FirstOrDefault();  // Using FirstOrDefault() to avoid exceptions if no records are found

                // Check if a register record is found
                if (registerRecord != null)
                {
                    // List of summary items to add to the DataTable
                    var summaryItems = new[]
                    {
                new { Section = "Total Cash Amount", Value = registerRecord.TotalCashAmount },
                new { Section = "Total Cash Submitted", Value = registerRecord.TotalCashSubmitted },
                new { Section = "Total Cheques", Value = registerRecord.TotalCheques },
                new { Section = "Total Cheques Amount", Value = registerRecord.TotalChequesAmount },
                new { Section = "Total Cheques Submitted", Value = registerRecord.TotalChequesSubmitted },
                new { Section = "Total Other Amount", Value = registerRecord.TotalOtherAmount },
                new { Section = "Total Refunds Amount", Value = registerRecord.TotalRefundsAmount },
                new { Section = "Total Expenses Amount", Value = registerRecord.TotalExpensesAmount },
                new { Section = "Total Gift Card Amount", Value = registerRecord.TotalGiftCardAmount },
                new { Section = "Total Return Orders Amount", Value = registerRecord.TotalReturnOrdersAmount },
                new { Section = "Cash In Hand", Value = registerRecord.CashInHand }
            };

                    // Adding each summary item as a new row in the DataTable
                    foreach (var item in summaryItems)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["Section"] = item.Section;
                        newRow["Value"] = item.Value + " " + Properties.Settings.Default.DefaultCurrency;
                        dt.Rows.Add(newRow);
                    }
                }
                else
                {
                    // Optionally handle the case where no active register record is found
                    Console.WriteLine("No active register record found for the specified business location.");
                }
            }
            catch (Exception ex)
            {
                // Log or handle exceptions if something goes wrong
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return dt;
        }

        public static string TodaySummaryMessage(int BusinessLocationId)
        {
            string table = "Today's Summary : \n";

            table = "Section                         Value     \n";

            RegisterRecord registerRecord = Shared.db.RegisterRecords
                   .Where(b => b.BusinessLocationId == BusinessLocationId)
                   .OrderByDescending(r => r.Id)
                   .First();

            table += "--------------------------------     --------    \n";

            if (registerRecord != null)
            {
                table += "Total Cash Amount               " + registerRecord.TotalCashAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Cash Submitted            " + registerRecord.TotalCashSubmitted + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Cheques                   " + registerRecord.TotalCheques + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Cheques Amount            " + registerRecord.TotalChequesAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Cheques Submitted         " + registerRecord.TotalChequesSubmitted + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Other Amount              " + registerRecord.TotalOtherAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Refunds Amount            " + registerRecord.TotalRefundsAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Expenses Amount           " + registerRecord.TotalExpensesAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Gift Card Amount          " + registerRecord.TotalGiftCardAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Total Return Orders Amount      " + registerRecord.TotalReturnOrdersAmount + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
                table += "Cash In Hand      " + registerRecord.CashInHand + " " + Properties.Settings.Default.DefaultCurrency + "        \n";
            }

            table += "--------------------------------     --------    \n";

            return table;
        }

        public static string CreateTodaySummaryMessage(int BusinessLocationId)
        {
            return TodaySummaryMessage(BusinessLocationId);
        }

        public static string CreateCloseRegisterMessage(int BusinessLocationId)
        {
            return TodaySummaryMessage(BusinessLocationId);
        }
    }
}
