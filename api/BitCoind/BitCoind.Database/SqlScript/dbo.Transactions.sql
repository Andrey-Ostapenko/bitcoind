CREATE TABLE [dbo].[Transactions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newsequentialid(), 
    [IdempotencyKey] UNIQUEIDENTIFIER NOT NULL UNIQUE,
	[Address] NVARCHAR(120) NOT NULL, 
    [Amount] NUMERIC(18, 5) NOT NULL,
	[Data] DATETIME NOT NULL, 
    [Confirmation] TINYINT DEFAULT 0 NOT NULL,
	[IsNew] BIT DEFAULT 1 NOT NULL,
	[WalletId] UNIQUEIDENTIFIER NOT NULL,
	[Type] NVARCHAR(50) NOT NULL
)

ALTER TABLE [dbo].[Transactions]
  ADD CONSTRAINT FK_Transactions_Wallet FOREIGN KEY (WalletId)     
      REFERENCES [dbo].[Wallets] (Id)
      ON DELETE CASCADE    
      ON UPDATE CASCADE
  ;
GO