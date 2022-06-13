USE [master]
GO
/****** Object:  Database [ISEdb]    Script Date: 07/06/2022 10:19:58 ******/
CREATE DATABASE [ISEdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ISEdb', FILENAME = N'H:\mssql\Data\ISEdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ISEdb_log', FILENAME = N'O:\mssql\Log\ISEdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ISEdb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ISEdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ISEdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ISEdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ISEdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ISEdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ISEdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ISEdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ISEdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ISEdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ISEdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ISEdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ISEdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ISEdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ISEdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ISEdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ISEdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ISEdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ISEdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ISEdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ISEdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ISEdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ISEdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ISEdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ISEdb] SET RECOVERY FULL 
GO
ALTER DATABASE [ISEdb] SET  MULTI_USER 
GO
ALTER DATABASE [ISEdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ISEdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ISEdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ISEdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ISEdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ISEdb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ISEdb', N'ON'
GO
ALTER DATABASE [ISEdb] SET QUERY_STORE = OFF
GO
USE [ISEdb]
GO
/****** Object:  User [MSWeb_AppUser]    Script Date: 07/06/2022 10:20:01 ******/
CREATE USER [MSWeb_AppUser] FOR LOGIN [MSWeb_AppUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KT\KondetiR]    Script Date: 07/06/2022 10:20:01 ******/
CREATE USER [KT\KondetiR] FOR LOGIN [KT\KondetiR] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [MSWeb_AppUser]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[Reference] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[LastUpdate] [datetime2](7) NULL,
	[StartDate] [datetime2](7) NULL,
	[FieldingPeriod] [int] NULL,
	[Status] [int] NULL,
	[LinkToSurvey] [nvarchar](500) NULL,
	[CintResponseId] [int] NULL,
	[CintSelfLink] [nvarchar](500) NULL,
	[CintCurrentCostLink] [nvarchar](500) NULL,
	[CintTestingLink] [nvarchar](500) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectTargetAudience]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTargetAudience](
	[Id] [nvarchar](50) NOT NULL,
	[ProjectId] [nvarchar](50) NOT NULL,
	[TargetAudienceId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProjectTargetAudience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Qualification]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Qualification](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[LogicalOperator] [nvarchar](50) NULL,
	[NumberOfRequiredConditions] [int] NULL,
	[IsActive] [bit] NULL,
	[PreCodes] [nvarchar](200) NULL,
	[QualificationOrder] [int] NULL,
 CONSTRAINT [PK_Qualification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quota]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quota](
	[Id] [nvarchar](50) NOT NULL,
	[QuotaName] [nvarchar](200) NOT NULL,
	[Limit] [int] NULL,
	[PreCode] [nvarchar](200) NULL,
	[MinAge] [int] NULL,
	[MaxAge] [int] NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_Quota] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuotaGroup]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuotaGroup](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_QuotaGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuotaGroupQuota]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuotaGroupQuota](
	[Id] [nvarchar](50) NOT NULL,
	[QuotaGroupId] [nvarchar](50) NOT NULL,
	[QuotaId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_QuotaGroupQuota] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TargetAudience]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetAudience](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[TANumber] [int] NULL,
	[Limit] [int] NULL,
	[LimitType] [nvarchar](50) NULL,
 CONSTRAINT [PK_TargetAudience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TargetAudienceQualification]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetAudienceQualification](
	[Id] [nvarchar](50) NOT NULL,
	[TargetAudienceId] [nvarchar](50) NOT NULL,
	[QualificationId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TargetAudienceQualification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TargetAudienceQuotaGroup]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetAudienceQuotaGroup](
	[Id] [nvarchar](50) NOT NULL,
	[TargetAudienceId] [nvarchar](50) NOT NULL,
	[QuotaGroupId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TargetAudienceQuotaGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 07/06/2022 10:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'377e49e4-410c-43b7-aebe-4e0c56ecf910', N'Study on Television marketing', N'MAC007', N'90166606-ebe5-4a5c-873b-60f2157ba060', CAST(N'2022-06-06T22:00:11.0221014' AS DateTime2), CAST(N'2022-12-29T00:00:00.0000000' AS DateTime2), 60, 1, N'https://sim.cintworks.net/[ID]', 93821, N'https://api.cintworks.net/ordering/Surveys/93821', N'https://api.cintworks.net/ordering/Surveys/93821/CurrentCost', N'https://api.cintworks.net/ordering/Surveys/93821/Test')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'3a8f5757-56d4-4671-a304-816b3315affd', N'All about cars', N'MAC004', N'fdbf4f5c-5569-4a12-b6d7-f8ee7e23484d', CAST(N'2022-06-06T21:35:13.0525560' AS DateTime2), CAST(N'2022-08-23T00:00:00.0000000' AS DateTime2), 60, 0, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'5361ee95-7f0f-462a-b897-b6cf9364aeff', N'Study on Fizzy drinks ', N'MAC003', N'0490dddb-aa6c-4ee9-a50c-ad0f8d4f8f40', CAST(N'2022-06-06T21:33:17.4691752' AS DateTime2), CAST(N'2022-07-22T00:00:00.0000000' AS DateTime2), 60, 0, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'8d2f1989-9b9c-4f23-a938-239cc5a19867', N'Survey for students', N'MAC005', N'47f27a66-9957-4ee5-b712-6d14abc77580', CAST(N'2022-06-06T21:41:44.1191461' AS DateTime2), CAST(N'2022-09-23T00:00:00.0000000' AS DateTime2), 60, 0, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'a11fb372-bccd-41aa-a818-8971b33e035f', N'Stocks Study', N'MAC001', N'37ca0ced-f36f-4ca8-b5b9-6504cc3e50f2', CAST(N'2022-06-06T21:12:48.4052932' AS DateTime2), CAST(N'2022-06-26T00:00:00.0000000' AS DateTime2), 60, 0, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'ac5e5cea-29d6-430c-8cb6-f10dbe590a3c', N'Survey on Clothes', N'MAC002', N'f70e1efc-a197-4508-ad7b-fcac7c09b88b', CAST(N'2022-06-06T21:24:47.3699680' AS DateTime2), CAST(N'2022-06-30T00:00:00.0000000' AS DateTime2), 60, 1, N'https://sim.cintworks.net/[ID]', 93820, N'https://api.cintworks.net/ordering/Surveys/93820', N'https://api.cintworks.net/ordering/Surveys/93820/CurrentCost', N'https://api.cintworks.net/ordering/Surveys/93820/Test')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'c93fa78a-eb32-4027-a3b9-b0c677f5fb6f', N'Income Survey', N'MAC008', N'75ae9c53-38b3-45c2-8606-eca751d62840', CAST(N'2022-06-07T08:05:58.2400723' AS DateTime2), CAST(N'2022-01-29T00:00:00.0000000' AS DateTime2), 60, 4, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[Project] ([Id], [Name], [Reference], [UserId], [LastUpdate], [StartDate], [FieldingPeriod], [Status], [LinkToSurvey], [CintResponseId], [CintSelfLink], [CintCurrentCostLink], [CintTestingLink]) VALUES (N'ffe87379-7de4-42bc-87c7-db6f5eac3d8f', N'Survey on product demos ', N'MAC006', N'2f8b84ba-5f02-4239-b16c-1a9cb653a97d', CAST(N'2022-06-06T21:59:00.2177822' AS DateTime2), CAST(N'2022-11-26T00:00:00.0000000' AS DateTime2), 60, 0, N'https://sim.cintworks.net/[ID]', 0, N'NA', N'NA', N'NA')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'15cf06c5-833e-41a8-8bd0-aea450b59ba9', N'377e49e4-410c-43b7-aebe-4e0c56ecf910', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'1ee821e1-65f7-4e39-a550-977a04b4e885', N'c93fa78a-eb32-4027-a3b9-b0c677f5fb6f', N'08f4ec9c-2802-41af-8d3b-9755809d2774')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'5d15d010-b77f-4ccb-b571-66f367dc0adf', N'a11fb372-bccd-41aa-a818-8971b33e035f', N'20ed454e-fe00-40b0-9455-404679fb3eae')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'61731fb7-bc8f-4cc8-a4c0-8a7436fce72f', N'ac5e5cea-29d6-430c-8cb6-f10dbe590a3c', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'6be97b97-6e7d-44e7-ac38-f84bb0849fc0', N'5361ee95-7f0f-462a-b897-b6cf9364aeff', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'8fe6009b-55db-411e-8978-682108ab9f7e', N'ffe87379-7de4-42bc-87c7-db6f5eac3d8f', N'92942935-5a3b-4e00-b938-3068fdc02922')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'ed9deb61-43fc-433c-91dc-46b92759d013', N'3a8f5757-56d4-4671-a304-816b3315affd', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a')
