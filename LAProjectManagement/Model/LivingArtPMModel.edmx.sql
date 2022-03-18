
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/15/2018 09:46:12
-- Generated from EDMX file: C:\Users\Kirill\source\repos\LAProjectManagement\LAProjectManagement\Model\LivingArtPMModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ScanLADB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Parts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parts];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[Recuts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recuts];
GO
IF OBJECT_ID(N'[dbo].[Units]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Units];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[Scanneds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scanneds];
GO
IF OBJECT_ID(N'[dbo].[Stations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Parts'
CREATE TABLE [dbo].[Parts] (
    [PartsID] int IDENTITY(1,1) NOT NULL,
    [PartFile] nvarchar(50)  NULL,
    [X] nvarchar(50)  NULL,
    [Y] nvarchar(50)  NULL,
    [Quantity] int  NULL,
    [Grain] nvarchar(50)  NULL,
    [TagVariables] nvarchar(max)  NULL,
    [JobName] nvarchar(50)  NULL,
    [ItemName] nvarchar(50)  NULL,
    [ItemPart] nvarchar(50)  NULL,
    [CabinetNum] int  NULL,
    [PartNum] int  NULL,
    [MaterialName] nvarchar(max)  NULL,
    [EdgeInfo] nvarchar(max)  NULL,
    [Barcode] nvarchar(max)  NULL,
    [PartOffset] int  NULL,
    [PartPriority] int  NULL,
    [PartRotation] int  NULL,
    [UnitUnitID] int  NOT NULL,
    [ScannedQuantity] int  NULL,
    [StatusID] int  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectID] int  NOT NULL,
    [ProjectName] nvarchar(50)  NULL,
    [Description] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [StatusID] int  NOT NULL
);
GO

-- Creating table 'Recuts'
CREATE TABLE [dbo].[Recuts] (
    [RecutID] int IDENTITY(1,1) NOT NULL,
    [PieceID] int  NULL,
    [OrderDate] nvarchar(50)  NULL,
    [Issue] nvarchar(max)  NULL,
    [StatusStatusID] int  NOT NULL
);
GO

-- Creating table 'Units'
CREATE TABLE [dbo].[Units] (
    [UnitID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Barcode] nvarchar(max)  NULL,
    [DepartureDate] nvarchar(50)  NULL,
    [ProjectProjectID] int  NOT NULL,
    [PartsAmount] nvarchar(max)  NULL,
    [PartsScanned] nvarchar(max)  NULL,
    [StatusID] int  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [StatusID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Scanneds'
CREATE TABLE [dbo].[Scanneds] (
    [ScannedID] int IDENTITY(1,1) NOT NULL,
    [DateTime] nvarchar(max)  NULL,
    [BarCode] nvarchar(max)  NULL,
    [StatusStatusID] int  NOT NULL,
    [StatioStationID] int  NOT NULL
);
GO

-- Creating table 'Stations'
CREATE TABLE [dbo].[Stations] (
    [StationID] int IDENTITY(1,1) NOT NULL,
    [StationName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [PartsID] in table 'Parts'
ALTER TABLE [dbo].[Parts]
ADD CONSTRAINT [PK_Parts]
    PRIMARY KEY CLUSTERED ([PartsID] ASC);
GO

-- Creating primary key on [ProjectID] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectID] ASC);
GO

-- Creating primary key on [RecutID] in table 'Recuts'
ALTER TABLE [dbo].[Recuts]
ADD CONSTRAINT [PK_Recuts]
    PRIMARY KEY CLUSTERED ([RecutID] ASC);
GO

-- Creating primary key on [UnitID] in table 'Units'
ALTER TABLE [dbo].[Units]
ADD CONSTRAINT [PK_Units]
    PRIMARY KEY CLUSTERED ([UnitID] ASC);
GO

-- Creating primary key on [StatusID] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([StatusID] ASC);
GO

-- Creating primary key on [ScannedID] in table 'Scanneds'
ALTER TABLE [dbo].[Scanneds]
ADD CONSTRAINT [PK_Scanneds]
    PRIMARY KEY CLUSTERED ([ScannedID] ASC);
GO

-- Creating primary key on [StationID] in table 'Stations'
ALTER TABLE [dbo].[Stations]
ADD CONSTRAINT [PK_Stations]
    PRIMARY KEY CLUSTERED ([StationID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------