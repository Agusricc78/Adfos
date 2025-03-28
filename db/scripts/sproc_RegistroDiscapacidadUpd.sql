
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RegistroDiscapacidadUpd]
( 
@pAfiliadoid	int
,@pVencimientoCertDisc DATETIME
,@pCodigoCertDisc      VARCHAR(40) = ''
,@pCertificadoPermanente       BIT
,@pPeriodo	datetime
,@pCuit	varchar(30)
,@pTipocomprobanteid	int
,@pTipoemisionid	int
,@pFechaemision	datetime
,@pCaecai	varchar(14)
,@pPuntoVenta	int
,@pNumeroComprobante int
,@pImporte	decimal(8,2)
,@pCantidad	int
,@pCodigopractica	int
,@pProvinciaId	int
,@pEsdependiente	bit
,@pProcesado int
,@pId int
,@pRazonSocial varchar(80)
,@pCbu varchar(30)
,@pCue varchar(30)
,@pToken      UNIQUEIDENTIFIER)
AS
BEGIN

DECLARE @vUserId INT ,@vResult int, @vIp VARCHAR(40) , @vUserName varchar(50)

  SELECT @vIp = ip, 
           @vUserId = userid 
    FROM   applogindata 
    WHERE  userid = (SELECT userid 
                     FROM   apptoken 
                     WHERE  authtoken = @pToken) 

IF EXISTS(select 1 from Prestadores where Cuit= @pCuit or (isNull(@pCuit,'')='' and Cue=@pCue))
BEGIN
    INSERT INTO HistoricoPrestadores ( Cuit
	                                  ,Cue
                                      ,RazonSocial
                                      ,CBU
                                      ,Banco
                                      ,Sucursal
                                      ,TipoCuenta
                                      ,Direccion
                                      ,FechaAlta
                                      ,FechaUltimaModificacion
                                      ,accionRealizada
                                      ,userId
                                      ,ip) 
	SELECT  Cuit
		   ,Cue
		   ,RazonSocial
		   ,CBU
		   ,Banco
		   ,Sucursal
		   ,TipoCuenta
		   ,Direccion
		   ,FechaAlta
		   ,FechaUltimaModificacion
		   ,accionRealizada
		   ,userId
		   ,ip
	FROM Prestadores
	WHERE Cuit = @pCuit or (isNull(@pCuit,'')='' and Cue=@pCue)

	UPDATE Prestadores SET cue = isnull(@pCue,cue)
						 , razonSocial = isnull(@pRazonSocial,razonsocial)
						 , cbu= isnull(@pCbu,cbu)
						 , fechaUltimaModificacion=GETDATE()
						 , accionRealizada = 'Actualizar'
						 , userId = @vUserId
						 , ip = @vIp
	WHERE Cuit = @pCuit or (isNull(@pCuit,'')='' and Cue=@pCue)
END
ELSE
BEGIN
	insert into prestadores select rtrim(ltrim(@pCuit)),rtrim(ltrim(@pCue)),@pRazonSocial,@pCbu,null,null,null,null,GETDATE(),GETDATE(),'Alta',@vUserId,@vIp
END

-- Tengo que actualizar los valores de Codigo Certificado y Vencimiento Certificado.
IF NULLIF(@pCodigoCertDisc, '') IS NOT NULL 
    OR NULLIF(@pVencimientoCertDisc, '') IS NOT NULL 
  BEGIN 
      IF EXISTS(SELECT 1 
                FROM   datosafiliado 
                WHERE  afiliadoid = @pAfiliadoid) 
        BEGIN 
			INSERT INTO HistoricoDatosAfiliado
					   (afiliadoId
					   ,codigoCertDisc
					   ,vencimientoCertDisc
					   ,certificadoPermanente
					   ,accionRealizada
					   ,userId
					   ,Ip
					   ,fechaUltimaModificacion)
			SELECT afiliadoId
			      ,codigoCertDisc
				  ,vencimientoCertDisc
				  ,certificadoPermanente
				  ,accionRealizada
				  ,userId
				  ,Ip
				  ,fechaUltimaModificacion
			FROM DatosAfiliado
			WHERE  afiliadoid = @pAfiliadoid 

            UPDATE datosafiliado 
            SET    codigocertdisc = @pCodigoCertDisc
                   , vencimientocertdisc = @pVencimientoCertDisc
                   , certificadoPermanente = @pCertificadoPermanente
				   , accionRealizada = 'Actualizar'
				   , userId = @vUserId
				   , ip = @vIp
				   , fechaUltimaModificacion=GETDATE()
            WHERE  afiliadoid = @pAfiliadoid 
        END 
      ELSE 
        BEGIN 
			INSERT INTO DatosAfiliado
					   (afiliadoId
					   ,codigoCertDisc
					   ,vencimientoCertDisc
					   ,certificadoPermanente
					   ,accionRealizada
					   ,userId
					   ,Ip
					   ,fechaUltimaModificacion)
				SELECT @pAfiliadoid, 
					   @pCodigoCertDisc, 
					   @pVencimientoCertDisc,
					   @pCertificadoPermanente,
					   'Alta',
					   @vUserId,
					   @vIp,
					   GETDATE()
        END 
  END 
 
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
	,certificadoPermanente    
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
	,certificadoPermanente     
	,accionRealizada 
	,userId
	,Ip             
	FROM RegistroDiscapacidad
	WHERE  id = @pId
	              
UPDATE RegistroDiscapacidad  
SET afiliadoid = @pAfiliadoid
     ,periodo = @pPeriodo
     ,cuit = CASE WHEN dbo.GetCueFlag(@pCodigopractica) = 1 THEN ISNULL(@pCue, @pCuit) ELSE @pCuit END
     ,tipocomprobanteid = @pTipocomprobanteid
     ,tipoemisionid = @pTipoemisionid
     ,fechaemision = @pFechaemision
     ,caecai = @pCaecai
     ,puntoVenta = @pPuntoVenta
     ,numeroComprobante = @pNumeroComprobante
     ,importe = @pImporte
     ,cantidad = @pCantidad
     ,codigopractica = @pCodigopractica
     ,provinciaId = @pProvinciaId
     ,esdependiente = @pEsdependiente
     ,procesado = @pProcesado
	 ,vencimientoCertDisc = @pVencimientoCertDisc
	 ,codigoCertDisc = @pCodigoCertDisc
	 ,certificadoPermanente = @pCertificadoPermanente
	 ,accionRealizada= 'Actualizar'
	 ,userId = @vUserId 
	 ,ip = @vIp
WHERE id = @pId

															 
END

 