-- Script de création de la table CashDiscrepancy
-- Pour traçabilité des écarts de caisse

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CashDiscrepancy')
BEGIN
    CREATE TABLE [dbo].[CashDiscrepancy](
        [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [RegisterRecordId] INT NOT NULL,
        [ExpectedAmount] DECIMAL(18, 2) NOT NULL,
        [ActualAmount] DECIMAL(18, 2) NOT NULL,
        [Discrepancy] DECIMAL(18, 2) NOT NULL,
        [Reason] NVARCHAR(MAX) NULL,
        [UserId] INT NOT NULL,
        [ReportedAt] DATETIME NOT NULL,
        [IsResolved] BIT NOT NULL DEFAULT 0,
        [ResolvedById] INT NULL,
        [ResolvedAt] DATETIME NULL,
        [ManagerComment] NVARCHAR(MAX) NULL,
        [DiscrepancyType] NVARCHAR(50) NULL,
        [Severity] NVARCHAR(20) NULL,

        CONSTRAINT [FK_CashDiscrepancy_RegisterRecord]
            FOREIGN KEY ([RegisterRecordId])
            REFERENCES [dbo].[RegisterRecord]([Id])
            ON DELETE CASCADE,

        CONSTRAINT [FK_CashDiscrepancy_User]
            FOREIGN KEY ([UserId])
            REFERENCES [dbo].[User]([Id]),

        CONSTRAINT [FK_CashDiscrepancy_ResolvedBy]
            FOREIGN KEY ([ResolvedById])
            REFERENCES [dbo].[User]([Id])
    );

    -- Index pour performance
    CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_RegisterRecordId]
        ON [dbo].[CashDiscrepancy]([RegisterRecordId]);

    CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_UserId]
        ON [dbo].[CashDiscrepancy]([UserId]);

    CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_ReportedAt]
        ON [dbo].[CashDiscrepancy]([ReportedAt] DESC);

    CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_IsResolved]
        ON [dbo].[CashDiscrepancy]([IsResolved]);

    CREATE NONCLUSTERED INDEX [IX_CashDiscrepancy_Severity]
        ON [dbo].[CashDiscrepancy]([Severity]);

    PRINT 'Table CashDiscrepancy créée avec succès';
END
ELSE
BEGIN
    PRINT 'Table CashDiscrepancy existe déjà';
END
GO
