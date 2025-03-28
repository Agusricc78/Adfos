
/****** Object:  StoredProcedure [dbo].[RendicionGet]    Script Date: 03/08/2019 13:30:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RendicionGet] (@pAfiliadoCuil    NUMERIC = 0, 
                                      @pPeriodoini      DATETIME = NULL, 
                                      @pPeriodofin      DATETIME = NULL, 
                                      @pCuit            VARCHAR(30) = NULL, 
                                      @pNumeroEnvioAfip INT = 0) 
AS 
  BEGIN ; 
      --WITH ctetotal (cuil, periodoprestacion, codigopractica, totalsolicitado, 
      --     totalliquidado) 
      --     AS (SELECT cuil, periodoprestacion, codigopractica, 
      --                Sum(importesolicitado), Sum(importeliquidado) TotalLiquidado 
      --         FROM   rendicion 
      --         WHERE  ((numeroenvioafip = @pNumeroEnvioAfip OR @pNumeroEnvioAfip = 0) 
      --                  AND (cuil = @pAfiliadoCuil OR @pAfiliadoCuil = 0)) 
      --         GROUP  BY cuil, periodoprestacion, codigopractica) 
      SELECT clave, 
             r.periodopresentacion, 
             r.periodoprestacion, 
             r.cuil, 
             af.nombre + ' ' + af.apellido NombreApellido, 
             r.codigopractica, 
             np.modulo Practica, 
             r.cuit, 
             Isnull(p.razonsocial, r.cuit) Prestador, 
             p.cbu, 
             tc.comprobante, 
             puntoventa, 
             numerocomprobante, 
             importesolicitado, 
             importeliquidado, 
             --TotSolicitado, 
             --TotLiquidado, 
			 ImpAliquidar AS ImporteAliquidar, 
             r.numeroenvioafip, 
             r.RegistroDiscapacidadId
      FROM   rendicion r 
             LEFT JOIN afiliadosview af 
                    ON af.cuil = r.cuil AND Isnull(af.baja, 'N') = 'N' 
             LEFT JOIN tipocomprobante tc 
                    ON tc.codigo = r.tipocomprobanteid 
             LEFT JOIN nomencladorpracticas np 
                    ON np.codigo = r.codigopractica 
             LEFT JOIN prestadores p 
                    ON p.cuit = r.cuit 
      WHERE  r.codigopractica NOT IN (SELECT codigo FROM nomencladorpracticas WHERE  cueflag = 1) 
             AND ( af.cuil = @pAfiliadoCuil OR @pAfiliadoCuil = 0 ) 
             AND (((NULLIF(@pPeriodoini, '') IS NULL) OR r.periodoprestacion >= @pPeriodoini) 
                   AND (( NULLIF(@pPeriodofin, '') IS NULL) OR r.periodoprestacion <= @pPeriodofin)) 
             AND (r.cuit = @pCuit OR @pCuit IS NULL) 
             AND (r.numeroenvioafip = @pNumeroEnvioAfip OR @pNumeroEnvioAfip = 0) 
      ORDER  BY r.cuit, r.periodoprestacion 
  END 