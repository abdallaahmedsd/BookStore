-- SQL Migration Script
-- Created on: 02/11/2025 16:29:41


-- Retrieve  order data
CREATE PROCEDURE Sales.SP_GetOrdersByID
    @Id INT                   
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
      
		IF Sales.Fun_IsOrderExistsById(@Id) <> 1
            THROW 50002, 'Order not found or has been deleted.', 1;

        SELECT Id, OrderDate, TotalAmoumt, Status, UserID
        FROM Sales.Orders
        WHERE Id = @Id and IsDeleted = 0;

    END TRY
    BEGIN CATCH
        -- Handle any errors
        THROW;
    END CATCH;
END;
