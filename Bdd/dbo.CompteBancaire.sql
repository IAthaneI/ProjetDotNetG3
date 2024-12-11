CREATE TABLE [dbo].[CompteBancaire] (
    [Id]            INT        IDENTITY (1, 1) NOT NULL,
    [NumCompte]     NCHAR (10) NOT NULL,
    [DateOuverture] DATE       NOT NULL,
    [Solde]         MONEY      DEFAULT ((1000)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

