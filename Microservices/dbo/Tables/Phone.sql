CREATE TABLE [dbo].[Phone]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [DialingCode] VARCHAR(10) NOT NULL, 
    [Number] VARCHAR(50) NOT NULL, 
    [Extension] VARCHAR(50) NULL
)
