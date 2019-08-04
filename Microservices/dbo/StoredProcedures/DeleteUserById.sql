CREATE PROCEDURE [dbo].[DeleteUserById]
	@UserId		INT
AS
	SET NOCOUNT ON;

	DELETE FROM [Microservices].[dbo].[User]
	WHERE [Id] = @UserId;

RETURN 0
