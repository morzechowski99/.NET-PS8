DROP PROCEDURE sp_productDelete
GO
CREATE PROCEDURE sp_productDelete
@ProductID int
AS
DELETE FROM Product WHERE Id = @ProductID
GO