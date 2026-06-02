using Pos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class DatabaseSeeder
    {
        public static void SeedProductWarehouse()
        {
            using(var context = new AppDbContext())
            {
                if (context.ProductWarehouses.Any())
                {
                    return;
                }

                var variant = context.Variations.First();
                var productBatch = context.ProductBatches.First();
                var warehouse = context.Warehouses.First();

                var productWarehouses = new List<ProductWarehouse>
                    {
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "248").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 16, // Quantity from the product data
                            Price = 450.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "113").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 4500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "109").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 10000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "110").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 7500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "111").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 1100.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "112").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 1100.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "623").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 98, // Quantity from the product data
                            Price = 0.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "176").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 9500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "8").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 15000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "202").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "247").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 28, // Quantity from the product data
                            Price = 145.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "249").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 28, // Quantity from the product data
                            Price = 145.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "151").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 116, // Quantity from the product data
                            Price = 156.81m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "150").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 45, // Quantity from the product data
                            Price = 146.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "124").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 6, // Quantity from the product data
                            Price = 150.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "231").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 3, // Quantity from the product data
                            Price = 3300.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "373").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 64000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "125").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 5000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "123").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 1200.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "246").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 7, // Quantity from the product data
                            Price = 1750.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "242").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 2900.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "241").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 10, // Quantity from the product data
                            Price = 990.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "105").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 15, // Quantity from the product data
                            Price = 570.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "362").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 5000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "156").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 9000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "356").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 19, // Quantity from the product data
                            Price = 6100.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1552").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 0.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "898").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 2000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "896").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 2000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "897").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 2000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "9").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 3850.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "371").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 0.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "120").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 10000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "10").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 20, // Quantity from the product data
                            Price = 3700.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "119").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 6000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "117").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 7, // Quantity from the product data
                            Price = 22000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "236").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 36000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "283").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 10000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "167").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "128").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 8000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "234").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 39500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "11").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 19000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "12").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 28500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "235").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 39500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "13").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 4, // Quantity from the product data
                            Price = 21000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "349").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 48000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "148").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 13, // Quantity from the product data
                            Price = 869.47m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "149").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 5, // Quantity from the product data
                            Price = 1150.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "118").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 3500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "144").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 8, // Quantity from the product data
                            Price = 710.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "143").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 7, // Quantity from the product data
                            Price = 708.33m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "372").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 0.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "212").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1122").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1255").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 38000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "240").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 9, // Quantity from the product data
                            Price = 770.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "362_G").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 74000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "286").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 24000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1340").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 74000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "14").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 35500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "135").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 4, // Quantity from the product data
                            Price = 53000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "15").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 105000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "16").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 22000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "17").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 22000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "18").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 8, // Quantity from the product data
                            Price = 34187.5m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "19").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 38000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "177").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 33500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "145").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 12, // Quantity from the product data
                            Price = 1680.29m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "146").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 3900.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "147").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 5, // Quantity from the product data
                            Price = 5350.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "330").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 12000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "296").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 63000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1049").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "344").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "345").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1302").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 28000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "20").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 31000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "142_D").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "282_A").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 50000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "209").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "193").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 48000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "146_S").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 42000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1476").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "355").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 4, // Quantity from the product data
                            Price = 29500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1451").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "332").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "163").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 23000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "264").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "141").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 175000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "388").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 35000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "186").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 25000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "194_J").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 67000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "21").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "284").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 52000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1297").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 64000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1294").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 43000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1030").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 60000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "153").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 39000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "169").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 25000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "180").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 68000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "23").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "22").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "192").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 57000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "150_E").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "157").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "151_H").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "24").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "204").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "292").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 38000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "293").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "295").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1543").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 155000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "387").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 115000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "276").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 39000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "385").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "290").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "196").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 67000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "331").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 69000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "218").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 39000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "222").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 44000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "166_W").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 67500.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "224").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 44000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "147_B").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "375").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 50000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "347").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 60000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "348").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "165").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 70000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "394").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "148_S").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 35000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "210").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "170").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 68000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "281").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "223").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 44000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1301").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "225").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 42000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "327").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "384").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 77000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "323").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "144_X").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 2, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "143_F").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "173").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 64000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "26").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1579").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 82000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "131").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 95000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "220").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 85000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "188").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 85000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "27").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "28").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 100000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "152_V").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 39000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "259").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 42000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "29").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "25").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 42000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "30").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 62000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "187").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "155").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "98").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "158").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 35000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "41").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "42").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 85000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "43").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 70000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "31").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 105000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "195").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 90000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "214_B").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 70000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "291").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1292").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 38000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "275").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 30000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1531").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "33").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "322").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 140000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "389").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "179").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 68000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1544").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 90000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "289").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 45000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "382").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 68000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1377").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 20000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "200").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 55000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1434").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 58000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "279").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "343").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "237").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 100000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1052").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "312").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 25000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "391").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "228").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 44000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "298").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 18000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "329").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "199").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 80000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "280").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 25000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1477").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 85000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "266").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 125000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1413").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 90000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "324").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 160000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "288").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 1, // Quantity from the product data
                            Price = 65000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "35").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 25000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1296").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 64000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "198").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 70000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "328").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 68000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "1103").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 40000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "272").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 48000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new ProductWarehouse
                        {
                            ProductId = context.Products.First(p => p.Sku == "44").Id, // Retrieve product by Sku
                            ProductBatchId = productBatch?.Id, // Nullable
                            VariantId = variant?.Id, // Nullable
                            WarehouseId = warehouse.Id, // Set the warehouse ID
                            Qty = 65, // Quantity from the product data
                            Price = 75000.0m, // Price from the product data (PurchasePriceExcTax)
                            ImeiNumber = "123456789012345", // Example IMEI number (can be customized)
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                    };

                context.ProductWarehouses.AddRange(productWarehouses);
                context.SaveChanges();
            }
        }

        public static void Seed(AppDbContext context)
        {
            if (context.Settings.Any())
            {
                return;
            }

            // Seed Settings (Example Data)
            var setting = new Setting
            {
                WhatsAppStatus = "Active",
                AccountSid = "your_account_sid_here",
                AuthToken = "your_auth_token_here",
                FromPhoneNumber = "+1234567890",
                WhatsAppMaintStatus = "Enabled",
                WhatsAppMaintInvoiceStatus = "Enabled",
                WhatsAppPhoneCode = "+1",
                MailMailer = "smtp",
                MailStatus = "Active",
                MailAllowHtml = "true",
                MailHost = "smtp.example.com",
                MailPort = 587,
                MailUsername = "your_email@example.com",
                MailPassword = "your_password_here",
                PurchaseCodeCondition = "Valid",
                PurchaseCode = "ABC-1234-XYZ",
                Company = "Example Company",
                Description = "A sample description for the company.",
                Title = "Company Title",
                SubTitle = "Company Subtitle",
                Email = "info@example.com",
                Website = "https://www.example.com",
                Address = "123 Main St, City, Country",
                Tel = "+1234567890",
                Fax = "+0987654321",
                Compte = "123456789",
                Rib = "987654321",
                Nis = "654321",
                Rc = "RC-123456",
                Ai = "AI-654321",
                IdFiscal = "IDF-123456",
                IsLockScreen = true,
                IsSoundAdded = true,
                IsSoundDeleted = true,
                IsSoundSelected = false,
                IsSoundDenied = false,
                IsSoundWrong = true,
                IsQuantityPopUp = true,
                PosTopBanner = true,
                PosBottomBanner = true,
                PosCategories = true,
                PosCategoriesWithoutImgs = false,
                PosProductsWithoutImgs = false,
                PoslayoutControlGroupLatestOrders = true,
                PoslayoutControlGroupLatestCustomers = true,
                PoslayoutControlGroupLatestSuppliers = true,
                PoslayoutControlGroupSalesReturns = true,
                PoslayoutControlGroupHold = true,
                PoslayoutControlGroupUnpaidOrders = true,
                PrinterReciept = "Reciept Printer",
                PrinterRecieptId = null, // Assuming related Printer record exists
                PrinterDocument = "Document Printer",
                PrinterDocumentId = null, // Assuming related Printer record exists
                Lang = "en",
                UpdatedAt = DateTime.UtcNow
            };

            context.Settings.Add(setting);

            // Save changes to the database
            context.SaveChanges();

            // Seed Countries (Example Data)
            var countries = new List<Country>
            {
                new Country
                {
                    Name = "United States",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Country
                {
                    Name = "Canada",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Countries.AddRange(countries);

            // Save changes to the database
            context.SaveChanges();

            // Ensure that countries are seeded first
            var usa = context.Countries.FirstOrDefault(c => c.Name == "United States");
            var canada = context.Countries.FirstOrDefault(c => c.Name == "Canada");

            if (usa == null || canada == null)
            {
                throw new InvalidOperationException("Countries must be seeded before seeding states.");
            }

            // Seed States (Example Data)
            var states = new List<State>
            {
                new State
                {
                    Name = "New York",
                    CountryId = usa.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new State
                {
                    Name = "California",
                    CountryId = usa.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new State
                {
                    Name = "Ontario",
                    CountryId = canada.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.States.AddRange(states);

            // Save changes to the database
            context.SaveChanges();

            // Ensure that states are seeded first
            var newYorkState = context.States.FirstOrDefault(s => s.Name == "New York");
            var californiaState = context.States.FirstOrDefault(s => s.Name == "California");
            var ontarioState = context.States.FirstOrDefault(s => s.Name == "Ontario");

            if (newYorkState == null || californiaState == null || ontarioState == null)
            {
                throw new InvalidOperationException("States must be seeded before seeding cities.");
            }

            // Seed Cities (Example Data)
            var cities = new List<City>
            {
                new City
                {
                    Name = "New York City",
                    StateId = newYorkState.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new City
                {
                    Name = "Los Angeles",
                    StateId = californiaState.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new City
                {
                    Name = "Toronto",
                    StateId = ontarioState.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Cities.AddRange(cities);

            // Save changes to the database
            context.SaveChanges();

            // Seed Roles (Example Data)
            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = "User",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = "Manager",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Role
                {
                    Name = "Supervisor",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Roles.AddRange(roles);

            // Save changes to the database
            context.SaveChanges();

            // Seed Business Locations (Example Data)
            var businessLocations = new List<BusinessLocation>
            {
                new BusinessLocation
                {
                    Name = "Head Office",
                    LocationId = "BL001",
                    Landmark = "Near Central Park",
                    City = "New York",
                    ZipCode = "10001",
                    State = "NY",
                    Country = "USA",
                    Mobile = "123-456-7890",
                    AlternateContactNumber = "098-765-4321",
                    Email = "headoffice@company.com",
                    Website = "https://company.com",
                    IsDefault = "True",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new BusinessLocation
                {
                    Name = "Branch Office",
                    LocationId = "BL002",
                    Landmark = "Downtown",
                    City = "Los Angeles",
                    ZipCode = "90001",
                    State = "CA",
                    Country = "USA",
                    Mobile = "321-654-9870",
                    AlternateContactNumber = "789-456-1230",
                    Email = "branchoffice@company.com",
                    Website = "https://branch.company.com",
                    IsDefault = "False",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.BusinessLocations.AddRange(businessLocations);

            // Save changes to the database
            context.SaveChanges();

            var registers = new List<Register>
            {
                new Register
                {
                    Code = "REG001",
                    Name = "Main Register",
                    Opened = "Yes",
                    BusinessLocationId = 1, // Assuming BusinessLocation with Id 1 exists
                    DeviceId = "DEV12345",
                    TerminalId = "TERM001",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Register
                {
                    Code = "REG002",
                    Name = "Secondary Register",
                    Opened = "No",
                    BusinessLocationId = 2, // Assuming BusinessLocation with Id 2 exists
                    DeviceId = "DEV67890",
                    TerminalId = "TERM002",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Registers.AddRange(registers);

            // Save changes to the database
            context.SaveChanges();

            // Ensure Business Locations are seeded (if not done previously)
            var headOffice = context.BusinessLocations.FirstOrDefault(bl => bl.Name == "Head Office");
            var branchOffice = context.BusinessLocations.FirstOrDefault(bl => bl.Name == "Branch Office");

            if (headOffice == null || branchOffice == null)
            {
                throw new InvalidOperationException("Business locations must be seeded before seeding users.");
            }

            // Ensure Roles are seeded (if not done previously)
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");

            if (adminRole == null || userRole == null)
            {
                throw new InvalidOperationException("Roles must be seeded before seeding users.");
            }

            // Seed Users (Example Data)
            var users = new List<User>
            {
                new User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    FullName = "John Doe",
                    UserLogin = "admin",
                    Email = "johndoe@example.com",
                    Phone = "123-456-7890",
                    Password = Function.PasswordHelper.HashPassword("123456"),
                    PinOne = "0",
                    PinTwo = "0",
                    PinThree = "0",
                    PinFour = "0",
                    Status = "Active",
                    Gender = "Male",
                    IsAdmin = "True",
                    BusinessLocationId = headOffice.Id,
                    RoleId = adminRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    FullName = "Jane Smith",
                    UserLogin = "user",
                    Email = "janesmith@example.com",
                    Phone = "987-654-3210",
                    Password = Function.PasswordHelper.HashPassword("123456"),
                    PinOne = "0",
                    PinTwo = "0",
                    PinThree = "0",
                    PinFour = "0",
                    Status = "Active",
                    Gender = "Female",
                    IsAdmin = "False",
                    BusinessLocationId = branchOffice.Id,
                    RoleId = userRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Users.AddRange(users);

            // Save changes to the database
            context.SaveChanges();

            // Seed Departments (Example Data)
            var departments = new List<Department>
            {
                new Department
                {
                    DepartmentName = "Human Resources",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Department
                {
                    DepartmentName = "IT",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Department
                {
                    DepartmentName = "Finance",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Departments.AddRange(departments);

            // Save changes to the database
            context.SaveChanges();

            // Seed Positions (Example Data)
            var positions = new List<Position>
            {
                new Position
                {
                    Title = "Manager",
                    Salary = 75000.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Position
                {
                    Title = "Software Engineer",
                    Salary = 95000.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Position
                {
                    Title = "Accountant",
                    Salary = 65000.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Positions.AddRange(positions);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities are seeded (Department, Position, etc.)
            var departmentHR = context.Departments.FirstOrDefault(d => d.DepartmentName == "Human Resources");
            var positionManager = context.Positions.FirstOrDefault(p => p.Title == "Manager");

            if (headOffice == null || departmentHR == null || positionManager == null)
            {
                throw new InvalidOperationException("Business locations, departments, and positions must be seeded before seeding employees.");
            }

            // Seed Employees (Example Data)
            var employees = new List<Employee>
            {
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    NameOfFather = "Richard Doe",
                    NameOfMother = "Elizabeth Doe",
                    Address = "1234 Main St, New York, NY",
                    Height = 1.80m,
                    Weight = 75.5m,
                    DateOfBirth = new DateTime(1990, 1, 1),
                    FamilySituation = "Married",
                    BloodGroup = "O+",
                    Civility = "Mr.",
                    Email = "john.doe@example.com",
                    PhoneNumber = "123-456-7890",
                    HireDate = new DateOnly(2015, 5, 15),
                    Assured = true,
                    NoCard = "CARD123",
                    CardDeliveryAt = DateTime.UtcNow.AddYears(-3),
                    NoPass = "PASS123",
                    PassDeliveryAt = DateTime.UtcNow.AddYears(-2),
                    BlackList = false,
                    SpouseName = "Jane Doe",
                    ShortBiography = "John has been a key part of the HR team for several years.",
                    ChildrenCount = 2,
                    EmergencyContactPhone = "987-654-3210",
                    EmergencyContactRelation = "Spouse",
                    EmergencyContactName = "Jane Doe",
                    EducationLevel = "Bachelor's Degree",
                    ExperienceYears = "10",
                    PreviousEmployer = "TechCorp",
                    WorkHoursPerWeek = 40,
                    VacationDays = 20,
                    SickDays = 5,
                    LastPromotionDate = DateTime.UtcNow.AddYears(-1),
                    LinkedInProfile = "https://linkedin.com/in/johndoe",
                    EmploymentType = "Full-time",
                    Gender = "Male",
                    CodePostal = "10001",
                    Code = "EMP001",
                    BusinessLocationId = headOffice.Id,
                    DepartmentId = departmentHR.Id,
                    PositionId = positionManager.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    NameOfFather = "Michael Smith",
                    NameOfMother = "Sarah Smith",
                    Address = "5678 Oak St, Los Angeles, CA",
                    Height = 1.65m,
                    Weight = 60m,
                    DateOfBirth = new DateTime(1985, 7, 10),
                    FamilySituation = "Single",
                    BloodGroup = "A+",
                    Civility = "Ms.",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "987-654-3210",
                    HireDate = new DateOnly(2018, 3, 1),
                    Assured = true,
                    NoCard = "CARD124",
                    CardDeliveryAt = DateTime.UtcNow.AddYears(-4),
                    NoPass = "PASS124",
                    PassDeliveryAt = DateTime.UtcNow.AddYears(-3),
                    BlackList = false,
                    SpouseName = null,
                    ShortBiography = "Jane is a dedicated member of the IT department.",
                    ChildrenCount = 0,
                    EmergencyContactPhone = "123-456-7890",
                    EmergencyContactRelation = "Parent",
                    EmergencyContactName = "Sarah Smith",
                    EducationLevel = "Master's Degree",
                    ExperienceYears = "8",
                    PreviousEmployer = "Tech Solutions",
                    WorkHoursPerWeek = 40,
                    VacationDays = 25,
                    SickDays = 3,
                    LastPromotionDate = DateTime.UtcNow.AddYears(-2),
                    LinkedInProfile = "https://linkedin.com/in/janesmith",
                    EmploymentType = "Full-time",
                    Gender = "Female",
                    CodePostal = "90001",
                    Code = "EMP002",
                    BusinessLocationId = headOffice.Id,
                    DepartmentId = departmentHR.Id,
                    PositionId = positionManager.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Employees.AddRange(employees);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (BusinessLocation, Employee, User) are seeded
            var businessLocation = context.BusinessLocations.FirstOrDefault(bl => bl.Name == "Head Office");
            var employee = context.Employees.FirstOrDefault(e => e.FirstName == "John");
            var user = context.Users.FirstOrDefault(u => u.UserLogin == "Admin");

            // Seed Warranties (Example Data)
            var warranties = new List<Warranty>
            {
                new Warranty
                {
                    Name = "Standard Warranty",
                    Description = "Covers repairs for 1 year",
                    Duration = 12, // In months
                    Type = "Limited",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Warranty
                {
                    Name = "Extended Warranty",
                    Description = "Covers repairs for 2 years with additional services",
                    Duration = 24, // In months
                    Type = "Full",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Warranty
                {
                    Name = "Lifetime Warranty",
                    Description = "Covers repairs and replacements for the lifetime of the product",
                    Duration = null, // Lifetime, no set duration
                    Type = "Lifetime",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Warranties.AddRange(warranties);

            // Save changes to the database
            context.SaveChanges();

            // Seed Warehouses (Example Data)
            var warehouses = new List<Warehouse>
            {
                new Warehouse
                {
                    Name = "Main Warehouse",
                    Phone = "123-456-7890",
                    Email = "mainwarehouse@example.com",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Warehouse
                {
                    Name = "Backup Warehouse",
                    Phone = "987-654-3210",
                    Email = "backupwarehouse@example.com",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Warehouse
                {
                    Name = "Overstock Warehouse",
                    Phone = "555-555-5555",
                    Email = "overstockwarehouse@example.com",
                    Status = "Inactive",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Warehouses.AddRange(warehouses);

            // Save changes to the database
            context.SaveChanges();

            if (user == null || headOffice == null)
            {
                throw new InvalidOperationException("User and BusinessLocation must be seeded before seeding vehicles.");
            }

            // Seed Variations (Example Data)
            var variations = new List<Variation>
            {
                new Variation
                {
                    Name = "Color",
                    Value = "Red",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Variation
                {
                    Name = "Size",
                    Value = "Large",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Variations.AddRange(variations);

            // Save changes to the database
            context.SaveChanges();

            // Ensure that variations are already seeded
            var colorVariation = context.Variations.FirstOrDefault(v => v.Name == "Color");
            var sizeVariation = context.Variations.FirstOrDefault(v => v.Name == "Size");

            if (colorVariation == null || sizeVariation == null)
            {
                throw new InvalidOperationException("Variations must be seeded before seeding variation values.");
            }

            // Seed VariationValues (Example Data)
            var variationValues = new List<VariationValue>
            {
                new VariationValue
                {
                    Value = "Red",
                    VariationId = colorVariation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new VariationValue
                {
                    Value = "Blue",
                    VariationId = colorVariation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new VariationValue
                {
                    Value = "Large",
                    VariationId = sizeVariation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new VariationValue
                {
                    Value = "Medium",
                    VariationId = sizeVariation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.VariationValues.AddRange(variationValues);

            // Save changes to the database
            context.SaveChanges();

            // Ensure that Users and Roles are already seeded
            var adminUser = context.Users.FirstOrDefault(u => u.UserLogin == "johndoe");
            var standardUser = context.Users.FirstOrDefault(u => u.UserLogin == "janedoe");

            // Ensure that Users and Permissions are already seeded
            var addCustomerPermission = context.Permissions.FirstOrDefault(p => p.Name == "Add Customer");
            var editCustomerPermission = context.Permissions.FirstOrDefault(p => p.Name == "Edit Customer");

            // Seed Units (Example Data)
            var units = new List<Unit>
            {
                new Unit
                {
                    Name = "Kilogram",
                    Symbol = "kg",
                    Description = "Kilograms, typically used for weight",
                    IsDivisible = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Unit
                {
                    Name = "Piece",
                    Symbol = "pc",
                    Description = "Pieces, typically used for countable items",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Unit
                {
                    Name = "Liter",
                    Symbol = "L",
                    Description = "Liters, typically used for volume",
                    IsDivisible = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Units.AddRange(units);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (Employee) are already seeded
            if (employee == null)
            {
                throw new InvalidOperationException("Employees must be seeded before seeding transport records.");
            }

            // Ensure related entities (User, FromWarehouse, ToWarehouse) are already seeded
            var fromWarehouse = context.Warehouses.FirstOrDefault(w => w.Name == "Main Warehouse");
            var toWarehouse = context.Warehouses.FirstOrDefault(w => w.Name == "Backup Warehouse");

            if (user == null || fromWarehouse == null || toWarehouse == null)
            {
                throw new InvalidOperationException("Users and Warehouses must be seeded before seeding transfers.");
            }

            // Seed Transfers (Example Data)
            var transfers = new List<Transfer>
            {
                new Transfer
                {
                    ReferenceNo = "TRF001",
                    Action = "Transfer Out",
                    UserId = user.Id,
                    Status = "Completed",
                    FromWarehouseId = fromWarehouse.Id,
                    ToWarehouseId = toWarehouse.Id,
                    Item = 1, // Example item count
                    TotalQty = 50,
                    TotalTax = 5.00m,
                    TotalCost = 500.00m,
                    ShippingCost = 20.00m,
                    GrandTotal = 525.00m,
                    Doc = "Document details",
                    Note = "First transfer from Main Warehouse to Backup Warehouse",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Transfer
                {
                    ReferenceNo = "TRF002",
                    Action = "Transfer In",
                    UserId = user.Id,
                    Status = "Pending",
                    FromWarehouseId = toWarehouse.Id,
                    ToWarehouseId = fromWarehouse.Id,
                    Item = 2, // Example item count
                    TotalQty = 1,
                    TotalTax = 10.00m,
                    TotalCost = 1000.00m,
                    ShippingCost = 50.00m,
                    GrandTotal = 1060.00m,
                    Doc = "Document details",
                    Note = "Second transfer from Backup Warehouse to Main Warehouse",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Transfers.AddRange(transfers);

            // Save changes to the database
            context.SaveChanges();

            // Seed Customer Tiers (Example Data)
            var tiers = new List<CustomerTier>
            {
                new CustomerTier
                {
                    Name = "Silver",
                    Description = "Entry level tier with basic benefits.",
                    ThresholdPoints = 500.00m,
                    Benefits = "5% Discount on all products.",
                    PointMultiplier = 1.0m,
                    DiscountRate = 5.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new CustomerTier
                {
                    Name = "Gold",
                    Description = "Mid-tier level with added benefits.",
                    ThresholdPoints = 1500.00m,
                    Benefits = "10% Discount on all products.",
                    PointMultiplier = 1.5m,
                    DiscountRate = 10.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new CustomerTier
                {
                    Name = "Platinum",
                    Description = "Top-tier level with maximum benefits.",
                    ThresholdPoints = 3000.00m,
                    Benefits = "15% Discount on all products.",
                    PointMultiplier = 2.0m,
                    DiscountRate = 15.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.CustomerTiers.AddRange(tiers);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (User, CustomerTier, Country, etc.) are already seeded
            var tierLevel = context.CustomerTiers.FirstOrDefault(t => t.Name == "Gold");
            var country = context.Countries.FirstOrDefault(c => c.Name == "United States");
            var state = context.States.FirstOrDefault(s => s.Name == "New York");
            var city = context.Cities.FirstOrDefault(ci => ci.Name == "New York City");

            if (user == null || tierLevel == null || country == null || state == null || city == null)
            {
                throw new InvalidOperationException("Users, Customer Tiers, Country, State, and City must be seeded before seeding customers.");
            }

            // Seed Customers (Example Data)
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "John",
                    LastName = "Doe",
                    FullName = "John Doe",
                    Email = "johndoe@example.com",
                    Phone = "123-456-7890",
                    Gender = "Male",
                    Status = "Active",
                    Address = "123 Main St, New York, NY",
                    TotalPointsAccumulated = 1000.50m,
                    CurrentPoints = 500.25m,
                    CurrentDue = 150.00m,
                    Code = "CUST001",
                    TierLevelId = tierLevel.Id,
                    CountryId = country.Id,
                    StateId = state.Id,
                    CityId = city.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Customer
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    FullName = "Jane Smith",
                    Email = "janesmith@example.com",
                    Phone = "987-654-3210",
                    Gender = "Female",
                    Status = "Active",
                    Address = "456 Oak St, New York, NY",
                    TotalPointsAccumulated = 2000.00m,
                    CurrentPoints = 1000.50m,
                    CurrentDue = 250.00m,
                    Code = "CUST002",
                    TierLevelId = tierLevel.Id,
                    CountryId = country.Id,
                    StateId = state.Id,
                    CityId = city.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Customers.AddRange(customers);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (Customer) are already seeded
            var customer1 = context.Customers.FirstOrDefault(c => c.Code == "CUST001");
            var customer2 = context.Customers.FirstOrDefault(c => c.Code == "CUST002");

            if (customer1 == null || customer2 == null)
            {
                throw new InvalidOperationException("Customers must be seeded before seeding transactions.");
            }

            // Seed Transactions (Example Data)
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Date = DateTime.UtcNow.AddDays(-5),
                    Amount = 250.00m,
                    PointsEarned = 50.00m,
                    PointsUsed = 0.00m,
                    TransactionType = "Purchase",
                    CustomerId = customer1.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Transaction
                {
                    Date = DateTime.UtcNow.AddDays(-3),
                    Amount = 150.00m,
                    PointsEarned = 30.00m,
                    PointsUsed = 10.00m,
                    TransactionType = "Purchase",
                    CustomerId = customer2.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Transaction
                {
                    Date = DateTime.UtcNow.AddDays(-1),
                    Amount = 100.00m,
                    PointsEarned = 20.00m,
                    PointsUsed = 5.00m,
                    TransactionType = "Purchase",
                    CustomerId = customer1.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Transactions.AddRange(transactions);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (User) are already seeded
            if (user == null)
            {
                throw new InvalidOperationException("Users must be seeded before seeding technicals.");
            }

            // Seed Taxes (Example Data)
            var taxes = new List<Tax>
            {
                new Tax
                {
                    Name = "No Tax",
                    Rate = 0,  // Example rate (0%)
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Tax
                {
                    Name = "Sales Tax",
                    Rate = 7.50m,  // Example rate (7.5%)
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Tax
                {
                    Name = "VAT",
                    Rate = 20.00m, // Example rate (20%)
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Tax
                {
                    Name = "Service Tax",
                    Rate = 5.00m, // Example rate (5%)
                    Status = "Inactive",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Taxes.AddRange(taxes);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (User) are already seeded
            if (user == null)
            {
                throw new InvalidOperationException("Users must be seeded before seeding suppliers.");
            }

            // Seed Suppliers (Example Data)
            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@example.com",
                    Gender = "Male",
                    Status = "Active",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Supplier
                {
                    FirstName = "Emily",
                    LastName = "Johnson",
                    Email = "emily.johnson@example.com",
                    Gender = "Female",
                    Status = "Inactive",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Suppliers.AddRange(suppliers);

            // Save changes to the database
            context.SaveChanges();

            // Seed Skills (Example Data)
            var skills = new List<Skill>
            {
                new Skill
                {
                    Name = "Plumbing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Skill
                {
                    Name = "Electrical Work",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Skill
                {
                    Name = "Carpentry",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Skill
                {
                    Name = "HVAC Maintenance",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Skills.AddRange(skills);

            // Save changes to the database
            context.SaveChanges();

            // Seed PriceGroups (Example Data)
            var priceGroups = new List<PriceGroup>
            {
                new PriceGroup
                {
                    Name = "Wholesale",
                    Description = "Wholesale pricing for regular customers.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PriceGroup
                {
                    Name = "Retail",
                    Description = "Retail pricing for loyal customers.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.PriceGroups.AddRange(priceGroups);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (BusinessLocation) are already seeded
            if (businessLocation == null)
            {
                throw new InvalidOperationException("Business locations must be seeded before seeding printers.");
            }

            // Seed Printers (Example Data)
            var printers = new List<Printer>
            {
                new Printer
                {
                    Title = "Receipt Printer",
                    Type = "Thermal",
                    CharactersPerLine = "48",
                    PrinterIpAddress = "192.168.1.100",
                    PrinterPort = 9100,
                    BusinessLocationId = businessLocation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Printer
                {
                    Title = "Document Printer",
                    Type = "Laser",
                    CharactersPerLine = "80",
                    PrinterIpAddress = "192.168.1.101",
                    PrinterPort = 9100,
                    BusinessLocationId = businessLocation.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Printers.AddRange(printers);

            // Save changes to the database
            context.SaveChanges();

            // Seed Categories (Example Data)
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Electronics",
                    Description = "Devices and gadgets",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Furniture",
                    Description = "Home and office furniture",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Appliances",
                    Description = "Household appliances",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Clothing",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Laptops",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Desktops & All-in-Ones",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Monitors",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Gaming",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "PC Accessories",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Electronics",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Parts, Batteries & Upgrades",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Software",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Services",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Support",
                    Description = "Men's and women's clothing",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Categories.AddRange(categories);

            // Save changes to the database
            context.SaveChanges();

            // Seed Brands (Example Data)
            var brands = new List<Brand>
            {
                new Brand
                {
                    Name = "Sony",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Brand
                {
                    Name = "Samsung",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Brand
                {
                    Name = "Apple",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Brand
                {
                    Name = "LG",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Brands.AddRange(brands);

            // Save changes to the database
            context.SaveChanges();

            // Seed Currencies (Example Data)
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Name = "US Dollar",
                    Code = "USD",
                    ExchangeRate = 1.00m,  // Base currency
                    Direction = true,  // True indicates positive exchange rate
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Euro",
                    Code = "EUR",
                    ExchangeRate = 0.85m,  // Exchange rate relative to USD
                    Direction = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "British Pound",
                    Code = "GBP",
                    ExchangeRate = 0.75m,  // Exchange rate relative to USD
                    Direction = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Japanese Yen",
                    Code = "JPY",
                    ExchangeRate = 110.00m,  // Exchange rate relative to USD
                    Direction = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Currencies.AddRange(currencies);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (User, Warehouse) are already seeded
            var warehouse = context.Warehouses.FirstOrDefault(w => w.Name == "Main Warehouse");

            if (user == null || warehouse == null)
            {
                throw new InvalidOperationException("Users and Warehouses must be seeded before seeding adjustments.");
            }

            // Seed Adjustments (Example Data)
            var adjustments = new List<Adjustment>
            {
                new Adjustment
                {
                    ReferenceNo = "ADJ-1001",
                    Doc = "ADJ-1001-Doc",
                    Action = "Add",
                    TotalQty = 50,
                    Item = 5,  // Assuming it's related to item count
                    Note = "Adding initial stock.",
                    WarehouseId = warehouse.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Adjustment
                {
                    ReferenceNo = "ADJ-1002",
                    Doc = "ADJ-1002-Doc",
                    Action = "Remove",
                    TotalQty = 10,
                    Item = 1,  // Assuming it's related to item count
                    Note = "Removed damaged stock.",
                    WarehouseId = warehouse.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Adjustments.AddRange(adjustments);

            // Save changes to the database
            context.SaveChanges();

            // Seed Assets (Example Data)
            var assets = new List<Asset>
            {
                new Asset
                {
                    AssetName = "Laptop",
                    Description = "Dell Inspiron 15",
                    SerialNumber = "SN123456789",
                    PurchaseDate = new DateOnly(2022, 3, 15),
                    WarrantyDate = new DateOnly(2025, 3, 15),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Asset
                {
                    AssetName = "Projector",
                    Description = "Epson EB-S41",
                    SerialNumber = "SN987654321",
                    PurchaseDate = new DateOnly(2021, 1, 10),
                    WarrantyDate = new DateOnly(2023, 1, 10),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Asset
                {
                    AssetName = "Printer",
                    Description = "HP LaserJet Pro",
                    SerialNumber = "SN567891234",
                    PurchaseDate = new DateOnly(2020, 6, 20),
                    WarrantyDate = new DateOnly(2023, 6, 20),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Assets.AddRange(assets);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (Employee, User) are already seeded
            if (employee == null || user == null)
            {
                throw new InvalidOperationException("Employees and Users must be seeded before seeding attendances.");
            }

            // Seed Attendance Records (Example Data)
            var attendances = new List<Attendance>
            {
                new Attendance
                {
                    EmployeeId = employee.Id,
                    UserId = user.Id,
                    Date = new DateOnly(2024, 10, 1),
                    TimeIn = new TimeOnly(9, 0), // 9:00 AM
                    TimeOut = new TimeOnly(17, 0), // 5:00 PM
                    HoursWorked = 8.00m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Attendance
                {
                    EmployeeId = employee.Id,
                    UserId = user.Id,
                    Date = new DateOnly(2024, 10, 2),
                    TimeIn = new TimeOnly(9, 0), // 9:00 AM
                    TimeOut = new TimeOnly(17, 30), // 5:30 PM
                    HoursWorked = 8.50m,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Attendances.AddRange(attendances);

            // Save changes to the database
            context.SaveChanges();

            // Ensure related entities (Customer, User, BusinessLocation) are already seeded
            var customer = context.Customers.FirstOrDefault(c => c.FirstName == "John" && c.LastName == "Doe");

            if (customer == null || user == null || businessLocation == null)
            {
                throw new InvalidOperationException("Customers, Users, and Business Locations must be seeded before seeding banks.");
            }

            // Ensure related entities (Brand, Category, Unit, Warranty) are already seeded
            var brand = context.Brands.FirstOrDefault(b => b.Name == "Apple");
            var category = context.Categories.FirstOrDefault(c => c.Name == "Electronics");
            var unit = context.Units.FirstOrDefault(u => u.Name == "Piece");
            var warranty = context.Warranties.FirstOrDefault(w => w.Name == "Standard Warranty");

            // Ensure none of them are null before proceeding with product seeding
            if (brand == null || category == null || unit == null || warranty == null)
            {
                throw new InvalidOperationException("Brand, Category, Unit, and Warranty must be seeded before seeding products.");
            }

            // Seed Products (Example Data)

            var products = new List<Product>
            {

                new Product
                {
                    ProductName = "ADAPTER HDMI TO VGA",
                    Sku = "248",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ADAPTER HDMI TO VGA",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN248",
                    ProductType = "General",
                    InitialQuantity = 16,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 450.0m,
                    PurchasePriceIncTax = 495.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 900m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ALIMENTATION ADVANCE 480W MAX ",
                    Sku = "113",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ALIMENTATION ADVANCE 480W MAX ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN113",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 4500.0m,
                    PurchasePriceIncTax = 4950.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 5500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ALIMENTATION ANTEC 500W",
                    Sku = "109",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ALIMENTATION ANTEC 500W",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN109",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 10000.0m,
                    PurchasePriceIncTax = 11000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 12500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ALIMENTATION AURES 500W",
                    Sku = "110",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ALIMENTATION AURES 500W",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN110",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 7500.0m,
                    PurchasePriceIncTax = 8250.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 8500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ALIMENTATION HYDRO PRO 500W",
                    Sku = "111",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ALIMENTATION HYDRO PRO 500W",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN111",
                    ProductType = "General",
                    InitialQuantity = 2,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 1100.0m,
                    PurchasePriceIncTax = 1210.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 14300m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ALIMENTATION SIMPLE ",
                    Sku = "112",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ALIMENTATION SIMPLE ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN112",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 1100.0m,
                    PurchasePriceIncTax = 1210.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1700m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "AUTRE REPARATION ",
                    Sku = "623",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "AUTRE REPARATION ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN623",
                    ProductType = "General",
                    InitialQuantity = 98,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 0.0m,
                    PurchasePriceIncTax = 0.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "BATTERIE POUR LAPTOP ",
                    Sku = "176",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "BATTERIE POUR LAPTOP ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN176",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 9500.0m,
                    PurchasePriceIncTax = 10450.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "BOITIER GAMER ",
                    Sku = "8",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "BOITIER GAMER ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN8",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 15000.0m,
                    PurchasePriceIncTax = 16500.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 18000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE ALIMENTATION ORG",
                    Sku = "202",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE ALIMENTATION ORG",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN202",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 500.0m,
                    PurchasePriceIncTax = 550.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE HDMI 1.5M",
                    Sku = "247",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE HDMI 1.5M",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN247",
                    ProductType = "General",
                    InitialQuantity = 28,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 145.0m,
                    PurchasePriceIncTax = 159.50m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 300m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE SECTEUR ",
                    Sku = "249",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE SECTEUR ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN249",
                    ProductType = "General",
                    InitialQuantity = 28,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 145.0m,
                    PurchasePriceIncTax = 159.50m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 300m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE TRAFLE 1.5M",
                    Sku = "151",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE TRAFLE 1.5M",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN151",
                    ProductType = "General",
                    InitialQuantity = 116,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 156.81m,
                    PurchasePriceIncTax = 172.49m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE USB IMPRIMENT 1.5M",
                    Sku = "150",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE USB IMPRIMENT 1.5M",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN150",
                    ProductType = "General",
                    InitialQuantity = 45,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 146.0m,
                    PurchasePriceIncTax = 160.60m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CABLE VGA ",
                    Sku = "124",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CABLE VGA ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN124",
                    ProductType = "General",
                    InitialQuantity = 6,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 150.0m,
                    PurchasePriceIncTax = 165.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CASQUE MOCRO SPIRIT OF GAMER ELITE H20",
                    Sku = "231",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CASQUE MOCRO SPIRIT OF GAMER ELITE H20",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN231",
                    ProductType = "General",
                    InitialQuantity = 3,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 3300.0m,
                    PurchasePriceIncTax = 3630.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 4500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CHARGEUR POUR LAPTOP HP",
                    Sku = "373",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CHARGEUR POUR LAPTOP HP",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN373",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 64000.0m,
                    PurchasePriceIncTax = 70400.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CHERGEUR POUE MAC SAFE 1",
                    Sku = "125",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CHERGEUR POUE MAC SAFE 1",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN125",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 5000.0m,
                    PurchasePriceIncTax = 5500.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 7500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CLAVIER ET SOURIS SANS FIL ",
                    Sku = "123",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CLAVIER ET SOURIS SANS FIL ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN123",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 1200.0m,
                    PurchasePriceIncTax = 1320.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1800m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CLAVIER ET SOURIS SANS FILL GKM 520",
                    Sku = "246",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CLAVIER ET SOURIS SANS FILL GKM 520",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN246",
                    ProductType = "General",
                    InitialQuantity = 7,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 1750.0m,
                    PurchasePriceIncTax = 1925.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 2500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CLAVIER ET SOURIS SANS FILL HAVIT HV-KB601GCM",
                    Sku = "242",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CLAVIER ET SOURIS SANS FILL HAVIT HV-KB601GCM",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN242",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 2900.0m,
                    PurchasePriceIncTax = 3190.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 3500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CLAVIER USB FLIXIBLE PM",
                    Sku = "241",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CLAVIER USB FLIXIBLE PM",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN241",
                    ProductType = "General",
                    InitialQuantity = 10,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 990.0m,
                    PurchasePriceIncTax = 1089.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "CLAVIER USB SIMPLE ",
                    Sku = "105",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "CLAVIER USB SIMPLE ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN105",
                    ProductType = "General",
                    InitialQuantity = 15,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 570.0m,
                    PurchasePriceIncTax = 627.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DALL 15.6\" LED",
                    Sku = "362",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DALL 15.6\" LED",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN362",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 5000.0m,
                    PurchasePriceIncTax = 5500.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 12000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DIQUE DUR USB 1TB",
                    Sku = "156",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DIQUE DUR USB 1TB",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN156",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 9000.0m,
                    PurchasePriceIncTax = 9900.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 10000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR 512 GB SSD NEW",
                    Sku = "356",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR 512 GB SSD NEW",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN356",
                    ProductType = "General",
                    InitialQuantity = 19,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 6100.0m,
                    PurchasePriceIncTax = 6710.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 7500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR EXTERNE 1TB",
                    Sku = "1552",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR EXTERNE 1TB",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN1552",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 0.0m,
                    PurchasePriceIncTax = 0.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR HDD 1TB 2.5\"",
                    Sku = "898",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR HDD 1TB 2.5\"",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN898",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 2000.0m,
                    PurchasePriceIncTax = 2200.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 5500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR HDD 1TB 3.5\"",
                    Sku = "896",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR HDD 1TB 3.5\"",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN896",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 2000.0m,
                    PurchasePriceIncTax = 2200.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 5500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR HDD 500 GB 3.5\"",
                    Sku = "897",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR HDD 500 GB 3.5\"",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN897",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 2000.0m,
                    PurchasePriceIncTax = 2200.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 4000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR SSD 256GB NEW",
                    Sku = "9",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR SSD 256GB NEW",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN9",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 3850.0m,
                    PurchasePriceIncTax = 4235.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 5500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR SSD 512 GB",
                    Sku = "371",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR SSD 512 GB",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN371",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 0.0m,
                    PurchasePriceIncTax = 0.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR SSD NVME 1TB",
                    Sku = "120",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR SSD NVME 1TB",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN120",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 10000.0m,
                    PurchasePriceIncTax = 11000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 16500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR SSD NVME 256 GB ",
                    Sku = "10",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR SSD NVME 256 GB ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN10",
                    ProductType = "General",
                    InitialQuantity = 20,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 3700.0m,
                    PurchasePriceIncTax = 4070.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 6500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR SSD NVME 512 GB ",
                    Sku = "119",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR SSD NVME 512 GB ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN119",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 6000.0m,
                    PurchasePriceIncTax = 6600.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 8900m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "DISQUE DUR USB 4TB ",
                    Sku = "117",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "DISQUE DUR USB 4TB ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN117",
                    ProductType = "General",
                    InitialQuantity = 7,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 22000.0m,
                    PurchasePriceIncTax = 24200.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 25000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN ACER 27\" IPS FULL HD 1080P",
                    Sku = "236",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN ACER 27\" IPS FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN236",
                    ProductType = "General",
                    InitialQuantity = 2,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 36000.0m,
                    PurchasePriceIncTax = 39600.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 45000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN AOC 24\"",
                    Sku = "283",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN AOC 24\"",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN283",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 10000.0m,
                    PurchasePriceIncTax = 11000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 15000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN AOC 27\" FULL HD 1080P IPS",
                    Sku = "167",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN AOC 27\" FULL HD 1080P IPS",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN167",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 30000.0m,
                    PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 35000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN ASUS EXTERNE ",
                    Sku = "128",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN ASUS EXTERNE ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN128",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 8000.0m,
                    PurchasePriceIncTax = 8800.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 18000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN DELL 27\" IPS FULL HD 1080P",
                    Sku = "234",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN DELL 27\" IPS FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN234",
                    ProductType = "General",
                    InitialQuantity = 2,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 39500.0m,
                    PurchasePriceIncTax = 43450.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 46000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN HP 19.5\"",
                    Sku = "11",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN HP 19.5\"",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN11",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 19000.0m,
                    PurchasePriceIncTax = 20900.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 22000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN HP 23.8\" LED IPS FULL HD 1080P",
                    Sku = "12",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN HP 23.8\" LED IPS FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN12",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 28500.0m,
                    PurchasePriceIncTax = 31350.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 35000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN HP 27\" IPS FULL HD 1080P",
                    Sku = "235",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN HP 27\" IPS FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN235",
                    ProductType = "General",
                    InitialQuantity = 2,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 39500.0m,
                    PurchasePriceIncTax = 43450.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 46000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN LENOVO 21.5\" LED IPS FULL HD 1080P",
                    Sku = "13",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN LENOVO 21.5\" LED IPS FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN13",
                    ProductType = "General",
                    InitialQuantity = 4,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 21000.0m,
                    PurchasePriceIncTax = 23100.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 25000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ECRAN TV IRIS 50\" 4K",
                    Sku = "349",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ECRAN TV IRIS 50\" 4K",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN349",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 48000.0m,
                    PurchasePriceIncTax = 52800.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ENCRE POUR CANON 100ML ",
                    Sku = "148",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ENCRE POUR CANON 100ML ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN148",
                    ProductType = "General",
                    InitialQuantity = 13,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 869.47m,
                    PurchasePriceIncTax = 956.42m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1400m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ENCRE POUR EPSON 100ML ",
                    Sku = "149",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ENCRE POUR EPSON 100ML ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN149",
                    ProductType = "General",
                    InitialQuantity = 5,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 1150.0m,
                    PurchasePriceIncTax = 1265.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "ENCRE RECHARGABLE POUR EPSON ORG",
                    Sku = "118",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "ENCRE RECHARGABLE POUR EPSON ORG",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN118",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 3500.0m,
                    PurchasePriceIncTax = 3850.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 5500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "FLASHE DIQUE 16GB ",
                    Sku = "144",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "FLASHE DIQUE 16GB ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN144",
                    ProductType = "General",
                    InitialQuantity = 8,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 710.0m,
                    PurchasePriceIncTax = 781.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "FLASHE DIQUE 32GB ",
                    Sku = "143",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "FLASHE DIQUE 32GB ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN143",
                    ProductType = "General",
                    InitialQuantity = 7,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 708.33m,
                    PurchasePriceIncTax = 779.16m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1300m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "FLASHE DISQUE 08 GB",
                    Sku = "372",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "FLASHE DISQUE 08 GB",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN372",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 0.0m,
                    PurchasePriceIncTax = 0.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "HONTURE ROBOT VACUUM CLEANER ",
                    Sku = "212",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "HONTURE ROBOT VACUUM CLEANER ",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN212",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 30000.0m,
                    PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 49000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "HP ELITE BOOK 640 G5 14\" I5 8 EME GEN 08 GB DDR4/ 256 GB SSD ECRAN FULL HD 1080P",
                    Sku = "1122",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "HP ELITE BOOK 640 G5 14\" I5 8 EME GEN 08 GB DDR4/ 256 GB SSD ECRAN FULL HD 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN1122",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 40000.0m,
                    PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 55000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "HP PROBOOK 650 G4 CORE I5 8EME GEN/ 08 RAM /256 SSD ECRAN FULL HDF 1080P",
                    Sku = "1255",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "HP PROBOOK 650 G4 CORE I5 8EME GEN/ 08 RAM /256 SSD ECRAN FULL HDF 1080P",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN1255",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 38000.0m,
                    PurchasePriceIncTax = 41800.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 49000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "HUB USB H103",
                    Sku = "240",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "HUB USB H103",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN240",
                    ProductType = "General",
                    InitialQuantity = 9,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 770.0m,
                    PurchasePriceIncTax = 847.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 1400m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMAC 24\" CORE I5/08GB DDR3/29GB SSD +1TB HDD ECRAN RETINA",
                    Sku = "362_G",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMAC 24\" CORE I5/08GB DDR3/29GB SSD +1TB HDD ECRAN RETINA",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN362_G",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 74000.0m,
                    PurchasePriceIncTax = 81400.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 95000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON 2420",
                    Sku = "286",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON 2420",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN286",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 24000.0m,
                    PurchasePriceIncTax = 26400.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 0m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON 275 DW",
                    Sku = "1340",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON 275 DW",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN1340",
                    ProductType = "General",
                    InitialQuantity = 1,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 74000.0m,
                    PurchasePriceIncTax = 81400.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 78000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON LBP 6030",
                    Sku = "14",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON LBP 6030",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN14",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 35500.0m,
                    PurchasePriceIncTax = 39050.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 36500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON MF 3010",
                    Sku = "135",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON MF 3010",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN135",
                    ProductType = "General",
                    InitialQuantity = 4,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 53000.0m,
                    PurchasePriceIncTax = 58300.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 45500m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON MF 453 DW",
                    Sku = "15",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON MF 453 DW",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN15",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 105000.0m,
                    PurchasePriceIncTax = 115500.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 115000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON PIXMA G2410",
                    Sku = "16",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON PIXMA G2410",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN16",
                    ProductType = "General",
                    InitialQuantity = 0,
                    AlertQuantity = 5, // Default alert quantity
                    PurchasePriceExcTax = 22000.0m,
                    PurchasePriceIncTax = 24200.00m, // Assuming 10% tax
                    Xmargin = 10.00m, // Arbitrary margin
                    SellingPrice = 24000m,
                    SellingPriceTaxType = "Inc",
                    IsDivisible = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    ProductName = "IMPRIMENT CANON PIXMA G2430",
                    Sku = "17",
                    Barcode = "0",
                    BarcodeType = "UPC",
                    ProductDescription = "IMPRIMENT CANON PIXMA G2430",
                    UnitId = unit.Id,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    WarrantyId = warranty.Id,
                    UserId = user.Id,
                    SerialNumber = "SN17",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 22000.0m,
        PurchasePriceIncTax = 24200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 24000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "IMPRIMENT CANON PIXMA G3410 WIFI",
        Sku = "18",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "IMPRIMENT CANON PIXMA G3410 WIFI",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN18",
        ProductType = "General",
        InitialQuantity = 8,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 34187.5m,
        PurchasePriceIncTax = 37606.25m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 31500m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "IMPRIMENT CANON PIXMA G3430 WIFI",
        Sku = "19",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "IMPRIMENT CANON PIXMA G3430 WIFI",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN19",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 38000.0m,
        PurchasePriceIncTax = 41800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 32500m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "IMPRIMENT EPSON L3250 W",
        Sku = "177",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "IMPRIMENT EPSON L3250 W",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN177",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 33500.0m,
        PurchasePriceIncTax = 36850.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 45000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "KASPERSKY 01 POSTE",
        Sku = "145",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "KASPERSKY 01 POSTE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN145",
        ProductType = "General",
        InitialQuantity = 12,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 1680.29m,
        PurchasePriceIncTax = 1848.32m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 1900m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "KASPERSKY 03 POSTE",
        Sku = "146",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "KASPERSKY 03 POSTE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN146",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 3900.0m,
        PurchasePriceIncTax = 4290.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 4500m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "KASPERSKY 05 POSTE",
        Sku = "147",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "KASPERSKY 05 POSTE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN147",
        ProductType = "General",
        InitialQuantity = 5,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 5350.0m,
        PurchasePriceIncTax = 5885.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 6000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER 14\" CHROMBOOK CELERON /04GB DDR4/128GB SSD ECRAN FULL HD 1080P",
        Sku = "330",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER 14\" CHROMBOOK CELERON /04GB DDR4/128GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN330",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 12000.0m,
        PurchasePriceIncTax = 13200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 22000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER 14\" TRAVELMATE N20H4 CORE I5 11EME/16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "296",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER 14\" TRAVELMATE N20H4 CORE I5 11EME/16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN296",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 63000.0m,
        PurchasePriceIncTax = 69300.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER 15\" ASPIRE 7 N19C5 CORE I7 9EME/16 GB DDR4/1TB SSD ECRAN FULL HD 1080P GTX 1650 GRAPHIQUE",
        Sku = "1049",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER 15\" ASPIRE 7 N19C5 CORE I7 9EME/16 GB DDR4/1TB SSD ECRAN FULL HD 1080P GTX 1650 GRAPHIQUE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1049",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER 15\" NITRO 5 CORE I5 7EME /08GB DDR4/256 GB SSD ECRAN FULL HD 1080P  GTX 1050",
        Sku = "344",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER 15\" NITRO 5 CORE I5 7EME /08GB DDR4/256 GB SSD ECRAN FULL HD 1080P  GTX 1050",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN344",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 40000.0m,
        PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER 15\" TARVEL MATE P259 CORE I5 7EME /08GB DDR/512GB SSD ECRAN HD",
        Sku = "345",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER 15\" TARVEL MATE P259 CORE I5 7EME /08GB DDR/512GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN345",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 30000.0m,
        PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER ASPIRE 3 17\" CORE I3 7EME GEN/04GB DDR4/128GB SSD/1TB HDD ECRAN HD ",
        Sku = "1302",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER ASPIRE 3 17\" CORE I3 7EME GEN/04GB DDR4/128GB SSD/1TB HDD ECRAN HD ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1302",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 28000.0m,
        PurchasePriceIncTax = 30800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 39000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER ASPIRE 3 17\" INTEL CELERON/04GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "20",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER ASPIRE 3 17\" INTEL CELERON/04GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN20",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 31000.0m,
        PurchasePriceIncTax = 34100.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER EXTENSA 15\" CORE I5 10EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "142_D",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER EXTENSA 15\" CORE I5 10EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN142_D",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER NITRON 15 RYZEN 5 2500U/08GB DDR4/512GB SSD /AMD GRAPHIQUE RX 560X /AMD RADEON VIGA 8 FULL HD 1080P",
        Sku = "282_A",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER NITRON 15 RYZEN 5 2500U/08GB DDR4/512GB SSD /AMD GRAPHIQUE RX 560X /AMD RADEON VIGA 8 FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN282_A",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 50000.0m,
        PurchasePriceIncTax = 55000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 75000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER SWIFT 14\" N19H4 CORE I7 10EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "209",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER SWIFT 14\" N19H4 CORE I7 10EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN209",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 78000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER SWIFT 3 14\" RYZEN 5 3000 SERIES /08GB DDR/512GB SSD ECRAN FULL HD 1080P",
        Sku = "193",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER SWIFT 3 14\" RYZEN 5 3000 SERIES /08GB DDR/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN193",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 48000.0m,
        PurchasePriceIncTax = 52800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER TRAVEL MATE 15\" CORE I3 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "146_S",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER TRAVEL MATE 15\" CORE I3 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN146_S",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 42000.0m,
        PurchasePriceIncTax = 46200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 52000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ACER15\" ASPIR VERO CORE I3 11EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "1476",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ACER15\" ASPIR VERO CORE I3 11EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1476",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 65000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "Produit Divers",
        Sku = "653121",
        Barcode = "0",
        IsDefault = true,
        BarcodeType = "UPC",
        ProductDescription = "Un Produit Divers Pour une saise Rapide",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1476",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 65000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 12\" VIVOBOOK E203N 04GB DDR4/128GB SSD ECRAN HD",
        Sku = "355",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 12\" VIVOBOOK E203N 04GB DDR4/128GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN355",
        ProductType = "General",
        InitialQuantity = 4,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 29500.0m,
        PurchasePriceIncTax = 32450.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 39500m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 13\" B9450F EXPERTBOOK CORE I5 10EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "1451",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 13\" B9450F EXPERTBOOK CORE I5 10EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1451",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 67000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 13\" ZENBOOK UX325E CORE I5 11EME GEN/16GB DDR4/512 GB DDR ECRAN FULL HD 1080P ",
        Sku = "332",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 13\" ZENBOOK UX325E CORE I5 11EME GEN/16GB DDR4/512 GB DDR ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN332",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 85000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 14\" E410M INTEL CELERON /04GB DDR4/64GB SSD+256GB SSD ",
        Sku = "163",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 14\" E410M INTEL CELERON /04GB DDR4/64GB SSD+256GB SSD ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN163",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 23000.0m,
        PurchasePriceIncTax = 25300.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 39000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 14\" EXPERTBOOK B5402CE CORE I7 11EME/24GB DDR4/512GB SSD ECRAN FULL HD 1080P NRBER PAD ",
        Sku = "264",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 14\" EXPERTBOOK B5402CE CORE I7 11EME/24GB DDR4/512GB SSD ECRAN FULL HD 1080P NRBER PAD ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN264",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 15\" F15 TUF 516PR CORE I7 11EME GEN H/16GB DDR4/512GB SSD NVIDIA RTX 3070 FULL HD 1080P ",
        Sku = "141",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 15\" F15 TUF 516PR CORE I7 11EME GEN H/16GB DDR4/512GB SSD NVIDIA RTX 3070 FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN141",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 175000.0m,
        PurchasePriceIncTax = 192500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 198000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS 15\" VIVOBOOK X505 RYZEN 5 PRO 2000 SERIES /08GB DDR4/256GB SSD/1TB HDD ECRAN HD",
        Sku = "388",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS 15\" VIVOBOOK X505 RYZEN 5 PRO 2000 SERIES /08GB DDR4/256GB SSD/1TB HDD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN388",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 35000.0m,
        PurchasePriceIncTax = 38500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 45000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS CHROMBOOK 14\" CORE I3 11EME GEN/08GB DDR4/128GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        Sku = "186",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS CHROMBOOK 14\" CORE I3 11EME GEN/08GB DDR4/128GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN186",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 25000.0m,
        PurchasePriceIncTax = 27500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 45000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS EXPERTBOOK 15\"B B1500C CORE I5 11EMEGEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "194_J",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS EXPERTBOOK 15\"B B1500C CORE I5 11EMEGEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN194_J",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 67000.0m,
        PurchasePriceIncTax = 73700.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVO BOOK K413J 14\" CORE I3 10EME GEN /08GB DDR4/256GB SSD ECRAN FULL HD 1080P AVEC NEMBER PAD",
        Sku = "21",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVO BOOK K413J 14\" CORE I3 10EME GEN /08GB DDR4/256GB SSD ECRAN FULL HD 1080P AVEC NEMBER PAD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN21",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK 14\" F1400E CORE I7 11EME GEN/16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        Sku = "284",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK 14\" F1400E CORE I7 11EME GEN/16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN284",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 52000.0m,
        PurchasePriceIncTax = 57200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 78000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK 15\" F515E CORE I5 11EME GEN/16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "1297",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK 15\" F515E CORE I5 11EME GEN/16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1297",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 64000.0m,
        PurchasePriceIncTax = 70400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 77000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK 15\" M712D RYZEN 5 3500U /12GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "1294",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK 15\" M712D RYZEN 5 3500U /12GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1294",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 43000.0m,
        PurchasePriceIncTax = 47300.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK 15\" X1605P CORE I5 11EME H 08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "1030",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK 15\" X1605P CORE I5 11EME H 08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1030",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 60000.0m,
        PurchasePriceIncTax = 66000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 82000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK D413D 14\" RYZEN 5 3000 SERIE /08GB DDR4/512GB SSD ECAN FULL HD 1080P ",
        Sku = "153",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK D413D 14\" RYZEN 5 3000 SERIE /08GB DDR4/512GB SSD ECAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN153",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 39000.0m,
        PurchasePriceIncTax = 42900.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 52000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK E510 15\" INTEL PENTUME /04GB DDR4/128GB SSD ECRAN FULL HD 1080P",
        Sku = "169",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK E510 15\" INTEL PENTUME /04GB DDR4/128GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN169",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 25000.0m,
        PurchasePriceIncTax = 27500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 36000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK M1502L 15\" RYZEN 5 4000 SERIE /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "180",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK M1502L 15\" RYZEN 5 4000 SERIE /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN180",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 68000.0m,
        PurchasePriceIncTax = 74800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 82000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK P1701F 17\" CORE I3 10EME GEN/01GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "23",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK P1701F 17\" CORE I3 10EME GEN/01GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN23",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 40000.0m,
        PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 57000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK R542U 15\" CORE I7 08EME GEN 16GB DDR4/128GB SSD/1TB HDD ECRAN FULL HD 1080P",
        Sku = "22",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK R542U 15\" CORE I7 08EME GEN 16GB DDR4/128GB SSD/1TB HDD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN22",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK X415J 14\" CORE I5 10 EME GEN /08GB DDR4/ 256 GB SSD ECRAN FULL HD 1080P",
        Sku = "192",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK X415J 14\" CORE I5 10 EME GEN /08GB DDR4/ 256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN192",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 57000.0m,
        PurchasePriceIncTax = 62700.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK X509F 15\" CORE I5 8 EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "150_E",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK X509F 15\" CORE I5 8 EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN150_E",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK X515J 15\" CORE I5 10EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "157",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK X515J 15\" CORE I5 10EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN157",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 68000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK X515J 15\" CORE I7 10EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "151_H",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK X515J 15\" CORE I7 10EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN151_H",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 85000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS VIVOBOOK X712F 17\" CORE I3 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "24",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS VIVOBOOK X712F 17\" CORE I3 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN24",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ASUS ZENBOOK 14 UX425J CORE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P WHITH NEMBER PAD",
        Sku = "204",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ASUS ZENBOOK 14 UX425J CORE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P WHITH NEMBER PAD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN204",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 72000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 14\" INSPERON CORE I5 8EME GEN/12GB DDR4/256GB SSD ECRAN HD",
        Sku = "292",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 14\" INSPERON CORE I5 8EME GEN/12GB DDR4/256GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN292",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 38000.0m,
        PurchasePriceIncTax = 41800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" LATITUDE 3590 CORE I5 7EME GEN/16GB DDR4/256 GB SSD ECRAN HD",
        Sku = "293",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" LATITUDE 3590 CORE I5 7EME GEN/16GB DDR4/256 GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN293",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 30000.0m,
        PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" LATITUDE 5540 CORE I5 13EME/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "295",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" LATITUDE 5540 CORE I5 13EME/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN295",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 95000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" PRECISION 3571 CORE I9 12EME GEN/32GB DDR5/1TB SSD ECRAN FULL HD 1080P RTX A1000 04GB",
        Sku = "1543",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" PRECISION 3571 CORE I9 12EME GEN/32GB DDR5/1TB SSD ECRAN FULL HD 1080P RTX A1000 04GB",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1543",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 155000.0m,
        PurchasePriceIncTax = 170500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 198000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" PRECISION 7560 CORE I5 12EME GEN/32GB DDR5/512 GBSSD ECRAN FULL HD 1080P RTX GRAPHIQUE",
        Sku = "387",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" PRECISION 7560 CORE I5 12EME GEN/32GB DDR5/512 GBSSD ECRAN FULL HD 1080P RTX GRAPHIQUE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN387",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 115000.0m,
        PurchasePriceIncTax = 126500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 150000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" VOSTRO 15 3000 CORE I5 8EME GEB /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "276",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" VOSTRO 15 3000 CORE I5 8EME GEB /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN276",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 39000.0m,
        PurchasePriceIncTax = 42900.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" VOSTRO CORE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "385",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" VOSTRO CORE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN385",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL 15\" VOSTRO CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "290",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL 15\" VOSTRO CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN290",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 75000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL INSPIRON 13\" 7306 CORE I5 11EME /08GB DDR4/32GB SSD +512GB SSD ECRAN FULL HD TACTILE X360 ",
        Sku = "196",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL INSPIRON 13\" 7306 CORE I5 11EME /08GB DDR4/32GB SSD +512GB SSD ECRAN FULL HD TACTILE X360 ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN196",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 67000.0m,
        PurchasePriceIncTax = 73700.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 13\" 7210 CORE I7 10EME GEN/16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P TACTILE DITACHABLE",
        Sku = "331",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 13\" 7210 CORE I7 10EME GEN/16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P TACTILE DITACHABLE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN331",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 69000.0m,
        PurchasePriceIncTax = 75900.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 82000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 13\" 7290 CORE I5 8EME/08GB DDR4/256GB SSD /ECRAN FULL HD 1080P",
        Sku = "218",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 13\" 7290 CORE I5 8EME/08GB DDR4/256GB SSD /ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN218",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 39000.0m,
        PurchasePriceIncTax = 42900.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 52000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\"  3420 CORE I3 11EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "222",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\"  3420 CORE I3 11EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN222",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 44000.0m,
        PurchasePriceIncTax = 48400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 3320 CORE I5 11EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080 ",
        Sku = "166_W",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 3320 CORE I5 11EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080 ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN166_W",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 67500.0m,
        PurchasePriceIncTax = 74250.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 78000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 3440 CORE I3 13EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "224",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 3440 CORE I3 13EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN224",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 44000.0m,
        PurchasePriceIncTax = 48400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 68000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5400 CORE I5 8EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "147_B",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5400 CORE I5 8EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN147_B",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5410 CORE I5 10 EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "375",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5410 CORE I5 10 EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN375",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 50000.0m,
        PurchasePriceIncTax = 55000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5420 CORE I5 11EME/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "347",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5420 CORE I5 11EME/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN347",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 60000.0m,
        PurchasePriceIncTax = 66000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 77000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5420AF CORE I5 11EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P AFICHEUR",
        Sku = "348",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5420AF CORE I5 11EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P AFICHEUR",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN348",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5430 CORE I5 12EME GEN /16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        Sku = "165",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5430 CORE I5 12EME GEN /16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN165",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 70000.0m,
        PurchasePriceIncTax = 77000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 85000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5430 CORE I7 12EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "394",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5430 CORE I7 12EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN394",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 105000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 5480 CORE I7 7EME/08GB DDR4/256GB SDD ECRAN TACTILE FULL HD 1080P",
        Sku = "148_S",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 5480 CORE I7 7EME/08GB DDR4/256GB SDD ECRAN TACTILE FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN148_S",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 35000.0m,
        PurchasePriceIncTax = 38500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 7330 CORE I5 12EME EVO/16GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "210",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 7330 CORE I5 12EME EVO/16GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN210",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 14\" 7420 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "170",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 14\" 7420 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN170",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 68000.0m,
        PurchasePriceIncTax = 74800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 78000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 3510 CORE I5 10 EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "281",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 3510 CORE I5 10 EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN281",
        ProductType = "General",
        InitialQuantity = 2,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 3520 CORE I3 11EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "223",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 3520 CORE I3 11EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN223",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 44000.0m,
        PurchasePriceIncTax = 48400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 3540 CORE I5 13EME GEN/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "1301",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 3540 CORE I5 13EME GEN/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1301",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 95000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5500 CORE I5 8EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "225",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5500 CORE I5 8EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN225",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 42000.0m,
        PurchasePriceIncTax = 46200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5510 COREE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "327",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5510 COREE I5 10EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN327",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 70000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5510 COREE I7 10EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "384",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5510 COREE I7 10EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN384",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 77000.0m,
        PurchasePriceIncTax = 84700.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 87000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5520 CORE I7 11EME GEN/16 GB DDR4/512 GB SSD ECRAN FULL HD 1080P NVIDIA GRAPHIQUE",
        Sku = "323",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5520 CORE I7 11EME GEN/16 GB DDR4/512 GB SSD ECRAN FULL HD 1080P NVIDIA GRAPHIQUE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN323",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 115000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5530 CORE I5 12EME GEN/08GB DDR4/256GB SSD ",
        Sku = "144_X",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5530 CORE I5 12EME GEN/08GB DDR4/256GB SSD ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN144_X",
        ProductType = "General",
        InitialQuantity = 2,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 92000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 15\" 5590 CORE I5 7EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "143_F",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 15\" 5590 CORE I5 7EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN143_F",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 40000.0m,
        PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 3520 15\" CORE I5 11EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "173",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 3520 15\" CORE I5 11EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN173",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 64000.0m,
        PurchasePriceIncTax = 70400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 77000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5320 14\" CORE I5 11EME GEN /08GB DDR4/256GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        Sku = "26",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5320 14\" CORE I5 11EME GEN /08GB DDR4/256GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN26",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 72000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5320 14\" CORE I7 11EME GEN /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "1579",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5320 14\" CORE I7 11EME GEN /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1579",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 82000.0m,
        PurchasePriceIncTax = 90200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 980000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5430 14\" CORE I5 12EME GEN /32GB DD4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "131",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5430 14\" CORE I5 12EME GEN /32GB DD4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN131",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 95000.0m,
        PurchasePriceIncTax = 104500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 120000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5430 14\" CORE I5 12EME/16GB DDR4/1TB GB SSD ECRAN FULL HD 1080P",
        Sku = "220",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5430 14\" CORE I5 12EME/16GB DDR4/1TB GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN220",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 85000.0m,
        PurchasePriceIncTax = 93500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5440 14 CORE I5 13EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "188",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5440 14 CORE I5 13EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN188",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 85000.0m,
        PurchasePriceIncTax = 93500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 97000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5520 15\" CORE I5 11EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "27",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5520 15\" CORE I5 11EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN27",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 80000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5520 15\" CORE I7 11EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "28",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5520 15\" CORE I7 11EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN28",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 100000.0m,
        PurchasePriceIncTax = 110000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 119000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5580 15\" CORE I5 7EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "152_V",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5580 15\" CORE I5 7EME GEN/08GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN152_V",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 39000.0m,
        PurchasePriceIncTax = 42900.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5580 15\" CORE I7 7EME/16GB DDR4/512GB SSD ECRAN FULL HD 1080P ",
        Sku = "259",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5580 15\" CORE I7 7EME/16GB DDR4/512GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN259",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 42000.0m,
        PurchasePriceIncTax = 46200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 5590 15\" CORE I5 8EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "29",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 5590 15\" CORE I5 8EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN29",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 7390 14\" CORE I5 7EME GEN 08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "25",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 7390 14\" CORE I5 7EME GEN 08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN25",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 42000.0m,
        PurchasePriceIncTax = 46200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 7400 14\" CORE I7 8EME GEN 08GB DDR4/512GB SSD ECRAN TACTILE ",
        Sku = "30",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 7400 14\" CORE I7 8EME GEN 08GB DDR4/512GB SSD ECRAN TACTILE ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN30",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 62000.0m,
        PurchasePriceIncTax = 68200.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 73000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE 7410 14\" CORE I5 10EME /08GB DDR4/256GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        Sku = "187",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE 7410 14\" CORE I5 10EME /08GB DDR4/256GB SSD ECRAN TACTILE X360 FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN187",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE E5470 14\" CORE I5 6EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "155",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE E5470 14\" CORE I5 6EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN155",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 30000.0m,
        PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 40000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE E5570 15\"CORE I7 6EME /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "98",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE E5570 15\"CORE I7 6EME /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN98",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE E7470 14\" CORE I7 6EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "158",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE E7470 14\" CORE I7 6EME GEN/16GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN158",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 35000.0m,
        PurchasePriceIncTax = 38500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 55000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE GRIS 15\" CORE I7 6EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P AMD GRAPHIQUE",
        Sku = "41",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE GRIS 15\" CORE I7 6EME GEN /16GB DDR4/256GB SSD ECRAN FULL HD 1080P AMD GRAPHIQUE",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN41",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 58000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL LATITUDE PRECISION 7730 17\" CORE I7 8EME GEN/32GB DDR 4/500 GB SSD QUADRO GRAPHIQUE P3200 ECRAN FULL HD 1080P",
        Sku = "42",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL LATITUDE PRECISION 7730 17\" CORE I7 8EME GEN/32GB DDR 4/500 GB SSD QUADRO GRAPHIQUE P3200 ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN42",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 85000.0m,
        PurchasePriceIncTax = 93500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 139000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL VOSRO 3500 15\" CORE I5 11EME GEN /08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "43",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL VOSRO 3500 15\" CORE I5 11EME GEN /08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN43",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 70000.0m,
        PurchasePriceIncTax = 77000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 80000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL VOSTO 5502 15\" CORE I7 11EME /16GB DDR4/512GB SSD NVIDIA GRAPHIQUE ECRAN FULL HD 1080P ",
        Sku = "31",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL VOSTO 5502 15\" CORE I7 11EME /16GB DDR4/512GB SSD NVIDIA GRAPHIQUE ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN31",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 105000.0m,
        PurchasePriceIncTax = 115500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 128000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL XPS 13\" 9310 CORE I7 11EME EVO/16GB DDR4/512GB SSD ECRAN FULL HD + TACTILE X360 ",
        Sku = "195",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL XPS 13\" 9310 CORE I7 11EME EVO/16GB DDR4/512GB SSD ECRAN FULL HD + TACTILE X360 ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN195",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 90000.0m,
        PurchasePriceIncTax = 99000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 135000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP DELL XPS 13\" P82G CORE I7 10EME/16GB DDR4/512GB SSD ECRAN TACTILE FULL HD 1080",
        Sku = "214_B",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP DELL XPS 13\" P82G CORE I7 10EME/16GB DDR4/512GB SSD ECRAN TACTILE FULL HD 1080",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN214_B",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 70000.0m,
        PurchasePriceIncTax = 77000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 92000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP ELITEBOOK 15\" 850 G6 CORE I7 8EME GEN/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "291",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP ELITEBOOK 15\" 850 G6 CORE I7 8EME GEN/16GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN291",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP FUJITSU 15\" CORE I5 7EME GEN /08GB DDR4/256 GB SSD ECRAN FUJLL HD 1080P",
        Sku = "1292",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP FUJITSU 15\" CORE I5 7EME GEN /08GB DDR4/256 GB SSD ECRAN FUJLL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1292",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 38000.0m,
        PurchasePriceIncTax = 41800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 49000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP FUJITSU 15\" CORE I5 7EME GEN /16GB DDR4/256 GB SSD ECRAN FUJLL HD 1080P",
        Sku = "275",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP FUJITSU 15\" CORE I5 7EME GEN /16GB DDR4/256 GB SSD ECRAN FUJLL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN275",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 30000.0m,
        PurchasePriceIncTax = 33000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 45000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP FUJITSU 15\" LIFBOOK 5E15A2 CORE I5 11EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "1531",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP FUJITSU 15\" LIFBOOK 5E15A2 CORE I5 11EME /08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1531",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 75000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP FUJITSU E5510 15\" CORE I3 10EM GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "33",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP FUJITSU E5510 15\" CORE I3 10EM GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN33",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP GYGABYTE 15\" G5 KF5 CORE I7 13EME GEN /16GB DDR4/512 GB SSD ECRAN FULL HD 1080O RTX 4060",
        Sku = "322",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP GYGABYTE 15\" G5 KF5 CORE I7 13EME GEN /16GB DDR4/512 GB SSD ECRAN FULL HD 1080O RTX 4060",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN322",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 140000.0m,
        PurchasePriceIncTax = 154000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 205000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HONOR 14\" NDR-WFE9 CORE I7 11EME/08GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        Sku = "389",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HONOR 14\" NDR-WFE9 CORE I7 11EME/08GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN389",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 96000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HONOR 15\" RYZEN 5 4000 SERIE /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "179",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HONOR 15\" RYZEN 5 4000 SERIE /16GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN179",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 68000.0m,
        PurchasePriceIncTax = 74800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 88000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 13\" ELITEBOOK 830 G8 CORE I7 11EME/32GB DDR4/512GB SSD ECRAN TACTILE FULL HD 1080P",
        Sku = "1544",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 13\" ELITEBOOK 830 G8 CORE I7 11EME/32GB DDR4/512GB SSD ECRAN TACTILE FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1544",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 90000.0m,
        PurchasePriceIncTax = 99000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 125000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" 14S-DQ5037NF CORE I3 12EME GEN /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "289",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" 14S-DQ5037NF CORE I3 12EME GEN /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN289",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 45000.0m,
        PurchasePriceIncTax = 49500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" 240 G8 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "382",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" 240 G8 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN382",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 68000.0m,
        PurchasePriceIncTax = 74800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" 445 G9 RYZEN 5 PRO 5000 SERIES /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "1377",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" 445 G9 RYZEN 5 PRO 5000 SERIES /08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1377",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 20000.0m,
        PurchasePriceIncTax = 22000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" 840 G7 CORE I5 10EME /16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "200",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" 840 G7 CORE I5 10EME /16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN200",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 55000.0m,
        PurchasePriceIncTax = 60500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 69000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" 845 G9 RYZEN 5 6000 SERIES /16GB DDR4/512 GBSSD ECRAN FULL HD 1080P SN/5CG3215R3PNFG",
        Sku = "1434",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" 845 G9 RYZEN 5 6000 SERIES /16GB DDR4/512 GBSSD ECRAN FULL HD 1080P SN/5CG3215R3PNFG",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1434",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 58000.0m,
        PurchasePriceIncTax = 63800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 89000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 14\" ELITEBOOK 840 G9 CORE I5 12EME GEN/16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        Sku = "279",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 14\" ELITEBOOK 840 G9 CORE I5 12EME GEN/16GB DDR4/512 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN279",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 15-DW1023NF CORE I5 10EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P ",
        Sku = "343",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 15-DW1023NF CORE I5 10EME/08GB DDR4/512GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN343",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 40000.0m,
        PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 65000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 15-DY2795WM CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "237",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 15-DY2795WM CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN237",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 100000.0m,
        PurchasePriceIncTax = 110000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 115000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 15S CORE I7 12EME/16GB DDR4/1TB SSD ECRAN FULL HD 1080 P ",
        Sku = "1052",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 15S CORE I7 12EME/16GB DDR4/1TB SSD ECRAN FULL HD 1080 P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1052",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 115000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 15S-FQ0091NF CELERON/08GB DDR4/2546 GB SSD ECRAN HD",
        Sku = "312",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 15S-FQ0091NF CELERON/08GB DDR4/2546 GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN312",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 25000.0m,
        PurchasePriceIncTax = 27500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 38000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 15S-FQ4014NP CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        Sku = "391",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 15S-FQ4014NP CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN391",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 250 G3 CORE I3 10EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "228",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 250 G3 CORE I3 10EME/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN228",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 44000.0m,
        PurchasePriceIncTax = 48400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 58000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 250 G4 CORE I3 5EME GEN/08GB DDR3/240 GB SSD ECRAN HD",
        Sku = "298",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 250 G4 CORE I3 5EME GEN/08GB DDR3/240 GB SSD ECRAN HD",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN298",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 18000.0m,
        PurchasePriceIncTax = 19800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 38000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 450 G8 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "329",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 450 G8 CORE I5 11EME GEN/08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN329",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 77000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" 650 G9 CORE I5 12 EME GEN/16 GB DDR4/256GB SSD ECRAN TACTILE FULL HD 1080P",
        Sku = "199",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" 650 G9 CORE I5 12 EME GEN/16 GB DDR4/256GB SSD ECRAN TACTILE FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN199",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 80000.0m,
        PurchasePriceIncTax = 88000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 110000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" PROBOOK RYZEN 5 4000 SERIES /16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        Sku = "280",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" PROBOOK RYZEN 5 4000 SERIES /16 GB DDR4/256 GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN280",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 25000.0m,
        PurchasePriceIncTax = 27500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 50000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" SPECTRE X360 CORE I7 11EME/16GB DDR4/512GB SSD ECRAN TACTILE 4K",
        Sku = "1477",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" SPECTRE X360 CORE I7 11EME/16GB DDR4/512GB SSD ECRAN TACTILE 4K",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1477",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 85000.0m,
        PurchasePriceIncTax = 93500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 98000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" VICTUS 16 RYZEN 5 5000 SERIES /16GB DDR4 /512 GB SSD ECRAN FULL HD 1080P 144 HZ RTX 3060 6G ",
        Sku = "266",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" VICTUS 16 RYZEN 5 5000 SERIES /16GB DDR4 /512 GB SSD ECRAN FULL HD 1080P 144 HZ RTX 3060 6G ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN266",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 125000.0m,
        PurchasePriceIncTax = 137500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 165000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" ZBOOK G5 X360 CORE I9 HK 8 EME GEN/32GB DDR4/1TB SSD QUADRO GRAPHIQUE A1000 04GB ",
        Sku = "1413",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" ZBOOK G5 X360 CORE I9 HK 8 EME GEN/32GB DDR4/1TB SSD QUADRO GRAPHIQUE A1000 04GB ",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1413",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 90000.0m,
        PurchasePriceIncTax = 99000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 138000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15\" ZBOOK POWER G8 WORKSTATION CORE I9 11EME GEN/32GB DDR4/1TB SSD ECRAN FULL HD 1080P RTX A2000",
        Sku = "324",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15\" ZBOOK POWER G8 WORKSTATION CORE I9 11EME GEN/32GB DDR4/1TB SSD ECRAN FULL HD 1080P RTX A2000",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN324",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 160000.0m,
        PurchasePriceIncTax = 176000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 195000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 15S-FQ5022NS CORE I5 12EME GEN /16GB DD4/512GB SSD ECRAN 15\" FULL HD 1080P",
        Sku = "288",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 15S-FQ5022NS CORE I5 12EME GEN /16GB DD4/512GB SSD ECRAN 15\" FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN288",
        ProductType = "General",
        InitialQuantity = 1,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 65000.0m,
        PurchasePriceIncTax = 71500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 86000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 17 INTEL CELERON/04GB DDR4/1TB HDD ECRAN 17\"",
        Sku = "35",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 17 INTEL CELERON/04GB DDR4/1TB HDD ECRAN 17\"",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN35",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 25000.0m,
        PurchasePriceIncTax = 27500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 38000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 17\" 17-BYXXX CORE I5 11EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        Sku = "1296",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 17\" 17-BYXXX CORE I5 11EME GEN/08GB DDR4/512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1296",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 64000.0m,
        PurchasePriceIncTax = 70400.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 77000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 17\" 17-CP0001SF RYZEN 7 5000 SERIES /16GB DDR4/512 TB SSD ECRAN FULL HD 1080P",
        Sku = "198",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 17\" 17-CP0001SF RYZEN 7 5000 SERIES /16GB DDR4/512 TB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN198",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 70000.0m,
        PurchasePriceIncTax = 77000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 105000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 17\" 17-CP0276NF RYZEN 5 5000 SERIES /08GB DDR4/ 512GB SSD ECRAN FULL HD 1080P",
        Sku = "328",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 17\" 17-CP0276NF RYZEN 5 5000 SERIES /08GB DDR4/ 512GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN328",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 68000.0m,
        PurchasePriceIncTax = 74800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 79000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 17\" PROBOOK 470 G5 CORE I5 8EME GEN/08GB DDR/256GB SSD ECRAN FULL HD 1080P",
        Sku = "1103",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 17\" PROBOOK 470 G5 CORE I5 8EME GEN/08GB DDR/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN1103",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 40000.0m,
        PurchasePriceIncTax = 44000.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 45000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 250 G8 15\" CORE I3 11EME GEN 08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        Sku = "272",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 250 G8 15\" CORE I3 11EME GEN 08GB DDR4/256GB SSD ECRAN FULL HD 1080P",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN272",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 48000.0m,
        PurchasePriceIncTax = 52800.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 59000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    },
    new Product
    {
        ProductName = "LAPTOP HP 250 G8 15\" CORE I3 11EME GEN/04GB DDR4/1TB HDD ECRAN FULL HD 1080P NEW",
        Sku = "44",
        Barcode = "0",
        BarcodeType = "UPC",
        ProductDescription = "LAPTOP HP 250 G8 15\" CORE I3 11EME GEN/04GB DDR4/1TB HDD ECRAN FULL HD 1080P NEW",
        UnitId = unit.Id,
        BrandId = brand.Id,
        CategoryId = category.Id,
        WarrantyId = warranty.Id,
        UserId = user.Id,
        SerialNumber = "SN44",
        ProductType = "General",
        InitialQuantity = 0,
        AlertQuantity = 5, // Default alert quantity
        PurchasePriceExcTax = 75000.0m,
        PurchasePriceIncTax = 82500.00m, // Assuming 10% tax
        Xmargin = 10.00m, // Arbitrary margin
        SellingPrice = 82000m,
        SellingPriceTaxType = "Inc",
        IsDivisible = false,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    }
};

            context.Products.AddRange(products);
            context.SaveChanges();

            var wholesaleGroup = context.PriceGroups.FirstOrDefault(pg => pg.Name == "Wholesale");
            var retailGroup = context.PriceGroups.FirstOrDefault(pg => pg.Name == "Retail");

            var productPrices = new List<ProductPrice>
            {

                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 450.0m,
                    ProductId = context.Products.First(p => p.Sku == "248").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 900m,
                    ProductId = context.Products.First(p => p.Sku == "248").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 4500.0m,
                    ProductId = context.Products.First(p => p.Sku == "113").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 5500m,
                    ProductId = context.Products.First(p => p.Sku == "113").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 10000.0m,
                    ProductId = context.Products.First(p => p.Sku == "109").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 12500m,
                    ProductId = context.Products.First(p => p.Sku == "109").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 7500.0m,
                    ProductId = context.Products.First(p => p.Sku == "110").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 8500m,
                    ProductId = context.Products.First(p => p.Sku == "110").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1100.0m,
                    ProductId = context.Products.First(p => p.Sku == "111").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 14300m,
                    ProductId = context.Products.First(p => p.Sku == "111").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1100.0m,
                    ProductId = context.Products.First(p => p.Sku == "112").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1700m,
                    ProductId = context.Products.First(p => p.Sku == "112").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 0.0m,
                    ProductId = context.Products.First(p => p.Sku == "623").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "623").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 9500.0m,
                    ProductId = context.Products.First(p => p.Sku == "176").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "176").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 15000.0m,
                    ProductId = context.Products.First(p => p.Sku == "8").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 18000m,
                    ProductId = context.Products.First(p => p.Sku == "8").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 500.0m,
                    ProductId = context.Products.First(p => p.Sku == "202").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1500m,
                    ProductId = context.Products.First(p => p.Sku == "202").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 145.0m,
                    ProductId = context.Products.First(p => p.Sku == "247").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 300m,
                    ProductId = context.Products.First(p => p.Sku == "247").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 145.0m,
                    ProductId = context.Products.First(p => p.Sku == "249").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 300m,
                    ProductId = context.Products.First(p => p.Sku == "249").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 156.81m,
                    ProductId = context.Products.First(p => p.Sku == "151").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 500m,
                    ProductId = context.Products.First(p => p.Sku == "151").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 146.0m,
                    ProductId = context.Products.First(p => p.Sku == "150").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 500m,
                    ProductId = context.Products.First(p => p.Sku == "150").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 150.0m,
                    ProductId = context.Products.First(p => p.Sku == "124").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 500m,
                    ProductId = context.Products.First(p => p.Sku == "124").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 3300.0m,
                    ProductId = context.Products.First(p => p.Sku == "231").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 4500m,
                    ProductId = context.Products.First(p => p.Sku == "231").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 64000.0m,
                    ProductId = context.Products.First(p => p.Sku == "373").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "373").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 5000.0m,
                    ProductId = context.Products.First(p => p.Sku == "125").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 7500m,
                    ProductId = context.Products.First(p => p.Sku == "125").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1200.0m,
                    ProductId = context.Products.First(p => p.Sku == "123").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1800m,
                    ProductId = context.Products.First(p => p.Sku == "123").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1750.0m,
                    ProductId = context.Products.First(p => p.Sku == "246").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 2500m,
                    ProductId = context.Products.First(p => p.Sku == "246").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 2900.0m,
                    ProductId = context.Products.First(p => p.Sku == "242").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 3500m,
                    ProductId = context.Products.First(p => p.Sku == "242").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 990.0m,
                    ProductId = context.Products.First(p => p.Sku == "241").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1500m,
                    ProductId = context.Products.First(p => p.Sku == "241").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 570.0m,
                    ProductId = context.Products.First(p => p.Sku == "105").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1000m,
                    ProductId = context.Products.First(p => p.Sku == "105").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 5000.0m,
                    ProductId = context.Products.First(p => p.Sku == "362").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 12000m,
                    ProductId = context.Products.First(p => p.Sku == "362").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 9000.0m,
                    ProductId = context.Products.First(p => p.Sku == "156").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 10000m,
                    ProductId = context.Products.First(p => p.Sku == "156").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 6100.0m,
                    ProductId = context.Products.First(p => p.Sku == "356").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 7500m,
                    ProductId = context.Products.First(p => p.Sku == "356").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 0.0m,
                    ProductId = context.Products.First(p => p.Sku == "1552").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "1552").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 2000.0m,
                    ProductId = context.Products.First(p => p.Sku == "898").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 5500m,
                    ProductId = context.Products.First(p => p.Sku == "898").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 2000.0m,
                    ProductId = context.Products.First(p => p.Sku == "896").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 5500m,
                    ProductId = context.Products.First(p => p.Sku == "896").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 2000.0m,
                    ProductId = context.Products.First(p => p.Sku == "897").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 4000m,
                    ProductId = context.Products.First(p => p.Sku == "897").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 3850.0m,
                    ProductId = context.Products.First(p => p.Sku == "9").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 5500m,
                    ProductId = context.Products.First(p => p.Sku == "9").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 0.0m,
                    ProductId = context.Products.First(p => p.Sku == "371").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "371").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 10000.0m,
                    ProductId = context.Products.First(p => p.Sku == "120").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 16500m,
                    ProductId = context.Products.First(p => p.Sku == "120").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 3700.0m,
                    ProductId = context.Products.First(p => p.Sku == "10").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 6500m,
                    ProductId = context.Products.First(p => p.Sku == "10").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 6000.0m,
                    ProductId = context.Products.First(p => p.Sku == "119").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 8900m,
                    ProductId = context.Products.First(p => p.Sku == "119").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 22000.0m,
                    ProductId = context.Products.First(p => p.Sku == "117").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 25000m,
                    ProductId = context.Products.First(p => p.Sku == "117").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 36000.0m,
                    ProductId = context.Products.First(p => p.Sku == "236").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "236").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 10000.0m,
                    ProductId = context.Products.First(p => p.Sku == "283").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 15000m,
                    ProductId = context.Products.First(p => p.Sku == "283").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "167").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 35000m,
                    ProductId = context.Products.First(p => p.Sku == "167").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 8000.0m,
                    ProductId = context.Products.First(p => p.Sku == "128").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 18000m,
                    ProductId = context.Products.First(p => p.Sku == "128").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39500.0m,
                    ProductId = context.Products.First(p => p.Sku == "234").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 46000m,
                    ProductId = context.Products.First(p => p.Sku == "234").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 19000.0m,
                    ProductId = context.Products.First(p => p.Sku == "11").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 22000m,
                    ProductId = context.Products.First(p => p.Sku == "11").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 28500.0m,
                    ProductId = context.Products.First(p => p.Sku == "12").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 35000m,
                    ProductId = context.Products.First(p => p.Sku == "12").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39500.0m,
                    ProductId = context.Products.First(p => p.Sku == "235").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 46000m,
                    ProductId = context.Products.First(p => p.Sku == "235").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 21000.0m,
                    ProductId = context.Products.First(p => p.Sku == "13").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 25000m,
                    ProductId = context.Products.First(p => p.Sku == "13").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 48000.0m,
                    ProductId = context.Products.First(p => p.Sku == "349").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "349").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 869.47m,
                    ProductId = context.Products.First(p => p.Sku == "148").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1400m,
                    ProductId = context.Products.First(p => p.Sku == "148").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1150.0m,
                    ProductId = context.Products.First(p => p.Sku == "149").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1500m,
                    ProductId = context.Products.First(p => p.Sku == "149").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 3500.0m,
                    ProductId = context.Products.First(p => p.Sku == "118").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 5500m,
                    ProductId = context.Products.First(p => p.Sku == "118").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 710.0m,
                    ProductId = context.Products.First(p => p.Sku == "144").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1000m,
                    ProductId = context.Products.First(p => p.Sku == "144").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 708.33m,
                    ProductId = context.Products.First(p => p.Sku == "143").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1300m,
                    ProductId = context.Products.First(p => p.Sku == "143").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 0.0m,
                    ProductId = context.Products.First(p => p.Sku == "372").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "372").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "212").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "212").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1122").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "1122").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 38000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1255").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "1255").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 770.0m,
                    ProductId = context.Products.First(p => p.Sku == "240").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1400m,
                    ProductId = context.Products.First(p => p.Sku == "240").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 74000.0m,
                    ProductId = context.Products.First(p => p.Sku == "362_G").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 95000m,
                    ProductId = context.Products.First(p => p.Sku == "362_G").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 24000.0m,
                    ProductId = context.Products.First(p => p.Sku == "286").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 0m,
                    ProductId = context.Products.First(p => p.Sku == "286").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 74000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1340").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 78000m,
                    ProductId = context.Products.First(p => p.Sku == "1340").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 35500.0m,
                    ProductId = context.Products.First(p => p.Sku == "14").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 36500m,
                    ProductId = context.Products.First(p => p.Sku == "14").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 53000.0m,
                    ProductId = context.Products.First(p => p.Sku == "135").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45500m,
                    ProductId = context.Products.First(p => p.Sku == "135").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 105000.0m,
                    ProductId = context.Products.First(p => p.Sku == "15").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 115000m,
                    ProductId = context.Products.First(p => p.Sku == "15").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 22000.0m,
                    ProductId = context.Products.First(p => p.Sku == "16").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 24000m,
                    ProductId = context.Products.First(p => p.Sku == "16").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 22000.0m,
                    ProductId = context.Products.First(p => p.Sku == "17").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 24000m,
                    ProductId = context.Products.First(p => p.Sku == "17").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 34187.5m,
                    ProductId = context.Products.First(p => p.Sku == "18").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 31500m,
                    ProductId = context.Products.First(p => p.Sku == "18").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 38000.0m,
                    ProductId = context.Products.First(p => p.Sku == "19").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 32500m,
                    ProductId = context.Products.First(p => p.Sku == "19").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 33500.0m,
                    ProductId = context.Products.First(p => p.Sku == "177").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "177").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 1680.29m,
                    ProductId = context.Products.First(p => p.Sku == "145").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 1900m,
                    ProductId = context.Products.First(p => p.Sku == "145").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 3900.0m,
                    ProductId = context.Products.First(p => p.Sku == "146").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 4500m,
                    ProductId = context.Products.First(p => p.Sku == "146").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 5350.0m,
                    ProductId = context.Products.First(p => p.Sku == "147").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 6000m,
                    ProductId = context.Products.First(p => p.Sku == "147").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 12000.0m,
                    ProductId = context.Products.First(p => p.Sku == "330").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 22000m,
                    ProductId = context.Products.First(p => p.Sku == "330").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 63000.0m,
                    ProductId = context.Products.First(p => p.Sku == "296").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "296").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1049").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "1049").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "344").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "344").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "345").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "345").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 28000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1302").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 39000m,
                    ProductId = context.Products.First(p => p.Sku == "1302").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 31000.0m,
                    ProductId = context.Products.First(p => p.Sku == "20").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "20").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "142_D").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "142_D").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 50000.0m,
                    ProductId = context.Products.First(p => p.Sku == "282_A").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 75000m,
                    ProductId = context.Products.First(p => p.Sku == "282_A").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "209").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 78000m,
                    ProductId = context.Products.First(p => p.Sku == "209").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 48000.0m,
                    ProductId = context.Products.First(p => p.Sku == "193").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "193").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 42000.0m,
                    ProductId = context.Products.First(p => p.Sku == "146_S").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 52000m,
                    ProductId = context.Products.First(p => p.Sku == "146_S").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1476").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 65000m,
                    ProductId = context.Products.First(p => p.Sku == "1476").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 29500.0m,
                    ProductId = context.Products.First(p => p.Sku == "355").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 39500m,
                    ProductId = context.Products.First(p => p.Sku == "355").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1451").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 67000m,
                    ProductId = context.Products.First(p => p.Sku == "1451").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "332").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 85000m,
                    ProductId = context.Products.First(p => p.Sku == "332").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 23000.0m,
                    ProductId = context.Products.First(p => p.Sku == "163").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 39000m,
                    ProductId = context.Products.First(p => p.Sku == "163").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "264").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "264").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 175000.0m,
                    ProductId = context.Products.First(p => p.Sku == "141").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 198000m,
                    ProductId = context.Products.First(p => p.Sku == "141").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 35000.0m,
                    ProductId = context.Products.First(p => p.Sku == "388").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "388").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 25000.0m,
                    ProductId = context.Products.First(p => p.Sku == "186").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "186").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 67000.0m,
                    ProductId = context.Products.First(p => p.Sku == "194_J").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "194_J").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "21").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "21").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 52000.0m,
                    ProductId = context.Products.First(p => p.Sku == "284").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 78000m,
                    ProductId = context.Products.First(p => p.Sku == "284").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 64000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1297").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 77000m,
                    ProductId = context.Products.First(p => p.Sku == "1297").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 43000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1294").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "1294").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 60000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1030").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 82000m,
                    ProductId = context.Products.First(p => p.Sku == "1030").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39000.0m,
                    ProductId = context.Products.First(p => p.Sku == "153").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 52000m,
                    ProductId = context.Products.First(p => p.Sku == "153").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 25000.0m,
                    ProductId = context.Products.First(p => p.Sku == "169").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 36000m,
                    ProductId = context.Products.First(p => p.Sku == "169").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 68000.0m,
                    ProductId = context.Products.First(p => p.Sku == "180").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 82000m,
                    ProductId = context.Products.First(p => p.Sku == "180").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "23").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 57000m,
                    ProductId = context.Products.First(p => p.Sku == "23").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "22").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "22").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 57000.0m,
                    ProductId = context.Products.First(p => p.Sku == "192").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "192").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "150_E").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "150_E").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "157").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 68000m,
                    ProductId = context.Products.First(p => p.Sku == "157").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "151_H").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 85000m,
                    ProductId = context.Products.First(p => p.Sku == "151_H").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "24").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "24").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "204").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 72000m,
                    ProductId = context.Products.First(p => p.Sku == "204").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 38000.0m,
                    ProductId = context.Products.First(p => p.Sku == "292").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "292").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "293").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "293").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "295").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 95000m,
                    ProductId = context.Products.First(p => p.Sku == "295").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 155000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1543").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 198000m,
                    ProductId = context.Products.First(p => p.Sku == "1543").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 115000.0m,
                    ProductId = context.Products.First(p => p.Sku == "387").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 150000m,
                    ProductId = context.Products.First(p => p.Sku == "387").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39000.0m,
                    ProductId = context.Products.First(p => p.Sku == "276").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "276").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "385").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "385").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "290").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 75000m,
                    ProductId = context.Products.First(p => p.Sku == "290").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 67000.0m,
                    ProductId = context.Products.First(p => p.Sku == "196").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "196").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 69000.0m,
                    ProductId = context.Products.First(p => p.Sku == "331").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 82000m,
                    ProductId = context.Products.First(p => p.Sku == "331").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39000.0m,
                    ProductId = context.Products.First(p => p.Sku == "218").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 52000m,
                    ProductId = context.Products.First(p => p.Sku == "218").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 44000.0m,
                    ProductId = context.Products.First(p => p.Sku == "222").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "222").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 67500.0m,
                    ProductId = context.Products.First(p => p.Sku == "166_W").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 78000m,
                    ProductId = context.Products.First(p => p.Sku == "166_W").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 44000.0m,
                    ProductId = context.Products.First(p => p.Sku == "224").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 68000m,
                    ProductId = context.Products.First(p => p.Sku == "224").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "147_B").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "147_B").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 50000.0m,
                    ProductId = context.Products.First(p => p.Sku == "375").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "375").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 60000.0m,
                    ProductId = context.Products.First(p => p.Sku == "347").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 77000m,
                    ProductId = context.Products.First(p => p.Sku == "347").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "348").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "348").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 70000.0m,
                    ProductId = context.Products.First(p => p.Sku == "165").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 85000m,
                    ProductId = context.Products.First(p => p.Sku == "165").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "394").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 105000m,
                    ProductId = context.Products.First(p => p.Sku == "394").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 35000.0m,
                    ProductId = context.Products.First(p => p.Sku == "148_S").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "148_S").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "210").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "210").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 68000.0m,
                    ProductId = context.Products.First(p => p.Sku == "170").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 78000m,
                    ProductId = context.Products.First(p => p.Sku == "170").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "281").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "281").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 44000.0m,
                    ProductId = context.Products.First(p => p.Sku == "223").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "223").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1301").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 95000m,
                    ProductId = context.Products.First(p => p.Sku == "1301").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 42000.0m,
                    ProductId = context.Products.First(p => p.Sku == "225").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "225").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "327").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 70000m,
                    ProductId = context.Products.First(p => p.Sku == "327").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 77000.0m,
                    ProductId = context.Products.First(p => p.Sku == "384").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 87000m,
                    ProductId = context.Products.First(p => p.Sku == "384").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "323").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 115000m,
                    ProductId = context.Products.First(p => p.Sku == "323").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "144_X").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 92000m,
                    ProductId = context.Products.First(p => p.Sku == "144_X").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "143_F").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "143_F").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 64000.0m,
                    ProductId = context.Products.First(p => p.Sku == "173").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 77000m,
                    ProductId = context.Products.First(p => p.Sku == "173").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "26").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 72000m,
                    ProductId = context.Products.First(p => p.Sku == "26").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 82000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1579").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 980000m,
                    ProductId = context.Products.First(p => p.Sku == "1579").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 95000.0m,
                    ProductId = context.Products.First(p => p.Sku == "131").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 120000m,
                    ProductId = context.Products.First(p => p.Sku == "131").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 85000.0m,
                    ProductId = context.Products.First(p => p.Sku == "220").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "220").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 85000.0m,
                    ProductId = context.Products.First(p => p.Sku == "188").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 97000m,
                    ProductId = context.Products.First(p => p.Sku == "188").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "27").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 80000m,
                    ProductId = context.Products.First(p => p.Sku == "27").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 100000.0m,
                    ProductId = context.Products.First(p => p.Sku == "28").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 119000m,
                    ProductId = context.Products.First(p => p.Sku == "28").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 39000.0m,
                    ProductId = context.Products.First(p => p.Sku == "152_V").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "152_V").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 42000.0m,
                    ProductId = context.Products.First(p => p.Sku == "259").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "259").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "29").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "29").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 42000.0m,
                    ProductId = context.Products.First(p => p.Sku == "25").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "25").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 62000.0m,
                    ProductId = context.Products.First(p => p.Sku == "30").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 73000m,
                    ProductId = context.Products.First(p => p.Sku == "30").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "187").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "187").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "155").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 40000m,
                    ProductId = context.Products.First(p => p.Sku == "155").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "98").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "98").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 35000.0m,
                    ProductId = context.Products.First(p => p.Sku == "158").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 55000m,
                    ProductId = context.Products.First(p => p.Sku == "158").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "41").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 58000m,
                    ProductId = context.Products.First(p => p.Sku == "41").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 85000.0m,
                    ProductId = context.Products.First(p => p.Sku == "42").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 139000m,
                    ProductId = context.Products.First(p => p.Sku == "42").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 70000.0m,
                    ProductId = context.Products.First(p => p.Sku == "43").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 80000m,
                    ProductId = context.Products.First(p => p.Sku == "43").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 105000.0m,
                    ProductId = context.Products.First(p => p.Sku == "31").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 128000m,
                    ProductId = context.Products.First(p => p.Sku == "31").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 90000.0m,
                    ProductId = context.Products.First(p => p.Sku == "195").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 135000m,
                    ProductId = context.Products.First(p => p.Sku == "195").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 70000.0m,
                    ProductId = context.Products.First(p => p.Sku == "214_B").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 92000m,
                    ProductId = context.Products.First(p => p.Sku == "214_B").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "291").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "291").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 38000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1292").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 49000m,
                    ProductId = context.Products.First(p => p.Sku == "1292").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 30000.0m,
                    ProductId = context.Products.First(p => p.Sku == "275").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "275").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1531").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 75000m,
                    ProductId = context.Products.First(p => p.Sku == "1531").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "33").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "33").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 140000.0m,
                    ProductId = context.Products.First(p => p.Sku == "322").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 205000m,
                    ProductId = context.Products.First(p => p.Sku == "322").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "389").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 96000m,
                    ProductId = context.Products.First(p => p.Sku == "389").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 68000.0m,
                    ProductId = context.Products.First(p => p.Sku == "179").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 88000m,
                    ProductId = context.Products.First(p => p.Sku == "179").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 90000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1544").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 125000m,
                    ProductId = context.Products.First(p => p.Sku == "1544").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 45000.0m,
                    ProductId = context.Products.First(p => p.Sku == "289").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "289").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 68000.0m,
                    ProductId = context.Products.First(p => p.Sku == "382").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "382").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 20000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1377").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "1377").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 55000.0m,
                    ProductId = context.Products.First(p => p.Sku == "200").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 69000m,
                    ProductId = context.Products.First(p => p.Sku == "200").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 58000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1434").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 89000m,
                    ProductId = context.Products.First(p => p.Sku == "1434").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "279").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "279").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "343").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 65000m,
                    ProductId = context.Products.First(p => p.Sku == "343").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 100000.0m,
                    ProductId = context.Products.First(p => p.Sku == "237").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 115000m,
                    ProductId = context.Products.First(p => p.Sku == "237").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1052").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 115000m,
                    ProductId = context.Products.First(p => p.Sku == "1052").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 25000.0m,
                    ProductId = context.Products.First(p => p.Sku == "312").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 38000m,
                    ProductId = context.Products.First(p => p.Sku == "312").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "391").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "391").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 44000.0m,
                    ProductId = context.Products.First(p => p.Sku == "228").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 58000m,
                    ProductId = context.Products.First(p => p.Sku == "228").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 18000.0m,
                    ProductId = context.Products.First(p => p.Sku == "298").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 38000m,
                    ProductId = context.Products.First(p => p.Sku == "298").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "329").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 77000m,
                    ProductId = context.Products.First(p => p.Sku == "329").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 80000.0m,
                    ProductId = context.Products.First(p => p.Sku == "199").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 110000m,
                    ProductId = context.Products.First(p => p.Sku == "199").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 25000.0m,
                    ProductId = context.Products.First(p => p.Sku == "280").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 50000m,
                    ProductId = context.Products.First(p => p.Sku == "280").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 85000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1477").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 98000m,
                    ProductId = context.Products.First(p => p.Sku == "1477").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 125000.0m,
                    ProductId = context.Products.First(p => p.Sku == "266").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 165000m,
                    ProductId = context.Products.First(p => p.Sku == "266").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 90000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1413").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 138000m,
                    ProductId = context.Products.First(p => p.Sku == "1413").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 160000.0m,
                    ProductId = context.Products.First(p => p.Sku == "324").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 195000m,
                    ProductId = context.Products.First(p => p.Sku == "324").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 65000.0m,
                    ProductId = context.Products.First(p => p.Sku == "288").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 86000m,
                    ProductId = context.Products.First(p => p.Sku == "288").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 25000.0m,
                    ProductId = context.Products.First(p => p.Sku == "35").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 38000m,
                    ProductId = context.Products.First(p => p.Sku == "35").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 64000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1296").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 77000m,
                    ProductId = context.Products.First(p => p.Sku == "1296").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 70000.0m,
                    ProductId = context.Products.First(p => p.Sku == "198").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 105000m,
                    ProductId = context.Products.First(p => p.Sku == "198").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 68000.0m,
                    ProductId = context.Products.First(p => p.Sku == "328").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 79000m,
                    ProductId = context.Products.First(p => p.Sku == "328").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 40000.0m,
                    ProductId = context.Products.First(p => p.Sku == "1103").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 45000m,
                    ProductId = context.Products.First(p => p.Sku == "1103").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 48000.0m,
                    ProductId = context.Products.First(p => p.Sku == "272").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 59000m,
                    ProductId = context.Products.First(p => p.Sku == "272").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Wholesale Price",
                    Price = 75000.0m,
                    ProductId = context.Products.First(p => p.Sku == "44").Id,
                    PriceGroupId = wholesaleGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductPrice
                {
                    GroupPrice = "Retail Price",
                    Price = 82000m,
                    ProductId = context.Products.First(p => p.Sku == "44").Id,
                    PriceGroupId = retailGroup.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductPrices.AddRange(productPrices);
            context.SaveChanges();

            // Ensure related entities (Employee, Asset) are already seeded
            var asset = context.Assets.FirstOrDefault();

            if (employee == null || asset == null)
            {
                throw new InvalidOperationException("Employees and assets must be seeded before seeding employee assets.");
            }

            // Seed Employee Assets (Example Data)
            var employeeAssets = new List<EmployeeAsset>
            {
                new EmployeeAsset
                {
                    EmployeeId = employee.Id,
                    AssetId = asset.Id,
                    AssignmentDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-30)), // Assigned 30 days ago
                    ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)), // Return in 30 days
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.EmployeeAssets.AddRange(employeeAssets);

            // Save changes to the database
            context.SaveChanges();

            var expenseCategories = new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    Code = "OFF001",
                    Name = "Office Supplies",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ExpenseCategory
                {
                    Code = "TRV002",
                    Name = "Travel",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ExpenseCategories.AddRange(expenseCategories);

            // Save changes to the database
            context.SaveChanges();

            var expenses = new List<Expense>
            {
                new Expense
                {
                    ReferenceNo = "EXP001",
                    Amount = 500.00m,
                    Note = "Purchased office supplies",
                    ExpenseCategoryId = context.ExpenseCategories.FirstOrDefault(ec => ec.Code == "OFF001")?.Id,
                    WarehouseId = 1, // Assume Warehouse with Id 1 exists
                    UserId = 1, // Assume User with Id 1 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Expense
                {
                    ReferenceNo = "EXP002",
                    Amount = 1200.00m,
                    Note = "Business travel expenses",
                    ExpenseCategoryId = context.ExpenseCategories.FirstOrDefault(ec => ec.Code == "TRV002")?.Id,
                    WarehouseId = 1,
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Expenses.AddRange(expenses);

            // Save changes to the database
            context.SaveChanges();

            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    ReferenceNo = "INV001",
                    Name = "January 2024 Stock Count",
                    Status = "Completed",
                    NbrProducts = 50,
                    InventoryMonth = 1,
                    InventoryYear = 2024,
                    DiffAmount = 1200.50m,
                    Gap = 10,
                    WarehouseId = 1, // Assume Warehouse with Id 1 exists
                    UserId = 1, // Assume User with Id 1 exists
                    Date = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Inventories.AddRange(inventories);

            // Save changes to the database
            context.SaveChanges();

            var inventoryItems = new List<InventoryItem>
            {
                new InventoryItem
                {
                    Name = "Product A",
                    Price = 20.00m,
                    TheoricalStock = 1,
                    PhysicalStock = 90,
                    Gap = 10,
                    DiffAmount = 200.00m,
                    ItemId = 1, // Assume Product with Id 1 exists
                    InventoryId = context.Inventories.FirstOrDefault(inv => inv.ReferenceNo == "INV001")?.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.InventoryItems.AddRange(inventoryItems);

            // Save changes to the database
            context.SaveChanges();

            var leaveRequests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    EmployeeId = 1, // Assume Employee with Id 1 exists
                    LeaveType = "Vacation",
                    StartDate = new DateOnly(2024, 1, 10),
                    EndDate = new DateOnly(2024, 1, 20),
                    Reason = "Family Vacation",
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new LeaveRequest
                {
                    EmployeeId = 2, // Assume Employee with Id 2 exists
                    LeaveType = "Sick Leave",
                    StartDate = new DateOnly(2024, 2, 5),
                    EndDate = new DateOnly(2024, 2, 8),
                    Reason = "Medical Issue",
                    Status = "Approved",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.LeaveRequests.AddRange(leaveRequests);

            // Save changes to the database
            context.SaveChanges();

            var productAdjustments = new List<ProductAdjustment>
            {
                new ProductAdjustment
                {
                    Qty = 10,
                    Action = "Add",
                    AdjustmentId = 1, // Assume Adjustment with Id 1 exists
                    ProductId = 1, // Assume Product with Id 1 exists
                    VariantId = null, // No variant
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductAdjustment
                {
                    Qty = -5,
                    Action = "Remove",
                    AdjustmentId = 2, // Assume Adjustment with Id 2 exists
                    ProductId = 2, // Assume Product with Id 2 exists
                    VariantId = 1, // Assume Variant with Id 1 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductAdjustments.AddRange(productAdjustments);

            // Save changes to the database
            context.SaveChanges();

            var productBarCodes = new List<ProductBarCode>
            {
                new ProductBarCode
                {
                    Value = "123456789012",
                    ProductId = 1, // Assume Product with Id 1 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductBarCode
                {
                    Value = "987654321098",
                    ProductId = 2, // Assume Product with Id 2 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductBarCodes.AddRange(productBarCodes);

            // Save changes to the database
            context.SaveChanges();

            var productBatches = new List<ProductBatch>
            {
                new ProductBatch
                {
                    Qty = 1,
                    ExpiredDate = DateTime.UtcNow.AddMonths(6), // Assume expiry in 6 months
                    BatchNo = "BATCH001",
                    ProductId = 1, // Assume Product with Id 1 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductBatch
                {
                    Qty = 2,
                    ExpiredDate = DateTime.UtcNow.AddMonths(12), // Assume expiry in 12 months
                    BatchNo = "BATCH002",
                    ProductId = 2, // Assume Product with Id 2 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductBatches.AddRange(productBatches);

            // Save changes to the database
            context.SaveChanges();

            var productFields = new List<ProductField>
            {
                new ProductField
                {
                    Name = "Color",
                    Description = "The color of the product",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductField
                {
                    Name = "Size",
                    Description = "The size of the product",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductFields.AddRange(productFields);

            // Save changes to the database
            context.SaveChanges();

            var colorField = context.ProductFields.FirstOrDefault(f => f.Name == "Color");
            var sizeField = context.ProductFields.FirstOrDefault(f => f.Name == "Size");

            if (colorField == null || sizeField == null)
            {
                return; // Ensure the fields are already created
            }

            var productFieldValues = new List<ProductFieldValue>
            {
                new ProductFieldValue
                {
                    Value = "Red",
                    ProductFieldId = colorField.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductFieldValue
                {
                    Value = "Blue",
                    ProductFieldId = colorField.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductFieldValue
                {
                    Value = "Small",
                    ProductFieldId = sizeField.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductFieldValue
                {
                    Value = "Large",
                    ProductFieldId = sizeField.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductFieldValues.AddRange(productFieldValues);

            // Save changes to the database
            context.SaveChanges();

            var redColorField = context.ProductFields.FirstOrDefault(f => f.Name == "Color");
            var product = context.Products.FirstOrDefault(p => p.ProductName == "Sample Product");

            if (redColorField == null || sizeField == null || product == null)
            {
                return; // Ensure necessary fields and products exist
            }

            var productHasFields = new List<ProductHasField>
            {
                new ProductHasField
                {
                    Name = "Color",
                    Value = "Red",
                    ProductFieldId = redColorField.Id,
                    ProductId = product.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new ProductHasField
                {
                    Name = "Size",
                    Value = "Large",
                    ProductFieldId = sizeField.Id,
                    ProductId = product.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductHasFields.AddRange(productHasFields);

            // Save changes to the database
            context.SaveChanges();

            var returnPurchase = context.ReturnPurchases.FirstOrDefault();
            var productBatch = context.ProductBatches.FirstOrDefault();

            if (returnPurchase == null || product == null || productBatch == null)
            {
                return; // Ensure necessary return and product entities exist
            }

            var productPurchaseReturns = new List<ProductPurchaseReturn>
            {
                new ProductPurchaseReturn
                {
                    ReturnId = returnPurchase.Id,
                    ProductId = product.Id,
                    ProductBatchId = productBatch.Id,
                    ImeiNumber = "123456789012345",
                    Qty = 5,
                    NetUnitPrice = 100.00m,
                    Discount = 10.00m,
                    TaxRate = 0.15m,
                    Tax = 13.50m,
                    Total = 503.50m, // (NetUnitPrice * Qty) - Discount + Tax
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductPurchaseReturns.AddRange(productPurchaseReturns);

            // Save changes to the database
            context.SaveChanges();

            var returnTransaction = context.Returns.FirstOrDefault();
            var variant = context.Variations.FirstOrDefault();

            if (returnTransaction == null || product == null)
            {
                return; // Ensure necessary data exists
            }

            var productReturns = new List<ProductReturn>
            {
                new ProductReturn
                {
                    ReturnId = returnTransaction.Id,
                    ProductId = product.Id,
                    ProductBatchId = productBatch?.Id,
                    VariantId = variant?.Id,
                    ImeiNumber = "123456789012345",
                    Qty = 10,
                    NetUnitPrice = 150.00m,
                    Discount = 20.00m,
                    TaxRate = 0.10m,
                    Tax = 13.00m,
                    Total = 1330.00m, // ((NetUnitPrice * Qty) - Discount) + Tax
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductReturns.AddRange(productReturns);

            // Save changes to the database
            context.SaveChanges();

            var transfer = context.Transfers.FirstOrDefault();

            if (transfer == null || product == null)
            {
                return; // Ensure necessary data exists
            }

            var productTransfers = new List<ProductTransfer>
            {
                new ProductTransfer
                {
                    TransferId = transfer.Id,
                    ProductId = product.Id,
                    ProductBatchId = productBatch?.Id,
                    VariantId = variant?.Id,
                    ImeiNumber = "987654321098765",
                    Qty = 50,
                    NetUnitCost = 100.00m,
                    TaxRate = 0.15m,
                    Tax = 7.50m,
                    Total = 107.50m, // (NetUnitCost * Qty) + Tax
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProductTransfers.AddRange(productTransfers);

            // Save changes to the database
            context.SaveChanges();

            var manager = context.Employees.FirstOrDefault();

            var projects = new List<Project>
            {
                new Project
                {
                    Name = "New Website Development",
                    Description = "Development of a new company website.",
                    StartDate = new DateOnly(2024, 01, 01),
                    EndDate = new DateOnly(2024, 06, 01),
                    Budget = 50000.00m,
                    Status = "In Progress",
                    ManagerId = manager.Id,
                    CustomerId = customer.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Projects.AddRange(projects);

            // Save changes to the database
            context.SaveChanges();

            var project = context.Projects.FirstOrDefault();

            var documents = new List<ProjectDocument>
            {
                new ProjectDocument
                {
                    ProjectId = project.Id,
                    DocumentName = "Project Plan",
                    DocumentType = "PDF",
                    DocumentPath = "/documents/project_plan.pdf",
                    UploadDate = new DateOnly(2024, 01, 01),
                    UploadedBy = user.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProjectDocuments.AddRange(documents);

            // Save changes to the database
            context.SaveChanges();

            if (project == null || user == null)
            {
                return; // Ensure necessary data exists
            }

            var milestones = new List<ProjectMilestone>
            {
                new ProjectMilestone
                {
                    ProjectId = project.Id,
                    Name = "Initial Planning",
                    Description = "Complete the initial planning and scope definition for the project.",
                    DueDate = new DateOnly(2024, 01, 15),
                    Status = "In Progress",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProjectMilestones.AddRange(milestones);

            // Save changes to the database
            context.SaveChanges();

            var tasks = new List<ProjectTask>
            {
                new ProjectTask
                {
                    ProjectId = project.Id,
                    Name = "Design UI",
                    Description = "Create and design the user interface for the project",
                    AssignedTo = employee.Id,
                    DueDate = new DateOnly(2024, 01, 20),
                    Status = "In Progress",
                    Priority = "High",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProjectTasks.AddRange(tasks);

            // Save changes to the database
            context.SaveChanges();

            var teamMembers = new List<ProjectTeamMember>
            {
                new ProjectTeamMember
                {
                    ProjectId = project.Id,
                    EmployeeId = employee.Id,
                    Role = "Developer",
                    JoinDate = new DateOnly(2024, 01, 01),
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ProjectTeamMembers.AddRange(teamMembers);

            // Save changes to the database
            context.SaveChanges();

            var purchases = new List<Purchase>
            {
                new Purchase
                {
                    Id = 1,
                    PurchaseType = "Retail",
                    ReferenceNo = "PO123456",
                    PurchaseDate = DateTime.UtcNow,
                    PurchaseStatus = "Completed",
                    DiscountType = "Percentage",
                    DiscountAmount = 10.5M,
                    AdditionalNotes = "Urgent purchase",
                    ShippingDetails = "Express Shipping",
                    AdditionalShippingCharges = 25.00M,
                    NetTotalAmount = 500.00M,
                    PaymentSatus = "Paid",
                    NumberItems = 5,
                    PaidAmount = 475.00M,
                    TotalTax = 25.00M,
                    TotalDiscount = 50.00M,
                    Due = 0.00M,
                    ReturnAmount = 0.00M,
                    Notes = "Order fulfilled",
                    PurchaseYear = 2024,
                    PurchaseMonth = 10,
                    SupplierId = 1,
                    BusinessLocationId = 1,
                    WarehouseId = 1,
                    UserId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Purchase
                {
                    Id = 2,
                    PurchaseType = "Wholesale",
                    ReferenceNo = "PO654321",
                    PurchaseDate = DateTime.UtcNow.AddDays(-10),
                    PurchaseStatus = "Pending",
                    DiscountType = "Fixed",
                    DiscountAmount = 20.00M,
                    AdditionalNotes = "Bulk order for the warehouse",
                    ShippingDetails = "Standard Shipping",
                    AdditionalShippingCharges = 10.00M,
                    NetTotalAmount = 1000.00M,
                    PaymentSatus = "Due",
                    NumberItems = 10,
                    PaidAmount = 980.00M,
                    TotalTax = 30.00M,
                    TotalDiscount = 20.00M,
                    Due = 50.00M,
                    ReturnAmount = 0.00M,
                    Notes = "Pending payment",
                    PurchaseYear = 2024,
                    PurchaseMonth = 9,
                    SupplierId = 2,
                    BusinessLocationId = 2,
                    WarehouseId = 2,
                    UserId = 2,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                }
            };

            context.Purchases.AddRange(purchases);

            // Save changes to the database
            context.SaveChanges();

            var purchasesDetail = new List<PurchaseDetail>
            {
                new PurchaseDetail
                {
                    ProductName = "Product 1",
                    PurchaseQuantity = 10,
                    UnitCostBd = 50.00M,
                    DiscountPercent = 5.0M,
                    UnitCostBt = 47.50M,
                    UnitSellingPrice = 60.00M,
                    ProfitMargin = 20.0M,
                    ExpiryDate = DateTime.UtcNow.AddYears(1),
                    ManufacturingDate = DateTime.UtcNow.AddMonths(-6),
                    LineTotal = 475.00M,
                    ProductId = 1,
                    PurchaseId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new PurchaseDetail
                {
                    ProductName = "Product 2",
                    PurchaseQuantity = 20,
                    UnitCostBd = 30.00M,
                    DiscountPercent = 0.0M,
                    UnitCostBt = 30.00M,
                    UnitSellingPrice = 40.00M,
                    ProfitMargin = 25.0M,
                    ExpiryDate = DateTime.UtcNow.AddYears(2),
                    ManufacturingDate = DateTime.UtcNow.AddMonths(-3),
                    LineTotal = 600.00M,
                    ProductId = 2,
                    PurchaseId = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.PurchaseDetails.AddRange(purchasesDetail);

            // Save changes to the database
            context.SaveChanges();

            var reward = new List<Reward>
            {
                 new Reward
                {
                    Name = "Free Coffee",
                    PointsRequired = 100M,
                    Description = "Get a free coffee with 100 points.",
                    Image = null,  // Assuming you'd populate this later
                    ExpirationDate = DateTime.UtcNow.AddMonths(1),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Reward
                {
                    Name = "Discount Coupon",
                    PointsRequired = 200M,
                    Description = "A 20% discount on your next purchase.",
                    Image = null,
                    ExpirationDate = DateTime.UtcNow.AddMonths(2),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };
            
            context.Rewards.AddRange(reward);

            // Save changes to the database
            context.SaveChanges();

            var redemptions = new List<Redemption>
            {
                new Redemption
                {
                    RedemptionDate = DateTime.UtcNow,
                    PointsSpent = 100M,
                    CustomerId = 1,  // Assuming customer with Id 1 exists
                    RewardId = 1,    // Assuming reward with Id 1 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Redemption
                {
                    RedemptionDate = DateTime.UtcNow,
                    PointsSpent = 200M,
                    CustomerId = 2,  // Assuming customer with Id 2 exists
                    RewardId = 2,    // Assuming reward with Id 2 exists
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Redemptions.AddRange(redemptions);

            // Save changes to the database
            context.SaveChanges();

            var registerRecords = new List<RegisterRecord>
            {
                new RegisterRecord
                {
                    UserId = 1,  // Assuming User with Id 1 exists
                    BusinessLocationId = 1,  // Assuming BusinessLocation with Id 1 exists
                    RegisterId = 1,  // Assuming Register with Id 1 exists
                    TotalCashAmount = 500.00m,
                    TotalCashSubmitted = 480.00m,
                    TotalCheques = 2,
                    TotalChequesAmount = 300.00m,
                    TotalChequesSubmitted = 300.00m,
                    TotalOtherAmount = 100.00m,
                    TotalRefundsAmount = 20.00m,
                    TotalExpensesAmount = 50.00m,
                    TotalGiftCardAmount = 30.00m,
                    TotalReturnOrdersAmount = 15.00m,
                    CashInHand = 450.00m,
                    ClosedById = 2,  // Assuming User with Id 2 closed the register
                    ClosedAt = DateTime.UtcNow,
                    TransferredToId = 3,  // Assuming User with Id 3 is the transferee
                    Comment = "End of day register closing.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.RegisterRecords.AddRange(registerRecords);

            // Save changes to the database
            context.SaveChanges();

            var returnTransactions = new List<Return>
            {
                new Return
                {
                    ReferenceNo = "RTN-001",
                    Status = "Pending",
                    Action = "Refund",
                    UserId = 1,
                    SaleId = 10,
                    CashRegisterId = 3,
                    CustomerId = 2,
                    WarehouseId = 5,
                    TotalQty = 3,
                    TotalDiscount = 20.00M,
                    TotalTax = 5.00M,
                    TotalPrice = 100.00M,
                    OrderTaxRate = 0.10M,
                    OrderTax = 10.00M,
                    GrandTotal = 90.00M,
                    Doc = null,
                    ReturnNote = "Customer returned due to wrong size.",
                    StaffNote = "Processed by cashier John Doe.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Returns.AddRange(returnTransactions);

            // Save changes to the database
            context.SaveChanges();

            var returnPurchases = new List<ReturnPurchase>
            {
                new ReturnPurchase
                {
                    ReferenceNo = "RP-001",
                    Action = "Refund",
                    Status = "Completed",
                    UserId = 1,
                    PurchaseId = 1,
                    CashRegisterId = 3,
                    SupplierId = 5,
                    WarehouseId = 2,
                    TotalQty = 10,
                    TotalDiscount = 50.00M,
                    TotalTax = 5.00M,
                    TotalPrice = 200.00M,
                    OrderTaxRate = 0.05M,
                    OrderTax = 10.00M,
                    GrandTotal = 210.00M,
                    ReturnNote = "Returned due to incorrect shipment.",
                    StaffNote = "Processed by Jane Doe.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.ReturnPurchases.AddRange(returnPurchases);

            // Save changes to the database
            context.SaveChanges();

            var rewardHistory = new List<RewardHistory>
            {
                new RewardHistory
                {
                    DateClaimed = DateTime.UtcNow,
                    Status = "Completed",
                    CustomerId = 1,
                    RewardId = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new RewardHistory
                {
                    DateClaimed = DateTime.UtcNow,
                    Status = "Pending",
                    CustomerId = 3,
                    RewardId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.RewardHistories.AddRange(rewardHistory);

            // Save changes to the database
            context.SaveChanges();

            var sale = new Sale
            {
                SaleType = "Retail",
                ReferenceNo = "SALE001",
                SaleDate = DateTime.UtcNow,
                SaleStatus = "Completed",
                DiscountType = "Percentage",
                DiscountAmount = 50M,
                ShippingDetails = "Standard shipping",
                NetTotalAmount = 500M,
                PaidAmount = 500M,
                CustomerId = 1,
                BusinessLocationId = 2,
                WarehouseId = 3,
                UserId = 4,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Sales.Add(sale);

            // Save changes to the database
            context.SaveChanges();

            var saleDetail = new SaleDetail
            {
                ProductName = "Laptop",
                SaleQuantity = 2,
                UnitCostBd = 800M,
                DiscountPercent = 10M,
                UnitCostBt = 720M,
                UnitSellingPrice = 1000M,
                ProfitMargin = 280M,
                LineTotal = 2000M, // 2 * 1000 (UnitSellingPrice)
                ProductId = 1,
                SaleId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.SaleDetails.Add(saleDetail);

            // Save changes to the database
            context.SaveChanges();

            var saleHold = new SaleHold
            {
                SaleType = "In-Store",
                ReferenceNo = "REF12345",
                SaleDate = DateTime.UtcNow,
                SaleStatus = "On Hold",
                DiscountType = "Percentage",
                DiscountAmount = 10M,
                AdditionalNotes = "Customer will finalize payment tomorrow",
                ShippingDetails = "Standard shipping",
                AdditionalShippingCharges = 5.00M,
                NetTotalAmount = 150.00M,
                PaymentSatus = "Pending",
                NumberItems = 3,
                PaidAmount = 0.00M,
                TotalTax = 12.50M,
                TotalDiscount = 10.00M,
                Due = 150.00M,
                Notes = "Awaiting payment",
                SaleYear = DateTime.UtcNow.Year,
                SaleMonth = DateTime.UtcNow.Month,
                CustomerId = 1,
                BusinessLocationId = 1,
                WarehouseId = 1,
                UserId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.SaleHolds.Add(saleHold);

            // Save changes to the database
            context.SaveChanges();

            var salePayment = new SalePayment
            {
                SaleId = 1,  // Assuming SaleId 1 exists
                CustomerId = 1,  // Assuming CustomerId 1 exists
                BusinessLocationId = 1,  // Assuming BusinessLocationId 1 exists
                UserId = 1,  // Assuming UserId 1 exists
                Amount = 200.00M,
                Paid = 150.00M,
                Remaining = 50.00M,
                Due = 50.00M,
                DueDate = DateTime.UtcNow.AddDays(7),
                IsPaid = false,  // Still some amount remaining
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.SalePayments.Add(salePayment);

            // Save changes to the database
            context.SaveChanges();

            if (businessLocation == null || employee == null || user == null)
            {
                throw new InvalidOperationException("Business location, employee, and user must be seeded before seeding wastes.");
            }

            // Seed Waste Records (Example Data)
            var wastes = new List<Waste>
            {
                new Waste
                {
                    ReferenceNo = "WASTE001",
                    Date = new DateOnly(2024, 1, 15),
                    TotalLoss = 500.00m,
                    Note = "Damaged goods due to handling",
                    Items = 10,
                    EmployeeId = employee.Id,
                    BusinessLocationId = businessLocation.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Waste
                {
                    ReferenceNo = "WASTE002",
                    Date = new DateOnly(2024, 2, 10),
                    TotalLoss = 300.00m,
                    Note = "Spoiled inventory",
                    Items = 5,
                    EmployeeId = employee.Id,
                    BusinessLocationId = businessLocation.Id,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Wastes.AddRange(wastes);

            // Save changes to the database
            context.SaveChanges();

            // Ensure the waste records are seeded
            var waste1 = context.Wastes.FirstOrDefault(w => w.ReferenceNo == "WASTE001");
            var waste2 = context.Wastes.FirstOrDefault(w => w.ReferenceNo == "WASTE002");

            if (waste1 == null || waste2 == null)
            {
                throw new InvalidOperationException("Wastes must be seeded before seeding waste items.");
            }

            // Seed WasteItems (Example Data)
            var wasteItems = new List<WasteItem>
            {
                new WasteItem
                {
                    ItemName = "Broken Chair",
                    WasteAmount = 100.00m,
                    LastPurchasePrice = 120.00m,
                    LossAmount = 100.00m,
                    Qty = 1,
                    WasteId = waste1.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new WasteItem
                {
                    ItemName = "Damaged Table",
                    WasteAmount = 200.00m,
                    LastPurchasePrice = 250.00m,
                    LossAmount = 200.00m,
                    Qty = 2,
                    WasteId = waste1.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
            };

            context.WasteItems.AddRange(wasteItems);

            // Save changes to the database
            context.SaveChanges();

            if (adminUser == null || standardUser == null || adminRole == null || userRole == null)
            {
                throw new InvalidOperationException("Users and Roles must be seeded before seeding user-role assignments.");
            }

            // Seed UserHasRole (Example Data)
            var userHasRoles = new List<UserHasRole>
            {
                new UserHasRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserHasRole
                {
                    UserId = standardUser.Id,
                    RoleId = userRole.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.UserHasRoles.AddRange(userHasRoles);

            // Save changes to the database
            context.SaveChanges();

            if (adminUser == null || standardUser == null || addCustomerPermission == null || editCustomerPermission == null)
            {
                throw new InvalidOperationException("Users and Permissions must be seeded before seeding user-permission assignments.");
            }

            // Seed UserHasPermission (Example Data)
            var userHasPermissions = new List<UserHasPermission>
            {
                new UserHasPermission
                {
                    UserId = adminUser.Id,
                    PermissionId = addCustomerPermission.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserHasPermission
                {
                    UserId = adminUser.Id,
                    PermissionId = editCustomerPermission.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new UserHasPermission
                {
                    UserId = standardUser.Id,
                    PermissionId = addCustomerPermission.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.UserHasPermissions.AddRange(userHasPermissions);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
