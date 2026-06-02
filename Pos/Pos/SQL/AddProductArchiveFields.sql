-- Script pour ajouter les champs d'archivage au produit
-- Soft Delete au lieu de Hard Delete

USE [Pos]
GO

-- Ajouter IsArchived (soft delete)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'IsArchived')
BEGIN
    ALTER TABLE [dbo].[Product]
    ADD [IsArchived] BIT NOT NULL DEFAULT 0;

    PRINT 'Colonne IsArchived ajoutée à Product';
END
ELSE
BEGIN
    PRINT 'Colonne IsArchived existe déjà';
END
GO

-- Ajouter ArchivedAt
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchivedAt')
BEGIN
    ALTER TABLE [dbo].[Product]
    ADD [ArchivedAt] DATETIME NULL;

    PRINT 'Colonne ArchivedAt ajoutée à Product';
END
ELSE
BEGIN
    PRINT 'Colonne ArchivedAt existe déjà';
END
GO

-- Ajouter ArchivedBy
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchivedBy')
BEGIN
    ALTER TABLE [dbo].[Product]
    ADD [ArchivedBy] INT NULL;

    -- Foreign key vers User
    ALTER TABLE [dbo].[Product]
    ADD CONSTRAINT [FK_Product_ArchivedBy_User]
        FOREIGN KEY ([ArchivedBy])
        REFERENCES [dbo].[User]([Id]);

    PRINT 'Colonne ArchivedBy ajoutée à Product avec FK vers User';
END
ELSE
BEGIN
    PRINT 'Colonne ArchivedBy existe déjà';
END
GO

-- Ajouter ArchiveReason
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'ArchiveReason')
BEGIN
    ALTER TABLE [dbo].[Product]
    ADD [ArchiveReason] NVARCHAR(500) NULL;

    PRINT 'Colonne ArchiveReason ajoutée à Product';
END
ELSE
BEGIN
    PRINT 'Colonne ArchiveReason existe déjà';
END
GO

-- Créer index pour performance (filtrer produits actifs)
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND name = 'IX_Product_IsArchived')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Product_IsArchived]
    ON [dbo].[Product]([IsArchived])
    INCLUDE ([Id], [Name], [SellingPrice]);

    PRINT 'Index IX_Product_IsArchived créé';
END
ELSE
BEGIN
    PRINT 'Index IX_Product_IsArchived existe déjà';
END
GO

PRINT 'Script terminé avec succès';
GO
