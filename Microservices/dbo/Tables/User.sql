CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Username] VARCHAR(50) NOT NULL, 
    [HashedPassword] VARCHAR(50) NOT NULL, 
    [Salt] VARCHAR(10) NOT NULL
)
