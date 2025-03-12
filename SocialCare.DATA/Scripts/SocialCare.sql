USE [master]
GO
/****** Object:  Database [SocialCare]    Script Date: 12/03/2025 05:40:00 ******/
CREATE DATABASE [SocialCare]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SocialCare', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SocialCare.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SocialCare_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SocialCare_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SocialCare] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SocialCare].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SocialCare] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SocialCare] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SocialCare] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SocialCare] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SocialCare] SET ARITHABORT OFF 
GO
ALTER DATABASE [SocialCare] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SocialCare] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SocialCare] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SocialCare] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SocialCare] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SocialCare] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SocialCare] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SocialCare] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SocialCare] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SocialCare] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SocialCare] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SocialCare] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SocialCare] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SocialCare] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SocialCare] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SocialCare] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SocialCare] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SocialCare] SET RECOVERY FULL 
GO
ALTER DATABASE [SocialCare] SET  MULTI_USER 
GO
ALTER DATABASE [SocialCare] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SocialCare] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SocialCare] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SocialCare] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SocialCare] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SocialCare] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SocialCare', N'ON'
GO
ALTER DATABASE [SocialCare] SET QUERY_STORE = ON
GO
ALTER DATABASE [SocialCare] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SocialCare]
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Compras](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPessoa] [int] NOT NULL,
	[dataCompra] [datetime] NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Compras] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContasPagar]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContasPagar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPessoa] [int] NOT NULL,
	[idCompra] [int] NULL,
	[data] [datetime] NOT NULL,
	[valor] [decimal](10, 2) NOT NULL,
	[dataVencimento] [datetime] NOT NULL,
	[dataPagamento] [datetime] NULL,
 CONSTRAINT [PK_ContasPagar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItensCompra]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItensCompra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCompra] [int] NOT NULL,
	[idProduto] [int] NOT NULL,
	[quantidade] [int] NOT NULL,
	[precoUnitario] [decimal](10, 2) NOT NULL,
	[subtotal]  AS ([quantidade]*[precoUnitario]),
 CONSTRAINT [PK_ItensCompra] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parametrizacao]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametrizacao](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](255) NOT NULL,
	[razao_social] [varchar](255) NOT NULL,
	[cnpj] [char](18) NOT NULL,
	[cidade] [varchar](100) NOT NULL,
	[bairro] [varchar](100) NOT NULL,
	[endereco] [varchar](255) NOT NULL,
	[numero] [varchar](10) NOT NULL,
	[email] [varchar](255) NULL,
	[site] [varchar](255) NULL,
	[telefone] [varchar](20) NULL,
	[logo] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoas]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](255) NOT NULL,
	[cidade] [varchar](100) NOT NULL,
	[bairro] [varchar](100) NOT NULL,
	[endereco] [varchar](255) NOT NULL,
	[numero] [varchar](10) NOT NULL,
	[email] [varchar](255) NULL,
	[telefone] [varchar](20) NULL,
	[tipo] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoas_Fisicas]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoas_Fisicas](
	[id] [int] NOT NULL,
	[cpf] [char](11) NOT NULL,
	[data_nascimento] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cpf] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoas_Juridicas]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoas_Juridicas](
	[id] [int] NOT NULL,
	[cnpj] [char](14) NOT NULL,
	[razao_social] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cnpj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 12/03/2025 05:40:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[preco] [decimal](10, 2) NOT NULL,
	[estoque] [int] NOT NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK_Compras_Pessoas] FOREIGN KEY([idPessoa])
REFERENCES [dbo].[Pessoas] ([id])
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK_Compras_Pessoas]
GO
ALTER TABLE [dbo].[ContasPagar]  WITH CHECK ADD  CONSTRAINT [FK_ContasPagar_Pessoas] FOREIGN KEY([idPessoa])
REFERENCES [dbo].[Pessoas] ([id])
GO
ALTER TABLE [dbo].[ContasPagar] CHECK CONSTRAINT [FK_ContasPagar_Pessoas]
GO
ALTER TABLE [dbo].[ItensCompra]  WITH CHECK ADD  CONSTRAINT [FK_ItensCompra_Compras] FOREIGN KEY([idCompra])
REFERENCES [dbo].[Compras] ([id])
GO
ALTER TABLE [dbo].[ItensCompra] CHECK CONSTRAINT [FK_ItensCompra_Compras]
GO
ALTER TABLE [dbo].[ItensCompra]  WITH CHECK ADD  CONSTRAINT [FK_ItensCompra_Produtos] FOREIGN KEY([idProduto])
REFERENCES [dbo].[Produtos] ([id])
GO
ALTER TABLE [dbo].[ItensCompra] CHECK CONSTRAINT [FK_ItensCompra_Produtos]
GO
ALTER TABLE [dbo].[Pessoas_Fisicas]  WITH CHECK ADD  CONSTRAINT [fk_pessoas_fisicas] FOREIGN KEY([id])
REFERENCES [dbo].[Pessoas] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pessoas_Fisicas] CHECK CONSTRAINT [fk_pessoas_fisicas]
GO
ALTER TABLE [dbo].[Pessoas_Juridicas]  WITH CHECK ADD  CONSTRAINT [fk_pessoas_juridicas] FOREIGN KEY([id])
REFERENCES [dbo].[Pessoas] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pessoas_Juridicas] CHECK CONSTRAINT [fk_pessoas_juridicas]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [CK_Compras_Total] CHECK  (([total]>=(0)))
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [CK_Compras_Total]
GO
ALTER TABLE [dbo].[ItensCompra]  WITH CHECK ADD  CONSTRAINT [CK_ItensCompras_PrecoUnitario] CHECK  (([precoUnitario]>=(0)))
GO
ALTER TABLE [dbo].[ItensCompra] CHECK CONSTRAINT [CK_ItensCompras_PrecoUnitario]
GO
ALTER TABLE [dbo].[ItensCompra]  WITH CHECK ADD  CONSTRAINT [CK_ItensCompras_Quantidade] CHECK  (([quantidade]>(0)))
GO
ALTER TABLE [dbo].[ItensCompra] CHECK CONSTRAINT [CK_ItensCompras_Quantidade]
GO
ALTER TABLE [dbo].[Pessoas]  WITH CHECK ADD CHECK  (([tipo]='J' OR [tipo]='F'))
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [CK_Produtos_Estoque] CHECK  (([estoque]>=(0)))
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [CK_Produtos_Estoque]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [CK_Produtos_Preco] CHECK  (([preco]>(0)))
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [CK_Produtos_Preco]
GO
USE [master]
GO
ALTER DATABASE [SocialCare] SET  READ_WRITE 
GO
