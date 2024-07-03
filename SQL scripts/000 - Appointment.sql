USE [booking]
GO

/****** Object:  Table [dbo].[Appointment]    Script Date: 01/07/2024 17:41:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
DROP TABLE [dbo].[Appointment]
GO

/****** Object:  Table [dbo].[Appointment]    Script Date: 01/07/2024 17:41:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Appointment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Date] [datetime] NULL,
	[PhoneNumber] [nvarchar](13) NULL,
	[Age] [int] NULL,
	[AnimalType] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


