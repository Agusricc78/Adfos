USE [final1_dev]
GO

/****** Object:  View [dbo].[AfiliadosView]    Script Date: 05/18/2019 17:26:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[AfiliadosView] WITH SCHEMABINDING AS
SELECT tt.[Afi_codigo]
      ,tt.[Afi_afi_codigo]
      ,tt.[Nro_Beneficiario]
      ,tt.[Nro_Inos]
      ,tt.[Nro_Legajo]
      ,tt.[Nombre]
      ,tt.[Apellido]
      ,tt.[Nacionalidad_codigo]
      ,tt.[Cuil]
      ,tt.[Fecha_Nacimiento]
      ,tt.[Sexo]
      ,tt.[Estado_Civil_codigo]
      ,tt.[Parentesco_codigo]
      ,tt.[Tipo_afi]
      ,tt.[tipo_beneficiario_titular]
      ,tt.[Empresa_codigo]
      ,tt.[OS_Codigo]
      ,tt.[Contratado]
      ,tt.[Vigencia_Contrato]
      ,tt.[Fecha_Ingreso_Empleo]
      ,tt.[Fecha_Egreso_Empleo]
      ,tt.[Fecha_Ingreso_OS]
      ,tt.[Fecha_Egreso_OS]
      ,tt.[Observaciones]
      ,tt.[Baja]
      ,tt.[Fecha_Baja]
      ,tt.[fecha_reingreso]
      ,tt.[fecha_de_alta]
      ,tt.[OS_Traspaso]
      ,tt.[Motivo]
      ,tt.[Adherente]
      ,tt.[Sindicato]
      ,tt.[Nro_sindicato]
      ,tt.[Jubilado]
      ,tt.[Traspaso]
      ,tt.[unifica_aportes]
      ,tt.[beneficios]
      ,tt.[prest_codigo]
      ,tt.[plan_prest]
      ,tt.[ingreso_SEADI]
      ,tt.[egreso_SEADI]
      ,tt.[sepelio]
      ,tt.[Nro_en_prestador]
      ,tt.[monotributista]
      ,tt.[fecha_alta_sistema]
      ,tt.[error_padron]
      ,tt.[fecha_proceso_padron]
      ,tt.[detalle_error_padron]
      ,tt.[sssAceptado]
      ,tt.[sssNovedad]
      ,tt.[fpresentacion]
      ,tt.[sssrotulo]
      ,tt.[sssFechaDesde]
      ,tt.[sssFechaHasta]
      ,tt.[sssDevolucionError]
      ,tt.[id_registro_padron_sss]
      ,tt.[fecha_modificacion]
      ,da.[vencimientoCertDisc]
      ,da.[codigoCertDisc]
      ,da.[certificadoPermanente]
FROM dbo.Ad_afiliados tt
INNER JOIN
    (SELECT Cuil, MAX(fecha_alta_sistema) AS MaxDateTime
    FROM dbo.Ad_afiliados
    GROUP BY Cuil) groupedtt
ON tt.Cuil = groupedtt.Cuil 
AND tt.fecha_alta_sistema = groupedtt.MaxDateTime
LEFT JOIN dbo.DatosAfiliado da
ON tt.Afi_codigo = da.afiliadoId
WHERE  tt.Cuil IS NOT NULL 
AND tt.Cuil <> ''
AND tt.Baja <> 'S'


GO

