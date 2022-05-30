CREATE TABLE [dbo].[FixerRates] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [CurrencyId] INT             NOT NULL,
    [ToUSDRate]  DECIMAL (18, 6) NOT NULL,
    CONSTRAINT [PK_FixerRates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixerRates_Currencies] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id])
);

