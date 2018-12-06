USE master;
GO

DROP DATABASE DapperCSharpDb;
GO

CREATE DATABASE DapperCSharpDb;
GO

USE DapperCSharpDb;
GO

CREATE TABLE Category (
    Id UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT PK_Category_Id
            PRIMARY KEY (Id),
    Description VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Person (
    Id UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT PK_Person_Id
            PRIMARY KEY (Id),
    Name VARCHAR(100) NOT NULL,    
    Email VARCHAR(255) NULL,
    CategoryId UNIQUEIDENTIFIER NULL
        CONSTRAINT FK_dbo_Person_CategoryId_dbo_Category_Id
            FOREIGN KEY (CategoryId) REFERENCES Category(Id)
);
GO

DECLARE @Id uniqueidentifier = NEWID();
INSERT 
    INTO [DapperCSharpDb].[dbo].[Person] (Id, Name, Email)
    VALUES (@Id, 'Test ' + CAST(@Id AS VARCHAR(38)), 'test' + CAST(@Id AS VARCHAR(38)) + '@email.com');
GO 10

DECLARE @Id uniqueidentifier = NEWID();
INSERT 
    INTO [DapperCSharpDb].[dbo].[Category] (Id, Description)
    VALUES (@Id, 'Test ' + CAST(@Id AS VARCHAR(38)));
GO 3