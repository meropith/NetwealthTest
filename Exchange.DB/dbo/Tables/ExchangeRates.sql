CREATE TABLE [dbo].[ExchangeRates] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [CurrencyId] INT             NOT NULL,
    [ToUSDRate]  DECIMAL (18, 6) NOT NULL,
    CONSTRAINT [PK_OandaRates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OandaRates_Currencies] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([Id])
);

