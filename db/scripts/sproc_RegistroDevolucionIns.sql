/****** Object:  StoredProcedure [dbo].[RegistroDevolucionIns]    Script Date: 18/02/2020 13:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistroDevolucionIns]
( 
 @pClave nvarchar(50)
,@pRnos nvarchar(50)
,@pTipoArchivo nvarchar(50)
,@pPeriodoPresentacion datetime
,@pPeriodoPrestacion datetime
,@pCuil varchar(30)
,@pCodigoPractica int
,@pCuit varchar(30)
,@pTipoComprobanteId int
,@pPuntoVenta int
,@pNumeroComprobante int
,@pImporteSolicitado decimal(8,2)
,@pImporteLiquidado decimal(8,2)
,@pNumeroEnvioAfip int
)
AS
BEGIN



if not exists(select 1 from RegistroDevolucion where [CLAVE_RENDICION] = @pClave
										and [RNOS] = @pRnos
										and [TIPO_ARCHIVO] = @pTipoArchivo
										and [PERIODO_PRESENTACION] = @pPeriodoPresentacion
										and [PERIODO_PRESTACION] = @pPeriodoPrestacion
										and [CUIL] = @pCuil
										and [CODIGO_PRACTICA] = @pCodigoPractica
										and [IMPORTE_SUBSIDIADO] = @pImporteLiquidado
										and [IMPORTE_SOLICITADO]= @pImporteSolicitado
										and [NRO_ENVIO_AFIP] = @pNumeroEnvioAfip
										and [CUIT_DEL_CBU] = @pCuit
										)

INSERT INTO RegistroDevolucion([CLAVE_RENDICION]
						,[RNOS]
						,[TIPO_ARCHIVO]
                        ,[PERIODO_PRESENTACION]
						,[PERIODO_PRESTACION]
						,[CUIL]
						,[CODIGO_PRACTICA]
						,[IMPORTE_SUBSIDIADO]
						,[IMPORTE_SOLICITADO]
						,[NRO_ENVIO_AFIP]
                        ,[CUIT_DEL_CBU]
						,[PROCESADO])
values(
						 @pClave
						,@pRnos
						,@pTipoArchivo
                        ,@pPeriodoPresentacion
						,@pPeriodoPrestacion
                        ,@pCuil
                        ,@pCodigoPractica
						,@pImporteLiquidado
                        ,@pImporteSolicitado
                        ,@pNumeroEnvioAfip
						,@pCuit
						,0)

END
