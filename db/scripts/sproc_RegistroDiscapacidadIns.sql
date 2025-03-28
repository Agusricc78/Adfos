
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RegistroDiscapacidadIns] (
												 @pAfiliadoid          INT, 
                                                 @pVencimientoCertDisc DATETIME, 
                                                 @pCodigoCertDisc      VARCHAR(40) = '', 
                                                 @pCertificadoPermanente       BIT, 
                                                 @pPeriodo             DATETIME, 
                                                 @pCuit                VARCHAR(30), 
                                                 @pTipocomprobanteid   INT, 
                                                 @pTipoemisionid       INT, 
                                                 @pFechaemision        DATETIME, 
                                                 @pCaecai              VARCHAR(14), 
                                                 @pPuntoVenta          INT, 
                                                 @pNumeroComprobante   INT, 
                                                 @pImporte             DECIMAL(8, 2), 
                                                 @pCantidad            INT, 
                                                 @pCodigopractica      INT, 
                                                 @pProvinciaId         INT, 
                                                 @pEsdependiente       BIT, 
                                                 @pRazonSocial         VARCHAR(80), 
                                                 @pCbu                 VARCHAR(30), 
                                                 @pCue                 VARCHAR(30),
												 @pToken			   UNIQUEIDENTIFIER
                                                ) 
AS 
  BEGIN TRY
  DECLARE @vUserId INT ,@vResult int, @vIp VARCHAR(40) , @vUserName varchar(50),@errText varchar(100)

    SELECT @vIp = ip, 
           @vUserId = userid 
    FROM   applogindata 
    WHERE  userid = (SELECT userid 
                     FROM   apptoken 
                     WHERE  authtoken = @pToken) 

	  
      IF EXISTS(SELECT 1 
                FROM   registrodiscapacidad 
                WHERE  cuit = @pCuit 
                       AND activo = 1
                       AND puntoventa = @pPuntoVenta 
                       AND numerocomprobante = @pNumeroComprobante 
                       AND @pPuntoVenta <> 0
					   AND procesado <> 7) 
        BEGIN 
		 set @errText ='Nro. Factura Duplicado:' + Replicate('0', ( 4 -Len(Cast(isnull(@pPuntoVenta,0) AS VARCHAR))))  
									   + Cast(isnull(@pPuntoVenta,0) AS VARCHAR)+'-'
	  								   + Replicate('0', ( 8 -Len(Cast(isnull(@pNumeroComprobante,0) AS VARCHAR(8)))))
									   + Cast(isnull(@pNumeroComprobante,0) AS VARCHAR(8)) 
									   + ',Cuit:'+cast(isnull(@pCuit,'') AS VARCHAR(20))

            RAISERROR (@errText,16,1) 
        END 
      ELSE 
        BEGIN 
            IF EXISTS(SELECT 1 
                      FROM   prestadores 
                      WHERE  cuit = @pCuit 
                              OR ( Isnull(@pCuit, '') = '' 
                                   AND cue = @pCue )) 
              BEGIN 
                  UPDATE prestadores 
                  SET    cue = Isnull(@pCue, cue), 
                         razonsocial = Isnull(@pRazonSocial, razonsocial), 
                         cbu = Isnull(@pCbu, cbu), 
						 accionRealizada = 'Actualizar',
                         fechaultimamodificacion = Getdate(),
						 userId = @vUserId,
						 ip = @vIp
                  WHERE  cuit = @pCuit 
                          OR ( Isnull(@pCuit, '') = '' 
                               AND cue = @pCue ) 
              END 
            ELSE 
              BEGIN 
                  INSERT INTO prestadores ([Cuit]
									      ,[Cue]
									      ,[RazonSocial]
									      ,[CBU]
									      ,[Banco]
									      ,[Sucursal]
									      ,[TipoCuenta]
									      ,[Direccion]
									      ,[FechaAlta]
									      ,[FechaUltimaModificacion]
										  ,[accionRealizada]
										  ,[userId]
										  ,[ip])
                  SELECT Rtrim(Ltrim(Isnull(@pCuit, @pCue))), 
                         Rtrim(Ltrim(@pCue)), 
                         @pRazonSocial, 
                         @pCbu, 
                         NULL, 
                         NULL, 
                         NULL, 
                         NULL, 
                         Getdate(), 
                         Getdate(),
						 'Alta',
						 @vUserId,
						 @vIp
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
							   , fechaUltimaModificacion = Getdate()
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
								   Getdate()
                    END 
              END 
                               
            INSERT INTO registrodiscapacidad 
                        (afiliadoid, 
                         periodo, 
                         cuit, 
                         tipocomprobanteid, 
                         tipoemisionid, 
                         fechaemision, 
                         caecai, 
                         puntoventa, 
                         numerocomprobante, 
                         importe, 
                         cantidad, 
                         codigopractica, 
                         provinciaid, 
                         esdependiente, 
                         procesado, 
                         fechaalta, 
                         fechaultimamodificacion,
                         codigoCertDisc,
                         vencimientoCertDisc,
                         certificadoPermanente,
						 accionRealizada,
						 userId,
						 ip) 
            VALUES      ( @pAfiliadoid, 
                          @pPeriodo, 
                          Isnull(@pCuit, @pCue), 
                          @pTipocomprobanteid, 
                          @pTipoemisionid, 
                          @pFechaemision, 
                          @pCaecai, 
                          @pPuntoVenta, 
                          @pNumeroComprobante, 
                          @pImporte, 
                          @pCantidad, 
                          @pCodigopractica, 
                          @pProvinciaId, 
                          @pEsdependiente, 
                          0, 
                          Getdate(), 
                          Getdate(),
                          @pCodigoCertDisc,
                          @pVencimientoCertDisc,
                          @pCertificadoPermanente,
						  'Alta',
					      @vUserId,
						  @vIp ) 

            SELECT Scope_identity() 
        END 
  END TRY
BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );
END CATCH