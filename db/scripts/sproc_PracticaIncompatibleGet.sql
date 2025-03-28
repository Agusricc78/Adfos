
/****** Object:  StoredProcedure [dbo].[PracticaIncompatibleGet]    Script Date: 06/04/2020 13:02:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PracticaIncompatibleGet] @pAfiliadoid int, 
                                                @pCodigo  INT, 
                                                @pPeriodo DATETIME 
AS 
  BEGIN 
      DECLARE @retvalue AS VARCHAR(500) 

      SET @retvalue = Stuff((SELECT ', ' 
                                    + Replicate('0', ( 4 -Len(Cast(Isnull(puntoventa,0) AS VARCHAR)))) 
                                    + Cast(Isnull(puntoventa, 0) AS VARCHAR) 
                                    + '-' 
                                    + Replicate('0', ( 8 -Len(Cast(Isnull(numerocomprobante, 0) AS VARCHAR(8))))) 
                                    + Cast(Isnull(numerocomprobante, 0) AS VARCHAR(8)) 
                             FROM   registrodiscapacidad 
                             WHERE  afiliadoid = @pAfiliadoid 
                                    AND procesado NOT IN ( 4, 7, 10, 99 ) 
                                    AND periodo = @pPeriodo 
                                    AND codigopractica NOT IN 
                                        (SELECT codigoincompatible 
                                         FROM nomencladorpracticasincompatibilidad 
                                         WHERE  codigo = @pCodigo) 
                             FOR xml path('')), 1, 2, '') 
      if isnull(@retvalue,'')=''
	  begin 
		SELECT 0 as codigo,Isnull(@retvalue, '') as respuesta
	  end
	  else
	  begin
	    select 1 as codigo, descripcion + ', revisar facturas:' + @retvalue  as respuesta from ErrorRegistroDiscapacidad ed
		inner join RelPracticaCodigoError rpe on rpe.codigoError = ed.codigo 
		where rpe.codigoPractica = @pCodigo 
	  end
  END 
GO

