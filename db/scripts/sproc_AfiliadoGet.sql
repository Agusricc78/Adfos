
/****** Object:  StoredProcedure [dbo].[AfiliadoGet]    Script Date: 03/21/2019 17:54:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AfiliadoGet] 
                                     @pCuil VARCHAR(50) = NULL, 
                                     @pNombre VARCHAR(50)= NULL, 
                                     @pApellido VARCHAR(50) = NULL ,
									 @pNroBeneficiario varchar(50) = null
AS 
  BEGIN 
      SELECT af.afi_codigo  AS Id, 
             af.nro_beneficiario  AS  NroBeneficiario, 
             af.nro_inos AS NroOrden, 
             af.nro_legajo AS NroLegajo, 
             af.cuil, 
             af.nombre, 
             af.apellido, 
             af.sexo, 
             af.fecha_de_alta AS FechaAlta, 
             af.fecha_nacimiento AS FechaNacimiento, 
             af.observaciones,
             af.codigoCertDisc AS CodigoCertDisc,
             af.vencimientoCertDisc AS VencimientoCertDisc,
             af.certificadoPermanente AS CertificadoPermanente
      --FROM   ad_afiliados af 
      FROM AfiliadosView af
      WHERE ( Isnull(@pCuil, '') = '' 
                    OR af.Cuil LIKE '%' + @pCuil + '%' ) 
	         AND ( Isnull(@pNombre, '') = '' 
                    OR af.nombre LIKE '%' + @pNombre + '%' ) 
             AND ( Isnull(@pApellido, '') = '' 
                    OR af.apellido LIKE @pApellido + '%' ) 
			AND ( Isnull(@pNroBeneficiario, '') = '' 
                    OR af.Nro_Beneficiario LIKE @pNroBeneficiario + '%' ) 
			AND Isnull(af.baja, 'N') = 'N' 
      ORDER  BY Apellido,Nombre 
  END 