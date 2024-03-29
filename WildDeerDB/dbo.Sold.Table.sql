USE [WildDeer]
GO
/****** Object:  Table [dbo].[Sold]    Script Date: 3/22/2024 1:55:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sold](
	[ProductID] [int] NOT NULL,
	[SellerID] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[BuyingDate] [date] NOT NULL,
	[SellInfoID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Sold] PRIMARY KEY CLUSTERED 
(
	[SellInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
