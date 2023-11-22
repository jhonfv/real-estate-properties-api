-- Crear la base de datos RealEstateDB
CREATE DATABASE Properties;
GO

USE Properties;
GO

-- Crear la tabla Owner
CREATE TABLE Owner (
    IdOwner INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    PhotoPath NVARCHAR(255),
    Birthday DATE NOT NULL
);
GO

-- Crear la tabla Property
CREATE TABLE Property (
    IdProperty INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Price MONEY NOT NULL,
    CodeInternal NVARCHAR(50) NOT NULL,
    Year INT NOT NULL,
    IdOwner INT NOT NULL,
    CONSTRAINT FK_Property_Owner FOREIGN KEY (IdOwner) REFERENCES Owner(IdOwner)
);
GO

-- Crear la tabla PropertyImage
CREATE TABLE PropertyImage (
    IdPropertyImage INT PRIMARY KEY IDENTITY(1,1),
    IdProperty INT NOT NULL,
    FilePath NVARCHAR(255) NOT NULL, 
    Enabled BIT NOT NULL,
    CONSTRAINT FK_PropertyImage_Property FOREIGN KEY (IdProperty) REFERENCES Property(IdProperty)
);
GO

-- Crear la tabla PropertyTrace
CREATE TABLE PropertyTrace (
    IdPropertyTrace INT PRIMARY KEY IDENTITY(1,1),
    DateSale DATE NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Value MONEY NOT NULL,
    Tax MONEY NOT NULL,
    IdProperty INT NOT NULL,
    CONSTRAINT FK_PropertyTrace_Property FOREIGN KEY (IdProperty) REFERENCES Property(IdProperty)
);
GO
