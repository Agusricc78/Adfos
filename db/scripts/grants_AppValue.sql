
INSERT INTO [dbo].[AppObject]
           ([appObject])
     VALUES
           ('AppValue')
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
           ,'GetByName'
           ,'Obtiene segun el nombre un valor de configuracion')

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




