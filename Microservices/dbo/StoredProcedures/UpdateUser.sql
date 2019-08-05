CREATE PROCEDURE [dbo].[UpdateUser]
	@UserId				INT,
	@FirstName			VARCHAR(50),
	@LastName			VARCHAR(50),
	@Username			VARCHAR(50),
	@HashedPassword		VARCHAR(MAX),
	@Salt				VARCHAR(50)
AS
	SET NOCOUNT ON;

	UPDATE [Microservices].[dbo].[User]
	SET
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[Username] = @Username,
		[HashedPassword] = @HashedPassword,
		[Salt] = @Salt
	WHERE
		[Id] = @UserId;

RETURN 0
