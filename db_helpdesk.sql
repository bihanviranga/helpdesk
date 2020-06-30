USE [master]
GO
/****** Object:  Database [db_helpdesk]    Script Date: 6/8/2020 11:42:43 PM ******/
CREATE DATABASE [db_helpdesk]
GO

ALTER DATABASE [db_helpdesk] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
	EXEC [db_helpdesk].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_helpdesk] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [db_helpdesk] SET ANSI_NULLS OFF
GO
ALTER DATABASE [db_helpdesk] SET ANSI_PADDING OFF
GO
ALTER DATABASE [db_helpdesk] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [db_helpdesk] SET ARITHABORT OFF
GO
ALTER DATABASE [db_helpdesk] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [db_helpdesk] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [db_helpdesk] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [db_helpdesk] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [db_helpdesk] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [db_helpdesk] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [db_helpdesk] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [db_helpdesk] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [db_helpdesk] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [db_helpdesk] SET  DISABLE_BROKER
GO
ALTER DATABASE [db_helpdesk] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [db_helpdesk] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [db_helpdesk] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [db_helpdesk] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [db_helpdesk] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [db_helpdesk] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [db_helpdesk] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [db_helpdesk] SET RECOVERY SIMPLE
GO
ALTER DATABASE [db_helpdesk] SET  MULTI_USER
GO
ALTER DATABASE [db_helpdesk] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [db_helpdesk] SET DB_CHAINING OFF
GO
ALTER DATABASE [db_helpdesk] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO
ALTER DATABASE [db_helpdesk] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO
ALTER DATABASE [db_helpdesk] SET DELAYED_DURABILITY = DISABLED
GO
ALTER DATABASE [db_helpdesk] SET QUERY_STORE = OFF
GO
USE [db_helpdesk]
GO
/****** Object:  Table [dbo].[Tkt_Article]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Article]
(
	[ArticleID] [char](36) NOT NULL,
	[ProductID] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](20) NOT NULL,
	[AcceptedBy] [nvarchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[AcceptedDate] [datetime] NULL,
	[ArticleTitle] [nvarchar](max) NOT NULL,
	[ArticleContent] [nvarchar](max) NOT NULL,
	[LastEditedDate] [datetime] NOT NULL,
	[LastEditedBy] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Tkt_Article] PRIMARY KEY CLUSTERED
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Category]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Category]
(
	[CategoryID] [nvarchar](20) NOT NULL,
	[CompanyID] [char](36) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	CONSTRAINT [PK_Tkt_Category] PRIMARY KEY CLUSTERED
(
	[CategoryID] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Company]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Company]
(
	[CompanyID] [char](36) NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	CONSTRAINT [PK_Tkt_Company] PRIMARY KEY CLUSTERED
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_CompanyBrand]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_CompanyBrand]
(
	[BrandID] [nvarchar](20) NOT NULL,
	[CompanyID] [char](36) NOT NULL,
	[BrandName] [nvarchar](max) NULL,
	CONSTRAINT [PK_Tkt_CompanyBrand] PRIMARY KEY CLUSTERED
(
	[BrandID] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Conversation]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Conversation]
(
	[CvID] [char](36) NOT NULL,
	[TicketID] [char](36) NOT NULL,
	[CvSender] [nvarchar](20) NOT NULL,
	[CvSenderType] [nvarchar](20) NOT NULL,
	[CvSendDate] [datetime] NOT NULL,
	[CvContent] [nvarchar](max) NOT NULL,
	CONSTRAINT [PK_Tkt_Conversation] PRIMARY KEY CLUSTERED
(
	[CvID] ASC,
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Module]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Module]
(
	[ModuleID] [nvarchar](20) NOT NULL,
	[CompanyID] [char](36) NOT NULL,
	[ModuleName] [nvarchar](max) NOT NULL,
	CONSTRAINT [PK_Tkt_Module] PRIMARY KEY CLUSTERED
(
	[ModuleID] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Notification]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Notification]
(
	[NotifID] [char](36) NOT NULL,
	[TicketID] [char](36) NOT NULL,
	[NotifContent] [nvarchar](max) NOT NULL,
	[NotifUser] [nvarchar](20) NOT NULL,
	[NotifRead] [bit] NOT NULL,
	[NotifURL] [nvarchar](max) NOT NULL,
	[NotifDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Tkt_Notification] PRIMARY KEY CLUSTERED
(
	[NotifID] ASC,
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_Product]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_Product]
(
	[ProductID] [nvarchar](50) NOT NULL,
	[CompanyID] [char](36) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	CONSTRAINT [PK_Tkt_Product] PRIMARY KEY CLUSTERED
(
	[ProductID] ASC,
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_ResTemplate]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_ResTemplate]
(
	[TemplateID] [int] IDENTITY(1,1),
	[TemplateName] [nvarchar](max) NOT NULL,
	[TemplateDescription] [nvarchar](max) NULL,
	[TemplateContent] [nvarchar](max) NOT NULL,
	[TemplateAddedBy] [nvarchar](20) NOT NULL,
	[TemplateAddedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_Tkt_ResTemplate] PRIMARY KEY CLUSTERED
(
	[TemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_TicketMaster]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_TicketMaster]
(
	[TicketID] [char](36) NOT NULL,
	[CompanyID] [char](36) NOT NULL,
	[ProductID] [nvarchar](50) NOT NULL,
	[ModuleID] [nvarchar](20) NOT NULL,
	[BrandID] [nvarchar](20) NULL,
	[CategoryID] [nvarchar](20) NOT NULL,
	[TktSubject] [nvarchar](max) NOT NULL,
	[TktContent] [nvarchar](max) NOT NULL,
	[TktStatus] [nvarchar](20) NULL,
	[TktPriority] [nvarchar](20) NULL,
	[TktCreatedBy] [nvarchar](20) NOT NULL,
	[TktAssignedTo] [nvarchar](20) NULL,
	[TktCreatedDate] [datetime] NOT NULL,
	[TktClosedDate] [datetime] NULL,
	[TktReopenedDate] [datetime] NULL,
	[TktFirstResponseDate] [datetime] NULL,
	[TktAttachment] [nvarchar](max) NULL,
	[TktRating] [nvarchar](20) NULL,
	CONSTRAINT [PK_Tkt_TicketMaster] PRIMARY KEY CLUSTERED
(
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_TicketOperator]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_TicketOperator]
(
	[TktOperator] [nvarchar](20) NOT NULL,
	[TicketID] [char](36) NOT NULL,
	[Seq_no] [int] NOT NULL,
	[AssignedDate] [datetime] NOT NULL,
	[AssignedBy] [datetime] NOT NULL,
	CONSTRAINT [PK_Tkt_TicketOperator] PRIMARY KEY CLUSTERED
(
	[TktOperator] ASC,
	[TicketID] ASC,
	[Seq_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_TicketTimeline]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_TicketTimeline]
(
	[TicketID] [char](36) NOT NULL,
	[TxnDateTime] [datetime] NOT NULL,
	[TktEvent] [nvarchar](max) NOT NULL,
	[TxnValues] [nvarchar](max) NOT NULL,
	[TxnUserID] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Tkt_TicketTimeline_1] PRIMARY KEY CLUSTERED
(
	[TicketID] ASC,
	[TxnDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tkt_user]    Script Date: 6/8/2020 11:42:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_User]
(
	[CompanyID] [char](36) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[UserType] [nvarchar](20) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[Phone] [char](10) NULL,
	[UserImage] [nvarchar](max) NULL,
	[UserRole] [nvarchar](20) NOT NULL,
	CONSTRAINT [PK_Tkt_userz] PRIMARY KEY CLUSTERED
(
	[CompanyID] ASC,
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Tkt_userToken]    Script Date: 6/8/2020 11:42:44 PM ******/
/*SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tkt_userToken](
	[username] [char](36) NOT NULL,
	[Token] [char](256) NOT NULL,
 CONSTRAINT [PK_Tkt_userToken] PRIMARY KEY CLUSTERED
(
	[username] ASC,
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO*/

USE [master]
GO
ALTER DATABASE [db_helpdesk] SET  READ_WRITE
GO
