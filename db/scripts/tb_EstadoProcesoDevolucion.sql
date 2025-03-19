/****** Object:  Table [dbo].[EstadoProcesoDiscapacidad]    Script Date: 25/03/2020 19:54:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[EstadoProcesoDevolucion](
	[id] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

INSERT INTO [dbo].[EstadoProcesoDevolucion]
           ([id]
           ,[Descripcion])
     VALUES
           (0
           ,'Sin Presentar')
GO

INSERT INTO [dbo].[EstadoProcesoDevolucion]
           ([id]
           ,[Descripcion])
     VALUES
           (1
           ,'Generado')
GO

INSERT INTO [dbo].[EstadoProcesoDevolucion]
           ([id]
           ,[Descripcion])
     VALUES
           (3
           ,'Aceptado')
GO

INSERT INTO [dbo].[EstadoProcesoDevolucion]
           ([id]
           ,[Descripcion])
     VALUES
           (4
           ,'Error')
GO

INSERT INTO [dbo].[EstadoProcesoDevolucion]
           ([id]
           ,[Descripcion])
     VALUES
           (5
           ,'Corregido')
GO

