CREATE FUNCTION [dbo].[GetCueFlag](@pCodigopractica int)
RETURNS BIT
AS
BEGIN

	DECLARE @retValue BIT
	SELECT @retValue = cueFlag 
	  FROM NomencladorPracticas 
	 WHERE Codigo = @pCodigopractica
	
	RETURN @retValue
END