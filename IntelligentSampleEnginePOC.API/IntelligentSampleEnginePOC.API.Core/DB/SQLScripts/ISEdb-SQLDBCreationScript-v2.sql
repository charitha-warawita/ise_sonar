USE [master]
GO
/****** Object:  Database [ISEdb]    Script Date: 26/07/2022 13:11:28 ******/
CREATE DATABASE [ISEdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ISEdb', FILENAME = N'H:\mssql\Data\ISEdb.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ISEdb_log', FILENAME = N'O:\mssql\Log\ISEdb_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  User [MSWeb_AppUser]    Script Date: 26/07/2022 13:11:31 ******/
CREATE USER [MSWeb_AppUser] FOR LOGIN [MSWeb_AppUser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [KT\KondetiR]    Script Date: 26/07/2022 13:11:31 ******/
CREATE USER [KT\KondetiR] FOR LOGIN [KT\KondetiR] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [MSWeb_AppUser]
GO
/****** Object:  Table [dbo].[AnswersByCountry]    Script Date: 26/07/2022 13:11:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswersByCountry](
	[CountryID] [int] NULL,
	[QuestionID] [int] NULL,
	[ValueCode] [int] NULL,
	[ValueText] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cint_Attribute_DT_Config]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cint_Attribute_DT_Config](
	[Name] [varchar](400) NOT NULL,
	[AttributeGroup] [varchar](200) NULL,
	[Position] [int] NULL,
	[ShortName] [varchar](100) NULL,
	[AttributeType] [varchar](20) NULL,
	[Source] [varchar](50) NULL,
	[Panelist_LP_Status] [char](5) NULL,
	[PIIInd] [tinyint] NULL,
	[ExportDisplayForAdminInd] [tinyint] NULL,
	[ExportDisplayForProjectUserInd] [tinyint] NULL,
	[ExportTab_Display_AdminInd] [tinyint] NULL,
	[ExportTab_Display_ProjectUserInd] [tinyint] NULL,
	[BulkImportAllowed_Ind] [tinyint] NULL,
	[DraggableForCountsInd] [tinyint] NULL,
	[MouseoverAttributeDescription] [varchar](4000) NULL,
	[ValueJoin] [varchar](9) NOT NULL,
	[AttributeValueList] [varchar](max) NULL,
	[AttributeValueListByPanel] [varchar](max) NULL,
	[SplitValuesByPanelInd] [tinyint] NOT NULL,
	[APISupport] [bit] NULL,
	[APIName] [varchar](60) NULL,
	[APIType] [varchar](30) NULL,
	[APIDescription] [varchar](200) NULL,
	[panelist_agc_status] [char](5) NULL,
	[AuditableInd] [tinyint] NULL,
	[Derived_attribute] [smallint] NULL,
 CONSTRAINT [PK_Cint_Attribute_DT_Config_Name] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cint_Panel]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cint_Panel](
	[CintPanelId] [int] NOT NULL,
	[CintPanelName] [varchar](100) NULL,
	[PanelBrand] [varchar](200) NULL,
	[CintCountryId] [int] NULL,
	[CintCountry] [varchar](100) NULL,
	[CintLanguage] [varchar](100) NULL,
	[CintCurrency] [varchar](100) NULL,
	[APIKey] [varchar](100) NULL,
	[APISecret] [varchar](100) NULL,
	[PointsRate] [decimal](6, 3) NULL,
	[PostalCodeRegex] [varchar](100) NULL,
	[PanelXML] [xml] NULL,
	[QuestionsXML] [xml] NULL,
	[ActiveCnt] [int] NULL,
	[RILCountyLanguageCode] [varchar](10) NULL,
	[CutOver] [bit] NOT NULL,
	[CutOverDate] [date] NULL,
	[UpdateSampleBaseProcessFlag] [char](1) NULL,
	[SampleBaseNewJoinGracePeriod] [smallint] NULL,
	[SampleBaseInactivityPeriod] [smallint] NULL,
	[SubsidiaryPanelFlag] [char](1) NULL,
	[PanelSourceCode] [varchar](20) NULL,
	[MarketCode] [char](2) NULL,
	[LanguageCode] [char](2) NULL,
	[SampleBaseLastLoginPeriod] [smallint] NULL,
 CONSTRAINT [PK_Cint_Panel_CintPanelId] PRIMARY KEY CLUSTERED 
(
	[CintPanelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cint_PanelAttribute]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cint_PanelAttribute](
	[CintIdent] [bigint] NOT NULL,
	[CintPanelId] [int] NULL,
	[AttributeGroup] [nvarchar](200) NULL,
	[Position] [int] NULL,
	[Type] [varchar](200) NULL,
	[Name] [varchar](400) NULL,
	[ShortName] [varchar](100) NULL,
	[Label] [varchar](1000) NULL,
	[Label_Local] [nvarchar](1000) NULL,
	[ValueCnt] [int] NULL,
	[HideOn] [varchar](20) NULL,
	[QuestionScope] [varchar](20) NULL,
	[LifePointsProfilerSurveyName] [varchar](100) NULL,
	[SmartScreenEnabled] [char](1) NULL,
	[QuickPollEnabled] [char](1) NULL,
	[PortalEnabled] [char](1) NULL,
	[Priority] [decimal](4, 2) NULL,
	[OptIn] [char](1) NULL,
	[SubjectToGDPR] [char](1) NULL,
	[ExpirationPeriod] [smallint] NULL,
	[ResponseOptionCnt] [smallint] NULL,
	[AskingGroup] [varchar](20) NULL,
	[AskingGroupPosition] [smallint] NULL,
	[BranchLogic] [varchar](300) NULL,
	[AttributeCreatedDtm] [datetime2](0) NULL,
	[ModifiedDtm] [datetime2](0) NULL,
	[PredictivePower] [decimal](6, 3) NULL,
	[ManualAttributeBoost] [int] NULL,
	[ManualAttributeBoostStartDtm] [datetime2](0) NULL,
	[OptInInd] [tinyint] NULL,
	[SubjectToGDPRInd] [tinyint] NULL,
	[SurveyTileEnabledInd] [char](1) NULL,
	[EndlessProfilerEnabledInd] [char](1) NULL,
	[SurveyTileEnabled] [char](1) NULL,
	[EndlessProfilerEnabled] [char](1) NULL,
 CONSTRAINT [PK_Cint_PanelAttribute_CintIdent] PRIMARY KEY CLUSTERED 
(
	[CintIdent] ASC
)WITH (PAD_INDEX = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cint_PanelAttributeValue]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cint_PanelAttributeValue](
	[CintIdent] [bigint] NOT NULL,
	[CintValueId] [bigint] NULL,
	[ValueCode] [int] NOT NULL,
	[ValueText] [nvarchar](400) NULL,
	[ValueText_Local] [nvarchar](400) NULL,
	[Status] [varchar](20) NULL,
	[GlobalVarId] [bigint] NULL,
	[CountryLanguageVarId] [bigint] NULL,
	[HideOn] [varchar](20) NULL,
	[ExclusiveInd] [tinyint] NULL,
 CONSTRAINT [PK_Cint_PanelAttributeValue_CintIdent_ValueCode] PRIMARY KEY CLUSTERED 
(
	[CintIdent] ASC,
	[ValueCode] ASC
)WITH (PAD_INDEX = ON, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CintApiInfo]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CintApiInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Url] [nvarchar](1000) NOT NULL,
	[Header_xapikey] [nvarchar](1000) NULL,
	[Header_Authorization] [nvarchar](1000) NULL,
	[Header_BAuthKey] [nvarchar](1000) NULL,
	[Header_BAuthSecret] [nvarchar](1000) NULL,
	[Header_ContentType] [nvarchar](200) NULL,
	[Header_Accept] [nvarchar](200) NULL,
 CONSTRAINT [PK_Cint_ApiJson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[ValidIRMin] [int] NULL,
	[ValidIRMax] [int] NULL,
	[ValidLOIMin] [int] NULL,
	[ValidLOIMax] [int] NULL,
	[PostalCodeTargetingEnabled] [bit] NULL,
	[AgeMin] [int] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryRegion]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryRegion](
	[Id] [int] NOT NULL,
	[RegionTypeId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_CountryRegion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CountryRegionType]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryRegionType](
	[Id] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Active] [bit] NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_CountryRegionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDetails]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDetails](
	[CustomerID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerUniqueGUID] [uniqueidentifier] NOT NULL,
	[CustomerName] [varchar](100) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFieldData]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFieldData](
	[CustomerFieldDataID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[FieldID] [varchar](100) NOT NULL,
	[FieldValue] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerFieldDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalAnswers]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalAnswers](
	[CountryID] [int] NULL,
	[QuestionID] [int] NULL,
	[ValueCode] [int] NULL,
	[ValueText] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalQuestions]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalQuestions](
	[QuestionID] [int] NULL,
	[QuestionCategory] [nvarchar](100) NULL,
	[QuestionName] [nvarchar](100) NULL,
	[QuestionText] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 26/07/2022 13:11:32 ******/
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
/****** Object:  Table [dbo].[Qualification]    Script Date: 26/07/2022 13:11:32 ******/
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
	[TargetAudienceId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Qualification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionsByCountry]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionsByCountry](
	[QuestionID] [int] NULL,
	[QuestionCategory] [nvarchar](100) NULL,
	[QuestionName] [nvarchar](100) NULL,
	[QuestionText] [nvarchar](1000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quota]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quota](
	[Id] [nvarchar](50) NOT NULL,
	[QuotaName] [nvarchar](200) NOT NULL,
	[Limit] [int] NULL,
	[Condition] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[TargetAudienceId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Quota] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TargetAudience]    Script Date: 26/07/2022 13:11:32 ******/
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
	[ProjectId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TargetAudience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26/07/2022 13:11:32 ******/
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
ALTER TABLE [dbo].[Cint_Attribute_DT_Config] ADD  CONSTRAINT [df_CADTC_ValueJoin]  DEFAULT ('ValueCode') FOR [ValueJoin]
GO
ALTER TABLE [dbo].[Cint_Attribute_DT_Config] ADD  DEFAULT ((0)) FOR [SplitValuesByPanelInd]
GO
ALTER TABLE [dbo].[Cint_Panel] ADD  DEFAULT ((0)) FOR [CutOver]
GO
ALTER TABLE [dbo].[CustomerFieldData]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[CustomerDetails] ([CustomerID])
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[Qualification]  WITH CHECK ADD  CONSTRAINT [FK_Qualification_TargetAudience] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[Qualification] CHECK CONSTRAINT [FK_Qualification_TargetAudience]
GO
ALTER TABLE [dbo].[Quota]  WITH CHECK ADD  CONSTRAINT [FK_Quota_TargetAudience] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[Quota] CHECK CONSTRAINT [FK_Quota_TargetAudience]
GO
ALTER TABLE [dbo].[TargetAudience]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudience_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[TargetAudience] CHECK CONSTRAINT [FK_TargetAudience_Project]
GO
/****** Object:  StoredProcedure [dbo].[CallCintandLoadCategories]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CallCintandLoadCategories] 
AS
BEGIN
	Declare @Url nvarchar(500)
		,@UrlPath nvarchar(500)
		,@XApiKey nvarchar(1000)
		,@Authorization nvarchar(1000)
		,@FullUrl nvarchar(1000)
		,@Json nvarchar(max)    
		,@ResponseStatusCode INT =0
		,@ResponsestatusText nvarchar(4000) = null 

	Select @Url = Url, @XApiKey = Header_xapikey, @Authorization = Header_Authorization
	FROM dbo.CintApiInfo ca where ca.Name = 'CintJson'

	SET @UrlPath = '/ordering/Reference/Survey/Categories'
	SET @FullUrl = @Url + @UrlPath

	PRINT @FullUrl + '  ' + @Authorization + '  ' + @XApiKey

	SET @Json = N'';

	EXEC dbo.CallCintAPIJson @xapikey = @XApiKey    
   ,@authorization = @Authorization    
   ,@fullURL = @FullUrl    
   ,@ResponseJSON = @Json OUTPUT
   ,@ResponseStatusCode=@ResponseStatusCode OUTPUT
   ,@ResponsestatusText=@ResponsestatusText OUTPUT

   IF (@ResponseStatusCode not in(400,401,403,404,405,406,409,412,422,429,500,301,304)) 
   BEGIN
	EXEC dbo.LoadCategories @JsonResponse = @Json
   END

   PRINT 'JSON Response length is : ' + CONVERT(varchar(10), LEN(@Json));
   PRINT 'Status Code is: ' + CONVERT(varchar(10),@ResponseStatusCode) + '  ' + @ResponsestatusText

END
GO
/****** Object:  StoredProcedure [dbo].[CallCintandLoadCountryRegionTypeRegion]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CallCintandLoadCountryRegionTypeRegion] 
AS
BEGIN
	Declare @Url nvarchar(500)
		,@UrlPath nvarchar(500)
		,@XApiKey nvarchar(1000)
		,@Authorization nvarchar(1000)
		,@FullUrl nvarchar(1000)
		,@Json nvarchar(max)    
		,@ResponseStatusCode INT =0
		,@ResponsestatusText nvarchar(4000) = null 

	Select @Url = Url, @XApiKey = Header_xapikey, @Authorization = Header_Authorization
	FROM dbo.CintApiInfo ca where ca.Name = 'CintJson'

	SET @UrlPath = '/ordering/reference/countries'
	SET @FullUrl = @Url + @UrlPath

	PRINT @FullUrl + '  ' + @Authorization + '  ' + @XApiKey

	SET @Json = N'';

	EXEC dbo.CallCintAPIJson @xapikey = @XApiKey    
   ,@authorization = @Authorization    
   ,@fullURL = @FullUrl    
   ,@ResponseJSON = @Json OUTPUT
   ,@ResponseStatusCode=@ResponseStatusCode OUTPUT
   ,@ResponsestatusText=@ResponsestatusText OUTPUT

   IF (@ResponseStatusCode not in(400,401,403,404,405,406,409,412,422,429,500,301,304)) 
   BEGIN
	EXEC dbo.LoadCountriesRegionTypesRegions @JsonResponse = @Json
   END

   PRINT 'JSON Response length is : ' + CONVERT(varchar(10), LEN(@Json));
   PRINT 'Status Code is: ' + CONVERT(varchar(10),@ResponseStatusCode) + '  ' + @ResponsestatusText

END
GO
/****** Object:  StoredProcedure [dbo].[CallCintandLoadGlobalQandAs]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CallCintandLoadGlobalQandAs] 
AS
BEGIN
	Declare @Url nvarchar(500)
		,@UrlPath nvarchar(500)
		,@XApiKey nvarchar(1000)
		,@Authorization nvarchar(1000)
		,@FullUrl nvarchar(1000)
		,@Json nvarchar(max)    
		,@ResponseStatusCode INT =0
		,@ResponsestatusText nvarchar(4000) = null 

	Select @Url = Url, @XApiKey = Header_xapikey, @Authorization = Header_Authorization
	FROM dbo.CintApiInfo ca where ca.Name = 'CintJson'

	SET @UrlPath = '/ordering/reference/questions'
	SET @FullUrl = @Url + @UrlPath

	PRINT @FullUrl + '  ' + @Authorization + '  ' + @XApiKey

	SET @Json = N'';

	EXEC dbo.CallCintAPIJson @xapikey = @XApiKey    
   ,@authorization = @Authorization    
   ,@fullURL = @FullUrl    
   ,@ResponseJSON = @Json OUTPUT
   ,@ResponseStatusCode=@ResponseStatusCode OUTPUT
   ,@ResponsestatusText=@ResponsestatusText OUTPUT

   IF (@ResponseStatusCode not in(400,401,403,404,405,406,409,412,422,429,500,301,304)) 
   BEGIN
	EXEC dbo.LoadGlobalQuestionsAnswers @JsonResponse = @Json
   END

   PRINT 'JSON Response length is : ' + CONVERT(varchar(10), LEN(@Json));
   PRINT 'Status Code is: ' + CONVERT(varchar(10),@ResponseStatusCode) + '  ' + @ResponsestatusText

END
GO
/****** Object:  StoredProcedure [dbo].[CallCintandLoadQandAsByCountry]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CallCintandLoadQandAsByCountry] 
	@CountryId int
AS
BEGIN
	Declare @Url nvarchar(500)
		,@UrlPath nvarchar(500)
		,@XApiKey nvarchar(1000)
		,@Authorization nvarchar(1000)
		,@FullUrl nvarchar(1000)
		,@Json nvarchar(max)    
		,@ResponseStatusCode INT =0
		,@ResponsestatusText nvarchar(4000) = null 

	Select @Url = Url, @XApiKey = Header_xapikey, @Authorization = Header_Authorization
	FROM dbo.CintApiInfo ca where ca.Name = 'CintJson'

	SET @UrlPath = '/ordering/reference/questions?countryId=' + CONVERT(varchar(10), @CountryId)
	SET @FullUrl = @Url + @UrlPath

	PRINT @FullUrl + '  ' + @Authorization + '  ' + @XApiKey

	SET @Json = N'';

	EXEC dbo.CallCintAPIJson @xapikey = @XApiKey    
   ,@authorization = @Authorization    
   ,@fullURL = @FullUrl    
   ,@ResponseJSON = @Json OUTPUT
   ,@ResponseStatusCode=@ResponseStatusCode OUTPUT
   ,@ResponsestatusText=@ResponsestatusText OUTPUT

   IF (@ResponseStatusCode not in(400,401,403,404,405,406,409,412,422,429,500,301,304)) 
   BEGIN
	EXEC dbo.LoadQuestionsAnswersByCountry @JsonResponse = @Json, @CountryId = @CountryId
   END

   PRINT 'JSON Response length is : ' + CONVERT(varchar(10), LEN(@Json));
   PRINT 'Status Code is: ' + CONVERT(varchar(10),@ResponseStatusCode) + '  ' + @ResponsestatusText

END
GO
/****** Object:  StoredProcedure [dbo].[CallCintAPIJson]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CallCintAPIJson] 
	( @key varchar(1000) = null
	, @secret varchar(1000) = null
	, @xapikey varchar(1000) = null
	, @authorization nvarchar(1000) = null
	, @fullURL nvarchar(max) = null
	, @URL_base nvarchar(max)  = null 
	, @URL_extension nvarchar(max) = null
	, @HTTPMethod varchar(20) = 'get'
	, @RequestBody nvarchar(4000) = null
	, @ResponseJSON nvarchar(max) output
	, @ResponseStatusCode int=200 output
	, @ResponsestatusText nvarchar(4000) = N'' output
	) 
AS 
BEGIN

	PRINT 'INSIDE Call Cint API Json - ' + @HTTPMethod + @fullURL + @xapikey + @authorization;
	declare @startdtm datetime2 = sysdatetime()
	Declare @url nvarchar(max) 
	Declare @Object as Int;
	DECLARE @hr  int
	Declare @json as table(Json_Table nvarchar(max))


	if @fullURL is not null 
	set @url = @fullURL 
	else
	set @url =   @URL_base +  @URL_extension

	--PRINT 'URL set to: ' + @url;

	IF OBJECT_ID('tempdb..#json') IS NOT NULL DROP TABLE #json
	CREATE TABLE #json ( ResponseJSON nvarchar(max) )
	DECLARE @HTTPStatus int 
	--Code Snippet
	Exec @hr = sp_OACreate 'MSXML2.ServerXMLHttp', @Object OUT;
	-- 1 ms
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object 

	-- PRINT 'server xml http crossed';

	Exec @hr = sp_OAMethod @Object, 'open', NULL, @httpMethod, @url, 'false'
	--1 ms
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object 

	EXEC @hr = sp_OAMethod @Object, 'setRequestHeader', null, 'x-api-key', @xapikey 
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object 
	--1ms

	EXEC @hr = sp_OAMethod @Object, 'setRequestHeader', null, 'Authorization', @authorization
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object 
	--1ms

	PRINT 'URL and header added';
	Exec @hr = sp_OAMethod @Object, 'Send'
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object 

	-- PRINT 'SEND success';

	EXEC @hr = sp_OAMethod @Object, 'responseText', @json OUTPUT
	IF @hr <> 0 EXEC sp_OAGetErrorInfo @Object

	INSERT into @json (Json_Table) exec sp_OAGetProperty @Object, 'responseText'
	EXEC sp_OAGetProperty @Object, 'Status', @ResponseStatusCode OUT
	EXEC sp_OAGetProperty @Object, 'StatusText', @ResponsestatusText OUT
	
	--Select * from @json
	select @ResponseJSON = Json_Table from @json
	-- PRINT 'length is' + CONVERT(varchar(10), Len(@ResponseJSON))

	EXEC sp_OADestroy @Object
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllTableRecords]    Script Date: 26/07/2022 13:11:32 ******/
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

	Delete FROM [ISEdb].[dbo].[QuotaGroupQuota]
	Delete FROM [ISEdb].[dbo].[Quota]
	Delete FROM [ISEdb].[dbo].[TargetAudienceQuotaGroup]
	Delete FROM [ISEdb].[dbo].[QuotaGroup]
	Delete FROM [ISEdb].[dbo].[TargetAudienceQualification]
	Delete FROM [ISEdb].[dbo].[Qualification]
	Delete FROM [ISEdb].[dbo].[ProjectTargetAudience]
	Delete FROM [ISEdb].[dbo].[TargetAudience]
	Delete FROM [ISEdb].[dbo].[Project]
	Delete FROM [ISEdb].[dbo].[User]
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllQAs]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllQAs]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select a.QuestionID, a.QuestionName, a.QuestionText, a.QuestionCategory, b.ValueCode, b.ValueText
	  from GlobalQuestions (nolock) a
	  left join GlobalAnswers (nolock) b on a.QuestionID = b.QuestionID
	  order by a.QuestionID, b.ValueCode
