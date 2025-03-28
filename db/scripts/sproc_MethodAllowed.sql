
/****** Object:  StoredProcedure [dbo].[MethodAllowed]    Script Date: 04/09/2019 15:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[MethodAllowed] 
(
@pControllerName VARCHAR(100),
@pActionName VARCHAR(100),
@pToken      UNIQUEIDENTIFIER,
@pDetails    VARCHAR(2000)
)
AS
BEGIN

	DECLARE @vUserId INT ,@vResult int, @vIp VARCHAR(40) , @vUserName varchar(50), @vFlog bit, @vBitacoraFull bit

    SELECT @vIp = ip, 
           @vUserId = userid 
    FROM   applogindata 
    WHERE  userid = (SELECT userid 
                     FROM   apptoken 
                     WHERE  authtoken = @pToken) 

	SELECT @vResult = 1 
	FROM   AppProfilePermissions app
		   INNER JOIN AppPermissions ap 
				   ON ap.profileId = app.profileId
		   INNER JOIN AppObject ao 
				   ON ao.id = app.appObjectId and ao.unsuscribeDate is null
		   INNER JOIN AppAction aa 
				   ON aa.id = app.appActionId and aa.unsuscribeDate is null
	WHERE ap.userId = @vUserId 
	  AND appObject = @pControllerName 
	  AND AppAction = @pActionName

IF ISNULL(@vResult,0) > 0
BEGIN
	SELECT @vUserName = userName FROM AppUser WHERE id=@vUserId 
	SELECT @vFLog=isnull(flog,0) FROM AppAction WHERE AppAction= @pActionName
	SELECT @vBitacoraFull= bitacoraFull FROM GeneralConfiguration WHERE unsuscribeDate is null
	IF @vFlog = 1 or @vBitacoraFull = 1
	BEGIN
		INSERT INTO Bitacora Select @vIp,@vUserId,@vUserName,@pControllerName,@pActionName,@pDetails,getdate()
	END
END
SELECT ISNULL(@vResult,0) result
END

GO
