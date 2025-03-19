
/****** Object:  Table [dbo].[DatosAfiliado]    Script Date: 03/23/2019 11:59:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DatosAfiliado](
	[afiliadoId] [int] NOT NULL,
	[codigoCertDisc] [varchar](40) NULL,
	[vencimientoCertDisc] [datetime] NOT NULL,
	accionRealizada varchar(50),
	userId int,
	Ip varchar(40),
	fechaUltimaModificacion Datetime
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