END
GO
/****** Object:  StoredProcedure [dbo].[GetAttributeGroups]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAttributeGroups]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT(AttributeGroup) FROM [ISEdb].[dbo].[Cint_PanelAttribute]
END
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCategories]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [ISEdb].[dbo].[Category] order by name
END
GO
/****** Object:  StoredProcedure [dbo].[GetCountries]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCountries]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, Name FROM [ISEdb].[dbo].[Country] order by Name
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfileCategories]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProfileCategories]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Distinct(QuestionCategory) from GlobalQuestions order by QuestionCategory
END
GO
/****** Object:  StoredProcedure [dbo].[GetQAsByCategory]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetQAsByCategory]
	@Category nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select a.QuestionID, a.QuestionName, a.QuestionText, a.QuestionCategory, b.ValueCode, b.ValueText
	  from GlobalQuestions (nolock) a
	  left join GlobalAnswers (nolock) b on a.QuestionID = b.QuestionID
	  where a.QuestionCategory = @Category
	  order by a.QuestionID, b.ValueCode
END
GO
/****** Object:  StoredProcedure [dbo].[LoadCategories]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoadCategories] 
	-- Add the parameters for the stored procedure here
	@JsonResponse nvarchar(max)
AS
BEGIN
	DECLARE @json NVARCHAR(MAX)
	SET @json = @JsonResponse

	MERGE INTO Category C 
	USING 
		(SELECT Id, Name 
		FROM OPENJSON (@json)
			WITH (
				Id int '$.id',
				Name nvarchar(500) '$.name'
			)) InputJson ON C.Id = InputJson.Id
	WHEN MATCHED THEN 
		UPDATE SET	C.Name  = InputJson.Name
	WHEN NOT MATCHED THEN
		INSERT (Id, Name)
		VALUES (InputJson.Id, InputJson.Name);

END
GO
/****** Object:  StoredProcedure [dbo].[LoadCountriesRegionTypesRegions]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoadCountriesRegionTypesRegions] 
	-- Add the parameters for the stored procedure here
	@JsonResponse nvarchar(max)
AS
BEGIN
	DECLARE @json NVARCHAR(MAX)
	SET @json = @JsonResponse

	MERGE INTO Country C /*(QuestionID, QuestionCategory, QuestionName, QuestionText)*/
	USING 
		(SELECT Id, Name, ValidIRMin, ValidIRMax, ValidLOIMin, ValidLOIMax, PostalCodeTargetingEnabled, AgeMin 
		FROM OPENJSON (@json)
			WITH (
				Id int '$.id',
				Name nvarchar(500) '$.name',
				ValidIRMin int '$.validIncidenceRateRange.min',
				ValidIRMax int '$.validIncidenceRateRange.max',
				ValidLOIMin int '$.validLengthOfInterviewRange.min',
				ValidLOIMax int '$.validLengthOfInterviewRange.max',
				PostalCodeTargetingEnabled bit '$.postalCodeTargetingEnabled',
				AgeMin int '$.ageMin'
			)) InputJson ON C.Id = InputJson.Id
	WHEN MATCHED THEN 
		UPDATE SET	C.Name  = InputJson.Name,
					C.ValidIRMin = InputJson.ValidIRMin,
					C.ValidIRMax = InputJson.ValidIRMax,
					C.ValidLOIMin = InputJson.ValidLOIMin,
					C.ValidLOIMax = InputJson.ValidLOIMax,
					C.PostalCodeTargetingEnabled = InputJson.PostalCodeTargetingEnabled,
					C.AgeMin = InputJson.AgeMin
	WHEN NOT MATCHED THEN
		INSERT (Id, Name, ValidIRMin, ValidIRMax, ValidLOIMin, ValidLOIMax, PostalCodeTargetingEnabled, AgeMin)
		VALUES (InputJson.Id, InputJson.Name, InputJson.ValidIRMin, InputJson.ValidIRMax, InputJson.ValidLOIMin, InputJson.ValidLOIMax, InputJson.PostalCodeTargetingEnabled, InputJson.AgeMin);

		
	MERGE INTO CountryRegionType CRT
	USING
		(SELECT Id, CountryId, Name, Active, Description 
		FROM OPENJSON ( @json )  
		WITH (   
				CountryId   int '$.id' ,  
				RegionTypes nvarchar(max) '$.regionTypes' as JSON
			)
			CROSS APPLY 
			OPENJSON(RegionTypes)
			WITH (
				Id int '$.id',
				Name nvarchar(500) '$.name',
				Active bit '$.active',
				Description nvarchar(1000) '$.description'
			)) CRTJson ON (CRT.Id = CRTJson.Id and CRT.CountryId = CRTJson.CountryId)
	WHEN MATCHED THEN
			UPDATE SET CRT.Name = CRTJson.Name,
					   CRT.Active = CRTJson.Active,
					   CRT.Description = CRTJson.Description
	WHEN NOT MATCHED THEN
			INSERT (Id, CountryId, Name, Active, Description)
			VALUES (CRTJson.Id, CRTJson.CountryId, CRTJson.Name, CRTJson.Active, CRTJson.Description);


	MERGE INTO CountryRegion CR
	USING
		(SELECT Id, RegiontypeId, CountryId, Name, Active 
		FROM OPENJSON ( @json )  
		WITH (   
				CountryId   int '$.id' ,  
				RegionTypes nvarchar(max) '$.regionTypes' as JSON
			)
			CROSS APPLY 
			OPENJSON(RegionTypes)
			WITH (
				RegionTypeId int '$.id',
				Regions nvarchar(max) '$.regions' as JSON
			)
			CROSS APPLY
			OPENJSON(Regions)
			WITH (
				Id int '$.id',
				Name nvarchar(500) '$.name',
				Active bit '$.active'
			)) CRJson ON (CR.Id = CRJson.Id and CR.CountryId = CRJson.CountryId and CR.RegionTypeId = CRJson.RegionTypeId)
	WHEN MATCHED THEN
			UPDATE SET CR.Name = CRJson.Name,
					   CR.Active = CRJson.Active
	WHEN NOT MATCHED THEN
			INSERT (Id, RegionTypeId, CountryId, Name, Active)
			VALUES (CRJson.Id, CRJson.RegionTypeId, CRJson.CountryId, CRJson.Name, CRJson.Active);
