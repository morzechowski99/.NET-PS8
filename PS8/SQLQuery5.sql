DROP PROCEDURE sp_productChange
GO
CREATE PROCEDURE sp_productChange
@name NCHAR (50),
@price MONEY,
@id int
AS
UPDATE PRODUCT 
SET NAME = @name, PRICE = @price
WHERE ID = @id
GO