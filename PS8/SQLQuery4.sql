DROP PROCEDURE sp_productAdd
GO
CREATE PROCEDURE sp_productAdd
@name NCHAR (50),
@price MONEY
AS
INSERT INTO Product (name,price) VALUES (@name,@price)