GO
INSERT [dbo].[ProjectTargetAudience] ([Id], [ProjectId], [TargetAudienceId]) VALUES (N'f3ba995d-a4a9-476f-8ddc-e2c5f2f1bfa0', N'8d2f1989-9b9c-4f23-a938-239cc5a19867', N'8113eb32-4d1f-4338-b635-7dc548cafc4e')
GO
INSERT [dbo].[Qualification] ([Id], [Name], [LogicalOperator], [NumberOfRequiredConditions], [IsActive], [PreCodes], [QualificationOrder]) VALUES (N'59995989-70f6-4a42-a0c9-70ba6976900c', N'Adult Age Set 2', N'or', 1, 1, N'35-45', NULL)
GO
INSERT [dbo].[Qualification] ([Id], [Name], [LogicalOperator], [NumberOfRequiredConditions], [IsActive], [PreCodes], [QualificationOrder]) VALUES (N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed', N'Adult Age Set 1', N'or', 1, 1, N'18-25', NULL)
GO
INSERT [dbo].[Qualification] ([Id], [Name], [LogicalOperator], [NumberOfRequiredConditions], [IsActive], [PreCodes], [QualificationOrder]) VALUES (N'dd9e5af1-0d31-48df-b711-25845cf12540', N'Adult Age Set 3', N'or', 1, 1, N'55-60', NULL)
GO
INSERT [dbo].[Qualification] ([Id], [Name], [LogicalOperator], [NumberOfRequiredConditions], [IsActive], [PreCodes], [QualificationOrder]) VALUES (N'edf174e4-e083-46c5-a0b7-437aaaf6f723', N'Adult Age Set 4', N'or', 1, 1, N'65-70', NULL)
GO
INSERT [dbo].[Quota] ([Id], [QuotaName], [Limit], [PreCode], [MinAge], [MaxAge], [Gender]) VALUES (N'298d5c93-85a8-434b-9041-f83ed137fca5', N'Female', 50, NULL, 0, 0, 2)
GO
INSERT [dbo].[Quota] ([Id], [QuotaName], [Limit], [PreCode], [MinAge], [MaxAge], [Gender]) VALUES (N'36521dc3-47aa-4c84-8213-c81d2a3517db', N'Old', 30, NULL, 36, 70, 0)
GO
INSERT [dbo].[Quota] ([Id], [QuotaName], [Limit], [PreCode], [MinAge], [MaxAge], [Gender]) VALUES (N'4fdad58c-cb3b-4f90-8864-c172ead60323', N'Male', 50, NULL, 0, 0, 1)
GO
INSERT [dbo].[Quota] ([Id], [QuotaName], [Limit], [PreCode], [MinAge], [MaxAge], [Gender]) VALUES (N'f2418c8f-8305-43a5-9c1b-4ed0ca0c4c8b', N'Young', 70, NULL, 18, 35, 0)
GO
INSERT [dbo].[QuotaGroup] ([Id], [Name], [IsActive]) VALUES (N'49ea854b-fa04-4718-a2df-c52dd1d76dad', N'Age', 1)
GO
INSERT [dbo].[QuotaGroup] ([Id], [Name], [IsActive]) VALUES (N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258', N'Gender', 1)
GO
INSERT [dbo].[QuotaGroupQuota] ([Id], [QuotaGroupId], [QuotaId]) VALUES (N'1d1c5534-677e-4a36-a8a0-85f16898ca6f', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258', N'4fdad58c-cb3b-4f90-8864-c172ead60323')
GO
INSERT [dbo].[QuotaGroupQuota] ([Id], [QuotaGroupId], [QuotaId]) VALUES (N'63fc29cf-b22a-486b-981d-ae1e1548b5d6', N'49ea854b-fa04-4718-a2df-c52dd1d76dad', N'36521dc3-47aa-4c84-8213-c81d2a3517db')
GO
INSERT [dbo].[QuotaGroupQuota] ([Id], [QuotaGroupId], [QuotaId]) VALUES (N'c1e9f67f-c848-479e-95f2-bd76492cfed9', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258', N'298d5c93-85a8-434b-9041-f83ed137fca5')
GO
INSERT [dbo].[QuotaGroupQuota] ([Id], [QuotaGroupId], [QuotaId]) VALUES (N'fd0a037f-b198-4b26-a919-e39e57624c97', N'49ea854b-fa04-4718-a2df-c52dd1d76dad', N'f2418c8f-8305-43a5-9c1b-4ed0ca0c4c8b')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'TAA03', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'TAA008', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'20ed454e-fe00-40b0-9455-404679fb3eae', N'TAA', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'TAA007', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'TAA04', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'TAA06', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'92942935-5a3b-4e00-b938-3068fdc02922', N'TAA06', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudience] ([Id], [Name], [TANumber], [Limit], [LimitType]) VALUES (N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'TAA01', NULL, 200, N'0')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'00933340-a6a8-48b4-86a9-e09f22a92716', N'92942935-5a3b-4e00-b938-3068fdc02922', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'03d41c76-62e5-4adc-a8f1-425c5c576dbf', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'182e247d-adff-4dfa-87a4-1e0359997790', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'1944fa4c-af33-4596-bcfa-f5670091fc0c', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'1cae81f7-4f38-41da-99cc-85c46c8d2fca', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'20cc7edf-41b4-4350-baba-274bf752ff6c', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'21222c6d-e715-461e-9443-66bb8c6a799d', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'26b52999-fc79-402d-b0b8-ea7ab3b917d6', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'2d40a7ee-ddc2-4cb4-821b-4d8b8c233fad', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'3198955a-8d9b-428d-83b7-467e3ddb28e9', N'92942935-5a3b-4e00-b938-3068fdc02922', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'46bb6e43-039c-4274-ab29-51f4af344576', N'92942935-5a3b-4e00-b938-3068fdc02922', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'4b58cf7e-5efa-4bf6-a417-ee71cf407964', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'4b65f2c2-d3c2-4870-9c2d-cb96f3972f37', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'5eafd5c5-6f01-47e2-b163-2f3cf64ca088', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'63156d2f-b1a9-4d47-afe3-eaf3231a8c32', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'64403785-c260-44a9-a848-39748c4aaf65', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'6624fc90-6628-44ed-b118-4d1dfa8f9e71', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'75832c37-e3e0-4b61-8472-4f2dde06938f', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'75a9e015-47bb-44d5-a9d4-63902e02dd33', N'92942935-5a3b-4e00-b938-3068fdc02922', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'7aa969d8-2242-475c-95f1-c6fd5388b73b', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'88bd9264-5158-4db3-be34-b384bb4d7145', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'8d15005b-167d-4438-acf3-f27500c0dc30', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'918b3d6b-84d0-4885-afaf-40df04192ccb', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'935734ee-a2c1-4ecf-82d0-a8a995c30349', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'b55cbbb8-3b42-41b2-ac27-e420a8fdbbed')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'9fc9b7a1-6bf6-4f01-81bd-bb1a59b4d4df', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'b0102d17-0631-4533-83b4-02c8a52ab4c5', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'b66ac122-7728-423f-981d-8967e6ee0796', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'c3b2020c-72ee-47bb-87a7-6e1fb979b38a', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'edf174e4-e083-46c5-a0b7-437aaaf6f723')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'd4c266e5-bd2f-47a1-9d42-35ab8351178b', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'd5e99431-a15a-43cd-ad32-708a720d8640', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'e49a6852-759d-445f-8a51-9f368cbc9090', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'59995989-70f6-4a42-a0c9-70ba6976900c')
GO
INSERT [dbo].[TargetAudienceQualification] ([Id], [TargetAudienceId], [QualificationId]) VALUES (N'ea4ecd5d-5ae1-4645-acde-ad48732f2585', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'dd9e5af1-0d31-48df-b711-25845cf12540')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'1127d906-5ff9-4e94-9d9e-0533b514a49d', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'1d8c5bf2-5281-406a-931d-2b60f5d6d19a', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'1fc8224c-0063-4494-8d65-6da65c91343e', N'62aa8c28-a70d-4bda-bb6b-b1452b4db68a', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'300f8381-caa2-4c12-bfd4-46daa5b0af1f', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'40ca187f-b4d8-4b32-bbed-3388e3162c9d', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'6bf4dc06-c02a-471a-9e6d-c92be30bfee2', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'6c55d513-940f-46a0-9223-25e99aa9e4fa', N'8113eb32-4d1f-4338-b635-7dc548cafc4e', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'6f7ddc0e-a11c-4487-8091-d35a2f5d5350', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'90dbfa5a-5f40-4672-ab92-9e92ac20c479', N'92942935-5a3b-4e00-b938-3068fdc02922', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'98e1e45d-8968-458f-91f1-978109c0da8a', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'a43298d2-eb90-4168-b9d6-c8094324ab66', N'92942935-5a3b-4e00-b938-3068fdc02922', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'b56c17bf-b877-44e9-a80e-02dc6bd6f288', N'03fed17c-5e12-4b63-8cdc-89dfb3bf2411', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'caa61b48-d566-4c56-afe0-68d43f7e4545', N'08f4ec9c-2802-41af-8d3b-9755809d2774', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'd0a42f06-8742-45e0-883d-191b031e70c0', N'b0ef4bdb-5d3a-4323-a629-1b6060c01952', N'49ea854b-fa04-4718-a2df-c52dd1d76dad')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'f4035bec-87b5-4100-8905-e7fb85ec126c', N'5cdd8dd2-9dd4-463c-9e59-5a1e95566e19', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[TargetAudienceQuotaGroup] ([Id], [TargetAudienceId], [QuotaGroupId]) VALUES (N'f83e6228-dee5-4899-8008-927576451697', N'20ed454e-fe00-40b0-9455-404679fb3eae', N'eb7ef5d4-c4ba-41dd-9e25-d49e2eec0258')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'0490dddb-aa6c-4ee9-a50c-ad0f8d4f8f40', N'Bravo', N'Bravo.Test@KK.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'2f8b84ba-5f02-4239-b16c-1a9cb653a97d', N'Echo', N'Echo.Test@KK.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'37ca0ced-f36f-4ca8-b5b9-6504cc3e50f2', N'Gopal', N'mailgopalv@gmail.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'47f27a66-9957-4ee5-b712-6d14abc77580', N'Delta', N'Delta.Test@KK.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'75ae9c53-38b3-45c2-8606-eca751d62840', N'Henry', N'Henry.Test@KK.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'90166606-ebe5-4a5c-873b-60f2157ba060', N'Frank', N'Frank.Test@KK.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'f70e1efc-a197-4508-ad7b-fcac7c09b88b', N'Alpha', N'Alpha.Test@kk.com')
GO
INSERT [dbo].[User] ([Id], [Name], [Email]) VALUES (N'fdbf4f5c-5569-4a12-b6d7-f8ee7e23484d', N'Charlie', N'Charlie.Test@KK.com')
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[ProjectTargetAudience]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTargetAudience_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudience] CHECK CONSTRAINT [FK_ProjectTargetAudience_Project]
GO
ALTER TABLE [dbo].[ProjectTargetAudience]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTargetAudience_TargetAudience] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudience] CHECK CONSTRAINT [FK_ProjectTargetAudience_TargetAudience]
GO
ALTER TABLE [dbo].[QuotaGroupQuota]  WITH CHECK ADD  CONSTRAINT [FK_QuotaGroupQuota_Quota] FOREIGN KEY([QuotaId])
REFERENCES [dbo].[Quota] ([Id])
GO
ALTER TABLE [dbo].[QuotaGroupQuota] CHECK CONSTRAINT [FK_QuotaGroupQuota_Quota]
GO
ALTER TABLE [dbo].[QuotaGroupQuota]  WITH CHECK ADD  CONSTRAINT [FK_QuotaGroupQuota_QuotaGroup] FOREIGN KEY([QuotaGroupId])
REFERENCES [dbo].[QuotaGroup] ([Id])
GO
ALTER TABLE [dbo].[QuotaGroupQuota] CHECK CONSTRAINT [FK_QuotaGroupQuota_QuotaGroup]
GO
ALTER TABLE [dbo].[TargetAudienceQualification]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudienceQualification_Qualification] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[Qualification] ([Id])
GO
ALTER TABLE [dbo].[TargetAudienceQualification] CHECK CONSTRAINT [FK_TargetAudienceQualification_Qualification]
GO
ALTER TABLE [dbo].[TargetAudienceQualification]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudienceQualification_TargetAudience] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[TargetAudienceQualification] CHECK CONSTRAINT [FK_TargetAudienceQualification_TargetAudience]
GO
ALTER TABLE [dbo].[TargetAudienceQuotaGroup]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudienceQuotaGroup_QuotaGroup] FOREIGN KEY([QuotaGroupId])
REFERENCES [dbo].[QuotaGroup] ([Id])
GO
ALTER TABLE [dbo].[TargetAudienceQuotaGroup] CHECK CONSTRAINT [FK_TargetAudienceQuotaGroup_QuotaGroup]
GO
ALTER TABLE [dbo].[TargetAudienceQuotaGroup]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudienceQuotaGroup_TargetAudience] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[TargetAudienceQuotaGroup] CHECK CONSTRAINT [FK_TargetAudienceQuotaGroup_TargetAudience]
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllTableRecords]    Script Date: 07/06/2022 10:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAllTableRecords] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete FROM [ISEdb].[dbo].[User]
	Delete FROM [ISEdb].[dbo].[QuotaGroupQuota]
	Delete FROM [ISEdb].[dbo].[Quota]
	Delete FROM [ISEdb].[dbo].[TargetAudienceQuotaGroup]
	Delete FROM [ISEdb].[dbo].[QuotaGroup]
	Delete FROM [ISEdb].[dbo].[TargetAudienceQualification]
	Delete FROM [ISEdb].[dbo].[Qualification]
	Delete FROM [ISEdb].[dbo].[ProjectTargetAudience]
	Delete FROM [ISEdb].[dbo].[TargetAudience]
	Delete FROM [ISEdb].[dbo].[Project]
