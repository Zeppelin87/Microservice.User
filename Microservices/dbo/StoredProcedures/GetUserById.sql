CREATE PROCEDURE [dbo].[GetUserById]
	@UserId		INT
AS
	SET NOCOUNT ON;
	
	SELECT
		[Id],
		[FirstName],
		[LastName],
		[Username],
		[HashedPassword],
		[Salt]
	FROM [Microservices].[dbo].[User] WITH (NOLOCK)
	WHERE
		[Id] = @UserId

	SELECT 
		E.[Id],
		E.[Address]
	FROM [Microservices].[dbo].[User_Email] AS UE WITH (NOLOCK)
	INNER JOIN [Microservices].[dbo].[Email] AS E WITH (NOLOCK)
	ON UE.[EmailId] = E.[Id]
	WHERE
		[UserId] = @UserId;

	SELECT
		P.[Id],
		P.[CountryCode],
		P.[Number],
		P.[Extension]
	FROM [Microservices].[dbo].[User_Phone] AS UP WITH (NOLOCK)
	INNER JOIN [Microservices].[dbo].[Phone] AS P WITH (NOLOCK)
	ON UP.[PhoneId] = P.[Id]
	WHERE
		[UserId] = @UserId;

RETURN 0
