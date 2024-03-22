USE [WildDeer]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 3/22/2024 1:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[FirstName] [nvarchar](15) NOT NULL,
	[LastName] [nvarchar](15) NOT NULL,
	[Birthday] [datetime] NULL,
	[JoinDate] [datetime] NOT NULL,
	[Adress] [nvarchar](75) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[HashedPassword] [nvarchar](max) NOT NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[UserID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
