
/****** Object:  StoredProcedure [dbo].[RespuestaClose]    Script Date: 04/08/2019 15:39:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[RespuestaClose]
( 
@pPeriodo	datetime
,@pCuil	varchar(30)
,@pCodigopractica	int
,@pProcesado int)
AS
BEGIN


update  RegistroDiscapacidad 
set procesado = 6,
    fechaUltimaModificacion = getdate()
where periodo =cast( @pPeriodo as date)
and  codigopractica = @pCodigopractica
AND procesado IN (1, 2, 3, 5, 6)
and afiliadoid = (select top 1 afi_codigo from Ad_afiliados where Cuil = @pCuil and Fecha_Baja is null and ISNULL(baja,'N')='N')


END

