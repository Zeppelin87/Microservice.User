CREATE PROCEDURE [dbo].[InsertPhone]
	@CountryCode	VARCHAR(10),
	@Number			VARCHAR(50),
	@Extension		VARCHAR(50)
AS
	SET NOCOUNT ON;

	INSERT INTO [Microservices].[dbo].[Phone]
	(
		[CountryCode],
		[Number],
		[Extension]
	)
	VALUES
	(
		@CountryCode,
		@Number,
		@Extension
	);

	DECLARE @Id AS INT = SCOPE_IDENTITY();
	SELECT @Id;

RETURN 0
