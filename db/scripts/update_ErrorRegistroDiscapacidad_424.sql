INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (420
           ,'Solo podr� solicitarse 1 c�digo de pr�ctica de los comprendidos entre 1 y 84. Se rechazar�n todos los registros asociados a los c�digos incompatibles.'
           ,'Verificar c�digos de pr�cticas para igual CUIL igual periodo de prestaci�n')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (421
           ,'Si se solicita el c�digo de pr�ctica 87 no se podr� solicitar para igual CUIL, igual PERIODO, el c�digo de pr�ctica 89. Se rechazar�n todos los registros asociados a los c�digos incompatibles.'
           ,'Verificar c�digos de pr�cticas para igual CUIL igual periodo de prestaci�n')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (422
           ,'Si se solicita el c�digo de pr�ctica 87 o el c�digo de pr�ctica 89, no se podr� solicitar para igual CUIL, igual PERIODO los c�digos de pr�cticas comprendidos entre 1 y 7. Se rechazar�n todos los registros asociados a los c�digos incompatibles.'
           ,'Verificar c�digos de pr�cticas para igual CUIL igual periodo de prestaci�n')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (423
           ,'Si se solicita el c�digo de pr�ctica 90 o 91 no se podr� solicitar para igual CUIL, igual PERIODO los c�digos de pr�cticas 92 y 93 En este caso se aplicar� la prioridad a los registros de los c�digos 92 y 93 y se rechazar�n los registros de los c�digos 90 y 91. Excepto que en un periodo anterior se hayan enviado registros con los c�digos 90 o 91, en este caso se rechazar�n los registros de los c�digos 92 y 93.'
           ,'Verificar c�digos de pr�cticas para igual CUIL igual periodo de prestaci�n')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (424
           ,'Si se solicita el c�digo de pr�ctica 90 o 91 no se podr� solicitar para igual CUIL, igual PERIODO el c�digo de pr�ctica 85. En este caso se aplicar� la prioridad a los registros de los c�digos 90 y 91 y se rechazar�n los registros del c�digo 85. Excepto que en un periodo anterior se hayan enviado registros con el c�digo 85, en este caso se rechazar�n los registros de los c�digos 90 y 91.'
           ,'Verificar c�digos de pr�cticas para igual CUIL igual periodo de prestaci�n')
GO