END
GO
/****** Object:  StoredProcedure [dbo].[LoadGlobalQuestionsAnswers]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoadGlobalQuestionsAnswers] 
	-- Add the parameters for the stored procedure here
	@JsonResponse nvarchar(max)
AS
BEGIN
	DECLARE @json NVARCHAR(MAX)
	SET @json = @JsonResponse

	MERGE INTO GlobalQuestions GQ /*(QuestionID, QuestionCategory, QuestionName, QuestionText)*/
	USING 
		(SELECT QuestionID, QuestionCategory, QuestionName, QuestionText 
		FROM OPENJSON (@json)
			WITH (
				QuestionID int '$.id',
				QuestionCategory nvarchar(100) '$.categoryName',
				QuestionName nvarchar(100) '$.name',
				QuestionText nvarchar(1000) '$.text'
			)) InputJson ON GQ.QuestionID = InputJson.QuestionID
	WHEN MATCHED THEN 
		UPDATE SET	GQ.QuestionCategory  = InputJson.QuestionCategory,
					GQ.QuestionName = InputJson.QuestionName,
					GQ.QuestionText = InputJson.QuestionText
	WHEN NOT MATCHED THEN
		INSERT (QuestionID, QuestionCategory, QuestionName, QuestionText)
		VALUES (InputJson.QuestionID, InputJson.QuestionCategory, InputJson.QuestionName, InputJson.QuestionText);


	MERGE INTO GlobalAnswers GA /*(CountryID, QuestionID, ValueCode, ValueText)*/
	USING
		(SELECT 999 as CountryID, QuestionID, ValueCode, ValueText 
		FROM OPENJSON ( @json )  
		WITH (   
				QuestionID   int '$.id' ,  
				Variable nvarchar(max) '$.variables' as JSON
			)
			CROSS APPLY 
			OPENJSON(Variable)
			WITH (
				ValueCode int '$.id',
				ValueText nvarchar(1000) '$.name'
			)) GAJson ON (GA.QuestionID = GAJson.QuestionID and GA.CountryID = GAJson.CountryID and GA.ValueCode = GAJson.ValueCode)
	WHEN MATCHED THEN
			UPDATE SET GA.ValueText = GAJson.ValueText
	WHEN NOT MATCHED THEN
			INSERT (CountryID, QuestionID, ValueCode, ValueText)
			VALUES (GAJSon.CountryID, GAJSon.QuestionID, GAJSon.ValueCode, GAJSon.ValueText);
