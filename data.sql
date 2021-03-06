USE [master]
GO
/****** Object:  Database [ManageEmployeeContacts]    Script Date: 6/25/2021 3:23:18 PM ******/
CREATE DATABASE [ManageEmployeeContacts]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManageEmployeeContacts', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManageEmployeeContacts.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManageEmployeeContacts_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManageEmployeeContacts_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ManageEmployeeContacts] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ManageEmployeeContacts].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ManageEmployeeContacts] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ARITHABORT OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ManageEmployeeContacts] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ManageEmployeeContacts] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ManageEmployeeContacts] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ManageEmployeeContacts] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ManageEmployeeContacts] SET  MULTI_USER 
GO
ALTER DATABASE [ManageEmployeeContacts] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ManageEmployeeContacts] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ManageEmployeeContacts] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ManageEmployeeContacts] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ManageEmployeeContacts] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ManageEmployeeContacts] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ManageEmployeeContacts] SET QUERY_STORE = OFF
GO
USE [ManageEmployeeContacts]
GO
/****** Object:  Table [dbo].[department]    Script Date: 6/25/2021 3:23:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[department_id] [int] IDENTITY(1,1) NOT NULL,
	[department_name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 6/25/2021 3:23:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[employee_fullname] [nvarchar](50) NOT NULL,
	[employee_dob] [date] NOT NULL,
	[employee_gender] [nvarchar](10) NOT NULL,
	[employee_email] [nvarchar](50) NOT NULL,
	[employee_phone] [nvarchar](10) NOT NULL,
	[employee_state] [nvarchar](50) NOT NULL,
	[employee_change_department] [nvarchar](50) NOT NULL,
	[department_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_employee_department] FOREIGN KEY([department_id])
REFERENCES [dbo].[department] ([department_id])
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee_department]
GO
USE [master]
GO
ALTER DATABASE [ManageEmployeeContacts] SET  READ_WRITE 
GO
