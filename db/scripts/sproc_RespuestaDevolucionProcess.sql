/****** Object:  StoredProcedure [dbo].[RespuestaProcess]    Script Date: 04/04/2020 17:42:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RespuestaDevolucionProcess]
( 
 @pClaveRendicion nvarchar(255)
,@pProcesado int
,@pCodigoErrorSSS int = 0)
AS
BEGIN

update  RegistroDevolucion 
set PROCESADO = @pProcesado,
	CODIGO_ERROR_SSS = -1* @pCodigoErrorSSS
where CLAVE_RENDICION = @pClaveRendicion

END