INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (420
           ,'Solo podrá solicitarse 1 código de práctica de los comprendidos entre 1 y 84. Se rechazarán todos los registros asociados a los códigos incompatibles.'
           ,'Verificar códigos de prácticas para igual CUIL igual periodo de prestación')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (421
           ,'Si se solicita el código de práctica 87 no se podrá solicitar para igual CUIL, igual PERIODO, el código de práctica 89. Se rechazarán todos los registros asociados a los códigos incompatibles.'
           ,'Verificar códigos de prácticas para igual CUIL igual periodo de prestación')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (422
           ,'Si se solicita el código de práctica 87 o el código de práctica 89, no se podrá solicitar para igual CUIL, igual PERIODO los códigos de prácticas comprendidos entre 1 y 7. Se rechazarán todos los registros asociados a los códigos incompatibles.'
           ,'Verificar códigos de prácticas para igual CUIL igual periodo de prestación')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (423
           ,'Si se solicita el código de práctica 90 o 91 no se podrá solicitar para igual CUIL, igual PERIODO los códigos de prácticas 92 y 93 En este caso se aplicará la prioridad a los registros de los códigos 92 y 93 y se rechazarán los registros de los códigos 90 y 91. Excepto que en un periodo anterior se hayan enviado registros con los códigos 90 o 91, en este caso se rechazarán los registros de los códigos 92 y 93.'
           ,'Verificar códigos de prácticas para igual CUIL igual periodo de prestación')
GO

INSERT INTO [final].[dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (424
           ,'Si se solicita el código de práctica 90 o 91 no se podrá solicitar para igual CUIL, igual PERIODO el código de práctica 85. En este caso se aplicará la prioridad a los registros de los códigos 90 y 91 y se rechazarán los registros del código 85. Excepto que en un periodo anterior se hayan enviado registros con el código 85, en este caso se rechazarán los registros de los códigos 90 y 91.'
           ,'Verificar códigos de prácticas para igual CUIL igual periodo de prestación')
GO


