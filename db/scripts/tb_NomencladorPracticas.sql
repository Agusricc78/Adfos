
ALTER TABLE dbo.NomencladorPracticas ADD
	dependencia BIT NOT NULL DEFAULT 0

GO

--UPDATE [dbo].[NomencladorPracticas]
--   SET dependencia = 0
--GO

-- Se coloca dependencia en practicas del 7 al 12 inclusive y la 96
UPDATE [dbo].[NomencladorPracticas]
   SET dependencia = 1
   WHERE ((Codigo >= 7 AND Codigo <= 12) OR Codigo = 96)
GO