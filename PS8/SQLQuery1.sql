DROP PROCEDURE sp_productList
GO
CREATE PROCEDURE sp_productList
AS
SELECT Id, name,price
FROM Product 
