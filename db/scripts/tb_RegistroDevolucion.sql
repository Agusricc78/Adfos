/****** Object:  Table [dbo].[RegistroDevolucion]    Script Date: 18/02/2020 13:53:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RegistroDevolucion](
	[CLAVE_RENDICION] [nvarchar](255) NULL,
	[RNOS] [nvarchar](255) NULL,
	[TIPO_ARCHIVO] [nvarchar](255) NULL,
	[PERIODO_PRESENTACION] [datetime] NULL,
	[PERIODO_PRESTACION] [datetime] NULL,
	[CUIL] [nvarchar](255) NULL,
	[CODIGO_PRACTICA] [int] NULL,
	[IMPORTE_SUBSIDIADO] [decimal](8, 2) NULL,
	[IMPORTE_SOLICITADO] [decimal](8, 2) NULL,
	[NRO_ENVIO_AFIP] [int] NULL,
	[CUIT_DEL_CBU] [nvarchar](255) NULL,
	[CBU] [nvarchar](255) NULL,
	[FECHA_TRANSFERENCIA_I] [datetime] NULL,
	[IMPORTE_APLICADO_SSS] [decimal](8, 2) NULL,
	[FONDOS_PROPIOS_OTRA_CUENTA] [decimal](8, 2) NULL,
	[PROCESADO] [int] NULL,
	[CODIGO_ERROR_SSS] [int] NULL
) ON [PRIMARY]

GO


