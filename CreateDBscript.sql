
/****** Object:  Database testGymDB    Script Date: 20-Nov-21 2:12:03 PM ******/
CREATE DATABASE testGymDB

GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC testGymDB.[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE testGymDB SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE testGymDB SET ANSI_NULLS OFF 
GO

ALTER DATABASE testGymDB SET ANSI_PADDING OFF 
GO

ALTER DATABASE testGymDB SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE testGymDB SET ARITHABORT OFF 
GO

ALTER DATABASE testGymDB SET AUTO_CLOSE OFF 
GO

ALTER DATABASE testGymDB SET AUTO_SHRINK OFF 
GO

ALTER DATABASE testGymDB SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE testGymDB SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE testGymDB SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE testGymDB SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE testGymDB SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE testGymDB SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE testGymDB SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE testGymDB SET  ENABLE_BROKER 
GO

ALTER DATABASE testGymDB SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE testGymDB SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE testGymDB SET TRUSTWORTHY OFF 
GO

ALTER DATABASE testGymDB SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE testGymDB SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE testGymDB SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE testGymDB SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE testGymDB SET RECOVERY FULL 
GO

ALTER DATABASE testGymDB SET  MULTI_USER 
GO

ALTER DATABASE testGymDB SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE testGymDB SET DB_CHAINING OFF 
GO

ALTER DATABASE testGymDB SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE testGymDB SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE testGymDB SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE testGymDB SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE testGymDB SET QUERY_STORE = OFF
GO

ALTER DATABASE testGymDB SET  READ_WRITE 
GO

CREATE TABLE testGymDB.[dbo].[Gyms](
	[GymId] [int] IDENTITY(1,1) NOT NULL,
	[StreetAdress] [nvarchar](max) NULL,
	[MaxPeople] [int] NOT NULL,
	[OperationalHours] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Gyms] PRIMARY KEY CLUSTERED 
(
	[GymId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE testGymDB.[dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE testGymDB.[dbo].[Trainers](
	[TrainerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Trainers] PRIMARY KEY CLUSTERED 
(
	[TrainerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE testGymDB.[dbo].[TrainingClasses](
	[TrainingClassId] [int] IDENTITY(1,1) NOT NULL,
	[GymId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[MaxPeople] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Start] [datetime2](7) NOT NULL,
	[End] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TrainingClasses] PRIMARY KEY CLUSTERED 
(
	[TrainingClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE testGymDB.[dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE testGymDB.[dbo].[Users] ADD  DEFAULT ((0)) FOR [RoleId]
GO

CREATE TABLE testGymDB.[dbo].[LoginCredentials](
	[LoginCredentialsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[Attempts] [int] NOT NULL,
	[LastAttempt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LoginCredentials] PRIMARY KEY CLUSTERED 
(
	[LoginCredentialsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE testGymDB.[dbo].[LoginCredentials] ADD  DEFAULT ((0)) FOR [Attempts]
GO

ALTER TABLE testGymDB.[dbo].[LoginCredentials] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [LastAttempt]
GO

CREATE TABLE testGymDB.[dbo].[Bookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[GymId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[TrainerId] [int] NULL,
	[TrainingClassId] [int] NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO









