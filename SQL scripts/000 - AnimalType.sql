USE [booking]
GO

/****** Object:  Table [dbo].[AnimalType]    Script Date: 01/07/2024 15:21:07 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AnimalType]') AND type in (N'U'))
DROP TABLE [dbo].[AnimalType]
GO

/****** Object:  Table [dbo].[AnimalType]    Script Date: 01/07/2024 15:21:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AnimalType](
	[ID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[AppointmentLength] [bigint] NULL
) ON [PRIMARY]
GO


