/****** Object:  StoredProcedure [dbo].[NomencladorPracticasGetbyId]    Script Date: 07/29/2019 17:20:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[NomencladorPracticasGetbyId] (@pCodigo int)
as
begin
select codigo,modulo,maximo,minimo,cueFlag, dependencia from NomencladorPracticas where codigo = @pCodigo
end


