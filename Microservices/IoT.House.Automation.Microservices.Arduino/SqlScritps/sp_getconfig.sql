CREATE PROCEDURE [Arduino].[sp_getconfig]
(
	@KEY VARCHAR(255)
)

AS

	BEGIN
		SELECT 
		cf.[Value]
		FROM [Arduino].Config cf
		WHERE cf.[Key] = @KEY
	END