--Id del AppObject Estados
DECLARE @NewAppObjectId INT
SELECT @NewAppObjectId = Id
FROM [dbo].[AppObject]
WHERE appObject = 'Estados';

INSERT INTO [dbo].[AppAction]
           ([appObjectId]
           ,[AppAction]
           ,[AppActionDescription])
     VALUES
           (@NewAppObjectId
           ,'GetTotales'
           ,'Obtiene el total de Registros Discapacidad por Estados')

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




