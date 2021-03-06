USE master;
GO

DECLARE @dbname nvarchar(15) = N'TimeManager';
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases  WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
DROP DATABASE TimeManager;
GO

CREATE DATABASE TimeManager;
GO

USE TimeManager;
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'seg')
BEGIN
	DROP SCHEMA seg;
END
GO

CREATE SCHEMA seg;
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'seg' AND  TABLE_NAME = 'Usuario'))
BEGIN
    DROP TABLE seg.Usuario;
END
GO

CREATE TABLE seg.Usuario
(
Id INT NOT NULL PRIMARY KEY IDENTITY ( 1, 1),
Nombre VARCHAR(100) NOT NULL DEFAULT '',
Apellido VARCHAR(100) NOT NULL DEFAULT '',
Correo VARCHAR(100) DEFAULT NULL,
Contrasenia VARCHAR(100) DEFAULT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'cat')
BEGIN
	DROP SCHEMA cat;
END
GO

CREATE SCHEMA cat;
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'cat' AND  TABLE_NAME = 'Departamento'))
BEGIN
    DROP TABLE cat.Departamento;
END
GO

CREATE TABLE cat.Departamento
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'cat' AND  TABLE_NAME = 'TiempoInvertido'))
BEGIN
    DROP TABLE cat.TiempoInvertido;
END
GO

CREATE TABLE cat.TiempoInvertido
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'cat' AND  TABLE_NAME = 'EstadoVisita'))
BEGIN
    DROP TABLE cat.EstadoVisita;
END
GO

CREATE TABLE cat.EstadoVisita
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'neg')
BEGIN
	DROP SCHEMA neg;
END
GO

CREATE SCHEMA neg;
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'neg' AND  TABLE_NAME = 'Cliente'))
BEGIN
    DROP TABLE neg.Cliente;
END
GO

CREATE TABLE neg.Cliente
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'neg' AND  TABLE_NAME = 'Proyecto'))
BEGIN
    DROP TABLE neg.Proyecto;
END
GO

CREATE TABLE neg.Proyecto
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(150) NOT NULL,
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

IF EXISTS (SELECT * FROM sys.schemas WHERE name = 'com')
BEGIN
	DROP SCHEMA com;
END
GO

CREATE SCHEMA com;
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'com' AND  TABLE_NAME = 'Boleta'))
BEGIN
    DROP TABLE com.Boleta;
END
GO

CREATE TABLE com.Boleta
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
NumeroBoleta VARCHAR(50) NOT NULL DEFAULT '',
FechaEntrada SMALLDATETIME NOT NULL DEFAULT GETDATE(),
FechaSalida SMALLDATETIME NOT NULL DEFAULT GETDATE(),
TiempoEfectivo DECIMAL(4,2),
TiempoInvertidoEn INT NOT NULL REFERENCES cat.TiempoInvertido(Id),
ProyectoId INT NOT NULL REFERENCES neg.Proyecto(Id),
ClienteId INT NOT NULL REFERENCES neg.Cliente(Id),
UsuarioId INT NOT NULL REFERENCES seg.Usuario(Id),
DepartamentoId INT NOT NULL REFERENCES cat.Departamento(Id),
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1,
EsCobrable BIT NOT NULL DEFAULT 1
);
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'com' AND  TABLE_NAME = 'Actividad'))
BEGIN
    DROP TABLE com.Actividad;
END
GO

CREATE TABLE com.Actividad
(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
BoletaId INT NOT NULL REFERENCES com.Boleta(Id),
Descripcion NVARCHAR(350) NOT NULL DEFAULT '',
FechaActividad DATE NOT NULL DEFAULT GETDATE(),
TiempoActividad DECIMAL(4,2) NOT NULL DEFAULT 0,
EstadoId INT NOT NULL REFERENCES cat.EstadoVisita(Id),
FechaRegistro SMALLDATETIME NOT NULL DEFAULT GETDATE(),
EsActivo BIT NOT NULL DEFAULT 1
);
GO

-- Inserción de datos
INSERT INTO cat.EstadoVisita(Nombre, FechaRegistro, EsActivo) VALUES ('Terminado', GETDATE(), 1);

INSERT INTO cat.TiempoInvertido(Nombre, FechaRegistro, EsActivo) VALUES ('Administrativo', GETDATE(), 1);

INSERT INTO cat.Departamento(Nombre, FechaRegistro, EsActivo) VALUES ('Desarrollo', GETDATE(), 1);