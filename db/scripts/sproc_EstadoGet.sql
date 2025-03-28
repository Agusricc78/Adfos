
/****** Object:  StoredProcedure [dbo].[ProvinciaGet]    Script Date: 04/02/2019 15:24:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EstadoGet]
AS
BEGIN 
	SELECT [id]
		  ,[Descripcion]
	FROM [dbo].[EstadoProcesoDiscapacidad]
	WHERE id NOT IN (9, 99)
	OR id IN (SELECT epd.id
			FROM [dbo].[EstadoProcesoDiscapacidad] epd
			RIGHT JOIN [dbo].[RegistroDiscapacidad] rd
			ON epd.id = rd.procesado
			WHERE rd.Activo = 1
			GROUP BY epd.id)
	ORDER BY [id]
END

