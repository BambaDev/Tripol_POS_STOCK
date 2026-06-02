using Pos.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    /// <summary>
    /// Gère l'archivage et la restauration des produits (Soft Delete)
    /// Alternative sécurisée à la suppression définitive
    /// </summary>
    public static class ProductArchiveManager
    {
        /// <summary>
        /// Archive un produit (soft delete)
        /// </summary>
        /// <param name="productId">ID du produit à archiver</param>
        /// <param name="userId">ID de l'utilisateur qui archive</param>
        /// <param name="reason">Raison de l'archivage (optionnel)</param>
        /// <param name="context">Contexte DB</param>
        /// <returns>Tuple (success, message)</returns>
        public static (bool success, string message) ArchiveProduct(
            int productId,
            int userId,
            string reason,
            AppDbContext context)
        {
            try
            {
                var product = context.Products.Find(productId);

                if (product == null)
                {
                    return (false, "Product not found.");
                }

                if (product.IsArchived)
                {
                    return (false, "Product is already archived.");
                }

                // Archiver le produit
                product.Archive(userId, reason);
                product.UpdatedAt = DateTime.Now;

                // Log d'audit
                AuditLogger.LogAction(
                    context,
                    "Archive Product",
                    "Product",
                    productId,
                    $"Product '{product.ProductName}' archived. Reason: {reason ?? "Not specified"}",
                    userId
                );

                context.SaveChanges();

                return (true, $"Product '{product.ProductName}' archived successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error archiving product: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Restaure un produit archivé
        /// </summary>
        public static (bool success, string message) RestoreProduct(
            int productId,
            int userId,
            AppDbContext context)
        {
            try
            {
                var product = context.Products.Find(productId);

                if (product == null)
                {
                    return (false, "Product not found.");
                }

                if (!product.IsArchived)
                {
                    return (false, "Product is not archived.");
                }

                // Restaurer le produit
                product.Restore();
                product.UpdatedAt = DateTime.Now;

                // Log d'audit
                AuditLogger.LogAction(
                    context,
                    "Restore Product",
                    "Product",
                    productId,
                    $"Product '{product.ProductName}' restored from archive",
                    userId
                );

                context.SaveChanges();

                return (true, $"Product '{product.ProductName}' restored successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error restoring product: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Compte les produits archivés
        /// </summary>
        public static int GetArchivedProductsCount(AppDbContext context)
        {
            return context.Products.Count(p => p.IsArchived);
        }

        /// <summary>
        /// Récupère tous les produits archivés
        /// </summary>
        public static System.Collections.Generic.List<Product> GetArchivedProducts(AppDbContext context)
        {
            return context.Products
                .Where(p => p.IsArchived)
                .Include(p => p.ArchivedByUser)
                .OrderByDescending(p => p.ArchivedAt)
                .ToList();
        }

        /// <summary>
        /// Récupère tous les produits actifs (non archivés)
        /// </summary>
        public static System.Collections.Generic.List<Product> GetActiveProducts(AppDbContext context)
        {
            return context.Products
                .Where(p => !p.IsArchived)
                .OrderByDescending(p => p.Id)
                .ToList();
        }

        /// <summary>
        /// Vérifie si un produit peut être supprimé définitivement
        /// (Même archivé, peut avoir des dépendances)
        /// </summary>
        public static (bool canDelete, string reason) CanPermanentlyDelete(int productId, AppDbContext context)
        {
            bool usedInSales = context.SaleDetails.Any(sd => sd.ProductId == productId);
            bool usedInPurchases = context.PurchaseDetails.Any(pd => pd.ProductId == productId);
            bool hasStock = context.ProductWarehouses.Any(pw => pw.ProductId == productId);
            bool hasVariants = context.ProductVariants.Any(pv => pv.ProductId == productId);

            if (usedInSales || usedInPurchases)
            {
                return (false, "Cannot permanently delete: Product is used in sales or purchase transactions. Archive it instead.");
            }

            if (hasStock)
            {
                return (false, "Cannot permanently delete: Product has stock records. Remove stock first.");
            }

            if (hasVariants)
            {
                return (false, "Cannot permanently delete: Product has variants. Delete variants first.");
            }

            return (true, "Product can be permanently deleted.");
        }

        /// <summary>
        /// Suppression définitive (DANGER - utiliser avec précaution)
        /// </summary>
        public static (bool success, string message) PermanentlyDeleteProduct(
            int productId,
            int userId,
            AppDbContext context)
        {
            try
            {
                var canDelete = CanPermanentlyDelete(productId, context);

                if (!canDelete.canDelete)
                {
                    return (false, canDelete.reason);
                }

                var product = context.Products.Find(productId);

                if (product == null)
                {
                    return (false, "Product not found.");
                }

                // Log avant suppression définitive
                AuditLogger.LogDelete(context, productId, product, userId);

                // Supprimer définitivement
                context.Products.Remove(product);
                context.SaveChanges();

                return (true, $"Product '{product.ProductName}' permanently deleted.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error permanently deleting product: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Archive automatiquement les produits sans stock depuis X mois
        /// (Peut être appelé par un job périodique)
        /// </summary>
        public static int AutoArchiveInactiveProducts(int inactiveMonths, int userId, AppDbContext context)
        {
            var cutoffDate = DateTime.Now.AddMonths(-inactiveMonths);

            var inactiveProducts = context.Products
                .Where(p => !p.IsArchived
                         && p.UpdatedAt < cutoffDate
                         && !context.SaleDetails.Any(sd => sd.ProductId == p.Id && sd.CreatedAt >= cutoffDate)
                         && !context.ProductWarehouses.Any(pw => pw.ProductId == p.Id && pw.Qty > 0))
                .ToList();

            int archivedCount = 0;

            foreach (var product in inactiveProducts)
            {
                product.Archive(userId, $"Auto-archived: No activity for {inactiveMonths} months");
                archivedCount++;
            }

            if (archivedCount > 0)
            {
                context.SaveChanges();

                AuditLogger.LogAction(
                    context,
                    "Auto Archive",
                    "Product",
                    null,
                    $"Auto-archived {archivedCount} inactive products (no activity for {inactiveMonths} months)",
                    userId
                );
            }

            return archivedCount;
        }
    }
}
