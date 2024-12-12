CREATE TABLE [dbo].[CarteBancaire] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [NumCarte]         NCHAR (4)    NOT NULL,
    [DateExpiration]   DATE          NOT NULL,
    [NomTitulaire]     NVARCHAR (30) NOT NULL,
    [CompteBancaireId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarteBancaire_CompteBancaire_CompteBancaireId] FOREIGN KEY ([CompteBancaireId]) REFERENCES [dbo].[CompteBancaire] ([Id]) ON DELETE CASCADE
);

