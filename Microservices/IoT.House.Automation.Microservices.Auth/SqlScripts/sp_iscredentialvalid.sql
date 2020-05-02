CREATE PROCEDURE [Auth].[sp_iscredentialvalid]
(
	@Username varchar(255),
	@Password varchar(255)
)

AS
	BEGIN
		SELECT 1
		FROM [Auth].[User] us
		WHERE us.[Username] = @Username
		and us.[Password] = @Password
	END