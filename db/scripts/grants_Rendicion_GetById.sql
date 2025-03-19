
INSERT INTO [dbo].[AppObject]
           ([appObject])
     VALUES
           ('Rendicion')
GO

--Id del AppObject insertado
DECLARE @RendicionAppObjectId INT
SELECT @RendicionAppObjectId = id FROM [dbo].[AppObject]
WHERE AppObject = 'Rendicion';
INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@RendicionAppObjectId
           ,'GetById'
           ,'Obtener Rendicion por Id')

--Id de la AppAction insertada
DECLARE @NewAppActionId INT
SELECT @NewAppActionId = SCOPE_IDENTITY();
INSERT INTO [dbo].[AppProfilePermissions]
           ([profileId]
           ,[appObjectId]
           ,[appActionId])
     VALUES
           (2
           ,@RendicionAppObjectId
           ,@NewAppActionId)
GO




