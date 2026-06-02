-- Script de création de la table UserSession
-- À exécuter dans SQL Server Management Studio ou Visual Studio

USE [Pos]
GO

-- Vérifier si la table existe déjà
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSession]') AND type in (N'U'))
BEGIN
    -- Créer la table UserSession
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
    )

    -- Créer les index
    CREATE UNIQUE NONCLUSTERED INDEX [IX_UserSession_Token] ON [dbo].[UserSession]
    (
        [Token] ASC
    )

    CREATE NONCLUSTERED INDEX [IX_UserSession_UserId] ON [dbo].[UserSession]
    (
        [UserId] ASC
    )

    CREATE NONCLUSTERED INDEX [IX_UserSession_ExpiresAt] ON [dbo].[UserSession]
    (
        [ExpiresAt] ASC
    )

    -- Ajouter la contrainte de clé étrangère
    ALTER TABLE [dbo].[UserSession] WITH CHECK ADD CONSTRAINT [FK_UserSession_User_UserId]
    FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE

    ALTER TABLE [dbo].[UserSession] CHECK CONSTRAINT [FK_UserSession_User_UserId]

    PRINT 'Table UserSession créée avec succès !'
END
ELSE
BEGIN
    PRINT 'La table UserSession existe déjà.'
END
GO

-- Ajouter un enregistrement dans __EFMigrationsHistory
IF NOT EXISTS (SELECT * FROM [dbo].[__EFMigrationsHistory] WHERE [MigrationId] = N'20260602000000_AddUserSessionTable')
BEGIN
    INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260602000000_AddUserSessionTable', N'9.0.0')

    PRINT 'Migration enregistrée dans __EFMigrationsHistory'
END
GO
