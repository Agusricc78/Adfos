/****** Object:  StoredProcedure [dbo].[RendicionCalc]    Script Date: 18/02/2020 13:37:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RendicionCalc] (@pNumeroEnvioAfip INT) 
AS 
  BEGIN ; 
      WITH ctetotal (cuil, periodoprestacion, codigopractica, TotalSolicitado, 
           TotalLiquidado) 
           AS (SELECT cuil, periodoprestacion, codigopractica, 
                      Sum(importesolicitado), Sum(importeliquidado) 
               FROM   rendicion 
               WHERE  (numeroenvioafip = @pNumeroEnvioAfip) 
               GROUP  BY cuil, periodoprestacion, codigopractica),
	ValoresActualizados AS 
	 (SELECT Clave, 
             Cast((r.importesolicitado / ctetotal.TotalSolicitado ) * 
                  TotalLiquidado AS DECIMAL(16, 2)) ImpAliquidar
      FROM   rendicion AS r
             LEFT JOIN ctetotal 
                    ON ctetotal.cuil = r.cuil 
                       AND ctetotal.periodoprestacion = r.periodoprestacion 
                       AND ctetotal.codigopractica = r.codigopractica 
      WHERE  r.codigopractica NOT IN (SELECT codigo FROM nomencladorpracticas WHERE  cueflag = 1) 
             AND (r.numeroenvioafip = @pNumeroEnvioAfip))
      
		UPDATE dbo.Rendicion
		SET ImpAliquidar = va.ImpAliquidar
		FROM dbo.Rendicion AS r
		JOIN ValoresActualizados AS va ON r.Clave = va.Clave;
		
		-- Coloca en estado Procesado (8) o Anulado Liquidado (10) todos los registros que vinieron en el archivo ENVIO
		UPDATE dbo.RegistroDiscapacidad
		--SET dbo.RegistroDiscapacidad.procesado = CASE WHEN procesado = 7 THEN 7 ELSE 8 END,
		SET dbo.RegistroDiscapacidad.procesado = CASE -- No cambia el estado de los Liquidado Manual (9) ni los Historicos (99)
													  WHEN rd.procesado IN (9, 99) THEN rd.procesado 
													  -- Si el estado es Anulado (7) lo pasa a Anulado Liquidado (10)
													  WHEN rd.procesado = 7 THEN 10
													  -- En cualquier otro caso lo pasa a Liquidado (8)
													  ELSE 8 END,
		dbo.RegistroDiscapacidad.NumeroEnvioAfip = @pNumeroEnvioAfip,
		dbo.RegistroDiscapacidad.RendicionId = ren.Clave
		FROM dbo.RegistroDiscapacidad AS rd
		INNER JOIN (SELECT MAX(id) AS lastRegisterId FROM dbo.RegistroDiscapacidad GROUP BY cuit, puntoVenta, numeroComprobante, periodo) AS rdUnico
		ON rd.id = rdUnico.lastRegisterId
		INNER JOIN dbo.Rendicion AS ren
		-- ON rd.cuit = ren.Cuit
		ON Replicate('0', ( 11 - Len(rd.cuit))) + rd.cuit = Replicate('0', ( 11 - Len(ren.Cuit))) + ren.Cuit
		AND rd.periodo = ren.PeriodoPrestacion
		AND rd.puntoVenta = ren.PuntoVenta
		AND rd.numeroComprobante = ren.NumeroComprobante
		AND rd.importe = ren.ImporteSolicitado
		AND ren.NumeroEnvioAfip = @pNumeroEnvioAfip;
		
		--Asocia el numero de RegistroDiscapacidad que solicito el reintegro recibido en el archivo ENVIO
		UPDATE dbo.Rendicion
		SET dbo.Rendicion.RegistroDiscapacidadId = rd.id
		FROM dbo.RegistroDiscapacidad AS rd
		INNER JOIN dbo.Rendicion AS ren
		-- ON rd.cuit = ren.Cuit
		ON Replicate('0', ( 11 - Len(rd.cuit))) + rd.cuit = Replicate('0', ( 11 - Len(ren.Cuit))) + ren.Cuit
		AND rd.periodo = ren.PeriodoPrestacion
		AND rd.puntoVenta = ren.PuntoVenta
		AND rd.numeroComprobante = ren.NumeroComprobante
		AND rd.importe = ren.ImporteSolicitado
		AND ren.NumeroEnvioAfip = @pNumeroEnvioAfip;

		--Guarda el valor calculado en ImpAliquidar en el IMPORTE APLICADO SSS de los Registro Devolucion
		UPDATE dbo.RegistroDevolucion
		SET CBU = Prest.CBU,
		IMPORTE_APLICADO_SSS = Ren.ImpAliquidar,
		FONDOS_PROPIOS_OTRA_CUENTA = r.IMPORTE_SOLICITADO - Ren.ImpAliquidar
		FROM dbo.RegistroDevolucion AS r
		JOIN Rendicion AS Ren ON r.[CLAVE_RENDICION] = Ren.Clave
		LEFT JOIN Prestadores AS Prest
	  	ON Ren.cuit = Prest.Cuit;
END 