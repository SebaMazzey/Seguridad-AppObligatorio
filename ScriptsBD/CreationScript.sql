-- Script de creación de base de datos para el servidor central
CREATE DATABASE IF NOT EXISTS `DemoSeguridad`;
USE `DemoSeguridad`;

CREATE TABLE `Roles` (
  `Name` varchar(50) NOT NULL,
  `Description` varchar(50),
  PRIMARY KEY (`Name`)
);

CREATE TABLE `Permissions` (
  `Name` varchar(50) NOT NULL,
  `Description` varchar(50),
  PRIMARY KEY (`Name`)
);

CREATE TABLE `Users` (
  `Email` varchar(50),
  `Hash_password` binary(60),
  `Hash_salt` varchar(20),
  `First_name` varchar(50),
  `Last_name` varchar(50),
  `Role_name` varchar(50),
  PRIMARY KEY (`email`),
  FOREIGN KEY (`Role_name`) REFERENCES `Roles` (`Name`)
);

CREATE TABLE `RolesPermissions` (
  `Role_Name` varchar(50) NOT NULL,
  `Permission_Name` varchar(50),
  PRIMARY KEY (`Role_Name`,`Permission_Name`),
  FOREIGN KEY (`Role_name`) REFERENCES `Roles` (`Name`),
  FOREIGN KEY (`Permission_Name`) REFERENCES `Permissions` (`Name`)
);

CREATE TABLE `Authors` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `First_name` varchar(50),
  `Last_name` varchar(50),
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Books` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(50),
  `Description` varchar(50),
  `Content` varchar(500),
  `Author_Id` int NOT NULL,
  PRIMARY KEY (`Id`),
  FOREIGN KEY (`Author_Id`) REFERENCES `Authors` (`Id`)
);


INSERT INTO DemoSeguridad.`Roles` (Name, Description) VALUES
('Administrador', 'Le da al usuario el rol de administrador'),
('Lector', 'Le da al usuario el rol de  lector');

INSERT INTO DemoSeguridad.`Permissions` (Name, Description) VALUES
('Libros.Leer', 'Permiso para leer libros'),
('Libros.Agregar', 'Permiso para agregar libros al sistema'),
('Libros.Eliminar', 'Permiso para eliminar libros del sistema');

INSERT INTO DemoSeguridad.`RolesPermissions` (Role_Name, Permission_Name) VALUES
('Administrador', 'Libros.Leer'),
('Administrador', 'Libros.Agregar'),
('Administrador', 'Libros.Eliminar'),
('Lector', 'Libros.Leer');
