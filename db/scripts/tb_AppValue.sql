/****** Object:  Table [dbo].[AppValue]    Script Date: 07/25/2019 17:11:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AppValue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[description] [varchar](200) NULL,
	[minValue] [decimal](8, 2) NULL,
	[maxValue] [decimal](8, 2) NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_AppValue] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


