
/****** Object:  StoredProcedure [dbo].[RegistroDiscapacidadGetById]    Script Date: 03/27/2019 21:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RegistroDiscapacidadGetById]
( @pId int)
AS
BEGIN
select rd.id
     ,afiliadoid 
     ,periodo						
     ,rd.cuit								
     ,tipocomprobanteid	
     ,isnull(tc.comprobante,'No Cargado') TipoComprobante
     ,tipoemisionid				
	 ,isnull(te.Descripcion,'No Cargado') TipoEmision
     ,fechaemision				
     ,caecai						
     ,puntoVenta				
     ,numeroComprobante
     ,importe						
     ,cantidad						
     ,codigopractica	
	 ,np.Modulo Practica		
     ,provinciaId					
	 ,ap.Provincia
     ,esdependiente			
     ,procesado	
     ,ep.descripcion AS estado
	 ,p.Cbu
	 ,p.RazonSocial
	 ,p.Cue
	 ,rd.codigoErrorSSS AS ErrorCodigo
	 ,er.descripcion AS ErrorDescripcion
	 ,er.verificacion AS ErrorVerificacion
	 ,codigoCertDisc AS CodigoCertDisc
	 ,vencimientoCertDisc AS VencimientoCertDisc
	 ,certificadoPermanente AS CertificadoPermanente
	 ,NumeroEnvioAfip
	 ,RendicionId
	 ,ISNULL(rd.fechaAlta, rd.fechaEmision) AS FechaAlta
	 ,af.Nombre AS NombreAfiliado
	 ,af.Apellido AS ApellidoAfiliado
	 ,af.Cuil AS CuilAfiliado
	 ,DATEDIFF(yy,af.Fecha_Nacimiento,getdate()) Edad
From RegistroDiscapacidad rd
left join Prestadores p on p.Cuit = rd.cuit  or (p.Cuit is null and p.Cue= rd.cuit)
left join Ad_provincias ap on ap.codigoSSS = rd.provinciaId
left join TipoComprobante tc on tc.codigo = rd.tipocomprobanteid
left join TipoEmision te on te.id =rd.tipoemisionid
left join NomencladorPracticas np on np.Codigo= rd.codigopractica
left join ErrorRegistroDiscapacidad er on er.codigo = (rd.codigoErrorSSS * -1)
left join estadoprocesodiscapacidad ep on ep.id = procesado 
left join Ad_afiliados af ON rd.afiliadoid = af.Afi_codigo
where rd.id = @pId
END


