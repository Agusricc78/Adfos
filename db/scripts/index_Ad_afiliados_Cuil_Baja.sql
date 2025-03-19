
/****** Object:  Index [Ad_afiliados_Cuil_Baja]    Script Date: 03/08/2019 11:45:48 ******/
CREATE NONCLUSTERED INDEX [Ad_afiliados_Cuil_Baja] ON [dbo].[Ad_afiliados] 
(
	[Cuil] ASC,
	[Baja] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


