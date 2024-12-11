drop table CarteBancaire;

CREATE TABLE [dbo].[CarteBancaire]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [NumCarte] NCHAR(19) NOT NULL, 
    [DateExpiration] DATE NOT NULL, 
    [NomTitulaire] NVARCHAR(30) NOT NULL,
	[CompteBancaireId] INT NOT NULL, 
    CONSTRAINT [FK_CarteBancaire_CompteBancaire_CompteBancaireId] FOREIGN KEY ([CompteBancaireId]) REFERENCES [dbo].[CompteBancaire] ([Id]) ON DELETE CASCADE
);
