CREATE DATABASE [SibersTestDB]
GO

USE [SibersTestDB]
GO

CREATE TABLE [dbo].[Employees] (
[EmployeeId] INT NOT NULL IDENTITY,
[Name] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[SurName] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[Patronymic] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[Email] NVARCHAR(100) NULL,
PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);
GO

CREATE TABLE [dbo].[Projects] (
[ProjectId] INT IDENTITY NOT NULL,
[Name] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[Customer] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[Performer] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[LeadId] INT NOT NULL,
[StartDate] DATE NULL,
[FinishDate] DATE NULL,
[Priority] INT NULL,
FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Employees] ([EmployeeId]),
PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);
GO

CREATE TABLE [dbo].[Projects_Employees] (
[Id] INT NOT NULL PRIMARY KEY,
[EmployeeId] INT NOT NULL,
[ProjectId] INT NOT NULL,
FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeId]),
FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId])
);
GO