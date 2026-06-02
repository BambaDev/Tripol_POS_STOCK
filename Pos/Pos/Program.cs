using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Pos.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pos.Models;
using DevExpress.XtraScheduler.Internal.Implementations;
using System.IO;
using System.Reflection;
using static DevExpress.Office.Drawing.LazyGroupBrush;
using Pos.Forms.Alert;
using System.Net.Mail;
using System.Threading;
using DevExpress.CodeParser;
using System.Drawing.Imaging;
using System.Drawing;

namespace Pos
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles and set text rendering before any forms or UI are created
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Seed the database
            using (AppDbContext db = new())
            {
                // Check if the database is already seeded
                db.Database.EnsureCreated();

                // Créer la table UserSession si elle n'existe pas
                try
                {
                    var tableExists = db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSession]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[UserSession](
                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                [Token] [nvarchar](128) NOT NULL,
                                [UserId] [int] NOT NULL,
                                [CreatedAt] [datetime] NOT NULL,
                                [ExpiresAt] [datetime] NOT NULL,
                                [LastActivityAt] [datetime] NULL,
                                [IpAddress] [nvarchar](45) NULL,
                                [UserAgent] [nvarchar](500) NULL,
                                [IsRevoked] [bit] NOT NULL,
                                [RevokedAt] [datetime] NULL,
                                CONSTRAINT [PK_UserSession] PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                            CREATE UNIQUE NONCLUSTERED INDEX [IX_UserSession_Token] ON [dbo].[UserSession]([Token] ASC);
                            CREATE NONCLUSTERED INDEX [IX_UserSession_UserId] ON [dbo].[UserSession]([UserId] ASC);
                            CREATE NONCLUSTERED INDEX [IX_UserSession_ExpiresAt] ON [dbo].[UserSession]([ExpiresAt] ASC);
                            ALTER TABLE [dbo].[UserSession] WITH CHECK ADD CONSTRAINT [FK_UserSession_User_UserId]
                            FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;
                        END
                    ");
                }
                catch (Exception ex)
                {
                    // Table déjà créée ou erreur SQL
                    Console.WriteLine($"UserSession table creation: {ex.Message}");
                }

                // Créer la table LoginAttempt si elle n'existe pas
                try
                {
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoginAttempt]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[LoginAttempt](
                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                [Username] [nvarchar](100) NOT NULL,
                                [AttemptTime] [datetime] NOT NULL,
                                [IsSuccessful] [bit] NOT NULL,
                                [IpAddress] [nvarchar](45) NULL,
                                [UserAgent] [nvarchar](500) NULL,
                                [FailureReason] [nvarchar](500) NULL,
                                CONSTRAINT [PK_LoginAttempt] PRIMARY KEY CLUSTERED ([Id] ASC)
                            );
                            CREATE NONCLUSTERED INDEX [IX_LoginAttempt_Username] ON [dbo].[LoginAttempt]([Username] ASC);
                            CREATE NONCLUSTERED INDEX [IX_LoginAttempt_AttemptTime] ON [dbo].[LoginAttempt]([AttemptTime] ASC);
                        END
                    ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"LoginAttempt table creation: {ex.Message}");
                }

                // Créer la table CashDiscrepancy si elle n'existe pas
                try
                {
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CashDiscrepancy]') AND type in (N'U'))
                        BEGIN
                            CREATE TABLE [dbo].[CashDiscrepancy](
                                [Id] [int] IDENTITY(1,1) NOT NULL,
                                [RegisterRecordId] [int] NOT NULL,
                                [ExpectedAmount] [decimal](18, 2) NOT NULL,
                                [ActualAmount] [decimal](18, 2) NOT NULL,
                                [Discrepancy] [decimal](18, 2) NOT NULL,
                                [Reason] [nvarchar](max) NULL,
                                [UserId] [int] NOT NULL,
                                [ReportedAt] [datetime] NOT NULL,
                                [IsResolved] [bit] NOT NULL DEFAULT 0,
                                [ResolvedById] [int] NULL,
                                [ResolvedAt] [datetime] NULL,
                                [ManagerComment] [nvarchar](max) NULL,
                                [DiscrepancyType] [nvarchar](50) NULL,
                                [Severity] [nvarchar](20) NULL,
                                CONSTRAINT [PK_CashDiscrepancy] PRIMARY KEY CLUSTERED ([Id] ASC),
                                CONSTRAINT [FK_CashDiscrepancy_RegisterRecord] FOREIGN KEY ([RegisterRecordId]) REFERENCES [dbo].[RegisterRecord]([Id]),
                                CONSTRAINT [FK_CashDiscrepancy_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([Id]),
                                CONSTRAINT [FK_CashDiscrepancy_ResolvedBy] FOREIGN KEY ([ResolvedById]) REFERENCES [dbo].[User]([Id])
                            );
                            CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_RegisterRecordId] ON [dbo].[CashDiscrepancy]([RegisterRecordId] ASC);
                            CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_UserId] ON [dbo].[CashDiscrepancy]([UserId] ASC);
                            CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_ReportedAt] ON [dbo].[CashDiscrepancy]([ReportedAt] DESC);
                        END
                    ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CashDiscrepancy table creation: {ex.Message}");
                }

                // Ajouter colonnes d'archivage à Product
                try
                {
                    // Vérifier et ajouter IsArchived
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'IsArchived')
                        BEGIN
                            ALTER TABLE [dbo].[Product] ADD [IsArchived] BIT NOT NULL DEFAULT 0;
                            PRINT 'Colonne IsArchived ajoutée';
                        END
                    ");

                    // Vérifier et ajouter ArchivedAt
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchivedAt')
                        BEGIN
                            ALTER TABLE [dbo].[Product] ADD [ArchivedAt] DATETIME NULL;
                            PRINT 'Colonne ArchivedAt ajoutée';
                        END
                    ");

                    // Vérifier et ajouter ArchivedBy
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchivedBy')
                        BEGIN
                            ALTER TABLE [dbo].[Product] ADD [ArchivedBy] INT NULL;
                            PRINT 'Colonne ArchivedBy ajoutée';
                        END
                    ");

                    // Vérifier et ajouter ArchiveReason
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchiveReason')
                        BEGIN
                            ALTER TABLE [dbo].[Product] ADD [ArchiveReason] NVARCHAR(500) NULL;
                            PRINT 'Colonne ArchiveReason ajoutée';
                        END
                    ");

                    // Créer index pour performance
                    db.Database.ExecuteSqlRaw(@"
                        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'IX_Product_IsArchived')
                        BEGIN
                            CREATE NONCLUSTERED INDEX [IX_Product_IsArchived] ON [dbo].[Product]([IsArchived]);
                            PRINT 'Index IX_Product_IsArchived créé';
                        END
                    ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Product archive fields creation: {ex.Message}");
                }

                DatabaseSeeder.Seed(db);
                DatabaseSeeder.SeedProductWarehouse();
                db.SaveChanges();
            }

            // Initialize background tasks
            InitializeBackgroundTasks();

            // Set language preferences
            var setting = Function.Helper.getSetting();
            Properties.Settings.Default.Lang = setting.Lang;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Lang);

            // Check if user session exists
            if (IsUserLoggedIn())
            {
                Application.Run(new Forms.MainFrm());
            }
            else
            {
                if (Function.Helper.hasUser())
                {
                    Application.Run(new Forms.Auth.Login());
                }
                else
                {
                    Application.Run(new Forms.Alert.InitAccount());
                }
            }
        }

        public static bool IsUserLoggedIn()
        {
            // Valider la session sécurisée
            return SessionManager.ValidateSession();
        }

        /// <summary>
        /// Initializes background tasks such as preloading data
        /// </summary>
        private static async void InitializeBackgroundTasks()
        {
            try
            {
                // Example background task: Load configuration asynchronously
                await Task.Run(() => LoadConfiguration());

                // You can add more tasks here
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing background tasks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Simulates loading configuration settings
        /// </summary>
        private static void LoadConfiguration()
        {
            // Simulated delay
            System.Threading.Thread.Sleep(2000);  // Simulates loading time

            // Load configuration logic here
            Console.WriteLine("Configuration loaded.");
        }
    }
}
