
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RegistroDiscapacidadDel]
( @pId int,
  @pToken      UNIQUEIDENTIFIER)
AS
BEGIN

DECLARE @vUserId INT ,@vResult int, @vIp VARCHAR(40) , @vUserName varchar(50)

  SELECT @vIp = ip, 
           @vUserId = userid 
    FROM   applogindata 
    WHERE  userid = (SELECT userid 
                     FROM   apptoken 
                     WHERE  authtoken = @pToken) 


INSERT INTO HistoricoDiscapacidad ( afiliadoid
	,periodo
	,cuit
	,tipocomprobanteid
	,tipoemisionid
	,fechaemision
	,caecai
	,puntoVenta
	,numeroComprobante
	,importe
	,cantidad
	,codigopractica
	,provinciaId
	,esdependiente
	,procesado
	,fechaAlta
	,fechaUltimaModificacion
	,codigoErrorSSS
	,vencimientoCertDisc
	,codigoCertDisc
	,accionRealizada 
	,userId
	,Ip         )
    SELECT afiliadoid
	,periodo
	,cuit
	,tipocomprobanteid
	,tipoemisionid
	,fechaemision
	,caecai
	,puntoVenta
	,numeroComprobante
	,importe
	,cantidad
	,codigopractica
	,provinciaId
	,esdependiente
	,procesado
	,fechaAlta
	,fechaUltimaModificacion
	,codigoErrorSSS
	,vencimientoCertDisc
	,codigoCertDisc                 
	,accionRealizada
	,userId
	,ip
	FROM RegistroDiscapacidad
	WHERE  id = @pId

	UPDATE RegistroDiscapacidad 
	SET Activo = 0,
	    accionRealizada = 'Borrar',
	    userId = @vUserId ,
		ip = @vIp,
		fechaUltimaModificacion = getdate()
	WHERE  id = @pId and isnull(procesado,0) <> 6

END



