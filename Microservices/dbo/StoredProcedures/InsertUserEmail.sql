CREATE PROCEDURE [dbo].[InsertUserEmail]
	@UserId		INT,
	@EmailId	INT
AS
	SET NOCOUNT ON;

	INSERT INTO [Microservices].[dbo].[User_Email]
	(
		[UserId],
		[EmailId]
	)
	VALUES
	(
		@UserId,
		@EmailId
	);

RETURN 0
