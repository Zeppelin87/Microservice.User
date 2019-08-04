CREATE PROCEDURE [dbo].[InsertUserPhone]
	@UserId		INT,
	@PhoneId	INT
AS
	SET NOCOUNT ON;
	
	INSERT INTO [Microservices].[dbo].[User_Phone]
	(
		[UserId],
		[PhoneId]
	)
	VALUES
	(
		@UserId,
		@PhoneId
	);

RETURN 0
