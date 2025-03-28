/****** Object:  StoredProcedure [dbo].[CabeceraMovTransfIns]    Script Date: 10/03/2020 15:02:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CabeceraMovTransfIns] (@pNombre        VARCHAR(100), 
                                      @pDescripcion   VARCHAR(100), 
                                      @pDatos         VARBINARY (max), 
                                      @pTipoRespuesta INT, 
                                      @pToken         UNIQUEIDENTIFIER) 
AS 
  BEGIN 
      DECLARE @vUserId AS INT 
      DECLARE @vIp AS VARCHAR(40) 
	  DECLARE @vAfiliadoId AS INT
      SELECT @vIp = ip, 
             @vUserId = userid 
      FROM   applogindata 
      WHERE  userid = (SELECT userid 
                       FROM   apptoken 
                       WHERE  authtoken = @pToken) 

                  INSERT INTO CabeceraMovTransf
                        (nombre, 
                         datos, 
                         tiporespuesta, 
                         activo, 
                         userid, 
                         ip) 
            VALUES      (@pNombre, 
                         @pDatos, 
                         @pTipoRespuesta, 
                         1, 
                         @vUserId, 
                         @vIp) 

            SELECT Scope_identity() 
   
  END 
