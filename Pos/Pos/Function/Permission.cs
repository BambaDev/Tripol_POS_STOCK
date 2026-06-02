using Pos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Pos.Function
{
    internal class Permission
    {
        /// <summary>
        /// Vérifie si l'utilisateur actuel a la permission demandée
        /// SÉCURISÉ: Vérifie toujours en base de données, jamais via Settings
        /// </summary>
        public static bool HasPermission(string permissionName)
        {
            int userId = Properties.Settings.Default.userId;

            if (userId == 0)
                return false;

            using (var context = new AppDbContext())
            {
                // Récupérer l'utilisateur avec son rôle depuis la DB (source de vérité)
                var user = context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null || user.Role == null)
                {
                    Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                    accessDenied.ShowDialog();
                    return false;
                }

                // Vérifier si l'utilisateur est Admin (depuis la DB, pas Settings)
                if (user.Role.Name == "Admin")
                    return true;

                // Vérifier la permission spécifique
                bool hasPermission = context.RoleHasPermissions
                    .Any(rhp => rhp.RoleId == user.Role.Id
                             && rhp.Permission.Name == permissionName);

                if (!hasPermission)
                {
                    Forms.Alert.AccessDenied accessDenied = new Forms.Alert.AccessDenied();
                    accessDenied.ShowDialog();
                }

                return hasPermission;
            }
        }

        /// <summary>
        /// Vérifie la permission sans afficher de dialogue (version silencieuse)
        /// SÉCURISÉ: Vérifie toujours en base de données
        /// </summary>
        public static bool HasPermissionWithoutAlert(string permissionName)
        {
            int userId = Properties.Settings.Default.userId;

            if (userId == 0)
                return false;

            using (var context = new AppDbContext())
            {
                // Récupérer l'utilisateur avec son rôle depuis la DB
                var user = context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null || user.Role == null)
                    return false;

                // Vérifier si Admin
                if (user.Role.Name == "Admin")
                    return true;

                // Vérifier la permission spécifique
                return context.RoleHasPermissions
                    .Any(rhp => rhp.RoleId == user.Role.Id
                             && rhp.Permission.Name == permissionName);
            }
        }

        /// <summary>
        /// Vérifie si l'utilisateur actuel est administrateur
        /// SÉCURISÉ: Vérifie en base de données
        /// </summary>
        public static bool IsAdmin()
        {
            int userId = Properties.Settings.Default.userId;

            if (userId == 0)
                return false;

            using (var context = new AppDbContext())
            {
                var user = context.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Id == userId);

                return user != null && user.Role != null && user.Role.Name == "Admin";
            }
        }
    }
}
