
--Nuevo Estado Historico que contiene todos los registros que
--quedaron EN LIQUIDACION luego de los cambios
INSERT INTO [dbo].[EstadoProcesoDiscapacidad]
           ([id]
           ,[Descripcion])
     VALUES
           (99
           ,'Histórico')
GO

UPDATE [dbo].[RegistroDiscapacidad]
   SET [procesado] = 99
 WHERE periodo < '2019-01-01'
 AND procesado IN (0, 2, 6)
GO


SELECT DISTINCT([procesado]) Estado, epd.Descripcion, COUNT(rd.Id) Total
FROM [final1_dev].[dbo].[RegistroDiscapacidad] rd
LEFT JOIN final1_dev.dbo.EstadoProcesoDiscapacidad epd
ON rd.procesado = epd.id
WHERE periodo < '2019-01-01'
GROUP BY procesado, epd.Descripcion
ORDER BY procesado

