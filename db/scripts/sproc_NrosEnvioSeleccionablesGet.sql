
/****** Object:  StoredProcedure [dbo].[[NrosEnvioSeleccionablesGet]]    Script Date: 03/11/2019 12:20:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[NrosEnvioSeleccionablesGet]
AS
BEGIN 
	SELECT DISTINCT([NumeroEnvioAfip]) AS Codigo,
	CAST([NumeroEnvioAfip] AS nvarchar) AS Nombre
	FROM [dbo].[Rendicion]
	GROUP BY [NumeroEnvioAfip]
	ORDER BY [NumeroEnvioAfip] DESC
END