END
GO
/****** Object:  StoredProcedure [dbo].[sp_createProject]    Script Date: 07/06/2022 10:20:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_createProject]
  @ID nvarchar(50),
  @Name nvarchar(50)
  ,@Reference nvarchar(50)
  ,@Userid nvarchar(50)
   ,@fielingPeriod  int
   ,@status  int
   ,@Linktosurvey  nvarchar(50)
   


AS 
BEGIN 

 SET NOCOUNT ON; 

  BEGIN 


	--		if @Userid is not null
	--begin
	--	select @id from dbo.[User] where @id=@Userid
	--		if @id is null
	--		begin 			
	--			insert into dbo.[User] (Id,[Name],Email) values (@Userid,@Name,@Reference)				
	--		end 
	--end

	if @Userid is not null
		begin 
			if (select 1 from dbo.[User] where @id=@Userid) is Null
			 begin 
					insert into dbo.[User] (Id,[Name],Email) values (@Userid,@Name,@Reference)
			End
		End


		
		insert into dbo.project(Id,[Name], Reference, UserId, FieldingPeriod,[Status],LinkToSurvey)
		values(@ID, @Name, @Reference,@UserId, @fielingPeriod, @status,@LinktoSurvey)
  END 
END
GO
USE [master]
GO
ALTER DATABASE [ISEdb] SET  READ_WRITE 
GO
