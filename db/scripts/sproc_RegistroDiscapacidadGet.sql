
/****** Object:  StoredProcedure [dbo].[RegistroDiscapacidadGet]    Script Date: 3/6/2019 13:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[RegistroDiscapacidadGet] (@pAfiliadoId INT, 
												 @pRnos varchar(30),
                                                 @pPeriodoini DATETIME = NULL, 
                                                 @pPeriodofin DATETIME = NULL, 
                                                 @pEstado INT = NULL,
                                                 @pNumeroEnvioAfip INT = 0,
                                                 @pFiltra     BIT = 0) 
AS 
  BEGIN 
      SELECT DISTINCT rd.id, 
                      afiliadoid, 
                      periodo, 
                      rd.cuit, 
                      af.cuil, 
                      tipocomprobanteid, 
                      Isnull(tc.comprobante, 'No Cargado') TipoComprobante, 
                      tipoemisionid, 
                      Isnull(te.descripcion, 'No Cargado') TipoEmision, 
                      fechaemision, 
                      caecai, 
                      rd.puntoventa, 
                      rd.numerocomprobante, 
                      importe, 
                      cantidad, 
                      codigopractica, 
                      np.modulo                            Practica, 
                      provinciaid, 
                      ap.provincia, 
                      esdependiente, 
                      procesado, 
                      ep.descripcion                       AS estado, 
                      rd.codigoErrorSSS AS ErrorCodigo,
                      codigoCertDisc AS CodigoCertDisc,
					  vencimientoCertDisc AS VencimientoCertDisc,
					  certificadoPermanente AS CertificadoPermanente,
					  NumeroEnvioAfip,
					  RendicionId,
					  ISNULL(rd.fechaAlta, rd.fechaEmision) AS FechaAlta,
                      'DS|'+ @pRnos +'|' + isnull(af.cuil,'') + '|'
                      + CASE WHEN codigoCertDisc IS NULL THEN Replicate(' ', 40) ELSE Cast(codigoCertDisc AS VARCHAR(40)) 
                      + Replicate(' ', ( 40 - Len(Cast(codigoCertDisc AS VARCHAR(40))))) END + '|' 
                      + CASE WHEN vencimientoCertDisc IS NULL THEN Replicate(' ', 10) ELSE CONVERT(VARCHAR, vencimientoCertDisc, 103) END + '|' 
                      + LEFT(CONVERT(VARCHAR, isnull(periodo,'19000101'), 112), 6) + '|' 
                      + CASE WHEN isnull(np.cueflag,0) = 1 THEN Replicate('0', (11-Len(isnull(cue,'')
                      )) 
                      ) + 
                      isnull(cue,'') ELSE isnull(rd.cuit,'') 
                      END + '|' 
                      + Replicate('0', (2 - Len(Cast(isnull(tipocomprobanteid,0) AS 
                      VARCHAR) 
                      ))) 
                      + Cast(isnull(tipocomprobanteid,0) AS VARCHAR) + '|' 
                      + isnull(te.codigo,'') + '|' 
                      + CONVERT(VARCHAR, isnull(fechaemision,'19000101'), 103) + '|' 
                      + Replicate('0', (14 - Len(isnull(caecai,'')))) + caecai 
                      + '|' 
                      + Replicate('0', ( 4 -Len(Cast(isnull(puntoventa,0) AS VARCHAR)))) 
                      + Cast(isnull(puntoventa,0) AS VARCHAR) + '|' 
                      + Replicate('0', ( 8 -Len(Cast(isnull(rd.numerocomprobante,0) AS 
                      VARCHAR(8 
                      ))))) 
                      + Cast(isnull(rd.numerocomprobante,0) AS VARCHAR(8)) 
                      + '|' 
                      + Replicate('0', ( 10 -Len(Replace(Replace(CONVERT(VARCHAR 
                      ( 
                      10), 
                      isnull(importe,0)), '.', 
                      ''), ',', '')))) 
                      + Replace(Replace(CONVERT(VARCHAR(10), isnull(importe,0)), '.', ''), 
                      ',', 
                      '') 
                      + '|' 
                      + Replicate('0', ( 10 -Len(Replace(Replace(CONVERT(VARCHAR 
                      ( 
                      10), 
                      isnull(importe,0)), '.', 
                      ''), ',', '')))) 
                      + Replace(Replace(CONVERT(VARCHAR(10), isnull(importe,0)), '.', ''), 
                      ',', 
                      '') 
                      + '|' 
                      + Replicate('0', ( 3 -Len(Cast(isnull(codigopractica,0) AS VARCHAR)) 
                      )) 
                      + Cast(isnull(codigopractica,0) AS VARCHAR) + '|' 
                      + Replicate('0', ( 6 -Len(Cast(isnull(cantidad,0) AS VARCHAR)))) 
                      + Cast(isnull(cantidad,0) AS VARCHAR) + '|' 
                      + Replicate('0', ( 2 -Len(Cast(CASE WHEN 
                      isnull(zonadesfavorablesss,0) 
                      = 1 
                      THEN 
                      isnull(provinciaid,0) ELSE 0 END AS VARCHAR)))) + Cast(CASE WHEN 
                      isnull(zonadesfavorablesss,0) = 1 
                      THEN isnull(provinciaid,0) ELSE 0 END AS VARCHAR) + '|' + CASE WHEN 
                      isnull(esdependiente,0) = 1 THEN 
                      'S' ELSE 'N' END                     Export 
      FROM   registrodiscapacidad rd 
             LEFT JOIN prestadores p 
                    ON p.cuit = rd.cuit 
             INNER JOIN ad_afiliados af 
                     ON af.afi_codigo = rd.afiliadoid 
             INNER JOIN ad_provincias ap 
                     ON ap.codigosss = rd.provinciaid 
             INNER JOIN tipocomprobante tc 
                     ON tc.codigo = rd.tipocomprobanteid 
             INNER JOIN tipoemision te 
                     ON te.id = rd.tipoemisionid 
             INNER JOIN nomencladorpracticas np 
                     ON np.codigo = rd.codigopractica 
             INNER JOIN estadoprocesodiscapacidad ep 
                     ON ep.id = procesado 
      WHERE  Activo = 1
			 AND( afiliadoid = @pAfiliadoId 
                OR @pAfiliadoId = 0 ) 
             AND ( ( ( NULLIF(@pPeriodoini, '') IS NULL ) 
                      OR periodo >= @pPeriodoini ) 
                   AND ( ( NULLIF(@pPeriodofin, '') IS NULL ) 
                          OR periodo <= @pPeriodofin ) ) 
			-- Para la generacion del archivo se corre con pFiltra = 1
			-- Entonces trae todos los ReintegroDiscapacidad que no tengan estado:
			-- ACEPTADO, ANULADO, LIQUIDADO, LIQUIDADO MANUAL, ANULADO LIQUIDADO y HISTORICO
             AND (rd.procesado NOT IN (6, 7, 8, 9, 10, 99) OR @pFiltra = 0) 
             AND ((@pEstado IS NULL) OR (procesado = @pEstado))
             AND ((@pNumeroEnvioAfip = NumeroEnvioAfip) OR (@pNumeroEnvioAfip = 0))
  END
