
CREATE TABLE [dbo].[HistoricoDatosAfiliado](
	[afiliadoId] [int] NOT NULL,
	[codigoCertDisc] [varchar](40) NULL,
	[vencimientoCertDisc] [datetime] NOT NULL,
	[accionRealizada] [varchar](50) NULL,
	[userId] [int] NULL,
	[Ip] [varchar](40) NULL,
	[fechaUltimaModificacion][datetime] NULL
) ON [PRIMARY]
GO

