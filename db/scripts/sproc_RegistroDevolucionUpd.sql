/****** Object:  StoredProcedure [dbo].[RegistroDevolucionUpd]    Script Date: 10/03/2020 14:50:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistroDevolucionUpd]
( 
 @pFechaTransferenciaI datetime
,@pImporteAplicadoSSS decimal(10,2)
,@pCuitDelCbu varchar(30)
,@pNroEnvioAfip int
,@pClaveRendicion nvarchar(255)
,@pProcesado int
)
AS
BEGIN

UPDATE [dbo].[RegistroDevolucion]
   SET FECHA_TRANSFERENCIA_I = @pFechaTransferenciaI,
		PROCESADO = isnull(@pProcesado, PROCESADO)
 WHERE (FECHA_TRANSFERENCIA_I IS NULL
		AND [IMPORTE_APLICADO_SSS] <= ABS(@pImporteAplicadoSSS)
		AND [CUIT_DEL_CBU] = @pCuitDelCbu
		AND [NRO_ENVIO_AFIP] = @pNroEnvioAfip 
		AND @pClaveRendicion IS NULL) 
 OR ([CLAVE_RENDICION] = @pClaveRendicion)

END
