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
  `Hash_salt` varchar(50),
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

INSERT INTO DemoSeguridad.`Authors` (First_name, Last_name) VALUES
('William', 'Shakespeare'),
('Charles', 'Dickens'),
('J. K.', 'Rowling');

INSERT INTO DemoSeguridad.`Books` (Title, Description, Author_Id, Content) VALUES
('The Merchant of Venice', 'Description of The Merchant of Venice', 1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus at sapien erat. Donec vitae convallis nunc. Nam pharetra, lacus eu volutpat fermentum, quam felis vulputate tellus, et maximus lectus leo eget enim. Vestibulum ac massa orci. Morbi a odio tristique, ullamcorper ante vitae, dictum mi. Donec aliquam tincidunt orci vel pretium. Etiam mi lectus, maximus vitae scelerisque non, dapibus ac dui.'),
('Romeo y Julieta', 'Descripción de Romeo y Julieta', 1, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus at sapien erat. Donec vitae convallis nunc. Nam pharetra, lacus eu volutpat fermentum, quam felis vulputate tellus, et maximus lectus leo eget enim. Vestibulum ac massa orci. Morbi a odio tristique, ullamcorper ante vitae, dictum mi. Donec aliquam tincidunt orci vel pretium. Etiam mi lectus, maximus vitae scelerisque non, dapibus ac dui.'),
('Oliver Twist', 'Description de Oliver Twist', 2, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus at sapien erat. Donec vitae convallis nunc. Nam pharetra, lacus eu volutpat fermentum, quam felis vulputate tellus, et maximus lectus leo eget enim. Vestibulum ac massa orci. Morbi a odio tristique, ullamcorper ante vitae, dictum mi. Donec aliquam tincidunt orci vel pretium. Etiam mi lectus, maximus vitae scelerisque non, dapibus ac dui.'),
('Harry Potter y la piedra filosofal ', 'Description de Harry Potter y la piedra filosofal ', 3, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus at sapien erat. Donec vitae convallis nunc. Nam pharetra, lacus eu volutpat fermentum, quam felis vulputate tellus, et maximus lectus leo eget enim. Vestibulum ac massa orci. Morbi a odio tristique, ullamcorper ante vitae, dictum mi. Donec aliquam tincidunt orci vel pretium. Etiam mi lectus, maximus vitae scelerisque non, dapibus ac dui.');

INSERT INTO DemoSeguridad.`Users` (Email, First_name, Last_name, Role_Name, Hash_password, Hash_salt) VALUES
('admin@gmail.com', 'Admin', 'Admin', 'Administrador', X'243261243131244A505741732F4E61555558676A6F6C7947657657732E53523143674A674A43693568476A7869586956725259787879365675654965', 'yCLta1cN4ydZiFyvPXGm3KD/ge58TfR9Eckaoqj1'),
('johndoe@gmail.com', 'John', 'Doe', 'Lector', X'243261243131246F323561775A4530373244446759695243364643734F796C586F7735765965574C32536B4D57505442316931694F46533678394861', 'bQjkpKsfgUhzQ783GVEQMVfu/4urlb+AHViD/MuR');

