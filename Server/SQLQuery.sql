CREATE DATABASE [SibersTestDB]
GO

USE [SibersTestDB]
GO

CREATE TABLE [dbo].[Employees] (
[EmployeeId] INT NOT NULL IDENTITY,
[Name] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
[SurName] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
[Patronymic] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
[Email] NVARCHAR(100) NOT NULL,
PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);
GO

CREATE TABLE [dbo].[Projects] (
[ProjectId] INT IDENTITY NOT NULL,
[Name] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
[Customer] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NOT NULL,
[Performer] NVARCHAR(100) COLLATE Cyrillic_General_CI_AS_KS NULL,
[LeadId] INT NULL,
[StartDate] DATE NOT NULL,
[FinishDate] DATE NOT NULL,
[Priority] INT NOT NULL,
FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Employees] ([EmployeeId]),
PRIMARY KEY CLUSTERED ([ProjectId] ASC)
);
GO

CREATE TABLE [dbo].[ProjectsEmployees] (
[Id] INT NOT NULL IDENTITY PRIMARY KEY,
[EmployeeId] INT NOT NULL,
[ProjectId] INT NOT NULL,
FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeId]),
FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([ProjectId])
);
GO