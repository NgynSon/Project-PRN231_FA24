CREATE DATABASE [Diary]
USE [Diary]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 7/15/2024 11:57:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment_Content] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interact]    Script Date: 7/15/2024 11:57:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interact](
	[UserId] [int] NULL,
	[PostId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Interact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 7/15/2024 11:57:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Post_Content] [nvarchar](max) NULL,
	[Date] [date] NULL,
	[IsPublic] [bit] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reply]    Script Date: 7/15/2024 11:57:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Reply_Content] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[CommentId] [int] NULL,
 CONSTRAINT [PK_Reply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/15/2024 11:57:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[DOB] [date] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Interact]  WITH CHECK ADD  CONSTRAINT [FK_Interact_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([Id])
GO
ALTER TABLE [dbo].[Interact] CHECK CONSTRAINT [FK_Interact_Post]
GO
ALTER TABLE [dbo].[Interact]  WITH CHECK ADD  CONSTRAINT [FK_Interact_User1] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Interact] CHECK CONSTRAINT [FK_Interact_User1]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Comment] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comment] ([Id])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Comment]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_User]
GO

INSERT INTO [dbo].[User] (Username, Password, Name, Gender, DOB) VALUES
('john_doe', 'password123', 'John Doe', 1, '1990-05-15'),
('jane_smith', 'password456', 'Jane Smith', 0, '1985-08-20'),
('alex_jones', 'password789', 'Alex Jones', 1, '1992-12-01'),
('emily_white', 'password111', 'Emily White', 0, '1995-04-30'),
('michael_brown', 'password222', 'Michael Brown', 1, '1988-03-22');
GO
-- Insert sample data into the [Post] table
INSERT INTO [dbo].[Post] (Post_Content, Date, IsPublic, UserId) VALUES
('Today was a good day!', '2024-07-01', 1, 1),
('Just finished a great book.', '2024-07-02', 0, 2),
('Feeling grateful for my friends.', '2024-07-03', 1, 3),
('Started learning a new language!', '2024-07-04', 1, 4),
('Had an amazing time hiking.', '2024-07-05', 0, 5);
GO
-- Insert sample data into the [Comment] table
INSERT INTO [dbo].[Comment] (Comment_Content, UserId, PostId) VALUES
('That sounds wonderful!', 2, 1),
('Congratulations on finishing!', 3, 2),
('It’s great to have supportive friends!', 4, 3),
('Learning languages is so rewarding!', 5, 4),
('I love hiking too!', 1, 5);
GO
-- Insert sample data into the [Interact] table
INSERT INTO [dbo].[Interact] (UserId, PostId) VALUES
(1, 3), -- John Doe liked Alex's post
(2, 1), -- Jane liked John's post
(3, 4), -- Alex liked Emily's post
(4, 5), -- Emily liked Michael's post
(5, 2); -- Michael liked Jane's post
GO
-- Insert sample data into the [Reply] table
INSERT INTO [dbo].[Reply] (Reply_Content, UserId, CommentId) VALUES
('Thank you, Jane!', 1, 1),
('Thanks, Alex!', 2, 2),
('Couldn’t agree more!', 3, 3),
('Absolutely! It’s so rewarding.', 4, 4),
('Maybe we could go hiking together!', 5, 5);
GO