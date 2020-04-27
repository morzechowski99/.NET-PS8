DROP PROCEDURE sp_productById
GO
CREATE PROCEDURE sp_productById
@ProductID int
AS
SELECT Id, name,price
FROM Product p
Where p.Id = @ProductID