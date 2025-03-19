INSERT INTO [dbo].[ErrorRegistroDiscapacidad]
           ([codigo]
           ,[descripcion]
           ,[verificacion])
     VALUES
           (356
           ,'Mes y a�o del vencimiento del certificado podr� ser como m�ximo 5 meses anterior al periodo de prestaci�n.'
           ,'Corroborar la fecha del vencimiento del certificado.')
GO

UPDATE [dbo].[ErrorRegistroDiscapacidad]
   SET [descripcion] = 'Debe ser informado y m�ximo 40 caracteres.'
      ,[verificacion] = 'Verificar si fue informado y cantidad de caracteres.'
 WHERE [codigo] = 306
GO

UPDATE [dbo].[ErrorRegistroDiscapacidad]
   SET [descripcion] = 'Debe ser informado, tiene que ser de 10 caracteres, formato de fecha valido y permitida.'
      ,[verificacion] = 'Verificar si fue informado, cantidad de caracteres y fecha enviada.'
 WHERE [codigo] = 307
GO


