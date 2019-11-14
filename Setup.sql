
-- Users Table
CREATE TABLE Users(
	[Id] UNIQUEIDENTIFIER DEFAULT NEWID(),
	[UserName] VARCHAR(50) NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
	[EmailAddress] VARCHAR(100) NOT NULL,
	[FirstName] VARCHAR(50) NOT NULL,
	[LastName] VARCHAR(50) NOT NULL,

	CONSTRAINT UsersPrimaryKey
	PRIMARY KEY(Id)
);

-- API KEY Table
CREATE TABLE ApiKeys(
	[Id] UNIQUEIDENTIFIER DEFAULT NEWID(),
	[Key] VARCHAR(100) NOT NULL UNIQUE,
	[Owner] VARCHAR(100) NOT NULL,
	[IssuedBy] VARCHAR(100) NOT NULL,
	[IssuedDateTime] DATETIME NOT NULL,
	[ValidFromDateTime] DATETIME NOT NULL,
	[ValidToDateTime] DATETIME NOT NULL

	CONSTRAINT ApiKeyPrimaryKey
	PRIMARY KEY(Id)
);

INSERT INTO ApiKeys
([Key], [Owner], [IssuedBy], [IssuedDateTime], [ValidFromDateTime], [ValidToDateTime])
VALUES('CzmFUSeyx-9c%VLkvFe^Tpu@!Wr]j#E]0+R@}u=@Y(30=s33>Fp~LmkuDm9*@', 'MyChat', 'Administrator', GETDATE(), GETDATE(), GETDATE() + 365);
