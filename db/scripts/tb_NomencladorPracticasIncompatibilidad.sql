/****** Object:  Table [dbo].[NomencladorPracticasIncompatibilidad]    Script Date: 06/04/2020 13:08:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NomencladorPracticasIncompatibilidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [int] NULL,
	[codigoIncompatible] [int] NULL
) ON [PRIMARY]
GO
