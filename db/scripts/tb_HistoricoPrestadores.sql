
CREATE TABLE [dbo].[HistoricoPrestadores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cuit] [varchar](30) NULL,
	[Cue] [varchar](30) NULL,
	[RazonSocial] [varchar](80) NULL,
	[CBU] [varchar](30) NULL,
	[Banco] [varchar](100) NULL,
	[Sucursal] [varchar](100) NULL,
	[TipoCuenta] [varchar](10) NULL,
	[Direccion] [varchar](100) NULL,
	[FechaAlta] [datetime] NULL,
	[FechaUltimaModificacion] [datetime] NULL,
	[accionRealizada] [varchar](50) NULL,
	[userId] [int] NULL,
	[ip] [varchar](40) NULL
) ON [PRIMARY]
GO

