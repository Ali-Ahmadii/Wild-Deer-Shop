USE [WildDeer]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 3/22/2024 1:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Username] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[ProductID] [int] NOT NULL,
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[WriterID] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
