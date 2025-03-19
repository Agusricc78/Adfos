
UPDATE [dbo].[EstadoProcesoDiscapacidad]
   SET [Descripcion] = 'Aceptado'
 WHERE [Id] = 3
GO

--INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
--           ([id]
--           ,[Descripcion])
--     VALUES
--           (7
--           ,'Anulado')
--GO

--INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
--           ([id]
--           ,[Descripcion])
--     VALUES
--           (8
--           ,'Liquidado')
--GO

UPDATE [dbo].[EstadoProcesoDiscapacidad]
   SET [Descripcion] = 'En Liquidación'
 WHERE [Id] = 6
GO

INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
           ([id]
           ,[Descripcion])
     VALUES
           (99
           ,'Histórico')
GO