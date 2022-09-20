CREATE TABLE Brands (
  Id int PRIMARY KEY IDENTITY,
  Name varchar(20)
);

CREATE TABLE Phones (
  Id int PRIMARY KEY IDENTITY,
  BrandId int FOREIGN KEY REFERENCES Brands(Id),
  Type varchar(100)
);

INSERT INTO Brands (Name) VALUES ('Apple');

INSERT INTO Phones (BrandId, Type) VALUES (1, 'iPhone 13');