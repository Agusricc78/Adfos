
/****** Object:  StoredProcedure [dbo].[ProvinciaGetById]    Script Date: 04/02/2019 15:24:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EstadoGetById]
@pId int
AS
BEGIN 
SELECT [id]
      ,[Descripcion]
  FROM [dbo].[EstadoProcesoDiscapacidad]
  WHERE Id = @pId

 END

