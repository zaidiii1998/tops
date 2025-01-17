USE [HU_TOPS]
GO
/****** Object:  Table [dbo].[Board]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Board](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Board] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BoardGroups]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoardGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[BoardId] [int] NULL,
 CONSTRAINT [PK_BoardGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[StateId] [tinyint] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Photograph] [varchar](100) NULL,
	[SSCMarkSheet] [varchar](100) NULL,
	[HSSCMarkSheet] [varchar](100) NULL,
	[CNIC] [varchar](100) NULL,
	[IsCompleted] [int] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Educational]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Educational](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[CurrentLevelOfEdu] [varchar](50) NULL,
	[CurrentCollege] [varchar](50) NULL,
	[CollegeAddress] [varchar](100) NULL,
	[CollegeStartDate] [int] NULL,
	[CollegeCompletionDate] [int] NULL,
	[HSSCPercentage] [varchar](50) NULL,
	[BoardOfEdu] [varchar](50) NULL,
	[BoardName] [varchar](50) NULL,
	[GroupOfStudy] [varchar](50) NULL,
	[GroupName] [varchar](50) NULL,
	[SchoolName] [varchar](50) NULL,
	[SchoolAddress] [varchar](100) NULL,
	[SSCPercentage] [varchar](50) NULL,
	[UniversityName] [varchar](50) NULL,
	[IntendedProgram] [varchar](50) NULL,
	[IsCompleted] [int] NULL,
 CONSTRAINT [PK_EducationalInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationalSubjects]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationalSubjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[ObtainMarks] [int] NULL,
	[TotalMarks] [int] NULL,
	[Grade] [varchar](50) NULL,
	[EducationalId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupSubjects]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupSubjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_GroupSubjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[CreatedDatetime] [datetime2](7) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalInformation]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HUTopId] [varchar](50) NOT NULL,
	[AppId] [int] NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[FatherFirstName] [varchar](50) NULL,
	[FatherMiddleName] [varchar](50) NULL,
	[FatherLastName] [varchar](50) NULL,
	[Gender] [varchar](10) NULL,
	[HusbandName] [varchar](50) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[CountyAdd] [varchar](50) NULL,
	[CellPhoneNumber] [varchar](15) NOT NULL,
	[AlternateCellPhoneNumber] [varchar](15) NULL,
	[TellUs] [varchar](50) NULL,
	[ResidentialAddress] [varchar](100) NULL,
	[ResidentialCity] [varchar](50) NULL,
	[ResidentialCityOther] [varchar](50) NULL,
	[ResidentialProvince] [varchar](50) NULL,
	[ResidentialCountry] [varchar](50) NULL,
	[ResidentialPostalCode] [int] NULL,
	[PermanentAddress] [varchar](100) NULL,
	[PermanentCity] [varchar](50) NULL,
	[PermanentCityOther] [varchar](50) NULL,
	[PermanentProvince] [varchar](50) NULL,
	[PermanentCountry] [varchar](50) NULL,
	[PermanentPostalCode] [int] NULL,
	[CNIC] [varchar](15) NOT NULL,
	[EmailAddress] [varchar](30) NOT NULL,
	[AlterEmailAddress] [varchar](30) NULL,
	[Password] [varbinary](50) NOT NULL,
	[SaveDate] [datetime2](7) NULL,
	[UpdateDate] [datetime2](7) NULL,
	[AppStatus] [int] NULL,
	[StudentStatus] [int] NULL,
	[TempId] [int] NULL,
	[School] [varchar](2) NULL,
	[SchoolCode] [int] NULL,
	[SchoolName] [varchar](50) NULL,
	[SchoolNameOther] [varchar](50) NULL,
	[CurrentQualification] [varchar](50) NULL,
	[CurrentQualificationOther] [varchar](50) NULL,
	[AlternateLandline] [varchar](15) NULL,
	[UserId] [int] NULL,
	[TestDate] [datetime2](7) NULL,
	[SubmissionDate] [datetime2](7) NULL,
	[Retake] [int] NULL,
	[IsAppliedBefore] [int] NULL,
	[AppliedBeforeMonth] [int] NULL,
	[AppliedBeforeYear] [int] NULL,
	[HomePhoneNumber] [varchar](15) NULL,
	[WhatsAppNumber] [varchar](15) NULL,
	[GuardianCellPhoneNumber] [varchar](12) NULL,
	[GuardianEmailAddress] [varchar](50) NULL,
	[HearAboutHU] [varchar](50) NULL,
	[HearAboutHUOther] [varchar](50) NULL,
	[CreatedDatetime] [datetime2](7) NULL,
	[Declaration] [int] NULL,
	[IsCompleted] [int] NULL,
 CONSTRAINT [PK_PersonalInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](25) NOT NULL,
	[CountryId] [tinyint] NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Board] ON 

INSERT [dbo].[Board] ([Id], [Name]) VALUES (1, N'Board Of Intermediate')
INSERT [dbo].[Board] ([Id], [Name]) VALUES (2, N'Federal Board')
INSERT [dbo].[Board] ([Id], [Name]) VALUES (3, N'Aga Khan University Examination Board')
SET IDENTITY_INSERT [dbo].[Board] OFF
GO
SET IDENTITY_INSERT [dbo].[BoardGroups] ON 

INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (1, N'Pre-Medical', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (2, N'Pre-Engineering', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (3, N'Computer Science', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (4, N'Commerce', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (5, N'General Science', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (6, N'Arts/Humanities Group:', 2)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (7, N'Pre-Medical', 3)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (8, N'Pre-Engineering', 3)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (9, N'Commerce', 3)
INSERT [dbo].[BoardGroups] ([Id], [Name], [BoardId]) VALUES (10, N'Humanities', 3)
SET IDENTITY_INSERT [dbo].[BoardGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (172, N'Abbottabad', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (173, N'Adezai', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (57, N'Ahmadpur East', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (58, N'Ahmed Nager Chatha', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (175, N'Akora Khattak', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (59, N'Ali Khan Abad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (60, N'Alipur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (174, N'Alpuri', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (61, N'Arifwala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (218, N'Askole', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (219, N'Astore', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (62, N'Attock', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (176, N'Ayubia', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (1, N'Badin', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (252, N'Bagh', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (65, N'Bahawalnagar', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (66, N'Bahawalpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (177, N'Banda Daud Shah', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (178, N'Bannu', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (179, N'Batkhela', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (180, N'Battagram', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (67, N'Bhakkar', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (64, N'Bhalwal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (63, N'Bhera', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (254, N'Bhimbar', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (2, N'Bhirkan', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (181, N'Birote', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (220, N'Bunji', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (68, N'Burewala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (4, N'Chak', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (182, N'Chakdara', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (256, N'Chakswari', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (70, N'Chakwal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (231, N'Chalt', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (158, N'Chaman', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (183, N'Charsadda', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (71, N'Chichawatni', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (221, N'Chilas', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (69, N'Chillianwala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (222, N'Chillinji', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (72, N'Chiniot', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (223, N'Chiran', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (73, N'Chishtian', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (184, N'Chitral', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (5, N'Dadu', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (185, N'Daggar', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (186, N'Dargai', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (75, N'Darya Khan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (74, N'Daska', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (164, N'Dera Allah Yar', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (76, N'Dera Ghazi Khan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (188, N'Dera Ismail Khan', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (163, N'Dera Murad Jamali', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (77, N'Dhaular', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (251, N'Dhirkot', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (6, N'Digri', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (78, N'Dina', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (79, N'Dinga', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (80, N'Dipalpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (7, N'Diplo', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (190, N'Dir', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (189, N'Doaba', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (8, N'Dokri', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (191, N'Drosh', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (81, N'Faisalabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (82, N'Fateh Jang', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (224, N'Gakuch', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (83, N'Ghakhar Mandi', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (225, N'Ghangche', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (226, N'Ghizer', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (9, N'Ghotki', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (235, N'Ghulmat', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (227, N'Gilgit', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (84, N'Gojra', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (228, N'Gorikot', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (87, N'Gujar Khan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (85, N'Gujranwala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (86, N'Gujrat', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (229, N'Gulmit', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (162, N'Gwadar', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (10, N'Haala', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (88, N'Hafizabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (253, N'Hajira', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (192, N'Hangu', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (193, N'Haripur', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (89, N'Haroonabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (90, N'Hasilpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (91, N'Haveli Lakha', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (159, N'Hub', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (11, N'Hyderabad', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (237, N'Ishkoman', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (12, N'Islamkot', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (13, N'Jacobabad', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (230, N'Jaglot', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (92, N'Jalalpur Jattan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (93, N'Jampur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (14, N'Jamshoro', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (94, N'Jaranwala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (107, N'Jauharabad', 1, 2)
GO
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (95, N'Jhang', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (96, N'Jhelum', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (15, N'Jungshahi', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (97, N'Kalabagh', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (171, N'Kalat', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (100, N'Kamalia', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (101, N'Kamoke', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (16, N'Kandhkot', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (17, N'Kandiaro', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (18, N'Karachi', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (194, N'Karak', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (236, N'Karimabad', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (98, N'Karor Lal Esan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (19, N'Kashmore', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (99, N'Kasur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (20, N'Keti Bandar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (21, N'Khairpur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (102, N'Khanewal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (103, N'Khanpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (238, N'Khaplu', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (168, N'Kharan', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (104, N'Kharian', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (105, N'Khushab', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (156, N'Khuzdar', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (195, N'Kohat', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (106, N'Kot Adu', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (250, N'Kotli', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (22, N'Kotri', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (196, N'Kulachi', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (108, N'Lahore', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (197, N'Lakki Marwat', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (109, N'Lalamusa', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (23, N'Larkana', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (198, N'Latamber', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (110, N'Layyah', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (111, N'Liaquat Pur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (112, N'Lodhran', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (166, N'Loralai', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (199, N'Madyan', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (115, N'Mailsi', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (113, N'Malakwal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (114, N'Mamoori', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (116, N'Mandi Bahauddin', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (200, N'Mansehra', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (201, N'Mardan', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (202, N'Mastuj', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (169, N'Mastung', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (24, N'Matiari', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (25, N'Mehar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (29, N'Mehrabpur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (117, N'Mian Channu', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (118, N'Mianwali', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (122, N'Mianwali Bangla', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (203, N'Mingora', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (239, N'Minimerg', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (248, N'Mirpur', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (26, N'Mirpur Khas', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (240, N'Misgar', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (27, N'Mithani', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (28, N'Mithi', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (30, N'Moro', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (119, N'Multan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (121, N'Muridke', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (120, N'Murree', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (247, N'Muzaffarabad', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (123, N'Muzaffargarh', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (234, N'Nagar Proper', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (31, N'Nagarparkar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (124, N'Narowal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (32, N'Naudero', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (33, N'Naushahro Feroze', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (34, N'Naushara', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (35, N'Nawabshah', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (36, N'Nazimabad', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (204, N'Nowshera', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (170, N'Nushki', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (125, N'Okara', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (206, N'Pabbi', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (205, N'Paharpur', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (127, N'Pakpattan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (255, N'Palandri', 1, 6)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (167, N'Pasni', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (241, N'Passu', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (128, N'Pattoki', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (207, N'Peshawar', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (129, N'Pir Mahal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (130, N'Qaimpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (37, N'Qambar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (38, N'Qasimabad', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (233, N'Qasimabad Masoot', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (131, N'Qila Didar Singh', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (155, N'Quetta', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (132, N'Rabwah', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (135, N'Rahim Yar Khan', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (133, N'Raiwind', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (134, N'Rajanpur', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (3, N'Rajo Khanani', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (39, N'Ranipur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (40, N'Ratodero', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (249, N'Rawala Kot', 1, 6)
GO
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (136, N'Rawalpindi', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (126, N'Renala Khurd', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (41, N'Rohri', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (137, N'Sadiqabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (138, N'Safdarabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (139, N'Sahiwal', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (208, N'Saidu Sharif', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (42, N'Sakrand', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (43, N'Sanghar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (140, N'Sangla Hill', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (141, N'Sarai Alamgir', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (142, N'Sargodha', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (44, N'Shahbandar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (45, N'Shahdadkot', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (46, N'Shahdadpur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (47, N'Shahpur Chakar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (143, N'Shakargarh', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (144, N'Sheikhupura', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (210, N'Shewa Adda', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (48, N'Shikarpaur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (242, N'Shimshal', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (209, N'Shorkot', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (145, N'Sialkot', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (160, N'Sibi', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (148, N'Siranwali', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (243, N'Skardu', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (146, N'Sohawa', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (147, N'Soianwala', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (49, N'Sukkur', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (244, N'Sust', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (211, N'Swabi', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (212, N'Swat', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (245, N'Taghafari', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (149, N'Talagang', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (51, N'Tando Adam Khan', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (52, N'Tando Allahyar', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (53, N'Tando Muhammad Khan', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (213, N'Tangi', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (50, N'Tangwani', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (214, N'Tank', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (150, N'Taxila', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (215, N'Thall', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (54, N'Thatta', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (232, N'Thole', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (246, N'Thorar', 1, 5)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (216, N'Timergara', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (151, N'Toba Tek Singh', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (217, N'Tordher', 1, 4)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (157, N'Turbat', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (55, N'Umerkot', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (165, N'Usta Mohammad', 1, 3)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (152, N'Vehari', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (153, N'Wah Cantonment', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (56, N'Warah', 1, 1)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (154, N'Wazirabad', 1, 2)
INSERT [dbo].[City] ([Id], [Name], [Status], [StateId]) VALUES (161, N'Zhob', 1, 3)
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Name], [Status]) VALUES (1, N'Pakistan', 1)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Documents] ON 

INSERT [dbo].[Documents] ([Id], [UserId], [Photograph], [SSCMarkSheet], [HSSCMarkSheet], [CNIC], [IsCompleted]) VALUES (1, 1, N'C:\Users\muhammad.zaid\Source\Repos\HUTOPS\HUTOPS\HUTOPS\UploadedFiles\cnic - Copy (2).jpg', N'C:\Users\muhammad.zaid\Source\Repos\HUTOPS\HUTOPS\HUTOPS\UploadedFiles\cnic.jpg', N'', N'', 1)
SET IDENTITY_INSERT [dbo].[Documents] OFF
GO
SET IDENTITY_INSERT [dbo].[Educational] ON 

INSERT [dbo].[Educational] ([Id], [UserId], [CurrentLevelOfEdu], [CurrentCollege], [CollegeAddress], [CollegeStartDate], [CollegeCompletionDate], [HSSCPercentage], [BoardOfEdu], [BoardName], [GroupOfStudy], [GroupName], [SchoolName], [SchoolAddress], [SSCPercentage], [UniversityName], [IntendedProgram], [IsCompleted]) VALUES (24, 1, N'Already enrolled in a University', N'PNPI', N'tower khi', 2019, 2023, N'45.64%', N'2', N'Federal Board', N'3', N'Computer Science', N'Alies Academy', N'lines area khi', N'45.64%', NULL, N'BS Computer Engineering', 1)
SET IDENTITY_INSERT [dbo].[Educational] OFF
GO
SET IDENTITY_INSERT [dbo].[EducationalSubjects] ON 

INSERT [dbo].[EducationalSubjects] ([Id], [Name], [ObtainMarks], [TotalMarks], [Grade], [EducationalId]) VALUES (93, N'Computer Science', 0, 0, N'A', 24)
INSERT [dbo].[EducationalSubjects] ([Id], [Name], [ObtainMarks], [TotalMarks], [Grade], [EducationalId]) VALUES (94, N'Mathematics', 0, 0, N'A', 24)
INSERT [dbo].[EducationalSubjects] ([Id], [Name], [ObtainMarks], [TotalMarks], [Grade], [EducationalId]) VALUES (95, N'Physics', 0, 0, N'A', 24)
INSERT [dbo].[EducationalSubjects] ([Id], [Name], [ObtainMarks], [TotalMarks], [Grade], [EducationalId]) VALUES (96, N'English', 0, 0, N'A', 24)
SET IDENTITY_INSERT [dbo].[EducationalSubjects] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupSubjects] ON 

INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (1, N'Biology', 1)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (2, N'Chemistry', 1)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (3, N'Physics', 1)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (4, N'English', 1)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (5, N'Mathematics', 2)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (6, N'Chemistry', 2)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (7, N'Physics', 2)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (8, N'English', 2)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (9, N'Computer Science', 3)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (10, N'Mathematics', 3)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (11, N'Physics', 3)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (12, N'English', 3)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (13, N'Accounting', 4)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (14, N'Economics', 4)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (15, N'Business Studies', 4)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (16, N'English', 4)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (17, N'History', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (18, N'Geography', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (19, N'Sociology', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (20, N'Psychology', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (21, N'Urdu', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (22, N'English', 6)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (23, N'Physics', 5)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (24, N'Chemistry', 5)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (25, N'Biology', 5)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (26, N'Mathematics', 5)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (27, N'Biology', 7)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (28, N' Chemistry', 7)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (29, N' Physics', 7)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (30, N' English', 7)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (31, N'Mathematics', 8)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (32, N' Chemistry', 8)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (33, N' Physics', 8)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (34, N' English', 8)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (35, N'Accounting', 9)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (36, N' Business Studies', 9)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (37, N' Economics', 9)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (38, N' English', 9)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (39, N'History', 10)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (40, N' Geography', 10)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (41, N' Sociology', 10)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (42, N' Psychology', 10)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (43, N' Urdu', 10)
INSERT [dbo].[GroupSubjects] ([Id], [Name], [GroupId]) VALUES (44, N' English', 10)
SET IDENTITY_INSERT [dbo].[GroupSubjects] OFF
GO
SET IDENTITY_INSERT [dbo].[Log] ON 

INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (1, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-04T22:45:21.4558208' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (2, N'ActivityLog', N'User Request to Register New Application', CAST(N'2023-09-04T22:59:55.9867242' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (3, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-04T22:59:56.4528236' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (4, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-04T23:00:01.9483731' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (5, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-04T23:03:29.5262806' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (6, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T09:44:48.1536918' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (7, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T09:45:20.0456448' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (8, N'ActivityLog', N'User Request to Register New Application Details: Firstname: , LastName: , Email: , CNIC: , PhoneNumber:  ', CAST(N'2023-09-05T09:45:32.8405452' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (9, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T09:45:33.5871038' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (10, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T09:45:33.5941032' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (11, N'ActivityLog', N'User Request to Register New Application Details: Firstname: B, LastName: B, Email: , CNIC: , PhoneNumber:  ', CAST(N'2023-09-05T09:46:09.4164027' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (12, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T09:46:09.4346249' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (13, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T09:46:09.4422663' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (14, N'ActivityLog', N'User Request to Register New Application Details: Firstname: , LastName: , Email: , CNIC: , PhoneNumber:  ', CAST(N'2023-09-05T09:47:43.6143568' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (15, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T09:47:43.6302107' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (16, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T09:47:43.6343901' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (17, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T09:53:10.9049140' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (18, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T09:53:13.4164897' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (19, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T09:54:20.7565979' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (20, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:02:56.0014196' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (21, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:03:10.8041662' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (22, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:04:03.6724100' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (23, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:04:10.3381079' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (24, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:05:32.9312127' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (25, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:07:12.5994873' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (26, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:08:03.4571062' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (27, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:08:23.4133653' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (28, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:09:43.2241440' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (29, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:11:51.6848662' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (30, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:11:55.2655585' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (31, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:12:03.6337611' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (32, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:12:24.2078626' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (33, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:12:33.2472260' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (34, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:12:42.8018548' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (35, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:13:24.8712096' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (36, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:13:30.5377337' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (37, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:31.9617663' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (38, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:40.1551561' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (39, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:49.1496443' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (40, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:53.3349402' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (41, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:54.8775621' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (42, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:14:56.4911410' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (43, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:17:42.9275676' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (44, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:19:34.3569473' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (45, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:19:41.4349499' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (46, N'ActivityLog', N'User Request to Register New Application Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@gmail.com, CNIC: 42301-0412887-8, PhoneNumber: 0304-1020473 ', CAST(N'2023-09-05T10:20:37.8065204' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (47, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T10:20:37.8202094' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (48, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@gmail.com, CNIC: 42301-0412887-8, PhoneNumber: 0304-1020473 ', CAST(N'2023-09-05T10:20:37.8279230' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (49, N'ActivityLog', N'User account has been successfully created.UserID: 3UserNameMuhammad Zaid', CAST(N'2023-09-05T10:20:37.9375371' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (50, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:58:30.0506606' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (51, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:58:31.6958679' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (52, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T10:58:44.6970485' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (53, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:13:18.5541827' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (54, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:13:36.4824215' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (55, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T11:13:41.7414192' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (56, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:20:02.1703635' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (57, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T11:20:06.2042417' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (58, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:22:34.0736762' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (59, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:22:41.5523866' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (60, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:22:49.2872422' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (61, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T11:22:53.5552963' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (62, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T11:43:07.5069244' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (63, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T11:43:19.0438471' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (64, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T12:12:13.9940890' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (65, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T12:12:23.6112801' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (66, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T12:12:28.1692993' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (67, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T12:12:32.7780669' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (68, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T12:14:51.1648767' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (69, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T12:15:01.8301434' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (70, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T12:44:50.0798509' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (71, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T12:45:14.7551669' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (72, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T14:33:13.6162527' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (73, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T14:33:24.0803576' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (74, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T14:46:43.0801896' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (75, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T14:46:54.9982844' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (76, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:01:19.4706026' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (77, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T15:01:41.4852908' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (78, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:03:48.9079915' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (79, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T15:03:55.8647991' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (80, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:08:14.1026112' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (81, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-05T15:08:21.5629212' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (82, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:36:37.3235064' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (83, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:36:40.5304894' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (84, N'ActivityLog', N'User Request to Register New Application Details: Firstname: @#--&652, LastName: 2$$>?))8**, Email: sadaqat@yopmail.com, CNIC: 42301-0412838-9, PhoneNumber: 4654-6546546 ', CAST(N'2023-09-05T15:38:51.0077894' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (85, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:38:51.0250711' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (86, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T15:38:51.0410597' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (87, N'ActivityLog', N'User Request to Register New Application Details: Firstname: , LastName: , Email: , CNIC: , PhoneNumber:  ', CAST(N'2023-09-05T15:42:38.0432759' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (88, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:42:38.0594259' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (89, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T15:42:38.0694941' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (90, N'ActivityLog', N'User Request to Register New Application Details: Firstname: @%$>00)}}, LastName: %@%$>00)}}%, Email: zaid@itggs.com, CNIC: 00000-0000000-0, PhoneNumber: 0000-0000000 ', CAST(N'2023-09-05T15:44:09.4280320' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (91, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:44:09.4364981' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (92, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T15:44:09.4405779' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (93, N'ActivityLog', N'User Request to Register New Application Details: Firstname: @%$>00)}}, LastName: @%$>00)}}, Email: zaid@gmail.com vg, CNIC: 00000-0000000-0, PhoneNumber: 0000-0000000 ', CAST(N'2023-09-05T15:45:15.6020801' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (94, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:45:15.6230281' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (95, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: @%$>00)}}, LastName: @%$>00)}}, Email: zaid@gmail.com vg, CNIC: 00000-0000000-0, PhoneNumber: 0000-0000000 ', CAST(N'2023-09-05T15:45:15.6319668' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (96, N'ActivityLog', N'User account has been successfully created.UserID: 4UserName@%$>00)}} @%$>00)}}', CAST(N'2023-09-05T15:45:15.6858476' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (97, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:45:46.7204896' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (98, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:45:49.3431554' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (99, N'ActivityLog', N'User Request to Register New Application Details: Firstname: @%$>00)}}, LastName: @%$>00)}}, Email: ddd, CNIC: 00000-0000000-1, PhoneNumber: 0000-0000044 ', CAST(N'2023-09-05T15:46:45.5025129' AS DateTime2))
GO
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (100, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:46:45.5195324' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (101, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: @%$>00)}}, LastName: @%$>00)}}, Email: ddd, CNIC: 00000-0000000-1, PhoneNumber: 0000-0000044 ', CAST(N'2023-09-05T15:46:45.5231129' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (102, N'ActivityLog', N'User account has been successfully created.UserID: 5UserName@%$>00)}} @%$>00)}}', CAST(N'2023-09-05T15:46:45.5540989' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (103, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:50:31.0311666' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (104, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T15:50:34.7923965' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (105, N'ActivityLog', N'User Request to Register New Application Details: Firstname: d, LastName: f, Email: ee@er.com, CNIC: 44444-4444444-4, PhoneNumber: 4444-4444444 ', CAST(N'2023-09-05T15:53:38.3623718' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (106, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:53:38.3702454' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (107, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T15:53:38.3702454' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (108, N'ActivityLog', N'User Request to Register New Application Details: Firstname: 1111111111111111111111111, LastName: qqqqqqqqqqqqqqqqqqqqqqqqq, Email: abc@fds.vo, CNIC: 11111-1111111-1, PhoneNumber: 1111-1111111 ', CAST(N'2023-09-05T15:54:57.2949728' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (109, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:54:57.3094099' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (110, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T15:54:57.3181019' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (111, N'ActivityLog', N'User Request to Register New Application Details: Firstname: 0111111111111111111111111, LastName: sssssssssssssssssssssssss, Email: 2222@dd.co, CNIC: 22222-2222222-2, PhoneNumber: 2222-2222222 ', CAST(N'2023-09-05T15:56:13.1499219' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (112, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T15:56:13.1620402' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (113, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: 0111111111111111111111111, LastName: sssssssssssssssssssssssss, Email: 2222@dd.co, CNIC: 22222-2222222-2, PhoneNumber: 2222-2222222 ', CAST(N'2023-09-05T15:56:13.1681055' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (114, N'ActivityLog', N'User account has been successfully created.UserID: 6UserName0111111111111111111111111 sssssssssssssssssssssssss', CAST(N'2023-09-05T15:56:13.1937620' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (115, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:05:43.4218795' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (116, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:15:44.2910326' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (117, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:15:55.0551054' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (118, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:17:14.4518164' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (119, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:17:17.2830607' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (120, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:17:40.1618855' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (121, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:18:09.5510908' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (122, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:22:34.8970611' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (123, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:25:46.5887513' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (124, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:28:59.1815932' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (125, N'ActivityLog', N'User Request to Register New Application Details: Firstname: 123, LastName: 123, Email: za@d.f, CNIC: 42301-0411221-2, PhoneNumber: 1212-1212112 ', CAST(N'2023-09-05T16:29:31.0529305' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (126, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:29:31.0604570' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (127, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: 123, LastName: 123, Email: za@d.f, CNIC: 42301-0411221-2, PhoneNumber: 1212-1212112 ', CAST(N'2023-09-05T16:29:31.0662247' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (128, N'ActivityLog', N'User account has been successfully created.UserID: 7UserName123 123', CAST(N'2023-09-05T16:29:31.1670675' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (129, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:29:47.7010756' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (130, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:29:51.6397895' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (131, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:30:03.4311545' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (132, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:30:34.3483074' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (133, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:30:53.8740335' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (134, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:31:03.2389108' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (135, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: kklk, Email: 2323232@56.c, CNIC: 23456-7890987-6, PhoneNumber: 2323-2323323 ', CAST(N'2023-09-05T16:31:47.2597984' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (136, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:31:47.2637984' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (137, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:31:47.2652398' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (138, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:49.3059603' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (139, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:53.0941154' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (140, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:54.0446619' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (141, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:54.2902769' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (142, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:54.5292399' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (143, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:34:54.7031303' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (144, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:35:30.5291434' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (145, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:36:17.9248245' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (146, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:36:33.8751803' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (147, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:36:52.1060506' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (148, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:41:38.4159773' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (149, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: Z, Email: 1231121, CNIC: 68446-5454654-5, PhoneNumber: 0345-4545454 ', CAST(N'2023-09-05T16:42:29.9877629' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (150, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:42:29.9989252' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (151, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:42:30.0008575' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (152, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: Z, Email: 1231121, CNIC: 68446-5454654-5, PhoneNumber: 0345-4545454 ', CAST(N'2023-09-05T16:45:45.1659079' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (153, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:45:45.1795937' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (154, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:45:45.1819725' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (155, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: 12, Email: 1212121212121212121, CNIC: 21212-1212121-2, PhoneNumber: 0312-1212121 ', CAST(N'2023-09-05T16:46:44.7985810' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (156, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:46:44.8089501' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (157, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:46:44.8119505' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (158, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: 12, Email: 1212121212121212121, CNIC: 21212-1212121-2, PhoneNumber: 0312-1212121 ', CAST(N'2023-09-05T16:47:21.3550314' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (159, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:47:21.3632224' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (160, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:47:21.3655601' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (161, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: 12, Email: 1212121212121212121, CNIC: 21212-1212121-2, PhoneNumber: 0312-1212121 ', CAST(N'2023-09-05T16:51:38.5203277' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (162, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T16:51:38.5283158' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (163, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T16:51:38.5302644' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (164, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:51:43.8825026' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (165, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:55:16.6038941' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (166, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:55:37.0303634' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (167, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:55:51.9730507' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (168, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T16:59:22.4229380' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (169, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:01:29.6444281' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (170, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:02:25.7916004' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (171, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:05:58.6081580' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (172, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:06:19.2288339' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (173, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:07:26.7247617' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (174, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:08:05.9533292' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (175, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:11:00.1569627' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (176, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:12:00.9833951' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (177, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:12:29.4111206' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (178, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:12:40.6883985' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (179, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:13:12.5248363' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (180, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:13:49.4485464' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (181, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:14:23.0826063' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (182, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:15:17.3117799' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (183, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:18:35.8690396' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (184, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:20:23.7896156' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (185, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:20:29.1678221' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (186, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:20:37.5461052' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (187, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:21:24.4580480' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (188, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:21:37.6259969' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (189, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:21:52.5921762' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (190, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:22:31.3427809' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (191, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:22:35.0937534' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (192, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:23:39.6533903' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (193, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:24:35.8541343' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (194, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:26:02.6304890' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (195, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:26:11.8383644' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (196, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T17:26:22.9670028' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (197, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-05T17:26:30.9145955' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (198, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Ali', CAST(N'2023-09-05T17:27:47.3654687' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (199, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:46:25.5239506' AS DateTime2))
GO
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (200, N'ActivityLog', N'User Request to Register New Application Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@as.com, CNIC: 54654-6546546-5, PhoneNumber: 0345-4545454 ', CAST(N'2023-09-05T20:47:49.6447895' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (201, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T20:47:49.6706829' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (202, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@as.com, CNIC: 54654-6546546-5, PhoneNumber: 0345-4545454 ', CAST(N'2023-09-05T20:47:49.6738440' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (203, N'ActivityLog', N'User account has been successfully created.UserID: 8UserNameMuhammad Zaid', CAST(N'2023-09-05T20:47:49.7385932' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (204, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:48:09.0509442' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (205, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:48:11.8892304' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (206, N'ActivityLog', N'User Request to Register New Application Details: Firstname: sdf, LastName: sd, Email: dsfsdfgdgdfg, CNIC: 46546-5454654-6, PhoneNumber: 0354-5454545 ', CAST(N'2023-09-05T20:49:19.7313369' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (207, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T20:49:19.7478544' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (208, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T20:49:19.7570242' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (209, N'ActivityLog', N'User Request to Register New Application Details: Firstname: sdf, LastName: sd, Email: dsfsdfgdgdfg, CNIC: 46546-5454654-6, PhoneNumber: 0354-5454545 ', CAST(N'2023-09-05T20:50:15.5763125' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (210, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T20:50:15.5922675' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (211, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T20:50:15.6062528' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (212, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:50:19.1841341' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (213, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:50:21.5985968' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (214, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:50:56.0155156' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (215, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:51:29.4606121' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (216, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:57:15.6312339' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (217, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:57:56.5211565' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (218, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T20:58:03.2308713' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (219, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:08:48.5286920' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (220, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:10:10.1331384' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (221, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:11:25.2761794' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (222, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:14:41.1800928' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (223, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0345-4545454 ', CAST(N'2023-09-05T21:15:16.0699631' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (224, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:15:16.1008253' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (225, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:15:16.1088521' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (226, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-05T21:17:58.1128969' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (227, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:21:25.5981324' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (228, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:21:25.6483033' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (229, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-05T21:21:38.8727197' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (230, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:21:43.3927500' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (231, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:21:43.4122805' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (232, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-05T21:25:45.2365184' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (233, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:25:45.2556938' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (234, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:25:45.2616650' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (235, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-05T21:26:04.7554500' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (236, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:26:04.7814365' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (237, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:26:04.7871852' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (238, N'ActivityLog', N'User Request to Register New Application Details: Firstname: nm, LastName: n, Email: zaid@its.c, CNIC: 42301-0412838-0, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-05T21:26:23.0131614' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (239, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:26:23.0323454' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (240, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:26:23.0409497' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (241, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:31:39.0441915' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (242, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:31:44.1443490' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (243, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: kj, Email: zaid@its.com, CNIC: 42301-0412838-0, PhoneNumber: 0312-3121313 ', CAST(N'2023-09-05T21:32:35.2436318' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (244, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:32:35.2648906' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (245, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:32:35.2736620' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (246, N'ActivityLog', N'User Request to Register New Application Details: Firstname: M, LastName: kj, Email: gjhghgjhghjgjhghjg, CNIC: 42301-0412837-8, PhoneNumber: 0312-3121315 ', CAST(N'2023-09-05T21:33:42.0351521' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (247, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-05T21:33:42.0527486' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (248, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-05T21:33:42.0638752' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (249, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:35:06.8763724' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (250, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Ali', CAST(N'2023-09-05T21:35:10.8456274' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (251, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:37:40.7131320' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (252, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:37:44.2012532' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (253, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:40:21.3903262' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (254, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:44:16.0221403' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (255, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:46:44.0421957' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (256, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:47:56.8928441' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (257, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T21:50:16.9333068' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (258, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:05:02.2305430' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (259, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:05:36.6011781' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (260, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:05:46.8718183' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (261, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:06:57.9362599' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (262, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:16:31.4440102' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (263, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:17:04.2531706' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (264, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:17:26.9055808' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (265, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:19:09.4529267' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (266, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:21:07.9123956' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (267, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:27:48.2692502' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (268, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:28:29.0368926' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (269, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:33:07.2236219' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (270, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:36:44.9423142' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (271, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:38:05.2698950' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (272, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-05T22:38:43.9304409' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (273, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:22:43.8881812' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (274, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:24:03.9300569' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (275, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:25:46.0708923' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (276, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:26:53.2201297' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (277, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:27:41.1560029' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (278, N'ActivityLog', N'User Request to Register New Application Details: Firstname: abc, LastName: acb, Email: 2222@dd.co, CNIC: 22222-2222222-2, PhoneNumber: 0322-2222222 ', CAST(N'2023-09-06T09:29:13.8362618' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (279, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:29:13.8467476' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (280, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: abc, LastName: acb, Email: 2222@dd.co, CNIC: 22222-2222222-2, PhoneNumber: 0322-2222222 ', CAST(N'2023-09-06T09:29:13.8549322' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (281, N'ActivityLog', N'User account has been successfully created.UserID: 9UserNameabc acb', CAST(N'2023-09-06T09:29:13.9841274' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (282, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:29:19.8440722' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (283, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:29:24.6452580' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (284, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:35:03.6125611' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (285, N'ActivityLog', N'User Request to Register New Application Details: Firstname: asd, LastName: ghj, Email: sdfd, CNIC: 45454-4545454-5, PhoneNumber: 0345-4545456 ', CAST(N'2023-09-06T09:36:15.9491312' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (286, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:36:15.9653645' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (287, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-06T09:36:15.9708708' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (288, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:36:24.0195749' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (289, N'ActivityLog', N'User Request to Register New Application Details: Firstname: jhk, LastName: hjh, Email: sadf, CNIC: 46546-5456454-6, PhoneNumber: 0345-6454564 ', CAST(N'2023-09-06T09:37:00.8016416' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (290, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:37:00.8161938' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (291, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-06T09:37:00.8260714' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (292, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:37:31.9728685' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (293, N'ActivityLog', N'User Request to Register New Application Details: Firstname: hkj, LastName: jkj, Email: sad, CNIC: 54654-5646546-5, PhoneNumber: 0345-6545454 ', CAST(N'2023-09-06T09:38:00.6144882' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (294, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:38:00.6275562' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (295, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-06T09:38:00.6361029' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (296, N'ActivityLog', N'User Request to Register New Application Details: Firstname: hkj, LastName: jkj, Email: sad, CNIC: 54654-5646546-5, PhoneNumber: 0345-6545454 ', CAST(N'2023-09-06T09:38:39.5121063' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (297, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:38:39.5389517' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (298, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-06T09:38:39.5439980' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (299, N'ActivityLog', N'User Request to Register New Application Details: Firstname: hkj, LastName: jkj, Email: sad, CNIC: 54654-5646546-5, PhoneNumber: 0345-6545454 ', CAST(N'2023-09-06T09:39:15.7850898' AS DateTime2))
GO
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (300, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:39:15.7964185' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (301, N'ActivityLog', N'User-provided data is Invalid', CAST(N'2023-09-06T09:39:15.8050216' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (302, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:42:19.8852295' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (303, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Ali', CAST(N'2023-09-06T09:42:47.7231484' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (304, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:48:23.9988056' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (305, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:48:26.5233277' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (306, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:49:29.9708583' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (307, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:49:36.0323179' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (308, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:51:22.8219082' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (309, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:55:23.9303466' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (310, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T09:55:29.0124186' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (311, N'ActivityLog', N'User Request to Register New Application Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@its.com, CNIC: 42301-0412838-7, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-06T09:55:56.9210376' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (312, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T09:55:56.9371103' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (313, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@its.com, CNIC: 42301-0412838-7, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-06T09:55:56.9436243' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (314, N'ActivityLog', N'User account has been successfully created.UserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T09:55:57.0494478' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (315, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T10:28:28.5746457' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (316, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T10:28:29.7237788' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (317, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T10:28:37.6019070' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (318, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T11:21:41.8378819' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (319, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T11:21:53.9430177' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (320, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T12:36:16.5311515' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (321, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T12:36:27.5959977' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (322, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T12:40:29.9229632' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (323, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T12:40:39.0071682' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (324, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T12:55:43.4094809' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (325, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T12:55:52.5829633' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (326, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T12:57:27.0561767' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (327, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T12:57:36.4570256' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (328, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T13:05:37.1045899' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (329, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T13:06:56.8733427' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (330, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T13:12:47.6731369' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (331, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T14:49:13.8743783' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (332, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T15:57:03.2184719' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (333, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T15:57:14.4989152' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (334, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:03:15.6434402' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (335, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T16:04:15.4352369' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (336, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:15:16.5289503' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (337, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-06T16:15:25.7733685' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (338, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-06T16:16:30.8895875' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (339, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T16:16:36.7337647' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (340, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:38:49.1248777' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (341, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:39:02.9844931' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (342, N'ActivityLog', N'User Request to Register New Application Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@its.com, CNIC: 42301-0412838-7, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-06T16:39:31.9452763' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (343, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-06T16:39:31.9532683' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (344, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: Muhammad, LastName: Zaid, Email: zaid@its.com, CNIC: 42301-0412838-7, PhoneNumber: 0304-1020472 ', CAST(N'2023-09-06T16:39:31.9552669' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (345, N'ActivityLog', N'User account has been successfully created.UserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T16:39:32.0277282' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (346, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:50:38.9947265' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (347, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T16:50:53.3085804' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (348, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T16:51:56.2640145' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (349, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T16:52:16.4554268' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (350, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:00:46.0646645' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (351, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:00:58.7787168' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (352, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:03:56.9582464' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (353, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:04:10.3716527' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (354, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:23:50.6443811' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (355, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:24:00.5228266' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (356, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:25:06.8901091' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (357, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:25:20.6283984' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (358, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:27:54.8710602' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (359, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:28:05.1944670' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (360, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:29:29.9528503' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (361, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:29:39.9336240' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (362, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:39:04.9129292' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (363, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:39:13.8775074' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (364, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-06T17:41:01.6570540' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (365, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-06T17:41:06.2244217' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (366, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T15:25:25.8539968' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (367, N'ActivityLog', N'User LogIn FailedReason: Account does not exist!!!', CAST(N'2023-09-07T15:25:41.1300509' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (368, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T15:25:50.2501505' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (369, N'ActivityLog', N'User-requested to Update Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"   C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T16:40:04.1333869' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (370, N'ActivityLog', N'User has Successfully Updated Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"   C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T16:40:04.1676245' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (371, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"    C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:19:45.6563272' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (372, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"    C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:19:45.6764630' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (373, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T17:19:56.5332129' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (374, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T17:20:01.5305568' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (375, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"    C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:20:07.8111899' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (376, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"    C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:20:07.8308462' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (377, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"    C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:20:07.8808066' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (378, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:20:20.5431442' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (379, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:20:20.5618918' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (380, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:24:40.3194994' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (381, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:24:40.3525555' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (382, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:26:07.3343786' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (383, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:26:07.3727058' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (384, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T17:28:08.4580340' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (385, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T17:28:22.3284229' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (386, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"a","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:28:55.6899358' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (387, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"a","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:28:55.7195634' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (388, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"a","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:31:30.8022996' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (389, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"a","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"F","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:31:30.8272204' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (390, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:31:56.2220081' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (391, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:31:56.2417467' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (392, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"     C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:31:56.2867579' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (393, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:32:07.7883383' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (394, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:32:07.8238399' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (395, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:34:26.9865448' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (396, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:34:27.0068656' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (397, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"155","ResidentialCityOther":"value=","ResidentialProvince":"3","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:35:06.5182883' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (398, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"155","ResidentialCityOther":"value=","ResidentialProvince":"3","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:36:09.6192937' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (399, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T17:47:04.6874273' AS DateTime2))
GO
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (400, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T17:47:18.0086957' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (401, N'Exception', N'Exception occurred during Submiting personal Information.Model Details: null', CAST(N'2023-09-07T17:47:49.4871521' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (402, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T17:49:25.5976400' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (403, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T17:49:39.9023966' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (404, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Z","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:49:53.2711475' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (405, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Z","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T17:49:53.2956925' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (406, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T19:44:35.2958697' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (407, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T19:44:50.1957464' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (408, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:45:02.1202458' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (409, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:45:02.1917620' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (410, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:45:02.3064580' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (411, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:45:02.3249773' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (412, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:48:34.6609883' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (413, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:48:34.7033424' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (414, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"M","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"1","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:50:45.4278738' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (415, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"M","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"1","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:50:45.4456856' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (416, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T19:52:51.4046567' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (417, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-07T19:53:05.0315721' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (418, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Mu","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:53:25.2647653' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (419, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Mu","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T19:53:25.2976815' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (420, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Mu","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T20:06:37.0896592' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (421, N'ActivityLog', N'User provided data is InValid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Mu","MiddleName":null,"LastName":"Za","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.c","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T20:06:37.1337621' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (422, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"2023-09-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T20:16:58.4028045' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (423, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"2023-09-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T20:16:58.4169019' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (424, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"2023-09-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"      C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":null,"PermanentCity":null,"PermanentCityOther":null,"PermanentProvince":null,"PermanentCountry":null,"PermanentPostalCode":0,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-07T20:16:58.4688292' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (425, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-07T23:11:13.6201690' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (426, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T09:20:13.6625282' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (427, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T09:22:02.9287413' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (428, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:22:47.0080789' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (429, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:22:47.0797969' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (430, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":null,"CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"       C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:22:47.1462014' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (431, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T09:29:16.1856180' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (432, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T09:29:23.9417859' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (433, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T09:30:55.6558580' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (434, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T09:31:07.6042528' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (435, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T09:47:53.9951410' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (436, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T09:48:04.6080519' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (437, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"        C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:48:31.5508665' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (438, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"        C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:48:31.5555704' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (439, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"        C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-08T09:48:31.5744669' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (440, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T10:29:06.8953670' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (441, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T10:29:16.5242106' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (442, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T11:12:08.4964816' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (443, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T11:12:19.8456936' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (444, N'ActivityLog', N'Model Validation Is true', CAST(N'2023-09-08T11:12:59.6786790' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (445, N'ActivityLog', N'User-provided data has been successfully validated.Details: Firstname: Muhammad, LastName: Naseer, Email: naseer@gmail.com, CNIC: 45645-4654654-6, PhoneNumber: 0345-5454545 ', CAST(N'2023-09-08T11:12:59.6866562' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (446, N'ActivityLog', N'User account has been successfully created.UserID: 2UserNameMuhammad Naseer', CAST(N'2023-09-08T11:12:59.7347292' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (447, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T11:14:15.7498277' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (448, N'ActivityLog', N'User Successfully LogInUserID: 2UserNameMuhammad Naseer', CAST(N'2023-09-08T11:14:32.1145168' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (449, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T12:20:04.3882875' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (450, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T12:20:12.3413462' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (451, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T12:25:04.7369674' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (452, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T12:25:41.5965673' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (453, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T13:07:08.7637978' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (454, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T13:07:16.8467333' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (455, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T13:13:07.1635948' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (456, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T13:16:34.9693458' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (457, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T13:16:42.7630407' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (458, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T15:36:16.9330065' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (459, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T15:37:40.3580280' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (460, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T15:38:09.9109392' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (461, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":null,"CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T16:02:52.8977876' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (462, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":null,"CollegeStartDate":null,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T16:03:20.4169277' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (463, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":"Khi","CollegeStartDate":2020,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T16:10:51.0711157' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (464, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"HSSC II Completed","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-08T16:14:27.2904410' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (465, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T16:23:26.7322255' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (466, N'ActivityLog', N'User Successfully LogInUserID: 2UserNameMuhammad Naseer', CAST(N'2023-09-08T16:23:33.9290615' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (467, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T17:28:05.4974356' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (468, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T17:28:17.6323764' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (469, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-08T17:31:42.8383670' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (470, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-08T17:31:47.5997061' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (471, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T09:34:08.5424901' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (472, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T09:34:19.1544326' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (473, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null}', CAST(N'2023-09-09T09:42:46.1403040' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (474, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-09T09:42:46.1466016' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (475, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science"}', CAST(N'2023-09-09T09:42:46.1615141' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (476, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T09:55:35.3953833' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (477, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T09:55:51.7949276' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (478, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"         C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T09:56:07.2579868' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (479, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"         C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T09:56:07.2658111' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (480, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"         C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T09:56:07.2852465' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (481, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T09:56:15.9860927' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (482, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:56:15.9860927' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (483, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:56:16.0029322' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (484, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T09:56:42.4507523' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (485, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:56:42.4594693' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (486, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:56:42.4844099' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (487, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T09:57:40.2188682' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (488, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:57:40.2357928' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (489, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:57:40.2552048' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (490, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T09:59:05.2489269' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (491, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:59:05.2627385' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (492, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T09:59:05.2807221' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (493, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T10:03:45.8004556' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (494, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T10:03:45.8300797' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (495, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T10:03:45.8449304' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (496, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T10:07:26.8627617' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (497, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T10:07:26.8767058' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (498, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T10:07:26.8934751' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (499, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T12:00:47.0628716' AS DateTime2))
GO
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (500, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T12:00:58.9827008' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (501, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"          C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T12:01:06.4529620' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (502, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"          C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T12:01:06.4616946' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (503, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"          C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T12:01:06.5282577' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (504, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T12:01:10.3402123' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (505, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T12:01:10.3560136' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (506, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T12:01:10.4276889' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (507, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T12:04:58.9149346' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (508, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T12:05:07.8517767' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (509, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T12:41:30.1040375' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (510, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T12:41:36.6231645' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (511, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T12:56:07.8240308' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (512, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T12:56:15.8223902' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (513, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T13:13:02.7556208' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (514, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T13:13:13.4172700' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (515, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T13:44:13.0286116' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (516, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T13:44:42.9428381' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (517, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T15:24:30.5286892' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (518, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T15:24:35.2166994' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (519, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"           C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T15:24:40.6694674' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (520, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"           C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T15:24:40.7171464' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (521, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"           C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T15:24:40.7917309' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (522, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T15:24:48.2111621' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (523, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T15:24:48.2184999' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (524, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T15:24:48.2671721' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (525, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T16:12:06.2416825' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (526, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T16:12:14.3646291' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (527, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"            C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:12:20.3262614' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (528, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"            C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:12:20.3300351' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (529, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"            C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:12:20.4811862' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (530, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T16:12:27.7989735' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (531, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T16:12:27.8057904' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (532, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T16:12:27.8746312' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (533, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T16:18:25.2413820' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (534, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T16:19:09.6483315' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (535, N'ActivityLog', N'User-requested to Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"             C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:19:14.8222317' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (536, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"             C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:19:14.8282307' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (537, N'ActivityLog', N'User Submited Personal Information Successfully. Details: {"Id":1,"HUTopId":null,"AppId":null,"FirstName":"Muhammad","MiddleName":null,"LastName":"Zaid","FatherFirstName":"Muhammad","FatherMiddleName":"Ilyas","FatherLastName":"Ahmed","Gender":"M","HusbandName":null,"DateOfBirth":"1998-01-06T00:00:00","CountyAdd":null,"CellPhoneNumber":"0304-1020472","AlternateCellPhoneNumber":"0303-0412121","TellUs":null,"ResidentialAddress":"             C-119","ResidentialCity":"18","ResidentialCityOther":"value=","ResidentialProvince":"2","ResidentialCountry":"1","ResidentialPostalCode":45666,"PermanentAddress":"C-119","PermanentCity":"18","PermanentCityOther":null,"PermanentProvince":"1","PermanentCountry":"1","PermanentPostalCode":75500,"CNIC":"42301-0412838-7","EmailAddress":"zaid@its.com","AlterEmailAddress":"JJJHJ@mm.vo","Password":null,"SaveDate":null,"UpdateDate":null,"AppStatus":null,"StudentStatus":null,"TempId":null,"School":null,"SchoolCode":null,"SchoolName":null,"SchoolNameOther":null,"CurrentQualification":null,"CurrentQualificationOther":null,"AlternateLandline":null,"UserId":null,"TestDate":null,"SubmissionDate":null,"Retake":null,"IsAppliedBefore":null,"AppliedBeforeMonth":null,"AppliedBeforeYear":null,"HomePhoneNumber":"4230104128387","WhatsAppNumber":"0304-1020472","GuardianCellPhoneNumber":"0303-1211221","GuardianEmailAddress":"asad@gmail.com","HearAboutHU":null,"HearAboutHUOther":null,"CreatedDatetime":null,"Declaration":null,"IsCompleted":null}', CAST(N'2023-09-09T16:19:14.8480023' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (538, N'ActivityLog', N'User-requested to Submit Educational Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":null,"GroupName":null,"IsCompleted":null}', CAST(N'2023-09-09T16:19:17.7977398' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (539, N'ActivityLog', N'User provided data is Valid for Submit Personal Information. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T16:19:17.8017422' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (540, N'ActivityLog', N'Educational Information Submited Successfully. Details: {"Id":0,"UserId":null,"CurrentLevelOfEdu":"Already enrolled in a University","CurrentCollege":"PNPI","CollegeAddress":"tower khi","CollegeStartDate":2019,"CollegeCompletionDate":2023,"HSSCPercentage":"45.64%","BoardOfEdu":"2","GroupOfStudy":"3","SchoolName":"Alies Academy","SchoolAddress":"lines area khi","SSCPercentage":"45.64%","UniversityName":null,"IntendedProgram":"BS Computer Engineering","BoardName":"Federal Board","GroupName":"Computer Science","IsCompleted":1}', CAST(N'2023-09-09T16:19:17.8197624' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (541, N'ActivityLog', N'User has navigated to the registration page.', CAST(N'2023-09-09T17:17:20.3106063' AS DateTime2))
INSERT [dbo].[Log] ([Id], [Type], [Description], [CreatedDatetime]) VALUES (542, N'ActivityLog', N'User Successfully LogInUserID: 1UserNameMuhammad Zaid', CAST(N'2023-09-09T17:17:29.3039897' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Log] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonalInformation] ON 

INSERT [dbo].[PersonalInformation] ([Id], [HUTopId], [AppId], [FirstName], [MiddleName], [LastName], [FatherFirstName], [FatherMiddleName], [FatherLastName], [Gender], [HusbandName], [DateOfBirth], [CountyAdd], [CellPhoneNumber], [AlternateCellPhoneNumber], [TellUs], [ResidentialAddress], [ResidentialCity], [ResidentialCityOther], [ResidentialProvince], [ResidentialCountry], [ResidentialPostalCode], [PermanentAddress], [PermanentCity], [PermanentCityOther], [PermanentProvince], [PermanentCountry], [PermanentPostalCode], [CNIC], [EmailAddress], [AlterEmailAddress], [Password], [SaveDate], [UpdateDate], [AppStatus], [StudentStatus], [TempId], [School], [SchoolCode], [SchoolName], [SchoolNameOther], [CurrentQualification], [CurrentQualificationOther], [AlternateLandline], [UserId], [TestDate], [SubmissionDate], [Retake], [IsAppliedBefore], [AppliedBeforeMonth], [AppliedBeforeYear], [HomePhoneNumber], [WhatsAppNumber], [GuardianCellPhoneNumber], [GuardianEmailAddress], [HearAboutHU], [HearAboutHUOther], [CreatedDatetime], [Declaration], [IsCompleted]) VALUES (1, N'HUTOPS23-00001', NULL, N'Muhammad', N'', N'Zaid', N'Muhammad', N'Ilyas', N'Ahmed', N'M', N'', CAST(N'1998-01-06T00:00:00.0000000' AS DateTime2), NULL, N'0304-1020472', N'0303-0412121', NULL, N'             C-119', N'Karachi', N'value=', N'Punjab', N'Pakistan', 45666, N'C-119', N'Karachi', NULL, N'Sindh', N'Pakistan', 75500, N'42301-0412838-7', N'zaid@its.com', N'JJJHJ@mm.vo', 0x4297F44B13955235245B2497399D7A93, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'4230104128387', N'0304-1020472', N'0303-1211221', N'asad@gmail.com', NULL, NULL, CAST(N'2023-09-06T16:39:32.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[PersonalInformation] ([Id], [HUTopId], [AppId], [FirstName], [MiddleName], [LastName], [FatherFirstName], [FatherMiddleName], [FatherLastName], [Gender], [HusbandName], [DateOfBirth], [CountyAdd], [CellPhoneNumber], [AlternateCellPhoneNumber], [TellUs], [ResidentialAddress], [ResidentialCity], [ResidentialCityOther], [ResidentialProvince], [ResidentialCountry], [ResidentialPostalCode], [PermanentAddress], [PermanentCity], [PermanentCityOther], [PermanentProvince], [PermanentCountry], [PermanentPostalCode], [CNIC], [EmailAddress], [AlterEmailAddress], [Password], [SaveDate], [UpdateDate], [AppStatus], [StudentStatus], [TempId], [School], [SchoolCode], [SchoolName], [SchoolNameOther], [CurrentQualification], [CurrentQualificationOther], [AlternateLandline], [UserId], [TestDate], [SubmissionDate], [Retake], [IsAppliedBefore], [AppliedBeforeMonth], [AppliedBeforeYear], [HomePhoneNumber], [WhatsAppNumber], [GuardianCellPhoneNumber], [GuardianEmailAddress], [HearAboutHU], [HearAboutHUOther], [CreatedDatetime], [Declaration], [IsCompleted]) VALUES (2, N'HUTOPS23-00002', NULL, N'Muhammad', N'', N'Naseer', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'0345-5454545', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'45645-4654654-6', N'naseer@gmail.com', NULL, 0x4297F44B13955235245B2497399D7A93, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2023-09-08T11:12:59.7300000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[PersonalInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (6, N'Azad Kashmir', 1, 1)
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (3, N'Balochistan', 1, 1)
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (5, N'Gilgit-Baltistan', 1, 1)
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (4, N'Khyber Pakhtunkhwa', 1, 1)
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (2, N'Punjab', 1, 1)
INSERT [dbo].[State] ([Id], [Name], [CountryId], [Status]) VALUES (1, N'Sindh', 1, 1)
SET IDENTITY_INSERT [dbo].[State] OFF
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_IsCompleted]  DEFAULT ((0)) FOR [IsCompleted]
GO
ALTER TABLE [dbo].[Educational] ADD  CONSTRAINT [DF_Educational_IsCompleted]  DEFAULT ((0)) FOR [IsCompleted]
GO
ALTER TABLE [dbo].[Log] ADD  CONSTRAINT [DF_Log_CreatedDatetime]  DEFAULT (getdate()) FOR [CreatedDatetime]
GO
ALTER TABLE [dbo].[PersonalInformation] ADD  CONSTRAINT [DF_PersonalInformation_AppStatus]  DEFAULT ((0)) FOR [AppStatus]
GO
ALTER TABLE [dbo].[PersonalInformation] ADD  CONSTRAINT [DF_PersonalInformation_StudentStatus]  DEFAULT ((0)) FOR [StudentStatus]
GO
ALTER TABLE [dbo].[PersonalInformation] ADD  CONSTRAINT [DF_PersonalInformation_CreatedDatetime]  DEFAULT (getdate()) FOR [CreatedDatetime]
GO
ALTER TABLE [dbo].[PersonalInformation] ADD  CONSTRAINT [DF_PersonalInformation_IsCompleted]  DEFAULT ((0)) FOR [IsCompleted]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_Status]  DEFAULT ((1)) FOR [Status]
GO
/****** Object:  StoredProcedure [dbo].[InsertBoardGroups]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertBoardGroups]
    @GroupNames NVARCHAR(MAX),
    @BoardId INT
AS
BEGIN
    DECLARE @Groups TABLE ([Name] NVARCHAR(100))
    
    -- Split the comma-separated subject names and insert into the @Subjects table variable
    INSERT INTO @Groups ([Name])
    SELECT value FROM STRING_SPLIT(@GroupNames, ',')

    -- Insert records into the GroupSubjects table from the @Subjects table variable
    INSERT INTO BoardGroups ([Name], BoardId)
    SELECT [Name], @BoardId FROM @Groups
END
GO
/****** Object:  StoredProcedure [dbo].[InsertGroupSubjects]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertGroupSubjects]
    @SubjectNames NVARCHAR(MAX),
    @GroupId INT
AS
BEGIN
    DECLARE @Subjects TABLE (Name NVARCHAR(100))
    
    -- Split the comma-separated subject names and insert into the @Subjects table variable
    INSERT INTO @Subjects (Name)
    SELECT value FROM STRING_SPLIT(@SubjectNames, ',')

    -- Insert records into the GroupSubjects table from the @Subjects table variable
    INSERT INTO GroupSubjects ([Name], GroupId)
    SELECT [Name], @GroupId FROM @Subjects
END



GO
/****** Object:  StoredProcedure [dbo].[WEB_CheckPersonalInfo]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[WEB_CheckPersonalInfo]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 IF EXISTS(SELECT Id FROM PersonalInformation WHERE 
		Id = @Id AND
        (COALESCE(FirstName, '') != '' OR TRIM(FirstName) != '') AND
        (COALESCE(LastName, '') != '' OR TRIM(LastName) != '') AND
        (COALESCE(FatherFirstName, '') != '' OR TRIM(FatherFirstName) != '') AND
        (COALESCE(FatherLastName, '') != '' OR TRIM(FatherLastName) != '') AND
        (COALESCE(Gender, '') != '' OR TRIM(Gender) != '') AND
		
        (DateOfBirth IS NOT NULL) AND
        (COALESCE(CellPhoneNumber, '') != '' OR TRIM(CellPhoneNumber) != '') AND
        (COALESCE(ResidentialAddress, '') != '' OR TRIM(ResidentialAddress) != '') AND
        ((COALESCE(ResidentialCity, '') != '' OR TRIM(ResidentialCity) = '') OR (COALESCE(ResidentialCityOther, '') != '' OR TRIM(ResidentialCityOther) != '')) AND
        (COALESCE(ResidentialProvince, '') != '' OR TRIM(ResidentialProvince) != '') AND
        (COALESCE(ResidentialCountry, '') != '' OR TRIM(ResidentialCountry) != '') AND
        
		
        (COALESCE(PermanentAddress, '') != '' OR TRIM(PermanentAddress) != '') AND
        ((COALESCE(PermanentCity, '') != '' OR TRIM(PermanentCity) != '') OR (COALESCE(PermanentCityOther, '') != '' OR TRIM(PermanentCityOther) != '')) AND
        (COALESCE(PermanentProvince, '') != '' OR TRIM(PermanentProvince) != '') AND
        (COALESCE(PermanentCountry, '') != '' OR TRIM(PermanentCountry) != '') AND

		(COALESCE(CNIC, '') != '' OR TRIM(CNIC) != '') AND
		(COALESCE(EmailAddress, '') != '' OR TRIM(EmailAddress) != '') AND
		(COALESCE(GuardianCellPhoneNumber, '') != '' OR TRIM(GuardianCellPhoneNumber) != '') AND
		(COALESCE(GuardianEmailAddress, '') != '' OR TRIM(GuardianEmailAddress) != '')

    ) 
	BEGIN
        -- If all fields are either NULL or empty, set status to 'not completed'
        UPDATE PersonalInformation
        SET IsCompleted = 1 WHERE 
		Id = @Id;
		SELECT 1 AS Response, 'Personal Information Completed' AS Reason
	END
    ELSE
	BEGIN
        -- If any field is filled, set status to 'completed'
        UPDATE PersonalInformation
        SET IsCompleted = 0 WHERE 
		Id = @Id
		SELECT 0 AS Response, 'Personal Information not Completed' AS Reason;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_CreateUser]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_CreateUser]
	-- Add the parameters for the stored procedure here
	@FirstName VARCHAR(50),
	@MiddleName VARCHAR(50),
	@LastName VARCHAR(50),
	@CNIC VARCHAR(15),
	@CellPhoneNumber VARCHAR(50),
	@EmailAddress VARCHAR(50),
	@Password VARCHAR(25)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
			IF EXISTS(SELECT Id FROM PersonalInformation WHERE CNIC = @CNIC)
            BEGIN
                  SELECT 0 AS 'Response', 'CNIC Already Exist' AS Reason -- CNIC already exist.
            END
			ELSE IF EXISTS(SELECT Id FROM PersonalInformation WHERE CellPhoneNumber = @CellPhoneNumber)
            BEGIN
                  SELECT 0 AS 'Response', 'Phone Number Already Exist' AS Reason -- Number Already exist.
            END
			ELSE IF EXISTS(SELECT Id FROM PersonalInformation WHERE EmailAddress = @EmailAddress)
            BEGIN
                  SELECT 0 AS 'Response', 'Email Already Exist' AS Reason -- Email Already Exist.
            END
			ELSE
			BEGIN
				DECLARE @HUTopId varchar(50) = dbo.GenerateHUTopId();
				--INSERTION IN USER TABLE
				INSERT INTO PersonalInformation(FirstName, MiddleName, LastName, CNIC, CellPhoneNumber, EmailAddress, [Password], HUTopId )
				VALUES(@FirstName, @MiddleName, @LastName, @CNIC, @CellPhoneNumber, @EmailAddress, HASHBYTES('MD5', @Password), @HUTopId);

				If SCOPE_IDENTITY() is null
					BEGIN
						SELECT 0 as 'Response', 'Insertion Failed' AS Reason
					END
				ELSE
					BEGIN
						SELECT CAST(SCOPE_IDENTITY() AS INT) as 'Response', 'User Created Successfully' AS Reason
					END
			END
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_GetAll]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_GetAll]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from PersonalInformation
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_GetBoards]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_GetBoards]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Board
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_GetGroups]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_GetGroups]
	-- Add the parameters for the stored procedure here
	@BoardId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM BoardGroups WHERE BoardId = @BoardId
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_GetSubjects]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_GetSubjects]
	-- Add the parameters for the stored procedure here
	@GroupId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM GroupSubjects WHERE GroupId = @GroupId
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_InsertEducation]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_InsertEducation]
	-- Add the parameters for the stored procedure here
	@UserId INT,
	@CurrentLevel VARCHAR(50) = NULL,
	@CurrentCollege VARCHAR(50) = NULL,
	@CollegeAddress VARCHAR(100) = NULL,
	@CollegeST VARCHAR(50) = NULL,
	@CollegeCD VARCHAR(50) = NULL,
	@HSSCPercentage VARCHAR(50) = NULL,
	@BoardOfEdu VARCHAR(50) = NULL,
	@BoardName VARCHAR(50) = NULL,
	@Group VARCHAR(50) = NULL,
	@GroupName VARCHAR(50) = NULL,
	@SchoolName VARCHAR(50) = NULL,
	@SchoolAddress VARCHAR(50) = NULL,
	@SSCPercentage VARCHAR(50) = NULL,
	@UniversityName VARCHAR(50) = NULL,
	@IntendedProgram VARCHAR(50) = NULL,
	@IsCompleted INT = 0,
	-- Subjects Info
	@SubjectName VARCHAR(MAX),
	@SubjectObtain VARCHAR(MAX),
	@SubjectTotal VARCHAR(MAX),
	@SubjectGrade VARCHAR(MAX)
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @EducationalId INT;  SELECT @EducationalId = Id FROM Educational WHERE UserId = @UserId
	DELETE FROM Educational WHERE UserId = @UserId


    -- Insert statements for procedure here
	INSERT INTO Educational(
	UserId,
	CurrentLevelOfEdu,
	CurrentCollege,
	CollegeAddress,
	CollegeStartDate,
	CollegeCompletionDate,
	HSSCPercentage,
	BoardOfEdu,
	GroupOfStudy,
	SchoolName,
	SchoolAddress,
	SSCPercentage,
	UniversityName,
	IntendedProgram,
	BoardName,
	GroupName,
	IsCompleted
	)
	VALUES(
	@UserId,
	@CurrentLevel,
	@CurrentCollege,
	@CollegeAddress ,
	@CollegeST,
	@CollegeCD,
	@HSSCPercentage,
	@BoardOfEdu,
	@Group,
	@SchoolName,
	@SchoolAddress,
	@SSCPercentage,
	@UniversityName,
	@IntendedProgram,
	@BoardName,
	@GroupName,
	@IsCompleted
	) 
	If SCOPE_IDENTITY() is null
		BEGIN
			SELECT 0 as Response, 'Failed' AS Reason
		END
	ELSE
		BEGIN
			DELETE FROM EducationalSubjects WHERE EducationalId = @EducationalId


			-- Insert statements for procedure here
			DECLARE @Name NVARCHAR(255);
			DECLARE @ObtainMarks INT;
			DECLARE @TotalMarks INT;
			DECLARE @Grade NVARCHAR(255);
			DECLARE @EduId INT = SCOPE_IDENTITY();
			WHILE CHARINDEX(',', @SubjectName) > 0
			BEGIN
				SELECT @Name = LEFT(@SubjectName, CHARINDEX(',', @SubjectName) - 1);
				SELECT @SubjectName = STUFF(@SubjectName, 1, CHARINDEX(',', @SubjectName), '');

				SELECT @ObtainMarks = LEFT(@SubjectObtain, CHARINDEX(',', @SubjectObtain) - 1);
				SELECT @SubjectObtain = STUFF(@SubjectObtain, 1, CHARINDEX(',', @SubjectObtain), '');

				SELECT @TotalMarks = LEFT(@SubjectTotal, CHARINDEX(',', @SubjectTotal) - 1);
				SELECT @SubjectTotal = STUFF(@SubjectTotal, 1, CHARINDEX(',', @SubjectTotal), '');

				SELECT @Grade = LEFT(@SubjectGrade, CHARINDEX(',', @SubjectGrade) - 1);
				SELECT @SubjectGrade = STUFF(@SubjectGrade, 1, CHARINDEX(',', @SubjectGrade), '');

				INSERT INTO EducationalSubjects([Name], ObtainMarks, TotalMarks, Grade, EducationalId)
				VALUES (@Name, @ObtainMarks, @TotalMarks, @Grade, @EduId);
			END;
			SELECT 1 as Response, 'Success' AS Reason
		END


END
GO
/****** Object:  StoredProcedure [dbo].[WEB_InsertEducationalSubjects]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_InsertEducationalSubjects]
	-- Add the parameters for the stored procedure here
	@EducationalId INT,
	@SubjectName VARCHAR(MAX),
	@SubjectObtain VARCHAR(MAX),
	@SubjectTotal VARCHAR(MAX),
	@SubjectGrade VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM EducationalSubjects WHERE EducationalId = @EducationalId


    -- Insert statements for procedure here
	DECLARE @Name NVARCHAR(255);
    DECLARE @ObtainMarks INT;
    DECLARE @TotalMarks INT;
    DECLARE @Grade NVARCHAR(255);

    WHILE CHARINDEX(',', @SubjectName) > 0
    BEGIN
        SELECT @Name = LEFT(@SubjectName, CHARINDEX(',', @SubjectName) - 1);
        SELECT @SubjectName = STUFF(@SubjectName, 1, CHARINDEX(',', @SubjectName), '');

        SELECT @ObtainMarks = LEFT(@SubjectObtain, CHARINDEX(',', @SubjectObtain) - 1);
        SELECT @SubjectObtain = STUFF(@SubjectObtain, 1, CHARINDEX(',', @SubjectObtain), '');

        SELECT @TotalMarks = LEFT(@SubjectTotal, CHARINDEX(',', @SubjectTotal) - 1);
        SELECT @SubjectTotal = STUFF(@SubjectTotal, 1, CHARINDEX(',', @SubjectTotal), '');

        SELECT @Grade = LEFT(@SubjectGrade, CHARINDEX(',', @SubjectGrade) - 1);
        SELECT @SubjectGrade = STUFF(@SubjectGrade, 1, CHARINDEX(',', @SubjectGrade), '');

        INSERT INTO EducationalSubjects([Name], ObtainMarks, TotalMarks, Grade, EducationalId)
        VALUES (@Name, @ObtainMarks, @TotalMarks, @Grade, @EducationalId);
    END;
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UpdateAddressDetails]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_UpdateAddressDetails]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@ResidentialAddress VARCHAR(100) = NULL,
	@ResidentialCountry VARCHAR(50) = NULL,
	@ResidentialProvince VARCHAR(50) = NULL,
	@ResidentialCity VARCHAR(50) = NULL,
	@ResidentialCityOther VARCHAR(50) = NULL,
	@ResidentialPostalCode INT = NULL,
	@PermanentAddress VARCHAR(100) = NULL,
	@PermanentCountry VARCHAR(50) = NULL,
	@PermanentProvince VARCHAR(50) = NULL,
	@PermanentCity VARCHAR(50) = NULL,
	@PermanentCityOther VARCHAR(50) = NULL,
	@PermanentPostalCode INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE PersonalInformation
	SET
	ResidentialAddress = @ResidentialAddress,
	ResidentialCountry = @ResidentialCountry,
	ResidentialProvince = @ResidentialProvince,
	ResidentialCity = @ResidentialCity,
	ResidentialCityOther = @ResidentialCityOther,
	ResidentialPostalCode = @ResidentialPostalCode,
	PermanentAddress = @PermanentAddress,
	PermanentCountry = @PermanentCountry,
	PermanentProvince = @PermanentProvince,
	PermanentCity = @PermanentCity,
	PermanentCityOther = @PermanentCityOther,
	PermanentPostalCode = @PermanentPostalCode WHERE Id = Id
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UpdateContactDetails]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================
CREATE PROCEDURE [dbo].[WEB_UpdateContactDetails]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@CellPhone VARCHAR(15) = NULL,
	@WhatsApp VARCHAR(15) = NULL,
	@AltCellPhone VARCHAR(15) = NULL,
	@HomeCellPhone VARCHAR(15) = NULL,
	@AltLandline VARCHAR(15) = NULL,
	@GuardianCellPhone VARCHAR(15) = NULL,
	@GuardianEmail VARCHAR(30) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE PersonalInformation SET
	CellPhoneNumber = @CellPhone, 
	WhatsAppNumber = @WhatsApp, 
	AlternateCellPhoneNumber = @AltCellPhone, 
	HomePhoneNumber = @HomeCellPhone, 
	AlternateLandline = @AltLandline, 
	GuardianCellPhoneNumber = @GuardianCellPhone, 
	GuardianEmailAddress= @GuardianEmail WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UpdatePersonal]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[WEB_UpdatePersonal]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@FName Varchar(50),
	@MName Varchar(50) = NULL,
	@LName Varchar(50),
	@FatherFName Varchar(50),
	@FatherMName Varchar(50) = NULL,
	@FatherLName Varchar(50),
	@Gender Varchar(50),
	@HusbandName Varchar(50) = NULL,
	@DOB Varchar(50),
	@CNIC Varchar(50),
	@Email Varchar(50),
	@AlterEmail Varchar(50) = NULL,

	-- Contact
	@CellPhone VARCHAR(15) = NULL,
	@WhatsApp VARCHAR(15) = NULL,
	@AltCellPhone VARCHAR(15) = NULL,
	@HomeCellPhone VARCHAR(15) = NULL,
	@AltLandline VARCHAR(15) = NULL,
	@GuardianCellPhone VARCHAR(15) = NULL,
	@GuardianEmail VARCHAR(30) = NULL,

	-- Address
	@ResidentialAddress VARCHAR(100),
	@ResidentialCountry VARCHAR(50),
	@ResidentialProvince VARCHAR(50),
	@ResidentialCity VARCHAR(50),
	@ResidentialCityOther VARCHAR(50),
	@ResidentialPostalCode INT = NULL,
	@PermanentAddress VARCHAR(100),
	@PermanentCountry VARCHAR(50),
	@PermanentProvince VARCHAR(50),
	@PermanentCity VARCHAR(50),
	@PermanentCityOther VARCHAR(50),
	@PermanentPostalCode INT = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE PersonalInformation SET
	FirstName = @FName, 
	MiddleName = @MName, 
	LastName = @LName, 
	FatherFirstName = @FatherFName, 
	FatherMiddleName = @FatherMName, 
	FatherLastName = @FatherLName, 
	Gender= @Gender, 
	HusbandName = @HusbandName, 
	DateOfBirth = @DOB,
	AlterEmailAddress = @AlterEmail,
	-- Contact
	WhatsAppNumber = @WhatsApp, 
	AlternateCellPhoneNumber = @AltCellPhone, 
	HomePhoneNumber = @HomeCellPhone, 
	AlternateLandline = @AltLandline, 
	GuardianCellPhoneNumber = @GuardianCellPhone,
	GuardianEmailAddress= @GuardianEmail,
	--Address
	ResidentialAddress = @ResidentialAddress,
	ResidentialCountry = @ResidentialCountry,
	ResidentialProvince = @ResidentialProvince,
	ResidentialCity = @ResidentialCity,
	ResidentialCityOther = @ResidentialCityOther,
	ResidentialPostalCode = @ResidentialPostalCode,
	PermanentAddress = @PermanentAddress,
	PermanentCountry = @PermanentCountry,
	PermanentProvince = @PermanentProvince,
	PermanentCity = @PermanentCity,
	PermanentCityOther = @PermanentCityOther,
	PermanentPostalCode = @PermanentPostalCode,
	IsCompleted = 1 WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UpdatePersonalInfo]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[WEB_UpdatePersonalInfo]
	-- Add the parameters for the stored procedure here
	@Id INT,
	@FName Varchar(50),
	@MName Varchar(50) = NULL,
	@LName Varchar(50),
	@FatherFName Varchar(50),
	@FatherMName Varchar(50) = NULL,
	@FatherLName Varchar(50),
	@Gender Varchar(50),
	@HusbandName Varchar(50) = NULL,
	@DOB Varchar(50),
	@CNIC Varchar(50),
	@Email Varchar(50),
	@AlterEmail Varchar(50) = NULL

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE PersonalInformation SET
	FirstName = @FName, 
	MiddleName = @MName, 
	LastName = @LName, 
	FatherFirstName = @FatherFName, 
	FatherMiddleName = @FatherMName, 
	FatherLastName = @FatherLName, 
	Gender= @Gender, 
	HusbandName = @HusbandName, 
	DateOfBirth = @DOB, 
	CNIC = @CNIC, 
	EmailAddress = @Email, 
	AlterEmailAddress = @AlterEmail WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[WEB_UserLogin]    Script Date: 9/9/2023 5:19:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Muhammad Zaid
-- =============================================

CREATE PROCEDURE [dbo].[WEB_UserLogin] 
	@Email VARCHAR(25),
	@Password VARCHAR(25)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Id INT; 
	SELECT @Id = Id FROM PersonalInformation WHERE EmailAddress = @Email AND [Password] = HASHBYTES('MD5', @Password);
	IF (@Id is null)
      BEGIN
		SELECT -1 AS 'Response', 'Account does not exist!!!' AS Reason -- User invalid.
      END
    ELSE
	  BEGIN
         SELECT *, @Id AS 'Response', 'Account successfully loggedIn' AS Reason FROM PersonalInformation WHERE Id = @Id; -- Valid User
	  END
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Enabled, 0 = Disabled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Enabled, 0 = Disabled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = false, 1 = true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Educational', @level2type=N'COLUMN',@level2name=N'IsCompleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Male, 2 = Female, 3 = Other' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonalInformation', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = True, 0 = False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonalInformation', @level2type=N'COLUMN',@level2name=N'IsAppliedBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = True, 0 = False' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonalInformation', @level2type=N'COLUMN',@level2name=N'Declaration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 = False, 1 = True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PersonalInformation', @level2type=N'COLUMN',@level2name=N'IsCompleted'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = Enabled, 0 = Disabled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'State', @level2type=N'COLUMN',@level2name=N'Status'
GO
