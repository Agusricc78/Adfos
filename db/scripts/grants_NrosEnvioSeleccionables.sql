
INSERT INTO [dbo].[AppObject]
           ([appObject])
     VALUES
           ('NrosEnvioSeleccionables')
GO

--Id del AppObject insertado
DECLARE @NewAppObjectId INT
SELECT @NewAppObjectId = SCOPE_IDENTITY();
INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@NewAppObjectId
           ,'GetList'
           ,'Obtiene el listado de Nros Envio AFIP cargados')

--Id de la AppAction insertada
DECLARE @NewAppActionId INT
SELECT @NewAppActionId = SCOPE_IDENTITY();
INSERT INTO [dbo].[AppProfilePermissions]
           ([profileId]
           ,[appObjectId]
           ,[appActionId])
     VALUES
           (2
           ,@NewAppObjectId
           ,@NewAppActionId)
GO




