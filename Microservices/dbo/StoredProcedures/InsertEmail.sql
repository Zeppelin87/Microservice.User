CREATE PROCEDURE [dbo].[InsertEmail]
	@Address	VARCHAR(100)
AS
	SET NOCOUNT ON;
	
	INSERT INTO [Microservices].[dbo].[Email]
	(
		[Address]
	)
	VALUES
	(
		@Address
	);

	DECLARE @Id AS INT = SCOPE_IDENTITY();
	SELECT @Id;

RETURN 0
