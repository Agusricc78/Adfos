/****** Object:  StoredProcedure [dbo].[RegistroDiscapacidadGetTotales]    Script Date: 04/24/2019 18:52:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EstadoGetTotales] (@pAfiliadoId INT, 
										  @pPeriodoini DATETIME = NULL, 
										  @pPeriodofin DATETIME = NULL,
										  @pEstado INT = NULL,
                                          @pNumeroEnvioAfip INT = 0) 
AS 
  BEGIN 
		SELECT epd.id
			  ,epd.Descripcion
			  ,COUNT(rd.id) as Total
		FROM [dbo].[EstadoProcesoDiscapacidad] epd
		LEFT JOIN RegistroDiscapacidad rd
		ON epd.id = rd.procesado 
		--AND rd.Activo = 1
		--AND (((NULLIF(@pPeriodoini, '') IS NULL) OR rd.periodo >= @pPeriodoini) 
		--	 AND ((NULLIF(@pPeriodofin, '') IS NULL) OR rd.periodo <= @pPeriodofin)) 
	    AND  rd.Activo = 1
		AND (afiliadoid = @pAfiliadoId OR @pAfiliadoId = 0) 
		AND (((NULLIF(@pPeriodoini, '') IS NULL) OR rd.periodo >= @pPeriodoini) 
		   AND ((NULLIF(@pPeriodofin, '') IS NULL) OR rd.periodo <= @pPeriodofin)) 
		AND ((@pEstado IS NULL) OR (rd.procesado = @pEstado))
		AND ((@pNumeroEnvioAfip = rd.NumeroEnvioAfip) OR (@pNumeroEnvioAfip = 0))
		GROUP BY epd.id, epd.Descripcion

  END 