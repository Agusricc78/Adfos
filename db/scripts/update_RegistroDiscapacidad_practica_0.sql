
-- Se pasan a INACTIVO los registros que tenian CodigoPractica = 0
SELECT * FROM RegistroDiscapacidad
WHERE codigopractica = 0
AND Activo = 1

UPDATE [dbo].RegistroDiscapacidad
   SET Activo = 0
 WHERE codigopractica = 0

SELECT * FROM RegistroDiscapacidad
WHERE codigopractica = 0
AND Activo = 1
