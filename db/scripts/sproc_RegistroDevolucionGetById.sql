
/****** Object:  StoredProcedure [dbo].[[RegistroDevolucionGetById]]    Script Date: 04/04/2019 21:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistroDevolucionGetById] (@pClave VARCHAR(50))
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
			 r.[CODIGO_ERROR_SSS] as CodigoErrorSSS
      FROM   [RegistroDevolucion] r 
             LEFT JOIN afiliadosview af 
                    ON af.cuil = r.cuil AND Isnull(af.baja, 'N') = 'N' 
             LEFT JOIN nomencladorpracticas np 
                    ON np.codigo = r.[CODIGO_PRACTICA] 
             LEFT JOIN prestadores p 
                    ON p.cuit = r.[CUIT_DEL_CBU] 
             INNER JOIN estadoprocesodevolucion ep 
                     ON ep.id = r.[PROCESADO] 
	  WHERE r.[CLAVE_RENDICION] = @pClave
  END 