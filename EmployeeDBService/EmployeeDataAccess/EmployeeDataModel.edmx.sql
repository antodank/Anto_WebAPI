
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/28/2020 23:20:33
-- Generated from EDMX file: F:\PRACS\WebAPI\Github_WebAPI\Anto_WebAPI\EmployeeDBService\EmployeeDataAccess\EmployeeDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbSample];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[TblUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TblUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Gender] nvarchar(50)  NULL,
    [Salary] int  NULL
);
GO

-- Creating table 'TblUsers'
CREATE TABLE [dbo].[TblUsers] (
    [EmpId] int IDENTITY(1,1) NOT NULL,
    [Fname] nvarchar(30)  NOT NULL,
    [Mname] nvarchar(30)  NULL,
    [Lname] nvarchar(30)  NULL,
    [Gender] nchar(10)  NOT NULL,
    [EMail] nvarchar(50)  NOT NULL,
    [DOB] nvarchar(30)  NOT NULL,
    [MaritalStatus] nvarchar(30)  NOT NULL,
    [Hobbies] nvarchar(30)  NULL,
    [Telephone] nvarchar(30)  NULL,
    [Mobile] nvarchar(30)  NULL,
    [Address] nvarchar(300)  NOT NULL,
    [PinCode] nvarchar(30)  NOT NULL,
    [State] nvarchar(30)  NOT NULL,
    [Nationality] nvarchar(30)  NOT NULL,
    [DOJ] nvarchar(30)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmpId] in table 'TblUsers'
ALTER TABLE [dbo].[TblUsers]
ADD CONSTRAINT [PK_TblUsers]
    PRIMARY KEY CLUSTERED ([EmpId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------