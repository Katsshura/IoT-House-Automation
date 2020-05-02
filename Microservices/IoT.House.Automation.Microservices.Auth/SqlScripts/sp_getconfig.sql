CREATE PROCEDURE [Auth].[sp_getconfig]
(
	@KEY VARCHAR(255)
)

AS

	BEGIN
		SELECT 
		cf.[Value]
		FROM Auth.Config cf
		WHERE cf.[Key] = @KEY
	END