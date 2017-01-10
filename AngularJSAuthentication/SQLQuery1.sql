GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 07/11/2016 21:51:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
    [RatingsId] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
    [BookId] [numeric](10, 0) NOT NULL,
    [sumofRatings] [float] NULL,
	[numberofRatings] [int] NULL,
    
CONSTRAINT [PK_Ratings_Book] PRIMARY KEY CLUSTERED 
(
	[RatingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Book]

