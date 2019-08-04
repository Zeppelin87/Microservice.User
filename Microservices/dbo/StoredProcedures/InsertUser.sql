CREATE PROCEDURE [dbo].[InsertUser]
	@FirstName			VARCHAR(50),
	@LastName			VARCHAR(50),
	@Username			VARCHAR(50),
	@HashedPassword		VARCHAR(MAX),
	@Salt				VARCHAR(50)

AS
	SET NOCOUNT ON;

	INSERT INTO [Microservices].[dbo].[User]
	(
		[FirstName],
		[LastName], 
		[Username], 
		[HashedPassword], 
		[Salt]
	)
	VALUES
	(			
		@FirstName,
		@LastName,
		@Username,
		@HashedPassword,
		@Salt			
	);

	DECLARE @UserId AS INT = SCOPE_IDENTITY();
	SELECT @UserId;

RETURN 0
