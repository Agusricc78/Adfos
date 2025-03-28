/****** Object:  StoredProcedure [dbo].[RegistroDevolucionGet]    Script Date: 03/08/2019 13:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistroDevolucionGet] (@pAfiliadoCuil    NUMERIC = 0, 
                                      @pPeriodoini      DATETIME = NULL, 
                                      @pPeriodofin      DATETIME = NULL, 
                                      @pCuit            VARCHAR(30) = NULL, 
                                      @pNumeroEnvioAfip INT = 0) 
AS 
  BEGIN ; 
      SELECT r.[CLAVE_RENDICION] as ClaveRendicion, 
			 r.[RNOS] as Rnos,
			 r.[TIPO_ARCHIVO] as TipoArchivo,
             r.[PERIODO_PRESENTACION] as PeriodoPresentacion, 
             r.[PERIODO_PRESTACION] as PeriodoPrestacion, 
             r.[CUIL] as Cuil, 
             af.nombre + ' ' + af.apellido as NombreApellido, 
             r.[CODIGO_PRACTICA] as CodigoPractica, 
             np.modulo as Practica, 
			 r.[IMPORTE_SUBSIDIADO] as ImporteSubsidiado, 
			 r.[IMPORTE_SOLICITADO] as ImporteSolicitado, 
			 r.[NRO_ENVIO_AFIP] as NroEnvioAfip,
             r.[CUIT_DEL_CBU] as CuitDelCbu, 
			 Isnull(p.razonsocial, r.[CUIT_DEL_CBU]) Prestador, 
			 r.[CBU] as Cbu, 
             r.[FECHA_TRANSFERENCIA_I] as FechaTransferenciaI,
			 r.[IMPORTE_APLICADO_SSS] AS ImporteAplicadoSSS, 
			 r.[FONDOS_PROPIOS_OTRA_CUENTA] as FondosPropiosOtraCuenta,
			 r.[PROCESADO] as Procesado,
			 ep.Descripcion as Estado,
			 r.[CODIGO_ERROR_SSS] as CodigoErrorSSS,

			CONVERT(VARCHAR, [CLAVE_RENDICION])
				+ '|' + r.[RNOS]
				+ '|' + r.[TIPO_ARCHIVO]
				+ '|' + LEFT(CONVERT(VARCHAR, isnull(r.[PERIODO_PRESENTACION],'19000101'), 112), 6)
				+ '|' + LEFT(CONVERT(VARCHAR, isnull(r.PERIODO_PRESTACION,'19000101'), 112), 6)
				+ '|' + CONVERT(VARCHAR, r.[CUIL])
				+ '|' + Replicate('0', ( 3 -Len(Cast(isnull(r.[CODIGO_PRACTICA],0) AS VARCHAR)))) + Cast(isnull(r.[CODIGO_PRACTICA],0) AS VARCHAR)
				+ '|' + Replicate('0', ( 13 -Len(Cast(isnull(r.[IMPORTE_SUBSIDIADO],0) AS VARCHAR)))) + Cast(isnull(r.[IMPORTE_SUBSIDIADO],0) AS VARCHAR)
				+ '|' + Replicate('0', ( 13 -Len(Cast(isnull(r.[IMPORTE_SOLICITADO],0) AS VARCHAR)))) + Cast(isnull(r.[IMPORTE_SOLICITADO],0) AS VARCHAR)
				+ '|' + Replicate('0', ( 4 -Len(Cast(isnull(r.[NRO_ENVIO_AFIP],0) AS VARCHAR)))) + Cast(isnull(r.[NRO_ENVIO_AFIP],0) AS VARCHAR)
				+ '|' + Replicate('0', ( 11 -Len(Cast(isnull(r.[CUIT_DEL_CBU],0) AS VARCHAR)))) + Cast(isnull(r.[CUIT_DEL_CBU],0) AS VARCHAR)
				+ '|' + CONVERT(VARCHAR, ISNULL(r.[CBU],  + Replicate('0', 22)))
				+ '|' + Replicate(' ', 20) -- ORDEN DE PAGO I
				+ '|' + Replicate(' ', 20) -- ORDEN DE PAGO II
				+ '|' + CONVERT(VARCHAR, isnull(r.[FECHA_TRANSFERENCIA_I],'19000101'), 103)
				+ '|' + Replicate(' ', 10) -- FECHA TRANSFERENCIA II
				+ '|' + Replicate('0', 10) -- CHEQUE
				+ '|' + Replicate('0', ( 13 -Len(Cast(isnull(r.[IMPORTE_APLICADO_SSS],0) AS VARCHAR)))) + Cast(isnull(r.[IMPORTE_APLICADO_SSS],0) AS VARCHAR) -- IMPORTE TRANSFERIDO
				+ '|' + Replicate('0', 12) -- RETENCION DE GANANCIAS
				+ '|' + Replicate('0', 12) -- RETENCION IIBB
				+ '|' + Replicate('0', 12) -- OTRAS RETENCIONES
				+ '|' + Replicate('0', ( 13 -Len(Cast(isnull(r.[IMPORTE_APLICADO_SSS],0) AS VARCHAR)))) + Cast(isnull(r.[IMPORTE_APLICADO_SSS],0) AS VARCHAR) -- IMPORTE APLICADO SSS
				+ '|' + Replicate('0', 12) -- FONDOS PROPIOS
				+ '|' + Replicate('0', ( 13 -Len(Cast(isnull(r.[FONDOS_PROPIOS_OTRA_CUENTA],0) AS VARCHAR)))) + Cast(isnull(r.[FONDOS_PROPIOS_OTRA_CUENTA],0) AS VARCHAR) -- FONDOS PROPIOS OTRA CUENTA
				+ '|' + Replicate('0', 8) -- NRO DE RECIBO
				+ '|' + Replicate('0', 12) -- IMPORTE TRASLADADO
				+ '|' + Replicate('0', 12) -- IMPORTE DEVUELTO
				+ '|' + Replicate('0', 12) -- SALDO NO APLICADO
				+ '|' + Replicate('0', 12) -- RECUPERO FONDOS PROPIOS
				+ '|' + Replicate(' ', 150) -- OBSERVACIONES
				Export

      FROM   [RegistroDevolucion] r 
             LEFT JOIN afiliadosview af 
                    ON af.cuil = r.cuil AND Isnull(af.baja, 'N') = 'N' 
             LEFT JOIN nomencladorpracticas np 
                    ON np.codigo = r.[CODIGO_PRACTICA] 
             LEFT JOIN prestadores p 
                    ON p.cuit = r.[CUIT_DEL_CBU] 
             LEFT JOIN estadoprocesodevolucion ep 
                     ON ep.id = r.[PROCESADO] 
      WHERE  ( af.cuil = @pAfiliadoCuil OR @pAfiliadoCuil = 0 ) 
             AND (((NULLIF(@pPeriodoini, '') IS NULL) OR r.[PERIODO_PRESTACION] >= @pPeriodoini) 
                   AND (( NULLIF(@pPeriodofin, '') IS NULL) OR r.[PERIODO_PRESTACION] <= @pPeriodofin)) 
             AND (r.[CUIT_DEL_CBU] = @pCuit OR @pCuit IS NULL) 
             AND (r.[NRO_ENVIO_AFIP] = @pNumeroEnvioAfip OR @pNumeroEnvioAfip = 0) 
      ORDER  BY r.[CUIT_DEL_CBU], r.[PERIODO_PRESENTACION] 
  END 