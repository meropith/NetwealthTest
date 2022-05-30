CREATE TABLE [dbo].[Currencies] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [ISOCode]     NVARCHAR (5)  NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [IsSupported] BIT           CONSTRAINT [DF_Currencies_IsSupported] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

