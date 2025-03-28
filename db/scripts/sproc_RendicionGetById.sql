
/****** Object:  StoredProcedure [dbo].[[RendicionGetById]]    Script Date: 04/04/2019 21:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RendicionGetById] (@pClave VARCHAR(50))
AS 
  BEGIN ; 
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
             r.numeroenvioafip 
      FROM   rendicion r 
             LEFT JOIN afiliadosview af 
                    ON af.cuil = r.cuil AND Isnull(af.baja, 'N') = 'N' 
             LEFT JOIN tipocomprobante tc 
                    ON tc.codigo = r.tipocomprobanteid 
             LEFT JOIN nomencladorpracticas np 
                    ON np.codigo = r.codigopractica 
             LEFT JOIN prestadores p 
                    ON p.cuit = r.cuit 
	  WHERE Clave = @pClave
      --WHERE  r.codigopractica NOT IN (SELECT codigo FROM nomencladorpracticas WHERE  cueflag = 1) 
      --       AND ( af.cuil = @pAfiliadoCuil OR @pAfiliadoCuil = 0 ) 
      --       AND (((NULLIF(@pPeriodoini, '') IS NULL) OR r.periodoprestacion >= @pPeriodoini) 
      --             AND (( NULLIF(@pPeriodofin, '') IS NULL) OR r.periodoprestacion <= @pPeriodofin)) 
      --       AND (r.cuit = @pCuit OR @pCuit IS NULL) 
      --       AND (r.numeroenvioafip = @pNumeroEnvioAfip OR @pNumeroEnvioAfip = 0) 
      --ORDER  BY r.cuit, r.periodoprestacion 
  END 