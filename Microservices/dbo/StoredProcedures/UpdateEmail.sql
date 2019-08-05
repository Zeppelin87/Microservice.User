CREATE PROCEDURE [dbo].[UpdateEmail]
	@EmailId	INT,
	@Address	VARCHAR(100)
AS
	SET NOCOUNT ON;

	UPDATE [Microservices].[dbo].[Email]
	SET
		[Address] = @Address
	WHERE
		[Id] = @EmailId;

RETURN 0
