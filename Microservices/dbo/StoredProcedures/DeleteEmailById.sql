CREATE PROCEDURE [dbo].[DeleteEmailById]
	@EmailId	INT
AS
	SET NOCOUNT ON;

	DELETE FROM [Microservices].[dbo].[User_Email]
	WHERE [EmailId] = @EmailId;

	DELETE FROM [Microservices].[dbo].[Email]
	WHERE [Id] = @EmailId;

RETURN 0
