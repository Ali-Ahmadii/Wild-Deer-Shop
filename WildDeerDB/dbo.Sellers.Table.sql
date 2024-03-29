USE [WildDeer]
GO
/****** Object:  Table [dbo].[Sellers]    Script Date: 3/22/2024 1:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sellers](
	[OwnerName] [nvarchar](25) NOT NULL,
	[CompanyName] [nvarchar](30) NOT NULL,
	[Adress] [nvarchar](75) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[JoinedDate] [datetime] NOT NULL,
	[MostProductsEra] [nvarchar](15) NULL,
	[Username] [nvarchar](20) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[SellerID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Sellers] PRIMARY KEY CLUSTERED 
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
