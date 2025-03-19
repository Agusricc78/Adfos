
--Nuevo Estado Liquidado Manual, contiene todos los Reintegros Liquidados Manualmente
INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
           ([id]
           ,[Descripcion])
     VALUES
           (9
           ,'Liquidado Manual')
GO

--Nuevo Estado Anulado Liquidado, contiene todos los Reintegros Liquidados que estaban Anulados
INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
           ([id]
           ,[Descripcion])
     VALUES
           (10
           ,'Anulado Liquidado')
GO
