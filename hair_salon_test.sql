USE [hair_salon_test]
GO
/****** Object:  Table [dbo].[appointments]    Script Date: 7/15/2016 4:48:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[appointments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_id] [int] NULL,
	[stylist_id] [int] NULL,
	[date_and_time] [datetime] NULL,
	[duration] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[clients]    Script Date: 7/15/2016 4:48:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[stylist_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stylists]    Script Date: 7/15/2016 4:48:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[appointments] ON 

INSERT [dbo].[appointments] ([id], [client_id], [stylist_id], [date_and_time], [duration]) VALUES (135, 1, 115, CAST(N'2017-05-21T12:00:00.000' AS DateTime), 15)
INSERT [dbo].[appointments] ([id], [client_id], [stylist_id], [date_and_time], [duration]) VALUES (136, 1, 115, CAST(N'2016-11-21T14:00:00.000' AS DateTime), 30)
SET IDENTITY_INSERT [dbo].[appointments] OFF
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (143, N'Pete', 116)
INSERT [dbo].[clients] ([id], [name], [stylist_id]) VALUES (144, N'Prasad', 116)
SET IDENTITY_INSERT [dbo].[clients] OFF
