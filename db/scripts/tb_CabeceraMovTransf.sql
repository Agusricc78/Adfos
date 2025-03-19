/****** Object:  Table [dbo].[CabeceraMovTransf]    Script Date: 10/03/2020 15:03:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CabeceraMovTransf](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Datos] [varbinary](max) NULL,
	[TipoRespuesta] [int] NULL,
	[userId] [int] NULL,
	[ip] [varchar](40) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_CabeceraMovTransf] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


