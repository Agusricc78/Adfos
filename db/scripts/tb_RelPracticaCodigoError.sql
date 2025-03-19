/****** Object:  Table [dbo].[RelPracticaCodigoError]    Script Date: 06/04/2020 13:10:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RelPracticaCodigoError](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoPractica] [int] NULL,
	[codigoError] [int] NULL
) ON [PRIMARY]
GO
