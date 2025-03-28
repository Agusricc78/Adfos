
/****** Object:  StoredProcedure [dbo].[RespuestaProcess]    Script Date: 04/08/2019 15:27:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RespuestaProcess]
( 
@pPeriodo	datetime
,@pCuit	varchar(30)
,@pCaecai	varchar(14)
,@pPuntoVenta	int
,@pNumeroComprobante	int
,@pCodigopractica	int
,@pProcesado int
,@pCodigoErrorSSS int = 0)
AS
BEGIN


update  RegistroDiscapacidad 
set procesado = @pProcesado,
    fechaUltimaModificacion = getdate(),
	codigoErrorSSS = -1* @pCodigoErrorSSS
where periodo =cast( @pPeriodo as date)
and Replicate('0', ( 11 - Len(cuit))) + cuit = Replicate('0', ( 11 - Len(@pCuit))) + @pCuit
and caecai = @pCaecai	
and puntoVenta = @pPuntoVenta	
and numeroComprobante = @pNumeroComprobante	
and codigopractica = @pCodigopractica
AND procesado NOT IN (6, 7, 8, 9, 10, 99)

END