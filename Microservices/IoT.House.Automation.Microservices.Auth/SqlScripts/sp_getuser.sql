CREATE PROCEDURE [Auth].[sp_getuser]
(
	@Username varchar(255)
)

AS
	BEGIN
		SELECT
		us.Id,
		us.Username,
		us.Email
		FROM [Auth].[User] us
		WHERE us.[Username] = @Username
	END