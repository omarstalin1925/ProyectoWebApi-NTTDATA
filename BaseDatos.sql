USE [master]
GO
/****** Object:  Database [ArquitecturaMicroservicio]    Script Date: 26/06/2022 15:58:32 ******/
CREATE DATABASE [ArquitecturaMicroservicio]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ArquitecturaMicroservicio', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ArquitecturaMicroservicio.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ArquitecturaMicroservicio_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ArquitecturaMicroservicio_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ArquitecturaMicroservicio].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ARITHABORT OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET RECOVERY FULL 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET  MULTI_USER 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ArquitecturaMicroservicio', N'ON'
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET QUERY_STORE = OFF
GO
USE [ArquitecturaMicroservicio]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 26/06/2022 15:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[PersonaId] [int] NOT NULL,
	[Contrasenia] [varchar](50) NOT NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 26/06/2022 15:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[CuentaId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[NumCuenta] [varchar](10) NULL,
	[TipoCuenta] [varchar](50) NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimiento]    Script Date: 26/06/2022 15:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimiento](
	[MovimientoId] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Fecha] [date] NULL,
	[TipoMovimiento] [varchar](50) NULL,
	[Valor] [decimal](18, 2) NULL,
	[Saldo] [decimal](18, 2) NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED 
(
	[MovimientoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 26/06/2022 15:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[PersonaId] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](10) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Genero] [char](1) NULL,
	[Edad] [int] NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](10) NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[PersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Persona1] FOREIGN KEY([PersonaId])
REFERENCES [dbo].[Persona] ([PersonaId])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_Persona1]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Cliente]
GO
ALTER TABLE [dbo].[Movimiento]  WITH CHECK ADD  CONSTRAINT [FK_Movimiento_Cuenta] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([CuentaId])
GO
ALTER TABLE [dbo].[Movimiento] CHECK CONSTRAINT [FK_Movimiento_Cuenta]
GO
USE [master]
GO
ALTER DATABASE [ArquitecturaMicroservicio] SET  READ_WRITE 
GO
