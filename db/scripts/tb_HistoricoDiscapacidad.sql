
CREATE TABLE [dbo].[HistoricoDiscapacidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[afiliadoid] [int] NOT NULL,
	[periodo] [datetime] NOT NULL,
	[cuit] [varchar](30) NULL,
	[tipocomprobanteid] [int] NULL,
	[tipoemisionid] [int] NULL,
	[fechaemision] [datetime] NULL,
	[caecai] [varchar](14) NULL,
	[puntoVenta] [int] NULL,
	[numeroComprobante] [int] NULL,
	[importe] [decimal](8, 2) NULL,
	[cantidad] [int] NULL,
	[codigopractica] [int] NULL,
	[provinciaId] [int] NULL,
	[esdependiente] [bit] NULL,
	[procesado] [int] NULL,
	[fechaAlta] [datetime] NULL,
	[fechaUltimaModificacion] [datetime] NULL,
	[codigoErrorSSS] [int] NULL,
	[vencimientoCertDisc] [datetime] NULL,
	[codigoCertDisc] [varchar](40) NULL,
	[accionRealizada] [varchar](50) NULL,
	[userId] [int] NULL,
	[ip] [varchar](40) NULL
) ON [PRIMARY]
GO

