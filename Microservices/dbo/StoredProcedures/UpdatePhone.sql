CREATE PROCEDURE [dbo].[UpdatePhone]
	@PhoneId		INT,
	@CountryCode	VARCHAR(10), 
	@Number			VARCHAR(50),
	@Extension		VARCHAR(50) = NULL
AS
	SET NOCOUNT ON;
	
	UPDATE [Microservices].[dbo].[Phone]
	SET
		[CountryCode] = @CountryCode,
		[Number] = @Number,
		[Extension] = @Extension
	WHERE
		[Id] = @PhoneId;

RETURN 0
