USE [master]
GO
/****** Object:  Database [nanoe_web]    Script Date: 2022/11/02 2:07:59 AM ******/
CREATE DATABASE [nanoe_web]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'nanoe_web', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\nanoe_web.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'nanoe_web_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\nanoe_web_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [nanoe_web] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [nanoe_web].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [nanoe_web] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [nanoe_web] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [nanoe_web] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [nanoe_web] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [nanoe_web] SET ARITHABORT OFF 
GO
ALTER DATABASE [nanoe_web] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [nanoe_web] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [nanoe_web] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [nanoe_web] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [nanoe_web] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [nanoe_web] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [nanoe_web] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [nanoe_web] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [nanoe_web] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [nanoe_web] SET  DISABLE_BROKER 
GO
ALTER DATABASE [nanoe_web] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [nanoe_web] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [nanoe_web] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [nanoe_web] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [nanoe_web] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [nanoe_web] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [nanoe_web] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [nanoe_web] SET RECOVERY FULL 
GO
ALTER DATABASE [nanoe_web] SET  MULTI_USER 
GO
ALTER DATABASE [nanoe_web] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [nanoe_web] SET DB_CHAINING OFF 
GO
ALTER DATABASE [nanoe_web] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [nanoe_web] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [nanoe_web] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [nanoe_web] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'nanoe_web', N'ON'
GO
ALTER DATABASE [nanoe_web] SET QUERY_STORE = OFF
GO
USE [nanoe_web]
GO
/****** Object:  User [nanoe_web1]    Script Date: 2022/11/02 2:07:59 AM ******/
CREATE USER [nanoe_web1] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[nanoe_web]
GO
/****** Object:  User [nanoe_web]    Script Date: 2022/11/02 2:07:59 AM ******/
CREATE USER [nanoe_web] FOR LOGIN [nanoe_web] WITH DEFAULT_SCHEMA=[nanoe_web]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [nanoe_web]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [nanoe_web]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [nanoe_web]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [nanoe_web]
GO
ALTER ROLE [db_datareader] ADD MEMBER [nanoe_web]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [nanoe_web]
GO
/****** Object:  UserDefinedFunction [dbo].[WordCount]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[WordCount] ( @InputString VARCHAR(4000) ) 
RETURNS INT
AS
BEGIN

DECLARE @Index          INT
DECLARE @Char           CHAR(1)
DECLARE @PrevChar       CHAR(1)
DECLARE @WordCount      INT

SET @Index = 1
SET @WordCount = 0

WHILE @Index <= LEN(@InputString)
BEGIN
    SET @Char     = SUBSTRING(@InputString, @Index, 1)
    SET @PrevChar = CASE WHEN @Index = 1 THEN ' '
                         ELSE SUBSTRING(@InputString, @Index - 1, 1)
                    END

    IF @PrevChar = ' ' AND @Char != ' '
        SET @WordCount = @WordCount + 1

    SET @Index = @Index + 1
END

RETURN @WordCount

END
GO
/****** Object:  Table [dbo].[MNovelChapter]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelChapter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelinstance_id] [int] NOT NULL,
	[title] [varchar](max) NULL,
	[orderposition] [int] NOT NULL,
 CONSTRAINT [PK_MNovelChapter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelChapterNote]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelChapterNote](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelchapter_id] [int] NOT NULL,
	[note] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MNovelChapterNote] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelContent]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelContent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelinstance_id] [int] NOT NULL,
	[id_before] [int] NOT NULL,
	[id_after] [int] NOT NULL,
	[flag] [varchar](50) NOT NULL,
	[text] [varchar](max) NOT NULL,
	[_lastupdate] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelDeletion]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelDeletion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelinstance_id] [int] NOT NULL,
	[wherewas_before] [int] NOT NULL,
	[wherewas_before_text] [varchar](max) NOT NULL,
	[wherewas_after] [int] NOT NULL,
	[wherewas_after_text] [varchar](max) NOT NULL,
	[text] [varchar](max) NOT NULL,
	[flag] [varchar](50) NOT NULL,
	[whenwas_deleted] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MNovelDeletion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelInstance]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelInstance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](256) NOT NULL,
	[info] [varchar](max) NOT NULL,
	[_lastupdate] [varchar](50) NOT NULL,
	[_deleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelNote]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelNote](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelinstance_id] [int] NOT NULL,
	[title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MNovelNote] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MNovelNoteNote]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MNovelNoteNote](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelnote_id] [int] NOT NULL,
	[note] [varchar](max) NOT NULL,
 CONSTRAINT [PK_MNovelNoteNote] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTracking]    Script Date: 2022/11/02 2:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTracking](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[novelinstance_id] [int] NOT NULL,
	[session_start] [varchar](125) NOT NULL,
	[session_end] [varchar](125) NOT NULL,
	[wordcount_start] [int] NOT NULL,
	[wordcount_end] [int] NOT NULL,
	[chapters_start] [int] NOT NULL,
	[chapters_end] [int] NOT NULL,
	[paragraphs_start] [int] NOT NULL,
	[paragraphs_end] [int] NOT NULL,
	[bookmarks_start] [int] NOT NULL,
	[bookmarks_end] [int] NOT NULL,
	[notes_start] [int] NOT NULL,
	[notes_end] [int] NOT NULL,
 CONSTRAINT [PK_TTracking_] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MNovelInstance] ADD  CONSTRAINT [DF_MNovelInstance__deleted]  DEFAULT ((0)) FOR [_deleted]
GO
USE [master]
GO
ALTER DATABASE [nanoe_web] SET  READ_WRITE 
GO
