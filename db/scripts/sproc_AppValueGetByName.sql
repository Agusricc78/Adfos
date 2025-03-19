/****** Object:  StoredProcedure [dbo].[[AppValueGetByName]]    Script Date: 07/25/2019 18:19:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AppValueGetByName]
@pName varchar(100)
AS
BEGIN 
	SELECT [id]
		  ,[name]
		  ,[description]
		  ,[minValue]
		  ,[maxValue]
		  ,[active]
	  FROM [dbo].[AppValue]
	  WHERE name = @pName		 
 END

