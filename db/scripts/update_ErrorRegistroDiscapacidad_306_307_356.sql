INSERT INTO [dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (356
           ,'Mes y año del vencimiento del certificado podrá ser como máximo 5 meses anterior al periodo de prestación.'
           ,'Corroborar la fecha del vencimiento del certificado.')
GO

UPDATE [dbo].[ErrorRegistroDiscapacidad]
   SET [descripcion] = 'Debe ser informado y máximo 40 caracteres.'
      ,[verificacion] = 'Verificar si fue informado y cantidad de caracteres.'
 WHERE [codigo] = 306
GO

UPDATE [dbo].[ErrorRegistroDiscapacidad]
   SET [descripcion] = 'Debe ser informado, tiene que ser de 10 caracteres, formato de fecha valido y permitida.'
      ,[verificacion] = 'Verificar si fue informado, cantidad de caracteres y fecha enviada.'
 WHERE [codigo] = 307
GO


