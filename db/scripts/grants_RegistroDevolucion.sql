
INSERT INTO [dbo].[AppObject]
           ([appObject])
     VALUES
           ('RegistroDevolucion')
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
           ,'Lista de registros devolucion')

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


INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@NewAppObjectId
           ,'GetById'
           ,'Obtiene registro devolucion por Id')

--Id de la AppAction insertada
SELECT @NewAppActionId = SCOPE_IDENTITY();
INSERT INTO [dbo].[AppProfilePermissions]
           ([profileId]
           ,[appObjectId]
           ,[appActionId])
     VALUES
           (2
           ,@NewAppObjectId
           ,@NewAppActionId)


INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@NewAppObjectId
           ,'Put'
           ,'Actualización de registro devolucion')

--Id de la AppAction insertada
SELECT @NewAppActionId = SCOPE_IDENTITY();
INSERT INTO [dbo].[AppProfilePermissions]
           ([profileId]
           ,[appObjectId]
           ,[appActionId])
     VALUES
           (2
           ,@NewAppObjectId
           ,@NewAppActionId)


INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@NewAppObjectId
           ,'GetValidate'
           ,'Validacion Registros Devolucions')

--Id de la AppAction insertada
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
