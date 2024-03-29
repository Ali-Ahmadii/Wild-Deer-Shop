USE [WildDeer]
GO
/****** Object:  Table [dbo].[ExtraImages]    Script Date: 3/22/2024 1:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraImages](
	[ProductID] [int] NOT NULL,
	[IMG1] [nvarchar](max) NULL,
	[IMG2] [nvarchar](max) NULL,
	[IMG3] [nvarchar](max) NULL,
	[IMG4] [nvarchar](max) NULL,
	[IMG5] [nvarchar](max) NULL,
	[IMG6] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExtraImages] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
