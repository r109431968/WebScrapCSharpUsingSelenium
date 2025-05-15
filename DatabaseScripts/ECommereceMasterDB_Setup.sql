CREATE DATABASE ECommereceDB;
GO

USE ECommereceDB;
GO

CREATE TABLE ProductDetails (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Price VARCHAR(100),
    Rating VARCHAR(10),
    Description VARCHAR(MAX)
);
GO

CREATE PROCEDURE SPAddProduct
    @Name VARCHAR(100),
    @Price VARCHAR(100),
    @Rating VARCHAR(10),
    @Description VARCHAR(MAX)
AS
BEGIN
    INSERT INTO ProductDetails (Name, Price, Rating, Description)
    VALUES (@Name, @Price, @Rating, @Description);
END
GO

Select * from ProductDetails