END
GO
/****** Object:  StoredProcedure [dbo].[LoadQuestionsAnswersByCountry]    Script Date: 26/07/2022 13:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoadQuestionsAnswersByCountry] 
	-- Add the parameters for the stored procedure here
	@JsonResponse nvarchar(max), @CountryId int
AS
BEGIN
	DECLARE @json NVARCHAR(MAX)
	SET @json = @JsonResponse

	MERGE INTO QuestionsByCountry GQ /*(QuestionID, QuestionCategory, QuestionName, QuestionText)*/
	USING 
		(SELECT QuestionID, QuestionCategory, QuestionName, QuestionText 
		FROM OPENJSON (@json)
			WITH (
				QuestionID int '$.id',
				QuestionCategory nvarchar(100) '$.categoryName',
				QuestionName nvarchar(100) '$.name',
				QuestionText nvarchar(1000) '$.text'
			)) InputJson ON GQ.QuestionID = InputJson.QuestionID
	WHEN MATCHED THEN 
		UPDATE SET	GQ.QuestionCategory  = InputJson.QuestionCategory,
					GQ.QuestionName = InputJson.QuestionName,
					GQ.QuestionText = InputJson.QuestionText
	WHEN NOT MATCHED THEN
		INSERT (QuestionID, QuestionCategory, QuestionName, QuestionText)
		VALUES (InputJson.QuestionID, InputJson.QuestionCategory, InputJson.QuestionName, InputJson.QuestionText);


	MERGE INTO AnswersByCountry GA /*(CountryID, QuestionID, ValueCode, ValueText)*/
	USING
		(SELECT @CountryId as CountryID, QuestionID, ValueCode, ValueText 
		FROM OPENJSON ( @json )  
		WITH (   
				QuestionID   int '$.id' ,  
				Variable nvarchar(max) '$.variables' as JSON
			)
			CROSS APPLY 
			OPENJSON(Variable)
			WITH (
				ValueCode int '$.id',
				ValueText nvarchar(1000) '$.name'
			)) GAJson ON (GA.QuestionID = GAJson.QuestionID and GA.CountryID = GAJson.CountryID and GA.ValueCode = GAJson.ValueCode)
	WHEN MATCHED THEN
			UPDATE SET GA.ValueText = GAJson.ValueText
	WHEN NOT MATCHED THEN
			INSERT (CountryID, QuestionID, ValueCode, ValueText)
			VALUES (GAJSon.CountryID, GAJSon.QuestionID, GAJSon.ValueCode, GAJSon.ValueText);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_createProject]    Script Date: 26/07/2022 13:11:32 ******/
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
