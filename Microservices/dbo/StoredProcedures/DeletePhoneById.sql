CREATE PROCEDURE [dbo].[DeletePhoneById]
	@PhoneId	INT
AS
	SET NOCOUNT ON;

	DELETE FROM [Microservices].[dbo].[User_Phone]
	WHERE [PhoneId] = @PhoneId;

	DELETE FROM [Microservices].[dbo].[Phone]
	WHERE [Id] = @PhoneId

RETURN 0
