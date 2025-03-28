
CREATE TABLE [dbo].[Bitacora](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ip] [varchar](40) NULL,
	[userId] [int] NULL,
	[userName] [varchar](50) NULL,
	[controler] [varchar](100) NULL,
	[accion] [varchar](100) NULL,
	[details] [varchar](2000) NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GeneralConfiguration](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bitacoraFull] [bit] NULL,
	[unSuscribeDate] [datetime] NULL,
 CONSTRAINT [PK_GeneralConfiguration] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO GeneralConfiguration SELECT 0,NULL
GO 

ALTER TABLE [dbo].[GeneralConfiguration] ADD  DEFAULT ((0)) FOR [bitacoraFull]
GO

ALTER TABLE appAction ADD Flog BIT NULL
GO

UPDATE AppAction SET flog = 1 WHERE AppAction in ('Insert','Update','Delete','SelfUpdate','Post','Put','Delete')
GO