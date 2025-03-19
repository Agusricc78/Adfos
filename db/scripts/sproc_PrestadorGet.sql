
/****** Object:  StoredProcedure [dbo].[PrestadorGet]    Script Date: 03/03/2019 23:31:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER  PROCEDURE [dbo].[PrestadorGet]
(
@pCuit varchar(30) = null,
@pRazonSocial varchar(100) = null
)
AS
BEGIN
SELECT [Id]
      ,[Cuit]
      ,[Cue]
      ,[RazonSocial]
      ,[CBU]
      ,[Banco]
      ,[Sucursal]
      ,[TipoCuenta]
      ,[Direccion]
      ,[FechaAlta]
      ,[FechaUltimaModificacion]
  FROM [Prestadores]
  WHERE (RTRIM(LTRIM(Cuit)) = RTRIM(LTRIM(@pCuit)) OR @pCuit IS NULL)
  AND (UPPER(RazonSocial) LIKE '%' + LTRIM(RTRIM(UPPER(@pRazonSocial))) +'%' OR @pRazonSocial IS NULL)
  ORDER BY [RazonSocial], [Cuit]            
END


GO


